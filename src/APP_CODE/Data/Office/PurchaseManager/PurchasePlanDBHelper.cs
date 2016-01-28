/**********************************************
 * 类作用：   采购计划数据库层处理
 * 建立人：   王保军   
 * 建立时间： 2009/04/16
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

namespace XBase.Data.Office.PurchaseManager
{
    public  class PurchasePlanDBHelper
    {
        #region 选择产品信息
        public static DataTable GetProductInfo()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT isnull(A.ID,0) AS ProductID                                                 ");
            sql.AppendLine("      ,isnull(A.ProdNo,'') AS ProductNo                                            ");
            sql.AppendLine("      ,isnull(A.ProductName,'') AS ProductName                                     ");
            sql.AppendLine("      ,isnull(A.UnitID,0) AS UnitID                                                ");
            sql.AppendLine("      ,isnull(B.CodeName,'') AS UnitName                                           ");
            sql.AppendLine("      ,isnull(A.Specification,'') AS Specification                                 ");
            sql.AppendLine("      ,isnull(A.TaxBuy,0) AS UnitPrice                                             ");
            sql.AppendLine("      ,isnull(A.StandardBuy,0) AS StandardBuy                                      ");
            sql.AppendLine("FROM officedba.ProductInfo AS A                                                    ");
            sql.AppendLine("LEFT JOIN officedba.CodeUnitType AS B ON A.CompanyCD=B.CompanyCD AND A.UnitID=B.ID ");
            sql.AppendLine(" WHERE A.CompanyCD=@CompanyCD");
            sql.AppendLine(" AND A.UsedStatus=@UsedStatus");

            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", "1"));

            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 选择采购申请
        public static DataTable GetPurchaseApply(string CompanyCD, string ProductNo, string ProductName, string StartDate, string EndDate
            , int pageIndex, int pageCount, string OrderBy, string OrderByType, ref int totalCount, string PurchaseArriveEFIndex, string PurchaseArriveEFDesc)
        {
            SqlParameter[] parms = new SqlParameter[10];
            parms[0] = new SqlParameter("@CompanyCD", CompanyCD);
            parms[1] = new SqlParameter("@ProductNo", ProductNo);
            parms[2] = new SqlParameter("@ProductName", ProductName);
            parms[3] = new SqlParameter("@StartDate", StartDate);
            parms[4] = new SqlParameter("@EndDate", EndDate);
            parms[5] = new SqlParameter("@pageIndex", pageIndex);
            parms[6] = new SqlParameter("@pageCount", pageCount);
            parms[7] = new SqlParameter("@OrderBy", OrderBy);
            parms[8] = new SqlParameter("@OrderByType", OrderByType);
            parms[9] = MakeParam("@totalCount", SqlDbType.Int, 0, ParameterDirection.Output, null);
            DataTable dt = SqlHelper.ExecuteStoredProcedure("[officedba].[ProcgetPurApp]", parms);
            totalCount = int.Parse(parms[9].Value.ToString());
            return dt;
        }
        #endregion

        #region 选择采购需求
        public static DataTable GetPurchaseRequire(string CompanyCD, string ProductNo, string ProductName, string StartDate, string EndDate
            , int pageIndex, int pageCount, string OrderBy,string OrderByType, ref int totalCount)
        {
            SqlParameter[] parms = new SqlParameter[10];
            parms[0] = new SqlParameter("@CompanyCD",CompanyCD);
            parms[1] = new SqlParameter("@ProductNo",ProductNo);
            parms[2] = new SqlParameter("@ProductName",ProductName);
            parms[3] = new SqlParameter("@StartDate",StartDate);
            parms[4] = new SqlParameter("@EndDate",EndDate);
            parms[5] = new SqlParameter("@pageIndex",pageIndex);
            parms[6] = new SqlParameter("@pageCount",pageCount);
            parms[7] = new SqlParameter("@OrderBy",OrderBy);
            parms[8] = new SqlParameter("@OrderByType", OrderByType);
            parms[9] = MakeParam("@totalCount", SqlDbType.Int, 0, ParameterDirection.Output, null);									

            DataTable dt = SqlHelper.ExecuteStoredProcedure("[officedba].[ProcgetPurRqr]", parms);
            totalCount =  int.Parse(parms[9].Value.ToString());
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

        #region 采购计划主表操作
        #region 采购计划主表插入
        public static SqlCommand InsertPurchasePlanPrimary(PurchasePlanModel PurchasePlanM)
        {
            SqlCommand comm = new SqlCommand();

            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("INSERT INTO officedba.PurchasePlan ");
            sql.AppendLine("           (CompanyCD              ");
            sql.AppendLine("           ,PlanNo                 ");
            sql.AppendLine("           ,Title                  ");
            sql.AppendLine("           ,FromType               ");
            sql.AppendLine("           ,PlanUserID             ");
            sql.AppendLine("           ,Purchaser              ");
            sql.AppendLine("           ,PlanDate               ");
            sql.AppendLine("           ,PlanDeptID             ");
            sql.AppendLine("           ,PlanMoney              ");
            sql.AppendLine("           ,CountTotal             ");
            sql.AppendLine("           ,CurrencyType           ");
            sql.AppendLine("           ,Rate                   ");
            sql.AppendLine("           ,BillStatus             ");
            sql.AppendLine("           ,TypeID                 ");
            sql.AppendLine("           ,Remark                 ");
            sql.AppendLine("           ,Creator                ");
            sql.AppendLine("           ,CreateDate             ");
            sql.AppendLine("           ,Confirmor              ");
            sql.AppendLine("           ,ConfirmDate            ");
            sql.AppendLine("           ,Closer                 ");
            sql.AppendLine("           ,CloseDate              ");
            sql.AppendLine("           ,ModifiedDate           ");
            sql.AppendLine("           ,ModifiedUserID         ");
            sql.AppendLine("           )                       ");
            sql.AppendLine("     VALUES(                       ");
            sql.AppendLine("    		@CompanyCD             ");
            sql.AppendLine("           ,@PlanNo                ");
            sql.AppendLine("           ,@Title                 ");
            sql.AppendLine("           ,@FromType              ");
            sql.AppendLine("           ,@PlanUserID            ");
            sql.AppendLine("           ,@Purchaser             ");
            sql.AppendLine("           ,@PlanDate              ");
            sql.AppendLine("           ,@PlanDeptID            ");
            sql.AppendLine("           ,@PlanMoney             ");
            sql.AppendLine("           ,@CountTotal            ");
            sql.AppendLine("           ,@CurrencyType          ");
            sql.AppendLine("           ,@Rate                  ");
            sql.AppendLine("           ,@BillStatus            ");
            sql.AppendLine("           ,@TypeID                ");
            sql.AppendLine("           ,@Remark                ");
            sql.AppendLine("           ,@Creator               ");
            sql.AppendLine("           ,@CreateDate            ");
            sql.AppendLine("           ,@Confirmor             ");
            sql.AppendLine("           ,@ConfirmDate           ");
            sql.AppendLine("           ,@Closer                ");
            sql.AppendLine("           ,@CloseDate             ");
            sql.AppendLine("           ,@ModifiedDate          ");
            sql.AppendLine("           ,@ModifiedUserID        ");
            sql.AppendLine("     )                             ");
            sql.AppendLine("set @IndexID = @@IDENTITY");
            #endregion

            #region 传参
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanNo", PurchasePlanM.PlanNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", PurchasePlanM.Title));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromType", PurchasePlanM.FromType));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanUserID", PurchasePlanM.PlanUserID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Purchaser", PurchasePlanM.Purchaser));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanDate", PurchasePlanM.PlanDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanDeptID", PurchasePlanM.PlanDeptID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanMoney", PurchasePlanM.PlanMoney));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CountTotal", PurchasePlanM.CountTotal));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CurrencyType", PurchasePlanM.CurrencyType));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Rate", PurchasePlanM.Rate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", PurchasePlanM.BillStatus));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TypeID", PurchasePlanM.TypeID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", PurchasePlanM.Remark));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator", PurchasePlanM.Creator));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateDate", PurchasePlanM.CreateDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Confirmor", PurchasePlanM.Confirmor));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ConfirmDate", PurchasePlanM.ConfirmDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Closer", PurchasePlanM.Closer));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CloseDate", PurchasePlanM.CloseDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedDate", PurchasePlanM.ModifiedDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", PurchasePlanM.ModifiedUserID));

            SqlParameter IndexID = new SqlParameter("@IndexID", SqlDbType.Int);
            IndexID.Direction = ParameterDirection.Output;
            comm.Parameters.Add(IndexID);
            #endregion

            comm.CommandText = sql.ToString();

            return comm;
        }
        #endregion

        #region 采购计划主表更新
        public static SqlCommand UpdatePurchasePlanPrimary(PurchasePlanModel PurchasePlanM)
        {
            SqlCommand comm = new SqlCommand();

            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE officedba.PurchasePlan          ");
            sql.AppendLine("   SET CompanyCD     =@CompanyCD       ");
            sql.AppendLine("      ,PlanNo        =@PlanNo          ");
            sql.AppendLine("      ,Title         =@Title           ");
            sql.AppendLine("      ,FromType      =@FromType        ");
            sql.AppendLine("      ,PlanUserID    =@PlanUserID      ");
            sql.AppendLine("      ,Purchaser     =@Purchaser       ");
            sql.AppendLine("      ,PlanDate      =@PlanDate        ");
            sql.AppendLine("      ,PlanDeptID    =@PlanDeptID      ");
            sql.AppendLine("      ,PlanMoney     =@PlanMoney       ");
            sql.AppendLine("      ,CountTotal    =@CountTotal      ");
            sql.AppendLine("      ,CurrencyType  =@CurrencyType    ");
            sql.AppendLine("      ,Rate          =@Rate            ");
            sql.AppendLine("      ,BillStatus    =@BillStatus      ");
            sql.AppendLine("      ,TypeID        =@TypeID          ");
            sql.AppendLine("      ,Remark        =@Remark          ");
            sql.AppendLine("      ,Creator       =@Creator         ");
            sql.AppendLine("      ,CreateDate    =@CreateDate      ");
            sql.AppendLine("      ,Confirmor     =@Confirmor       ");
            sql.AppendLine("      ,ConfirmDate   =@ConfirmDate     ");
            sql.AppendLine("      ,Closer        =@Closer          ");
            sql.AppendLine("      ,CloseDate     =@CloseDate       ");
            sql.AppendLine("      ,ModifiedDate  =getDate()  ");
            sql.AppendLine("      ,ModifiedUserID=@ModifiedUserID  ");
            sql.AppendLine(" WHERE ID=@ID                          ");
            #endregion

            #region 传参
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanNo", PurchasePlanM.PlanNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", PurchasePlanM.Title));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromType", PurchasePlanM.FromType));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanUserID", PurchasePlanM.PlanUserID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Purchaser", PurchasePlanM.Purchaser));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanDate", PurchasePlanM.PlanDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanDeptID", PurchasePlanM.PlanDeptID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanMoney", PurchasePlanM.PlanMoney));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CountTotal", PurchasePlanM.CountTotal));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CurrencyType", PurchasePlanM.CurrencyType));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Rate", PurchasePlanM.Rate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", PurchasePlanM.BillStatus));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TypeID", PurchasePlanM.TypeID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", PurchasePlanM.Remark));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator", PurchasePlanM.Creator));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateDate", PurchasePlanM.CreateDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Confirmor", PurchasePlanM.Confirmor));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ConfirmDate", PurchasePlanM.ConfirmDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Closer", PurchasePlanM.Closer));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CloseDate", PurchasePlanM.CloseDate));
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedDate", PurchasePlanM.ModifiedDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", PurchasePlanM.ModifiedUserID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", PurchasePlanM.ID));
            #endregion

            comm.CommandText = sql.ToString();

            return comm;
        }
        #endregion

        #region 采购计划主表批量删除
        public static SqlCommand DeletePurchasePlanPrimary(string IDs)
        {
            SqlCommand comm = new SqlCommand();
            string sql = "delete officedba.PurchasePlan where CompanyCD=@CompanyCD and ID in ("+IDs+") ";

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));

            comm.CommandText = sql;
            return comm;
        }

        #endregion

        #region 采购计划主表检索
        public static DataTable SelectPurchasePlanPrimary(PurchasePlanModel PurchasePlanM, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            SqlCommand comm = new SqlCommand();

            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT isnull(A.ID          ,0)   AS  ID                                                 ");
            sql.AppendLine("      ,isnull(A.PlanNo      ,'')  AS  PlanNo                                             ");
            sql.AppendLine("      ,isnull(A.Title       ,'')  AS  PlanTitle                                          ");
            sql.AppendLine("      ,isnull(A.PlanUserID  ,0)   AS  PlanUserID                                         ");
            sql.AppendLine("      ,isnull(B.EmployeeName,'')  AS  PlanUserName                                       ");
            sql.AppendLine("      ,isnull(CONVERT(varchar(100), A.PlanDate, 23),'') as PlanDate                   ");
            sql.AppendLine("      ,isnull(A.PlanDeptID  ,0)   AS  PlanDeptID                                         ");
            sql.AppendLine("	  	,isnull(C.DeptName    ,'')  AS  PlanDeptName                                     ");
            sql.AppendLine("      ,isnull(A.PlanMoney   , 0)  AS  PlanMoney                                          ");
            sql.AppendLine("      ,isnull(A.BillStatus  ,'')  AS  BillStatus                                         ");
            sql.AppendLine(" ,isnull(A.CountTotal,0) AS CountTotal ");
            sql.AppendLine(" ,case A.BillStatus when '1' then '制单' when '2' then '执行' when '3' then '变更'       ");
            sql.AppendLine(" when '4' then '手动结单' when '5' then '自动结单' else '出错了' end AS BillStatusName   ");
            sql.AppendLine("	,isnull(D.FlowStatus,'')  AS FlowStatus                                              ");
            sql.AppendLine(" ,case D.FlowStatus when '1' then '待审批' when '2' then '审批中' when '3' then '审批通过' ");
            sql.AppendLine(" when '4' then '审批不通过'  when '5' then '撤销审批' else '' END AS FlowStatusName,isnull( A.ModifiedDate,'') AS ModifiedDate");
            sql.AppendLine("  FROM officedba.PurchasePlan AS A                                                       ");
            sql.AppendLine("  LEFT JOIN officedba.EmployeeInfo AS B ON A.CompanyCD=B.CompanyCD AND A.PlanUserID=B.ID ");
            sql.AppendLine("  LEFT JOIN officedba.DeptInfo AS C ON A.CompanyCD=C.CompanyCD AND A.PlanDeptID=C.ID     ");
            sql.AppendLine("  LEFT JOIN officedba.FlowInstance AS D ON A.CompanyCD=D.CompanyCD AND D.BillTypeFlag=6  ");
            sql.AppendLine("  AND D.BillTypeCode=2 AND A.ID=D.BillID                                 ");
            sql.AppendLine(" AND D.ID=(SELECT max(ID) FROM officedba.FlowInstance AS F WHERE A.ID = F.BillID AND F.BillTypeFlag = 6 AND F.BillTypeCode = 2)");
            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine(" AND A.CompanyCD=@CompanyCD");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", PurchasePlanM.CompanyCD));
            if (PurchasePlanM.PlanNo != "")
            {
                sql.AppendLine(" AND  A.PlanNo like @PlanNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanNo", "%" + PurchasePlanM.PlanNo + "%"));
            }
            if (PurchasePlanM.Title != "")
            {
                sql.AppendLine("  AND A.Title like @Title ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", "%" + PurchasePlanM.Title + "%"));
            }
            if (PurchasePlanM.PlanUserID != "")
            {
                sql.AppendLine(" AND A.PlanUserID = @PlanUserID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanUserID", PurchasePlanM.PlanUserID));
            }
            if(PurchasePlanM.PlanMoney !="")
            {
                sql.AppendLine(" AND A.PlanMoney >= @PlanMoney");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanMoney", PurchasePlanM.PlanMoney));
            }
            if (PurchasePlanM.TotalMoneyMax != "")
            {
                sql.AppendLine(" AND A.PlanMoney <= @TotalMoneyMax");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalMoneyMax", PurchasePlanM.TotalMoneyMax));
            }
            if (PurchasePlanM.PlanDeptID != "")
            {
                sql.AppendLine(" AND A.PlanDeptID = @PlanDeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanDeptID", PurchasePlanM.PlanDeptID));
            }
            if (PurchasePlanM.PlanDate != "")
            {
                sql.AppendLine(" AND A.PlanDate >= @PlanDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanDate", PurchasePlanM.PlanDate));
            }
            if (PurchasePlanM.EndPlanDate != "")
            {
                sql.AppendLine(" AND A.PlanDate <= @EndPlanDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndPlanDate", PurchasePlanM.EndPlanDate));
            }
            if (PurchasePlanM.FlowStatus != "0")
            {
                if (PurchasePlanM.FlowStatus == "")
                {
                    sql.AppendLine(" AND D.FlowStatus is null");
                }
                else
                {
                    sql.AppendLine(" AND D.FlowStatus = @FlowStatus ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@FlowStatus", PurchasePlanM.FlowStatus));
                }
            }
            if (PurchasePlanM.BillStatus != "0")
            {
                sql.AppendLine(" AND A.BillStatus = @BillStatus");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", PurchasePlanM.BillStatus));
            }



            if (!string.IsNullOrEmpty(PurchasePlanM.EFIndex) && !string.IsNullOrEmpty(PurchasePlanM.EFDesc))
            {
                sql.AppendLine(" and a.ExtField" + PurchasePlanM.EFIndex + " LIKE @EFDesc");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + PurchasePlanM.EFDesc + "%"));
            }
            #endregion
            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);

            //return SqlHelper.ExecuteSql(sql.ToString());
        }


        public static DataTable SelectPurchasePlanPrimary(PurchasePlanModel PurchasePlanM, string OrderBy)
        {
            SqlCommand comm = new SqlCommand();

            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT isnull(A.ID          ,0)   AS  ID                                                 ");
            sql.AppendLine("      ,isnull(A.PlanNo      ,'')  AS  PlanNo                                             ");
            sql.AppendLine("      ,isnull(A.Title       ,'')  AS  PlanTitle                                          ");
            sql.AppendLine("      ,isnull(A.PlanUserID  ,0)   AS  PlanUserID                                         ");
            sql.AppendLine("      ,isnull(B.EmployeeName,'')  AS  PlanUserName                                       ");
            sql.AppendLine("      ,isnull(CONVERT(varchar(100), A.PlanDate, 23),'') as PlanDate                   ");
            sql.AppendLine("      ,isnull(A.PlanDeptID  ,0)   AS  PlanDeptID                                         ");
            sql.AppendLine("	  	,isnull(C.DeptName    ,'')  AS  PlanDeptName                                     ");
            sql.AppendLine("      ,isnull(A.PlanMoney   , 0)  AS  PlanMoney                                          ");
            sql.AppendLine("      ,isnull(A.BillStatus  ,'')  AS  BillStatus                                         ");
            sql.AppendLine(" ,isnull(A.CountTotal,0) AS CountTotal ");
            sql.AppendLine(" ,case A.BillStatus when '1' then '制单' when '2' then '执行' when '3' then '变更'       ");
            sql.AppendLine(" when '4' then '手动结单' when '5' then '自动结单' else '出错了' end AS BillStatusName   ");
            sql.AppendLine("	,isnull(D.FlowStatus,'')  AS FlowStatus                                              ");
            sql.AppendLine(" ,case D.FlowStatus when '1' then '待审批' when '2' then '审批中' when '3' then '审批通过' ");
            sql.AppendLine(" when '4' then '审批不通过' else '' END AS FlowStatusName");
            sql.AppendLine("  FROM officedba.PurchasePlan AS A                                                       ");
            sql.AppendLine("  LEFT JOIN officedba.EmployeeInfo AS B ON A.CompanyCD=B.CompanyCD AND A.PlanUserID=B.ID ");
            sql.AppendLine("  LEFT JOIN officedba.DeptInfo AS C ON A.CompanyCD=C.CompanyCD AND A.PlanDeptID=C.ID     ");
            sql.AppendLine("  LEFT JOIN officedba.FlowInstance AS D ON A.CompanyCD=D.CompanyCD AND D.BillTypeFlag=6  ");
            sql.AppendLine("  AND D.BillTypeCode=2 AND A.ID=D.BillID                                 ");
            sql.AppendLine(" AND D.ID=(SELECT max(ID) FROM officedba.FlowInstance AS F WHERE A.ID = F.BillID AND F.BillTypeFlag = 6 AND F.BillTypeCode = 2)");
            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine(" AND A.CompanyCD=@CompanyCD");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", PurchasePlanM.CompanyCD));
            if (PurchasePlanM.PlanNo != "")
            {
                sql.AppendLine(" AND  A.PlanNo like @PlanNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanNo", "%" + PurchasePlanM.PlanNo + "%"));
            }
            if (PurchasePlanM.Title != "")
            {
                sql.AppendLine("  AND A.Title like @Title ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", "%" + PurchasePlanM.Title + "%"));
            }
            if (PurchasePlanM.PlanUserID != "")
            {
                sql.AppendLine(" AND A.PlanUserID = @PlanUserID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanUserID", PurchasePlanM.PlanUserID));
            }
            if (PurchasePlanM.PlanMoney != "")
            {
                sql.AppendLine(" AND A.PlanMoney >= @PlanMoney");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanMoney", PurchasePlanM.PlanMoney));
            }
            if (PurchasePlanM.TotalMoneyMax != "")
            {
                sql.AppendLine(" AND A.PlanMoney <= @TotalMoneyMax");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalMoneyMax", PurchasePlanM.TotalMoneyMax));
            }
            if (PurchasePlanM.PlanDeptID != "")
            {
                sql.AppendLine(" AND A.PlanDeptID = @PlanDeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanDeptID", PurchasePlanM.PlanDeptID));
            }
            if (PurchasePlanM.PlanDate != "")
            {
                sql.AppendLine(" AND A.PlanDate >= @PlanDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanDate", PurchasePlanM.PlanDate));
            }
            if (PurchasePlanM.EndPlanDate != "")
            {
                sql.AppendLine(" AND A.PlanDate <= @EndPlanDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndPlanDate", PurchasePlanM.EndPlanDate));
            }
            if (PurchasePlanM.FlowStatus != "0")
            {
                if (PurchasePlanM.FlowStatus == "")
                {
                    sql.AppendLine(" AND D.FlowStatus is null");
                }
                else
                {
                    sql.AppendLine(" AND D.FlowStatus = @FlowStatus ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@FlowStatus", PurchasePlanM.FlowStatus));
                }
            }
            if (PurchasePlanM.BillStatus != "0")
            {
                sql.AppendLine(" AND A.BillStatus = @BillStatus");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", PurchasePlanM.BillStatus));
            }

            if (!string.IsNullOrEmpty(PurchasePlanM.EFIndex) && !string.IsNullOrEmpty(PurchasePlanM.EFDesc))
            {
                sql.AppendLine(" and a.ExtField" + PurchasePlanM.EFIndex + " LIKE @EFDesc");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + PurchasePlanM.EFDesc + "%"));
            }



            sql.AppendLine(" ORDER BY "+OrderBy+"");
            #endregion
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);

            //return SqlHelper.ExecuteSql(sql.ToString());
        }
        #endregion

        #region 判断采购计划有没有被引用
        public static bool IsCitePurPlan(string ID)
        {
            bool IsCite = false;
            //采购询价引用
            if (!IsCite)
            {
                string sql = "SELECT ID FROM officedba.PurchaseAskPriceDetail WHERE FromType=@FromType AND FromBillID=@ID";
                SqlParameter[] parameters = {
                    new SqlParameter("@FromType", SqlDbType.VarChar),
					new SqlParameter("@ID", SqlDbType.VarChar),};
                parameters[0].Value = "2";
                parameters[1].Value = ID;
                IsCite = SqlHelper.Exists(sql, parameters);
            }
            //采购合同引用
            if (!IsCite)
            {
                string sql = "SELECT ID FROM officedba.PurchaseContract WHERE FromType=@FromType AND FromBillID=@ID";
                SqlParameter[] parameters = {
                    new SqlParameter("@FromType", SqlDbType.VarChar),
					new SqlParameter("@ID", SqlDbType.VarChar),};
                parameters[0].Value = "2";
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
                parameters[0].Value = "2";
                parameters[1].Value = ID;
                IsCite = SqlHelper.Exists(sql, parameters);
            }
            return IsCite;
        }
        #endregion

        #region 获取某条采购计划主表信息
        public static DataTable GetPurchasePlanPrimary(string ID)
        {

            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID                                                                                                               ");
            sql.AppendLine("      ,A.CompanyCD                                                                                                        ");
            sql.AppendLine("      ,isnull(A.PlanNo    ,'')  AS PlanNo                                                                                 ");
            sql.AppendLine("      ,isnull(A.Title     ,'')  AS Title                                                                                  ");
            sql.AppendLine("      ,isnull(A.FromType  ,'')  AS FromType                                                                               ");
            sql.AppendLine("      ,isnull(case A.FromType when '0' then '无来源' when '1' then '采购申请单' when '2' then '采购需求' else '出错了' end ,'')  AS FromTypeName   ");
            sql.AppendLine("      ,isnull(A.PlanUserID                    ,'')  AS PlanUserID                                                         ");
            sql.AppendLine("		  ,isnull(B.EmployeeName   ,'') AS PlanUserName                                                                       ");
            sql.AppendLine("      ,isnull(A.Purchaser  ,'')  AS  PurchaserID                                                                            ");
            sql.AppendLine("		  ,isnull(C.EmployeeName ,'')   AS PurchaserName                                                                      ");
            sql.AppendLine("      ,isnull(CONVERT(varchar(100), A.PlanDate, 23),'') as PlanDate                                                       ");
            sql.AppendLine("      ,isnull(A.PlanDeptID               ,0)  AS  PlanDeptID                                                              ");
            sql.AppendLine("		  ,isnull(D.DeptName ,'') AS  PlanDeptName                                                                            ");
            sql.AppendLine("      ,Convert(numeric(22," + userInfo.SelPoint + "),a.PlanMoney) as PlanMoney                                                          ");
            sql.AppendLine("      , Convert(numeric(22," + userInfo.SelPoint + "),a.CountTotal) as CountTotal                                            ");
            sql.AppendLine("      ,isnull(A.CurrencyType             ,0)  AS  CurrencyType                                                            ");
            sql.AppendLine("      ,Convert(numeric(12," + userInfo.SelPoint + "),a.Rate) as Rate                                                                  ");
            sql.AppendLine("      ,isnull(A.BillStatus               ,'') AS  BillStatus                                                              ");
            sql.AppendLine("			,isnull(case A.BillStatus when '1' then '制单' when '2' then '执行' when '3' then '变更'  when '4' then '手工结单'  ");
            sql.AppendLine("			 when '5' then '自动结单' else '出错了' end ,'') AS BillStatusName                                                  ");
            sql.AppendLine("      ,isnull(A.TypeID,0) AS  TypeID                                                                                      ");
            sql.AppendLine("	  ,isnull(E.TypeName,'') AS  TypeName                                                                                   ");
            sql.AppendLine("      ,isnull(A.Remark,'')  AS  Remark                                                                                    ");
            sql.AppendLine("      ,isnull(A.Creator,0) AS Creator                                                                                     ");
            sql.AppendLine("      ,isnull(F.EmployeeName,'') AS CreatorName                                                                           ");
            sql.AppendLine("      ,isnull(CONVERT(varchar(100), A.CreateDate, 23),'') as CreateDate                                                   ");
            sql.AppendLine("      ,isnull(A.Confirmor ,0)  AS Confirmor                                                                               ");
            sql.AppendLine("	  ,isnull(G.EmployeeName,'') AS ConfirmorName                                                                           ");
            sql.AppendLine("      ,isnull(CONVERT(varchar(100), A.ConfirmDate, 23),'') as ConfirmDate                                                 ");
            sql.AppendLine("      ,isnull(A.Closer,0) AS Closer                                                                                       ");
            sql.AppendLine("	  ,isnull(H.EmployeeName,'') AS CloserName                                                                              ");
            sql.AppendLine("      ,isnull(CONVERT(varchar(100), A.CloseDate, 23),'') as CloseDate                                                     ");
            sql.AppendLine("      ,isnull(CONVERT(varchar(100), A.ModifiedDate, 23),'') as ModifiedDate                                               ");
            sql.AppendLine("      ,isnull(A.ModifiedUserID,0) AS ModifiedUserID                                                                       "); 
            sql.AppendLine("      ,isnull(J.FlowStatus,'') AS FlowStatus ");
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

            sql.AppendLine("  FROM officedba.PurchasePlan AS A                                                                                        ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS B ON A.CompanyCD=B.CompanyCD AND A.PlanUserID=B.ID                                    ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS C ON A.CompanyCD=C.CompanyCD AND A.Purchaser=C.ID                                    ");
            sql.AppendLine("LEFT JOIN officedba.DeptInfo AS D ON A.CompanyCD=D.CompanyCD AND A.PlanDeptID=D.ID                                        ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS E ON A.CompanyCD=E.CompanyCD AND A.TypeID=E.ID                                      ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS F ON A.CompanyCD=F.CompanyCD AND A.Creator=F.ID                                    ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS G ON A.CompanyCD=G.CompanyCD AND A.Confirmor=G.ID                                     ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS H ON A.CompanyCD=H.CompanyCD AND A.Closer=H.ID                                        ");
            sql.AppendLine("LEFT JOIN officedba.UserInfo AS I ON A.CompanyCD=I.CompanyCD AND A.ModifiedUserID=I.UserID                                ");
            sql.AppendLine(" LEFT JOIN officedba.FlowInstance AS J ON A.CompanyCD=J.CompanyCD AND A.ID = J.BillID AND J.BillTypeFlag = 6 AND J.BillTypeCode = 2    ");
            sql.AppendLine(" AND J.ID=(SELECT max(ID) FROM officedba.FlowInstance AS K WHERE A.CompanyCD=K.CompanyCD AND A.ID = K.BillID AND K.BillTypeFlag = 6 AND K.BillTypeCode = 2 )");            
            sql.AppendLine(" WHERE 1=1 AND A.CompanyCD = '" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "'");
            sql.AppendLine(" AND A.ID = '" +ID+ "'");
            #endregion

            return SqlHelper.ExecuteSql(sql.ToString());
        }
        #endregion

        #region 确认
        //判断单据是不是可以确认
        public static bool CanConfirmPurPlan(string ID,out string Reason)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT B.SortNo                                         ");
                sql.AppendLine("FROM  officedba.PurchasePlan AS A INNER JOIN            ");
                sql.AppendLine("  officedba.PurchasePlanSource AS B                     ");
                sql.AppendLine("  ON A.CompanyCD = B.CompanyCD AND A.PlanNo = B.PlanNo  ");
                sql.AppendLine("  AND B.FromType = '2' LEFT OUTER JOIN                  ");
                sql.AppendLine("  officedba.PurchaseRequire AS C ON B.FromBillID = C.ID ");
                sql.AppendLine("WHERE     A.ID = @ID AND C.ProdID IS NULL               ");

                SqlCommand comm = new SqlCommand();
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID",ID));
                comm.CommandText = sql.ToString();
                DataTable dt = SqlHelper.ExecuteSearch(comm);
                Reason = string.Empty;
                if (dt.Rows.Count > 0)
                {
                    Reason = "第(";
                    for (int i = 0; i < dt.Rows.Count; ++i)
                    {
                        Reason += ""+dt.Rows[i]["SortNo"].ToString()+",";
                    }
                    Reason = Reason.Remove(Reason.Length - 1, 1);
                    Reason += ")条明细的源单已被删除，不能确认";
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static SqlCommand ConfirmPurchasePlan(string ID)
        {
            SqlCommand comm = new SqlCommand();

            #region SQL文
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("update officedba.PurchasePlan set ");
            strSql.AppendLine(" Confirmor =@Confirmor");
            strSql.AppendLine(" ,BillStatus=@BillStatus");
            strSql.AppendLine(" ,ConfirmDate=getdate()");
            strSql.AppendLine(" ,ModifiedUserID=@ModifiedUserID");
            strSql.AppendLine(" ,ModifiedDate=getdate()");
            strSql.AppendLine(" where");
            strSql.AppendLine(" ID= @ID");
            #endregion

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Confirmor", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", "2"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));

            comm.CommandText = strSql.ToString();
            return comm;
        }

        //确认时回写采购申请
        public static SqlCommand WritePurchaseApply(ProductModel ProductM)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE officedba.PurchaseApplyDetail");
            sql.AppendLine(" SET PlanedCount=isnull(PlanedCount,0)+@PlanCount");
            sql.AppendLine(" WHERE CompanyCD=@CompanyCD");
            sql.AppendLine(" AND ApplyNo=@FromBillNo");
            sql.AppendLine(" AND SortNo=@FromLineNo");

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromBillNo", ProductM.FromBillNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromLineNo", ProductM.FromLineNo));
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanCount", ProductM.UsedUnitCount));
            }
            else
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanCount", ProductM.ProductCount));
            }
          
            comm.CommandText = sql.ToString();
            return comm;
        }
        

        //确认时更改采购需求中已订购数量
        public static SqlCommand WritePurchaseRequire(ProductModel ProductM)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE officedba.PurchaseRequire ");
            sql.AppendLine("SET OrderCount=isnull(OrderCount,0)+@OrderCount");
            sql.AppendLine(" WHERE ID=@ID");

            //if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            //{
            //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderCount", ProductM.UsedUnitCount));
            //}
            //else
            //{
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderCount", ProductM.ProductCount));
            //}
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ProductM.FromBillID));
            comm.CommandText = sql.ToString();
            return comm;
        }

        
        #endregion

        #region 取消确认
        //更改主表
        public static SqlCommand CancelConfirm(string ID)
        {
            SqlCommand comm = new SqlCommand();

            #region SQL文
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("update officedba.PurchasePlan set ");
            strSql.AppendLine(" Confirmor =null");
            strSql.AppendLine(" ,BillStatus=@BillStatus");
            strSql.AppendLine(" ,ConfirmDate=null");
            strSql.AppendLine(" ,ModifiedUserID=@ModifiedUserID");
            strSql.AppendLine(" ,ModifiedDate=getdate()");
            strSql.AppendLine(" where");
            strSql.AppendLine(" ID= @ID");
            #endregion

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", "1"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));

            comm.CommandText = strSql.ToString();
            return comm;
        }
        //取消确认时回写采购申请
        public static SqlCommand WritePurchaseApplyDesc(ProductModel ProductM)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE officedba.PurchaseApplyDetail");
            sql.AppendLine(" SET PlanedCount=isnull(PlanedCount,0)-@PlanCount");
            sql.AppendLine(" WHERE CompanyCD=@CompanyCD");
            sql.AppendLine(" AND ApplyNo=@FromBillNo");
            sql.AppendLine(" AND SortNo=@FromLineNo");

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromBillNo", ProductM.FromBillNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromLineNo", ProductM.FromLineNo));
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanCount", ProductM.UsedUnitCount));
            }
            else
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanCount", ProductM.ProductCount));
            }
            comm.CommandText = sql.ToString();
            return comm;
        }

        //取消确认时，更改采购需求中已订购数量
        public static SqlCommand WritePurchaseRequireDesc(ProductModel ProductM)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE officedba.PurchaseRequire ");
            sql.AppendLine("SET OrderCount=isnull(OrderCount,0)-@OrderCount");
            sql.AppendLine(" WHERE ID=@ID");
            //if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            //{
            //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanCount", ProductM.UsedUnitCount));
            //}
            //else
            //{
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderCount", ProductM.ProductCount));
            //}
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ProductM.FromBillID));
            comm.CommandText = sql.ToString();
            return comm;
        }
        #endregion
        #region 结单
        public static bool ClosePurchasePlan(string ID)
        {

            #region SQL文
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("update officedba.PurchasePlan set ");
            strSql.AppendLine(" Closer =  @Closer");
            strSql.AppendLine(" ,BillStatus=@BillStatus");
            strSql.AppendLine(" ,CloseDate=getdate()");
            strSql.AppendLine(" ,ModifiedUserID=@ModifiedUserID");
            strSql.AppendLine(" ,ModifiedDate=getdate()");
            strSql.AppendLine(" where");
            strSql.AppendLine(" ID= @ID");
            #endregion
            SqlParameter[] param = new SqlParameter[4];
            param[0] = SqlHelper.GetParameterFromString("@Closer", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString());
            param[1] = SqlHelper.GetParameterFromString("@BillStatus", "4");
            param[2] = SqlHelper.GetParameterFromString("@ModifiedUserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID);
            param[3] = SqlHelper.GetParameterFromString("@ID", ID);
            #region 参数

            int count = SqlHelper.ExecuteTransSql(strSql.ToString(), param);
            if (count > 0)
            {
                return true;
            }
            return false;
            #endregion
        }
        #endregion

        #region 取消结单
        public static bool CancelClosePurchasePlan(string ID)
        {
            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update officedba.PurchasePlan set ");
            sql.AppendLine("  Closer = null");
            sql.AppendLine(" ,CloseDate = null");
            sql.AppendLine(" ,BillStatus = @BillStatus");
            sql.AppendLine(" ,ModifiedUserID=@ModifiedUserID");
            sql.AppendLine(" ,ModifiedDate=getdate()");
            sql.AppendLine(" where ID= @ID");

            SqlParameter[] param = new SqlParameter[3];
            param[0] = SqlHelper.GetParameterFromString("@BillStatus", "2");
            param[1] = SqlHelper.GetParameterFromString("@ModifiedUserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID);
            param[2] = SqlHelper.GetParameterFromString("@ID", ID);
            #endregion

            return SqlHelper.ExecuteTransSql(sql.ToString(), param) > 0 ? true : false;
        }
        #endregion
        #endregion

        #region 采购计划明细来源表操作
        #region 新增
        public static SqlCommand InsertPurchasePlanSource(PurchasePlanSourceModel PurchasePlanSourceM)
        {
            SqlCommand comm = new SqlCommand();

            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("INSERT INTO officedba.PurchasePlanSource ");
            sql.AppendLine("          ( CompanyCD                    ");
            sql.AppendLine("           ,SortNo                       ");
            sql.AppendLine("           ,PlanNo                       ");
            sql.AppendLine("           ,FromType                     ");
            sql.AppendLine("           ,FromBillID                   ");
            sql.AppendLine("           ,FromSortNo                   ");
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            {
                sql.AppendLine("           ,UsedUnitID                        ");
                sql.AppendLine("           ,UsedUnitCount                         ");
                sql.AppendLine("           ,UsedPrice                        ");
                sql.AppendLine("           ,ExRate                        ");
            } 
            sql.AppendLine("           ,ProductID                    ");
            sql.AppendLine("           ,ProductNo                    ");
            sql.AppendLine("           ,ProductName                  ");
            sql.AppendLine("           ,UnitID                       ");
            sql.AppendLine("           ,UnitPrice                    ");
            sql.AppendLine("           ,TotalPrice                   ");
            sql.AppendLine("           ,RequireCount                 ");
            sql.AppendLine("           ,RequireDate                  ");
            sql.AppendLine("           ,ProviderID                   ");
            sql.AppendLine("           ,ApplyReason                  ");
            sql.AppendLine("           ,Remark                       ");
            sql.AppendLine("           ,PlanCount                    ");
            sql.AppendLine("           ,PlanTakeDate                 ");
            sql.AppendLine("           ,ModifiedDate                 ");
            sql.AppendLine("           ,ModifiedUserID               ");
            sql.AppendLine("           )                             ");
            sql.AppendLine("     VALUES(                             ");
            sql.AppendLine(" 			@CompanyCD         	         ");
            sql.AppendLine("           ,@SortNo                      ");
            sql.AppendLine("           ,@PlanNo                      ");
            sql.AppendLine("           ,@FromType                    ");
            sql.AppendLine("           ,@FromBillID                  ");
            sql.AppendLine("           ,@FromSortNo                  ");
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            {
                sql.AppendLine("           ,@UsedUnitID                        ");
                sql.AppendLine("           ,@UsedUnitCount                         ");
                sql.AppendLine("           ,@UsedPrice                        ");
                sql.AppendLine("           ,@ExRate                        ");
            } 
            sql.AppendLine("           ,@ProductID                   ");
            sql.AppendLine("           ,@ProductNo                   ");
            sql.AppendLine("           ,@ProductName                 ");
            sql.AppendLine("           ,@UnitID                      ");
            sql.AppendLine("           ,@UnitPrice                   ");
            sql.AppendLine("           ,@TotalPrice                  ");
            sql.AppendLine("           ,@RequireCount                ");
            sql.AppendLine("           ,@RequireDate                 ");
            sql.AppendLine("           ,@ProviderID                  ");
            sql.AppendLine("           ,@ApplyReason                 ");
            sql.AppendLine("           ,@Remark                      ");
            sql.AppendLine("           ,@PlanCount                   ");
            sql.AppendLine("           ,@PlanTakeDate                ");
            sql.AppendLine("           ,@ModifiedDate                ");
            sql.AppendLine("           ,@ModifiedUserID              ");
            sql.AppendLine(")                                        ");

            #endregion

            #region 传参
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedUnitID", PurchasePlanSourceM.UsedUnitID));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedUnitCount", PurchasePlanSourceM.UsedUnitCount));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedPrice", PurchasePlanSourceM.UsedPrice));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ExRate", PurchasePlanSourceM.ExRate));
            }

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SortNo", PurchasePlanSourceM.SortNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanNo", PurchasePlanSourceM.PlanNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromType", PurchasePlanSourceM.FromType));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromBillID", PurchasePlanSourceM.FromBillID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromSortNo", PurchasePlanSourceM.FromSortNo));
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromDeptID", PurchasePlanSourceM.FromDeptID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", PurchasePlanSourceM.ProductID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductNo", PurchasePlanSourceM.ProductNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", PurchasePlanSourceM.ProductName));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UnitID", PurchasePlanSourceM.UnitID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UnitPrice", PurchasePlanSourceM.UnitPrice));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalPrice", PurchasePlanSourceM.TotalPrice));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@RequireCount", PurchasePlanSourceM.RequireCount));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@RequireDate", PurchasePlanSourceM.RequireDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", PurchasePlanSourceM.ProviderID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ApplyReason", PurchasePlanSourceM.ApplyReason));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", PurchasePlanSourceM.Remark));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanCount", PurchasePlanSourceM.PlanCount));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanTakeDate", PurchasePlanSourceM.PlanTakeDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedDate", PurchasePlanSourceM.ModifiedDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", PurchasePlanSourceM.ModifiedUserID));
            #endregion

            comm.CommandText = sql.ToString();
            return comm;
        }
        #endregion

        #region 删除
        public static SqlCommand DeletePurchasePlanSource(string PlanNos)
        {
            SqlCommand comm = new SqlCommand();

            string sql = "delete officedba.PurchasePlanSource where CompanyCD=@CompanyCD and PlanNo in ('"+PlanNos+"')";

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));

            comm.CommandText = sql;
            return comm;
        }
        #endregion

        #region 查询
        public static DataTable GetPurchasePlanSource(string ID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT isnull(A.ID             ,0)  AS ID                                                  ");
            sql.AppendLine("      ,isnull(A.SortNo      ,0) AS SortNo                                           ");
            sql.AppendLine("      ,isnull(A.CompanyCD      ,'') AS CompanyCD                                           ");
            sql.AppendLine("      ,isnull(A.PlanNo         ,'') AS PlanNo                                              ");
            sql.AppendLine("      ,isnull(A.FromType       ,'') AS FromType                                            ");
            sql.AppendLine("      ,case A.FromType when 0 then '无来源' when '1' then '采购申请单' when '2' then '采购需求'  ");
            sql.AppendLine("      else '出错了' end AS FromTypeName                                                    ");
            sql.AppendLine("      ,isnull(A.FromBillID     ,0)  AS FromBillID                                          ");
            sql.AppendLine(" ,case A.FromType when '1' then (select ApplyNo FROM officedba.PurchaseApply WHERE ID=A.FromBillID) ");
            sql.AppendLine(" when '2' then (select L.MRPNo FROM officedba.PurchaseRequire AS K INNER JOIN officedba.MRP AS L ON K.MRPCD = L.ID WHERE K.ID=A.FromBillID) else '' END AS FromBillNo    ");
            //sql.AppendLine("      ,isnull(B.ApplyNo ,'')        AS FromBillNo                                          ");
            sql.AppendLine("      ,isnull(A.FromSortNo     ,0)  AS FromSortNo                                          ");
            sql.AppendLine("      ,isnull(A.ProductID      ,0)  AS ProductID                                           ");
            sql.AppendLine("      ,isnull(F.ProdNo      ,'') AS ProductNo                                           ");
            sql.AppendLine("      ,isnull(F.ProductName    ,'') AS ProductName                                         ");
            sql.AppendLine("      ,isnull(F.Specification,'')AS Specification");
            sql.AppendLine("      ,isnull(F.UnitID         ,0)  AS UnitID                                              ");
            sql.AppendLine("      ,isnull(C.CodeName ,'')       AS UnitName                                            ");
            sql.AppendLine("      , Convert(numeric(14," + userInfo.SelPoint + "),isnull(a.UnitPrice,0)) as UnitPrice                                          ");
            sql.AppendLine("      ,Convert(numeric(22," + userInfo.SelPoint + "),isnull(a.TotalPrice,0)) as TotalPrice                                       ");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),isnull(a.RequireCount,0)) as RequireCount                                      ");
            sql.AppendLine("      ,isnull(CONVERT(varchar(100), A.RequireDate, 23) ,'') AS RequireDate                                         ");
            sql.AppendLine("      ,isnull(A.ProviderID     ,0)  AS ProviderID                                          ");
            sql.AppendLine("      ,isnull(D.CustName ,'')       AS ProviderName                                        ");
            sql.AppendLine("      ,isnull(A.ApplyReason    ,0)  AS ApplyReasonID                                       ");
            sql.AppendLine("      ,isnull(E.CodeName ,'')       AS ApplyReasonName                                     ");
            sql.AppendLine("      ,isnull(A.Remark         ,'') AS Remark                                              ");
            sql.AppendLine("      ,     Convert(numeric(14," + userInfo.SelPoint + "),isnull(a.PlanCount,0)) as PlanCount                                ");
            sql.AppendLine("      ,isnull(ff.CodeName ,'')       AS UsedUnitName                                            ");
            sql.AppendLine("      ,isnull(A.UsedUnitID      ,0)  AS UsedUnitID                                           ");
            sql.AppendLine("      ,  Convert(numeric(14," + userInfo.SelPoint + "),isnull(a.UsedUnitCount,0)) as UsedUnitCount                                   ");
            sql.AppendLine("      , Convert(numeric(14," + userInfo.SelPoint + "),isnull(a.UsedPrice,0)) as UsedPrice                                      ");
            

            sql.AppendLine("      ,isnull(CONVERT(varchar(100), A.PlanTakeDate, 23)  ,'') AS PlanTakeDate                                        ");
            sql.AppendLine("      ,isnull(CONVERT(varchar(100), A.ModifiedDate, 23)  ,'') AS ModifiedDate                                        ");
            sql.AppendLine("      ,isnull(A.ModifiedUserID ,0)  AS ModifiedUserID  ,isnull(H.TypeName,'') as ColorName                                    ");
            sql.AppendLine("  FROM officedba.PurchasePlanSource AS A                                                   ");
            //sql.AppendLine(" LEFT JOIN officedba.PurchaseApply AS B ON A.CompanyCD=B.CompanyCD AND A.FromBillID=B.ID   ");
            sql.AppendLine(" LEFT JOIN officedba.CodeUnitType AS C ON A.CompanyCD=C.CompanyCD AND A.UnitID=C.ID        ");
            sql.AppendLine(" LEFT JOIN officedba.ProviderInfo AS D ON A.CompanyCD=D.CompanyCD AND A.ProviderID=D.ID    ");
            sql.AppendLine(" LEFT JOIN officedba.CodeReasonType AS E ON A.CompanyCD=E.CompanyCD AND A.ApplyReason=E.ID ");
            sql.AppendLine(" LEFT JOIN officedba.CodeUnitType AS ff ON A.CompanyCD=ff.CompanyCD AND A.UsedUnitID=ff.ID        ");
            sql.AppendLine(" LEFT JOIN officedba.ProductInfo AS F ON A.CompanyCD=F.CompanyCD AND A.ProductID=F.ID");
            sql.AppendLine("left join officedba.CodePublicType H on F.ColorID=H.ID");
            sql.AppendLine(" INNER JOIN officedba.PurchasePlan AS G ON A.CompanyCD=G.CompanyCD AND A.PlanNo=G.PlanNo AND G.ID=@ID");
            sql.AppendLine(" WHERE 1=1 ");

            sql.AppendLine(" AND A.CompanyCD=@CompanyCD");

            SqlParameter[] param = new SqlParameter[2];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            param[1] = SqlHelper.GetParameter("@ID", ID);
            #endregion

            return SqlHelper.ExecuteSql(sql.ToString(),param);            
        }
        #endregion
        #endregion

        #region 采购计划明细表操作
        #region 新增
        public static SqlCommand InsertPurchasePlanDetail(PurchasePlanDetailModel PurchasePlanDetailM)
        {
            SqlCommand comm = new SqlCommand();

            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("INSERT INTO officedba.PurchasePlanDetail");
            sql.AppendLine("          ( CompanyCD                   ");
            sql.AppendLine("           ,PlanNo                      ");
            sql.AppendLine("           ,SortNo                      ");
            sql.AppendLine("           ,ProductID                   ");
            sql.AppendLine("           ,ProductNo                   ");
            sql.AppendLine("           ,ProductName                 ");
            sql.AppendLine("           ,UnitID                      ");
            sql.AppendLine("           ,UnitPrice                   ");
            sql.AppendLine("           ,TotalPrice                  ");
            sql.AppendLine("           ,ProductCount                ");
            sql.AppendLine("           ,RequireDate                 ");
            sql.AppendLine("           ,ProviderID                  ");
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            {
                sql.AppendLine("           ,UsedUnitID                        ");
                sql.AppendLine("           ,UsedUnitCount                         ");
                sql.AppendLine("           ,UsedPrice                        ");
                sql.AppendLine("           ,ExRate                        ");
            } 
            sql.AppendLine("           ,OrderCount                  ");
            sql.AppendLine("           ,ModifiedDate                ");
            sql.AppendLine("           ,ModifiedUserID              ");
            sql.AppendLine("           )                            ");
            sql.AppendLine("     VALUES(                            ");
            sql.AppendLine("     		@CompanyCD                  ");
            sql.AppendLine("           ,@PlanNo                     ");
            sql.AppendLine("           ,@SortNo                     ");
            sql.AppendLine("           ,@ProductID                  ");
            sql.AppendLine("           ,@ProductNo                  ");
            sql.AppendLine("           ,@ProductName                ");
            sql.AppendLine("           ,@UnitID                     ");
            sql.AppendLine("           ,@UnitPrice                  ");
            sql.AppendLine("           ,@TotalPrice                 ");
            sql.AppendLine("           ,@ProductCount               ");
            sql.AppendLine("           ,@RequireDate                ");
            sql.AppendLine("           ,@ProviderID                 ");
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            {
                sql.AppendLine("           ,@UsedUnitID                        ");
                sql.AppendLine("           ,@UsedUnitCount                         ");
                sql.AppendLine("           ,@UsedPrice                        ");
                sql.AppendLine("           ,@ExRate                        ");
            } 
            sql.AppendLine("           ,@OrderCount                 ");
            sql.AppendLine("           ,@ModifiedDate               ");
            sql.AppendLine("           ,@ModifiedUserID             ");
            sql.AppendLine("           )                            ");

            #endregion

            #region 传参
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedUnitID", PurchasePlanDetailM.UsedUnitID));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedUnitCount", PurchasePlanDetailM.UsedUnitCount));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedPrice", PurchasePlanDetailM.UsedPrice));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ExRate", PurchasePlanDetailM.ExRate));
            }

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanNo", PurchasePlanDetailM.PlanNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SortNo", PurchasePlanDetailM.SortNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", PurchasePlanDetailM.ProductID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductNo", PurchasePlanDetailM.ProductNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", PurchasePlanDetailM.ProductName));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UnitID", PurchasePlanDetailM.UnitID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UnitPrice", PurchasePlanDetailM.UnitPrice));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TotalPrice", PurchasePlanDetailM.TotalPrice));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductCount", PurchasePlanDetailM.ProductCount));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@RequireDate", PurchasePlanDetailM.RequireDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", PurchasePlanDetailM.ProviderID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderCount", PurchasePlanDetailM.OrderCount));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedDate", PurchasePlanDetailM.ModifiedDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", PurchasePlanDetailM.ModifiedUserID));

            #endregion

            comm.CommandText = sql.ToString();
            return comm;
        }
        #endregion

        #region 删除
        public static SqlCommand DeletePurchasePlanDetail(string PlanNos)
        {
            SqlCommand comm = new SqlCommand();
            string sql = "delete officedba.PurchasePlanDetail where CompanyCD=@CompanyCD and PlanNo in ('"+PlanNos+"')";

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));

            comm.CommandText = sql;
            return comm;
        }
        #endregion

        #region 查询
        public static DataTable GetPurchasePlanDetail(string ID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT isnull(A.ID             ,0) AS ID                                               ");
            sql.AppendLine("      ,isnull(A.CompanyCD      ,'') AS CompanyCD                                       ");
            sql.AppendLine("      ,isnull(A.PlanNo         ,'') AS PlanNo                                          ");
            sql.AppendLine("      ,isnull(A.SortNo         ,0) AS SortNo                                           ");
            sql.AppendLine("      ,isnull(A.ProductID      ,0) AS ProductID                                        ");
            sql.AppendLine("      ,isnull(D.ProdNo      ,'') AS ProductNo                                       ");
            sql.AppendLine("      ,isnull(D.ProductName    ,'') AS ProductName                                     ");
            sql.AppendLine("      ,isnull(D.Specification  ,'')AS Specification");
            sql.AppendLine("      ,isnull(A.UnitID         ,0) AS UnitID                                           ");
            sql.AppendLine("      ,isnull(B.CodeName       ,'') AS UnitName                                        ");
            sql.AppendLine("      ,  Convert(numeric(14," + userInfo.SelPoint + "),isnull(a.UnitPrice,0)) as UnitPrice                         ");
            sql.AppendLine("      ,Convert(numeric(22," + userInfo.SelPoint + "),isnull(a.TotalPrice,0)) as TotalPrice                            ");
            sql.AppendLine("      ,   Convert(numeric(22," + userInfo.SelPoint + "),isnull(a.ProductCount,0)) as ProductCount                               ");
            sql.AppendLine("      ,isnull(CONVERT(varchar(100), A.RequireDate, 23) ,'') AS RequireDate                                     ");
            sql.AppendLine("      ,isnull(A.ProviderID     ,0) AS ProviderID                                       ");
            sql.AppendLine("      ,isnull(C.CustName       ,'') AS ProviderName                                    ");
            //sql.AppendLine("      ,isnull(A.Remark         ,'') AS Remark                                          ");
            sql.AppendLine("      ,     Convert(numeric(14," + userInfo.SelPoint + "),isnull(a.OrderCount,0)) as OrderCount                                       ");
            sql.AppendLine("      ,isnull(CONVERT(varchar(100), A.ModifiedDate, 23) ,'') AS ModifiedDate                                    ");
            sql.AppendLine("      ,isnull(A.ModifiedUserID ,'') AS ModifiedUserID                                  ");

            sql.AppendLine("      ,isnull(A.UsedUnitID      ,0)  AS UsedUnitID                                           ");
            sql.AppendLine("      ,     Convert(numeric(14," + userInfo.SelPoint + "),isnull(a.UsedUnitCount,0)) as UsedUnitCount                                          ");
            sql.AppendLine("      ,    Convert(numeric(14," + userInfo.SelPoint + "),isnull(a.UsedPrice,0)) as UsedPrice                                           ");
            sql.AppendLine("      ,  Convert(numeric(12," + userInfo.SelPoint + "),isnull(a.ExRate,0)) as ExRate                                           ");
            sql.AppendLine("      ,isnull(F.CodeName       ,'') AS UsedUnitName,isnull(H.TypeName,'') as ColorName                                        ");

            sql.AppendLine("  FROM officedba.PurchasePlanDetail AS A                                               ");
            sql.AppendLine("LEFT JOIN officedba.CodeUnitType AS B ON A.CompanyCD=B.CompanyCD AND A.UnitID=B.ID     ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS C ON A.CompanyCD=C.CompanyCD AND A.ProviderID=C.ID ");
            sql.AppendLine(" LEFT JOIN officedba.ProductInfo AS D ON A.CompanyCD=D.CompanyCD AND A.ProductID=D.ID");
            sql.AppendLine("LEFT JOIN officedba.CodeUnitType AS F ON A.CompanyCD=f.CompanyCD AND A.UsedUnitID=F.ID     ");
            sql.AppendLine("left join officedba.CodePublicType H on D.ColorID=H.ID");
            sql.AppendLine(" INNER JOIN officedba.PurchasePlan AS E ON A.CompanyCD=E.CompanyCD AND A.PlanNo=E.PlanNo AND E.ID=@ID");
            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine(" AND A.CompanyCD=@CompanyCD");

            SqlParameter[] param = new SqlParameter[2];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            param[1] = SqlHelper.GetParameter("@ID", ID);
            #endregion

            return SqlHelper.ExecuteSql(sql.ToString(), param);
        }
        #endregion
        #endregion

        #region 根据物品ID选择推荐供应商
        public static string GetRcmPrv(string ProductID)
        {
            SqlCommand comm = new SqlCommand();

            string sql = "SELECT TOP 1 A.ProductID, B.ID AS ProviderID, B.CustNo AS ProviderNo, B.CustName AS ProviderName\n"
                         + " FROM         officedba.ProviderProduct AS A INNER JOIN\n"
                         + " officedba.ProviderInfo AS B ON A.CompanyCD = B.CompanyCD AND A.CustNo = B.CustNo\n"
                         + " WHERE A.ProductID=@ProductID ";
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID",ProductID));
            comm.CommandText = sql;
            DataTable dt = SqlHelper.ExecuteSearch(comm);

            if (dt.Rows.Count == 0)
                return "|";
            return  dt.Rows[0]["ProviderID"].ToString() + "|" + dt.Rows[0]["ProviderName"].ToString();
        }
        #endregion
    }
}

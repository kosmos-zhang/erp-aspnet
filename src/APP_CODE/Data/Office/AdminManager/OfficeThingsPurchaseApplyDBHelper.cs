/**********************************************                                                                                                                            
 * 类作用：   采购申请增、删、改、查(行政模块)                                                                                                                                     
 * 建立人：   王保军                
 * 建立时间： 2009/06/18        
 ***********************************************/


using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using XBase.Model.Office.AdminManager;
using XBase.Model.Office.ProductionManager;
using XBase.Model.Office.SellManager;
using System.Collections;
using XBase.Common;

namespace XBase.Data.Office.AdminManager
{
  public   class OfficeThingsPurchaseApplyDBHelper
    {
      public static DataTable SelectDetailsUC(string CompanyCD, string ProductNo, string ProductName, string StartDate, string EndDate
            , int pageIndex, int pageCount, string OrderBy, ref int totalCount)
      {
               SqlCommand comm = new SqlCommand();
               StringBuilder sql = new StringBuilder();
               UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
               sql.AppendLine("SELECT A.ID   as FromBillID      ");
            sql.AppendLine("      ,  isnull(A.CompanyCD      ,'') AS CompanyCD       ");
            sql.AppendLine("      , isnull(A.ApplyNo        ,'') AS FromBillNo        ");
            sql.AppendLine("      ,A.SortNo     ");
            sql.AppendLine("     ,A.ThingID  as ProductID                                                   ");
            sql.AppendLine("     ,isnull(GG.ThingNo, '') as ProductNo                                                    ");
            sql.AppendLine("     ,isnull(GG.ThingName,'') as     ProductName                                               ");
            sql.AppendLine("     ,GG.UnitID                                                        ");
            sql.AppendLine("     ,isnull(B.TypeName,'')  AS UnitName                                          ");
            sql.AppendLine("     ,Convert(numeric(14," + userInfo.SelPoint + "),isnull(a.RequireCount,0)) as RequireCount                           "); 
            sql.AppendLine("     ,CONVERT(varchar(100), A.RequireDate, 23) as RequireDate         ");
            sql.AppendLine("     ,  Convert(numeric(14," + userInfo.SelPoint + "),isnull(a.InCount,0)) as InCount                                           ");
            sql.AppendLine("     ,isnull(GG.ThingType,'') AS Specification  ");
            sql.AppendLine("     ,isnull( C.TypeName,'') AS TypeName  ");
            sql.AppendLine("FROM officedba.OfficeThingsPurchaseApplyDetail AS A                         ");
            sql.AppendLine("left outer join officedba.OfficeThingsInfo as GG  on a.CompanyCD=GG.CompanyCD and a.ThingID=GG.id                        ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS B ON GG.UnitID=B.ID                ");
            sql.AppendLine(" LEFT JOIN officedba.CodePublicType AS C ON GG.TypeID =C.ID    ");
            sql.AppendLine("INNER JOIN officedba.OfficeThingsPurchaseApply AS E ON A.CompanyCD=E.CompanyCD AND A.ApplyNo=E.ApplyNo  ");
        

           

        

          comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
          if (!string .IsNullOrEmpty (ProductNo)   )
          {
              sql.AppendLine("     AND GG.ThingNo like '%'+ @ProductNo +'%'         ");
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductNo", ProductNo));
          }
          if (  !string .IsNullOrEmpty (ProductName)  )
          {
              sql.AppendLine("	 AND GG.ThingName like '%'+ @ProductName +'%'           ");
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", ProductName));
          }


          if (  !string .IsNullOrEmpty (StartDate))
          {
              sql.AppendLine("	 AND E.ApplyDate >= @StartApplyDate ");
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartApplyDate", StartDate));
          }
          if ( !string .IsNullOrEmpty ( EndDate) )
          {
              sql.AppendLine("	 AND E.ApplyDate <= @EndApplyDate ");
              comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndApplyDate", EndDate));
          }
        


          comm.CommandText = sql.ToString();
          return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount); 
      }


        #region 主表操作
        #region select
        public static DataTable SelectPrimary(OfficeThingsPurchaseApplyModel PurchaseApplyM, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID");
            sql.AppendLine("     , isnull(A.CompanyCD   ,'') AS CompanyCD                 ");
            sql.AppendLine("      , isnull(A.ApplyNo     ,'') AS ApplyNo                   ");
            sql.AppendLine("      , isnull(A.Subject       ,'') AS Title                     "); 
            sql.AppendLine("      , isnull(A.ApplyUserID ,0) AS ApplyUserID               ");
            sql.AppendLine("      , isnull(C.EmployeeName,'') AS ApplyUserName              ");
            sql.AppendLine("      , isnull(A.ApplyDeptID ,0) AS ApplyDeptID               ");
            sql.AppendLine("      , isnull(D.DeptName    ,'') AS ApplyDeptName                  ");
            sql.AppendLine("      , CONVERT(varchar(100), A.ApplyDate, 23) AS ApplyDate                 ");
            sql.AppendLine("      , isnull(A.BillStatus  ,'') AS BillStatus                ");
            sql.AppendLine("      , case A.BillStatus when '1' then '制单' when '2' then '执行' when '3' then '变更'");
            sql.AppendLine("      when '4' then '手工结单' when '5' then '自动结单' end AS BillStatusName ");
            sql.AppendLine(" ,isnull(A.ModifiedDate,'') AS ModifiedDate");
            sql.AppendLine(" FROM  officedba.OfficeThingsPurchaseApply AS A                             ");
            sql.AppendLine(" INNER JOIN officedba.EmployeeInfo AS C ON A.ApplyUserID = C.ID ");
            sql.AppendLine(" INNER JOIN officedba.DeptInfo AS D ON A.ApplyDeptID = D.ID     ");
            sql.AppendLine("WHERE A.CompanyCD = @CompanyCD");

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            if (PurchaseApplyM.ApplyNo != "" && PurchaseApplyM.ApplyNo != null)
            {
                sql.AppendLine("     AND A.ApplyNo like '%'+ @ApplyNo +'%'         ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ApplyNo", PurchaseApplyM.ApplyNo));
            }
            if (PurchaseApplyM.Subject != "" && PurchaseApplyM.Subject != null)
            {
                sql.AppendLine("	 AND A.Subject like '%'+ @Title +'%'           ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", PurchaseApplyM.Subject));
            }
            if (PurchaseApplyM.ApplyUserID != "" && PurchaseApplyM.ApplyUserID != null)
            {
                sql.AppendLine("	 AND A.ApplyUserID = @ApplyUserID   ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ApplyUserID", PurchaseApplyM.ApplyUserID));
            }
            if (PurchaseApplyM.ApplyDeptID != "" && PurchaseApplyM.ApplyDeptID != null)
            {
                sql.AppendLine("	 AND A.ApplyDeptID = @ApplyDeptID     ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ApplyDeptID", PurchaseApplyM.ApplyDeptID));
            }
            
            if (PurchaseApplyM.StartApplyDate != "")
            {
                sql.AppendLine("	 AND A.ApplyDate >= @StartApplyDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartApplyDate", PurchaseApplyM.StartApplyDate));
            }
            if (PurchaseApplyM.EndApplyDate != "")
            {
                sql.AppendLine("	 AND A.ApplyDate <= @EndApplyDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndApplyDate", PurchaseApplyM.EndApplyDate));
            }
            if (PurchaseApplyM.BillStatus != "0")
            {
                sql.AppendLine("	 AND A.BillStatus = @BillStatus ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", PurchaseApplyM.BillStatus));
            }
          
         
            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
            //return SqlHelper.ExecuteSearch(comm);
        }

        //导出用
        public static DataTable SelectPrimary(OfficeThingsPurchaseApplyModel PurchaseApplyM, string OrderBy)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID");
            sql.AppendLine("     , isnull(A.CompanyCD   ,'') AS CompanyCD                 ");
            sql.AppendLine("      , isnull(A.ApplyNo     ,'') AS ApplyNo                   ");
            sql.AppendLine("      , isnull(A.Subject       ,'') AS Title                     ");  
            sql.AppendLine("      , isnull(A.ApplyUserID ,0) AS ApplyUserID               ");
            sql.AppendLine("      , isnull(C.EmployeeName,'') AS ApplyUserName              ");
            sql.AppendLine("      , isnull(A.ApplyDeptID ,0) AS ApplyDeptID               ");
            sql.AppendLine("      , isnull(D.DeptName    ,'') AS ApplyDeptName                  ");
            sql.AppendLine("      , CONVERT(varchar(100), A.ApplyDate, 23) AS ApplyDate                 ");
            sql.AppendLine("      , isnull(A.BillStatus  ,'') AS BillStatus                ");
            sql.AppendLine("      , case A.BillStatus when '1' then '制单' when '2' then '执行' when '3' then '变更'");
            sql.AppendLine("      when '4' then '手工结单' when '5' then '自动结单' end AS BillStatusName ");
            sql.AppendLine(" FROM  officedba.OfficeThingsPurchaseApply AS A                             "); 
            sql.AppendLine(" INNER JOIN officedba.EmployeeInfo AS C ON A.ApplyUserID = C.ID ");
            sql.AppendLine(" INNER JOIN officedba.DeptInfo AS D ON A.ApplyDeptID = D.ID     "); 
            sql.AppendLine("WHERE A.CompanyCD = @CompanyCD");

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            if (PurchaseApplyM.ApplyNo != "" && PurchaseApplyM.ApplyNo != null)
            {
                sql.AppendLine("     AND A.ApplyNo like '%'+ @ApplyNo +'%'         ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ApplyNo", PurchaseApplyM.ApplyNo));
            }
            if (PurchaseApplyM.Subject != "" && PurchaseApplyM.Subject != null)
            {
                sql.AppendLine("	 AND A.Title like '%'+ @Title +'%'           ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", PurchaseApplyM.Subject));
            }
            if (PurchaseApplyM.ApplyUserID != "" && PurchaseApplyM.ApplyUserID != null)
            {
                sql.AppendLine("	 AND A.ApplyUserID = @ApplyUserID   ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ApplyUserID", PurchaseApplyM.ApplyUserID));
            }
            if (PurchaseApplyM.ApplyDeptID != "" && PurchaseApplyM.ApplyDeptID != null)
            {
                sql.AppendLine("	 AND A.ApplyDeptID = @ApplyDeptID     ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ApplyDeptID", PurchaseApplyM.ApplyDeptID));
            }
             
            if (PurchaseApplyM.StartApplyDate != "")
            {
                sql.AppendLine("	 AND A.ApplyDate >= @StartApplyDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartApplyDate", PurchaseApplyM.StartApplyDate));
            }
            if (PurchaseApplyM.EndApplyDate != "")
            {
                sql.AppendLine("	 AND A.ApplyDate <= @EndApplyDate ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndApplyDate", PurchaseApplyM.EndApplyDate));
            }
            if (PurchaseApplyM.BillStatus != "0")
            {
                sql.AppendLine("	 AND A.BillStatus = @BillStatus ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", PurchaseApplyM.BillStatus));
            }
      
 


            sql.AppendLine("ORDER BY " + OrderBy + "");
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region delete

        public static SqlCommand DeletePurchaseApply(string IDs)
        {
            SqlCommand comm = new SqlCommand();
            string sql = "delete from officedba.OfficeThingsPurchaseApply where ID in (" + IDs + ")";
            comm.CommandText = sql;
            return comm;
        }
        #endregion

        #region select 单个
        public static DataTable SelectPrimary1(string ApplyNo)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID ,A.ApplyNo ,A.Title ,A.TypeID,B.TypeName ,A.FromType,A.Confirmor                                                                 ");
            sql.AppendLine("	  ,F.EmployeeName AS ConfirmorName, CONVERT(varchar(100), A.ConfirmDate, 23) as ConfirmDate                                                ");
            sql.AppendLine("      ,A.Closer,G.EmployeeName AS CloserName, CONVERT(varchar(100), A.CloseDate, 23) as CloseDate                                            ");
            sql.AppendLine("      ,A.Creator,H.EmployeeName AS CreatorName,CONVERT(varchar(100), A.CreateDate, 23) as CreateDate                                         ");
            sql.AppendLine("      ,A.ModifiedUserID                                                                                     ");
            sql.AppendLine("      ,CONVERT(varchar(100), A.ModifiedDate, 23) as ModifiedDate                                                                             ");
            sql.AppendLine("      ,case A.FromType when '0' then '无来源' when '1' then '销售订单'when '2' then '物料需求计划' end as FromTypeName                       ");
            sql.AppendLine("      ,A.ApplyUserID , C.EmployeeName AS ApplyUserName  ,A.ApplyDeptID  ,D.DeptName AS ApplyDeptName                                         ");
            sql.AppendLine("      ,CONVERT(varchar(100), A.ApplyDate, 23) as ApplyDate,A.BillStatus                                                                      ");
            sql.AppendLine("      ,case A.BillStatus when '1' then '制单' when '2' then '执行' when '3' then '变更'when '4' then '手工结单'                              ");
            sql.AppendLine("       when '5' then '自动结单' end AS BillStatusName                                                                                        ");
            sql.AppendLine(" FROM officedba.OfficeThingsPurchaseApply AS A                                                                                                           ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS B ON A.CompanyCD = B.CompanyCD AND B.TypeFlag=7 AND B.TypeCode=5 AND A.TypeID=B.ID                     ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS C ON A.CompanyCD = C.CompanyCD AND A.ApplyUserID=C.ID                                                    ");
            sql.AppendLine("LEFT JOIN officedba.DeptInfo AS D ON A.CompanyCD = D.CompanyCD AND A.ApplyDeptID=D.ID                                                        ");
            sql.AppendLine("LEFT JOIN officedba.FlowInstance AS E ON  A.CompanyCD = E.CompanyCD  AND E.BillTypeFlag = 6 AND E.BillTypeCode = 1 AND E.BillID = A.ApplyNo  ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS F ON A.CompanyCD = F.CompanyCD AND A.Confirmor=F.ID                                                      ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS G ON A.CompanyCD = G.CompanyCD AND A.Closer=G.ID                                                         ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS H ON A.CompanyCD = H.CompanyCD AND A.Creator=H.ID                                                        ");
            sql.AppendLine("LEFT JOIN officedba.UserInfo AS I ON A.CompanyCD = I.CompanyCD AND A.ModifiedUserID=I.UserID                                                 ");


            sql.AppendLine("WHERE 1=1");
            sql.AppendLine("AND A.CompanyCD =@CompanyCD");
            sql.AppendLine("  AND A.ApplyNo =@ApplyNo ");

            SqlParameter[] param = new SqlParameter[2];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            param[1] = SqlHelper.GetParameter("@ApplyNo", ApplyNo);
            return SqlHelper.ExecuteSql(sql.ToString(), param);
        }
        #endregion

        #region insert
        public static SqlCommand InsertPrimary(OfficeThingsPurchaseApplyModel PurchaseApplyM)
        {
            SqlCommand comm = new SqlCommand();

            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("            INSERT INTO officedba.OfficeThingsPurchaseApply ( ");

            sql.AppendLine("CompanyCD ,");
            sql.AppendLine("ApplyNo ,");
            sql.AppendLine("Subject ,");
            sql.AppendLine("ApplyUserID ,");
            sql.AppendLine("ApplyDeptID ,");
            sql.AppendLine("ApplyDate ,");
            sql.AppendLine("Address ,");
            sql.AppendLine("CountTotal ,");
            sql.AppendLine("Remark ,");
            sql.AppendLine("BillStatus ,");
            sql.AppendLine("Creator ,");
            sql.AppendLine("CreateDate ,");
            sql.AppendLine("ModifiedDate ,");
            sql.AppendLine("ModifiedUserID ) VALUES ");
            sql.AppendLine("    (  ");
            sql.AppendLine("    @CompanyCD,");
            sql.AppendLine("    @ApplyNo,");
            sql.AppendLine("    @Subject,");
            sql.AppendLine("    @ApplyUserID,");
            sql.AppendLine("    @ApplyDeptID,");
            sql.AppendLine("    @ApplyDate,");
            sql.AppendLine("    @Address,");
            sql.AppendLine("    @CountTotal,");
            sql.AppendLine("    @Remark,");
            sql.AppendLine("    @BillStatus,");
            sql.AppendLine("    @Creator,");
            sql.AppendLine("    getdate(),");
            sql.AppendLine("    getdate(),");
            sql.AppendLine("   @ModifiedUserID)");  
            sql.AppendLine("set @IndexID = @@IDENTITY");
            #endregion

            #region 传参
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", PurchaseApplyM.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ApplyNo", PurchaseApplyM.ApplyNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Subject", PurchaseApplyM.Subject)); 

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ApplyUserID", PurchaseApplyM.ApplyUserID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ApplyDeptID", PurchaseApplyM.ApplyDeptID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Address", PurchaseApplyM.Address));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CountTotal", PurchaseApplyM.CountTotal));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ApplyDate", PurchaseApplyM.ApplyDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", PurchaseApplyM.Remark.ToString()));

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", PurchaseApplyM.BillStatus.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator", PurchaseApplyM.Creator.ToString()));

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", PurchaseApplyM.ModifiedUserID)); 

            SqlParameter IndexID = new SqlParameter("@IndexID", SqlDbType.Int);
            IndexID.Direction = ParameterDirection.Output;
            comm.Parameters.Add(IndexID);

            #endregion

            comm.CommandText = sql.ToString();

            return comm;
        }
        #endregion

        #region update

        public static SqlCommand UpdatePrimary(OfficeThingsPurchaseApplyModel PurchaseApplyM)
        {
            SqlCommand comm = new SqlCommand();

            #region SQL文
            StringBuilder sql = new StringBuilder();


            sql.AppendLine("            update officedba.OfficeThingsPurchaseApply set   ");
sql.AppendLine("CompanyCD = @CompanyCD, ");
sql.AppendLine("ApplyNo = @ApplyNo, ");
sql.AppendLine("Subject = @Subject,");
sql.AppendLine("ApplyUserID = @ApplyUserID,");
sql.AppendLine("ApplyDeptID = @ApplyDeptID, ");
sql.AppendLine("ApplyDate = @ApplyDate,");
sql.AppendLine("Address = @Address, ");
sql.AppendLine("CountTotal = @CountTotal, ");
sql.AppendLine("Remark = @Remark, ");
sql.AppendLine("BillStatus = @BillStatus, "); 
sql.AppendLine("ModifiedDate = getDate() , ");
sql.AppendLine("ModifiedUserID = @ModifiedUserID   ");  
            sql.AppendLine(" WHERE ID=@ID                          ");

            #endregion

            #region 传参
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ApplyNo", PurchaseApplyM.ApplyNo)); 

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", PurchaseApplyM.CompanyCD )); 
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Subject", PurchaseApplyM.Subject)); 
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ApplyUserID", PurchaseApplyM.ApplyUserID.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ApplyDeptID", PurchaseApplyM.ApplyDeptID.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Address", PurchaseApplyM.Address.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CountTotal", PurchaseApplyM.CountTotal.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ApplyDate", PurchaseApplyM.ApplyDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", PurchaseApplyM.Remark.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", PurchaseApplyM.BillStatus.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", PurchaseApplyM.ModifiedUserID.ToString())); 
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", PurchaseApplyM.id.ToString()));
            #endregion

            comm.CommandText = sql.ToString();

            return comm;
        }
        #endregion

        #region 根据ID填充主表
        public static SqlCommand GetPurchaseApply(string ID)
        {
            SqlCommand comm = new SqlCommand();
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("  SELECT isnull(A.ID             ,0 ) AS ID                                                     ");
            sql.AppendLine("  ,      isnull(A.CompanyCD      ,'') AS CompanyCD                                              ");
            sql.AppendLine("  ,      isnull(A.ApplyNo        ,'') AS ApplyNo                                                ");
            sql.AppendLine("  ,      isnull(A.Subject          ,'') AS Subject                                                  ");
            sql.AppendLine("  ,      isnull(A.ApplyUserID    ,0 ) AS ApplyUserID                                            ");
            sql.AppendLine("  ,      isnull(I.EmployeeName   ,'') AS ApplyUserName                                          ");
            sql.AppendLine("  ,      isnull(A.ApplyDeptID    ,0 ) AS ApplyDeptID                                            ");
            sql.AppendLine("  ,      isnull(C.DeptName       ,'') AS DeptName                                               ");
            sql.AppendLine("  ,      isnull(A.Address        ,'') AS Address                                                ");
            sql.AppendLine("  ,      Convert(numeric(22," + userInfo.SelPoint + "),a.CountTotal) as CountTotal                  ");
            sql.AppendLine("  ,      isnull(A.Confirmor      ,0 ) AS Confirmor                                              ");
            sql.AppendLine("  ,      isnull(D.EmployeeName   ,'') AS ConfirmorName                                          ");
            sql.AppendLine("  ,      isnull(CONVERT(varchar(100), A.ConfirmDate, 23),'') AS ConfirmDate                     ");
            sql.AppendLine("  ,      isnull(CONVERT(varchar(100), A.ApplyDate, 23),'') AS ApplyDate                         ");
            sql.AppendLine("  ,      isnull(A.Remark         ,'') AS Remark                                                 ");
            sql.AppendLine("  ,      isnull(A.BillStatus     ,'') AS BillStatus                                             ");
            sql.AppendLine("  , case A.BillStatus when '1' then '制单' when '2' then '执行' when '3' then '变更'       ");
            sql.AppendLine(" when '4' then '手动结单' when '5' then '自动结单' else '出错了' end AS BillStatusName   ");
            sql.AppendLine("  ,      isnull(A.Creator        ,0 ) AS Creator                                                ");
            sql.AppendLine("  ,      isnull(F.EmployeeName   ,'') AS CreatorName                                            ");
            sql.AppendLine("  ,      isnull(CONVERT(varchar(100), A.CreateDate, 23),'') AS CreateDate                       ");
            sql.AppendLine("  ,      isnull(A.ModifiedUserID ,'') AS ModifiedUserID                                         ");
            sql.AppendLine("  ,      isnull(CONVERT(varchar(100), A.ModifiedDate, 23),'') ModifiedDate                      ");
            sql.AppendLine("  FROM  officedba.OfficeThingsPurchaseApply AS A                                                            "); 
            sql.AppendLine("  LEFT JOIN officedba.EmployeeInfo AS I ON A.ApplyUserID = I.ID                                 ");
            sql.AppendLine("  LEFT JOIN officedba.DeptInfo AS C ON A.ApplyDeptID = C.ID                                     ");
            sql.AppendLine("  LEFT JOIN officedba.EmployeeInfo AS D ON A.Confirmor = D.ID                                   "); 
            sql.AppendLine("  LEFT JOIN officedba.EmployeeInfo AS F ON A.Creator = F.ID                                     ");
            
            sql.AppendLine(" WHERE A.ID=@ID");
            #endregion

            #region 传参
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));
            #endregion

            comm.CommandText = sql.ToString();

            return comm;
        }
        #endregion

        #region 确认
        public static bool ConfirmPurchaseApply(string ID)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE officedba.OfficeThingsPurchaseApply    ");
            sql.AppendLine("   SET Confirmor = @Confirmor     ");
            sql.AppendLine("      ,ConfirmDate = getdate() ");
            sql.AppendLine("      ,ModifiedUserID = @ModifiedUserID     ");
            sql.AppendLine("      ,ModifiedDate = getdate() ");
            sql.AppendLine("      ,BillStatus = @BillStatus   ");
            sql.AppendLine(" WHERE ID=@ID                     ");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Confirmor", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString())); 
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID));        
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", "2"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteTransWithCommand(comm);
        }
        #endregion

        #region 取消确认
        //主表更改
        public static SqlCommand CancelConfirm(string ID)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE officedba.OfficeThingsPurchaseApply    ");
            sql.AppendLine("   SET Confirmor = null     ");
            sql.AppendLine("      ,ConfirmDate = null ");
            sql.AppendLine("      ,ModifiedUserID = @ModifiedUserID     ");
            sql.AppendLine("      ,ModifiedDate = getdate() ");
            sql.AppendLine("      ,BillStatus = @BillStatus   ");
            sql.AppendLine(" WHERE ID=@ID                     ");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", "1"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));
            comm.CommandText = sql.ToString();

            return comm;
        }
        #endregion

        #region 采购申请结单
        //public static bool CompletePuechaseApply(string ID)
        //{
        //    SqlCommand comm = new SqlCommand();
        //    StringBuilder sql = new StringBuilder();
        //    sql.AppendLine("UPDATE officedba.OfficeThingsPurchaseApply    ");
        //    sql.AppendLine("   SET Closer = @Closer     ");
        //    sql.AppendLine("      ,CloseDate = getdate() ");
        //    sql.AppendLine("      ,ModifiedUserID = @ModifiedUserID     ");
        //    sql.AppendLine("      ,ModifiedDate = getdate() ");
        //    sql.AppendLine("      ,BillStatus = @BillStatus   ");
        //    sql.AppendLine(" WHERE ID=@ID                     ");

        //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Closer", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString()));
        //    //     comm.Parameters.Add(SqlHelper.GetParameterFromString("@CloseDate", DateTime.Now.ToString()));
        //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID));
        //    //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedDate", DateTime.Now.ToString()));
        //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", "4"));
        //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));
        //    comm.CommandText = sql.ToString();
        //    return SqlHelper.ExecuteTransWithCommand(comm);
        //}
        #endregion

        #region 采购申请取消结单
        //public static bool ConcelCompletePurchaseApply(string ID)
        //{
        //    SqlCommand comm = new SqlCommand();
        //    StringBuilder sql = new StringBuilder();
        //    sql.AppendLine("UPDATE officedba.OfficeThingsPurchaseApply    ");
        //    sql.AppendLine("   SET Closer = @Closer     ");
        //    sql.AppendLine("      ,CloseDate = @CloseDate ");
        //    sql.AppendLine("      ,ModifiedUserID = @ModifiedUserID     ");
        //    sql.AppendLine("      ,ModifiedDate = getdate() ");
        //    sql.AppendLine("      ,BillStatus = @BillStatus   ");
        //    sql.AppendLine(" WHERE ID=@ID                     ");

        //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Closer", DBNull.Value.ToString()));
        //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CloseDate", DBNull.Value.ToString()));
        //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID));
        //    //   comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedDate", DateTime.Now.ToString()));
        //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", "2"));
        //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));
        //    comm.CommandText = sql.ToString();
        //    return SqlHelper.ExecuteTransWithCommand(comm);
        //}
        #endregion

        #region 采购申请有没有被引用
        public static bool IsCitePurApply(string ID)
        {
            bool IsCite = false;
            //用品入库 用品采购表officedba.OfficeThingsBuy引用
            if (!IsCite)
            {


                string sql = "SELECT b.ID FROM officedba.OfficeThingsBuy as a  ";
                sql += "left outer join officedba.OfficeThingsBuyDetail as b on a.CompanyCD=b.CompanyCD and a.BuyRecordNo=b.BuyRecordNo   ";
                sql += "WHERE a.FromType=@FromType AND b.FromBillID=@ID   "; 
                SqlParameter[] parameters = {
                    new SqlParameter("@FromType", SqlDbType.VarChar),
					new SqlParameter("@ID", SqlDbType.VarChar),};
                parameters[0].Value = "1";
                parameters[1].Value = ID;
                IsCite = SqlHelper.Exists(sql, parameters);
            }
            ////采购询价引用
            //if (!IsCite)
            //{
            //    string sql = "SELECT ID FROM officedba.PurchaseAskPriceDetail WHERE FromType=@FromType AND FromBillID=@ID";
            //    SqlParameter[] parameters = {
            //        new SqlParameter("@FromType", SqlDbType.VarChar),
            //        new SqlParameter("@ID", SqlDbType.VarChar),};
            //    parameters[0].Value = "1";
            //    parameters[1].Value = ID;
            //    IsCite = SqlHelper.Exists(sql, parameters);
            //}
            ////采购合同引用
            //if (!IsCite)
            //{
            //    string sql = "SELECT ID FROM officedba.PurchaseContract WHERE FromType=@FromType AND FromBillID=@ID";
            //    SqlParameter[] parameters = {
            //        new SqlParameter("@FromType", SqlDbType.VarChar),
            //        new SqlParameter("@ID", SqlDbType.VarChar),};
            //    parameters[0].Value = "1";
            //    parameters[1].Value = ID;
            //    IsCite = SqlHelper.Exists(sql, parameters);
            //}
            ////采购订单引用
            //if (!IsCite)
            //{
            //    string sql = "SELECT ID FROM officedba.PurchaseOrderDetail WHERE FromType=@FromType AND FromBillID=@ID";
            //    SqlParameter[] parameters = {
            //        new SqlParameter("@FromType", SqlDbType.VarChar),
            //        new SqlParameter("@ID", SqlDbType.VarChar),};
            //    parameters[0].Value = "1";
            //    parameters[1].Value = ID;
            //    IsCite = SqlHelper.Exists(sql, parameters);
            //}
            return IsCite;
        }
        #endregion
        #endregion

        #region 明细来源表操作
        #region insert
        /// <summary>
        /// 明细来源表insert
        /// </summary>
        /// <param name="PurchaseApplyM">主表</param>
        /// <param name="str">明细来源</param>
        /// <param name="str2">明细信息</param>
        /// <param name="ApplyNo"></param>
        /// <param name="dtlslen">原明细来源的长度</param>
        /// <returns></returns>
        public static SqlCommand InsertDtlS(OfficeThingsPurchaseApplyDetailModel PurchaseApplyDetailSourceM, string ApplyNo)
        {
            SqlCommand comm = new SqlCommand();

            #region SQL文
            StringBuilder sql = new StringBuilder();


            sql.AppendLine(" INSERT INTO officedba.OfficeThingsPurchaseApplyDetail (   ");
 
 sql.AppendLine("CompanyCD ,  ");
 sql.AppendLine("ApplyNo ,  ");
 sql.AppendLine("SortNo , ");
 sql.AppendLine("ThingID , ");
 sql.AppendLine("RequireCount , ");
 sql.AppendLine("RequireDate ) ");
 sql.AppendLine("VALUES ");
    sql.AppendLine("  (  ");
 sql.AppendLine("@CompanyCD , ");
 sql.AppendLine("@ApplyNo , ");
 sql.AppendLine("@SortNo , ");
 sql.AppendLine("@ThingID , ");
 sql.AppendLine("@RequireCount , ");
 sql.AppendLine("@RequireDate ) ");
 
           
            #endregion

            #region 传参 
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", PurchaseApplyDetailSourceM.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ApplyNo", ApplyNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SortNo", PurchaseApplyDetailSourceM.SortNo)); 
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ThingID", PurchaseApplyDetailSourceM.ThingID)); 
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@RequireCount", PurchaseApplyDetailSourceM.RequireCount)); 
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@RequireDate", PurchaseApplyDetailSourceM.RequireDate));
            #endregion

            comm.CommandText = sql.ToString();

            return comm;
        }
        #endregion

        #region delete
        public static SqlCommand DeleteDtlS(string ApplyNo)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("DELETE FROM officedba.OfficeThingsPurchaseApplyDetail ");
            sql.AppendLine("      WHERE CompanyCD=@CompanyCD      ");
            sql.AppendLine("	AND ApplyNo in ('" + ApplyNo + "')            ");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.CommandText = sql.ToString();
            return comm;
        }
        #endregion

        #region select
        public static SqlCommand GetPurchaseApplySource(string ID)
        {
            SqlCommand comm = new SqlCommand();
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID         ");
            sql.AppendLine("      ,  isnull(A.CompanyCD      ,'') AS CompanyCD       ");
            sql.AppendLine("      , isnull(A.ApplyNo        ,'') AS ApplyNo        ");
            sql.AppendLine("      ,A.SortNo     ");
            sql.AppendLine("     ,A.ThingID                                                     ");
            sql.AppendLine("     ,isnull(GG.ThingNo, '') as ProductNo                                                    ");
            sql.AppendLine("     ,isnull(GG.ThingName,'') as     ProductName                                               ");
            sql.AppendLine("     ,GG.UnitID                                                        ");
            sql.AppendLine("     ,isnull(B.TypeName,'')  AS UnitName                                          ");
            sql.AppendLine("     ,Convert(numeric(14," + userInfo.SelPoint + "),a.RequireCount) as RequireCount                           "); 
            sql.AppendLine("     ,CONVERT(varchar(100), A.RequireDate, 23) as RequireDate         ");
            sql.AppendLine("     ,  Convert(numeric(14," + userInfo.SelPoint + "),a.InCount) as InCount                                           ");
            sql.AppendLine("     ,isnull(GG.ThingType,'') AS Specification  ");
            sql.AppendLine("     ,isnull( C.TypeName,'') AS TypeName  ");
            sql.AppendLine("FROM officedba.OfficeThingsPurchaseApplyDetail AS A                         ");
            sql.AppendLine("left outer join officedba.OfficeThingsInfo as GG  on a.CompanyCD=GG.CompanyCD and a.ThingID=GG.id                        ");
            sql.AppendLine("LEFT JOIN officedba.CodePublicType AS B ON GG.UnitID=B.ID                ");
            sql.AppendLine(" LEFT JOIN officedba.CodePublicType AS C ON GG.TypeID =C.ID    ");
            sql.AppendLine("INNER JOIN officedba.OfficeThingsPurchaseApply AS E ON A.CompanyCD=E.CompanyCD AND A.ApplyNo=E.ApplyNo AND E.ID=@ID");
            #endregion

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));

            comm.CommandText = sql.ToString();

            return comm;

        }
        #endregion
        #endregion

        #region 明细表操作
        //#region insert
        //public static SqlCommand InsertDtl(OfficeThingsPurchaseApplyDetailModel PurchaseApplyDetailM, string ApplyNo)
        //{
        //    SqlCommand comm = new SqlCommand();

        //    #region SQL文
        //    StringBuilder sql = new StringBuilder();
        //    sql.AppendLine("INSERT INTO officedba.OfficeThingsPurchaseApplyDetail");
        //    sql.AppendLine("           (CompanyCD                    ");
        //    sql.AppendLine("           ,ApplyNo                      ");
        //    sql.AppendLine("           ,SortNo                       ");
        //    sql.AppendLine("           ,ProductID                    ");
        //    sql.AppendLine("           ,ProductNo                    ");

        //    sql.AppendLine("           ,ProductName                  ");
        //    sql.AppendLine("           ,ProductCount                 ");
        //    sql.AppendLine("           ,UnitID                       ");
        //    sql.AppendLine("           ,RequireDate                  ");
        //    if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
        //    {
        //        sql.AppendLine("           ,UsedUnitID                        ");
        //        sql.AppendLine("           ,UsedUnitCount                         ");
        //        sql.AppendLine("           ,ExRate                        ");
        //    }
        //    //  sql.AppendLine("           ,ApplyReason                  ");

        //    //sql.AppendLine("           ,Remark                       ");
        //    sql.AppendLine("           ,PlanedCount                  ");
        //    //sql.AppendLine("           ,ModifiedDate                 ");
        //    //sql.AppendLine("           ,ModifiedUserID               ");
        //    sql.AppendLine("           )                             ");
        //    sql.AppendLine("     VALUES                              ");
        //    sql.AppendLine("           (@CompanyCD                   ");
        //    sql.AppendLine("           ,@ApplyNo                     ");
        //    sql.AppendLine("           ,@SortNo                      ");
        //    sql.AppendLine("           ,@ProductID                   ");
        //    sql.AppendLine("           ,@ProductNo                   ");

        //    sql.AppendLine("           ,@ProductName                 ");
        //    sql.AppendLine("           ,@ProductCount                ");
        //    sql.AppendLine("           ,@UnitID                      ");
        //    sql.AppendLine("           ,@RequireDate                 ");
        //    if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
        //    {
        //        sql.AppendLine("           ,@UsedUnitID                        ");
        //        sql.AppendLine("           ,@UsedUnitCount                         ");
        //        sql.AppendLine("           ,@ExRate                        ");
        //    }
        //    //    sql.AppendLine("           ,@ApplyReason                 ");

        //    //sql.AppendLine("           ,@Remark                      ");
        //    sql.AppendLine("           ,@PlanedCount                 ");
        //    //sql.AppendLine("           ,@ModifiedDate                ");
        //    //sql.AppendLine("           ,@ModifiedUserID              ");
        //    sql.AppendLine("           )                             ");

        //    #endregion

        //    #region 传参
        //    if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
        //    {
        //        comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedUnitID", PurchaseApplyDetailM.UsedUnitID));
        //        comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedUnitCount", PurchaseApplyDetailM.UsedUnitCount));
        //        comm.Parameters.Add(SqlHelper.GetParameterFromString("@ExRate", PurchaseApplyDetailM.ExRate));
        //    }

        //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", PurchaseApplyDetailM.CompanyCD));
        //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ApplyNo", ApplyNo));
        //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@SortNo", PurchaseApplyDetailM.SortNo.ToString()));
        //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", PurchaseApplyDetailM.ProductID.ToString()));
        //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductNo", PurchaseApplyDetailM.ProductNo.ToString()));

        //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", PurchaseApplyDetailM.ProductName.ToString()));
        //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductCount", PurchaseApplyDetailM.ProductCount.ToString()));
        //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@UnitID", PurchaseApplyDetailM.UnitID.ToString()));
        //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@RequireDate", PurchaseApplyDetailM.RequireDate.ToShortDateString()));
        //    //   comm.Parameters.Add(SqlHelper.GetParameterFromString("@ApplyReason", PurchaseApplyDetailM.ApplyReason.ToString()));

        //    //comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", PurchaseApplyDetailM.Remark.ToString()));
        //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanedCount", "0"));
        //    //comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedDate", PurchaseApplyDetailM.ModifiedDate.ToString()));
        //    //comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", PurchaseApplyDetailM.ModifiedUserID.ToString()));
        //    #endregion

        //    comm.CommandText = sql.ToString();

        //    return comm;
        //}
        //#endregion

        #region delete
        public static SqlCommand DeleteDtl(string ApplyNo)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("DELETE FROM officedba.OfficeThingsPurchaseApplyDetail ");
            sql.AppendLine("      WHERE CompanyCD=@CompanyCD      ");
            sql.AppendLine("	AND APplyNo in ('" + ApplyNo + "')            ");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.CommandText = sql.ToString();
            return comm;
        }
        #endregion

        #region select
        public static SqlCommand GetPurchaseApplyDetail(string ID)
        {
            SqlCommand comm = new SqlCommand();

            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID                                                          ");
            sql.AppendLine(",isnull(A.CompanyCD    ,'') AS CompanyCD                             ");
            sql.AppendLine(",isnull(A.ApplyNo      ,'') AS ApplyNo                               ");
            sql.AppendLine(",isnull(A.SortNo       ,0) AS SortNo                                 ");
            sql.AppendLine(",isnull(A.ProductID    ,0) AS ProductID                              ");
            sql.AppendLine(",isnull(A.ProductNo    ,'') AS ProductNo                             ");
            sql.AppendLine(",isnull(A.ProductName  ,'') AS ProductName                           ");
            sql.AppendLine(",Convert(numeric(14," + userInfo.SelPoint + "),a.ProductCount) as ProductCount                      ");
            sql.AppendLine(",isnull(A.UnitID       ,0) AS UnitID                                 ");
            sql.AppendLine(",isnull(B.CodeName     ,'') AS UnitName                              ");
            sql.AppendLine(",isnull(f.CodeName     ,'') AS UsedUnitName                              ");
            sql.AppendLine("     ,Convert(numeric(12," + userInfo.SelPoint + "),a.ExRate) as ExRate                                      ");
            sql.AppendLine("     ,Convert(numeric(14," + userInfo.SelPoint + "),a.UsedUnitCount) as UsedUnitCount                                         ");
            sql.AppendLine("     ,isnull(A.UsedUnitID,0) as    UsedUnitID                                               ");
            sql.AppendLine(",isnull(CONVERT(varchar(100), A.RequireDate, 23),'' )AS RequireDate  ");
            //sql.AppendLine(",isnull(A.ApplyReason  ,0) AS ApplyReason                            ");
            //sql.AppendLine(",isnull(C.CodeName     ,'') AS ApplyReasonName                       ");
            //sql.AppendLine(",isnull(A.Remark       ,'') AS Remark                                ");
            sql.AppendLine(", Convert(numeric(14," + userInfo.SelPoint + "),a.PlanedCount) as PlanedCount                           ");
            sql.AppendLine("     ,isnull(D.Specification,'') AS Specification,isnull(H.TypeName,'') as ColorName ");
            sql.AppendLine("FROM officedba.OfficeThingsPurchaseApplyDetail  AS A                             ");
            sql.AppendLine("LEFT JOIN officedba.CodeUnitType AS B ON A.UnitID=B.ID               ");
            //sql.AppendLine("LEFT JOIN officedba.CodeReasonType AS C ON A.ApplyReason=C.ID        ");
            sql.AppendLine("LEFT JOIN officedba.ProductInfo AS D ON A.ProductID=D.ID ");
            sql.AppendLine("LEFT JOIN officedba.CodeUnitType AS f ON A.UsedUnitID=f.ID               ");
            sql.AppendLine("left join officedba.CodePublicType H on D.ColorID=H.ID");
            sql.AppendLine("INNER JOIN officedba.OfficeThingsPurchaseApply AS E ON A.CompanyCD=E.CompanyCD AND A.ApplyNo=E.ApplyNo AND E.ID=@ID");
            #endregion

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID));

            comm.CommandText = sql.ToString();
            return comm;
        }
        #endregion
        #endregion


  
   

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
     

    

   

        #region 单位
        public static DataTable GetUnit()
        {
            string sql = "SELECT ID AS UnitID,CodeName AS UnitName FROM officedba.CodeUnitType WHERE CompanyCD = @CompanyCD  AND UsedStatus = '1'";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            return SqlHelper.ExecuteSql(sql, param);
        }
        #endregion

        #region 部门选择
        public static DataTable GetDeptSelect()
        {
            string sql = "SELECT ID,DeptID ,DeptPYCD AS DeptNo ,DeptName FROM officedba.DeptInfo WHERE CompanyCD =@CompanyCD ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            return SqlHelper.ExecuteSql(sql, param);
        }
        #endregion

        #region 采购类别
        public static DataTable GetPurchaseType()
        {
            string sql = "SELECT ID,TypeName FROM officedba.CodePublicType WHERE CompanyCD=@CompanyCD  AND TypeFlag=7 AND TypeCode=5";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            return SqlHelper.ExecuteSql(sql, param);
        }
        #endregion


        #region 根据产品名称查询物品信息 用于导入
        public static DataTable GetGoodsByProductName(string CompanyCD, string ProductNameKeys)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT p.ID,p.ProductName,p.ProdNo,p.Specification,p.UnitID,cu.CodeName,ISNULL(p.StandardBuy,0) AS StandardBuy,p.MakeUnitID,isnull(HF.TypeName,'') as ColorName   ");
            sbSql.AppendLine(" FROM officedba.ProductInfo AS p LEFT JOIN officedba.CodeUnitType AS cu ON p.UnitID=cu.ID ");
            sbSql.AppendLine(" left join officedba.CodePublicType HF on p.ColorID=HF.ID ");
            sbSql.AppendLine(" WHERE p.CheckStatus=1 and p.UsedStatus=1 and p.CompanyCD=@CompanyCD AND p.ProductName in (" + ProductNameKeys + ")   ORDER BY ID DESC");

            SqlParameter[] Paras = { SqlHelper.GetParameter("@CompanyCD", CompanyCD) };

            return SqlHelper.ExecuteSql(sbSql.ToString(), Paras);
        }
        #endregion
    }
}

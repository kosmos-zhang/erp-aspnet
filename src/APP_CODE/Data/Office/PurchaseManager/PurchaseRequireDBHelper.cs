/**********************************************
 * 类作用：   采购需求数据库层处理
 * 建立人：   王保军   
 * 建立时间： 2009/04/16
  * 修改人：   王保军                          *
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


namespace XBase.Data.Office.PurchaseManager
{
    public class PurchaseRequireDBHelper
    {
        #region 获取采购需求
        public static DataTable GetPurchaseRequireInfo(PurchaseRequireModel PurchaseRequireM, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            SqlCommand comm = new SqlCommand();
            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID                                                          ");
            sql.AppendLine(", isnull(A.MRPCD                              ,0) AS MRPCD           ");
            sql.AppendLine(", isnull(B.MRPNo                              ,'') AS MRPNo          ");
            sql.AppendLine(", isnull(A.ProdID                             ,0) AS ProdID          ");
            sql.AppendLine(", isnull(C.ProdNo                             ,'') AS ProdNo         ");
            sql.AppendLine(", isnull(C.ProductName                        ,'') AS ProductName    ");
            sql.AppendLine(", isnull(C.Specification                      ,'') AS Specification  ");
            sql.AppendLine(", isnull(C.TypeID                         ,0) AS ProdTypeID      ");
            sql.AppendLine(", isnull(D.CodeName                           ,'') AS ProductTypeName");
            sql.AppendLine(", isnull(C.UnitID                             ,0) AS UnitID          ");
            sql.AppendLine(", isnull(E.CodeName                           ,'') AS UnitName       ");
            sql.AppendLine(", isnull( A.NeedCount                         ,0) AS NeedCount       ");
            sql.AppendLine(", (SELECT sum(isnull(H.ProductCount,0)+isnull(H.RoadCount,0)-isnull(H.OutCount,0)-isnull(H.OrderCount,0)) FROM officedba.StorageProduct AS H WHERE A.CompanyCD=H.CompanyCD AND A.ProdID=H.ProductID) AS HasNum ");
            sql.AppendLine(", isnull(A.WantingNum                         ,0) AS WantingNum      ");
            sql.AppendLine(", isnull(A.WaitingDays                        ,0) AS WaitingDays     ");
            sql.AppendLine(", isnull(CONVERT(varchar(23),A.RequireDate,23),0) AS RequireDate     ");
            sql.AppendLine(", isnull(A.OrderCount,0) AS OrderCount");
            sql.AppendLine(", isnull(A.Creator                            ,0) AS Creator         ");
            sql.AppendLine(", isnull(F.EmployeeName                       ,'') AS CreatorName    ");
            sql.AppendLine(", isnull(CONVERT(varchar(23),A.CreateDate,23) ,'') AS CreateDate     ");
            sql.AppendLine(", isnull(A.Confirmor                          ,0) AS Confirmor       ");
            sql.AppendLine(", isnull(G.EmployeeName                       ,'') AS ConfirmorName  ");
            sql.AppendLine(", isnull(CONVERT(varchar(23),A.ConfirmDate,23),'') AS ConfirmDate,isnull(H.TypeName,'') as ColorName    ");
            sql.AppendLine("FROM officedba.PurchaseRequire AS A                                  ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS G ON A.Confirmor = G.ID          ");
            sql.AppendLine("INNER JOIN officedba.MRP AS B ON A.MRPCD = B.ID                       ");
            sql.AppendLine("LEFT JOIN officedba.ProductInfo AS C ON A.ProdID = C.ID              ");
            sql.AppendLine("LEFT JOIN officedba.CodeProductType AS D ON C.TypeID = D.ID      ");
            sql.AppendLine("LEFT JOIN officedba.CodeUnitType AS E ON C.UnitID = E.ID             ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS F ON A.Creator = F.ID            ");
            sql.AppendLine("left join officedba.CodePublicType H on C.ColorID=H.ID");

            sql.AppendLine("WHERE A.CompanyCD = @CompanyCD");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            if (PurchaseRequireM.ProdTypeID != "")
            {
                sql.AppendLine(" AND C.TypeID in (" + PurchaseRequireM.ProdTypeID + ")");
            }

            if (PurchaseRequireM.ProdID != "")
            {
                sql.AppendLine(" AND A.ProdID=@ProductID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", PurchaseRequireM.ProdID));
            }
            if (PurchaseRequireM.CreateCondition == "0")
            {//任何条件
            }
            else if (PurchaseRequireM.CreateCondition == "1")
            {//没有生成
                sql.AppendLine(" AND isnull(A.OrderCount,0)=0 ");
            }
            else if (PurchaseRequireM.CreateCondition == "2")
            {//部分生成
                sql.AppendLine(" AND isnull(A.OrderCount,0)<isnull(A.WantingNum,0) AND isnull(A.OrderCount,0) <> 0 ");
            }
            else if (PurchaseRequireM.CreateCondition == "3")
            {//生成完毕
                sql.AppendLine(" AND isnull(A.OrderCount,0)>=isnull(A.WantingNum,0)");
            }
            if (PurchaseRequireM.RequireDate != "")
            {
                sql.AppendLine(" AND A.RequireDate>=@StartDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", PurchaseRequireM.RequireDate));
            }
            if (PurchaseRequireM.EndRequireDate != "")
            {
                sql.AppendLine(" AND A.RequireDate<=@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", PurchaseRequireM.EndRequireDate));
            }
            #endregion

            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
            //return SqlHelper.ExecuteSearch(comm);

        }

        public static DataTable GetPurchaseRequireInfo(PurchaseRequireModel PurchaseRequireM, string OrderBy)
        {
            SqlCommand comm = new SqlCommand();
            #region SQL文
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID                                                          ");
            sql.AppendLine(", isnull(A.MRPCD                              ,0) AS MRPCD           ");
            sql.AppendLine(", isnull(B.MRPNo                              ,'') AS MRPNo          ");
            sql.AppendLine(", isnull(A.ProdID                             ,0) AS ProdID          ");
            sql.AppendLine(", isnull(C.ProdNo                             ,'') AS ProdNo         ");
            sql.AppendLine(", isnull(C.ProductName                        ,'') AS ProductName    ");
            sql.AppendLine(", isnull(C.Specification                      ,'') AS Specification  ");
            sql.AppendLine(", isnull(C.TypeID                         ,0) AS ProdTypeID      ");
            sql.AppendLine(", isnull(D.CodeName                           ,'') AS ProductTypeName");
            sql.AppendLine(", isnull(C.UnitID                             ,0) AS UnitID          ");
            sql.AppendLine(", isnull(E.CodeName                           ,'') AS UnitName       ");
            sql.AppendLine(", isnull( A.NeedCount                         ,0) AS NeedCount       ");
            sql.AppendLine(", (SELECT sum(isnull(H.ProductCount,0)+isnull(H.RoadCount,0)-isnull(H.OutCount,0)-isnull(H.OrderCount,0)) FROM officedba.StorageProduct AS H WHERE A.CompanyCD=H.CompanyCD AND A.ProdID=H.ProductID) AS HasNum ");
            sql.AppendLine(", isnull(A.WantingNum                         ,0) AS WantingNum      ");
            sql.AppendLine(", isnull(A.WaitingDays                        ,0) AS WaitingDays     ");
            sql.AppendLine(", isnull(CONVERT(varchar(23),A.RequireDate,23),0) AS RequireDate     ");
            sql.AppendLine(", isnull(A.OrderCount,0) AS OrderCount");
            sql.AppendLine(", isnull(A.Creator                            ,0) AS Creator         ");
            sql.AppendLine(", isnull(F.EmployeeName                       ,'') AS CreatorName    ");
            sql.AppendLine(", isnull(CONVERT(varchar(23),A.CreateDate,23) ,'') AS CreateDate     ");
            sql.AppendLine(", isnull(A.Confirmor                          ,0) AS Confirmor       ");
            sql.AppendLine(", isnull(G.EmployeeName                       ,'') AS ConfirmorName  ");
            sql.AppendLine(", isnull(CONVERT(varchar(23),A.ConfirmDate,23),'') AS ConfirmDate    ");
            sql.AppendLine("FROM officedba.PurchaseRequire AS A                                  ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS G ON A.Confirmor = G.ID          ");
            sql.AppendLine("INNER JOIN officedba.MRP AS B ON A.MRPCD = B.ID                       ");
            sql.AppendLine("LEFT JOIN officedba.ProductInfo AS C ON A.ProdID = C.ID              ");
            sql.AppendLine("LEFT JOIN officedba.CodeProductType AS D ON C.TypeID = D.ID      ");
            sql.AppendLine("LEFT JOIN officedba.CodeUnitType AS E ON C.UnitID = E.ID             ");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS F ON A.Creator = F.ID            ");
            sql.AppendLine("WHERE A.CompanyCD = @CompanyCD");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            if (PurchaseRequireM.ProdTypeID != "")
            {
                sql.AppendLine(" AND C.TypeID in (" + PurchaseRequireM.ProdTypeID + ")");
            }

            if (PurchaseRequireM.ProdID != "")
            {
                sql.AppendLine(" AND A.ProdID=@ProductID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", PurchaseRequireM.ProdID));
            }
            if (PurchaseRequireM.CreateCondition == "0")
            {//任何条件
            }
            else if (PurchaseRequireM.CreateCondition == "1")
            {//没有生成
                sql.AppendLine(" AND isnull(A.OrderCount,0)=0 ");
            }
            else if (PurchaseRequireM.CreateCondition == "2")
            {//部分生成
                sql.AppendLine(" AND isnull(A.OrderCount,0)<isnull(A.WantingNum,0) AND isnull(A.OrderCount,0) <> 0 ");
            }
            else if (PurchaseRequireM.CreateCondition == "3")
            {//生成完毕
                sql.AppendLine(" AND isnull(A.OrderCount,0)>=isnull(A.WantingNum,0)");
            }
            if (PurchaseRequireM.RequireDate != "")
            {
                sql.AppendLine(" AND A.RequireDate>=@StartDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", PurchaseRequireM.RequireDate));
            }
            if (PurchaseRequireM.EndRequireDate != "")
            {
                sql.AppendLine(" AND A.RequireDate<=@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", PurchaseRequireM.EndRequireDate));
            }
            sql.AppendLine(" ORDER BY "+OrderBy+"");
            #endregion

            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 删除采购需求
        public static bool DeletePurchaseRequireInfo(int[] IDS)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            foreach (int ID in IDS)
            {
                sb.Append(""+ID+",");
            }
            sb.Remove(sb.Length - 1, 1);
            //sb.ToString().TrimEnd(new char[] {','});
            sb.Append(")");

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("DELETE FROM officedba.PurchaseRequire");
            sql.AppendLine(" WHERE ID in "+sb.ToString()+"");

            SqlHelper.ExecuteSql(sql.ToString());
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }
        #endregion

        #region 获取指定物品类别ID的子类别ID和类别名称
        public static DataTable GetProductType(int ParentID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"]; 
            string sql;
            if (ParentID == 0)
            {
                sql = "select ID,isnull(CodeName,'') AS TypeName from officedba.CodeProductType WHERE CompanyCD='" + userInfo.CompanyCD + "' and  (SupperID IS NULL OR SupperID=0)";
            }
            else
            {
                sql = "select ID,isnull(CodeName,'') AS TypeName from officedba.CodeProductType WHERE CompanyCD='" + userInfo.CompanyCD + "' and SupperID=" + ParentID + "";

            }
            return SqlHelper.ExecuteSql(sql);
        }
        
        #endregion

        #region 获取物品ID、No、Name
        public static DataTable GetProduct()
        {
            string sql;
            sql = "SELECT ID AS ProductID,ProdNo AS ProductNo,ProductName FROM officedba.ProductInfo WHERE CompanyCD='" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "'";
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion
    }
}

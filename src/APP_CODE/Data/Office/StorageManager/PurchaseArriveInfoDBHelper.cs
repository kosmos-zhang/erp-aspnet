/**********************************************
 * 类作用：   期初库存数据库层处理
 * 建立人：   肖合明
 * 建立时间： 2009/03/29
 ***********************************************/
using System;
using XBase.Model.Office.StorageManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using XBase.Common;

namespace XBase.Data.Office.StorageManager
{
    public class PurchaseArriveInfoDBHelper
    {

        #region 查询：采购到货单
        /// <summary>
        /// 查询采购到货单
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetPurchaseArriveTableBycondition()
        {
            string sql = "select ID,CompanyCD,ArriveNo,Title,Purchaser,ProviderID,Creator,CONVERT(varchar(100),CreateDate, 23) as CreateDate from officedba.PurchaseArrive";
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion


        #region 采购到货及其明细信息列表(弹出层显示)
        public static DataTable GetPurchaseArriveInfo(string CompanyCD, string ArriveNo, string Title)
        {
            //string sql = "select a.ID,a.InNo,a.Title,a.StorageID,a.Executor,a.EnterDate,a.Remark,a.Creator,a.CreateDate,a.BillStatus,a.Confirmor,a.ConfirmDate,a.Closer,a.CloseDate,a.ModifiedDate,a.ModifiedUserID,b.SortNo,b.ProductID,b.UnitID,b.UnitPrice,b.TotalPrice,b.ProductCount,b.Remark as Remark1 from officedba.StorageInitail a left outer join officedba.StorageInitailDetail b on b.InNo=a.InNo  where a.CompanyCd='" + model.CompanyCD + "' and a.id=" + model.ID + "";
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select a.ID,a.ArriveNo,a.Title,a.ProviderID,isnull(z.TypeName,'') ColorName,");
            sql.AppendLine("ISNULL(j.CustName,'') as ProviderName, ");
            sql.AppendLine("ISNULL(i.EmployeeName,'') as Purchaser");
            sql.AppendLine(",a.DeptID,ISNULL(d.DeptName,'') DeptName");
            sql.AppendLine(",a.PayType,a.TypeID");
            sql.AppendLine(",a.SendAddress,a.ReceiveOverAddress,a.CurrencyType,a.Rate,a.Creator,CONVERT(varchar(100),a.CreateDate, 23) as CreateDate,b.ID as DetailID,b.ProductID,");
            sql.AppendLine("ISNULL(p.ProdNo,'') as ProductNo,ISNULL(p.ProductName,'') as ProductName");
            sql.AppendLine(",b.UnitID,isnull(c1.CodeName,'') as UnitName,isnull(b.ProductCount,0) JiBenCount,");
            sql.AppendLine("(ISNULL(b.ProductCount,0)-ISNULL(b.RejectCount,0)-ISNULL(b.BackCount,0)) as ProductCount");
            sql.AppendLine(",ISNULL(b.InCount,0) as InCount");
            sql.AppendLine(",b.UnitPrice,b.TotalPrice,b.Remark,b.FromBillID");
            sql.AppendLine(",'采购订单' as FromTypeName,b.FromLineNo from officedba.PurchaseArrive a");
            sql.AppendLine(" right outer join officedba.PurchaseArriveDetail b on b.ArriveNo=a.ArriveNo and a.CompanyCD=b.CompanyCD left join officedba.DeptInfo d on d.ID=a.DeptID");
            sql.AppendLine("left join officedba.ProductInfo p on p.ID=b.ProductID");
            sql.AppendLine("left join officedba.EmployeeInfo i on i.ID=a.Purchaser");
            sql.AppendLine("left join officedba.ProviderInfo j on j.ID=a.ProviderID ");
            sql.AppendLine(" left join officedba.CodeUnitType c1 on c1.ID = b.UnitID ");
            sql.AppendLine(" left join officedba.CodePublicType z on z.ID = p.ColorID ");
            
            sql.AppendLine("where a.CompanyCD='" + CompanyCD + "' and a.BillStatus=2");
            sql.AppendLine(" and (ISNULL(b.ProductCount,0)-ISNULL(b.RejectCount,0)-ISNULL(b.BackCount,0)-ISNULL(b.InCount,0))>0");
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            if (!string.IsNullOrEmpty(ArriveNo))
            {
                sql.AppendLine(" and a.ArriveNo like '%'+ @ArriveNo +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ArriveNo", ArriveNo));
            }
            if (!string.IsNullOrEmpty(Title))
            {
                sql.AppendLine(" and a.Title like '%'+ @Title +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", Title));
            }
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);

        }
        #endregion

        //点击弹出层确定，带出基本信息
        public static DataTable GetPAInfo(string ArriveNo, string CompanyCD)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select a.ID,a.ArriveNo,a.ProviderID,");
            sql.AppendLine("ISNULL(j.CustName,'') as ProviderName");
            sql.AppendLine(",a.Purchaser,c.EmployeeName as PurchaserName, a.PayType,a.TypeID,a.SendAddress,");
            sql.AppendLine("a.ReceiveOverAddress,a.CurrencyType,b.DeptName,a.Rate,'采购到货单' as FromTypeName from officedba.PurchaseArrive a");
            sql.AppendLine(" left join officedba.DeptInfo b on b.ID=a.DeptID");
            sql.AppendLine(" left join officedba.EmployeeInfo c on c.ID=a.Purchaser");
            sql.AppendLine("left join officedba.ProviderInfo j on j.ID=a.ProviderID");
            sql.AppendLine(" where a.CompanyCD='" + CompanyCD + "' and a.ArriveNo='" + ArriveNo + "'");
            return SqlHelper.ExecuteSql(sql.ToString());
        }

        public static DataTable GetPADetailInfo(string ArriveNo, string CompanyCD)
        {
            string sql = "select ProductID,ProductNo,ProductName,UnitID,ISNULL(ProductCount,0)-ISNULL(RejectCount,0)-ISNULL(BackCount,0) as ProductCount,InCount,UnitPrice,TotalPrice,Remark,FromBillID,FromType,FromLineNo from officedba.PurchaseArriveDetail where CompanyCD='" + CompanyCD + "' and ArriveNo=" + ArriveNo;
            return SqlHelper.ExecuteSql(sql);
        }
        /// <summary>
        /// 根据传过来的明细ID数组来获取明细列表
        /// </summary>
        /// <param name="strDetailIDList"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetInfoByArriveList(string strDetailIDList, string CompanyCD)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select a.ID,a.ArriveNo,a.SortNo as FromLineNo,a.ProductID,a.UsedUnitID,isnull(z.TypeName,'') ColorName ");
            sql.AppendLine(",ISNULL(p.ProdNo,'') as ProductNo");
            sql.AppendLine(",ISNULL(p.ProductName,'') as ProductName");
            sql.AppendLine(",q.CodeName as UnitID,p.UnitID DetailUnitID ");
            sql.AppendLine(",ISNULL(p.Specification,'') as Specification,p.StorageID,p.IsBatchNo");
            sql.AppendLine(",ISNULL(a.ProductCount,0)-ISNULL(a.RejectCount,0)-ISNULL(a.BackCount,0) as FromBillCount");
            sql.AppendLine(",ISNULL(a.InCount,0) as InCount");
            sql.AppendLine(",(ISNULL(a.ProductCount,0)-ISNULL(a.RejectCount,0)-ISNULL(a.BackCount,0)-ISNULL(a.InCount,0)) as ProductCount");
            sql.AppendLine(",a.InCount,a.TaxPrice as UnitPrice,a.Remark,a.FromType");
            sql.AppendLine(",ISNULL(a.TaxRate,0) as TaxRate ,ISNULL(a.Discount,0) as Discount");//税率和折扣
            sql.AppendLine(",'采购到货单' as FromTypeName");
            sql.AppendLine(",a.FromBillID from officedba.PurchaseArriveDetail a");
            sql.AppendLine(" left join officedba.ProductInfo p on p.ID=a.ProductID");
            sql.AppendLine(" left join officedba.CodeUnitType q on q.ID=p.UnitID");
            sql.AppendLine(" left join officedba.CodePublicType z on z.ID=p.ColorID");
            sql.AppendLine("where a.CompanyCD='" + CompanyCD + "' and a.ID in ( ");
            for (int i = 0; i < strDetailIDList.Split(',').Length - 1; i++)
            {
                sql.AppendLine("'" + strDetailIDList.Split(',')[i] + "', ");
            }
            string strSql = sql.ToString().Remove(sql.ToString().LastIndexOf(','));
            strSql += ")";
            return SqlHelper.ExecuteSql(strSql);
        }
    }
}

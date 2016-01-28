/**********************************************
 * 类作用：   其他入库（销售退货入库）数据库层处理
 * 建立人：   肖合明
 * 建立时间： 2009/04/10
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
    public class StorageSellBackDBHelper
    {
        #region 销售退货单及其明细信息列表(弹出层显示)
        /// <summary>
        /// 销售退货单及其明细信息列表(弹出层显示)
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetSBDetailInfo(string CompanyCD, string BackNo,string Title)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine(" select a.[ID],a.BackNo,isnull(a.Title,'') as BackCargoTheme ");
            sql.AppendLine(" ,b.ID as DetailID,b.ProductID,isnull(z.TypeName,'') ColorName");
            sql.AppendLine(" ,case when a.CreateDate IS NULL then '' else CONVERT(varchar(10),a.CreateDate, 23) end as CreateDate");
            sql.AppendLine(" ,ISNULL(i.ProdNo,'') as ProdNo,ISNULL(c1.CodeName,'') as UnitName,b.ProductCount JiBenCount ");
            sql.AppendLine(" ,ISNULL(i.ProductName,'') as ProductName,b.UnitPrice ");
            sql.AppendLine(" ,ISNULL(b.BackNumber,0) as BackNumber,ISNULL(b.InNumber,0) as InNumber,ISNULL(b.BackNumber,0)-ISNULL(b.InNumber,0) as NotInCount");
            sql.AppendLine(" from officedba.SellBackDetail b");
            sql.AppendLine(" left outer join officedba.SellBack a on b.BackNo=a.BackNo and a.CompanyCD=b.CompanyCD");
            sql.AppendLine(" left join officedba.ProductInfo i on b.ProductID=i.ID");
            sql.AppendLine(" left join officedba.CodeUnitType c1 on c1.ID = i.UnitID");
            sql.AppendLine(" left join officedba.CodePublicType z on z.ID = i.ColorID");
            sql.AppendLine(" where a.CompanyCD='" + CompanyCD + "' and a.BillStatus=2 and (ISNULL(b.BackNumber,0)-ISNULL(b.InNumber,0))>0");
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            if (!string.IsNullOrEmpty(BackNo))
            {
                sql.AppendLine(" and a.BackNo like '%'+ @BackNo +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BackNo", BackNo));
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

        #region 根据销售退货单明细中ID数组来获取信息（填充入库单中的明细）
        /// <summary>
        /// 根据销售退货单明细中ID数组来获取信息（填充入库单中的明细)
        /// </summary>
        /// <param name="strDetailIDList"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetInfoByDetalIDList(string strDetailIDList, string CompanyCD)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select b.ID,a.BackNo,a.ProductID,a.UsedUnitID,isnull(a.UsedUnitCount,0)UsedUnitCount");
            sql.AppendLine(",b.CustID,isnull(z.TypeName,'') ColorName");
            sql.AppendLine(",ISNULL(o.CustName,'') as CustName");
            sql.AppendLine(",ISNULL(p.ProdNo,'') as ProdNo");
            sql.AppendLine(",ISNULL(p.ProductName,'') as ProductName,p.StorageID");
            sql.AppendLine(",q.CodeName as UnitID,p.UnitID DetailUnitID,p.IsBatchNo");
            sql.AppendLine(",ISNULL(p.Specification,'') as Specification");
            sql.AppendLine("  ,ISNULL(a.BackNumber,0) as BackNumber,ISNULL(a.InNumber,0) as InNumber,ISNULL(a.BackNumber,0)-ISNULL(a.InNumber,0) as NotInCount");
            sql.AppendLine(",a.TaxPrice as UnitPrice,a.TaxPrice*(a.BackNumber-a.InNumber) as TotalPrice");
            sql.AppendLine(",ISNULL(a.Remark,'') as Remark");
            sql.AppendLine(",a.SortNo from officedba.SellBackDetail a");
            sql.AppendLine(" left join officedba.ProductInfo p on p.ID=a.ProductID");
            sql.AppendLine(" left join officedba.SellBack b on b.BackNo=a.BackNo and a.CompanyCD=b.CompanyCD");
            sql.AppendLine(" left join officedba.CustInfo o on b.CustID=o.ID ");
            sql.AppendLine(" left join officedba.CodeUnitType q on q.ID=p.UnitID");
            sql.AppendLine(" left join officedba.CodePublicType z on z.ID=p.ColorID");
            sql.AppendLine(" where a.CompanyCD='" + CompanyCD + "' and a.ID in ( ");
            for (int i = 0; i < strDetailIDList.Split(',').Length - 1; i++)
            {
                sql.AppendLine("'" + strDetailIDList.Split(',')[i] + "', ");
            }
            string strSql = sql.ToString().Remove(sql.ToString().LastIndexOf(','));
            strSql += ")";
            return SqlHelper.ExecuteSql(strSql);
        }
        #endregion
    }
}

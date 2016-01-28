/**********************************************
 * 类作用：   决策支持分析
 * 建立人：   莫申林
 * 建立时间： 2010/06/04
 ***********************************************/

using System;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using XBase.Common;

namespace XBase.Data.OperatingModel.DecisionData
{
   public class StorageAccountAnalysisDBHelper
    {
       /// <summary>
       /// 库存状况分析--分页查询
       /// </summary>
       /// <param name="QueryStr">查询条件</param>
       /// <param name="pageIndex">页数</param>
       /// <param name="pageSize">每页记录数</param>
       /// <param name="OrderBy">排序</param>
       /// <param name="totalCount">总记录数</param>
       /// <returns></returns>
       public static DataTable GetSotrageAccountAnalysis(string QueryStr, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
       {
           StringBuilder sqlStr = new StringBuilder();
           sqlStr.AppendLine(" select *,case when ProductCount-isnull(MaxStockNum,0)<0 then null else ProductCount-isnull(MaxStockNum,0)  end  as Chaochu, ");
           sqlStr.AppendLine(" case when isnull(MinStockNum,0)-ProductCount<0 then null else isnull(MinStockNum,0)-ProductCount  end  as Duanque ");
           sqlStr.AppendLine(" from ( ");
           sqlStr.AppendLine(" select a.*,b.ProductName,b.ProdNo as ProductNo, ");
           sqlStr.AppendLine(" isnull(b.Specification,'') as Specification, ");
           sqlStr.AppendLine(" b.MaxStockNum,b.MinStockNum,d.TypeName as ColorName,c.CodeName as UnitName   from  ");
           sqlStr.AppendLine(" ( ");
           sqlStr.AppendLine(" select CompanyCD,ProductID,sum(isnull(ProductCount,0)) as ProductCount   from officedba.StorageProduct group by ProductID,CompanyCD ");
           sqlStr.AppendLine(" ) a left outer join officedba.ProductInfo b on a.ProductID=b.ID ");
           sqlStr.AppendLine(" left join officedba.CodeUnitType c on b.UnitID=c.ID ");
           sqlStr.AppendLine(" left outer join officedba.CodePublicType d on b.ColorID=d.ID ");
           sqlStr.AppendLine(" where ( b.MaxStockNum is not null or b.MinStockNum is not null ) {0} ");
           sqlStr.AppendLine(" ) a ");
           string querySQL = string.Format(sqlStr.ToString(), QueryStr);
           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           comm.CommandText = querySQL;
           //执行查询
           return SqlHelper.PagerWithCommand(comm, pageIndex, pageSize, OrderBy, ref totalCount);
       }


       /// <summary>
       /// 库存状况分析--导出
       /// </summary>
       /// <param name="QueryStr"></param>
       /// <returns></returns>
       public static DataTable GetSotrageAccountAnalysis(string QueryStr)
       {
           StringBuilder sqlStr = new StringBuilder();
           sqlStr.AppendLine(" select ColorName,UnitName,ProductName,ProductNo,Specification,convert(decimal(20,6),ProductCount) as ProductCount ,convert(decimal(20,6),Chaochu) as Chaochu ,convert(decimal(20,6),Duanque) as Duanque ,convert(decimal(20,6),MaxStockNum) as MaxStockNum ,convert(decimal(20,6),MinStockNum) as MinStockNum   from ( ");
           sqlStr.AppendLine(" select *,case when ProductCount-isnull(MaxStockNum,0)<0 then 0 else ProductCount-isnull(MaxStockNum,0)  end  as Chaochu, ");
           sqlStr.AppendLine(" case when isnull(MinStockNum,0)-ProductCount<0 then 0 else isnull(MinStockNum,0)-ProductCount  end  as Duanque ");
           sqlStr.AppendLine(" from ( ");
           sqlStr.AppendLine(" select a.*,b.ProductName,b.ProdNo as ProductNo, ");
           sqlStr.AppendLine(" isnull(b.Specification,'') as Specification, ");
           sqlStr.AppendLine(" isnull(b.MaxStockNum,0) as  MaxStockNum,isnull(b.MinStockNum,0) as MinStockNum ,d.TypeName as ColorName,c.CodeName as UnitName   from  ");
           sqlStr.AppendLine(" ( ");
           sqlStr.AppendLine(" select CompanyCD,ProductID,sum(isnull(ProductCount,0)) as ProductCount   from officedba.StorageProduct group by ProductID,CompanyCD ");
           sqlStr.AppendLine(" ) a left outer join officedba.ProductInfo b on a.ProductID=b.ID ");
           sqlStr.AppendLine(" left join officedba.CodeUnitType c on b.UnitID=c.ID ");
           sqlStr.AppendLine(" left outer join officedba.CodePublicType d on b.ColorID=d.ID ");
           sqlStr.AppendLine(" where ( b.MaxStockNum is not null or b.MinStockNum is not null ) {0} ");
           sqlStr.AppendLine(" ) a ) a");
           string querySQL = string.Format(sqlStr.ToString(), QueryStr);

           //执行查询
           return SqlHelper.ExecuteSql(querySQL);
       }
    }
}

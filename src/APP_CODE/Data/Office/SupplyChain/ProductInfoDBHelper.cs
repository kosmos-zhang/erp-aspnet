using System;
using XBase.Model.Office.SupplyChain;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using XBase.Common;

using System.IO;
namespace XBase.Data.Office.SupplyChain
{
    public class ProductInfoDBHelper
    {
        #region 查询：物品档案
        /// <summary>
        /// 物品档案
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetProductInfoTableBycondition(ProductInfoModel model, string QueryID, string EFIndex, string EFDesc, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            string sql = "";
            sql += "select  e.ID,isnull(Convert(numeric(14," + userInfo.SelPoint + "),e.StandardBuy),0) as StandardBuy,e.ProdNo,e.ModifiedDate,e.ProductName,cbt.TypeName as ColorName,cbtm.TypeName as MaterialName,";
            sql += "nn1.CodeName as  SaleUnitName,nn2.CodeName as InUnitName,nn3.CodeName as StockUnitName,nn4.CodeName as MakeUnitName,";
            sql += " isnull(e.Source,'')as Source,isnull(e.PYShort,'')as PYShort,isnull(Convert(numeric(14," + userInfo.SelPoint + "),e.InTaxRate),0) as InTaxRate,";
            sql += " isnull(Convert(numeric(14," + userInfo.SelPoint + "),e.TaxBuy),0)as TaxBuy,";
            sql += " n.CodeName,isnull(f.CodeName,'') as CodeTypeName";
            sql += ",e.CompanyCD,isnull(e.Specification,'') as Specification,isnull(Convert(numeric(14," + userInfo.SelPoint + "),e.TaxRate),0)as TaxRate,isnull(e.GroupUnitNo,'')GroupUnitNo,e.SaleUnitID,";
            sql += " e.InUnitID,e.StockUnitID,e.MakeUnitID,e.IsBatchNo,     ";
            sql += " isnull(Convert(numeric(14," + userInfo.SelPoint + "),e.SellTax),0) as SellTax, e.MinusIs,isnull(Convert(numeric(14," + userInfo.SelPoint + "),e.StandardCost),0)as StandardCost,";
            sql += "isnull(Convert(numeric(12," + userInfo.SelPoint + "),e.Discount),0) as Discount,isnull(e.TypeID,'')as TypeID       ";
            sql += " ,e.UnitID,isnull(Convert(numeric(14," + userInfo.SelPoint + "),e.StandardSell),0) as StandardSell ,m.CurrentStore,";
            sql += " m.ProductCount from (select a.companycd,a.ProdNo,isnull(Convert(numeric(13," + userInfo.SelPoint + "), Sum((isnull(d.ProductCount,0)))),0)as ProductCount,isnull(Convert(numeric(14," + userInfo.SelPoint + "), ";
            sql += " Sum((isnull(d.ProductCount,0)+isnull(d.InCount,0)+isnull(d.RoadCount,0)-isnull(d.OutCount,0)-isnull(d.OrderCount,0)))),0)as CurrentStore ";
            sql += " from officedba.ProductInfo as a left join officedba.StorageProduct as d on a.ID=d.productID   and d.companycd=@CompanyCD            ";
            sql += " left join officedba.CodeUnitType as b on a.UnitID=b.ID ";
            if (!string.IsNullOrEmpty(QueryID))
            {
                if (QueryID != "DETAIL")
                {
                    if (QueryID != "0")
                    {
                        sql += " where d.StorageID=@StorageID1";
                        comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID1", QueryID));
                    }
                }
            }
            sql += " group by a.ProdNo,a.companycd) as m left join  officedba.ProductInfo as e";
            sql += " on  e.ProdNo=m.ProdNo left join officedba.CodeUnitType as n on e.UnitID=n.ID";
            sql += " left join officedba.UnitGroupDetail  as n1 on ";
            sql += " e.SaleUnitID=n1.UnitID and e.GroupUnitNo=n1.GroupUnitNo and e.CompanyCD=n1.CompanyCD";
            sql += " left join officedba.CodeUnitType nn1 on nn1.ID=n1.UnitID and nn1.CompanyCD=n1.CompanyCD";
            sql += " left join officedba.UnitGroupDetail   as n2";
            sql += " on e.InUnitID=n2.UnitID and e.GroupUnitNo=n2.GroupUnitNo and e.CompanyCD=n2.CompanyCD";
            sql += " left join officedba.CodeUnitType nn2 on nn2.ID=n2.UnitID and nn2.CompanyCD=n2.CompanyCD";
            sql += " left join officedba.UnitGroupDetail   as n3";
            sql += " on e.StockUnitID=n3.UnitID and e.GroupUnitNo=n3.GroupUnitNo and e.CompanyCD=n3.CompanyCD";
            sql += " left join officedba.CodeUnitType nn3 on nn3.ID=n3.UnitID and nn3.CompanyCD=n3.CompanyCD";
            sql += " left join officedba.UnitGroupDetail   as n4";
            sql += " on e.MakeUnitID=n4.UnitID and e.GroupUnitNo=n4.GroupUnitNo and e.CompanyCD=n4.CompanyCD";
            sql += " left join officedba.CodeUnitType nn4 on nn4.ID=n4.UnitID and nn4.CompanyCD=n4.CompanyCD";
            sql += " left join officedba.CodePublicType cbt on e.ColorID=cbt.ID";
            sql += " left join officedba.CodePublicType cbtm on e.Material=cbtm.ID";
            sql += " left join officedba.CodeProductType ";
            sql += " as f on e.typeid=f.id   ";
            if (!string.IsNullOrEmpty(QueryID))
            {
                if (QueryID != "DETAIL")
                {
                    if (QueryID != "0")
                    {
                        sql += " right join officedba.StorageProduct as sp on sp.ProductID=e.ID ";
                    }
                }
            }
            sql += " where e.CompanyCD=@CompanyCD and m.companycd=@CompanyCD  ";
            sql += " and e.CheckStatus='1'";
            if (!string.IsNullOrEmpty(QueryID))
            {
                if (QueryID != "DETAIL")
                {
                    if (QueryID != "0")
                    {
                        sql += " and sp.StorageID=@StorageID and sp.deptid is null and sp.companycd=@CompanyCD ";
                        comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", QueryID));
                    }
                }
            }
            //#endregion
            ////公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //编号
            if (!string.IsNullOrEmpty(model.Specification))
            {
                sql += "	and e.Specification LIKE @Specification ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Specification", "%" + model.Specification + "%"));
            }
            if (!string.IsNullOrEmpty(model.Manufacturer))
            {
                sql += "	and e.Manufacturer LIKE @Manufacturer ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Manufacturer", "%" + model.Manufacturer + "%"));
            }
            if (!string.IsNullOrEmpty(model.FromAddr))
            {
                sql += "	and e.FromAddr LIKE @FromAddr ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromAddr", "%" + model.FromAddr + "%"));
            }
            if (model.Material != "0")
            {
                sql += "	and e.Material =@Material ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Material", model.Material));
            }
            if (!string.IsNullOrEmpty(model.StartStorage))
            {
                sql += "	and m.ProductCount>@StartStorage ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartStorage", model.StartStorage));
            }
            if (!string.IsNullOrEmpty(model.EndStorage))
            {
                sql += "	and m.ProductCount<@EndStorage ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndStorage", model.EndStorage));
            }
            if (!string.IsNullOrEmpty(model.ProdNo))
            {
                sql += "	and e.ProdNo LIKE @ProdNo ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProdNo", "%" + model.ProdNo + "%"));
            }
            //名称
            if (!string.IsNullOrEmpty(model.ProductName))
            {
                sql += "	AND e.ProductName LIKE @ProductName ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", "%" + model.ProductName.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(model.PYShort))
            {
                sql += "	AND e.PYShort LIKE @PYShort ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PYShort", "%" + model.PYShort + "%"));
            }
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                sql += "	AND e.ExtField" + EFIndex + " LIKE '%" + EFDesc + "%' ";
            }
            if (!string.IsNullOrEmpty(model.ColorID))
            {
                sql += "	and e.ColorID =@ColorID ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ColorID", model.ColorID));
            }
            if (!string.IsNullOrEmpty(model.TypeID))
            {
                string ID = model.TypeID;
                string allID = "";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string[] IdS = null;
                ID = ID.Substring(0, ID.Length);
                IdS = ID.Split(',');

                for (int i = 0; i < IdS.Length; i++)
                {
                    IdS[i] = "'" + IdS[i] + "'";
                    sb.Append(IdS[i]);
                }
                allID = sb.ToString().Replace("''", "','");
                sql += "	AND e.TypeID in  (" + allID + ") ";
            }

            //指定命令的SQL文
            comm.CommandText = sql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        /// <summary>
        /// 采购用
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetProductInfoTableBycondition(ProductInfoModel model, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            string sql = "";
            sql += "select  e.ProdNo AS ,e.ModifiedDate,e.ProductName,isnull(e.Source,'')as Source,isnull(e.PYShort,'')as PYShort,isnull(Convert(numeric(10,2),e.InTaxRate),0) as InTaxRate,isnull(Convert(numeric(10,2),e.TaxBuy),0)as TaxBuy,";
            sql += " e.ID,n.CodeName,isnull(f.CodeName,'') as CodeTypeName";
            sql += ",e.CompanyCD,isnull(e.Specification,'') as Specification,isnull(Convert(numeric(10,2),e.TaxRate),0)as TaxRate,";
            sql += " isnull(Convert(numeric(10,2),e.SellTax),0) as SellTax, e.MinusIs,isnull(Convert(numeric(10,2),e.StandardCost),0)as StandardCost, isnull(Convert(numeric(6,2),e.Discount),0) as Discount,isnull(e.TypeID,'')as TypeID       ";
            sql += " ,e.UnitID,isnull(Convert(numeric(10,2),e.StandardSell),0) as StandardSell ,m.CurrentStore from (select a.ProdNo,isnull(Convert(numeric(10,2), ";
            sql += " Sum((isnull(d.ProductCount,0)+isnull(d.InCount,0)+isnull(d.RoadCount,0)-isnull(d.OutCount,0)-isnull(d.OrderCount,0)))),0)as CurrentStore";
            sql += " from officedba.ProductInfo as a left join officedba.StorageProduct as d on a.ID=d.productID                        ";
            sql += " left join officedba.CodeUnitType as b on a.UnitID=b.ID ";
            sql += " group by a.ProdNo) as m left join  officedba.ProductInfo as e";
            sql += " on  e.ProdNo=m.ProdNo left join officedba.CodeUnitType as n on e.UnitID=n.ID left join officedba.CodeProductType ";
            sql += " as f on e.typeid=f.id   ";
            sql += " where e.CompanyCD=@CompanyCD  ";
            sql += " and e.CheckStatus='1'";

            //#endregion

            ////公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //编号
            if (!string.IsNullOrEmpty(model.ProdNo))
            {
                sql += "	and e.ProdNo LIKE @ProdNo ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProdNo", "%" + model.ProdNo + "%"));
            }
            //名称
            if (!string.IsNullOrEmpty(model.ProductName))
            {
                sql += "	AND e.ProductName LIKE @ProductName ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", "%" + model.ProductName + "%"));
            }
            if (!string.IsNullOrEmpty(model.PYShort))
            {
                sql += "	AND e.PYShort LIKE @PYShort ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PYShort", "%" + model.PYShort + "%"));
            }
            if (!string.IsNullOrEmpty(model.TypeID))
            {
                string ID = model.TypeID;
                string allID = "";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string[] IdS = null;
                ID = ID.Substring(0, ID.Length);
                IdS = ID.Split(',');

                for (int i = 0; i < IdS.Length; i++)
                {
                    IdS[i] = "'" + IdS[i] + "'";
                    sb.Append(IdS[i]);
                }
                allID = sb.ToString().Replace("''", "','");
                sql += "	AND e.TypeID in  (" + allID + ") ";
            }

            //指定命令的SQL文
            comm.CommandText = sql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        /// <summary>
        /// 分店查询
        /// </summary>
        /// <param name="model"></param>
        /// <param name="QueryID"></param>
        /// <returns></returns>
        public static DataTable GetProductInfoBycondition(ProductInfoModel model)
        {
            string sql = "";
            sql += "select distinct e.ProdNo,e.ProductName,isnull(e.PYShort,'')as PYShort,isnull(Convert(numeric(10,2),e.InTaxRate),0) as InTaxRate,isnull(Convert(numeric(10,2),e.TaxBuy),0)as TaxBuy,";
            sql += " e.ID,n.CodeName,isnull(f.CodeName,'') as CodeTypeName";
            sql += ",e.CompanyCD,isnull(e.Specification,'') as Specification,isnull(Convert(numeric(10,2),e.TaxRate),0)as TaxRate,";
            sql += " isnull(Convert(numeric(10,2),e.SellTax),0) as SellTax, e.MinusIs,isnull(Convert(numeric(10,2),e.StandardCost),0)as StandardCost, isnull(Convert(numeric(6,2),e.Discount),0) as Discount,isnull(e.TypeID,'')as TypeID       ";
            sql += " ,e.UnitID,isnull(Convert(numeric(10,2),e.StandardSell),0) as StandardSell ,m.ProdNo,m.CurrentStore from (select a.ProdNo,isnull(Convert(numeric(10,2),Sum((isnull(d.ProductCount,0)+isnull(d.InCount,0)+isnull(d.RoadCount,0)-isnull(d.OutCount,0)-isnull(d.OrderCount,0)))),0)as CurrentStore";
            sql += " from officedba.ProductInfo as a left join officedba.StorageProduct as d on a.ID=d.productID                        ";
            sql += " left join officedba.CodeUnitType as b on a.UnitID=b.ID group by a.ProdNo) as m left join officedba.ProductInfo as e";
            sql += " on  e.ProdNo=m.ProdNo left join officedba.CodeUnitType as n on e.UnitID=n.ID left join officedba.CodeProductType ";
            sql += " as f on e.typeid=f.id right join  officedba.SubSellOrderDetail t  on e.ID=t.productID    ";
            sql += " where e.CompanyCD=@CompanyCD and  e.CheckStatus='1'";

            //#endregion
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            ////公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //编号
            if (!string.IsNullOrEmpty(model.ProdNo))
            {
                sql += "	and e.ProdNo LIKE @ProdNo ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProdNo", "%" + model.ProdNo + "%"));
            }
            //名称
            if (!string.IsNullOrEmpty(model.ProductName))
            {
                sql += "	AND e.ProductName LIKE @ProductName ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", "%" + model.ProductName + "%"));
            }
            if (!string.IsNullOrEmpty(model.PYShort))
            {
                sql += "	AND e.PYShort LIKE @PYShort ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PYShort", "%" + model.PYShort + "%"));
            }
            if (!string.IsNullOrEmpty(model.TypeID))
            {
                string ID = model.TypeID;
                string allID = "";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string[] IdS = null;
                ID = ID.Substring(0, ID.Length);
                IdS = ID.Split(',');

                for (int i = 0; i < IdS.Length; i++)
                {
                    IdS[i] = "'" + IdS[i] + "'";
                    sb.Append(IdS[i]);
                }
                allID = sb.ToString().Replace("''", "','");
                sql += "	AND e.TypeID in  (" + allID + ") ";
            }
            //指定命令的SQL文
            comm.CommandText = sql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }


        public static DataTable GetProductInfoTableByCheckcondition(string strID, string CompanyCD)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            string sql = "";
            sql += "select e.ProdNo,e.ProductName,isnull(e.PYShort,'')as PYShort,isnull(Convert(numeric(14," + userInfo.SelPoint + "),e.InTaxRate),0) as InTaxRate,isnull(e.IsBatchNo,'0')IsBatchNo,isnull(cbt.TypeName,'') as ColorName,isnull(cbm.TypeName,'') as MaterialName,";
            sql += " isnull(e.InUnitID,0)InUnitID,isnull(e.StockUnitID,0)StockUnitID,isnull(e.MakeUnitID,0)MakeUnitID,isnull(e.SaleUnitID,0)SaleUnitID,isnull(e.GroupUnitNo,'')GroupUnitNo,  ";
            sql += "isnull(nn1.CodeName,'')SaleUnitName,isnull(nn2.CodeName,'')InUnitName,isnull(nn3.CodeName,'')StockUnitName,isnull(nn4.CodeName,'')MakeUnitName,";
            sql += " isnull(Convert(numeric(14," + userInfo.SelPoint + "),e.TaxBuy),0)as TaxBuy,";
            sql += " e.ID,isnull(n.CodeName,'')CodeName,isnull(f.CodeName,'') as CodeTypeName";
            sql += ",e.CompanyCD,isnull(e.Specification,'') as Specification,isnull(Convert(numeric(14," + userInfo.SelPoint + "),e.TaxRate),0)as TaxRate,";
            sql += " isnull(Convert(numeric(14," + userInfo.SelPoint + "),e.SellTax),0) as SellTax,";
            sql += " isnull(e.MinusIs,'') as MinusIs,isnull(Convert(numeric(14," + userInfo.SelPoint + "),e.StandardCost),0)as StandardCost,";
            sql += " isnull(Convert(numeric(12," + userInfo.SelPoint + "),e.Discount),0) as Discount,isnull(e.TypeID,'')as TypeID ,";
            sql += " isnull(Convert(numeric(14," + userInfo.SelPoint + "),e.StandardBuy),0) as StandardBuy      ";
            sql += " ,e.UnitID,isnull(Convert(numeric(14," + userInfo.SelPoint + "),e.StandardSell),0) as StandardSell ,";
            sql += " m.ProdNo,m.CurrentStore from (select a.ProdNo,isnull(Convert(numeric(14," + userInfo.SelPoint + "),";
            sql += " Sum((isnull(d.ProductCount,0)+isnull(d.InCount,0)+isnull(d.RoadCount,0)-isnull(d.OutCount,0)-isnull(d.OrderCount,0)))),0)as CurrentStore";
            sql += " from officedba.ProductInfo as a left join officedba.StorageProduct as d on a.ID=d.productID                        ";
            sql += " left join officedba.CodeUnitType as b on a.UnitID=b.ID group by a.ProdNo) as m left join officedba.ProductInfo as e";
            sql += " on  e.ProdNo=m.ProdNo left join officedba.CodeUnitType as n on e.UnitID=n.ID left join officedba.CodeProductType ";
            sql += " as f on e.typeid=f.id     ";
            sql += " left join officedba.UnitGroupDetail  as n1 on ";
            sql += " e.SaleUnitID=n1.UnitID and e.GroupUnitNo=n1.GroupUnitNo and e.CompanyCD=n1.CompanyCD";
            sql += " left join officedba.CodeUnitType nn1 on nn1.ID=n1.UnitID and nn1.CompanyCD=n1.CompanyCD";
            sql += " left join officedba.UnitGroupDetail   as n2";
            sql += " on e.InUnitID=n2.UnitID and e.GroupUnitNo=n2.GroupUnitNo and e.CompanyCD=n2.CompanyCD";
            sql += " left join officedba.CodeUnitType nn2 on nn2.ID=n2.UnitID and nn2.CompanyCD=n2.CompanyCD";
            sql += " left join officedba.UnitGroupDetail   as n3";
            sql += " on e.StockUnitID=n3.UnitID and e.GroupUnitNo=n3.GroupUnitNo and e.CompanyCD=n3.CompanyCD";
            sql += " left join officedba.CodeUnitType nn3 on nn3.ID=n3.UnitID and nn3.CompanyCD=n3.CompanyCD";
            sql += " left join officedba.UnitGroupDetail   as n4";
            sql += " on e.MakeUnitID=n4.UnitID and e.GroupUnitNo=n4.GroupUnitNo and e.CompanyCD=n4.CompanyCD";
            sql += " left join officedba.CodeUnitType nn4 on nn4.ID=n4.UnitID and nn4.CompanyCD=n4.CompanyCD";
            sql += " left join officedba.CodePublicType cbt on e.ColorID=cbt.ID";
            sql += " left join officedba.CodePublicType cbm on e.Material=cbm.ID";
            sql += " where e.CompanyCD=@CompanyCD";
            string allid = "";
            StringBuilder sb = new System.Text.StringBuilder();
            string[] IdS = null;
            //userid = userid.Substring(0, userid.Length);
            IdS = strID.Split(',');
            for (int i = 0; i < IdS.Length; i++)
            {
                IdS[i] = "'" + IdS[i] + "'";
                sb.Append(IdS[i]);
            }
            allid = sb.ToString().Replace("''", "','");
            if (!string.IsNullOrEmpty(strID))
            {
                sql += " AND e.ID in  (" + allid + ")";
            }

            //#endregion
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            ////公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            //编号

            //指定命令的SQL文
            comm.CommandText = sql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion
        /// <summary>
        /// 查询物品信息
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        /// 
        public static DataTable GetProductInfo(ProductInfoModel Model, string EFIndex, string EFDesc, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT a.ID                             ");
            sql.AppendLine("      ,a.CompanyCD                      ");
            sql.AppendLine("      ,a.ProdNo                         ");
            sql.AppendLine("      ,a.PYShort                        ");
            sql.AppendLine("      ,a.ProductName                    ");
            sql.AppendLine("      ,a.ShortNam                       ");
            sql.AppendLine("      ,a.BarCode                        ");
            sql.AppendLine("      ,a.TypeID                         ");
            sql.AppendLine("      ,(case a.BigType when '1' then '成品' when '2' then '原材料' when '3' then '固定资产' ");
            sql.AppendLine("      when '4' then '低值易耗' when '5' then '包装物' when '6' then '服务产品' end) BigType ");
            sql.AppendLine("      ,a.GradeID                        ");
            sql.AppendLine("      ,(case a.Source when '0' then '自制' when '1' then '外购' when '2' then '委外' ");
            sql.AppendLine("      when '3' then '虚拟件' end ) Source ");
            sql.AppendLine("      ,a.UnitID                         ");
            sql.AppendLine(",isnull(ExtField1,'')as  ExtField1    ");
            sql.AppendLine(",isnull(ExtField2,'')as  ExtField2    ");
            sql.AppendLine(",isnull(ExtField3,'')as  ExtField3    ");
            sql.AppendLine(",isnull(ExtField4,'')as  ExtField4    ");
            sql.AppendLine(",isnull(ExtField5,'')as  ExtField5    ");
            sql.AppendLine(",isnull(ExtField6,'')as  ExtField6    ");
            sql.AppendLine(",isnull(ExtField7,'')as  ExtField7    ");
            sql.AppendLine(",isnull(ExtField8,'')as  ExtField8    ");
            sql.AppendLine(",isnull(ExtField9,'')as  ExtField9    ");
            sql.AppendLine(",isnull(ExtField10,'')as ExtField10   ");
            sql.AppendLine(",isnull(ExtField11,'')as ExtField11   ");
            sql.AppendLine(",isnull(ExtField12,'')as ExtField12   ");
            sql.AppendLine(",isnull(ExtField13,'')as ExtField13   ");
            sql.AppendLine(",isnull(ExtField14,'')as ExtField14   ");
            sql.AppendLine(",isnull(ExtField15,'')as ExtField15   ");
            sql.AppendLine(",isnull(ExtField16,'')as ExtField16   ");
            sql.AppendLine(",isnull(ExtField17,'')as ExtField17   ");
            sql.AppendLine(",isnull(ExtField18,'')as ExtField18   ");
            sql.AppendLine(",isnull(ExtField19,'')as ExtField19   ");
            sql.AppendLine(",isnull(ExtField20,'')as ExtField20   ");
            sql.AppendLine(",isnull(ExtField21,'')as ExtField21   ");
            sql.AppendLine(",isnull(ExtField22,'')as ExtField22   ");
            sql.AppendLine(",isnull(ExtField23,'')as ExtField23   ");
            sql.AppendLine(",isnull(ExtField24,'')as ExtField24   ");
            sql.AppendLine(",isnull(ExtField25,'')as ExtField25   ");
            sql.AppendLine(",isnull(ExtField26,'')as ExtField26   ");
            sql.AppendLine(",isnull(ExtField27,'')as ExtField27   ");
            sql.AppendLine(",isnull(ExtField28,'')as ExtField28   ");
            sql.AppendLine(",isnull(ExtField29,'')as ExtField29   ");
            sql.AppendLine(",isnull(ExtField30,'')as  ExtField30   ");

            sql.AppendLine("      ,a.Brand                          ");
            sql.AppendLine("      ,a.Specification                  ");
            sql.AppendLine("      ,a.ColorID                        ");
            sql.AppendLine("      ,a.Size                           ");
            sql.AppendLine("      ,(case a.StockIs when '1' then '是' when '0' then '否' end) StockIs ");
            sql.AppendLine("      ,(case a.ABCType when 'A' then 'A类' when 'B' then 'B类' when 'C' then 'C类' end) ABCType");
            sql.AppendLine("      ,a.Remark                         ");
            sql.AppendLine("      ,a.Creator                        ");
            sql.AppendLine(" 	,CONVERT(VARCHAR(10),a.CreateDate  ,21) AS CreateDate ");
            sql.AppendLine("      ,(case a.CheckStatus when '0' then '草稿' else '已审' end)CheckStatus ");
            sql.AppendLine("      ,a.CheckUser,a.CheckStatus as intCheckStatus                      ");
            sql.AppendLine(" 	,CONVERT(VARCHAR(10),a.CheckDate ,21) AS CheckDate ");
            sql.AppendLine("      ,(case a.UsedStatus when '0' then '停用' else '启用' end) UsedStatus");
            sql.AppendLine(" 	,CONVERT(VARCHAR(10),a.ModifiedDate ,21) AS ModifiedDate ");
            sql.AppendLine("      ,a.ModifiedUserID                 ");
            sql.AppendLine("      ,a.FromAddr                       ");
            sql.AppendLine("      ,a.DrawingNum                     ");
            sql.AppendLine("      ,a.ImgUrl                         ");
            sql.AppendLine("      ,a.FileNo                         ");
            sql.AppendLine("      ,a.PricePolicy                    ");
            sql.AppendLine("      ,a.Params                         ");
            sql.AppendLine("      ,a.Questions                      ");
            sql.AppendLine("      ,a.ReplaceName                    ");
            sql.AppendLine("      ,a.Description                    ");
            sql.AppendLine("      ,(case a.MinusIs when '0' then '否' when '1' then '是' end) MinusIs ");
            sql.AppendLine("      ,a.StorageID                      ");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.SafeStockNum) as SafeStockNum");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.MinStockNum) as MinStockNum");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.MaxStockNum) as MaxStockNum");
            sql.AppendLine("      ,'加权平均法' CalcPriceWays                  ");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.StandardCost) as StandardCost");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.PlanCost) as PlanCost");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.StandardSell) as StandardSell");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.SellMin) as SellMin");
            sql.AppendLine("      ,a.Material                       ");
            sql.AppendLine("      ,a.Manufacturer                   ");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.SellMax) as SellMax");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.TaxRate) as TaxRate");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.InTaxRate) as InTaxRate");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.SellTax) as SellTax");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.SellPrice) as SellPrice");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.TransferPrice) as TransferPrice");
            sql.AppendLine("      ,Convert(numeric(12," + userInfo.SelPoint + "),a.Discount) as Discount");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.StandardBuy) as StandardBuy");
            sql.AppendLine("      ,a.GroupUnitNo                    ");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.TaxBuy) as TaxBuy");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.BuyMax) as BuyMax");
            sql.AppendLine("      ,a.SaleUnitID, n.CodeName SaleUnitName ");
            sql.AppendLine("      ,a.InUnitID,m.CodeName InUnitName ");
            sql.AppendLine("      ,a.StockUnitID, o.CodeName StockUnitName ");
            sql.AppendLine("      ,a.MakeUnitID,p.CodeName MakeUnitName ");
            sql.AppendLine("      ,(case a.IsBatchNo when '1' then '是' when '0' then '否' end) IsBatchNo ");
            sql.AppendLine("      ,b.CodeName TypeName ");
            sql.AppendLine("      ,b.TypeFlag                       ");
            sql.AppendLine("      ,f.GroupUnitName                  ");
            sql.AppendLine("      ,c.EmployeeName     ");
            sql.AppendLine("      ,d.EmployeeName as CheckUserName,g.TypeName GradeName,h.TypeName BrandName  ");
            sql.AppendLine("      ,i.TypeName ColorName,j.CodeName UnitName,k.TypeName MaterialName,l.StorageName StorageName ");
            sql.AppendLine("  FROM officedba.ProductInfo as a       ");
            sql.AppendLine("left join officedba.EmployeeInfo as c on a.Creator=c.ID     ");
            sql.AppendLine("left join officedba.EmployeeInfo as d on a.CheckUser=d.ID   ");
            sql.AppendLine("left join officedba.UnitGroup as f on a.GroupUnitNo=f.GroupUnitNo and a.CompanyCD=f.CompanyCD   ");
            sql.AppendLine("left join officedba.CodePublicType as g on g.ID = a.GradeID ");
            sql.AppendLine("left join officedba.CodePublicType as h on h.ID = a.Brand ");
            sql.AppendLine("left join officedba.CodePublicType as i on i.ID = a.ColorID ");
            sql.AppendLine("left join officedba.CodeUnitType as j on j.ID = a.UnitID ");
            sql.AppendLine("left join officedba.CodePublicType as k on k.ID = a.Material ");
            sql.AppendLine("left join officedba.StorageInfo as l on l.ID = a.StorageID ");
            sql.AppendLine("left join officedba.CodeUnitType as m on m.ID = a.InUnitID ");
            sql.AppendLine("left join officedba.CodeUnitType as n on n.ID = a.SaleUnitID ");
            sql.AppendLine("left join officedba.CodeUnitType as o on o.ID = a.StockUnitID ");
            sql.AppendLine("left join officedba.CodeUnitType as p on p.ID = a.MakeUnitID ");

            sql.AppendLine(" left join officedba.CodeProductType as b on a.TypeID=b.ID where a.CompanyCD=@CompanyCD ");

            #region
      //      StringBuilder searchSql = new StringBuilder();
      //      searchSql.AppendLine("SELECT a.ID                                                 ");
      //      searchSql.AppendLine("      ,a.ProdNo                                             ");
      //      searchSql.AppendLine("      ,a.ProductName                                        ");
      //      searchSql.AppendLine("      ,a.CheckStatus                                        ");
      //      searchSql.AppendLine("      ,isnull(a.TypeID,'') as TypeID                        ");
      //      searchSql.AppendLine("      ,isnull(a.UnitID,'')as   UnitID                      ");
      //      searchSql.AppendLine(",isnull(ExtField1,'')as  ExtField1    ");
      //      searchSql.AppendLine(",isnull(ExtField2,'')as  ExtField2    ");
      //      searchSql.AppendLine(",isnull(ExtField3,'')as  ExtField3    ");
      //      searchSql.AppendLine(",isnull(ExtField4,'')as  ExtField4    ");
      //      searchSql.AppendLine(",isnull(ExtField5,'')as  ExtField5    ");
      //      searchSql.AppendLine(",isnull(ExtField6,'')as  ExtField6    ");
      //      searchSql.AppendLine(",isnull(ExtField7,'')as  ExtField7    ");
      //      searchSql.AppendLine(",isnull(ExtField8,'')as  ExtField8    ");
      //      searchSql.AppendLine(",isnull(ExtField9,'')as  ExtField9    ");
      //      searchSql.AppendLine(",isnull(ExtField10,'')as ExtField10   ");
      //      searchSql.AppendLine(",isnull(ExtField11,'')as ExtField11   ");
      //      searchSql.AppendLine(",isnull(ExtField12,'')as ExtField12   ");
      //      searchSql.AppendLine(",isnull(ExtField13,'')as ExtField13   ");
      //      searchSql.AppendLine(",isnull(ExtField14,'')as ExtField14   ");
      //      searchSql.AppendLine(",isnull(ExtField15,'')as ExtField15   ");
      //      searchSql.AppendLine(",isnull(ExtField16,'')as ExtField16   ");
      //      searchSql.AppendLine(",isnull(ExtField17,'')as ExtField17   ");
      //      searchSql.AppendLine(",isnull(ExtField18,'')as ExtField18   ");
      //      searchSql.AppendLine(",isnull(ExtField19,'')as ExtField19   ");
      //      searchSql.AppendLine(",isnull(ExtField20,'')as ExtField20   ");
      //      searchSql.AppendLine(",isnull(ExtField21,'')as ExtField21   ");
      //      searchSql.AppendLine(",isnull(ExtField22,'')as ExtField22   ");
      //      searchSql.AppendLine(",isnull(ExtField23,'')as ExtField23   ");
      //      searchSql.AppendLine(",isnull(ExtField24,'')as ExtField24   ");
      //      searchSql.AppendLine(",isnull(ExtField25,'')as ExtField25   ");
      //      searchSql.AppendLine(",isnull(ExtField26,'')as ExtField26   ");
      //      searchSql.AppendLine(",isnull(ExtField27,'')as ExtField27   ");
      //      searchSql.AppendLine(",isnull(ExtField28,'')as ExtField28   ");
      //      searchSql.AppendLine(",isnull(ExtField29,'')as ExtField29   ");
      //      searchSql.AppendLine(",isnull(ExtField30,'')as  ExtField30   ");
      //      /*
      //        ,[CompanyCD],[ProdNo],[ProductName],
      //       * [PYShort],[ShortNam] ,[BarCode],
      //       * [TypeID],[UnitID],[CheckStatus],[Specification],
      //       * ,[ExtField1],[ExtField30],[ColorID],[Creator],[CreateDate],[UsedStatus]
      //       * 
      //       * 
      //       * [BigType],[GradeID],[Source]
      //,[Brand],[Size],[StockIs]
      //,[ABCType],[Remark]
      //,[CheckUser],[CheckDate],[ModifiedDate]
      //,[ModifiedUserID],[FromAddr],[DrawingNum],[ImgUrl]
      //,[FileNo],[PricePolicy],[Params],[Questions],[ReplaceName]
      //,[Description],[MinusIs],[StorageID],[SafeStockNum]
      //,[MinStockNum],[MaxStockNum],[CalcPriceWays],[StandardCost]
      //,[PlanCost],[StandardSell],[SellMin],[SellMax]
      //,[TaxRate],[InTaxRate],[SellTax],[SellPrice],[TransferPrice]
      //,[Discount],[StandardBuy],[TaxBuy],[BuyMax]
      //,[Manufacturer],[Material],[GroupUnitNo]
      //,[SaleUnitID],[InUnitID],[StockUnitID]
      //,[MakeUnitID],[IsBatchNo]
      //       */
      //      //searchSql.AppendLine(",a.PYShort,a.ShortNam,a.BarCode,");


      //      searchSql.AppendLine("      ,isnull(a.Specification,'')as Specification            ");
      //      searchSql.AppendLine("     ,isnull(e.TypeName,'')as ColorName                 ");
      //      searchSql.AppendLine("      ,isnull(a.Creator,'')  as Creator                       ");
      //      searchSql.AppendLine("      ,isnull(b.EmployeeName,'') as   EmployeeName           ");
      //      searchSql.AppendLine("      ,isnull(Convert(VARCHAR(10),a.CreateDate,21),'')as CreateDate                 ");
      //      searchSql.AppendLine("      ,isnull(a.UsedStatus,'')as UsedStatus                 ");
      //      searchSql.AppendLine("      ,isnull(c.CodeName,'') as UnitName                 ");
      //      searchSql.AppendLine("     ,isnull(d.CodeName,'')as TypeName                 ");
      //      searchSql.AppendLine("  FROM [officedba].[ProductInfo] as a                ");
      //      searchSql.AppendLine("left join officedba.EmployeeInfo as b on a.Creator=b.ID  and a.CompanyCD=b.CompanyCD    ");
      //      searchSql.AppendLine("left join officedba.CodeUnitType  as c on a.UnitID=c.ID and a.CompanyCD=c.CompanyCD    ");
      //      searchSql.AppendLine("left join officedba.CodeProductType as d on a.TypeID=d.ID and a.CompanyCD=d.CompanyCD    ");
      //      searchSql.AppendLine("left join officedba.CodePublicType as e on a.ColorID=e.ID and a.CompanyCD=e.CompanyCD    ");
      //      searchSql.AppendLine("        where   a.CompanyCD=@CompanyCD                  ");

            //#endregion
            #endregion
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            ////公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", Model.CompanyCD));
            //编号
            if (!string.IsNullOrEmpty(Model.ProdNo))
            {
                sql.AppendLine("	and a.ProdNo LIKE @ProdNo ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProdNo", "%" + Model.ProdNo + "%"));
            }
            //名称
            if (!string.IsNullOrEmpty(Model.ProductName))
            {
                sql.AppendLine("	AND a.ProductName LIKE @ProductName ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", "%" + Model.ProductName + "%"));
            }
            if (!string.IsNullOrEmpty(Model.BarCode))
            {
                sql.AppendLine("	AND a.BarCode LIKE @BarCode ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BarCode", "%" + Model.BarCode + "%"));
            }
            if (!string.IsNullOrEmpty(Model.ColorID))
            {
                sql.AppendLine("	AND a.ColorID = @ColorID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ColorID", Model.ColorID));
            }
            if (!string.IsNullOrEmpty(Model.Specification))
            {
                sql.AppendLine("	AND a.Specification LIKE @Specification ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Specification", "%" + Model.Specification + "%"));
            }
            if (!string.IsNullOrEmpty(Model.PYShort))
            {
                sql.AppendLine("	AND a.PYShort LIKE @PYShort ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PYShort", "%" + Model.PYShort + "%"));
            }
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                sql.AppendLine("	AND a.ExtField" + EFIndex + " LIKE '%" + EFDesc + "%' ");
            }
            if (!string.IsNullOrEmpty(Model.TypeID))
            {
                string ID = Model.TypeID;
                string allID = "";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string[] IdS = null;
                ID = ID.Substring(0, ID.Length);
                IdS = ID.Split(',');

                for (int i = 0; i < IdS.Length; i++)
                {
                    IdS[i] = "'" + IdS[i] + "'";
                    sb.Append(IdS[i]);
                }
                allID = sb.ToString().Replace("''", "','");
                sql.AppendLine("	AND a.TypeID in  (" + allID + ") ");
            }
            if (!string.IsNullOrEmpty(Model.UsedStatus))
            {
                sql.AppendLine("	AND a.UsedStatus = @UsedStatus ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", Model.UsedStatus));
            }
            if (!string.IsNullOrEmpty(Model.CheckStatus))
            {
                sql.AppendLine("	AND a.CheckStatus =@CheckStatus ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CheckStatus", Model.CheckStatus));
            }
            //指定命令的SQL文
            comm.CommandText = sql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }

        #region 查询：物品档案（批次)
        /// <summary>
        /// 物品档案
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetProductInfoBatchTableBycondition(ProductInfoModel model, string QueryID, string EFIndex, string EFDesc, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            string sql = "";
            sql += "select  e.ID,isnull(Convert(numeric(14," + userInfo.SelPoint + "),e.StandardBuy),0) as StandardBuy,e.ProdNo,e.ModifiedDate,e.ProductName,cbt.TypeName as ColorName,";
            sql += "nn1.CodeName as  SaleUnitName,nn2.CodeName as InUnitName,nn3.CodeName as StockUnitName,nn4.CodeName as MakeUnitName,";
            sql += " isnull(e.Source,'')as Source,isnull(e.PYShort,'')as PYShort,isnull(Convert(numeric(14," + userInfo.SelPoint + "),e.InTaxRate),0) as InTaxRate,";
            sql += " isnull(Convert(numeric(14," + userInfo.SelPoint + "),e.TaxBuy),0)as TaxBuy,";
            sql += " n.CodeName,isnull(f.CodeName,'') as CodeTypeName";
            sql += ",e.CompanyCD,isnull(e.Specification,'') as Specification,isnull(Convert(numeric(14," + userInfo.SelPoint + "),e.TaxRate),0)as TaxRate,isnull(e.GroupUnitNo,'')GroupUnitNo,e.SaleUnitID,";
            sql += " e.InUnitID,e.StockUnitID,e.MakeUnitID,e.IsBatchNo,     ";
            sql += " isnull(Convert(numeric(14," + userInfo.SelPoint + "),e.SellTax),0) as SellTax, e.MinusIs,isnull(Convert(numeric(14," + userInfo.SelPoint + "),e.StandardCost),0)as StandardCost,";
            sql += "isnull(Convert(numeric(12," + userInfo.SelPoint + "),e.Discount),0) as Discount,isnull(e.TypeID,'')as TypeID       ";
            sql += " ,e.UnitID,isnull(Convert(numeric(14," + userInfo.SelPoint + "),e.StandardSell),0) as StandardSell ,m.CurrentStore,m.BatchNo,";
            sql += " m.ProductCount from (select a.companycd,a.ProdNo,d.BatchNo,isnull(Convert(numeric(13," + userInfo.SelPoint + "), Sum((isnull(d.ProductCount,0)))),0)as ProductCount,isnull(Convert(numeric(14," + userInfo.SelPoint + "), ";
            sql += " Sum((isnull(d.ProductCount,0)+isnull(d.InCount,0)+isnull(d.RoadCount,0)-isnull(d.OutCount,0)-isnull(d.OrderCount,0)))),0)as CurrentStore ";
            sql += " from officedba.ProductInfo as a left join officedba.StorageProduct as d on a.ID=d.productID   and d.companycd=@CompanyCD            ";
            sql += " left join officedba.CodeUnitType as b on a.UnitID=b.ID ";
            if (!string.IsNullOrEmpty(QueryID))
            {
                if (QueryID != "DETAIL")
                {
                    if (QueryID != "0")
                    {
                        sql += " where d.StorageID=@StorageID1";
                        sql += " group by a.ProdNo,a.companycd,d.BatchNo) as m left join  officedba.ProductInfo as e";
                        comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID1", QueryID));
                    }
                    else
                        sql += " group by a.ProdNo,a.companycd,d.BatchNo) as m left join  officedba.ProductInfo as e";
                }
                else
                    sql += " group by a.ProdNo,a.companycd,d.BatchNo) as m left join  officedba.ProductInfo as e";
            }
            else
                sql += " where isnull(d.ProductCount,0)>0 group by a.ProdNo,a.companycd,d.BatchNo) as m left join  officedba.ProductInfo as e";
            sql += " on  e.ProdNo=m.ProdNo left join officedba.CodeUnitType as n on e.UnitID=n.ID";
            sql += " left join officedba.UnitGroupDetail  as n1 on ";
            sql += " e.SaleUnitID=n1.UnitID and e.GroupUnitNo=n1.GroupUnitNo and e.CompanyCD=n1.CompanyCD";
            sql += " left join officedba.CodeUnitType nn1 on nn1.ID=n1.UnitID and nn1.CompanyCD=n1.CompanyCD";
            sql += " left join officedba.UnitGroupDetail   as n2";
            sql += " on e.InUnitID=n2.UnitID and e.GroupUnitNo=n2.GroupUnitNo and e.CompanyCD=n2.CompanyCD";
            sql += " left join officedba.CodeUnitType nn2 on nn2.ID=n2.UnitID and nn2.CompanyCD=n2.CompanyCD";
            sql += " left join officedba.UnitGroupDetail   as n3";
            sql += " on e.StockUnitID=n3.UnitID and e.GroupUnitNo=n3.GroupUnitNo and e.CompanyCD=n3.CompanyCD";
            sql += " left join officedba.CodeUnitType nn3 on nn3.ID=n3.UnitID and nn3.CompanyCD=n3.CompanyCD";
            sql += " left join officedba.UnitGroupDetail   as n4";
            sql += " on e.MakeUnitID=n4.UnitID and e.GroupUnitNo=n4.GroupUnitNo and e.CompanyCD=n4.CompanyCD";
            sql += " left join officedba.CodeUnitType nn4 on nn4.ID=n4.UnitID and nn4.CompanyCD=n4.CompanyCD";
            sql += " left join officedba.CodePublicType cbt on e.ColorID=cbt.ID";
            sql += " left join officedba.CodeProductType ";
            sql += " as f on e.typeid=f.id   ";
            if (!string.IsNullOrEmpty(QueryID))
            {
                if (QueryID != "DETAIL")
                {
                    if (QueryID != "0")
                    {
                        sql += " right join officedba.StorageProduct as sp on sp.ProductID=e.ID ";
                    }
                }
            }
            sql += " where e.CompanyCD=@CompanyCD and m.companycd=@CompanyCD  ";
            sql += " and e.CheckStatus='1'";
            if (!string.IsNullOrEmpty(QueryID))
            {
                if (QueryID != "DETAIL")
                {
                    if (QueryID != "0")
                    {
                        sql += " and sp.StorageID=@StorageID and sp.deptid is null and sp.companycd=@CompanyCD ";
                        comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", QueryID));
                    }
                }
            }
            //#endregion
            ////公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //编号
            if (!string.IsNullOrEmpty(model.Specification))
            {
                sql += "	and e.Specification LIKE @Specification ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Specification", "%" + model.Specification + "%"));
            }
            if (!string.IsNullOrEmpty(model.Manufacturer))
            {
                sql += "	and e.Manufacturer LIKE @Manufacturer ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Manufacturer", "%" + model.Manufacturer + "%"));
            }
            if (!string.IsNullOrEmpty(model.FromAddr))
            {
                sql += "	and e.FromAddr LIKE @FromAddr ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromAddr", "%" + model.FromAddr + "%"));
            }
            if (model.Material != "0")
            {
                sql += "	and e.Material =@Material ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Material", model.Material));
            }
            if (!string.IsNullOrEmpty(model.StartStorage))
            {
                sql += "	and m.ProductCount>@StartStorage ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartStorage", model.StartStorage));
            }
            if (!string.IsNullOrEmpty(model.EndStorage))
            {
                sql += "	and m.ProductCount<@EndStorage ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndStorage", model.EndStorage));
            }
            if (!string.IsNullOrEmpty(model.ProdNo))
            {
                sql += "	and e.ProdNo LIKE @ProdNo ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProdNo", "%" + model.ProdNo + "%"));
            }
            //名称
            if (!string.IsNullOrEmpty(model.ProductName))
            {
                sql += "	AND e.ProductName LIKE @ProductName ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", "%" + model.ProductName.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(model.PYShort))
            {
                sql += "	AND e.PYShort LIKE @PYShort ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PYShort", "%" + model.PYShort + "%"));
            }
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                sql += "	AND e.ExtField" + EFIndex + " LIKE '%" + EFDesc + "%' ";
            }
            if (!string.IsNullOrEmpty(model.ColorID))
            {
                sql += "	and e.ColorID =@ColorID ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ColorID", model.ColorID));
            }
            if (!string.IsNullOrEmpty(model.TypeID))
            {
                string ID = model.TypeID;
                string allID = "";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string[] IdS = null;
                ID = ID.Substring(0, ID.Length);
                IdS = ID.Split(',');

                for (int i = 0; i < IdS.Length; i++)
                {
                    IdS[i] = "'" + IdS[i] + "'";
                    sb.Append(IdS[i]);
                }
                allID = sb.ToString().Replace("''", "','");
                sql += "	AND e.TypeID in  (" + allID + ") ";
            }

            //指定命令的SQL文
            comm.CommandText = sql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion


        /// <summary>
        /// 列表物品查询
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable SearchProduct(ProductInfoModel model, string conditions, int pageIndex, int pageCount, string OrderBy, ref int totalCount, string EFIndex, string EFDesc)
        {
            //例如：conditions=select distinct isnull(a.ProductID,b.ProductID) as ProductID from officedba.SubSellBackDetail a full join officedba.SubSellOrderDetail b on a.ProductID=b.ProductID
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select  g.* from (");
            searchSql.AppendLine("select distinct e.ProdNo,e.ProductName,isnull(e.PYShort,'')as PYShort,isnull(Convert(numeric(10,2),e.InTaxRate),0) as InTaxRate,isnull(Convert(numeric(10,2),e.TaxBuy),0)as TaxBuy,");
            searchSql.AppendLine(" e.ID,n.CodeName,isnull(f.CodeName,'') as CodeTypeName");
            searchSql.AppendLine(",e.CompanyCD,isnull(e.Specification,'') as Specification,isnull(Convert(numeric(10,2),e.TaxRate),0)as TaxRate,");
            searchSql.AppendLine(" isnull(Convert(numeric(10,2),e.SellTax),0) as SellTax, e.MinusIs,isnull(Convert(numeric(10,2),e.StandardCost),0)as StandardCost, isnull(Convert(numeric(6,2),e.Discount),0) as Discount,isnull(e.TypeID,'')as TypeID       ");
            searchSql.AppendLine(" ,e.UnitID,isnull(Convert(numeric(10,2),e.StandardSell),0) as StandardSell ,m.CurrentStore from (select a.ProdNo,isnull(Convert(numeric(10,2),Sum((isnull(d.ProductCount,0)+isnull(d.InCount,0)+isnull(d.RoadCount,0)-isnull(d.OutCount,0)-isnull(d.OrderCount,0)))),0)as CurrentStore");
            searchSql.AppendLine(" from officedba.ProductInfo as a left join officedba.StorageProduct as d on a.ID=d.productID  and a.CompanyCD=d.CompanyCD                      ");
            searchSql.AppendLine(" left join officedba.CodeUnitType as b on a.UnitID=b.ID and a.CompanyCD=b.CompanyCD group by a.ProdNo) as m left join officedba.ProductInfo as e");
            searchSql.AppendLine(" on  e.ProdNo=m.ProdNo left join officedba.CodeUnitType as n on e.UnitID=n.ID and e.CompanyCD=n.CompanyCD left join officedba.CodeProductType ");
            searchSql.AppendLine(" as f on e.typeid=f.id and e.CompanyCD=f.CompanyCD  ");
            searchSql.AppendLine(" where e.CompanyCD=@CompanyCD and  e.CheckStatus='1'");
            //#endregion
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            ////公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //编号
            if (!string.IsNullOrEmpty(model.ProdNo))
            {
                searchSql.AppendLine("	and e.ProdNo LIKE @ProdNo ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProdNo", "%" + model.ProdNo + "%"));
            }
            //名称
            if (!string.IsNullOrEmpty(model.ProductName))
            {
                searchSql.AppendLine("	AND e.ProductName LIKE @ProductName ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", "%" + model.ProductName + "%"));
            }
            if (!string.IsNullOrEmpty(model.PYShort))
            {
                searchSql.AppendLine("	AND e.PYShort LIKE @PYShort ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PYShort", "%" + model.PYShort + "%"));
            }
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                searchSql.AppendLine("	AND e.ExtField" + EFIndex + " LIKE '%" + EFDesc + "%' ");
            }
            if (!string.IsNullOrEmpty(model.TypeID))
            {
                string ID = model.TypeID;
                string allID = "";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string[] IdS = null;
                ID = ID.Substring(0, ID.Length);
                IdS = ID.Split(',');

                for (int i = 0; i < IdS.Length; i++)
                {
                    IdS[i] = "'" + IdS[i] + "'";
                    sb.Append(IdS[i]);
                }
                allID = sb.ToString().Replace("''", "','");
                searchSql.AppendLine("	AND e.TypeID in  (" + allID + ") ");
            }
            searchSql.AppendLine("  )g ");
            if (!string.IsNullOrEmpty(conditions))
            {
                searchSql.AppendLine(" inner join (" + conditions + ")");
                searchSql.AppendLine(" k on g.ID=k.ProductID");
            }
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }

        /// <summary>
        /// 采购列表物品查询
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable PurchaseSearchProduct(ProductInfoModel model, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            //例如：conditions=select distinct isnull(a.ProductID,b.ProductID) as ProductID from officedba.SubSellBackDetail a full join officedba.SubSellOrderDetail b on a.ProductID=b.ProductID
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select distinct e.ProdNo,e.ProductName,isnull(e.PYShort,'')as PYShort,isnull(Convert(numeric(10,2),e.InTaxRate),0) as InTaxRate,isnull(Convert(numeric(10,2),e.TaxBuy),0)as TaxBuy,");
            searchSql.AppendLine(" e.ID,n.CodeName,isnull(f.CodeName,'') as CodeTypeName");
            searchSql.AppendLine(",e.CompanyCD,isnull(e.Specification,'') as Specification,isnull(Convert(numeric(10,2),e.TaxRate),0)as TaxRate,");
            searchSql.AppendLine(" isnull(Convert(numeric(10,2),e.SellTax),0) as SellTax, e.MinusIs,isnull(Convert(numeric(10,2),e.StandardBuy),0)as StandardBuy, isnull(Convert(numeric(6,2),e.Discount),0) as Discount,isnull(e.TypeID,'')as TypeID       ");
            searchSql.AppendLine(" ,e.UnitID,isnull(Convert(numeric(10,2),e.StandardSell),0) as StandardSell ,m.CurrentStore from (select a.ProdNo,isnull(Convert(numeric(10,2),Sum((isnull(d.ProductCount,0)+isnull(d.InCount,0)+isnull(d.RoadCount,0)-isnull(d.OutCount,0)-isnull(d.OrderCount,0)))),0)as CurrentStore");
            searchSql.AppendLine(" from officedba.ProductInfo as a left join officedba.StorageProduct as d on a.ID=d.productID                        ");
            searchSql.AppendLine(" left join officedba.CodeUnitType as b on a.UnitID=b.ID group by a.ProdNo) as m left join officedba.ProductInfo as e");
            searchSql.AppendLine(" on  e.ProdNo=m.ProdNo left join officedba.CodeUnitType as n on e.UnitID=n.ID left join officedba.CodeProductType ");
            searchSql.AppendLine(" as f on e.typeid=f.id   ");
            searchSql.AppendLine(" where e.CompanyCD=@CompanyCD and  e.CheckStatus='1'");
            //#endregion
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            ////公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //编号
            if (!string.IsNullOrEmpty(model.ProdNo))
            {
                searchSql.AppendLine("	and e.ProdNo LIKE @ProdNo ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProdNo", "%" + model.ProdNo + "%"));
            }
            //名称
            if (!string.IsNullOrEmpty(model.ProductName))
            {
                searchSql.AppendLine("	AND e.ProductName LIKE @ProductName ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", "%" + model.ProductName + "%"));
            }
            if (!string.IsNullOrEmpty(model.PYShort))
            {
                searchSql.AppendLine("	AND e.PYShort LIKE @PYShort ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@PYShort", "%" + model.PYShort + "%"));
            }
            if (!string.IsNullOrEmpty(model.TypeID))
            {
                string ID = model.TypeID;
                string allID = "";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string[] IdS = null;
                ID = ID.Substring(0, ID.Length);
                IdS = ID.Split(',');

                for (int i = 0; i < IdS.Length; i++)
                {
                    IdS[i] = "'" + IdS[i] + "'";
                    sb.Append(IdS[i]);
                }
                allID = sb.ToString().Replace("''", "','");
                searchSql.AppendLine("	AND e.TypeID in  (" + allID + ") ");
            }
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }

        /// <summary>
        /// 插入物品档案信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InsertProductInfo(ProductInfoModel model, out string ID, Hashtable htExtAttr)
        {
            //SQL拼写
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("INSERT INTO officedba.ProductInfo");
            sql.AppendLine("           (CompanyCD                      ");
            sql.AppendLine("           ,ProdNo                         ");
            sql.AppendLine("           ,PYShort                        ");
            sql.AppendLine("           ,ProductName                    ");
            sql.AppendLine("           ,ShortNam                       ");
            sql.AppendLine("           ,BarCode                        ");
            sql.AppendLine("           ,TypeID                         ");
            sql.AppendLine("           ,BigType                        ");
            sql.AppendLine("           ,GradeID                        ");
            if (int.Parse(model.Source) > 0)
            {
                sql.AppendLine("           ,Source                         ");
            }
            sql.AppendLine("           ,UnitID                         ");
            sql.AppendLine("           ,Brand                          ");
            sql.AppendLine("           ,Specification                  ");
            sql.AppendLine("           ,ColorID                        ");
            sql.AppendLine("           ,Size                           ");
            sql.AppendLine("           ,StockIs                        ");
            if (!string.IsNullOrEmpty(model.ABCType))
            {
                sql.AppendLine("           ,ABCType                        ");
            }
            sql.AppendLine("           ,Remark                         ");
            sql.AppendLine("           ,Creator                        ");
            sql.AppendLine("           ,CreateDate                     ");
            sql.AppendLine("           ,CheckStatus                    ");
            sql.AppendLine("           ,CheckUser                      ");
            sql.AppendLine("           ,CheckDate                      ");
            sql.AppendLine("           ,UsedStatus                     ");
            sql.AppendLine("           ,ModifiedDate                   ");
            sql.AppendLine("           ,ModifiedUserID                 ");
            sql.AppendLine("           ,FromAddr                       ");
            sql.AppendLine("           ,DrawingNum                     ");
            sql.AppendLine("           ,ImgUrl                         ");
            sql.AppendLine("           ,FileNo                         ");
            sql.AppendLine("           ,PricePolicy                    ");
            sql.AppendLine("           ,Params                         ");
            sql.AppendLine("           ,Questions                      ");
            sql.AppendLine("           ,ReplaceName                    ");
            sql.AppendLine("           ,Description                    ");
            sql.AppendLine("           ,MinusIs                        ");
            sql.AppendLine("           ,StorageID                      ");
            sql.AppendLine("           ,SafeStockNum                   ");
            sql.AppendLine("           ,MinStockNum                    ");
            sql.AppendLine("           ,MaxStockNum                    ");
            sql.AppendLine("           ,CalcPriceWays                  ");
            sql.AppendLine("           ,StandardCost                   ");
            sql.AppendLine("           ,PlanCost                       ");
            sql.AppendLine("           ,StandardSell                   ");
            sql.AppendLine("           ,SellMin                        ");
            sql.AppendLine("           ,SellMax                        ");
            sql.AppendLine("           ,TaxRate                        ");
            sql.AppendLine("           ,InTaxRate                      ");
            sql.AppendLine("           ,SellTax                        ");
            sql.AppendLine("           ,SellPrice                      ");
            sql.AppendLine("           ,TransferPrice                  ");
            sql.AppendLine("           ,Discount                       ");
            sql.AppendLine("           ,StandardBuy                    ");
            sql.AppendLine("           ,TaxBuy                         ");
            sql.AppendLine("           ,Manufacturer                   ");
            sql.AppendLine("           ,Material                       ");
            sql.AppendLine("           ,GroupUnitNo                    ");
            sql.AppendLine("           ,SaleUnitID                     ");
            sql.AppendLine("           ,InUnitID                       ");
            sql.AppendLine("           ,StockUnitID                    ");
            sql.AppendLine("           ,MakeUnitID                     ");
            sql.AppendLine("           ,IsBatchNo                      ");
            sql.AppendLine("           ,BuyMax)                        ");
            sql.AppendLine("     VALUES                                ");
            sql.AppendLine("           (@CompanyCD                     ");
            sql.AppendLine("           ,@ProdNo                        ");
            sql.AppendLine("           ,@PYShort                       ");
            sql.AppendLine("           ,@ProductName                   ");
            sql.AppendLine("           ,@ShortNam                      ");
            sql.AppendLine("           ,@BarCode                       ");
            sql.AppendLine("           ,@TypeID                        ");
            sql.AppendLine("           ,@BigType                       ");
            sql.AppendLine("           ,@GradeID                       ");
            if (int.Parse(model.Source) > 0)
            {
                sql.AppendLine("           ,@Source                        ");
            }
            sql.AppendLine("           ,@UnitID                        ");
            sql.AppendLine("           ,@Brand                         ");
            sql.AppendLine("           ,@Specification                 ");
            sql.AppendLine("           ,@ColorID                       ");
            sql.AppendLine("           ,@Size                          ");
            sql.AppendLine("           ,@StockIs                       ");
            if (!string.IsNullOrEmpty(model.ABCType))
            {
                sql.AppendLine("           ,@ABCType                       ");
            }
            sql.AppendLine("           ,@Remark                        ");
            sql.AppendLine("           ,@Creator                       ");
            sql.AppendLine("           ,@CreateDate                    ");
            sql.AppendLine("           ,@CheckStatus                   ");
            sql.AppendLine("           ,@CheckUser                     ");
            sql.AppendLine("           ,@CheckDate                     ");
            sql.AppendLine("           ,@UsedStatus                    ");
            sql.AppendLine("           ,@ModifiedDate                  ");
            sql.AppendLine("           ,@ModifiedUserID               ");
            sql.AppendLine("           ,@FromAddr       ");
            sql.AppendLine("           ,@DrawingNum     ");
            sql.AppendLine("           ,@ImgUrl         ");
            sql.AppendLine("           ,@FileNo         ");
            sql.AppendLine("           ,@PricePolicy    ");
            sql.AppendLine("           ,@Params         ");
            sql.AppendLine("           ,@Questions      ");
            sql.AppendLine("           ,@ReplaceName    ");
            sql.AppendLine("           ,@Description    ");
            sql.AppendLine("           ,@MinusIs              ");
            sql.AppendLine("           ,@StorageID               ");
            sql.AppendLine("           ,@SafeStockNum         ");
            sql.AppendLine("           ,@MinStockNum          ");
            sql.AppendLine("           ,@MaxStockNum          ");
            sql.AppendLine("           ,@CalcPriceWays        ");
            sql.AppendLine("           ,@StandardCost         ");
            sql.AppendLine("           ,@PlanCost             ");
            sql.AppendLine("           ,@StandardSell         ");
            sql.AppendLine("           ,@SellMin              ");
            sql.AppendLine("           ,@SellMax              ");
            sql.AppendLine("           ,@TaxRate              ");
            sql.AppendLine("           ,@InTaxRate            ");
            sql.AppendLine("           ,@SellTax              ");
            sql.AppendLine("           ,@SellPrice            ");
            sql.AppendLine("           ,@TransferPrice        ");
            sql.AppendLine("           ,@Discount             ");
            sql.AppendLine("           ,@StandardBuy          ");
            sql.AppendLine("           ,@TaxBuy               ");
            sql.AppendLine("           ,@Manufacturer         ");
            sql.AppendLine("           ,@Material             ");
            sql.AppendLine("           ,@GroupUnitNo           ");
            sql.AppendLine("           ,@SaleUnitID            ");
            sql.AppendLine("           ,@InUnitID              ");
            sql.AppendLine("           ,@StockUnitID           ");
            sql.AppendLine("           ,@MakeUnitID            ");
            sql.AppendLine("           ,@IsBatchNo            ");
            sql.AppendLine("           ,@BuyMax)              ");
            sql.AppendLine("   SET @ID= @@IDENTITY  ");
            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            //设置存储过程名
            comm.CommandText = sql.ToString();
            //设置保存的参数
            SetSaveParameter(comm, model);
            //添加返回参数
            comm.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));
            ArrayList lstCmd = new ArrayList();
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(model, htExtAttr, cmd);


            lstCmd.Add(comm);
            if (htExtAttr != null)
                lstCmd.Add(cmd);

            //执行登陆操作
            bool isSucc = SqlHelper.ExecuteTransWithArrayList(lstCmd);
            //设置ID
            //model.ID = int.Parse(comm.Parameters["@ProdID"].Value);
            ID = comm.Parameters["@ID"].Value.ToString();
            return isSucc;
        }

        /// <summary>
        /// 扩展属性保存操作
        /// </summary>
        /// <returns></returns>
        private static void GetExtAttrCmd(ProductInfoModel model, Hashtable htExtAttr, SqlCommand cmd)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "UPDATE officedba.ProductInfo set ";
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql += de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",";
                    cmd.Parameters.AddWithValue("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim());
                }
                int iLength = strSql.Length - 1;
                strSql = strSql.Substring(0, iLength);
                strSql += " where CompanyCD = @CompanyCD  AND ProdNo = @ProdNo";
                cmd.Parameters.AddWithValue("@CompanyCD", model.CompanyCD);
                cmd.Parameters.AddWithValue("@ProdNo", model.ProdNo);
                cmd.CommandText = strSql;
            }
            catch
            { }


        }

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="comm"></param>
        /// <param name="model"></param>
        private static void SetSaveParameter(SqlCommand comm, ProductInfoModel model)
        {
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));//公司代码                                                
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProdNo", model.ProdNo));//物品编号                                                      
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PYShort", model.PYShort));//拼音缩写                                                    
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", model.ProductName));//物品名称                                            
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ShortNam", model.ShortNam));//名称简称                                                  
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BarCode", model.BarCode));//条码                                                        
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TypeID", model.TypeID));//物品分类ID（对应物品分类代码表ID）                            
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BigType", model.BigType));//所属大类
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@GradeID", model.GradeID));//物品档次级别ID（对应分类代码表ID）                          
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UnitID", model.UnitID));//单位ID（计量单位ID，对应计量单位表ID）                        
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Brand", model.Brand));//品牌ID（对应分类代码表ID）                                      
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ColorID", model.ColorID));//颜色ID（对应分类代码表ID）                                  
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Specification", model.Specification));//规格型号                                        
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Size", model.Size));//尺寸                                                              
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Source", model.Source));//物品来源分类（1：库存；2：生产；3：外购；4：委托加工）        
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromAddr", model.FromAddr));//产地                                                      
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DrawingNum", model.DrawingNum));//图号                                                  
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ImgUrl", model.ImgUrl));//产品图片                                                      
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@FileNo", model.FileNo));//批准文号                                                      
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PricePolicy", model.PricePolicy));//价格策略                                            
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Params", model.Params));//技术参数                                                      
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Questions", model.Questions));//常见问题                                                
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ReplaceName", model.ReplaceName));//替代品名称                                          
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Description", model.Description));//物品描述信息                                        
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StockIs", model.StockIs));//是否计入库存（0否,1是）                                     
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@MinusIs", model.MinusIs));//是否允许负库存（0否,1是）                                   
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", model.StorageID));//主放仓库(对应仓库表ID)                                  
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SafeStockNum", model.SafeStockNum));//安全库存量                                        
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@MinStockNum", model.MinStockNum));//最低库存量                                          
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@MaxStockNum", model.MaxStockNum));//最高库存量                                          
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ABCType", model.ABCType));//ABC分类（A/B/C）                                            
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CalcPriceWays", model.CalcPriceWays));//成本核算计价方法
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StandardCost", model.StandardCost));//标准成本                                          
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@PlanCost", model.PlanCost));//计划成本                                                  
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StandardSell", model.StandardSell));//标准售价                                          
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SellMin", model.SellMin));//最低售价限制                                                
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SellMax", model.SellMax));//最高售价限制                                                
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaxRate", model.TaxRate));//销项税率(%)                                                 
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@InTaxRate", model.InTaxRate));//进项税率(%)                                             
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SellTax", model.SellTax));//含税售价                                                    
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SellPrice", model.SellPrice));//零售价                                                  
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TransferPrice", model.TransferPrice));//调拨单价                                        
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Discount", model.Discount));//折扣率（%）                                               
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StandardBuy", model.StandardBuy));//采购含税价                                          
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaxBuy", model.TaxBuy));//采购单价                                                      
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BuyMax", model.BuyMax));//最高采购价限制                                                
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", model.Remark));//备注    
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Manufacturer", model.Manufacturer));//备注 
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Material", model.Material));//备注                               
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator", model.Creator));//建档人ID                                                    
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateDate", model.CreateDate));//建档日期                                              
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CheckStatus", model.CheckStatus));//审核状态（0草稿，1已审）                            
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CheckUser", model.CheckUser));//审核人ID                                                
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CheckDate", model.CheckDate));//审核日期                                                
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", model.UsedStatus));//启用状态（0停用，1启用）                              
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedDate", model.ModifiedDate));//最后更新日期                                      
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));//最后更新用户ID(对应操作用户U  serID) 
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@GroupUnitNo", model.GroupNo));//计量单位组编号
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SaleUnitID", model.SellUnit));//销售单位
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@InUnitID", model.PurchseUnit));//采购单位
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StockUnitID", model.StorageUnit));//库存单位
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@MakeUnitID", model.ProductUnit));//生产完工入库单位
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@IsBatchNo", model.IsBatchNo));//生产完工入库单位

        }

        /// <summary>
        /// 修改物品档案
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateProductInfo(ProductInfoModel model, Hashtable htExtAttr)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE officedba.ProductInfo                ");
            sql.AppendLine("   SET                                      ");
            sql.AppendLine("      PYShort = @PYShort                    ");
            sql.AppendLine("      ,ProductName = @ProductName           ");
            sql.AppendLine("      ,ShortNam = @ShortNam                 ");
            sql.AppendLine("      ,BarCode = @BarCode                   ");
            sql.AppendLine("      ,TypeID = @TypeID                     ");
            sql.AppendLine("      ,BigType = @BigType                   ");
            sql.AppendLine("      ,GradeID = @GradeID                   ");
            if (int.Parse(model.Source) > 0)
            {
                sql.AppendLine("      ,Source = @Source                    ");
            }
            sql.AppendLine("      ,UnitID = @UnitID                    ");
            sql.AppendLine("      ,Brand = @Brand                      ");
            sql.AppendLine("      ,Specification = @Specification      ");
            sql.AppendLine("      ,ColorID = @ColorID                  ");
            sql.AppendLine("      ,Size = @Size                        ");
            sql.AppendLine("      ,StockIs = @StockIs                  ");
            if (!string.IsNullOrEmpty(model.ABCType))
            {
                sql.AppendLine("      ,ABCType = @ABCType                  ");
            }
            sql.AppendLine("      ,Remark = @Remark                    ");
            sql.AppendLine("      ,Creator = @Creator                   ");
            sql.AppendLine("      ,CreateDate = @CreateDate             ");
            sql.AppendLine("      ,CheckStatus = @CheckStatus           ");
            sql.AppendLine("      ,CheckUser = @CheckUser               ");
            sql.AppendLine("      ,CheckDate = @CheckDate               ");
            sql.AppendLine("      ,UsedStatus = @UsedStatus             ");
            sql.AppendLine("      ,ModifiedDate = @ModifiedDate         ");
            sql.AppendLine("      ,ModifiedUserID = @ModifiedUserID     ");
            sql.AppendLine("      ,FromAddr = @FromAddr                 ");
            sql.AppendLine("      ,DrawingNum = @DrawingNum            ");
            sql.AppendLine("      ,ImgUrl = @ImgUrl                     ");
            sql.AppendLine("      ,FileNo = @FileNo                     ");
            sql.AppendLine("      ,PricePolicy = @PricePolicy           ");
            sql.AppendLine("      ,Params = @Params                     ");
            sql.AppendLine("      ,Questions = @Questions               ");
            sql.AppendLine("      ,ReplaceName = @ReplaceName           ");
            sql.AppendLine("      ,Description = @Description           ");
            sql.AppendLine("      ,MinusIs = @MinusIs                    ");
            sql.AppendLine("      ,StorageID = @StorageID                 ");
            sql.AppendLine("      ,SafeStockNum = @SafeStockNum           ");
            sql.AppendLine("      ,MinStockNum = @MinStockNum             ");
            sql.AppendLine("      ,MaxStockNum = @MaxStockNum             ");
            sql.AppendLine("      ,CalcPriceWays = @CalcPriceWays         ");
            sql.AppendLine("      ,StandardCost = @StandardCost           ");
            sql.AppendLine("      ,PlanCost = @PlanCost                   ");
            sql.AppendLine("      ,StandardSell = @StandardSell           ");
            sql.AppendLine("      ,SellMin = @SellMin                     ");
            sql.AppendLine("      ,SellMax = @SellMax                     ");
            sql.AppendLine("      ,TaxRate = @TaxRate                     ");
            sql.AppendLine("      ,InTaxRate = @InTaxRate                 ");
            sql.AppendLine("      ,SellTax = @SellTax                     ");
            sql.AppendLine("      ,SellPrice = @SellPrice                 ");
            sql.AppendLine("      ,TransferPrice =@TransferPrice          ");
            sql.AppendLine("      ,Discount = @Discount                   ");
            sql.AppendLine("      ,StandardBuy = @StandardBuy             ");
            sql.AppendLine("      ,TaxBuy = @TaxBuy                       ");
            sql.AppendLine("      ,BuyMax = @BuyMax                       ");
            sql.AppendLine("      ,Manufacturer = @Manufacturer           ");
            sql.AppendLine("      ,GroupUnitNo = @GroupUnitNo             ");
            sql.AppendLine("      ,SaleUnitID = @SaleUnitID               ");
            sql.AppendLine("      ,InUnitID = @InUnitID                   ");
            sql.AppendLine("      ,StockUnitID = @StockUnitID             ");
            sql.AppendLine("      ,MakeUnitID = @MakeUnitID               ");
            sql.AppendLine("      ,Material = @Material                   ");
            sql.AppendLine("      ,IsBatchNo = @IsBatchNo                 ");
            sql.AppendLine("      where                                   ");
            sql.AppendLine(" 	CompanyCD = @CompanyCD                    ");
            sql.AppendLine(" 	AND ProdNo = @ProdNo                      ");
            //定义更新基本信息的命令  
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql.ToString();
            SetSaveParameter(comm, model);//其他参数
            //执行更新并设置更新结果
            bool result = false;
            ArrayList lstCmd = new ArrayList();
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(model, htExtAttr, cmd);
            lstCmd.Add(comm);
            if (htExtAttr != null)
                lstCmd.Add(cmd);


            result = SqlHelper.ExecuteTransWithArrayList(lstCmd);
            return result;
        }

        /// <summary>
        /// 周军，获取物品扩展属性值方法
        /// </summary>
        /// <returns></returns>
        public static DataTable GetExtAttrValue(string strKey, string strProNo, string CompanyCD)
        {
            //DataTable dt = new DataTable();
            string strSql = "select " + strKey + " from officedba.ProductInfo where CompanyCD = @CompanyCD AND ProdNo = @ProdNo ";
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", CompanyCD));
            arr.Add(new SqlParameter("@ProdNo", strProNo));
            return SqlHelper.ExecuteSql(strSql.ToString(), arr);
        }


        /// <summary>
        /// 删除物品信息
        /// </summary>
        /// <param name="TypeFlag"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteProductInfo(string ID, string CompanyCD)
        {
            string allID = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string[] Delsql = new string[1];
            try
            {
                string[] IdS = null;
                ID = ID.Substring(0, ID.Length);
                IdS = ID.Split(',');

                for (int i = 0; i < IdS.Length; i++)
                {
                    IdS[i] = "'" + IdS[i] + "'";
                    sb.Append(IdS[i]);
                }
                allID = sb.ToString().Replace("''", "','");

                Delsql[0] = "delete from  officedba.ProductInfo where ID IN (" + allID + ") and CompanyCD = @CompanyCD and CheckStatus='0'";

                SqlCommand comm = new SqlCommand();
                comm.CommandText = Delsql[0].ToString();

                //设置参数
                comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                ArrayList lstDelete = new ArrayList();
                comm.CommandText = Delsql[0].ToString();
                //添加基本信息更新命令
                lstDelete.Add(comm);
                return SqlHelper.ExecuteTransWithArrayList(lstDelete);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region 判断当前ProductID记录是否存在
        public static bool Exists(string ProductID, string CompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from officedba.StorageProduct");
            strSql.Append(" where  ProductID=@ProductID and CompanyCD=@CompanyCD and DeptID is null ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ProductID", SqlDbType.VarChar),
                    new SqlParameter("@CompanyCD", SqlDbType.VarChar),};
            parameters[0].Value = ProductID;
            parameters[1].Value = CompanyCD;
            return SqlHelper.Exists(strSql.ToString(), parameters);
        }
        #endregion

        public static DataTable GetProductInfoByID(int ID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT a.ID                             ");
            sql.AppendLine("      ,a.CompanyCD                      ");
            sql.AppendLine("      ,a.ProdNo                         ");
            sql.AppendLine("      ,a.PYShort                        ");
            sql.AppendLine("      ,a.ProductName                    ");
            sql.AppendLine("      ,a.ShortNam                       ");
            sql.AppendLine("      ,a.BarCode                        ");
            sql.AppendLine("      ,a.TypeID                         ");
            sql.AppendLine("      ,a.BigType                        ");
            sql.AppendLine("      ,a.GradeID                        ");
            sql.AppendLine("      ,a.Source                         ");
            sql.AppendLine("      ,a.UnitID                         ");
            sql.AppendLine("      ,a.Brand                          ");
            sql.AppendLine("      ,a.Specification                  ");
            sql.AppendLine("      ,a.ColorID                        ");
            sql.AppendLine("      ,a.Size                           ");
            sql.AppendLine("      ,a.StockIs                        ");
            sql.AppendLine("      ,a.ABCType                        ");
            sql.AppendLine("      ,a.Remark                         ");
            sql.AppendLine("      ,a.Creator                        ");
            sql.AppendLine(" 	,CONVERT(VARCHAR(10),a.CreateDate  ,21) AS CreateDate ");
            sql.AppendLine("      ,a.CheckStatus                    ");
            sql.AppendLine("      ,a.CheckUser                      ");
            sql.AppendLine(" 	,CONVERT(VARCHAR(10),a.CheckDate ,21) AS CheckDate ");
            sql.AppendLine("      ,a.UsedStatus                     ");
            sql.AppendLine(" 	,CONVERT(VARCHAR(10),a.ModifiedDate ,21) AS ModifiedDate ");
            sql.AppendLine("      ,a.ModifiedUserID                 ");
            sql.AppendLine("      ,a.FromAddr                       ");
            sql.AppendLine("      ,a.DrawingNum                     ");
            sql.AppendLine("      ,a.ImgUrl                         ");
            sql.AppendLine("      ,a.FileNo                         ");
            sql.AppendLine("      ,a.PricePolicy                    ");
            sql.AppendLine("      ,a.Params                         ");
            sql.AppendLine("      ,a.Questions                      ");
            sql.AppendLine("      ,a.ReplaceName                    ");
            sql.AppendLine("      ,a.Description                    ");
            sql.AppendLine("      ,a.MinusIs                        ");
            sql.AppendLine("      ,a.StorageID                      ");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.SafeStockNum) as SafeStockNum");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.MinStockNum) as MinStockNum");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.MaxStockNum) as MaxStockNum");
            sql.AppendLine("      ,a.CalcPriceWays                  ");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.StandardCost) as StandardCost");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.PlanCost) as PlanCost");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.StandardSell) as StandardSell");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.SellMin) as SellMin");
            sql.AppendLine("      ,a.Material                       ");
            sql.AppendLine("      ,a.Manufacturer                   ");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.SellMax) as SellMax");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.TaxRate) as TaxRate");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.InTaxRate) as InTaxRate");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.SellTax) as SellTax");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.SellPrice) as SellPrice");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.TransferPrice) as TransferPrice");
            sql.AppendLine("      ,Convert(numeric(12," + userInfo.SelPoint + "),a.Discount) as Discount");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.StandardBuy) as StandardBuy");
            sql.AppendLine("      ,a.GroupUnitNo                    ");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.TaxBuy) as TaxBuy");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.BuyMax) as BuyMax");
            sql.AppendLine("      ,a.SaleUnitID                     ");
            sql.AppendLine("      ,a.InUnitID                       ");
            sql.AppendLine("      ,a.StockUnitID                    ");
            sql.AppendLine("      ,a.MakeUnitID                     ");
            sql.AppendLine("      ,a.IsBatchNo                      ");
            sql.AppendLine("      ,b.CodeName                       ");
            sql.AppendLine("      ,b.TypeFlag                       ");
            sql.AppendLine("      ,f.GroupUnitName                  ");
            sql.AppendLine("      ,c.EmployeeName as CreatorName    ");
            sql.AppendLine("      ,d.EmployeeName as CheckUserName  ");
            sql.AppendLine("  FROM officedba.ProductInfo as a       ");
            sql.AppendLine("left join officedba.EmployeeInfo as c on a.Creator=c.ID     ");
            sql.AppendLine("left join officedba.EmployeeInfo as d on a.CheckUser=d.ID   ");
            sql.AppendLine("left join officedba.UnitGroup as f on a.GroupUnitNo=f.GroupUnitNo and a.CompanyCD=f.CompanyCD   ");
            sql.AppendLine(" left join officedba.CodeProductType as b on a.TypeID=b.ID where a.ID=@ID");
            SqlParameter[] param = new SqlParameter[1];
            //人员ID
            param[0] = SqlHelper.GetParameter("@ID", ID);
            //执行查询
            DataTable data = SqlHelper.ExecuteSql(sql.ToString(), param);
            return data;
        }


        #region 查询：当前库存量
        /// <summary>
        /// 当前库存量
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStorageCount(int ProductID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            string sql = "select sum(Convert(numeric(22," + userInfo.SelPoint + "),ProductCount))as ProductCount from officedba.StorageProduct where ProductID=@ProductID";
            SqlParameter[] param = new SqlParameter[1];
            //人员ID
            param[0] = SqlHelper.GetParameter("@ProductID", ProductID);
            return SqlHelper.ExecuteSql(sql, param);
        }
        #endregion


        #region 判断当前storageID中对应的ProductID记录是否存在
        public static bool Exists(string storageID, string ProductID, string CompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from officedba.StorageProduct");
            strSql.Append(" where storageID=@storageID and ProductID=@ProductID and CompanyCD=@CompanyCD ");
            SqlParameter[] parameters = {
					new SqlParameter("@storageID", SqlDbType.VarChar),
                    new SqlParameter("@ProductID", SqlDbType.VarChar),
                    new SqlParameter("@CompanyCD", SqlDbType.VarChar),};
            parameters[0].Value = storageID;
            parameters[1].Value = ProductID;
            parameters[2].Value = CompanyCD;
            return SqlHelper.Exists(strSql.ToString(), parameters);
        }
        #endregion


        public static bool UpdateStatus(int ProductID, string Status, string CheckUser, string CheckDate, string ModifiedUserID, string StorageID, string CompanyCD)
        {
            ArrayList listADD = new ArrayList();
            string sql = "update officedba.ProductInfo set CheckStatus=@CheckStatus,CheckUser=@CheckUser,CheckDate=@CheckDate,UsedStatus='1',ModifiedUserID=@ModifiedUserID,ModifiedDate=@ModifiedDate where ID=@ID";
            string sql2 = " insert into officedba.StorageProduct(StorageID,ProductID,CompanyCD) values(@StorageID,@ProductID2,@CompanyCD)";
            SqlCommand comm = new SqlCommand();
            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameter("@CheckStatus", Status));
            comm.Parameters.Add(SqlHelper.GetParameter("@CheckUser", CheckUser));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", System.DateTime.Now));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", ModifiedUserID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", ProductID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CheckDate", CheckDate));
            ArrayList lst = new ArrayList();
            comm.CommandText = sql;
            //添加基本信息更新命令
            lst.Add(comm);

            SqlCommand comm2 = new SqlCommand();
            comm2.Parameters.Add(SqlHelper.GetParameter("@StorageID", StorageID));
            comm2.Parameters.Add(SqlHelper.GetParameter("@ProductID2", ProductID));
            comm2.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            comm2.CommandText = sql2;
            bool flag = Exists(StorageID, Convert.ToString(ProductID), CompanyCD);
            if (!flag)
                lst.Add(comm2);
            return SqlHelper.ExecuteTransWithArrayList(lst);

        }


        #region 根据扫描条码获取商品信息
        /// <summary>
        /// 根据扫描条码获取商品信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetDtGoodsInfoByBarcode(string CompanyCD, string Barcode, string QueryID)
        {
            //定义查询的命令
            string sql = "";
            sql += "select       ISNULL(e.IsBatchNo,'')IsBatchNo,isnull(Convert(numeric(10,2),e.StandardBuy),0) as StandardBuy,e.ProdNo,e.ModifiedDate,e.ProductName,isnull(e.Source,'')as Source,isnull(e.PYShort,'')as PYShort,isnull(Convert(numeric(10,2),e.InTaxRate),0) as InTaxRate,isnull(Convert(numeric(10,2),e.TaxBuy),0)as TaxBuy,";
            sql += " e.ID,n.CodeName,isnull(f.CodeName,'') as CodeTypeName,ISNULL(cpt.TypeName,'') AS ColorName,ISNULL(cptm.TypeName,'') AS MaterialName";
            sql += ",e.CompanyCD,isnull(e.Specification,'') as Specification,isnull(Convert(numeric(10,2),e.TaxRate),0)as TaxRate,";
            sql += " isnull(Convert(numeric(10,2),e.SellTax),0) as SellTax, e.MinusIs,isnull(Convert(numeric(10,2),e.StandardCost),0)as StandardCost, isnull(Convert(numeric(6,2),e.Discount),0) as Discount,isnull(e.TypeID,'')as TypeID       ";
            sql += " ,e.UnitID,isnull(Convert(numeric(10,2),e.StandardSell),0) as StandardSell ,m.CurrentStore,m.ProductCount from (select a.companycd,a.ProdNo,isnull(Convert(numeric(10,2), Sum((isnull(d.ProductCount,0)))),0)as ProductCount,isnull(Convert(numeric(10,2), ";
            sql += " Sum((isnull(d.ProductCount,0)+isnull(d.InCount,0)+isnull(d.RoadCount,0)-isnull(d.OutCount,0)-isnull(d.OrderCount,0)))),0)as CurrentStore ";
            sql += " from officedba.ProductInfo as a ";
            sql += " left join officedba.StorageProduct as d on a.ID=d.productID   and d.companycd='" + CompanyCD + "'                     ";
            sql += " left join officedba.CodeUnitType as b on a.UnitID=b.ID ";
            if (!string.IsNullOrEmpty(QueryID))
            {
                if (QueryID != "DETAIL")
                {
                    if (QueryID != "0")
                    {
                        sql += " where d.StorageID='" + QueryID + "'";
                    }
                }
            }
            sql += " group by a.ProdNo,a.companycd) as m left join  officedba.ProductInfo as e";
            sql += " on  e.ProdNo=m.ProdNo left join officedba.CodeUnitType as n on e.UnitID=n.ID left join officedba.CodeProductType ";
            sql += " as f on e.typeid=f.id   ";
            sql += " LEFT JOIN officedba.CodePublicType cpt ON e.ColorID=cpt.ID ";
            sql += " LEFT JOIN officedba.CodePublicType cptm ON e.Material=cptm.ID ";
            if (!string.IsNullOrEmpty(QueryID))
            {
                if (QueryID != "DETAIL")
                {
                    if (QueryID != "0")
                    {
                        sql += " right join officedba.StorageProduct as sp on sp.ProductID=e.ID ";
                    }
                }
            }

            sql += " where e.CompanyCD='" + CompanyCD + "' and m.companycd='" + CompanyCD + "' and BarCode='" + Barcode + "'   ";
            sql += " and e.CheckStatus='1'";
            if (!string.IsNullOrEmpty(QueryID))
            {
                if (QueryID != "DETAIL")
                {
                    if (QueryID != "0")
                    {
                        sql += " and sp.StorageID='" + QueryID + "' and sp.deptid is null and sp.companycd='" + CompanyCD + "' ";
                    }
                }
            }
            try
            {
                return SqlHelper.ExecuteSql(sql);
            }
            catch 
            {
                return null;
            }
        }
        #endregion

        #region  验证条码的唯一性 add by hm
        /// <summary>
        /// 如果不存在则返回false，如果存在则返回true; add by hm
        /// </summary>
        /// <param name="BarCode"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool CheckBarCode(string BarCode, string CompanyCD)
        {
            string StrSql = "select count(*) from officedba.ProductInfo where CompanyCD='" + CompanyCD + "' and BarCode='" + BarCode + "' ";
            return SqlHelper.Exists(StrSql, null);
        }

        #endregion

        /// <summary>
        /// 根据企业编号获取企业设置的文件上传目录
        /// zxb 2009-08-17
        /// </summary>
        /// <param name="companycd">企业编号</param>
        /// <returns></returns>
        public static string GetCompanyUpFilePath(string companycd)
        {
            string sqlstr = "select * from pubdba.companyOpenServ where CompanyCD=@companycd";
            SqlParameter[] parameters = { new SqlParameter("@companycd", SqlDbType.VarChar, 50) };
            parameters[0].Value = companycd;
            DataTable dt = SqlHelper.ExecuteSql(sqlstr, parameters);
            if (!Directory.Exists(dt.Rows[0]["DocSavePath"].ToString()))
            {
                Directory.CreateDirectory(dt.Rows[0]["DocSavePath"].ToString());
            }
            return dt.Rows[0]["DocSavePath"].ToString();
        }

        /// <summary>
        /// 获取企业导入的商品Excel表的数据
        /// zxb 2009-08-17
        /// </summary>
        /// <param name="companycd">企业编号</param>
        /// <param name="fname">excel文件名</param>
        /// <param name="tbname">excel表名</param>
        /// <returns></returns>
        public static DataSet GetProductInfoFromExcel(string companycd, string fname, string tbname)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@compcode",companycd),
                new SqlParameter("@fname",fname),
                new SqlParameter("@tbname",tbname)
            };
            DataTable dt = SqlHelper.ExecuteStoredProcedure("[officedba].[excelSelect]", param);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            return ds;
        }
        #region  根据ID获取物品的扩展属性
        public static DataTable GetProductInfoByAttr(int ID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT ID            ");
            sql.AppendLine(",ExtField1  ");
            sql.AppendLine(",ExtField2  ");
            sql.AppendLine(",ExtField3  ");
            sql.AppendLine(",ExtField4  ");
            sql.AppendLine(",ExtField5  ");
            sql.AppendLine(",ExtField6  ");
            sql.AppendLine(",ExtField7  ");
            sql.AppendLine(",ExtField8  ");
            sql.AppendLine(",ExtField9  ");
            sql.AppendLine(",ExtField10 ");
            sql.AppendLine(",ExtField11 ");
            sql.AppendLine(",ExtField12 ");
            sql.AppendLine(",ExtField13 ");
            sql.AppendLine(",ExtField14 ");
            sql.AppendLine(",ExtField15 ");
            sql.AppendLine(",ExtField16 ");
            sql.AppendLine(",ExtField17 ");
            sql.AppendLine(",ExtField18 ");
            sql.AppendLine(",ExtField19 ");
            sql.AppendLine(",ExtField20 ");
            sql.AppendLine(",ExtField21 ");
            sql.AppendLine(",ExtField22 ");
            sql.AppendLine(",ExtField23 ");
            sql.AppendLine(",ExtField24 ");
            sql.AppendLine(",ExtField25 ");
            sql.AppendLine(",ExtField26 ");
            sql.AppendLine(",ExtField27 ");
            sql.AppendLine(",ExtField28 ");
            sql.AppendLine(",ExtField29 ");
            sql.AppendLine(" ,ExtField30");
            sql.AppendLine("  FROM officedba.ProductInfo where ID=@ID");
            SqlParameter[] param = new SqlParameter[1];
            //人员ID
            param[0] = SqlHelper.GetParameter("@ID", ID);
            //执行查询
            DataTable data = SqlHelper.ExecuteSql(sql.ToString(), param);
            return data;
        }
        #endregion
        /// <summary>
        /// 取Excel数据并读取到officedba.ProductInfo中
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public static DataSet ReadEexcel(string FilePath, string companycd)
        {
            string connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;data source=" + FilePath;
            string sql = "SELECT distinct * FROM [Sheet1$]";
            DataSet ds = new DataSet();
            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter(sql, connStr);
            da.Fill(ds);
            //删除历史记录
            SqlParameter[] paramter = { new SqlParameter("@companycd", companycd) };
            sql = "delete from officedba.ProductInfo_temp where [企业编号]=@companycd";
            SqlHelper.ExecuteTransSql(sql, paramter);
            //传到临时表中
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"\s");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                SqlParameter[] param = 
                {
                    new SqlParameter("@companycd",companycd),
                    new SqlParameter("@id",ds.Tables[0].Rows[i][0].ToString()),
                    new SqlParameter("@ProdNo",ds.Tables[0].Rows[i][1].ToString()),
                    new SqlParameter("@ProductName", r.Replace(ds.Tables[0].Rows[i][2].ToString().Trim()," ",100)),
                    new SqlParameter("@ShortNam",ds.Tables[0].Rows[i][3].ToString()),

                    new SqlParameter("@TypeID",ds.Tables[0].Rows[i][4].ToString()), 

                    new SqlParameter("@BarCode",ds.Tables[0].Rows[i][5].ToString()),
                    new SqlParameter("@UnitID",ds.Tables[0].Rows[i][6].ToString()),
                    new SqlParameter("@Specification",ds.Tables[0].Rows[i][7].ToString()),
                    new SqlParameter("@ColorID",ds.Tables[0].Rows[i][8].ToString()),
                    new SqlParameter("@FromAddr",ds.Tables[0].Rows[i][9].ToString()),
                    new SqlParameter("@StorageID",ds.Tables[0].Rows[i][10].ToString()),
                    new SqlParameter("@MinStockNum",ds.Tables[0].Rows[i][11].ToString()),
                    new SqlParameter("@MaxStockNum",ds.Tables[0].Rows[i][12].ToString()),
                    new SqlParameter("@SafeStockNum",ds.Tables[0].Rows[i][13].ToString()),
                    new SqlParameter("@StandardSell",ds.Tables[0].Rows[i][14].ToString()),
                    new SqlParameter("@TaxRate",ds.Tables[0].Rows[i][15].ToString()),
                    new SqlParameter("@InTaxRate",ds.Tables[0].Rows[i][16].ToString()),
                    new SqlParameter("@SellTax",ds.Tables[0].Rows[i][17].ToString()),
                    new SqlParameter("@StandardBuy",ds.Tables[0].Rows[i][18].ToString()),
                    new SqlParameter("@TaxBuy",ds.Tables[0].Rows[i][19].ToString()),
                    new SqlParameter("@StandardCost",ds.Tables[0].Rows[i][20].ToString())
                };
                string lenstr = string.Empty;
                for (int j = 0; j < 20; j++)
                {
                    if (ds.Tables[0].Rows[i][j].ToString().Trim().Length < 1)
                    {
                        lenstr += "#";
                    }
                }
                if (lenstr.Length == 20)
                {
                    continue;
                }
                sql = "insert into officedba.ProductInfo_temp values(@id,@companycd,@ProdNo,@ProductName,@ShortNam,@BarCode,@UnitID,@Specification,@ColorID,@FromAddr,@StorageID,@MinStockNum,@MaxStockNum,@SafeStockNum,@StandardSell,@TaxRate,@InTaxRate,@SellTax,@StandardBuy,@TaxBuy,@TypeID,@StandardCost)";
                SqlHelper.ExecuteTransSql(sql, param);
            }
            sql = "select * from officedba.ProductInfo_temp where [企业编号]=@companycd order by [流水号]";
            ds = new DataSet();
            SqlParameter[] paramter1 = { new SqlParameter("@companycd", companycd) };
            DataTable dt = SqlHelper.ExecuteSql(sql, paramter1);
            ds.Tables.Add(dt);
            return ds;
        }

        /// <summary>
        /// 判断指定商品中对给定的字段和值，是否存在对应值
        /// zxb 2009-08-18
        /// </summary>
        /// <param name="sqlTypeCode">字段名判断</param>
        /// <param name="compID"></param>
        /// <param name="productNo"></param>
        /// <param name="compNo"></param>
        /// <returns></returns>
        public static bool ChargeProduct(string sqlTypeCode, string codeNum, string compNo)
        {
            SqlParameter[] parameters = { new SqlParameter("@codeNum", SqlDbType.VarChar, 50),
                                          new SqlParameter("@compid",SqlDbType.VarChar,50)};
            parameters[0].Value = codeNum;
            parameters[1].Value = compNo;
            object obj = SqlHelper.ExecuteScalar("select count(*) from officedba.ProductInfo where " + sqlTypeCode + "=@codeNum and CompanyCD=@compid", parameters);
            return Convert.ToInt32(obj) > 0 ? true : false;
        }

        /// <summary>
        /// 判断给定库名和公司名，是否存在该仓库
        /// zxb 2009-08-18
        /// </summary>
        /// <param name="storagename"></param>
        /// <param name="compid"></param>
        /// <returns></returns>
        public static bool ChargeStorage(string storagename, string compid)
        {
            SqlParameter[] parameters = { new SqlParameter("@storagename", SqlDbType.VarChar, 50),
                                          new SqlParameter("@companyid",SqlDbType.VarChar,50)};
            parameters[0].Value = storagename;
            parameters[1].Value = compid;
            object obj = SqlHelper.ExecuteScalar("select count(*) from officedba.StorageInfo where StorageName=@storagename and CompanyCD=@companyid and UsedStatus=1", parameters);
            return Convert.ToInt32(obj) > 0 ? true : false;
        }

        /// <summary>
        /// 判断给定单位名称和公司名，是否存在该单位
        /// zxb 2009-08-18
        /// </summary>
        /// <param name="codename"></param>
        /// <param name="compid"></param>
        /// <returns></returns>
        public static bool ChargeCodeUnit(string codename, string compid)
        {
            SqlParameter[] parameters = { new SqlParameter("@codename", SqlDbType.VarChar, 50),
                                          new SqlParameter("@companyid",SqlDbType.VarChar,50)};
            parameters[0].Value = codename;
            parameters[1].Value = compid;
            object obj = SqlHelper.ExecuteScalar("select count(*) from officedba.CodeUnitType where CodeName=@codename and CompanyCD=@companyid", parameters);
            return Convert.ToInt32(obj) > 0 ? true : false;
        }

        /// <summary>
        /// 将企业上传的Excel中的数据追加到商品表中
        /// zxb 2009-08-18
        /// </summary>
        /// <param name="companycd">企业编号</param>
        /// <param name="fname">excel文件名</param>
        /// <param name="tbname">excel表名</param>
        /// <returns></returns>
        public static int GetExcelToProductInfo(string companycd, string usercode)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@compcode",companycd),
                new SqlParameter("@createPerson",usercode)
            };
            DataTable dt = SqlHelper.ExecuteStoredProcedure("[officedba].[excelIntoSql]", param);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            return ds.Tables[0].Rows.Count;
        }

        /// <summary>
        /// 删除给定路径的文件
        /// zxb 2009-08-19
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteFile(string companycd, string filename)
        {
            //获取用户的路径
            SqlParameter[] parameters = { new SqlParameter("@companycd", SqlDbType.VarChar, 50) };
            parameters[0].Value = companycd;
            DataTable dt = SqlHelper.ExecuteSql("select * from pubdba.companyOpenServ where CompanyCD=@companycd", parameters);
            string path = dt.Rows[0]["DocSavePath"].ToString() + @"\" + filename;
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
            }
        }

        /// <summary>
        /// 写入日志
        /// 2009-09-14 by zxb
        /// </summary>
        /// <param name="companyCD"></param>
        /// <param name="deptID"></param>
        /// <param name="exportEmpID"></param>
        /// <param name="exportObject"></param>
        /// <param name="exportNum"></param>
        /// <param name="exportResult"></param>
        /// <param name="exportError"></param>
        public static void LogInsert(string companyCD, int deptID, string exportEmpID, string exportObject, int exportNum, int exportResult, string exportError)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@CompanyCD",companyCD),
                new SqlParameter("@DeptID",deptID),
                new SqlParameter("@ExportEmpID",exportEmpID),
                new SqlParameter("@ExportObject",exportObject),
                new SqlParameter("@ExportNum",exportNum),
                new SqlParameter("@ExportResult",exportResult),
                new SqlParameter("@exportError",exportError)
            };
            string sql = "insert into officedba.ExportLog(CompanyCD,DeptID,ExportEmpID,ExportObject,ExportNum,ExportResult,ExportError) values(@CompanyCD,@DeptID,@ExportEmpID,@ExportObject,@ExportNum,@ExportResult,@exportError)";
            SqlHelper.ExecuteTransSql(sql, param);
        }

        /// <summary>
        /// 导入日志分页
        /// 2009-09-11 by zxb
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <param name="mod"></param>
        /// <param name="companycd"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetLogPage(string userid, string begindate, string enddate, string mod, string companycd, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@pageindex",pageIndex),
                new SqlParameter("@pagesize",pageCount),
                new SqlParameter("@usercode",userid),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@mod",mod),
                new SqlParameter("@companycd",companycd),
                new SqlParameter("@order",OrderBy)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[ExpLogPage]", param);
            totalCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        public static DataSet GetError(string ncode)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@id",ncode)
            };

            DataTable dt = SqlHelper.ExecuteSql("select * from officedba.ExportLog where ID=@id", param);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            return ds;
        }

        /// <summary>
        /// 获取日志菜单名称
        /// 2009-09-23 by zxb
        /// </summary>
        /// <param name="ncode"></param>
        /// <returns></returns>
        public static string GetLogTitle(string ncode)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@id",ncode)
            };

            DataTable dt = SqlHelper.ExecuteSql("select ModuleName from pubdba.SysModule where ModuleID=@id", param);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            return ds.Tables[0].Rows[0][0].ToString();
        }

        /// <summary>
        /// 判断物品类别
        /// zxb 2009-12-30
        /// </summary>
        /// <param name="codename"></param>
        /// <param name="compid"></param>
        /// <returns></returns>
        public static bool ChargeCodeType(string codename, string compid)
        {
            SqlParameter[] parameters = { new SqlParameter("@codename", SqlDbType.VarChar, 50),
                                          new SqlParameter("@companyid",SqlDbType.VarChar,50)};
            parameters[0].Value = codename;
            parameters[1].Value = compid;
            object obj = SqlHelper.ExecuteScalar("select count(*) from officedba.CodeProductType where CodeName=@codename and CompanyCD=@companyid and UsedStatus=1", parameters);
            return Convert.ToInt32(obj) > 0 ? true : false;
        }

        /// <summary>
        /// 判断条码是否重复
        /// </summary>
        /// <param name="codename"></param>
        /// <param name="compid"></param>
        /// <returns></returns>
        public static bool ChargeBarCode(string codename, string compid)
        {
            SqlParameter[] parameters = { new SqlParameter("@codename", SqlDbType.VarChar, 50),
                                          new SqlParameter("@companyid",SqlDbType.VarChar,50)};
            parameters[0].Value = codename;
            parameters[1].Value = compid;
            object obj = SqlHelper.ExecuteScalar("select count(*) from officedba.ProductInfo where BarCode=@codename and CompanyCD=@companyid", parameters);
            return Convert.ToInt32(obj) > 0 ? true : false;
        }
        /// <summary>
        /// 判断是否存在该颜色
        /// </summary>
        /// <param name="colorName"></param>
        /// <param name="companyCD"></param>
        /// <returns></returns>
        public static bool ValidateProductColor(string colorName, string companyCD)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("SELECT COUNT(*) FROM officedba.CodePublicType  ");
            sbSql.AppendLine(" WHERE TypeFlag=5 AND TypeCode=3 AND CompanyCD=@CompanyCD AND TypeName=@ColorName AND UsedStatus='1' ");

            SqlParameter[] sqlParams = new SqlParameter[2];
            int index = 0;
            sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            sqlParams[index++] = SqlHelper.GetParameter("@ColorName", colorName);

            object res = SqlHelper.ExecuteScalar(sbSql.ToString(), sqlParams);
            return Convert.ToInt32(res) > 0 ? true : false;

        }


        #region 查询计量单位组信息
        public static DataTable GetUnitGroupList(string CompanyCD, string GroupUnitNo)
        {
            //string sql = " SELECT a.ID,a.GroupUnitNo,a.UnitID,b.CodeName " +
            //                      " FROM Officedba.UnitGroupDetail a left join Officedba.CodeUnitType b on a.CompanyCD=b.CompanyCD and a.UnitID=b.ID" +
            //                      " WHERE a.CompanyCD=@CompanyCD and a.GroupUnitNo=@GroupUnitNo";
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("select info.ID ,info.GroupUnitNo,info.UnitID,c.CodeName from");
            sbSql.AppendLine("(");
            sbSql.AppendLine("	select b.ID,b.GroupUnitNo,b.BaseUnitID as UnitID from officedba.UnitGroup b");
            sbSql.AppendLine("	where b.CompanyCD=@CompanyCD and b.GroupUnitNo=@GroupUnitNo");
            sbSql.AppendLine("union all");
            sbSql.AppendLine("	(");
            sbSql.AppendLine("		select a.ID,a.GroupUnitNo,a.UnitID");
            sbSql.AppendLine("		from officedba.UnitGroupDetail a where a.CompanyCD=@CompanyCD and a.GroupUnitNo=@GroupUnitNo");
            sbSql.AppendLine("	)");
            sbSql.AppendLine(") as info");
            sbSql.AppendLine("left join officedba.CodeUnitType c on info.UnitID=c.ID");
            //----------------------
            SqlParameter[] param = new SqlParameter[2];
            param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            param[1] = SqlHelper.GetParameter("@GroupUnitNo", GroupUnitNo);
            DataTable dt = SqlHelper.ExecuteSql(sbSql.ToString(), param);
            return dt;
        }
        #endregion

        #region 根据物品表的生产等单位查询单位名称
        public static DataSet GetListUnitName(int ProdID)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@ProductId",ProdID)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetUnitGroupByProductId]", param);
            return ds;
        }
        #endregion

        #region 判断某物品所有批次是否清零
        /// <summary>
        /// 判断某物品所有批次是否清零
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public static decimal GetProductCountByAllBatchNo(string prodNo, string companyCD)
        {
            string sql = "select isnull(sum(isnull(ProductCount,0)),0) as TotalProductCount from officedba.StorageProduct where productid=(select top 1 id from officedba.ProductInfo where CompanyCD=@CompanyCD and ProdNo=@ProdNo) and (BatchNo is not null or BatchNo<>'')";
            SqlParameter[] parms = new SqlParameter[2];
            parms[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            parms[1] = SqlHelper.GetParameter("@ProdNo", prodNo);
            object obj = SqlHelper.ExecuteScalar(sql, parms);
            if (obj == null)
            {
                return Convert.ToDecimal("0");
            }
            else
            {
                return Convert.ToDecimal(obj);
            }
        }
        #endregion

        #region 判断某物品是否确认过
        /// <summary>
        /// 判断某物品是否确认过
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static bool IsConfirmProduct(string productID)
        {
            SqlParameter[] parameters = { new SqlParameter("@ProductID", SqlDbType.VarChar, 50) };
            parameters[0].Value = productID;
            object obj = SqlHelper.ExecuteScalar("select count(*) from officedba.StorageProduct where ProductID=@ProductID", parameters);
            return Convert.ToInt32(obj) > 0 ? true : false;
        }
        #endregion

        #region 获取匹配的物品信息
        /// <summary>
        /// 根据输入的物品编号获取匹配的物品信息
        /// </summary>
        /// <param name="companyCD"></param>
        /// <param name="prodNo"></param>
        /// <returns></returns>
        public static DataTable GetMatchProductInfo(string companyCD,string prodNo)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            #region 查询语句
            //查询SQL拼写
            StringBuilder infoSql = new StringBuilder();
            infoSql.AppendLine("select a.ID,a.ProdNo,a.ProductName,a.StandardSell,a.UnitID,n.CodeName,");
            infoSql.AppendLine("	a.TaxRate,a.SellTax,a.Discount,a.Specification,");
            infoSql.AppendLine("	isnull(f.CodeName,'') as CodeTypeName,a.TaxBuy,");
            infoSql.AppendLine("	isnull(a.TypeID,'')as TypeID,a.StandardBuy,");
            infoSql.AppendLine("	a.InTaxRate,a.StandardCost,a.GroupUnitNo,");
            infoSql.AppendLine("	a.SaleUnitID,a.InUnitID,a.StockUnitID,");
            infoSql.AppendLine("	a.MakeUnitID,a.IsBatchNo,l.TypeName as ColorName");
            infoSql.AppendLine("from officedba.ProductInfo a");
            infoSql.AppendLine("left join officedba.CodeUnitType n on a.UnitID=n.ID");
            infoSql.AppendLine("left join officedba.CodeProductType as f on a.typeid=f.id");
            infoSql.AppendLine("left join officedba.CodePublicType l on a.ColorID=l.ID");
            infoSql.AppendLine("where a.CompanyCD=@CompanyCD and a.ProdNo=@ProdNo");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProdNo", prodNo));


            //指定命令的SQL文
            comm.CommandText = infoSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion
    }

}

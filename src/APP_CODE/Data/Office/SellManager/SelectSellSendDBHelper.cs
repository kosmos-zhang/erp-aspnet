using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using XBase.Data.DBHelper;
using System.Data;
using XBase.Model.Office.SellManager;
using XBase.Common;

namespace XBase.Data.Office.SellManager
{
    public class SelectSellSendDBHelper
    {
        /// <summary>
        /// 选择执行状态的单据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSellSendList(string busType, string OrderNo, string Title, string CustName, int? CurrencyType, string model, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strSql = string.Empty;
            string strCompanyCD = string.Empty;//单位编号
           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            strSql = "SELECT s.ID, s.Title, e.EmployeeName, s.Rate, cts.CurrencyName, c.CustName, s.SendNo, CONVERT(varchar(100), ISNULL(s.IntendSendDate, GETDATE()), 23)   AS IntendSendDate " +
                     "  FROM         officedba.SellSend AS s LEFT OUTER JOIN  " +
                     "  officedba.CurrencyTypeSetting AS cts ON s.CurrencyType = cts.ID LEFT OUTER JOIN   " +
                     " officedba.CustInfo AS c ON s.CustID = c.ID LEFT OUTER JOIN  " +
                     "  officedba.EmployeeInfo AS e ON s.Seller = e.ID " +
                     " WHERE  s.CompanyCD=@CompanyCD ";
            if (busType != null)
            {
                strSql += " and  s.BusiType='"+busType+"'";
            }
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            if (OrderNo != null)
            {
                strSql += " and s.SendNo like  '%" + OrderNo + "%'";
            }
            if (Title != null)
            {
                strSql += " and s.Title like '%" + Title + "%'";

            }
            if (CustName != null)
            {
                strSql += " and c.CustName like '%" + CustName + "%'";

            }
            if (CurrencyType != null)
            {
                strSql += " and s.CurrencyType = " + CurrencyType;

            }
            if (model != "all")
            {
                strSql += " and  s.BillStatus = '2' ";
            }
            else
            {
                strSql += " and  s.BillStatus <> '1'  ";
            }
            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
            
        }

        /// <summary>
        /// 获取发货单详细信息
        /// </summary>
        /// <param name="strOffNo"></param>
        public static DataTable GetSellSendInfo(string strOrderID)
        {
            string strSql = string.Empty;//查询报价单信息
            string strCompanyCD = string.Empty;//单位编号

            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            strSql = "select a.CustTel,a.MoneyType,a.SendNo,a.SendAddr, a.CustID, a.Seller, a.EmployeeName, "
                    + " a.PayType, a.Title,a.CarryType,a.isAddTax,a.DeptName, "
                    + " a.CurrencyType, a.Rate, a.SellType, a.BusiType, a.TakeType, "
                    + " a.CustType, b.TypeName, a.CustName ,a.SellDeptId,a.CurrencyName ,a.Discount"
                    + " FROM (SELECT c.Tel as CustTel,o.MoneyType,o.SendNo,o.CompanyCD, o.CustID, "
                    + " o.Seller, e.EmployeeName,o.PayType, o.Title, o.CarryType, o.isAddTax, "
                    + " o.CurrencyType, o.Rate, o.SellType, o.BusiType, o.TakeType, c.CustType, "
                    + " c.CustName ,o.SellDeptId, d.DeptName ,ct.CurrencyName,o.SendAddr,o.Discount "
                    + " FROM  officedba.SellSend AS o LEFT OUTER JOIN "
                    + " officedba.DeptInfo AS d ON o.SellDeptId = d.ID and o.CompanyCD = d.CompanyCD LEFT OUTER JOIN "
                    + " officedba.EmployeeInfo AS e ON o.Seller = e.ID and o.CompanyCD = e.CompanyCD LEFT OUTER JOIN "
                    + " officedba.CurrencyTypeSetting AS ct ON o.CurrencyType = ct.ID and o.CompanyCD = ct.CompanyCD LEFT OUTER JOIN "
                    + " officedba.CustInfo AS c ON o.CustID = c.ID and o.CompanyCD = c.CompanyCD "
                    + " where  (o.ID = " + strOrderID + ") and o.CompanyCD='" + strCompanyCD + "'  ) AS a LEFT OUTER JOIN "
                    + " officedba.CodePublicType AS b ON a.CustType = b.ID and a.CompanyCD = b.CompanyCD ";


            return SqlHelper.ExecuteSql(strSql);

        }

        /// <summary>
        /// 获取发货单明细信息
        /// </summary>
        /// <param name="strOffNo"></param>
        public static DataTable GetSellSendDetail(string strOrderID, string strOrderDetailID, string CustID,string busType)
        {
            string strSql = string.Empty;//查询报价单信息
            string strCompanyCD = string.Empty;//单位编号
       
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            strSql = "SELECT ssd.CompanyCD, ssd.SendNo, ssd.SortNo, ssd.ProductID, ssd.ProductCount,isnull(pc.TypeName,'') as ColorName, ";
            strSql += " ssd.UnitID, ssd.UnitPrice, ssd.TotalPrice, ssd.Discount, ssd.Package, ";
            strSql += " ssd.TaxPrice, ssd.TaxRate, ss.ID,isnull(ssd.OutCount,0) as OutCount,isnull(ssd.BackCount,0) as BackCount, ";
            strSql += " ssd.TotalFee, ssd.TotalTax, c.CodeName, p.ProductName, p.ProdNo, isnull(p.StandardCost,0) as StandardCost,isnull(ssd.SttlCount,0) as SttlCount, ";
            strSql += " p.Specification, CONVERT(varchar(100), ssd.SendDate, 23) AS SendDate, ssd.ID AS orderID ";
            strSql += " ,isnull(p.StandardSell,0) as StandardSell,c2.CodeName as UsedUnitName,";
            strSql += "ssd.UsedUnitID,isnull(ssd.UsedUnitCount,0) as UsedUnitCount,isnull(ssd.UsedPrice,0) as UsedPrice,isnull(ssd.ExRate,1) as ExRate  ";
            strSql += " FROM officedba.SellSendDetail AS ssd LEFT JOIN ";
            strSql += " officedba.SellSend AS ss ON ssd.SendNo = ss.SendNo AND ssd.CompanyCD = ss.CompanyCD LEFT JOIN ";
            strSql += " officedba.CodeUnitType AS c ON ssd.UnitID = c.ID AND ssd.CompanyCD = c.CompanyCD LEFT JOIN ";
            strSql += " officedba.CodeUnitType AS c2 ON ssd.UsedUnitID = c2.ID AND ssd.CompanyCD = c2.CompanyCD  LEFT JOIN ";
            strSql += " officedba.ProductInfo AS p ON ssd.ProductID = p.ID AND ssd.CompanyCD = p.CompanyCD ";
            strSql += " left join officedba.CodePublicType as pc on pc.ID=p.ColorID ";
            strSql += " where ssd.CompanyCD='" + strCompanyCD + "' ";
            if (strOrderID != null)
            {
                strSql += " and ss.ID in(" + strOrderID + ")";
            }
            if (strOrderDetailID != null)
            {
                strSql += " and ssd.ID in (" + strOrderDetailID + ")";
            }
            if (CustID != null)
            {
                strSql += " and ss.CustID =" + CustID;
            }
            if (busType != null)
            {
                strSql += " and  ss.BusiType='" + busType + "'";
            }
            return SqlHelper.ExecuteSql(strSql);

        }

        /// <summary>
        /// 获取发货单明细信息
        /// </summary>
        /// <param name="strOffNo"></param>
        public static DataTable GetSellSendDetail(string strOrderID, string strOrderDetailID, string CustID, string busType, string CurrencyType, string Rate, string OrderID,
            string OrderNo, string Title, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strSql = string.Empty;//查询报价单信息
            string strCompanyCD = string.Empty;//单位编号
            string selPointLen = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;//小数位数
            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;


                strSql = "SELECT ssd.CompanyCD, ssd.SendNo, ssd.SortNo, ssd.ProductID, "
                    + " convert(decimal(22,"+selPointLen+"),isnull(ssd.ProductCount,0)) as ProductCount, "
                    + " ssd.UnitID, convert(decimal(22,"+selPointLen+"),isnull(ssd.UnitPrice,0)) as UnitPrice,"
                    + " convert(decimal(22,"+selPointLen+"),isnull(ssd.TotalPrice,0)) as TotalPrice,ssd.Discount, ssd.Package,isnull(pc.TypeName,'') as ColorName, "
                    + " convert(decimal(22,"+selPointLen+"),isnull(ssd.TaxPrice,0)) as TaxPrice,"
                    + " convert(decimal(22,"+selPointLen+"),isnull(ssd.TaxRate,0)) as TaxRate,ss.ID,"
                    + " convert(decimal(22,"+selPointLen+"),isnull(ssd.OutCount,0)) as OutCount,"
                    + " convert(decimal(22,"+selPointLen+"),isnull(ssd.BackCount,0)) as BackCount, "
                    + " convert(decimal(22,"+selPointLen+"),isnull(ssd.TotalFee,0)) as TotalFee, "
                    + " convert(decimal(22,"+selPointLen+"),isnull(ssd.TotalTax,0)) as TotalTax,c.CodeName, p.ProductName, p.ProdNo, "
                    + " convert(decimal(22,"+selPointLen+"),isnull(ssd.SttlCount,0)) as SttlCount, "
                    + " convert(decimal(22," + selPointLen + "),isnull(ssd.BackCount,0)) as BackedCount, "
                    + " p.Specification, CONVERT(varchar(100), ssd.SendDate, 23) AS SendDate, ssd.ID AS orderID, "
                    + " ssd.UsedUnitID,"
                    + " convert(decimal(22,"+selPointLen+"),isnull(ssd.UsedUnitCount,0)) as UsedUnitCount,"
                    + " convert(decimal(22,"+selPointLen+"),isnull(ssd.UsedPrice,0)) as UsedPrice "
                    //+ " ,isnull(sd.ExRate,1) as ExRate,(sd.UsedUnitCount - ISNULL(sd.UseStockCount, 0.0000)) AS UsedPProductCount "
                    + " FROM officedba.SellSendDetail AS ssd LEFT JOIN "
                    + " officedba.SellSend AS ss ON ssd.SendNo = ss.SendNo AND ssd.CompanyCD = ss.CompanyCD LEFT JOIN "
                    + " officedba.CodeUnitType AS c ON ssd.UnitID = c.ID AND ssd.CompanyCD = c.CompanyCD LEFT JOIN "
                    + " officedba.ProductInfo AS p ON ssd.ProductID = p.ID AND ssd.CompanyCD = p.CompanyCD "
                    + " left join officedba.CodePublicType as pc on pc.ID=p.ColorID "
                    + " where  ss.BillStatus='2'  and ssd.CompanyCD=@CompanyCD ";

            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            if (strOrderID != null)
            {
                strSql += " and ss.ID in(" + strOrderID + ")";
            }
            if (strOrderDetailID != null)
            {
                strSql += " and ssd.ID in (" + strOrderDetailID + ")";
            }
            if (CustID != null)
            {
                strSql += " and ss.CustID =" + CustID;
            }
            if (busType != null)
            {
                strSql += " and  ss.BusiType='" + busType + "'";
            }
            if (OrderNo != null)
            {
                strSql += " and ss.SendNo like  '%" + OrderNo + "%'";
            }
            if (Title != null)
            {
                strSql += " and ss.Title like '%" + Title + "%'";

            }
            if (CurrencyType != null)
            {
                strSql += " and ss.CurrencyType=@CurrencyType ";
                arr.Add(new SqlParameter("@CurrencyType", CurrencyType));
            }
            if (Rate != null)
            {
                strSql += " and ss.Rate=" + Rate;

            }
            if (OrderID != null)
            {
                strSql += " and ss.ID<>@OrderID1 ";
                arr.Add(new SqlParameter("@OrderID1", OrderID));
            }
            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);

        }
    }
}

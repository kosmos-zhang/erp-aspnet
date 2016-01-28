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
    public class SelectSellOrderDetailDBHelper
    {

        /// <summary>
        /// 选择执行状态的单据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSellOrderList(string OrderNo, string Title, string CustName, int? CurrencyType, string model, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strSql = string.Empty;
            string strCompanyCD = string.Empty;//单位编号
            bool isMoreUnit = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit;//多计量单位
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

                strSql = "SELECT s.ID, s.Title, e.EmployeeName, s.Rate, cts.CurrencyName, c.CustName, CONVERT(varchar(100), s.OrderDate, 23) AS OrderDate, s.OrderNo ";
                strSql += " ,case when (SELECT isnull( sum(SendCount),0) as SendCount    ";
                strSql += " FROM officedba.SellOrderDetail          ";
                strSql += " WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD) =0 then '未发货'     ";
                strSql += " when (SELECT isnull( sum(SendCount),0) as SendCount     ";
                strSql += " FROM officedba.SellOrderDetail                 ";
                strSql += " WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD) >0  and  ";
                strSql += " (SELECT isnull( sum(SendCount),0) as SendCount   ";
                strSql += " FROM officedba.SellOrderDetail               ";
                strSql += " WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD)<  ";
                if (isMoreUnit)
                {
                    strSql += " (SELECT isnull( sum(UsedUnitCount),0) as SendCount   ";
                }
                else
                {
                    strSql += " (SELECT isnull( sum(ProductCount),0) as SendCount   ";
                }
                strSql += " FROM officedba.SellOrderDetail      ";
                strSql += " WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD)   ";
                strSql += " then '部分发货'       ";
                strSql += " when (SELECT isnull( sum(SendCount),0) as SendCount ";
                strSql += " FROM officedba.SellOrderDetail      ";
                strSql += " WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD) <>0  and   ";
                strSql += " (SELECT isnull( sum(SendCount),0) as SendCount   ";
                strSql += " FROM officedba.SellOrderDetail    ";
                strSql += " WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD)>=   ";
                if (isMoreUnit)
                {
                    strSql += " (SELECT isnull( sum(UsedUnitCount),0) as SendCount   ";
                }
                else
                {
                    strSql += " (SELECT isnull( sum(ProductCount),0) as SendCount   ";
                }
                strSql += " FROM officedba.SellOrderDetail    ";
                strSql += " WHERE OrderNo = s.OrderNo and CompanyCD=s.CompanyCD)";
                strSql += " then '已发货'  " ;
                strSql += " end as isSendText ";
                strSql += " FROM  officedba.SellOrder AS s LEFT OUTER JOIN ";
                strSql += " officedba.CurrencyTypeSetting AS cts ON s.CurrencyType = cts.ID LEFT OUTER JOIN  ";
                strSql += " officedba.CustInfo AS c ON s.CustID = c.ID LEFT OUTER JOIN  ";
                strSql += " officedba.EmployeeInfo AS e ON s.Seller = e.ID ";
                strSql += " WHERE  s.CompanyCD=@CompanyCD";

            //过滤单据：显示当前用户拥有权限查看的单据
            int empid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
            strSql += " and ( charindex('," + empid + ",' , ','+s.CanViewUser+',')>0 or s.Creator=" + empid + " OR s.CanViewUser='' OR s.CanViewUser is null) ";

            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            if (OrderNo != null)
            {
                strSql += " and s.OrderNo like  '%" + OrderNo + "%'";
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
                strSql += " and  s.BillStatus <> '1' and s.Status<>'3' ";
            }
            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
        }


        /// <summary>
        /// 获取订单详细信息
        /// </summary>
        /// <param name="strOffNo"></param>
        public static DataTable GetSellOrderInfo(string strOrderID)
        {
            string strSql = string.Empty;//查询报价单信息
            string strCompanyCD = string.Empty;//单位编号

            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            strSql = "SELECT a.CustTel,a.MoneyType,a.OrderNo, a.CustID, a.Seller, a.EmployeeName,a.OrderMethod, a.PayType, a.Title,a.CarryType,a.isAddTax,a.DeptName,"
                        + " a.CurrencyType, a.Rate, a.SellType, a.BusiType, a.TakeType,  a.CustType, b.TypeName, a.CustName ,a.SellDeptId,a.CurrencyName,a.Discount "
                        + " FROM (SELECT c.Tel as CustTel,o.MoneyType, o.CompanyCD,o.OrderNo, o.CustID, o.Seller, e.EmployeeName,o.PayType, o.Title, o.CarryType, o.isAddTax,  "
                        + " o.CurrencyType, o.Rate, o.SellType, o.BusiType, o.TakeType, c.CustType, c.CustName,o.OrderMethod ,o.SellDeptId, d.DeptName ,ct.CurrencyName,o.Discount "
                        + " FROM  officedba.SellOrder AS o LEFT OUTER JOIN officedba.DeptInfo AS d ON o.SellDeptId = d.ID and o.CompanyCD=d.CompanyCD LEFT OUTER JOIN   "
                        + " officedba.EmployeeInfo AS e ON o.Seller = e.ID and o.CompanyCD=e.CompanyCD LEFT OUTER JOIN   officedba.CurrencyTypeSetting AS ct ON o.CurrencyType = ct.ID and o.CompanyCD=ct.CompanyCD LEFT OUTER JOIN  "
                        + " officedba.CustInfo AS c ON o.CustID = c.ID and o.CompanyCD=c.CompanyCD where  (o.ID = " + strOrderID + ") and o.CompanyCD='" + strCompanyCD + "' ) AS a LEFT OUTER JOIN "
                        + " officedba.CodePublicType AS b ON a.CustType = b.ID and  a.CompanyCD=b.CompanyCD ";

            return SqlHelper.ExecuteSql(strSql);

        }


        /// <summary>
        /// 获取订单明细列表
        /// </summary>
        /// <param name="iCustID">客户编号</param>
        /// <param name="strDetailID2">订单明细ID列表</param>
        /// <param name="strOrderID">订单编号</param>
        /// <param name="CurrencyType">币种</param>
        /// <param name="OrderID">已选择的订单的id</param>
        /// <param name="OrderNo">查询条件订单编号</param>
        /// <param name="Title">查询条件订单主题</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetSellOrderDetailByCustID(int? iCustID, string strDetailID2, string strOrderID, string CurrencyType,string Rate, string OrderID,
            string OrderNo, string Title, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strSql = string.Empty;
            string strCompanyCD = string.Empty;//单位编号
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string selPointLen = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;//小数位数

            strSql += "SELECT s.ID as OrderID,isnull(s.CurrencyType,0) as CurrencyType,isnull(s.Rate,0.0000) as Rate, sd.SortNo,          ";
            strSql += "convert(decimal(22," + selPointLen + "),isnull( sd.PlanProductCount,0)) as PlanProductCount,";
            strSql += "isnull(pc.TypeName,'') as ColorName,   ";
            strSql += "convert(decimal(22," + selPointLen + "),isnull(sd.UseStockCount,0)) as UseStockCount,       ";
            strSql += "convert(decimal(22," + selPointLen + "),ISNULL(sd.ProductCount,0)) AS ProductCount,isnull( p.Specification,'''') as Specification,  ";
            strSql += "isnull(u.CodeName,'''') as CodeName,isnull( p.ProductName,'''') as ProductName,isnull( p.ProdNo,'''') as ProdNo,   ";
            strSql += "convert(decimal(22," + selPointLen + "),(sd.ProductCount - ISNULL(sd.UseStockCount, 0))) AS PProductCount, sd.ProductID,isnull(p.StandardCost,0) as StandardCost,    ";
            //单位，数量，单价，换算率，计划生产数量（UsedPProductCount）
            strSql += " sd.UsedUnitID,convert(decimal(22," + selPointLen + "),isnull(sd.UsedUnitCount,0)) as UsedUnitCount,";
            strSql += " convert(decimal(22," + selPointLen + "),isnull(sd.UsedPrice,0)) as UsedPrice,isnull(sd.ExRate,1) as ExRate,";
            strSql += "convert(decimal(22," + selPointLen + "),(sd.UsedUnitCount - ISNULL(sd.UseStockCount,0))) AS UsedPProductCount,";
            strSql += "sd.ID as DetailID,s.OrderNo,convert(decimal(22," + selPointLen + "),isnull(sd.TaxPrice,0)) as TaxPrice,";
            strSql += " convert(decimal(22," + selPointLen + "),isnull(sd.TaxRate,0)) as TaxRate,  ";
            strSql += "convert(decimal(22," + selPointLen + "),isnull(sd.TotalFee,0)) as TotalFee,";
            strSql += "convert(decimal(22," + selPointLen + "),isnull(sd.TotalTax,0)) as TotalTax, ";
            strSql += "sd.UnitID,convert(decimal(22," + selPointLen + "),isnull( sd.UnitPrice,0)) as UnitPrice,";
            strSql += "convert(decimal(22," + selPointLen + "),isnull(sd.TotalPrice,0)) as TotalPrice,   ";
            strSql += " isnull(sd.Discount,100) as Discount, sd.Package,isnull(sd.Remark,'''') as Remark,    ";
            strSql += "convert(decimal(22," + selPointLen + "),(sd.ProductCount-isnull(sd.SendCount,0))) as pCount,    ";
            strSql += "convert(decimal(22," + selPointLen + "),isnull(sd.SendCount,0)) as transactCount    ";
            strSql += "FROM                            ";
            strSql += "officedba.SellOrder AS s left JOIN                                                                                ";
            strSql += "officedba.SellOrderDetail AS sd ON s.OrderNo = sd.OrderNo                                                          ";
            strSql += "and s.Status='1' and s.BillStatus='2'                                                                          ";
            strSql += "and s.CompanyCD= sd.CompanyCD                                                                                      ";
            strSql += "left JOIN                                                                                                         ";
            strSql += "officedba.CodeUnitType AS u                                                                                        ";
            strSql += "ON sd.UnitID = u.ID and s.CompanyCD= u.CompanyCD                                                                   ";
            strSql += "left JOIN                                                                                                         ";
            strSql += "officedba.ProductInfo AS p                                                                                         ";
            strSql += "ON sd.ProductID = p.ID and s.CompanyCD= p.CompanyCD                                                                ";
            strSql += "left join officedba.CodePublicType as pc on pc.ID=p.ColorID   ";
            strSql += "where (sd.ProductCount - ISNULL(sd.SendCount, 0.0000))>0  and  s.CompanyCD=@CompanyCD                                                          ";
            //过滤单据：显示当前用户拥有权限查看的单据
            int empid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
            strSql += " and ( charindex('," + empid + ",' , ','+s.CanViewUser+',')>0 or s.Creator=" + empid + " OR s.CanViewUser='' OR s.CanViewUser is null) ";

            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            if (OrderNo != null)
            {
                strSql += " and s.OrderNo like  '%" + OrderNo + "%'";
            }
            if (Title != null)
            {
                strSql += " and s.Title like '%" + Title + "%'";

            }
            if (iCustID != null)
            {
                strSql += " and (s.CustID =@CustID )";
                arr.Add(new SqlParameter("@CustID", iCustID));
            }
            if (strDetailID2 != null)
            {
                strSql += "  and sd.ID in(" + strDetailID2 + ") ";

            }
            if (strOrderID != null)
            {
                strSql += " and s.ID=@OrderID ";
                arr.Add(new SqlParameter("@OrderID", strOrderID));
            }
            if (CurrencyType != null)
            {
                strSql += " and s.CurrencyType=@CurrencyType ";
                arr.Add(new SqlParameter("@CurrencyType", CurrencyType));
            }
            if (Rate != null)
            {
                strSql += " and s.Rate="+Rate;
                
            }
            if (OrderID != null)
            {
                strSql += " and s.ID<>@OrderID1 ";
                arr.Add(new SqlParameter("@OrderID1", OrderID));
            }
            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
        }
        /// <summary>
        /// 获取订单明细列表
        /// </summary>
        /// <param name="iCustID">客户编号</param>
        /// <param name="strDetailID2">订单明细ID列表</param>
        /// <param name="strOrderID">订单编号</param>
        /// <returns></returns>
        public static DataTable GetSellOrderDetailByCustID(int? iCustID, string strDetailID2, string strOrderID)
        {
            string strSql = string.Empty;

            string strCompanyCD = string.Empty;//单位编号
            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            SqlParameter[] paras = new SqlParameter[5];
            if (iCustID == null)
            {
                paras[0] = new SqlParameter("@CustID", DBNull.Value);

            }
            else
            {
                paras[0] = new SqlParameter("@CustID", iCustID);

            }
            if (strDetailID2 == null)
            {
                paras[1] = new SqlParameter("@DetailID", DBNull.Value);

            }
            else
            {
                paras[1] = new SqlParameter("@DetailID", strDetailID2);

            }
            if (strOrderID == null)
            {
                paras[2] = new SqlParameter("@OrderID", DBNull.Value);

            }
            else
            {
                paras[2] = new SqlParameter("@OrderID", strOrderID);

            }

            paras[3] = new SqlParameter("@CompanyCD", strCompanyCD);
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            {
                paras[4] = new SqlParameter("@IsMoreUnit", 1);
            }
            else
            {
                paras[4] = new SqlParameter("@IsMoreUnit", DBNull.Value);
            }

            return SqlHelper.ExecuteStoredProcedure("officedba.SelectSellSendDetail_PR", paras);
        }
    }
}


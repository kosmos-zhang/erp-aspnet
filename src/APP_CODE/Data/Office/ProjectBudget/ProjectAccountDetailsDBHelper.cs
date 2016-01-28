/********************************
 * 描述：核算明细
 * 创建人：hexw
 * 创建时间：2010-5-18
 * *****************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using XBase.Data.DBHelper;

namespace XBase.Data.Office.ProjectBudget
{
    public class ProjectAccountDetailsDBHelper
    {
        #region  获取“设备与原材料采购”明细列表
        /// <summary>
        /// 获取“设备与原材料采购”明细列表
        /// 2010-06-4修改：增加“采购数据来源”检索条件，及是否附件无来源采购到货单
        /// </summary>
        /// <param name="isMoreUnit">多计量单位</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <param name="ProjectID">所属项目</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetPurchaseDetailInfoList(string purchaseType,bool isMoreUnit,string strCompanyCD, string ProjectID, string BeginDate, string EndDate)
        {
            StringBuilder strSql = new StringBuilder();
            #region 采购订单
            if (purchaseType == "1" || purchaseType=="3")
            { 
                strSql.AppendLine("select a.ProductID,b.ProductName,b.Specification,");
                if (isMoreUnit)
                {
                    strSql.AppendLine("a.UsedUnitID,isnull(a.UsedUnitCount,0) as ProductCount,");
                }
                else
                {
                    strSql.AppendLine("a.UnitID,isnull(a.ProductCount,0) as ProductCount,");
                }
                strSql.AppendLine("a.TaxPrice,isnull(a.Discount,100) as Discount,");
                strSql.AppendLine("convert(varchar(10),c.OrderDate,23) as DealDate,c.Purchaser as DealEmployee,isnull(c.Rate,1) as Rate,");
                strSql.AppendLine("isnull(e.EmployeeName,'') as DealEmployeeName,isnull(d.CodeName,'') as UnitName,1 as Sign ");
                strSql.AppendLine("from officedba.PurchaseOrderDetail a ");
                strSql.AppendLine("left join officedba.ProductInfo b on a.ProductID=b.ID");
                strSql.AppendLine("left join officedba.PurchaseOrder c on a.OrderNo=c.OrderNo and a.CompanyCD=c.CompanyCD");
                if (isMoreUnit)
                {
                    strSql.AppendLine("left join officedba.CodeUnitType d on d.ID=a.UsedUnitID");
                }
                else
                { 
                    strSql.AppendLine("left join officedba.CodeUnitType d on d.ID=a.UnitID");
                }
                strSql.AppendLine("left join officedba.EmployeeInfo e on e.ID=c.Purchaser");
                strSql.AppendLine("where a.CompanyCD=@CompanyCD and c.BillStatus='2' ");
                if (!string.IsNullOrEmpty(ProjectID))
                {
                    strSql.AppendLine(" and c.ProjectID=@ProjectID ");
                }
                if (!string.IsNullOrEmpty(BeginDate))
                {
                    strSql.AppendLine(" and c.OrderDate>=@BeginDate ");
                }
                if (!string.IsNullOrEmpty(EndDate))
                {
                    strSql.AppendLine(" and c.OrderDate<dateadd(dd,1,@EndDate) ");
                }
            }
            
            #endregion

            #region 所有采购到货单
            if (purchaseType == "2")
            { 
                strSql.AppendLine("select a.ProductID,b.ProductName,b.Specification,");
                if (isMoreUnit)
                {
                    strSql.AppendLine("a.UsedUnitID,isnull(a.UsedUnitCount,0) as ProductCount,");
                }
                else
                {
                    strSql.AppendLine("a.UnitID,isnull(a.ProductCount,0) as ProductCount,");
                }
                strSql.AppendLine("a.TaxPrice,isnull(a.Discount,100) as Discount,");
                strSql.AppendLine("convert(varchar(10),c.ArriveDate,23) as DealDate,c.Purchaser as DealEmployee,isnull(c.Rate,1) as Rate,");
                strSql.AppendLine("isnull(e.EmployeeName,'') as DealEmployeeName,isnull(d.CodeName,'') as UnitName,1 as Sign ");
                strSql.AppendLine("from officedba.PurchaseArriveDetail a ");
                strSql.AppendLine("left join officedba.ProductInfo b on a.ProductID=b.ID");
                strSql.AppendLine("left join officedba.PurchaseArrive c on a.ArriveNo=c.ArriveNo and a.CompanyCD=c.CompanyCD");
                if (isMoreUnit)
                {
                    strSql.AppendLine("left join officedba.CodeUnitType d on d.ID=a.UsedUnitID");
                }
                else
                {
                    strSql.AppendLine("left join officedba.CodeUnitType d on d.ID=a.UnitID");
                }
                strSql.AppendLine("left join officedba.EmployeeInfo e on e.ID=c.Purchaser");
                strSql.AppendLine("where a.CompanyCD=@CompanyCD and c.BillStatus='2' ");
                if (!string.IsNullOrEmpty(ProjectID))
                {
                    strSql.AppendLine(" and c.ProjectID=@ProjectID ");
                }
                if (!string.IsNullOrEmpty(BeginDate))
                {
                    strSql.AppendLine(" and c.ArriveDate>=@BeginDate ");
                }
                if (!string.IsNullOrEmpty(EndDate))
                {
                    strSql.AppendLine(" and c.ArriveDate<dateadd(dd,1,@EndDate) ");
                }
            }
            #endregion

            #region 无来源采购到货单
            if (purchaseType == "3")
            {
                strSql.AppendLine(" union all ");
                strSql.AppendLine("select a.ProductID,b.ProductName,b.Specification,");
                if (isMoreUnit)
                {
                    strSql.AppendLine("a.UsedUnitID,isnull(a.UsedUnitCount,0) as ProductCount,");
                }
                else
                {
                    strSql.AppendLine("a.UnitID,isnull(a.ProductCount,0) as ProductCount,");
                }
                strSql.AppendLine("a.TaxPrice,isnull(a.Discount,100) as Discount,");
                strSql.AppendLine("convert(varchar(10),c.ArriveDate,23) as DealDate,c.Purchaser as DealEmployee,isnull(c.Rate,1) as Rate,");
                strSql.AppendLine("isnull(e.EmployeeName,'') as DealEmployeeName,isnull(d.CodeName,'') as UnitName,1 as Sign ");
                strSql.AppendLine("from officedba.PurchaseArriveDetail a ");
                strSql.AppendLine("left join officedba.ProductInfo b on a.ProductID=b.ID");
                strSql.AppendLine("left join officedba.PurchaseArrive c on a.ArriveNo=c.ArriveNo and a.CompanyCD=c.CompanyCD");
                if (isMoreUnit)
                {
                    strSql.AppendLine("left join officedba.CodeUnitType d on d.ID=a.UsedUnitID");
                }
                else
                {
                    strSql.AppendLine("left join officedba.CodeUnitType d on d.ID=a.UnitID");
                }
                strSql.AppendLine("left join officedba.EmployeeInfo e on e.ID=c.Purchaser");
                strSql.AppendLine("where a.CompanyCD=@CompanyCD and c.BillStatus='2' and c.FromType='0' ");
                if (!string.IsNullOrEmpty(ProjectID))
                {
                    strSql.AppendLine(" and c.ProjectID=@ProjectID ");
                }
                if (!string.IsNullOrEmpty(BeginDate))
                {
                    strSql.AppendLine(" and c.ArriveDate>=@BeginDate ");
                }
                if (!string.IsNullOrEmpty(EndDate))
                {
                    strSql.AppendLine(" and c.ArriveDate<dateadd(dd,1,@EndDate) ");
                }
            }
            #endregion

            strSql.AppendLine(" union all ");
            #region 采购退货
            strSql.AppendLine("select a.ProductID,b.ProductName,b.Specification,");
            if (isMoreUnit)
            {
                strSql.AppendLine("a.UsedUnitID,isnull(a.UsedUnitCount,0) as ProductCount,");
            }
            else
            {
                strSql.AppendLine("a.UnitID,isnull(a.ProductCount,0) as ProductCount,");
            }
            strSql.AppendLine(" a.TaxPrice,isnull(a.Discount,100) as Discount,");
            strSql.AppendLine("convert(varchar(10),c.RejectDate,23) as DealDate,c.Purchaser as DealEmployee,isnull(c.Rate,1) as Rate,");
            strSql.AppendLine("isnull(e.EmployeeName,'') as DealEmployeeName,isnull(d.CodeName,'') as UnitName,-1 as Sign");
            strSql.AppendLine(" from officedba.PurchaseRejectDetail a ");
            strSql.AppendLine("left join officedba.ProductInfo b on a.ProductID=b.ID");
            strSql.AppendLine("left join officedba.PurchaseReject c on a.RejectNo=c.RejectNo and a.CompanyCD=c.CompanyCD");
            if (isMoreUnit)
            {
                strSql.AppendLine("left join officedba.CodeUnitType d on d.ID=a.UsedUnitID");
            }
            else
            {
                strSql.AppendLine("left join officedba.CodeUnitType d on d.ID=a.UnitID");
            }
            strSql.AppendLine("left join officedba.EmployeeInfo e on e.ID=c.Purchaser");
            strSql.AppendLine("where a.CompanyCD=@CompanyCD and c.BillStatus='2' ");
            if (!string.IsNullOrEmpty(ProjectID))
            {
                strSql.AppendLine(" and c.ProjectID=@ProjectID ");
            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                strSql.AppendLine(" and c.RejectDate>=@BeginDate ");
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                strSql.AppendLine(" and c.RejectDate<dateadd(dd,1,@EndDate) ");
            }
            #endregion
            SqlParameter[] param = { 
                                    new SqlParameter("@CompanyCD",strCompanyCD),
                                    new SqlParameter("@ProjectID",ProjectID),
                                    new SqlParameter("@BeginDate",BeginDate),
                                    new SqlParameter("@EndDate",EndDate)
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        #region  获取“原材料消耗”明细列表
        /// <summary>
        /// 获取“原材料消耗”明细列表
        /// </summary>
        /// <param name="strCompanyCD">公司编码</param>
        /// <param name="ProjectID">所属项目</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetTakeMaterialDetailInfoList(bool isMoreUnit, string strCompanyCD, string ProjectID, string BeginDate, string EndDate)
        {
            StringBuilder strSql = new StringBuilder();
            #region 领料单
            strSql.AppendLine("select a.ProductID,b.ProductName,b.Specification,isnull(a.TotalPrice,0) as TotalPrice,");
            if (isMoreUnit)
            {
                strSql.AppendLine("a.UsedUnitID,isnull(a.UsedUnitCount,0) as ProductCount,isnull(a.UsedPrice,0) as TaxPrice,");
            }
            else
            {
                strSql.AppendLine("a.UnitID,isnull(a.TakeCount,0) as ProductCount,isnull(a.Price,0) as TaxPrice,");
            }
            strSql.AppendLine("convert(varchar(10),c.TakeDate,23) as DealDate,c.SaleID as DealEmployee,");
            strSql.AppendLine("isnull(e.EmployeeName,'') as DealEmployeeName,isnull(d.CodeName,'') as UnitName ");
            strSql.AppendLine("from officedba.TakeMaterialDetail a ");
            strSql.AppendLine("left join officedba.ProductInfo b on a.ProductID=b.ID");
            strSql.AppendLine("left join officedba.TakeMaterial c on a.TakeNo=c.TakeNo and a.CompanyCD=c.CompanyCD");
            if (isMoreUnit)
            {
                strSql.AppendLine("left join officedba.CodeUnitType d on d.ID=a.UsedUnitID");
            }
            else
            {
                strSql.AppendLine("left join officedba.CodeUnitType d on d.ID=a.UnitID");
            }
            strSql.AppendLine("left join officedba.EmployeeInfo e on e.ID=c.SaleID");
            strSql.AppendLine("where a.CompanyCD=@CompanyCD and (c.Takedate is not null) and (c.TakeDate is not null) ");
            if (!string.IsNullOrEmpty(ProjectID))
            {
                strSql.AppendLine(" and c.ProjectID=@ProjectID ");
            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                strSql.AppendLine(" and c.TakeDate>=@BeginDate ");
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                strSql.AppendLine(" and c.TakeDate<dateadd(dd,1,@EndDate) ");
            }
            #endregion
            strSql.AppendLine(" union all ");
            #region 退料单
            strSql.AppendLine("select a.ProductID,b.ProductName,b.Specification,isnull(-a.TotalPrice,0) as TotalPrice,");
            if (isMoreUnit)
            {
                strSql.AppendLine("a.UsedUnitID,isnull(a.UsedUnitCount,0) as ProductCount,isnull(a.UsedPrice,0) as TaxPrice,");
            }
            else
            {
                strSql.AppendLine("a.UnitID,isnull(a.BackCount,0) as ProductCount,isnull(a.Price,0) as TaxPrice,");
            }
            strSql.AppendLine("convert(varchar(10),c.ReceiveDate,23) as DealDate,c.SaleID as DealEmployee,");
            strSql.AppendLine("isnull(e.EmployeeName,'') as DealEmployeeName,isnull(d.CodeName,'') as UnitName");
            strSql.AppendLine(" from officedba.BackMaterialDetail a ");
            strSql.AppendLine("left join officedba.ProductInfo b on a.ProductID=b.ID");
            strSql.AppendLine("left join officedba.BackMaterial c on a.BackNo=c.BackNo and a.CompanyCD=c.CompanyCD");
            if (isMoreUnit)
            {
                strSql.AppendLine("left join officedba.CodeUnitType d on d.ID=a.UsedUnitID");
            }
            else
            {
                strSql.AppendLine("left join officedba.CodeUnitType d on d.ID=a.UnitID");
            }
            strSql.AppendLine("left join officedba.EmployeeInfo e on e.ID=c.SaleID");
            strSql.AppendLine(" left join officedba.TakeMaterial f on f.TakeNo=a.FromBillNo and a.CompanyCD=f.CompanyCD ");
            strSql.AppendLine("where a.CompanyCD=@CompanyCD and a.FromBillNo!='' and c.FromType='1' and (c.Receiver is not null) and (c.ReceiveDate is not null) ");
            if (!string.IsNullOrEmpty(ProjectID))
            {
                strSql.AppendLine(" and f.ProjectID=@ProjectID ");
            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                strSql.AppendLine(" and c.ReceiveDate>=@BeginDate ");
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                strSql.AppendLine(" and c.ReceiveDate<dateadd(dd,1,@EndDate) ");
            }
            #endregion
            SqlParameter[] param = { 
                                    new SqlParameter("@CompanyCD",strCompanyCD),
                                    new SqlParameter("@ProjectID",ProjectID),
                                    new SqlParameter("@BeginDate",BeginDate),
                                    new SqlParameter("@EndDate",EndDate)
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        #region  获取“其它入库”明细列表
        /// <summary>
        /// 获取“其它入库”明细列表
        /// </summary>
        /// <param name="strCompanyCD">公司编码</param>
        /// <param name="ProjectID">所属项目</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetOtherInDetailInfoList(bool isMoreUnit, string strCompanyCD, string ProjectID, string BeginDate, string EndDate)
        {
            StringBuilder strSql = new StringBuilder();
            #region 其它入库单
            strSql.AppendLine("select a.ProductID,b.ProductName,b.Specification,");
            if (isMoreUnit)
            {
                strSql.AppendLine("a.UsedUnitID,isnull(a.UsedUnitCount,0) as ProductCount,isnull(a.UsedPrice,0) as TaxPrice,");
            }
            else
            {
                strSql.AppendLine("a.UnitID,isnull(a.ProductCount,0) as ProductCount,isnull(a.UnitPrice,0) as TaxPrice,");
            }
            strSql.AppendLine("isnull(a.TotalPrice,0) as TotalPrice,");
            strSql.AppendLine("convert(varchar(10),c.EnterDate,23) as DealDate,c.Executor as DealEmployee,");
            strSql.AppendLine("isnull(e.EmployeeName,'') as DealEmployeeName,isnull(d.CodeName,'') as UnitName ");
            strSql.AppendLine("from officedba.StorageInOtherDetail a ");
            strSql.AppendLine("left join officedba.ProductInfo b on a.ProductID=b.ID");
            strSql.AppendLine("left join officedba.StorageInOther c on a.InNo=c.InNo and a.CompanyCD=c.CompanyCD");
            if (isMoreUnit)
            {
                strSql.AppendLine("left join officedba.CodeUnitType d on d.ID=a.UsedUnitID");
            }
            else
            {
                strSql.AppendLine("left join officedba.CodeUnitType d on d.ID=a.UnitID");
            }
            strSql.AppendLine("left join officedba.EmployeeInfo e on e.ID=c.Executor");
            strSql.AppendLine("where a.CompanyCD=@CompanyCD and c.BillStatus='2' ");
            if (!string.IsNullOrEmpty(ProjectID))
            {
                strSql.AppendLine(" and c.ProjectID=@ProjectID ");
            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                strSql.AppendLine(" and c.EnterDate>=@BeginDate ");
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                strSql.AppendLine(" and c.EnterDate<dateadd(dd,1,@EndDate) ");
            }
            #endregion
            SqlParameter[] param = { 
                                    new SqlParameter("@CompanyCD",strCompanyCD),
                                    new SqlParameter("@ProjectID",ProjectID),
                                    new SqlParameter("@BeginDate",BeginDate),
                                    new SqlParameter("@EndDate",EndDate)
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        #region  获取“其它出库”明细列表
        /// <summary>
        /// 获取“其它出库”明细列表
        /// </summary>
        /// <param name="strCompanyCD">公司编码</param>
        /// <param name="ProjectID">所属项目</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetOtherOutDetailInfoList(bool isMoreUnit, string strCompanyCD, string ProjectID, string BeginDate, string EndDate)
        {
            StringBuilder strSql = new StringBuilder();
            #region 其它出库单
            strSql.AppendLine("select a.ProductID,b.ProductName,b.Specification,");
            if (isMoreUnit)
            {
                strSql.AppendLine("a.UsedUnitID,isnull(a.UsedUnitCount,0) as ProductCount,isnull(a.UsedPrice,0) as TaxPrice,");
            }
            else
            {
                strSql.AppendLine("a.UnitID,isnull(a.ProductCount,0) as ProductCount,a.UnitPrice as TaxPrice,");
            }
            strSql.AppendLine("isnull(a.TotalPrice,0) as TotalPrice,");
            strSql.AppendLine("convert(varchar(10),c.OutDate,23) as DealDate,c.Transactor as DealEmployee,");
            strSql.AppendLine("isnull(e.EmployeeName,'') as DealEmployeeName,isnull(d.CodeName,'') as UnitName ");
            strSql.AppendLine("from officedba.StorageOutOtherDetail a ");
            strSql.AppendLine("left join officedba.ProductInfo b on a.ProductID=b.ID");
            strSql.AppendLine("left join officedba.StorageOutOther c on a.OutNo=c.OutNo and a.CompanyCD=c.CompanyCD");
            if (isMoreUnit)
            {
                strSql.AppendLine("left join officedba.CodeUnitType d on d.ID=a.UsedUnitID");
            }
            else
            {
                strSql.AppendLine("left join officedba.CodeUnitType d on d.ID=a.UnitID");
            }
            strSql.AppendLine("left join officedba.EmployeeInfo e on e.ID=c.Transactor");
            strSql.AppendLine("where a.CompanyCD=@CompanyCD and c.BillStatus='2' ");
            if (!string.IsNullOrEmpty(ProjectID))
            {
                strSql.AppendLine(" and c.ProjectID=@ProjectID ");
            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                strSql.AppendLine(" and c.OutDate>=@BeginDate ");
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                strSql.AppendLine(" and c.OutDate<dateadd(dd,1,@EndDate) ");
            }
            #endregion
            SqlParameter[] param = { 
                                    new SqlParameter("@CompanyCD",strCompanyCD),
                                    new SqlParameter("@ProjectID",ProjectID),
                                    new SqlParameter("@BeginDate",BeginDate),
                                    new SqlParameter("@EndDate",EndDate)
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        #region  获取“销售收入”明细列表
        /// <summary>
        /// 获取“销售收入”明细列表
        /// </summary>
        /// <param name="strCompanyCD">公司编码</param>
        /// <param name="ProjectID">所属项目</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetSellDetailInfoList(bool isMoreUnit, string strCompanyCD, string ProjectID, string BeginDate, string EndDate)
        {
            StringBuilder strSql = new StringBuilder();
            #region 销售订单
            strSql.AppendLine("select a.ProductID,b.ProductName,b.Specification,");
            if (isMoreUnit)
            {
                strSql.AppendLine("a.UsedUnitID,isnull(a.UsedUnitCount,0) as ProductCount,");
            }
            else
            {
                strSql.AppendLine("a.UnitID,isnull(a.ProductCount,0) as ProductCount,");
            }
            strSql.AppendLine("a.TaxPrice,isnull(a.TotalFee,0) as TotalPrice,a.Discount,");
            strSql.AppendLine("convert(varchar(10),c.OrderDate,23) as DealDate,c.Seller as DealEmployee,isnull(c.Rate,1) as Rate,");
            strSql.AppendLine("isnull(e.EmployeeName,'') as DealEmployeeName,isnull(d.CodeName,'') as UnitName ");
            strSql.AppendLine("from officedba.SellOrderDetail a ");
            strSql.AppendLine("left join officedba.ProductInfo b on a.ProductID=b.ID");
            strSql.AppendLine("left join officedba.SellOrder c on a.OrderNo=c.OrderNo and a.CompanyCD=c.CompanyCD");
            if (isMoreUnit)
            {
                strSql.AppendLine("left join officedba.CodeUnitType d on d.ID=a.UsedUnitID");
            }
            else
            {
                strSql.AppendLine("left join officedba.CodeUnitType d on d.ID=a.UnitID");
            }
            strSql.AppendLine("left join officedba.EmployeeInfo e on e.ID=c.Seller");
            strSql.AppendLine("where a.CompanyCD=@CompanyCD and c.BillStatus='2' ");
            if (!string.IsNullOrEmpty(ProjectID))
            {
                strSql.AppendLine(" and c.ProjectID=@ProjectID ");
            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                strSql.AppendLine(" and c.OrderDate>=@BeginDate ");
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                strSql.AppendLine(" and c.OrderDate<dateadd(dd,1,@EndDate) ");
            }
            #endregion
            strSql.AppendLine(" union all ");
            #region 销售退货
            strSql.AppendLine("select a.ProductID,b.ProductName,b.Specification,");
            if (isMoreUnit)
            {
                strSql.AppendLine("a.UsedUnitID,isnull(a.UsedUnitCount,0) as ProductCount,");
            }
            else
            {
                strSql.AppendLine("a.UnitID,isnull(a.ProductCount,0) as ProductCount,");
            }
            strSql.AppendLine("a.TaxPrice,isnull(-a.TotalFee,0) as TotalPrice,a.Discount,");
            strSql.AppendLine("convert(varchar(10),c.BackDate,23) as DealDate,c.Seller as DealEmployee,isnull(c.Rate,1) as Rate,");
            strSql.AppendLine("isnull(e.EmployeeName,'') as DealEmployeeName,isnull(d.CodeName,'') as UnitName");
            strSql.AppendLine(" from officedba.SellBackDetail a ");
            strSql.AppendLine("left join officedba.ProductInfo b on a.ProductID=b.ID");
            strSql.AppendLine("left join officedba.SellBack c on a.BackNo=c.BackNo and a.CompanyCD=c.CompanyCD");
            if (isMoreUnit)
            {
                strSql.AppendLine("left join officedba.CodeUnitType d on d.ID=a.UsedUnitID");
            }
            else
            {
                strSql.AppendLine("left join officedba.CodeUnitType d on d.ID=a.UnitID");
            }
            strSql.AppendLine("left join officedba.EmployeeInfo e on e.ID=c.Seller");
            strSql.AppendLine("where a.CompanyCD=@CompanyCD and c.BillStatus='2' ");
            if (!string.IsNullOrEmpty(ProjectID))
            {
                strSql.AppendLine(" and c.ProjectID=@ProjectID ");
            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                strSql.AppendLine(" and c.BackDate>=@BeginDate ");
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                strSql.AppendLine(" and c.BackDate<dateadd(dd,1,@EndDate) ");
            }
            #endregion
            SqlParameter[] param = { 
                                    new SqlParameter("@CompanyCD",strCompanyCD),
                                    new SqlParameter("@ProjectID",ProjectID),
                                    new SqlParameter("@BeginDate",BeginDate),
                                    new SqlParameter("@EndDate",EndDate)
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        #region 获取“费用支出”
        /// <summary>
        /// 获取“费用支出”
        /// </summary>
        /// <param name="strCompanyCD">公司编码</param>
        /// <param name="ProjectID">所属项目</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetFeeDetailInfoList(string strCompanyCD, string ProjectID, string BeginDate, string EndDate)
        {
            StringBuilder strSql = new StringBuilder();
            #region //费用票据中类别不为“费用报销单”
            strSql.AppendLine(" select b.FeesType,convert(varchar(10),b.CreateDate,23) as DealDate,b.Executor,");
            strSql.AppendLine("a.FeeID ,isnull(a.FeeTotal,0) as FeeTotal,isnull(b.CurrencyRate,1) as Rate, ");
            strSql.AppendLine(" isnull(c.CodeName,'') as FeesName,isnull(e.EmployeeName,'') as DealEmployeeName,");
            //费用票据类型（0.无来源，1.采购订单、2.采购到货单、3.采购退货单、4.销售订单、5.销售发货通知单、6.销售退货单、7.费用报销单、8.销售出库单、9.其他出库单）
            strSql.AppendLine(" case b.FeesType when 0 then '无来源' when 1 then '采购订单' when 2 then '采购到货单' when 3 then '采购退货单' ");
            strSql.AppendLine(" when 4 then '销售订单' when 5 then '销售发货通知单' when 6 then '销售退货单' ");
            strSql.AppendLine(" when 8 then '销售出库单' when 9 then '其他出库单' end as FeesTypeName ");
            strSql.AppendLine(" from officedba.FeesDetail a ");
            strSql.AppendLine(" left join officedba.Fees b on a.FeesNo =b.FeesNo  and a.CompanyCD=b.CompanyCD ");
            strSql.AppendLine(" left join officedba.CodeFeeType c on c.ID=a.FeeID ");
            strSql.AppendLine(" left join officedba.EmployeeInfo e on e.ID=b.Executor ");
            strSql.AppendLine(" where a.CompanyCD=@CompanyCD and b.ConfirmStatus=1 ");
            strSql.AppendLine(" and b.FeesType!=7 ");
            if (!string.IsNullOrEmpty(ProjectID))
            {
                strSql.AppendLine(" and b.ProjectID=@ProjectID ");
            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                strSql.AppendLine(" and b.CreateDate>=@BeginDate ");
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                strSql.AppendLine(" and b.CreateDate< dateadd(dd,1,@EndDate)");
            }
            #endregion
            strSql.AppendLine(" union all ");

            #region //费用报销单
            strSql.AppendLine(" select 7 as FeesType,convert(varchar(10),b.ReimbDate,23) as DealDate,b.UserReimbID as Executor,");
            strSql.AppendLine("a.FeeNameID as FeeID,isnull(a.ReimbAmount,0) as FeeTotal, 1 as Rate,");
            strSql.AppendLine(" isnull(c.CodeName,'') as FeesName,isnull(e.EmployeeName,'') as DealEmployeeName,");
            strSql.AppendLine(" '费用报销单' as FeesTypeName ");
            strSql.AppendLine(" from officedba.FeeReturnDetail a ");
            strSql.AppendLine(" left join officedba.FeeReturn b on a.ReimbID=b.ID and b.CompanyCD=@CompanyCD ");
            strSql.AppendLine(" left join officedba.CodeFeeType c on c.ID=a.FeeNameID ");
            strSql.AppendLine(" left join officedba.EmployeeInfo e on e.ID=b.UserReimbID ");
            strSql.AppendLine(" where b.Status='2' ");
            if (!string.IsNullOrEmpty(ProjectID))
            {
                strSql.AppendLine(" and b.ProjectID=@ProjectID ");
            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                strSql.AppendLine(" and b.ReimbDate>=@BeginDate ");
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                strSql.AppendLine(" and b.ReimbDate< dateadd(dd,1,@EndDate)");
            }
            #endregion
            SqlParameter[] param = { 
                                    new SqlParameter("@CompanyCD",strCompanyCD),
                                    new SqlParameter("@ProjectID",ProjectID),
                                    new SqlParameter("@BeginDate",BeginDate),
                                    new SqlParameter("@EndDate",EndDate)
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        #region  获取“收款单”明细列表
        /// <summary>
        /// 获取“收款单”明细列表
        /// </summary>
        /// <param name="strCompanyCD">公司编码</param>
        /// <param name="ProjectID">所属项目</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetInComeBillDetailInfoList(string strCompanyCD, string ProjectID, string BeginDate, string EndDate)
        {
            StringBuilder strSql = new StringBuilder();
            #region 收款单
            strSql.AppendLine("select a.CustName,convert(varchar(10),a.AcceDate,23) as DealDate,a.Executor,isnull(e.EmployeeName,'') as DealEmployeeName,");
            strSql.AppendLine("isnull(a.TotalPrice,0) as TotalPrice,isnull(a.CurrencyRate,1) as Rate ");
            strSql.AppendLine("from officedba.IncomeBill a ");
            strSql.AppendLine("left join officedba.EmployeeInfo e on e.ID=a.Executor");
            strSql.AppendLine("where a.CompanyCD=@CompanyCD and a.ConfirmStatus='1' ");
            if (!string.IsNullOrEmpty(ProjectID))
            {
                strSql.AppendLine(" and a.ProjectID=@ProjectID ");
            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                strSql.AppendLine(" and a.AcceDate>=@BeginDate ");
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                strSql.AppendLine(" and a.AcceDate<dateadd(dd,1,@EndDate) ");
            }
            #endregion
            SqlParameter[] param = { 
                                    new SqlParameter("@CompanyCD",strCompanyCD),
                                    new SqlParameter("@ProjectID",ProjectID),
                                    new SqlParameter("@BeginDate",BeginDate),
                                    new SqlParameter("@EndDate",EndDate)
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        #region  获取“付款单”明细列表
        /// <summary>
        /// 获取“付款单”明细列表
        /// </summary>
        /// <param name="strCompanyCD">公司编码</param>
        /// <param name="ProjectID">所属项目</param>
        /// <param name="BeginDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public static DataTable GetPayBillDetailInfoList(string strCompanyCD, string ProjectID, string BeginDate, string EndDate)
        {
            StringBuilder strSql = new StringBuilder();
            #region 收款单
            strSql.AppendLine("select a.CustName,convert(varchar(10),a.PayDate,23) as DealDate,a.Executor,isnull(e.EmployeeName,'') as DealEmployeeName,");
            strSql.AppendLine("isnull(a.PayAmount,0) as TotalPrice,isnull(a.CurrencyRate,1) as Rate ");
            strSql.AppendLine("from officedba.PayBill a ");
            strSql.AppendLine("left join officedba.EmployeeInfo e on e.ID=a.Executor");
            strSql.AppendLine("where a.CompanyCD=@CompanyCD and a.ConfirmStatus='1' ");
            if (!string.IsNullOrEmpty(ProjectID))
            {
                strSql.AppendLine(" and a.ProjectID=@ProjectID ");
            }
            if (!string.IsNullOrEmpty(BeginDate))
            {
                strSql.AppendLine(" and a.PayDate>=@BeginDate ");
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                strSql.AppendLine(" and a.PayDate<dateadd(dd,1,@EndDate) ");
            }
            #endregion
            SqlParameter[] param = { 
                                    new SqlParameter("@CompanyCD",strCompanyCD),
                                    new SqlParameter("@ProjectID",ProjectID),
                                    new SqlParameter("@BeginDate",BeginDate),
                                    new SqlParameter("@EndDate",EndDate)
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion
    }
}

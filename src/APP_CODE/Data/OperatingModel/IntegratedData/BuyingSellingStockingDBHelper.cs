/**********************************************
 * 类作用：   进销存分析
 * 建立人：   王玉贞
 * 建立时间： 2010/05/05
 ***********************************************/

using System;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using XBase.Common;
using XBase.Model.Office.SupplyChain;


namespace XBase.Data.OperatingModel.IntegratedData
{
    public class BuyingSellingStockingDBHelper
    {
        #region 进销存日报表
        /// <summary>
        /// 进销存日报表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="DailyDate"></param>
        /// <param name="EFIndex"></param>
        /// <param name="EFDesc"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetBuyingSellingStockingByDay(ProductInfoModel model, string DailyDate, string BatchNo, string EFIndex, string EFDesc, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT	isnull( CONVERT(CHAR(10), a.DailyDate, 23),'') as DailyDate,b.ProductName,a.ProductID,b.Specification,a.BatchNo,");
            searchSql.AppendLine("		Convert(numeric(22," + userInfo.SelPoint + "),isnull((select top 1 b.TodayCount  from officedba.StorageDaily b where");
            searchSql.AppendLine("			a.ProductID=b.ProductID");
            searchSql.AppendLine("			and b.DailyDate>=(CONVERT(CHAR(10), dateadd(dd,-1,a.DailyDate), 23))");
            searchSql.AppendLine("			and b.DailyDate<CONVERT(CHAR(10), a.DailyDate, 23)");
            searchSql.AppendLine("		),0)) as YestodayCount,");
            searchSql.AppendLine("		Convert(numeric(22," + userInfo.SelPoint + "),a.PhurInCount) as PhurInCount,Convert(numeric(22," + userInfo.SelPoint + "),a.MakeInCount) as MakeInCount,");
            searchSql.AppendLine("		Convert(numeric(22," + userInfo.SelPoint + "),a.DispInCount) as DispInCount,");
            searchSql.AppendLine("		Convert(numeric(22," + userInfo.SelPoint + "),a.OtherInCount) as OtherInCount,Convert(numeric(22," + userInfo.SelPoint + "),a.SendInCount) as SendInCount,Convert(numeric(22," + userInfo.SelPoint + "),a.SubSaleBackInCount) as SubSaleBackInCount,");
            searchSql.AppendLine("		Convert(numeric(22," + userInfo.SelPoint + "),a.TakeInCount) as TakeInCount,Convert(numeric(22," + userInfo.SelPoint + "),a.InTotal) as InTotal,Convert(numeric(22," + userInfo.SelPoint + "),a.SaleFee) as SaleFee,Convert(numeric(22," + userInfo.SelPoint + "),a.PhurBackFee) as PhurBackFee,");
            searchSql.AppendLine("		Convert(numeric(22," + userInfo.SelPoint + "),a.InitInCount) as InitInCount,Convert(numeric(22," + userInfo.SelPoint + "),a.InitBatchCount) as InitBatchCount,Convert(numeric(22," + userInfo.SelPoint + "),a.SaleBackInCount) as SaleBackInCount,");
            searchSql.AppendLine("		Convert(numeric(22," + userInfo.SelPoint + "),a.RedInCount) as RedInCount,Convert(numeric(22," + userInfo.SelPoint + "),a.BackInCount) as BackInCount,");
            searchSql.AppendLine("		");
            searchSql.AppendLine("		Convert(numeric(22," + userInfo.SelPoint + "),a.SaleOutCount) as SaleOutCount,Convert(numeric(22," + userInfo.SelPoint + "),a.TakeOutCount) as TakeOutCount,");
            searchSql.AppendLine("		Convert(numeric(22," + userInfo.SelPoint + "),a.DispOutCount) as DispOutCount,Convert(numeric(22," + userInfo.SelPoint + "),a.BadCount) as BadCount,");
            searchSql.AppendLine("		Convert(numeric(22," + userInfo.SelPoint + "),a.OtherOutCount) as OtherOutCount,Convert(numeric(22," + userInfo.SelPoint + "),a.SendOutCount) as SendOutCount,Convert(numeric(22," + userInfo.SelPoint + "),a.SubSaleOutCount) as SubSaleOutCount,");
            searchSql.AppendLine("		Convert(numeric(22," + userInfo.SelPoint + "),a.OutTotal) as OutTotal,Convert(numeric(22," + userInfo.SelPoint + "),a.PhurFee) as PhurFee,Convert(numeric(22," + userInfo.SelPoint + "),a.SaleBackFee) as SaleBackFee,");
            searchSql.AppendLine("		Convert(numeric(22," + userInfo.SelPoint + "),a.PhurBackOutCount) as PhurBackOutCount,Convert(numeric(22," + userInfo.SelPoint + "),a.RedOutCount) as RedOutCount,Convert(numeric(22," + userInfo.SelPoint + "),a.LendCount) as LendCount,");
            searchSql.AppendLine("		Convert(numeric(22," + userInfo.SelPoint + "),ABS(a.CheckCount)) as CheckCount,Convert(numeric(22," + userInfo.SelPoint + "),ABS(a.AdjustCount)) as AdjustCount,");
            searchSql.AppendLine("		Convert(numeric(22," + userInfo.SelPoint + "),ABS(a.TodayCount)) as TodayCount,a.CreateDate,");
            searchSql.AppendLine("		b.ExtField1,b.ExtField2,b.ExtField3,b.ExtField4,b.ExtField5,b.ExtField6,b.ExtField7,b.ExtField8,b.ExtField9,b.ExtField10,");
            searchSql.AppendLine("		b.ExtField11,b.ExtField12,b.ExtField13,b.ExtField14,b.ExtField15,b.ExtField16,b.ExtField17,b.ExtField18,b.ExtField19,b.ExtField20,");
            searchSql.AppendLine("		b.ExtField21,b.ExtField22,b.ExtField23,b.ExtField24,b.ExtField25,b.ExtField26,b.ExtField27,b.ExtField28,b.ExtField29,b.ExtField30");
            searchSql.AppendLine("FROM	officedba.StorageDaily a ");
            searchSql.AppendLine("left join officedba.ProductInfo b on a.ProductID=b.ID");
            searchSql.AppendLine("where a.CompanyCD=@CompanyCD ");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            //单据编号
            if (!string.IsNullOrEmpty(DailyDate))
            {
                searchSql.AppendLine(" and CONVERT(CHAR(10), a.DailyDate, 23)=@DailyDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DailyDate", DailyDate));
            }
            //物品编号
            if (!string.IsNullOrEmpty(model.ProdNo))
            {
                searchSql.AppendLine(" and b.ProdNo like @ProdNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProdNo", "%" + model.ProdNo + "%"));
            }
            //物品名称
            if (!string.IsNullOrEmpty(model.ProductName))
            {
                searchSql.AppendLine(" and b.ProductName like @ProductName");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", "%" + model.ProductName + "%"));
            }
            //物品规格
            if (!string.IsNullOrEmpty(model.Specification))
            {
                searchSql.AppendLine(" and b.Specification like @Specification");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Specification", "%" + model.Specification + "%"));
            }
            //仓库
            if (!string.IsNullOrEmpty(model.StorageID))
            {
                if (int.Parse(model.StorageID) > 0)
                {
                    searchSql.AppendLine(" and a.StorageID=@StorageID");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", model.StorageID));
                }
            }
            //仓库
            if (!string.IsNullOrEmpty(BatchNo))
            {
                searchSql.AppendLine(" and a.BatchNo=@BatchNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", BatchNo));
            }
            //条码
            if (!string.IsNullOrEmpty(model.BarCode))
            {
                searchSql.AppendLine(" and b.BarCode=@BarCode");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BarCode", model.BarCode));
            }
            //产地
            if (!string.IsNullOrEmpty(model.FromAddr))
            {
                searchSql.AppendLine(" and b.FromAddr like @FromAddr");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromAddr", "%" + model.FromAddr + "%"));
            }
            //厂家
            if (!string.IsNullOrEmpty(model.Manufacturer))
            {
                searchSql.AppendLine(" and b.Manufacturer like @Manufacturer");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Manufacturer", "%" + model.Manufacturer + "%"));
            }
            //尺寸
            if (!string.IsNullOrEmpty(model.Size))
            {
                searchSql.AppendLine(" and b.Size=@Size");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Size", model.Size));
            }
            //材质
            if (!string.IsNullOrEmpty(model.Material))
            {
                searchSql.AppendLine(" and b.Material=@Material");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Material", model.Material));
            }
            //颜色
            if (!string.IsNullOrEmpty(model.ColorID))
            {
                searchSql.AppendLine(" and b.ColorID=@ColorID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ColorID", model.ColorID));
            }
            //其他条件
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                if (int.Parse(EFIndex) > 0)
                {
                    searchSql.AppendLine(" and b.ExtField" + EFIndex + " LIKE @EFDesc");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + EFDesc + "%"));
                }
            }
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion

        #region 总计:进销存日报表
        /// <summary>
        /// 进销存日报表总计
        /// </summary>
        /// <param name="model"></param>
        /// <param name="DailyDate"></param>
        /// <param name="BatchNo"></param>
        /// <param name="EFIndex"></param>
        /// <param name="EFDesc"></param>
        /// <returns></returns>
        public static DataTable GetSumBuyingSellingStockingByDay(ProductInfoModel model, string DailyDate, string BatchNo, string EFIndex, string EFDesc)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT	Convert(numeric(22," + userInfo.SelPoint + "),sum(InTotal)) as InTotalCount,Convert(numeric(22," + userInfo.SelPoint + "),sum(OutTotal)) as OutTotalCount,Convert(numeric(22," + userInfo.SelPoint + "),sum(SaleFee)) as SaleFeeCount,");
            searchSql.AppendLine("		Convert(numeric(22," + userInfo.SelPoint + "),sum(PhurFee)) as PhurFeeCount,Convert(numeric(22," + userInfo.SelPoint + "),sum(PhurBackFee)) as PhurBackFeeCount,Convert(numeric(22," + userInfo.SelPoint + "),sum(SaleBackFee)) as SaleBackFeeCount ");
            searchSql.AppendLine("FROM	officedba.StorageDaily a");
            searchSql.AppendLine("left join officedba.ProductInfo b on a.ProductID=b.ID");
            searchSql.AppendLine("where a.CompanyCD=@CompanyCD ");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            //单据编号
            if (!string.IsNullOrEmpty(DailyDate))
            {
                searchSql.AppendLine(" and CONVERT(CHAR(10), a.DailyDate, 23)=@DailyDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DailyDate", DailyDate));
            }
            //物品编号
            if (!string.IsNullOrEmpty(model.ProdNo))
            {
                searchSql.AppendLine(" and b.ProdNo like @ProdNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProdNo", "%" + model.ProdNo + "%"));
            }
            //物品名称
            if (!string.IsNullOrEmpty(model.ProductName))
            {
                searchSql.AppendLine(" and b.ProductName like @ProductName");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", "%" + model.ProductName + "%"));
            }
            //物品规格
            if (!string.IsNullOrEmpty(model.Specification))
            {
                searchSql.AppendLine(" and b.Specification like @Specification");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Specification", "%" + model.Specification + "%"));
            }
            //仓库
            if (!string.IsNullOrEmpty(model.StorageID))
            {
                if (int.Parse(model.StorageID) > 0)
                {
                    searchSql.AppendLine(" and a.StorageID=@StorageID");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", model.StorageID));
                }
            }
            //仓库
            if (!string.IsNullOrEmpty(BatchNo))
            {
                searchSql.AppendLine(" and a.BatchNo=@BatchNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", BatchNo));
            }
            //条码
            if (!string.IsNullOrEmpty(model.BarCode))
            {
                searchSql.AppendLine(" and b.BarCode=@BarCode");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BarCode", model.BarCode));
            }
            //产地
            if (!string.IsNullOrEmpty(model.FromAddr))
            {
                searchSql.AppendLine(" and b.FromAddr like @FromAddr");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromAddr", "%" + model.FromAddr + "%"));
            }
            //厂家
            if (!string.IsNullOrEmpty(model.Manufacturer))
            {
                searchSql.AppendLine(" and b.Manufacturer like @Manufacturer");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Manufacturer", "%" + model.Manufacturer + "%"));
            }
            //尺寸
            if (!string.IsNullOrEmpty(model.Size))
            {
                searchSql.AppendLine(" and b.Size=@Size");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Size", model.Size));
            }
            //材质
            if (!string.IsNullOrEmpty(model.Material))
            {
                searchSql.AppendLine(" and b.Material=@Material");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Material", model.Material));
            }
            //颜色
            if (!string.IsNullOrEmpty(model.ColorID))
            {
                searchSql.AppendLine(" and b.ColorID=@ColorID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ColorID", model.ColorID));
            }
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                if (int.Parse(EFIndex) > 0)
                {
                    searchSql.AppendLine(" and b.ExtField" + EFIndex + " LIKE @EFDesc");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + EFDesc + "%"));
                }
            }
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 进销存日报表明细
        /// <summary>
        /// 进销存日报表明细表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="Happendate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetBuyingSellingStockingByDayDetail(ProductInfoModel model, string HappenDate, string BatchNo, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select a.ID,a.BillNo,a.ProductID,b.ProductName,a.StorageID,c.StorageName,a.BillType,");
            searchSql.AppendLine("		a.BatchNo,Convert(numeric(22," + userInfo.SelPoint + "),a.HappenCount) as HappenCount,a.HappenDate as OperateDate,Convert(numeric(22," + userInfo.SelPoint + "),a.ProductCount) as ProductCount,a.Creator,d.EmployeeName,");
            searchSql.AppendLine("		isnull( CONVERT(CHAR(10), a.HappenDate, 23),'') as HappenDate,");
            searchSql.AppendLine("		case	when a.BillType=1 then '期初库存录入'");
            searchSql.AppendLine("				when a.BillType=2 then '期初库存批量导入'");
            searchSql.AppendLine("				when a.BillType=3 then	'采购入库单'");
            searchSql.AppendLine("				when a.BillType=4 then	'生产完工入库单'");
            searchSql.AppendLine("				when a.BillType=5 then	'其他入库单'");
            searchSql.AppendLine("				when a.BillType=6 then	'红冲入库单'");
            searchSql.AppendLine("				when a.BillType=7 then	'销售出库单'");
            searchSql.AppendLine("				when a.BillType=8 then	'其他出库单'");
            searchSql.AppendLine("				when a.BillType=9 then	'红冲出库单'");
            searchSql.AppendLine("				when a.BillType=10 then	'借货申请单'");
            searchSql.AppendLine("				when a.BillType=11 then	'借货返还单'");
            searchSql.AppendLine("				when a.BillType=12 then	'调拨出库'");
            searchSql.AppendLine("				when a.BillType=13 then	'调拨入库'");
            searchSql.AppendLine("				when a.BillType=14 then	'日常调整单'");
            searchSql.AppendLine("				when a.BillType=15 then	'期末盘点单'");
            searchSql.AppendLine("				when a.BillType=16 then	'库存报损单'");
            searchSql.AppendLine("				when a.BillType=17 then	'领料单'");
            searchSql.AppendLine("				when a.BillType=18 then	'退料单'");
            searchSql.AppendLine("				when a.BillType=19 then	'配送单'");
            searchSql.AppendLine("				when a.BillType=20 then	'配送退货单'");
            searchSql.AppendLine("				when a.BillType=21 then	'门店销售管理'");
            searchSql.AppendLine("				when a.BillType=22 then	'门店销售退货'");
            searchSql.AppendLine("		 end as strBillText");
            searchSql.AppendLine("from officedba.StorageAccount a");
            searchSql.AppendLine("left join officedba.ProductInfo b on a.ProductID=b.ID");
            searchSql.AppendLine("left join officedba.StorageInfo c on a.StorageID=c.ID");
            searchSql.AppendLine("left join officedba.EmployeeInfo d on a.Creator=d.ID");
            searchSql.AppendLine("where CONVERT(CHAR(10), a.HappenDate, 23)=@Happendate and a.CompanyCD=@CompanyCD");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Happendate", HappenDate));
            //物品ID
            if (model.ID > 0)
            {
                searchSql.AppendLine(" and a.ProductID=@ProductID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", model.ID.ToString()));
            }
            //仓库
            if (!string.IsNullOrEmpty(BatchNo))
            {
                searchSql.AppendLine(" and a.BatchNo=@BatchNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", BatchNo));
            }
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion

        #region 进销存汇总表明细
        /// <summary>
        /// 进销存汇总表明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="Happendate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetTotalInAndOutDetail(ProductInfoModel model, string StartDate, string EndDate, string BatchNo, string EFIndex, string EFDesc, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select a.ID,a.BillNo,a.ProductID,b.ProductName,a.StorageID,c.StorageName,a.BillType,");
            searchSql.AppendLine("		a.BatchNo,Convert(numeric(22," + userInfo.SelPoint + "),a.HappenCount) as HappenCount,a.HappenDate as OperateDate,Convert(numeric(22," + userInfo.SelPoint + "),a.ProductCount) as ProductCount,a.Creator,d.EmployeeName,");
            searchSql.AppendLine("		isnull( CONVERT(CHAR(10), a.HappenDate, 23),'') as HappenDate,");
            searchSql.AppendLine("		case	when a.BillType=1 then '期初库存录入'");
            searchSql.AppendLine("				when a.BillType=2 then '期初库存批量导入'");
            searchSql.AppendLine("				when a.BillType=3 then	'采购入库单'");
            searchSql.AppendLine("				when a.BillType=4 then	'生产完工入库单'");
            searchSql.AppendLine("				when a.BillType=5 then	'其他入库单'");
            searchSql.AppendLine("				when a.BillType=6 then	'红冲入库单'");
            searchSql.AppendLine("				when a.BillType=7 then	'销售出库单'");
            searchSql.AppendLine("				when a.BillType=8 then	'其他出库单'");
            searchSql.AppendLine("				when a.BillType=9 then	'红冲出库单'");
            searchSql.AppendLine("				when a.BillType=10 then	'借货申请单'");
            searchSql.AppendLine("				when a.BillType=11 then	'借货返还单'");
            searchSql.AppendLine("				when a.BillType=12 then	'调拨出库'");
            searchSql.AppendLine("				when a.BillType=13 then	'调拨入库'");
            searchSql.AppendLine("				when a.BillType=14 then	'日常调整单'");
            searchSql.AppendLine("				when a.BillType=15 then	'期末盘点单'");
            searchSql.AppendLine("				when a.BillType=16 then	'库存报损单'");
            searchSql.AppendLine("				when a.BillType=17 then	'领料单'");
            searchSql.AppendLine("				when a.BillType=18 then	'退料单'");
            searchSql.AppendLine("				when a.BillType=19 then	'配送单'");
            searchSql.AppendLine("				when a.BillType=20 then	'配送退货单'");
            searchSql.AppendLine("				when a.BillType=21 then	'门店销售管理'");
            searchSql.AppendLine("				when a.BillType=22 then	'门店销售退货'");
            searchSql.AppendLine("		 end as strBillText");
            searchSql.AppendLine("from officedba.StorageAccount a");
            searchSql.AppendLine("left join officedba.ProductInfo b on a.ProductID=b.ID");
            searchSql.AppendLine("left join officedba.StorageInfo c on a.StorageID=c.ID");
            searchSql.AppendLine("left join officedba.EmployeeInfo d on a.Creator=d.ID");
            searchSql.AppendLine("where a.HappenDate >=@StartDate and a.HappenDate<=@EndDate and a.CompanyCD=@CompanyCD");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", StartDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", EndDate));
            //物品编号
            if (!string.IsNullOrEmpty(model.ProdNo))
            {
                searchSql.AppendLine(" and b.ProdNo like @ProdNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProdNo", "%" + model.ProdNo + "%"));
            }
            //物品名称
            if (!string.IsNullOrEmpty(model.ProductName))
            {
                searchSql.AppendLine(" and b.ProductName like @ProductName");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", "%" + model.ProductName + "%"));
            }
            //物品规格
            if (!string.IsNullOrEmpty(model.Specification))
            {
                searchSql.AppendLine(" and b.Specification like @Specification");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Specification", "%" + model.Specification + "%"));
            }
            //仓库
            if (!string.IsNullOrEmpty(model.StorageID))
            {
                if (int.Parse(model.StorageID) > 0)
                {
                    searchSql.AppendLine(" and a.StorageID=@StorageID");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", model.StorageID));
                }
            }
            //仓库
            if (!string.IsNullOrEmpty(BatchNo))
            {
                searchSql.AppendLine(" and a.BatchNo=@BatchNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", BatchNo));
            }
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                if (int.Parse(EFIndex) > 0)
                {
                    searchSql.AppendLine(" and b.ExtField" + EFIndex + " LIKE @EFDesc");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + EFDesc + "%"));
                }
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion

        #region 门店进销存日报表（分店报表）
        /// <summary>
        /// 门店进销存日报表（分店报表）
        /// </summary>
        /// <param name="model">检索条件实体</param>
        /// <param name="EFIndex">扩展属性</param>
        /// <param name="EFDesc">扩展属性</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetSubStoreInvoicingDateRptData(XBase.Model.Office.OperatingModel.SubStoreInvoicingDateRptModel model, string EFIndex, string EFDesc, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select a.ID,isnull( CONVERT(CHAR(10), a.DailyDate, 23),'') as DailyDate,a.BatchNo,");
            strSql.AppendLine(" a.ProductID,a.DeptID,b.ProductName,b.Specification,");
            strSql.AppendLine(" convert(decimal(22," + model.PreLength + "),(select top 1 c.TodayCount  from officedba.SubStorageDaily c ");
            strSql.AppendLine("	  where a.ProductID=c.ProductID and c.CompanyCD=@CompanyCD and c.DeptID=@SubDeptID ");
            strSql.AppendLine("	  and c.DailyDate=(CONVERT(CHAR(10), dateadd(dd,-1,@InitDate), 23)) ");
            //批次
            if (!string.IsNullOrEmpty(model.BatchNo))
            {
                strSql.AppendLine(" and c.BatchNo like @BatchNo ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", "%" + model.BatchNo + "%"));
            }
            strSql.AppendLine(" )) as YestodayCount,");//上日结存
            strSql.AppendLine(" convert(decimal(22," + model.PreLength + "),(a.SendInCount+a.DispInCont-a.DispOutCount-a.SendOutCount)) as TodaySendCount,");//本日配送
            strSql.AppendLine(" convert(decimal(22," + model.PreLength + "),(a.SubSaleOutCount+a.SaleOutCount)) as TodaySellCount,");//本日销货
            strSql.AppendLine(" convert(decimal(22," + model.PreLength + "),isnull(a.SendOutCount,0)) as SendOutCount,");//配送退货出库数量
            strSql.AppendLine(" convert(decimal(22," + model.PreLength + "),isnull(a.SaleOutCount,0)) as SaleOutCount,");//销售出库数量（总店发货模式）
            strSql.AppendLine(" convert(decimal(22," + model.PreLength + "),isnull(a.SubSaleOutCount,0)) as SubSaleOutCount,");//销售出库数量（分店发货模式）
            strSql.AppendLine("convert(decimal(22," + model.PreLength + "),isnull(a.DispOutCount,0)) as DispOutCount,");//门店调拨出库数量
            strSql.AppendLine(" convert(decimal(22," + model.PreLength + "),isnull(a.TodayCount,0)) as TodayCount,");//当日结存量(现有存量，从门店分仓存量表获取)
            strSql.AppendLine("convert(decimal(22," + model.PreLength + "),isnull(a.SaleFee,0)) as SaleFee,");//当日销售金额
            strSql.AppendLine("convert(decimal(22," + model.PreLength + "),isnull(a.SaleBackFee,0)) as SaleBackFee, ");//当日退货金额
            strSql.AppendLine(" convert(decimal(22," + model.PreLength + "),isnull(a.SendInCount,0)) as SendInCount,");//配送入库数量
            strSql.AppendLine(" convert(decimal(22," + model.PreLength + "),isnull(a.DispInCont,0)) as DispInCont,");//门店调拨入库数量

            strSql.AppendLine("convert(decimal(22," + model.PreLength + "),isnull(a.InitInCount,0)) as InitInCount, ");//门店期初库存录入数量
            strSql.AppendLine("convert(decimal(22," + model.PreLength + "),isnull(a.InitBatchCount,0)) as InitBatchCount, ");//门店期初库存导入数量
            strSql.AppendLine("convert(decimal(22," + model.PreLength + "),isnull(a.SubSaleBackInCount,0)) as SubSaleBackInCount, ");//销售退货入库数量（分店发货模式）
            strSql.AppendLine("convert(decimal(22," + model.PreLength + "),isnull(a.InTotal,0)) as InTotal, ");//入库合计
            strSql.AppendLine("convert(decimal(22," + model.PreLength + "),isnull(a.OutTotal,0)) as OutTotal, ");//出库合计

            strSql.AppendLine(" b.ExtField1,b.ExtField2,b.ExtField3,b.ExtField4,b.ExtField5,b.ExtField6,b.ExtField7,b.ExtField8,b.ExtField9,b.ExtField10,");
            strSql.AppendLine(" b.ExtField11,b.ExtField12,b.ExtField13,b.ExtField14,b.ExtField15,b.ExtField16,b.ExtField17,b.ExtField18,b.ExtField19,b.ExtField20,");
            strSql.AppendLine(" b.ExtField21,b.ExtField22,b.ExtField23,b.ExtField24,b.ExtField25,b.ExtField26,b.ExtField27,b.ExtField28,b.ExtField29,b.ExtField30 ");
            strSql.AppendLine(" from officedba. SubStorageDaily a ");
            strSql.AppendLine(" left join officedba.ProductInfo b on a.ProductID=b.ID ");
            strSql.AppendLine(" where a.CompanyCD=@CompanyCD and a.DeptID=@SubDeptID ");

            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SubDeptID", model.SubDeptID));

            //日期
            if (!string.IsNullOrEmpty(model.InitDate))
            {
                strSql.AppendLine(" and CONVERT(CHAR(10), a.DailyDate, 23)=@InitDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@InitDate", model.InitDate));
            }
            //物品编号
            if (!string.IsNullOrEmpty(model.ProductNo))
            {
                strSql.AppendLine(" and b.ProdNo=@ProdNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProdNo", model.ProductNo));
            }
            //物品名称
            if (!string.IsNullOrEmpty(model.ProductName))
            {
                strSql.AppendLine(" and b.ProductName like @ProductName");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", "%" + model.ProductName + "%"));
            }
            //物品规格
            if (!string.IsNullOrEmpty(model.Specification))
            {
                strSql.AppendLine(" and b.Specification like @Specification");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Specification", "%" + model.Specification + "%"));
            }
            //批次
            if (!string.IsNullOrEmpty(model.BatchNo))
            {
                strSql.AppendLine(" and a.BatchNo like @BatchNo ");
                //comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", "%" + model.BatchNo + "%"));
            }
            //厂家
            if (!string.IsNullOrEmpty(model.Manufacturer))
            {
                strSql.AppendLine(" and b.Manufacturer like @Manufacturer ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Manufacturer", "%" + model.Manufacturer + "%"));
            }
            //产地
            if (!string.IsNullOrEmpty(model.FromAddr))
            {
                strSql.AppendLine(" and b.FromAddr like @FromAddr ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromAddr", "%" + model.FromAddr + "%"));
            }
            //颜色
            if (!string.IsNullOrEmpty(model.ColorID))
            {
                strSql.AppendLine(" and b.ColorID=@ColorID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ColorID", model.ColorID));
            }
            //尺寸
            if (!string.IsNullOrEmpty(model.Size))
            {
                strSql.AppendLine(" and b.Size=@Size ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Size", model.Size));
            }
            //条码
            if (!string.IsNullOrEmpty(model.BarCode))
            {
                strSql.AppendLine(" and b.BarCode=@BarCode ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BarCode", model.BarCode));
            }
            //材质
            if (!string.IsNullOrEmpty(model.MaterialID))
            {
                strSql.AppendLine(" and b.Material=@MaterialID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@MaterialID", model.MaterialID));
            }
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                if (int.Parse(EFIndex) > 0)
                {
                    strSql.AppendLine(" and b.ExtField" + EFIndex + " LIKE @EFDesc");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + EFDesc + "%"));
                }
            }
            //指定命令的SQL文
            comm.CommandText = strSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref totalCount);
        }
        #endregion

        #region 总计：门店进销存日报表（分店报表）
        /// <summary>
        /// 总计：门店进销存日报表（分店报表）
        /// </summary>
        /// <param name="model"></param>
        /// <param name="EFIndex"></param>
        /// <param name="EFDesc"></param>
        /// <returns></returns>
        public static DataTable GetSumSubStoreInvoicingDateRptData(XBase.Model.Office.OperatingModel.SubStoreInvoicingDateRptModel model, string EFIndex, string EFDesc)
        {
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            #region 查询语句
            //查询SQL拼写
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine("select convert(decimal(22," + model.PreLength + "),sum(d.YestodayCount)) as TotalYestodayCount,");
            strSql.AppendLine(" convert(decimal(22," + model.PreLength + "),sum(d.TodaySendCount)) as TotalTodaySendCount,");
            strSql.AppendLine(" convert(decimal(22," + model.PreLength + "),sum(d.TodaySellCount)) as TotalTodaySellCount,");
            strSql.AppendLine(" convert(decimal(22," + model.PreLength + "),sum(d.SendOutCount)) as TotalSendOutCount,");
            strSql.AppendLine(" convert(decimal(22," + model.PreLength + "),sum(d.SaleOutCount)) as TotalSaleOutCount,");
            strSql.AppendLine(" convert(decimal(22," + model.PreLength + "),sum(d.SubSaleOutCount)) as TotalSubSaleOutCount,");
            strSql.AppendLine(" convert(decimal(22," + model.PreLength + "),sum(d.TodayCount)) as TotalTodayCount,");
            strSql.AppendLine(" convert(decimal(22," + model.PreLength + "),sum(d.SaleFee)) as TotalSaleFee,");
            strSql.AppendLine(" convert(decimal(22," + model.PreLength + "),sum(d.SaleBackFee)) as TotalSaleBackFee, ");

            strSql.AppendLine("convert(decimal(22," + model.PreLength + "),sum(d.InitInCount)) as TotalInitInCount, ");//门店期初库存录入数量
            strSql.AppendLine("convert(decimal(22," + model.PreLength + "),sum(d.InitBatchCount)) as TotalInitBatchCount, ");//门店期初库存导入数量
            strSql.AppendLine("convert(decimal(22," + model.PreLength + "),sum(d.SubSaleBackInCount)) as TotalSubSaleBackInCount, ");//销售退货入库数量（分店发货模式）
            strSql.AppendLine("convert(decimal(22," + model.PreLength + "),sum(d.InTotal)) as TotalInTotal, ");//入库合计
            strSql.AppendLine("convert(decimal(22," + model.PreLength + "),sum(d.OutTotal)) as TotalOutTotal ");//出库合计

            strSql.AppendLine(" from (");
            strSql.AppendLine(" select (select top 1 c.TodayCount  from officedba.SubStorageDaily c ");
            strSql.AppendLine("	  where a.ProductID=c.ProductID and c.CompanyCD=@CompanyCD and c.DeptID=@SubDeptID ");
            strSql.AppendLine("	  and c.DailyDate=(CONVERT(CHAR(10), dateadd(dd,-1,@InitDate), 23)) ");
            //批次
            if (!string.IsNullOrEmpty(model.BatchNo))
            {
                strSql.AppendLine(" and a.BatchNo like @BatchNo ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", "%" + model.BatchNo + "%"));
            }
            strSql.AppendLine(" ) as YestodayCount,");
            strSql.AppendLine(" (a.SendInCount+a.DispInCont-a.DispOutCount-a.SendOutCount) as TodaySendCount,");
            strSql.AppendLine(" (a.SubSaleOutCount+a.SaleOutCount) as TodaySellCount,");
            strSql.AppendLine(" isnull(a.SendOutCount,0) as SendOutCount,");
            strSql.AppendLine(" isnull(a.SaleOutCount,0) as SaleOutCount,isnull(a.SubSaleOutCount,0) as SubSaleOutCount, ");
            strSql.AppendLine(" isnull(a.TodayCount,0) as TodayCount,isnull(a.SaleFee,0) as SaleFee,isnull(a.SaleBackFee,0) as SaleBackFee, ");
            strSql.AppendLine(" isnull(a.InitInCount,0) as InitInCount,isnull(a.InitBatchCount,0) as InitBatchCount,");
            strSql.AppendLine(" isnull(a.SubSaleBackInCount,0) as SubSaleBackInCount,isnull(a.InTotal,0) as InTotal,");
            strSql.AppendLine(" isnull(a.OutTotal,0) as OutTotal ");
            strSql.AppendLine(" from officedba. SubStorageDaily a ");
            strSql.AppendLine(" left join officedba.ProductInfo b on a.ProductID=b.ID ");
            strSql.AppendLine(" where a.CompanyCD=@CompanyCD and a.DeptID=@SubDeptID ");
            #endregion
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SubDeptID", model.SubDeptID));

            #region 参数过滤
            //日期
            if (!string.IsNullOrEmpty(model.InitDate))
            {
                strSql.AppendLine(" and CONVERT(CHAR(10), a.DailyDate, 23)=@InitDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@InitDate", model.InitDate));
            }
            //物品编号
            if (!string.IsNullOrEmpty(model.ProductNo))
            {
                strSql.AppendLine(" and b.ProdNo=@ProdNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProdNo", model.ProductNo));
            }
            //物品名称
            if (!string.IsNullOrEmpty(model.ProductName))
            {
                strSql.AppendLine(" and b.ProductName like @ProductName");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", "%" + model.ProductName + "%"));
            }
            //物品规格
            if (!string.IsNullOrEmpty(model.Specification))
            {
                strSql.AppendLine(" and b.Specification like @Specification");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Specification", "%" + model.Specification + "%"));
            }
            //批次
            if (!string.IsNullOrEmpty(model.BatchNo))
            {
                strSql.AppendLine(" and a.BatchNo like @BatchNo ");
                //comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", "%" + model.BatchNo + "%"));
            }
            //厂家
            if (!string.IsNullOrEmpty(model.Manufacturer))
            {
                strSql.AppendLine(" and b.Manufacturer like @Manufacturer ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Manufacturer", "%" + model.Manufacturer + "%"));
            }
            //产地
            if (!string.IsNullOrEmpty(model.FromAddr))
            {
                strSql.AppendLine(" and b.FromAddr like @FromAddr ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromAddr", "%" + model.FromAddr + "%"));
            }
            //颜色
            if (!string.IsNullOrEmpty(model.ColorID))
            {
                strSql.AppendLine(" and b.ColorID=@ColorID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ColorID", model.ColorID));
            }
            //尺寸
            if (!string.IsNullOrEmpty(model.Size))
            {
                strSql.AppendLine(" and b.Size=@Size ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Size", model.Size));
            }
            //条码
            if (!string.IsNullOrEmpty(model.BarCode))
            {
                strSql.AppendLine(" and b.BarCode=@BarCode ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BarCode", model.BarCode));
            }
            //材质
            if (!string.IsNullOrEmpty(model.MaterialID))
            {
                strSql.AppendLine(" and b.Material=@MaterialID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@MaterialID", model.MaterialID));
            }
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                if (int.Parse(EFIndex) > 0)
                {
                    strSql.AppendLine(" and b.ExtField" + EFIndex + " LIKE @EFDesc");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + EFDesc + "%"));
                }
            }
            #endregion
            strSql.AppendLine("  ) d");
            //指定命令的SQL文
            comm.CommandText = strSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 门店进销存月报表
        public static DataTable GetSubStoreMonthReport(string currentMonth, string preMonth, string CompanyCD, Hashtable htParams, string EFIndex, bool IsAll,int pageIndex,int pageSize,string OrderBy,ref int TotalCount)
        {
            StringBuilder sbSql = new StringBuilder();
            string extStr = string.Empty;
            if (!string.IsNullOrEmpty(EFIndex) && htParams.ContainsKey("EFDesc"))
            {
                extStr = "c.ExtField" + EFIndex+",";
            }


            #region SQL
            if (IsAll)
            {
                sbSql.AppendLine(" SELECT " + extStr + " SUM(ISNULL(TodayCount,0)) AS CurrentMonthCount ");
                sbSql.AppendLine(" ,SUM(ISNULL(SendInCount,0)) AS CurrentMonthSendInCount ");
                sbSql.AppendLine(" ,SUM(ISNULL(SubSaleOutCount,0)) AS CurrentMonthSaleCount  ");
                sbSql.AppendLine(" ,SUM(ISNULL(SaleFee,0)) AS CurrentMonthSaleFee ");
                sbSql.AppendLine(" ,SUM(ISNULL(SaleBackFee,0)) AS CurrentMonthSaleBackFee, ");
                sbSql.AppendLine(" ( ");
                sbSql.AppendLine(" SELECT ISNULL(SUM(TodayCount),0)  ");
                sbSql.AppendLine(" FROM officedba.SubStorageDaily ");
                sbSql.AppendLine(" WHERE CONVERT(varchar(7),DailyDate,126)=@PreDailyDate AND CompanyCD=@CompanyCD AND DeptID=@DeptID");
                sbSql.AppendLine(" )   AS PreMonthCount --上月结存 ");
                sbSql.AppendLine(" FROM officedba.SubStorageDaily  ");
                sbSql.AppendLine(" WHERE CONVERT(varchar(7),DailyDate,126)=@CurrentDailyDate AND CompanyCD=@CompanyCD AND DeptID=@DeptID ");
            }
            else
            {
                sbSql.AppendLine("SELECT  " + extStr + " A.*,ISNULL(B.PreMonthCount,0) AS PreMonthCount , ");
                sbSql.AppendLine("c.ProductName,c.Specification,c.ProdNo ");
                sbSql.AppendLine("FROM  ");
                sbSql.AppendLine("(SELECT ISNULL(SUM(ISNULL(TodayCount,0)),0) AS CurrentMonthCount ");
                sbSql.AppendLine(",ISNULL(SUM(ISNULL(SendInCount,0)),0) AS CurrentMonthSendInCount ");
                sbSql.AppendLine(",ISNULL(SUM(ISNULL(SubSaleOutCount,0)),0) AS CurrentMonthSaleCount  ");
                sbSql.AppendLine(",ISNULL(SUM(ISNULL(SaleFee,0)),0) AS CurrentMonthSaleFee ");
                sbSql.AppendLine(",ISNULL(SUM(ISNULL(SaleBackFee,0)),0) AS CurrentMonthSaleBackFee ");
                sbSql.AppendLine(",ProductID,BatchNo ");
                sbSql.AppendLine("FROM officedba.SubStorageDaily  ");
                sbSql.AppendLine("WHERE CONVERT(varchar(7),DailyDate,126)=@CurrentDailyDate AND CompanyCD=@CompanyCD AND DeptID=@DeptID");
                sbSql.AppendLine("GROUP BY ProductID,BatchNo) AS a ");
                sbSql.AppendLine("LEFT JOIN  ");
                sbSql.AppendLine("( ");
                sbSql.AppendLine("SELECT SUM(ISNULL(TodayCount,0)) AS PreMonthCount,ProductID ,BatchNo ");
                sbSql.AppendLine("FROM officedba.SubStorageDaily ");
                sbSql.AppendLine("WHERE CONVERT(varchar(7),DailyDate,126)=@PreDailyDate AND CompanyCD=@CompanyCD AND DeptID=@DeptID ");
                sbSql.AppendLine("GROUP BY ProductID,BatchNo ");
                sbSql.AppendLine(")  AS b ON a.ProductID=b.ProductID AND a.BatchNo=b.BatchNo ");
                sbSql.AppendLine("LEFT JOIN officedba.ProductInfo as c ON a.ProductID=c.ID");
                sbSql.AppendLine("WHERE 1=1   ");
            }
            #endregion

            #region 参数
            SqlParameter[] sqlParams = new SqlParameter[htParams.Count + 3];
            int index = 0;
            sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            sqlParams[index++] = SqlHelper.GetParameter("@PreDailyDate", preMonth);
            sqlParams[index++] = SqlHelper.GetParameter("@CurrentDailyDate", currentMonth);

            if (htParams.ContainsKey("ProductID"))
            {
                sbSql.AppendLine(" AND c.ID=@ProductID ");
                sqlParams[index++] = SqlHelper.GetParameter("@ProductID", htParams["ProductID"].ToString());
            }
            if (htParams.ContainsKey("ProductName"))
            {
                sbSql.AppendLine(" AND c.ProductName LIKE @ProductName");
                sqlParams[index++] = SqlHelper.GetParameter("@ProductName", htParams["ProductName"].ToString());
            }
            if (htParams.ContainsKey("Specification"))
            {
                sbSql.AppendLine(" AND c.Specification LIKE @Specification");
                sqlParams[index++] = SqlHelper.GetParameter("@Specification", htParams["Specification"].ToString());
            }
            if (htParams.ContainsKey("BatchNo"))
            {
                sbSql.AppendLine(" AND a.BatchNo LIKE @BatchNo");
                sqlParams[index++] = SqlHelper.GetParameter("@BatchNo", htParams["BatchNo"].ToString());
            }
            if (htParams.ContainsKey("Manufacturer"))
            {
                sbSql.AppendLine("  AND c.Manufacturer LIKE @Manufacturer");
                sqlParams[index++] = SqlHelper.GetParameter("@Manufacturer", htParams["Manufacturer"].ToString());
            }
            if (htParams.ContainsKey("FromAddr"))
            {
                sbSql.AppendLine(" AND c.FromAddr LIKE @FromAddr");
                sqlParams[index++] = SqlHelper.GetParameter("@FromAddr", htParams["FromAddr"].ToString());
            }
            if (htParams.ContainsKey("ColorID"))
            {
                sbSql.AppendLine(" AND c.ColorID=@ColorID");
                sqlParams[index++] = SqlHelper.GetParameter("@ColorID", htParams["ColorID"].ToString());
            }
            if (htParams.ContainsKey("Size"))
            {
                sbSql.AppendLine(" AND c.Size LIKE @Size ");
                sqlParams[index++] = SqlHelper.GetParameter("@Size", htParams["Size"].ToString());
            }
            if (htParams.ContainsKey("BarCode"))
            {
                sbSql.AppendLine(" AND c.BarCode LIKE @BarCode");
                sqlParams[index++] = SqlHelper.GetParameter("@BarCode", htParams["BarCode"].ToString());
            }
            if (htParams.ContainsKey("MaterialID"))
            {
                sbSql.AppendLine(" AND c.Material=@MaterialID ");
                sqlParams[index++] = SqlHelper.GetParameter("@MaterialID", htParams["MaterialID"].ToString());
            }

            if (htParams.ContainsKey("DeptID"))
            {
                sqlParams[index++] = SqlHelper.GetParameter("@DeptID", htParams["DeptID"].ToString());
            }

            if (!string.IsNullOrEmpty(EFIndex) && htParams.ContainsKey("EFDesc"))
            {
                if (int.Parse(EFIndex) > 0)
                {
                    sbSql.AppendLine(" AND c.ExtField" + EFIndex + " LIKE @EFDesc");
                    sqlParams[index++] = SqlHelper.GetParameterFromString("@EFDesc", htParams["EFDesc"].ToString());
                }
            }

            #endregion

            return SqlHelper.CreateSqlByPageExcuteSql(sbSql.ToString(), pageIndex, pageSize, OrderBy, sqlParams, ref TotalCount);

        }

        public static DataTable GetSubStoreMonthReport(string currentMonth, string preMonth, string CompanyCD, Hashtable htParams, string EFIndex, bool IsAll,string OrderBy)
        {
            StringBuilder sbSql = new StringBuilder();

            string extStr = string.Empty;
            if (!string.IsNullOrEmpty(EFIndex) && htParams.ContainsKey("EFDesc"))
            {
                extStr = "c.ExtField" + EFIndex + ",";
            }


            #region SQL
            if (IsAll)
            {
                sbSql.AppendLine(" SELECT " + extStr + "  CONVERT(VARCHAR(7),'" + currentMonth + "') AS CurrentMonth,SUM(ISNULL(TodayCount,0)) AS CurrentMonthCount ");
                sbSql.AppendLine(" ,SUM(ISNULL(SendInCount,0)) AS CurrentMonthSendInCount ");
                sbSql.AppendLine(" ,SUM(ISNULL(SubSaleOutCount,0)) AS CurrentMonthSaleCount  ");
                sbSql.AppendLine(" ,SUM(ISNULL(SaleFee,0)) AS CurrentMonthSaleFee ");
                sbSql.AppendLine(" ,SUM(ISNULL(SaleBackFee,0)) AS CurrentMonthSaleBackFee, ");
                sbSql.AppendLine(" ( ");
                sbSql.AppendLine(" SELECT ISNULL(SUM(TodayCount),0)  ");
                sbSql.AppendLine(" FROM officedba.SubStorageDaily ");
                sbSql.AppendLine(" WHERE CONVERT(varchar(7),DailyDate,126)=@PreDailyDate AND CompanyCD=@CompanyCD AND DeptID=@DeptID");
                sbSql.AppendLine(" )   AS PreMonthCount --上月结存 ");
                sbSql.AppendLine(" FROM officedba.SubStorageDaily  ");
                sbSql.AppendLine(" WHERE CONVERT(varchar(7),DailyDate,126)=@CurrentDailyDate AND CompanyCD=@CompanyCD AND DeptID=@DeptID ");
            }
            else
            {
                sbSql.AppendLine("SELECT " + extStr + " A.*, CONVERT(VARCHAR(7),'" + currentMonth + "') AS CurrentMonth, ISNULL(B.PreMonthCount,0) AS PreMonthCount , ");
                sbSql.AppendLine("c.ProductName,c.Specification,c.ProdNo ");
                sbSql.AppendLine("FROM  ");
                sbSql.AppendLine("(SELECT ISNULL(SUM(ISNULL(TodayCount,0)),0) AS CurrentMonthCount ");
                sbSql.AppendLine(",ISNULL(SUM(ISNULL(SendInCount,0)),0) AS CurrentMonthSendInCount ");
                sbSql.AppendLine(",ISNULL(SUM(ISNULL(SubSaleOutCount,0)),0) AS CurrentMonthSaleCount  ");
                sbSql.AppendLine(",ISNULL(SUM(ISNULL(SaleFee,0)),0) AS CurrentMonthSaleFee ");
                sbSql.AppendLine(",ISNULL(SUM(ISNULL(SaleBackFee,0)),0) AS CurrentMonthSaleBackFee ");
                sbSql.AppendLine(",ProductID,BatchNo ");
                sbSql.AppendLine("FROM officedba.SubStorageDaily  ");
                sbSql.AppendLine("WHERE CONVERT(varchar(7),DailyDate,126)=@CurrentDailyDate AND CompanyCD=@CompanyCD AND DeptID=@DeptID");
                sbSql.AppendLine("GROUP BY ProductID,BatchNo) AS a ");
                sbSql.AppendLine("LEFT JOIN  ");
                sbSql.AppendLine("( ");
                sbSql.AppendLine("SELECT SUM(ISNULL(TodayCount,0)) AS PreMonthCount,ProductID ,BatchNo ");
                sbSql.AppendLine("FROM officedba.SubStorageDaily ");
                sbSql.AppendLine("WHERE CONVERT(varchar(7),DailyDate,126)=@PreDailyDate AND CompanyCD=@CompanyCD AND DeptID=@DeptID ");
                sbSql.AppendLine("GROUP BY ProductID,BatchNo ");
                sbSql.AppendLine(")  AS b ON a.ProductID=b.ProductID AND a.BatchNo=b.BatchNo ");
                sbSql.AppendLine("LEFT JOIN officedba.ProductInfo as c ON a.ProductID=c.ID");
                sbSql.AppendLine("WHERE 1=1   ");
            }
            #endregion

            #region 参数
            SqlParameter[] sqlParams = new SqlParameter[htParams.Count + 3];
            int index = 0;
            sqlParams[index++] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            sqlParams[index++] = SqlHelper.GetParameter("@PreDailyDate", preMonth);
            sqlParams[index++] = SqlHelper.GetParameter("@CurrentDailyDate", currentMonth);

            if (htParams.ContainsKey("ProductID"))
            {
                sbSql.AppendLine(" AND c.ID=@ProductID ");
                sqlParams[index++] = SqlHelper.GetParameter("@ProductID", htParams["ProductID"].ToString());
            }
            if (htParams.ContainsKey("ProductName"))
            {
                sbSql.AppendLine(" AND c.ProductName LIKE @ProductName");
                sqlParams[index++] = SqlHelper.GetParameter("@ProductName", htParams["ProductName"].ToString());
            }
            if (htParams.ContainsKey("Specification"))
            {
                sbSql.AppendLine(" AND c.Specification LIKE @Specification");
                sqlParams[index++] = SqlHelper.GetParameter("@Specification", htParams["Specification"].ToString());
            }
            if (htParams.ContainsKey("BatchNo"))
            {
                sbSql.AppendLine(" AND a.BatchNo LIKE @BatchNo");
                sqlParams[index++] = SqlHelper.GetParameter("@BatchNo", htParams["BatchNo"].ToString());
            }
            if (htParams.ContainsKey("Manufacturer"))
            {
                sbSql.AppendLine("  AND c.Manufacturer LIKE @Manufacturer");
                sqlParams[index++] = SqlHelper.GetParameter("@Manufacturer", htParams["Manufacturer"].ToString());
            }
            if (htParams.ContainsKey("FromAddr"))
            {
                sbSql.AppendLine(" AND c.FromAddr LIKE @FromAddr");
                sqlParams[index++] = SqlHelper.GetParameter("@FromAddr", htParams["FromAddr"].ToString());
            }
            if (htParams.ContainsKey("ColorID"))
            {
                sbSql.AppendLine(" AND c.ColorID=@ColorID");
                sqlParams[index++] = SqlHelper.GetParameter("@ColorID", htParams["ColorID"].ToString());
            }
            if (htParams.ContainsKey("Size"))
            {
                sbSql.AppendLine(" AND c.Size LIKE @Size ");
                sqlParams[index++] = SqlHelper.GetParameter("@Size", htParams["Size"].ToString());
            }
            if (htParams.ContainsKey("BarCode"))
            {
                sbSql.AppendLine(" AND c.BarCode LIKE @BarCode");
                sqlParams[index++] = SqlHelper.GetParameter("@BarCode", htParams["BarCode"].ToString());
            }
            if (htParams.ContainsKey("MaterialID"))
            {
                sbSql.AppendLine(" AND c.Material=@MaterialID ");
                sqlParams[index++] = SqlHelper.GetParameter("@MaterialID", htParams["MaterialID"].ToString());
            }

            if (htParams.ContainsKey("DeptID"))
            {
                sqlParams[index++] = SqlHelper.GetParameter("@DeptID", htParams["DeptID"].ToString());
            }

            if (!string.IsNullOrEmpty(EFIndex) && htParams.ContainsKey("EFDesc"))
            {
                if (int.Parse(EFIndex) > 0)
                {
                    sbSql.AppendLine(" AND c.ExtField" + EFIndex + " LIKE @EFDesc");
                    sqlParams[index++] = SqlHelper.GetParameterFromString("@EFDesc", htParams["EFDesc"].ToString());
                }
            }

            #endregion


            sbSql.AppendLine(" ORDER BY " + OrderBy);
            return SqlHelper.ExecuteSql(sbSql.ToString(), sqlParams);

        }

        #endregion

        #region 门店进销存月报表(分店报表 add by ellen)
        public static DataTable GetSubStoreMonthReportList(bool isAll,Hashtable htParams, string CompanyCD, string StartDailyDate, string EndDailyDate, string EFIndex, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            StringBuilder searchSql = new StringBuilder();
            if (!isAll)
            {

                #region 查询语句
                //查询SQL拼写

                searchSql.AppendLine("select	SUBSTRING(CONVERT(VARCHAR ,a.DailyDate ,120) ,0 ,11) AS DailyDate,c.ProductName,c.Specification,a.BatchNo,");
                searchSql.AppendLine("		c.ExtField1,c.ExtField2,c.ExtField3,c.ExtField4,c.ExtField5,");
                searchSql.AppendLine("		c.ExtField6,c.ExtField7,c.ExtField8,c.ExtField9,c.ExtField10,");
                searchSql.AppendLine("		Convert(numeric(22," + userInfo.SelPoint + "),isnull(a.TodayCount,0)) as TodayCount,");
                searchSql.AppendLine("		Convert(numeric(22," + userInfo.SelPoint + "),isnull(a.SaleFee,0)) as SaleFee,");
                searchSql.AppendLine("		Convert(numeric(22," + userInfo.SelPoint + "),isnull(a.SaleBackFee,0)) as SaleBackFee,");
                searchSql.AppendLine("		--收入部分");
                searchSql.AppendLine("		Convert(numeric(22," + userInfo.SelPoint + "),isnull((select top 1 b.TodayCount  from officedba.SubStorageDaily b where a.ProductID=b.ProductID");
                searchSql.AppendLine("			and b.DailyDate>=(CONVERT(CHAR(10), dateadd(dd,-1,a.DailyDate), 23))");
                searchSql.AppendLine("			and b.DailyDate<CONVERT(CHAR(10), a.DailyDate, 23)),0)) as YestodayCount,");
                searchSql.AppendLine("		Convert(numeric(22," + userInfo.SelPoint + "),isnull(a.InitInCount,0)) as InitInCount,");
                searchSql.AppendLine("		Convert(numeric(22," + userInfo.SelPoint + "),isnull(a.InitBatchCount,0)) as InitBatchCount,");
                searchSql.AppendLine("		Convert(numeric(22," + userInfo.SelPoint + "),isnull(a.SendInCount,0)) as SendInCount,");
                searchSql.AppendLine("		Convert(numeric(22," + userInfo.SelPoint + "),isnull(a.SubSaleBackInCount,0)) as SubSaleBackInCount,");
                searchSql.AppendLine("		Convert(numeric(22," + userInfo.SelPoint + "),isnull(a.DispInCont,0)) as DispInCont,");
                searchSql.AppendLine("		Convert(numeric(22," + userInfo.SelPoint + "),isnull(a.InTotal,0)) as InTotal,");
                searchSql.AppendLine("		--支出部分");
                searchSql.AppendLine("		Convert(numeric(22," + userInfo.SelPoint + "),isnull(a.SubSaleOutCount,0)) as SubSaleOutCount,");
                searchSql.AppendLine("		Convert(numeric(22," + userInfo.SelPoint + "),isnull(a.DispOutCount,0)) as DispOutCount,");
                searchSql.AppendLine("		Convert(numeric(22," + userInfo.SelPoint + "),isnull(a.SendOutCount,0)) as SendOutCount,");
                searchSql.AppendLine("		Convert(numeric(22," + userInfo.SelPoint + "),isnull(a.OutTotal,0)) as OutTotal");
                searchSql.AppendLine("from officedba.SubStorageDaily a ");
                searchSql.AppendLine("left join officedba.ProductInfo c on a.ProductID=c.ID");
                searchSql.AppendLine("where a.CompanyCD=@CompanyCD and a.DeptID=@DeptID");
                searchSql.AppendLine("and CONVERT(CHAR(10), a.DailyDate, 23)>=@StartDailyDate");
                searchSql.AppendLine("and CONVERT(CHAR(10), a.DailyDate, 23)<=@EndDailyDate");



                #endregion

                #region 参数
                //定义查询的命令
                SqlCommand comm = new SqlCommand();
                //添加公司代码参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", htParams["DeptID"].ToString()));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDailyDate", StartDailyDate));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDailyDate", EndDailyDate));


                //物品编号
                if (htParams.ContainsKey("ProductID"))
                {
                    searchSql.AppendLine("  and a.ProductID=@ProductID");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", htParams["ProductID"].ToString()));
                }
                //物品名称
                if (htParams.ContainsKey("ProductName"))
                {
                    searchSql.AppendLine("  and c.ProductName LIKE @ProductName");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", htParams["ProductName"].ToString()));
                }
                //物品规格
                if (htParams.ContainsKey("Specification"))
                {
                    searchSql.AppendLine("  and c.Specification LIKE @Specification");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Specification", htParams["Specification"].ToString()));
                }
                //批次
                if (htParams.ContainsKey("BatchNo"))
                {
                    searchSql.AppendLine(" and a.BatchNo LIKE @BatchNo");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", htParams["BatchNo"].ToString()));
                }
                //厂家
                if (htParams.ContainsKey("Manufacturer"))
                {
                    searchSql.AppendLine("  and c.Manufacturer LIKE @Manufacturer");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Manufacturer", htParams["Manufacturer"].ToString()));
                }
                //产地
                if (htParams.ContainsKey("Manufacturer"))
                {
                    searchSql.AppendLine(" and c.FromAddr LIKE @FromAddr");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromAddr", htParams["Manufacturer"].ToString()));
                }
                //颜色
                if (htParams.ContainsKey("ColorID"))
                {
                    searchSql.AppendLine("and c.ColorID=@ColorID");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ColorID", htParams["ColorID"].ToString()));
                }
                //大小
                if (htParams.ContainsKey("Size"))
                {
                    searchSql.AppendLine(" and c.Size LIKE @Size");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Size", htParams["Size"].ToString()));
                }
                //条码
                if (htParams.ContainsKey("BarCode"))
                {
                    searchSql.AppendLine(" and c.BarCode LIKE @BarCode");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@BarCode", htParams["BarCode"].ToString()));
                }
                //材质
                if (htParams.ContainsKey("MaterialID"))
                {
                    searchSql.AppendLine(" and c.Material LIKE @MaterialID");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@MaterialID", htParams["MaterialID"].ToString()));
                }
                //其他条件
                if (!string.IsNullOrEmpty(EFIndex) && htParams.ContainsKey("EFDesc"))
                {
                    if (int.Parse(EFIndex) > 0)
                    {
                        searchSql.AppendLine(" AND c.ExtField" + EFIndex + " LIKE @EFDesc");
                        comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", htParams["EFDesc"].ToString()));
                    }
                }


                //指定命令的SQL文
                comm.CommandText = searchSql.ToString();
                #endregion
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
            }
            else
            {
                #region 查询语句
                //查询SQL拼写

                searchSql.AppendLine("SELECT			   SUM(ISNULL(ssd.InTotal,0)) AS InTotal");
                searchSql.AppendLine("                    ,SUM(ISNULL(ssd.OutTotal,0)) AS OutTotal");
                searchSql.AppendLine("                    ,SUM(ISNULL(ssd.SaleFee,0)) AS SaleFee");
                searchSql.AppendLine("                    ,SUM(ISNULL(ssd.SaleBackFee,0)) AS SaleBackFee");
                searchSql.AppendLine("		              ,SUBSTRING(CONVERT(VARCHAR ,ssd.DailyDate ,120) ,0 ,11) AS DailyDate");
                searchSql.AppendLine("                    ,ISNULL((SELECT TOP(1)  SUM(ISNULL(ssd1.TodayCount,0)) AS YestodayCount");
                searchSql.AppendLine("					FROM officedba.SubStorageDaily ssd1");
                searchSql.AppendLine("					LEFT JOIN officedba.ProductInfo pi1 ON ssd1.ProductID=pi1.ID");
                searchSql.AppendLine("					WHERE ssd1.DailyDate=DATEADD(DAY,-1,ssd.DailyDate) AND ssd1.DeptID=ssd.DeptID");
                searchSql.AppendLine("					AND ssd1.CompanyCD=@CompanyCD");
                searchSql.AppendLine("					GROUP BY ssd1.DailyDate,ssd1.DeptID");
                searchSql.AppendLine("),0) AS YestodayCount");
                searchSql.AppendLine("                                  ,SUM(ISNULL(ssd.TodayCount,0)) AS TodayCount");
                searchSql.AppendLine("                                  ,SUM(ISNULL(ssd.InitInCount,0)) AS InitInCount");
                searchSql.AppendLine("                                  ,SUM(ISNULL(ssd.InitBatchCount,0)) AS InitBatchCount");
                searchSql.AppendLine("                                  ,SUM(ISNULL(ssd.SendInCount,0)) AS SendInCount");
                searchSql.AppendLine("                                  ,SUM(ISNULL(ssd.SubSaleBackInCount,0)) AS SubSaleBackInCount");
                searchSql.AppendLine("                                  ,SUM(ISNULL(ssd.DispInCont,0)) AS DispInCont");
                searchSql.AppendLine("                                  ,SUM(ISNULL(ssd.SubSaleOutCount,0)) AS SubSaleOutCount");
                searchSql.AppendLine("                                  ,SUM(ISNULL(ssd.SendOutCount,0)) AS SendOutCount");
                searchSql.AppendLine("                                  ,SUM(ISNULL(ssd.DispOutCount,0)) AS DispOutCount");
                searchSql.AppendLine("FROM officedba.SubStorageDaily ssd");
                searchSql.AppendLine("                            LEFT JOIN officedba.ProductInfo pi1 ON ssd.ProductID=pi1.ID ");
                searchSql.AppendLine(" WHERE ssd.CompanyCD=@CompanyCD and ssd.DeptID=@DeptID");
                searchSql.AppendLine(" AND DATEDIFF(DAY,ssd.DailyDate,@StartDailyDate)<=0 ");
                searchSql.AppendLine(" AND DATEDIFF(DAY,ssd.DailyDate,@EndDailyDate)>=0");
                searchSql.AppendLine("GROUP BY ssd.DailyDate,ssd.DeptID");

                #endregion

                #region 参数
                SqlCommand comm = new SqlCommand();
                //添加公司代码参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", htParams["DeptID"].ToString()));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDailyDate", StartDailyDate));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDailyDate", EndDailyDate));
                comm.CommandText = searchSql.ToString();
                #endregion

                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
            }

            //执行查询
            
        }
        #endregion

        #region 门店进销存月报表合计(分店报表 add by ellen)
        public static DataTable GetSubStoreMonthReportSum(bool isAll, Hashtable htParams, string CompanyCD, string StartDailyDate, string EndDailyDate, string EFIndex, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            StringBuilder searchSql = new StringBuilder();
            if (!isAll)
            {

                #region 查询语句
                //查询SQL拼写

                searchSql.AppendLine("select");
                searchSql.AppendLine("  Convert(numeric(22,"+userInfo.SelPoint+"),isnull(sum(a.SaleFee),0)) as SumSaleFee,");
                searchSql.AppendLine("  Convert(numeric(22," + userInfo.SelPoint + "),isnull(sum(a.SaleBackFee),0)) as SumSaleBackFee,");
                searchSql.AppendLine("  Convert(numeric(22,"+userInfo.SelPoint+"),isnull(sum(a.InTotal),0)) as SumInTotal,");
                searchSql.AppendLine("  Convert(numeric(22,"+userInfo.SelPoint+"),isnull(sum(a.OutTotal),0)) as SumOutTotal");
                searchSql.AppendLine("from officedba.SubStorageDaily a ");
                searchSql.AppendLine("left join officedba.ProductInfo c on a.ProductID=c.ID");
                searchSql.AppendLine("where a.CompanyCD=@CompanyCD and a.DeptID=@DeptID");
                searchSql.AppendLine("and CONVERT(CHAR(10), a.DailyDate, 23)>=@StartDailyDate");
                searchSql.AppendLine("and CONVERT(CHAR(10), a.DailyDate, 23)<=@EndDailyDate");



                #endregion

                #region 参数
                //定义查询的命令
                SqlCommand comm = new SqlCommand();
                //添加公司代码参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", htParams["DeptID"].ToString()));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDailyDate", StartDailyDate));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDailyDate", EndDailyDate));


                //物品编号
                if (htParams.ContainsKey("ProductID"))
                {
                    searchSql.AppendLine("  and a.ProductID=@ProductID");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", htParams["ProductID"].ToString()));
                }
                //物品名称
                if (htParams.ContainsKey("ProductName"))
                {
                    searchSql.AppendLine("  and c.ProductName LIKE @ProductName");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", htParams["ProductName"].ToString()));
                }
                //物品规格
                if (htParams.ContainsKey("Specification"))
                {
                    searchSql.AppendLine("  and c.Specification LIKE @Specification");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Specification", htParams["Specification"].ToString()));
                }
                //批次
                if (htParams.ContainsKey("BatchNo"))
                {
                    searchSql.AppendLine(" and a.BatchNo LIKE @BatchNo");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", htParams["BatchNo"].ToString()));
                }
                //厂家
                if (htParams.ContainsKey("Manufacturer"))
                {
                    searchSql.AppendLine("  and c.Manufacturer LIKE @Manufacturer");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Manufacturer", htParams["Manufacturer"].ToString()));
                }
                //产地
                if (htParams.ContainsKey("Manufacturer"))
                {
                    searchSql.AppendLine(" and c.FromAddr LIKE @FromAddr");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromAddr", htParams["Manufacturer"].ToString()));
                }
                //颜色
                if (htParams.ContainsKey("ColorID"))
                {
                    searchSql.AppendLine("and c.ColorID=@ColorID");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ColorID", htParams["ColorID"].ToString()));
                }
                //大小
                if (htParams.ContainsKey("Size"))
                {
                    searchSql.AppendLine(" and c.Size LIKE @Size");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Size", htParams["Size"].ToString()));
                }
                //条码
                if (htParams.ContainsKey("BarCode"))
                {
                    searchSql.AppendLine(" and c.BarCode LIKE @BarCode");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@BarCode", htParams["BarCode"].ToString()));
                }
                //材质
                if (htParams.ContainsKey("MaterialID"))
                {
                    searchSql.AppendLine(" and c.Material LIKE @MaterialID");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@MaterialID", htParams["MaterialID"].ToString()));
                }
                //其他条件
                if (!string.IsNullOrEmpty(EFIndex) && htParams.ContainsKey("EFDesc"))
                {
                    if (int.Parse(EFIndex) > 0)
                    {
                        searchSql.AppendLine(" AND c.ExtField" + EFIndex + " LIKE @EFDesc");
                        comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", htParams["EFDesc"].ToString()));
                    }
                }


                //指定命令的SQL文
                comm.CommandText = searchSql.ToString();
                #endregion
                return SqlHelper.ExecuteSearch(comm);
            }
            else
            {
                #region 查询语句
                //查询SQL拼写
                searchSql.AppendLine("SELECT				Convert(numeric(22,"+userInfo.SelPoint+"),isnull(sum(ssd.InTotal),0)) as SumInTotal");
                searchSql.AppendLine("                    ,Convert(numeric(22,"+userInfo.SelPoint+"),isnull(sum(ssd.OutTotal),0)) as SumOutTotal");
                searchSql.AppendLine("                    ,Convert(numeric(22,"+userInfo.SelPoint+"),isnull(sum(ssd.SaleFee),0)) as SumSaleFee");
                searchSql.AppendLine("                    ,Convert(numeric(22,"+userInfo.SelPoint+"),isnull(sum(ssd.SaleBackFee),0)) as SumSaleBackFee");
                searchSql.AppendLine("                    FROM officedba.SubStorageDaily ssd");
                searchSql.AppendLine(" WHERE ssd.CompanyCD=@CompanyCD and ssd.DeptID=@DeptID");
                searchSql.AppendLine(" AND DATEDIFF(DAY,ssd.DailyDate,@StartDailyDate)<=0 ");
                searchSql.AppendLine(" AND DATEDIFF(DAY,ssd.DailyDate,@EndDailyDate)>=0");

                #endregion

                #region 参数
                SqlCommand comm = new SqlCommand();
                //添加公司代码参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", htParams["DeptID"].ToString()));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDailyDate", StartDailyDate));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDailyDate", EndDailyDate));
                comm.CommandText = searchSql.ToString();
                #endregion

                return SqlHelper.ExecuteSearch(comm);
            }

            //执行查询

        }
        #endregion

    }
}

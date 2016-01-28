/**********************************************
 * 类作用：   月报表处理
 * 建立人：   王保军
 * 建立时间： 2010/05/08
 ***********************************************/


using System;
using XBase.Model.Office.StorageManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using XBase.Common;
using XBase.Model.Office.SupplyChain;

namespace XBase.Data.OperatingModel.IntegratedData
{
   public   class MonthDayDBHelper
    {
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

        #region 进销存汇总表（全部）
        /// <summary>
        /// 进销存汇总表
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
        public static DataTable GetAllStorageInAndOutInfo(ProductInfoModel model, string DailyDate, string EndDate, string BatchNo, string EFIndex, string EFDesc, int pageIndex, int pageCount, string OrderBy, out DataTable dt, ref int totalCount)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT	CONVERT(varchar(10),a.DailyDate,120) as DailyDate,");
          
           searchSql.AppendLine("  isnull((");
searchSql.AppendLine(" select  SUM(b.TodayCount) from officedba.StorageDaily b");
searchSql.AppendLine(" where   b.Companycd='"+userInfo .CompanyCD +"' ");
searchSql.AppendLine(" and   CONVERT(CHAR(10), b.DailyDate, 23)=");
searchSql.AppendLine(" CONVERT(CHAR(10), dateadd(dd,-1,CONVERT(varchar(10),a.DailyDate,120)), 23) )  ,0 ) as YestodayCount,");

            searchSql.AppendLine("		SUM(Convert(numeric(22,2),a.PhurInCount)) as PhurInCount,SUM(Convert(numeric(22,2),a.MakeInCount)) as MakeInCount,");
            searchSql.AppendLine("		SUM(Convert(numeric(22,2),a.DispInCount)) as DispInCount,");
            searchSql.AppendLine("		SUM(Convert(numeric(22,2),a.OtherInCount)) as OtherInCount,SUM(Convert(numeric(22,2),a.SendInCount)) as SendInCount,SUM(Convert(numeric(22,2),a.SubSaleBackInCount)) as SubSaleBackInCount,");
            searchSql.AppendLine("		SUM(Convert(numeric(22,2),a.TakeInCount)) as TakeInCount,SUM(Convert(numeric(22,2),a.InTotal)) as InTotal,SUM(Convert(numeric(22,2),a.SaleFee)) as SaleFee,SUM(Convert(numeric(22,2),a.PhurBackFee)) as PhurBackFee,");
            searchSql.AppendLine("		SUM(Convert(numeric(22,2),a.InitInCount)) as InitInCount,SUM(Convert(numeric(22,2),a.InitBatchCount)) as InitBatchCount,SUM(Convert(numeric(22,2),a.SaleBackInCount)) as SaleBackInCount,");
            searchSql.AppendLine("		SUM(Convert(numeric(22,2),a.RedInCount)) as RedInCount,SUM(Convert(numeric(22,2),a.BackInCount)) as BackInCount,");
            searchSql.AppendLine("		");
            searchSql.AppendLine("		SUM(Convert(numeric(22,2),a.SaleOutCount)) as SaleOutCount,SUM(Convert(numeric(22,2),a.TakeOutCount)) as TakeOutCount,");
            searchSql.AppendLine("		SUM(Convert(numeric(22,2),a.DispOutCount)) as DispOutCount,SUM(Convert(numeric(22,2),a.BadCount)) as BadCount,");
            searchSql.AppendLine("		SUM(Convert(numeric(22,2),a.OtherOutCount)) as OtherOutCount,SUM(Convert(numeric(22,2),a.SendOutCount)) as SendOutCount,SUM(Convert(numeric(22,2),a.SubSaleOutCount)) as SubSaleOutCount,");
            searchSql.AppendLine("		SUM(Convert(numeric(22,2),a.OutTotal)) as OutTotal,SUM(Convert(numeric(22,2),a.PhurFee)) as PhurFee,SUM(Convert(numeric(22,2),a.SaleBackFee)) as SaleBackFee,");
            searchSql.AppendLine("		SUM(Convert(numeric(22,2),a.PhurBackOutCount)) as PhurBackOutCount,SUM(Convert(numeric(22,2),a.RedOutCount)) as RedOutCount,SUM(Convert(numeric(22,2),a.LendCount)) as LendCount,");
            searchSql.AppendLine("		SUM(Convert(numeric(22,2),ABS(a.CheckCount))) as CheckCount,SUM(Convert(numeric(22,2),ABS(a.AdjustCount))) as AdjustCount,");
            searchSql.AppendLine("		SUM(Convert(numeric(22,2),ABS(a.TodayCount))) as TodayCount ");
            searchSql.AppendLine("FROM	officedba.StorageDaily a ");
            searchSql.AppendLine("where a.CompanyCD=@CompanyCD ");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            //开始日期
            if (!string.IsNullOrEmpty(DailyDate))
            {
                searchSql.AppendLine(" and a.DailyDate>=@DailyDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DailyDate", DailyDate));
            }
            //结束日期
            if (!string.IsNullOrEmpty(EndDate))
            {
                searchSql.AppendLine(" and a.DailyDate<=@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", EndDate));
            }
            searchSql.AppendLine(" GROUP BY CONVERT(varchar(10),a.DailyDate,120)");
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            dt = GetAllTotal(model, DailyDate, EndDate, BatchNo, EFIndex, EFDesc, false);
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #region 进销存汇总 总计
        /// <summary>
        /// 进销存汇总 总计
        /// </summary>
        /// <param name="model"></param>
        /// <param name="DailyDate"></param>
        /// <param name="BatchNo"></param>
        /// <param name="EFIndex"></param>
        /// <param name="EFDesc"></param>
        /// <returns></returns>
        public static DataTable GetAllTotal(ProductInfoModel model, string DailyDate, string EndDate, string BatchNo, string EFIndex, string EFDesc, bool flag)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT	sum(InTotal) as InTotalCount,sum(OutTotal) as outTotalCount,sum(SaleFee) as SaleFeeCount,");
            searchSql.AppendLine("		sum(PhurFee) as PhurFeeCount,sum(PhurBackFee) as PhurBackFeeCount,sum(SaleBackFee) as SaleBackFeeCount");
            searchSql.AppendLine("FROM	officedba.StorageDaily a left join officedba.ProductInfo b on a.ProductID=b.ID and a.CompanyCD=b.CompanyCD ");
            searchSql.AppendLine("where a.CompanyCD=@CompanyCD");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));

            //开始日期
            if (!string.IsNullOrEmpty(DailyDate))
            {
                searchSql.AppendLine(" and a.DailyDate>=@DailyDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DailyDate", DailyDate));
            }
            //结束日期
            if (!string.IsNullOrEmpty(EndDate))
            {
                searchSql.AppendLine(" and a.DailyDate<=@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", EndDate));
            }
            if (flag)
            {
                //物品编号
                if (!string.IsNullOrEmpty(model.ProdNo))
                {
                    searchSql.AppendLine(" and b.ProdNo=@ProdNo");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProdNo", model.ProdNo));
                }
                //物品名称
                if (!string.IsNullOrEmpty(model.ProductName))
                {
                    searchSql.AppendLine("	and b.ProductName like  '%'+ @ProductName + '%' ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", model.ProductName));
                }
                //物品规格
                if (!string.IsNullOrEmpty(model.Specification))
                {
                    searchSql.AppendLine("	and b.Specification like  '%'+ @Specification + '%' ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Specification", model.Specification));
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
                //批次
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
            }
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion
        #endregion

        #region 进销存汇总表（单品）
        /// <summary>
        /// 进销存汇总表
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
        public static DataTable GetStorageInAndOutTotalInfo(ProductInfoModel model, string DailyDate, string EndDate, string BatchNo, string EFIndex, string EFDesc, int pageIndex, int pageCount, string OrderBy, out DataTable dt, ref int totalCount)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT	isnull( CONVERT(CHAR(10), a.DailyDate, 23),'') as DailyDate,isnull(b.ProductName,'') as ProductName,a.ProductID,isnull(b.Specification,'') as Specification,");
            searchSql.AppendLine("		Convert(numeric(22,2),isnull((select top 1 b.TodayCount  from officedba.StorageDaily b where");
            searchSql.AppendLine("			a.ProductID=b.ProductID");
            searchSql.AppendLine("			and b.DailyDate>=(CONVERT(CHAR(10), dateadd(dd,-1,a.DailyDate), 23))");
            searchSql.AppendLine("			and b.DailyDate<CONVERT(CHAR(10), a.DailyDate, 23)");
            searchSql.AppendLine("		),0)) as YestodayCount,");
            searchSql.AppendLine("		Convert(numeric(22,2),a.PhurInCount) as PhurInCount,Convert(numeric(22,2),a.MakeInCount) as MakeInCount,");
            searchSql.AppendLine("		Convert(numeric(22,2),a.DispInCount) as DispInCount,");
            searchSql.AppendLine("		Convert(numeric(22,2),a.OtherInCount) as OtherInCount,Convert(numeric(22,2),a.SendInCount) as SendInCount,Convert(numeric(22,2),a.SubSaleBackInCount) as SubSaleBackInCount,");
            searchSql.AppendLine("		Convert(numeric(22,2),a.TakeInCount) as TakeInCount,Convert(numeric(22,2),a.InTotal) as InTotal,Convert(numeric(22,2),a.SaleFee) as SaleFee,Convert(numeric(22,2),a.PhurBackFee) as PhurBackFee,");
            searchSql.AppendLine("		Convert(numeric(22,2),a.InitInCount) as InitInCount,Convert(numeric(22,2),a.InitBatchCount) as InitBatchCount,Convert(numeric(22,2),a.SaleBackInCount) as SaleBackInCount,");
            searchSql.AppendLine("		Convert(numeric(22,2),a.RedInCount) as RedInCount,Convert(numeric(22,2),a.BackInCount) as BackInCount,");
            searchSql.AppendLine("		");
            searchSql.AppendLine("		Convert(numeric(22,2),a.SaleOutCount) as SaleOutCount,Convert(numeric(22,2),a.TakeOutCount) as TakeOutCount,");
            searchSql.AppendLine("		Convert(numeric(22,2),a.DispOutCount) as DispOutCount,Convert(numeric(22,2),a.BadCount) as BadCount,");
            searchSql.AppendLine("		Convert(numeric(22,2),a.OtherOutCount) as OtherOutCount,Convert(numeric(22,2),a.SendOutCount) as SendOutCount,Convert(numeric(22,2),a.SubSaleOutCount) as SubSaleOutCount,");
            searchSql.AppendLine("		Convert(numeric(22,2),a.OutTotal) as OutTotal,Convert(numeric(22,2),a.PhurFee) as PhurFee,Convert(numeric(22,2),a.SaleBackFee) as SaleBackFee,");
            searchSql.AppendLine("		Convert(numeric(22,2),a.PhurBackOutCount) as PhurBackOutCount,Convert(numeric(22,2),a.RedOutCount) as RedOutCount,Convert(numeric(22,2),a.LendCount) as LendCount,");
            searchSql.AppendLine("isnull(a.BatchNo,'') as BatchNo,");
            searchSql.AppendLine("		Convert(numeric(22,2),ABS(a.CheckCount)) as CheckCount,Convert(numeric(22,2),ABS(a.AdjustCount)) as AdjustCount,");
            searchSql.AppendLine("		Convert(numeric(22,2),ABS(a.TodayCount)) as TodayCount,a.CreateDate,");
            searchSql.AppendLine("		b.ExtField1,b.ExtField2,b.ExtField3,b.ExtField4,b.ExtField5,b.ExtField6,b.ExtField7,b.ExtField8,b.ExtField9,b.ExtField10,");
            searchSql.AppendLine("		b.ExtField11,b.ExtField12,b.ExtField13,b.ExtField14,b.ExtField15,b.ExtField16,b.ExtField17,b.ExtField18,b.ExtField19,b.ExtField20,");
            searchSql.AppendLine("		b.ExtField21,b.ExtField22,b.ExtField23,b.ExtField24,b.ExtField25,b.ExtField26,b.ExtField27,b.ExtField28,b.ExtField29,b.ExtField30");
            searchSql.AppendLine("FROM	officedba.StorageDaily a ");
            searchSql.AppendLine("left join officedba.ProductInfo b on a.ProductID=b.ID  and a.CompanyCD=b.CompanyCD  ");
            searchSql.AppendLine("where a.CompanyCD=@CompanyCD ");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
           
           
            
            
          
            if (!string.IsNullOrEmpty(model.ColorID))
            {
                searchSql.AppendLine(" and b.ColorID=@ColorID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ColorID", model .ColorID ));
            }

            if (!string.IsNullOrEmpty(model.Material))
            {
                searchSql.AppendLine(" and b.Material=@Material");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Material", model.Material));
            }

            if (!string.IsNullOrEmpty(model.Manufacturer))
            {
                searchSql.AppendLine("	and b.Manufacturer like  '%'+ @Manufacturer + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Manufacturer", model.Manufacturer));
            }

            if (!string.IsNullOrEmpty(model.FromAddr))
            {
                searchSql.AppendLine("	and b.FromAddr like  '%'+ @FromAddr + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromAddr", model.FromAddr));
            }

            if (!string.IsNullOrEmpty(model.BarCode))
            {
                searchSql.AppendLine("	and b.BarCode like  '%'+ @BarCode + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BarCode", model.BarCode));
            }

            if (!string.IsNullOrEmpty(model.Size))
            {
                searchSql.AppendLine("	and b.Size like  '%'+ @Size + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Size", model.Size));
            }

            //开始日期
            if (!string.IsNullOrEmpty(DailyDate))
            {
                searchSql.AppendLine(" and DailyDate>=@DailyDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DailyDate", DailyDate));
            }
            //结束日期
            if (!string.IsNullOrEmpty(EndDate))
            {
                searchSql.AppendLine(" and a.DailyDate<=@EndDate");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", EndDate));
            }
            //物品编号
            if (!string.IsNullOrEmpty(model.ProdNo))
            {
                searchSql.AppendLine(" and b.ProdNo=@ProdNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProdNo", model.ProdNo));
            }
            //物品名称
            if (!string.IsNullOrEmpty(model.ProductName))
            {
                searchSql.AppendLine("	and b.ProductName like  '%'+ @ProductName + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", model.ProductName));
            }
            //物品规格
            if (!string.IsNullOrEmpty(model.Specification))
            {
                searchSql.AppendLine("	and b.Specification like  '%'+ @Specification + '%' ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Specification", model.Specification));
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
            //批次
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
            dt = GetAllTotal(model, DailyDate, EndDate, BatchNo, EFIndex, EFDesc, true);
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion

    } 
}

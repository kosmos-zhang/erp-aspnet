/**********************************************
 * 类作用：   门店进销存分析
 * 建立人：   zhangyy
 * 建立时间： 2010/04/06
 ***********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Common;
using XBase.Data.DBHelper;
using XBase.Model.Office.SupplyChain;

namespace XBase.Data.OperatingModel.IntegratedData
{
    public class SotreBuySellStockByDayDBHelper
    {
        #region 根据条件获取门店进销存日报表_单个
        /// <summary>
        /// 根据条件获取门店进销存日报表
        /// </summary>
        /// <param name="DeptID"></param>
        /// <param name="EnterDate"></param>
        /// <param name="ProductNo"></param>
        /// <param name="ProductName"></param>
        /// <param name="Specification"></param>
        /// <param name="EFIndex"></param>
        /// <param name="EFDesc"></param>
        /// <param name="BatchNo"></param>
        /// <param name="StorageID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetBuySellStockByDayList(string Color, string FromAddr, string Manufacturer, string Size, string Material, string BarCode,
            string DeptID, string EnterDate, string ProductNo, string ProductName, string Specification,
            string EFIndex, string EFDesc, string BatchNo, string StorageID, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" select a.ID,a.CompanyCD,a.DeptID,b.DeptName,Convert(varchar(10),a.DailyDate,23) DailyDate ");
            searchSql.AppendLine("   ,a.ProductID,c.ProductName,c.Specification,a.BatchNo,isnull(a.InitInCount,0)InitInCount,isnull(a.InitBatchCount,0)InitBatchCount, ");
            searchSql.AppendLine("      isnull((select top 1 isnull(z.TodayCount,0) from officedba.SubStorageDaily z  ");
            searchSql.AppendLine("      where z.ProductID = a.ProductID	and z.DeptID = a.DeptID  ");
            searchSql.AppendLine("          and z.DailyDate = Convert(varchar(10),dateadd(dd,-1,a.DailyDate),23)),0) yesCount, ");
            searchSql.AppendLine("   isnull(a.SendInCount,0)SendInCount,isnull(a.SubSaleOutCount,0)SubSaleOutCount,a.TodayCount,a.SaleFee,isnull(a.SubSaleBackInCount,0)SubSaleBackInCount,isnull(a.DispInCont,0)DispInCont ");
            //searchSql.AppendLine("		c.ExtField1,c.ExtField2,c.ExtField3,c.ExtField4,c.ExtField5,c.ExtField6,c.ExtField7,c.ExtField8,c.ExtField9,c.ExtField10,");
            //searchSql.AppendLine("		c.ExtField11,c.ExtField12,c.ExtField13,c.ExtField14,c.ExtField15,c.ExtField16,c.ExtField17,c.ExtField18,c.ExtField19,c.ExtField20,");
            //searchSql.AppendLine("		c.ExtField21,c.ExtField22,c.ExtField23,c.ExtField24,c.ExtField25,c.ExtField26,c.ExtField27,c.ExtField28,c.ExtField29,c.ExtField30 ");
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                if (int.Parse(EFIndex) > 0)
                {
                    searchSql.AppendLine("       ,c.ExtField" + EFIndex + " ExtField ");
                }
            }
            searchSql.AppendLine(" ,a.SaleBackFee,a.InTotal,isnull(a.DispOutCount,0)DispOutCount,isnull(a.SendOutCount,0)SendOutCount,a.OutTotal from officedba.SubStorageDaily a ");
            searchSql.AppendLine(" left join officedba.DeptInfo b on a.DeptID = b.id ");
            searchSql.AppendLine(" left join officedba.ProductInfo c on c.id = a.ProductID ");
            searchSql.AppendLine(" left join officedba.CodePublicType d on d.id = c.ColorID ");
            searchSql.AppendLine(" left join officedba.CodePublicType e on e.id = c.Material ");
            searchSql.AppendLine(" where a.DailyDate = @DailyDate and a.CompanyCD = @CompanyCD ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DailyDate", EnterDate));

            #region
            //颜色
            if (!string.IsNullOrEmpty(Color))
            {
                searchSql.AppendLine(" and d.TypeName like '%'+ @Color +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Color", Color));
            }
            //产地
            if (!string.IsNullOrEmpty(FromAddr))
            {
                searchSql.AppendLine(" and c.FromAddr like '%'+ @FromAddr +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromAddr", FromAddr));
            }
            //厂家
            if (!string.IsNullOrEmpty(Manufacturer))
            {
                searchSql.AppendLine(" and c.Manufacturer like '%'+ @Manufacturer +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Manufacturer", Manufacturer));
            }
            //尺寸
            if (!string.IsNullOrEmpty(Size))
            {
                searchSql.AppendLine(" and c.Size like '%'+ @Size +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Size", Size));
            }
            //材质
            if (!string.IsNullOrEmpty(Material))
            {
                searchSql.AppendLine(" and e.TypeName like '%'+ @Material +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Material", Material));
            }
            //条码
            if (!string.IsNullOrEmpty(BarCode))
            {
                searchSql.AppendLine(" and c.BarCode like '%'+ @BarCode +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BarCode", BarCode));
            }
            //门店ID
            if (!string.IsNullOrEmpty(DeptID))
            {
                searchSql.AppendLine(" and a.DeptID=@DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
            }
            //物品编号
            if (!string.IsNullOrEmpty(ProductNo))
            {
                searchSql.AppendLine(" and a.ProductNo=@ProductNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductNo", ProductNo));
            }
            //品名
            if (!string.IsNullOrEmpty(ProductName))
            {
                searchSql.AppendLine(" and c.ProductName like '%'+ @ProductName +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", ProductName));
            }
            //规格
            if (!string.IsNullOrEmpty(Specification))
            {
                searchSql.AppendLine(" and c.Specification like '%'+ @Specification +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Specification", Specification));
            }
            //批次
            if (!string.IsNullOrEmpty(BatchNo))
            {
                searchSql.AppendLine(" and a.BatchNo like '%'+ @BatchNo +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", BatchNo));
            }
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                if (int.Parse(EFIndex) > 0)
                {
                    searchSql.AppendLine(" and c.ExtField" + EFIndex + " LIKE @EFDesc");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + EFDesc + "%"));
                }
            }
            #endregion
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion

        #region 根据条件获取门店进销存日报表_求和
        public static DataTable GetBuySellStockByDay_Sum(string Color, string FromAddr, string Manufacturer, string Size, string Material, string BarCode,
            string DeptID, string EnterDate, string ProductNo, string ProductName, string Specification,
            string EFIndex, string EFDesc, string BatchNo, string StorageID)
        {
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT	sum(isnull(InTotal,0)) as InTotalCount,sum(isnull(OutTotal,0)) as OutTotalCount");
            searchSql.AppendLine("		,sum(isnull(SaleFee,0)) as SaleFeeCount,sum(isnull(SaleBackFee,0)) as SaleBackFeeCount ");
            searchSql.AppendLine("FROM	officedba.SubStorageDaily a");
            searchSql.AppendLine(" left join officedba.DeptInfo b on a.DeptID = b.id ");
            searchSql.AppendLine(" left join officedba.ProductInfo c on c.id = a.ProductID ");
            searchSql.AppendLine(" left join officedba.CodePublicType d on d.id = c.ColorID ");
            searchSql.AppendLine(" left join officedba.CodePublicType e on e.id = c.Material ");
            searchSql.AppendLine(" where a.DailyDate = @DailyDate and a.CompanyCD = @CompanyCD ");
            //searchSql.AppendLine("left join officedba.ProductInfo b on a.ProductID=b.ID");
            //searchSql.AppendLine("where a.CompanyCD=@CompanyCD ");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DailyDate", EnterDate));

            #region
            //颜色
            if (!string.IsNullOrEmpty(Color))
            {
                searchSql.AppendLine(" and d.TypeName like '%'+ @Color +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Color", Color));
            }
            //产地
            if (!string.IsNullOrEmpty(FromAddr))
            {
                searchSql.AppendLine(" and c.FromAddr like '%'+ @FromAddr +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromAddr", FromAddr));
            }
            //厂家
            if (!string.IsNullOrEmpty(Manufacturer))
            {
                searchSql.AppendLine(" and c.Manufacturer like '%'+ @Manufacturer +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Manufacturer", Manufacturer));
            }
            //尺寸
            if (!string.IsNullOrEmpty(Size))
            {
                searchSql.AppendLine(" and c.Size like '%'+ @Size +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Size", Size));
            }
            //材质
            if (!string.IsNullOrEmpty(Material))
            {
                searchSql.AppendLine(" and e.TypeName like '%'+ @Material +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Material", Material));
            }
            //条码
            if (!string.IsNullOrEmpty(BarCode))
            {
                searchSql.AppendLine(" and c.BarCode like '%'+ @BarCode +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BarCode", BarCode));
            }
            //门店ID
            if (!string.IsNullOrEmpty(DeptID))
            {
                searchSql.AppendLine(" and a.DeptID=@DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
            }
            //物品编号
            if (!string.IsNullOrEmpty(ProductNo))
            {
                searchSql.AppendLine(" and a.ProductNo=@ProductNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductNo", ProductNo));
            }
            //品名
            if (!string.IsNullOrEmpty(ProductName))
            {
                searchSql.AppendLine(" and c.ProductName like '%'+ @ProductName +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", ProductName));
            }
            //规格
            if (!string.IsNullOrEmpty(Specification))
            {
                searchSql.AppendLine(" and c.Specification like '%'+ @Specification +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Specification", Specification));
            }
            //批次
            if (!string.IsNullOrEmpty(BatchNo))
            {
                searchSql.AppendLine(" and a.BatchNo like '%'+ @BatchNo +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", BatchNo));
            }
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                if (int.Parse(EFIndex) > 0)
                {
                    searchSql.AppendLine(" and c.ExtField" + EFIndex + " LIKE @EFDesc");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + EFDesc + "%"));
                }
            }
            #endregion
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 根据条件获取门店进销存日报表_汇总
        /// <summary>
        /// 根据条件获取门店进销存日报表
        /// </summary>
        /// <param name="DeptID"></param>
        /// <param name="EnterDate"></param>
        /// <param name="BatchNo"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetBuySellStockByDayList_hz(string DeptID, string EnterDate, string BatchNo,
                                                        int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" select '1' ID,c.DeptID,d.DeptName,c.DailyDate,'' ProductID,'' ProductName,'' Specification,");
            searchSql.AppendLine(" 		'' BatchNo,c.yesCount,c.SendInCount,");
            searchSql.AppendLine(" 		c.SubSaleOutCount,c.TodayCount,c.SaleFee,c.SaleBackFee,");
            searchSql.AppendLine(" 		c.InitInCount,isnull(c.InitBatchCount,0)InitBatchCount,c.SubSaleBackInCount,c.DispInCont,");
            searchSql.AppendLine(" 		c.InTotal,c.DispOutCount,c.SendOutCount,c.OutTotal ");
            searchSql.AppendLine(" from ");
            searchSql.AppendLine(" 		(select b.DeptID,b.DailyDate,");
            searchSql.AppendLine(" 			sum(isnull(b.yesCount,0))yesCount,");
            searchSql.AppendLine(" 			sum(b.SendInCount)SendInCount,");
            searchSql.AppendLine(" 			sum(b.SubSaleOutCount)SubSaleOutCount,");
            searchSql.AppendLine(" 			sum(b.TodayCount)TodayCount,");
            searchSql.AppendLine(" 			sum(b.SaleFee)SaleFee,");
            searchSql.AppendLine(" 			sum(b.SaleBackFee)SaleBackFee,");
            searchSql.AppendLine(" 			sum(b.InitInCount)InitInCount,sum(b.InitBatchCount)InitBatchCount,");
            searchSql.AppendLine(" 			sum(b.SubSaleBackInCount)SubSaleBackInCount,sum(b.DispInCont)DispInCont,");
            searchSql.AppendLine(" 			sum(b.InTotal)InTotal,sum(b.DispOutCount)DispOutCount,");
            searchSql.AppendLine(" 			sum(b.SendOutCount)SendOutCount,sum(b.OutTotal)OutTotal");
            searchSql.AppendLine(" 		from ");
            searchSql.AppendLine(" 			 (select a.DeptID,Convert(varchar(10),a.DailyDate,23) DailyDate,a.BatchNo,");
            searchSql.AppendLine(" 				  (select top 1 isnull(z.TodayCount,0)TodayCount from officedba.SubStorageDaily z  ");
            searchSql.AppendLine(" 				  where z.ProductID = a.ProductID	and z.DeptID = a.DeptID  ");
            searchSql.AppendLine(" 					  and z.DailyDate = Convert(varchar(10),dateadd(dd,-1,a.DailyDate),23)) yesCount, ");
            searchSql.AppendLine(" 				isnull(a.SendInCount,0)SendInCount,isnull(a.SubSaleOutCount,0)SubSaleOutCount,isnull(a.TodayCount,0)TodayCount,isnull(a.SaleFee,0)SaleFee,isnull(a.SaleBackFee,0)SaleBackFee");
            searchSql.AppendLine(" 				,isnull(a.InitInCount,0)InitInCount,isnull(a.InitBatchCount,0)InitBatchCount,isnull(a.SubSaleBackInCount,0)SubSaleBackInCount,isnull(a.DispInCont,0)DispInCont");//++
            searchSql.AppendLine(" 				,isnull(a.InTotal,0)InTotal,isnull(a.DispOutCount,0)DispOutCount,isnull(a.SendOutCount,0)SendOutCount,isnull(a.OutTotal,0)OutTotal");//++
            searchSql.AppendLine(" 			  from officedba.SubStorageDaily a ");
            searchSql.AppendLine(" 			  where a.DailyDate = @DailyDate and a.CompanyCD = @CompanyCD ");
            searchSql.AppendLine(" 			  ) b");
            searchSql.AppendLine(" 		group by b.DeptID,b.DailyDate");
            searchSql.AppendLine(" 		) c");
            searchSql.AppendLine(" left join officedba.DeptInfo d on c.DeptID = d.id where 1=1 ");



            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DailyDate", EnterDate));

            //门店ID
            if (!string.IsNullOrEmpty(DeptID))
            {
                searchSql.AppendLine(" and c.DeptID=@DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
            }

            //批次
            if (!string.IsNullOrEmpty(BatchNo))
            {
                searchSql.AppendLine(" and c.BatchNo like '%'+@BatchNo +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", BatchNo));
            }
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }

        public static DataTable GetBuySellStockByDayList_Sum(string DeptID, string EnterDate, string BatchNo)
        {
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT	sum(InTotal) as InTotalCount,sum(OutTotal) as OutTotalCount");
            searchSql.AppendLine("		,sum(SaleFee) as SaleFeeCount,sum(SaleBackFee) as SaleBackFeeCount ");
            searchSql.AppendLine("FROM	officedba.SubStorageDaily c");
            searchSql.AppendLine(" left join officedba.DeptInfo d on c.DeptID = d.id ");
            searchSql.AppendLine(" where c.DailyDate = @DailyDate and c.CompanyCD = @CompanyCD ");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DailyDate", EnterDate));

            //门店ID
            if (!string.IsNullOrEmpty(DeptID))
            {
                searchSql.AppendLine(" and c.DeptID=@DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
            }

            //批次
            if (!string.IsNullOrEmpty(BatchNo))
            {
                searchSql.AppendLine(" and c.BatchNo like '%'+@BatchNo +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", BatchNo));
            }
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
            //return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion

        #region 根据条件获取门店进销存日报表_单个_导出
        /// <summary>
        /// 根据条件获取门店进销存日报表
        /// </summary>
        /// <param name="DeptID"></param>
        /// <param name="EnterDate"></param>
        /// <param name="ProductNo"></param>
        /// <param name="ProductName"></param>
        /// <param name="Specification"></param>
        /// <param name="EFIndex"></param>
        /// <param name="EFDesc"></param>
        /// <param name="BatchNo"></param>
        /// <param name="StorageID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetBuySellStockByDayList_Export(string Color, string FromAddr, string Manufacturer, string Size, string Material, string BarCode,
            string DeptID, string EnterDate, string ProductNo, string ProductName, string Specification,
            string EFIndex, string EFDesc, string BatchNo, string OrderBy)
        {
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" select a.ID,a.CompanyCD,a.DeptID,b.DeptName,Convert(varchar(10),a.DailyDate,23) DailyDate ");
            searchSql.AppendLine("   ,a.ProductID,c.ProductName,c.Specification,a.BatchNo, ");
            searchSql.AppendLine("      isnull((select top 1 isnull(z.TodayCount,0) from officedba.SubStorageDaily z  ");
            searchSql.AppendLine("      where z.ProductID = a.ProductID	and z.DeptID = a.DeptID  ");
            searchSql.AppendLine("          and z.DailyDate = Convert(varchar(10),dateadd(dd,-1,a.DailyDate),23)),0) yesCount, ");
            searchSql.AppendLine("   a.SendInCount,a.SubSaleOutCount,a.TodayCount,a.SaleFee ");
            //searchSql.AppendLine("		c.ExtField1,c.ExtField2,c.ExtField3,c.ExtField4,c.ExtField5,c.ExtField6,c.ExtField7,c.ExtField8,c.ExtField9,c.ExtField10,");
            //searchSql.AppendLine("		c.ExtField11,c.ExtField12,c.ExtField13,c.ExtField14,c.ExtField15,c.ExtField16,c.ExtField17,c.ExtField18,c.ExtField19,c.ExtField20,");
            //searchSql.AppendLine("		c.ExtField21,c.ExtField22,c.ExtField23,c.ExtField24,c.ExtField25,c.ExtField26,c.ExtField27,c.ExtField28,c.ExtField29,c.ExtField30 ");
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                if (int.Parse(EFIndex) > 0)
                {
                    searchSql.AppendLine("       ,c.ExtField" + EFIndex + " ExtField ");
                }
            }
            searchSql.AppendLine(" ,a.SaleBackFee from officedba.SubStorageDaily a ");
            searchSql.AppendLine(" left join officedba.DeptInfo b on a.DeptID = b.id ");
            searchSql.AppendLine(" left join officedba.ProductInfo c on c.id = a.ProductID ");
            searchSql.AppendLine(" left join officedba.CodePublicType d on d.id = c.ColorID ");
            searchSql.AppendLine(" left join officedba.CodePublicType e on e.id = c.Material ");
            searchSql.AppendLine(" where a.DailyDate = @DailyDate and a.CompanyCD = @CompanyCD ");
            #endregion

            try
            {
                //定义查询的命令
                SqlCommand comm = new SqlCommand();
                //添加公司代码参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DailyDate", EnterDate));
                //颜色
                if (!string.IsNullOrEmpty(Color))
                {
                    searchSql.AppendLine(" and d.TypeName like '%'+ @Color +'%'");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Color", Color));
                }
                //产地
                if (!string.IsNullOrEmpty(FromAddr))
                {
                    searchSql.AppendLine(" and c.FromAddr like '%'+ @FromAddr +'%'");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromAddr", FromAddr));
                }
                //厂家
                if (!string.IsNullOrEmpty(Manufacturer))
                {
                    searchSql.AppendLine(" and c.Manufacturer like '%'+ @Manufacturer +'%'");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Manufacturer", Manufacturer));
                }
                //尺寸
                if (!string.IsNullOrEmpty(Size))
                {
                    searchSql.AppendLine(" and c.Size like '%'+ @Size +'%'");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Size", Size));
                }
                //材质
                if (!string.IsNullOrEmpty(Material))
                {
                    searchSql.AppendLine(" and e.TypeName like '%'+ @Material +'%'");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Material", Material));
                }
                //条码
                if (!string.IsNullOrEmpty(BarCode))
                {
                    searchSql.AppendLine(" and c.BarCode like '%'+ @BarCode +'%'");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@BarCode", BarCode));
                }
                //门店ID
                if (!string.IsNullOrEmpty(DeptID))
                {
                    searchSql.AppendLine(" and a.DeptID=@DeptID");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
                }
                //物品编号
                if (!string.IsNullOrEmpty(ProductNo))
                {
                    searchSql.AppendLine(" and a.ProductNo=@ProductNo");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductNo", ProductNo));
                }
                //品名
                if (!string.IsNullOrEmpty(ProductName))
                {
                    searchSql.AppendLine(" and c.ProductName like '%'+ @ProductName +'%'");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", ProductName));
                }
                //规格
                if (!string.IsNullOrEmpty(Specification))
                {
                    searchSql.AppendLine(" and c.Specification like '%'+ @Specification +'%'");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Specification", Specification));
                }
                //批次
                if (!string.IsNullOrEmpty(BatchNo))
                {
                    searchSql.AppendLine(" and a.BatchNo like '%'+ @BatchNo +'%'");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", BatchNo));
                }
                if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
                {
                    if (int.Parse(EFIndex) > 0)
                    {
                        searchSql.AppendLine(" and c.ExtField" + EFIndex + " LIKE @EFDesc");
                        comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + EFDesc + "%"));
                    }
                }

                searchSql.AppendLine(OrderBy);
                //指定命令的SQL文
                comm.CommandText = searchSql.ToString();

                DataTable dt = SqlHelper.ExecuteSearch(comm);
                //执行查询
                return dt;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 根据条件获取门店进销存日报表_汇总_导出
        /// <summary>
        /// 根据条件获取门店进销存日报表
        /// </summary>
        /// <param name="DeptID"></param>
        /// <param name="EnterDate"></param>
        /// <param name="BatchNo"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetBuySellStockByDayList_hz_Export(string DeptID, string EnterDate, string BatchNo, string OrderBy)
        {
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" select '1' ID,c.DeptID,d.DeptName,c.DailyDate,'' ProductName,'' Specification,");
            searchSql.AppendLine(" 		c.BatchNo,c.yesCount,c.SendInCount,");
            searchSql.AppendLine(" 		c.SubSaleOutCount,c.TodayCount,");
            searchSql.AppendLine(" 		c.SaleFee,c.SaleBackFee");
            searchSql.AppendLine(" from ");
            searchSql.AppendLine(" 		(select b.DeptID,b.DailyDate,b.BatchNo,");
            searchSql.AppendLine(" 			sum(isnull(b.yesCount,0))yesCount,");
            searchSql.AppendLine(" 			sum(b.SendInCount)SendInCount,");
            searchSql.AppendLine(" 			sum(b.SubSaleOutCount)SubSaleOutCount,");
            searchSql.AppendLine(" 			sum(b.TodayCount)TodayCount,");
            searchSql.AppendLine(" 			sum(b.SaleFee)SaleFee,");
            searchSql.AppendLine(" 			sum(b.SaleBackFee)SaleBackFee");
            searchSql.AppendLine(" 		from ");
            searchSql.AppendLine(" 			 (select a.DeptID,Convert(varchar(10),a.DailyDate,23) DailyDate,a.BatchNo,");
            searchSql.AppendLine(" 				  (select top 1 z.TodayCount from officedba.SubStorageDaily z  ");
            searchSql.AppendLine(" 				  where z.ProductID = a.ProductID	and z.DeptID = a.DeptID  ");
            searchSql.AppendLine(" 					  and z.DailyDate = Convert(varchar(10),dateadd(dd,-1,a.DailyDate),23)) yesCount, ");
            searchSql.AppendLine(" 				a.SendInCount,a.SubSaleOutCount,");
            searchSql.AppendLine(" 				a.TodayCount,a.SaleFee,a.SaleBackFee");
            searchSql.AppendLine(" 			  from officedba.SubStorageDaily a ");
            searchSql.AppendLine(" 			  where a.DailyDate = @DailyDate and a.CompanyCD = @CompanyCD ");
            searchSql.AppendLine(" 			  ) b");
            searchSql.AppendLine(" 		group by b.DeptID,b.DailyDate,b.BatchNo");
            searchSql.AppendLine(" 		) c");
            searchSql.AppendLine(" left join officedba.DeptInfo d on c.DeptID = d.id where 1=1 ");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DailyDate", EnterDate));

            //门店ID
            if (!string.IsNullOrEmpty(DeptID))
            {
                searchSql.AppendLine(" and c.DeptID=@DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
            }

            //批次
            if (!string.IsNullOrEmpty(BatchNo))
            {
                searchSql.AppendLine(" and c.BatchNo like '%'+@BatchNo +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", BatchNo));
            }
            searchSql.AppendLine(OrderBy);
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
            //return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion

         

        #region 根据条件获取门店进销存日报表_合计
        /// <summary>
        /// 根据条件获取门店进销存日报表
        /// </summary>
        /// <param name="DeptID"></param>
        /// <param name="EnterDate"></param>
        /// <param name="BatchNo"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetBuySellStockByDayList_Sum(string Color, string FromAddr, string Size, string Material, string BarCode,
            string ProductNo, string ProductName, string Specification, string Manufacturer, string DeptID, string EnterDate, string BatchNo, string OrderBy)
        {
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" 		select ");
            searchSql.AppendLine(" 			isnull(sum(isnull(b.yesCount,0)),0)yesCount,");
            searchSql.AppendLine(" 			isnull(sum(b.SendInCount),0)SendInCount,");
            searchSql.AppendLine(" 			isnull(sum(b.SubSaleOutCount),0)SubSaleOutCount,");
            searchSql.AppendLine(" 			isnull(sum(b.TodayCount),0)TodayCount,");
            searchSql.AppendLine(" 			isnull(sum(b.SaleFee),0)SaleFee,");
            searchSql.AppendLine(" 			isnull(sum(b.SaleBackFee),0)SaleBackFee");
            searchSql.AppendLine(" 		from ");
            searchSql.AppendLine(" 			 (select a.DeptID,Convert(varchar(10),a.DailyDate,23) DailyDate,a.BatchNo,");
            searchSql.AppendLine(" 				  (select top 1 z.TodayCount from officedba.SubStorageDaily z  ");
            searchSql.AppendLine(" 				  where z.ProductID = a.ProductID	and z.DeptID = a.DeptID  ");
            searchSql.AppendLine(" 					  and z.DailyDate = Convert(varchar(10),dateadd(dd,-1,a.DailyDate),23)) yesCount, ");
            searchSql.AppendLine(" 				a.SendInCount,a.SubSaleOutCount,");
            searchSql.AppendLine(" 				a.TodayCount,a.SaleFee,a.SaleBackFee");
            searchSql.AppendLine(" 			  from officedba.SubStorageDaily a ");
            searchSql.AppendLine(" 			  left join officedba.productinfo x1 on x1.id= a.ProductID ");
            searchSql.AppendLine("            left join officedba.CodePublicType x2 on x2.id = x1.ColorID ");
            searchSql.AppendLine("            left join officedba.CodePublicType x3 on x3.id = x1.Material ");
            searchSql.AppendLine(" 			  where a.DailyDate = @DailyDate and a.CompanyCD = @CompanyCD ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DailyDate", EnterDate));
            //颜色
            if (!string.IsNullOrEmpty(Color))
            {
                searchSql.AppendLine(" and x2.TypeName like '%'+ @Color +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Color", Color));
            }
            //产地
            if (!string.IsNullOrEmpty(FromAddr))
            {
                searchSql.AppendLine(" and x1.FromAddr like '%'+ @FromAddr +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromAddr", FromAddr));
            }
            //厂家
            if (!string.IsNullOrEmpty(Manufacturer))
            {
                searchSql.AppendLine(" and x1.Manufacturer like '%'+ @Manufacturer +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Manufacturer", Manufacturer));
            }
            //尺寸
            if (!string.IsNullOrEmpty(Size))
            {
                searchSql.AppendLine(" and x1.Size like '%'+ @Size +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Size", Size));
            }
            //材质
            if (!string.IsNullOrEmpty(Material))
            {
                searchSql.AppendLine(" and x3.TypeName like '%'+ @Material +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Material", Material));
            }
            //条码
            if (!string.IsNullOrEmpty(BarCode))
            {
                searchSql.AppendLine(" and x1.BarCode like '%'+ @BarCode +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BarCode", BarCode));
            }
            //物品编号
            if (!string.IsNullOrEmpty(ProductNo))
            {
                searchSql.AppendLine(" and x1.ProductNo=@ProductNo");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductNo", ProductNo));
            }
            //品名
            if (!string.IsNullOrEmpty(ProductName))
            {
                searchSql.AppendLine(" and x1.ProductName=@ProductName");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", ProductName));
            }
            //规格
            if (!string.IsNullOrEmpty(Specification))
            {
                searchSql.AppendLine(" and x1.Specification=@Specification");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Specification", Specification));
            }
            //门店ID
            if (!string.IsNullOrEmpty(DeptID))
            {
                searchSql.AppendLine(" and a.DeptID=@DeptID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
            }
            //批次
            if (!string.IsNullOrEmpty(BatchNo))
            {
                searchSql.AppendLine(" and a.BatchNo like '%'+@BatchNo +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", BatchNo));
            }

            searchSql.AppendLine(" 			  ) b");
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion
    }

    /// <summary>
    /// 门店进销存月报表 
    /// </summary>
    public class SubStoreMonthReportDBHelper
    {
        /// <summary>
        /// 门店进销存月报表
        /// </summary>
        /// <param name="model">产品信息</param>
        /// <param name="SumModel">汇总方式</param>
        /// <param name="SubStoreID">部门</param>
        /// <param name="dStime">开始时间</param>
        /// <param name="dEtime">结束时间</param>
        /// <param name="BatchNo">批次</param>
        /// <param name="EFIndex">扩展索引</param>
        /// <param name="EFDesc">扩展条件</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageCount">每页数</param>
        /// <param name="OrderBy">排序</param>
        /// <param name="totalCount">总数</param>
        /// <returns></returns>
        public static DataTable GetSubStoreMonthReport(ProductInfoModel model, bool SumModel, string SubStoreID
            , DateTime dStime, DateTime dEtime, string BatchNo, string EFIndex, string EFDesc
            , int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            //定义查询的命令
            SqlCommand comm = GetSubStoreCommand(model, SumModel, SubStoreID, dStime, dEtime, BatchNo, EFIndex, EFDesc, false);
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }

        /// <summary>
        /// 门店进销存月报表汇总信息
        /// </summary>
        /// <param name="model">产品信息</param>
        /// <param name="SumModel">汇总方式</param>
        /// <param name="SubStoreID">部门</param>
        /// <param name="dStime">开始时间</param>
        /// <param name="dEtime">结束时间</param>
        /// <param name="BatchNo">批次</param>
        /// <param name="EFIndex">扩展索引</param>
        /// <param name="EFDesc">扩展条件</param>
        /// <returns></returns>
        public static DataTable GetSubStoreMonthReportTotal(ProductInfoModel model, bool SumModel, string SubStoreID
            , DateTime dStime, DateTime dEtime, string BatchNo, string EFIndex, string EFDesc)
        {
            //定义查询的命令
            SqlCommand comm = GetSubStoreCommand(model, SumModel, SubStoreID, dStime, dEtime, BatchNo, EFIndex, EFDesc, true);
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }

        /// <summary>
        /// 门店进销存月报表(导出使用)
        /// </summary>
        /// <param name="model">产品信息</param>
        /// <param name="SumModel">汇总方式</param>
        /// <param name="SubStoreID">部门</param>
        /// <param name="dStime">开始时间</param>
        /// <param name="dEtime">结束时间</param>
        /// <param name="BatchNo">批次</param>
        /// <param name="EFIndex">扩展索引</param>
        /// <param name="EFDesc">扩展条件</param>
        /// <returns></returns>
        public static DataTable GetSubReportToExel(ProductInfoModel model, bool SumModel, string SubStoreID
            , DateTime dStime, DateTime dEtime, string BatchNo, string EFIndex, string EFDesc)
        {
            //定义查询的命令
            SqlCommand comm = GetSubStoreCommand(model, SumModel, SubStoreID, dStime, dEtime, BatchNo, EFIndex, EFDesc, false);
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }


        /// <summary>
        /// 获得门店进销存月报表查询语句
        /// </summary>
        /// <param name="model">产品信息</param>
        /// <param name="SumModel">汇总方式</param>
        /// <param name="SubStoreID">部门</param>
        /// <param name="dStime">开始时间</param>
        /// <param name="dEtime">结束时间</param>
        /// <param name="BatchNo">批次</param>
        /// <param name="EFIndex">扩展索引</param>
        /// <param name="EFDesc">扩展条件</param>
        /// <param name="bTotal">是否是求总量</param>
        /// <returns></returns>
        private static SqlCommand GetSubStoreCommand(ProductInfoModel model, bool SumModel, string SubStoreID
            , DateTime dStime, DateTime dEtime, string BatchNo, string EFIndex, string EFDesc, bool bTotal)
        {
            //查询SQL拼写
            StringBuilder sb = new StringBuilder();
            StringBuilder sbLast = new StringBuilder();

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            int id = 0;
            sb.AppendLine(@"SELECT SUM(ISNULL(ssd.InTotal,0)) AS InTotal
                          ,SUM(ISNULL(ssd.OutTotal,0)) AS OutTotal
                          ,SUM(ISNULL(ssd.SaleFee,0)) AS SaleFee
                          ,SUM(ISNULL(ssd.SaleBackFee,0)) AS SaleBackFee ");
            if (!bTotal)
            {// 不是求总量模式
                sb.AppendLine(@"  ,di.DeptName
		                          ,SUBSTRING(CONVERT(VARCHAR ,ssd.DailyDate ,120) ,0 ,11) AS DailyDate
                                  ,ISNULL(({0}),0) AS LastTimeCount
                                  ,SUM(ISNULL(ssd.TodayCount,0)) AS TodayCount
                                  ,SUM(ISNULL(ssd.InitInCount,0)) AS InitInCount
                                  ,SUM(ISNULL(ssd.InitBatchCount,0)) AS InitBatchCount
                                  ,SUM(ISNULL(ssd.SendInCount,0)) AS SendInCount
                                  ,SUM(ISNULL(ssd.SubSaleBackInCount,0)) AS SubSaleBackInCount
                                  ,SUM(ISNULL(ssd.DispInCont,0)) AS DispInCont
                                  ,SUM(ISNULL(ssd.SubSaleOutCount,0)) AS SubSaleOutCount
                                  ,SUM(ISNULL(ssd.SendOutCount,0)) AS SendOutCount
                                  ,SUM(ISNULL(ssd.DispOutCount,0)) AS DispOutCount");
            }
            if (!SumModel && !bTotal)
            {// 单品模式
                sb.AppendLine(",pi1.ProdNo, pi1.ProductName,pi1.Specification,ssd.BatchNo");
            }

            // 上日结算
            sbLast.AppendLine(@"SELECT TOP(1)  SUM(ISNULL(ssd1.TodayCount,0)) AS LasttimeCount
					FROM officedba.SubStorageDaily ssd1
					LEFT JOIN officedba.ProductInfo pi1 ON ssd1.ProductID=pi1.ID
					WHERE ssd1.DailyDate=DATEADD(DAY,-1,ssd.DailyDate) AND ssd1.DeptID=ssd.DeptID ");
            if (!SumModel)
            {// 单品模式
                sbLast.Append(" AND pi1.ID=ssd.ProductID AND ISNULL(ssd1.BatchNo,'')=ISNULL(ssd.BatchNo,'') ");
            }
            bool bExt = !string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc) && (int.Parse(EFIndex) > 0);
            if (bExt && !bTotal)
            {
                sb.AppendLine(" ,pi1.ExtField" + EFIndex + " AS ExtField ");
            }
            sb.AppendLine(@"FROM officedba.SubStorageDaily ssd
                            LEFT JOIN officedba.ProductInfo pi1 ON ssd.ProductID=pi1.ID 
                            LEFT JOIN officedba.DeptInfo di ON ssd.DeptID=di.ID
                        ");
            // 机构
            sb.AppendLine(" WHERE ssd.CompanyCD=@CompanyCD ");
            sbLast.AppendLine(" AND ssd1.CompanyCD=@CompanyCD ");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            // 分店

            if (int.TryParse(SubStoreID, out id) && id > 0)
            {
                sb.AppendLine(" AND ssd.DeptID=@DeptID ");
                comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", id));
            }
            //  开始时间
            sb.AppendLine(" AND DATEDIFF(DAY,ssd.DailyDate,@dStime)<=0 ");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@dStime", dStime.ToString()));

            //  结束时间
            sb.AppendLine(" AND DATEDIFF(DAY,ssd.DailyDate,@dEtime)>=0 ");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@dEtime", dEtime.ToString()));

            //颜色
            if (int.TryParse(model.ColorID, out id) && id > 0)
            {
                sb.AppendLine(" AND pi1.ColorID=@ColorID ");
                comm.Parameters.Add(SqlHelper.GetParameter("@ColorID", id));
            }
            //产地
            if (!string.IsNullOrEmpty(model.FromAddr))
            {
                sb.AppendLine(" AND pi1.FromAddr LIKE @FromAddr ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromAddr", string.Format("%{0}%", model.FromAddr)));
            }
            //厂家
            if (!string.IsNullOrEmpty(model.Manufacturer))
            {
                sb.AppendLine(" AND pi1.Manufacturer LIKE @Manufacturer ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Manufacturer", string.Format("%{0}%", model.Manufacturer)));
            }
            //尺寸
            if (!string.IsNullOrEmpty(model.Size))
            {
                sb.AppendLine(" AND pi1.Size LIKE @Size ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Size", string.Format("%{0}%", model.Size)));
            }
            //材质
            if (int.TryParse(model.Material, out id) && id > 0)
            {
                sb.AppendLine(" AND pi1.Material=@Material ");
                comm.Parameters.Add(SqlHelper.GetParameter("@Material", id));
            }
            //条码
            if (!string.IsNullOrEmpty(model.BarCode))
            {
                sb.AppendLine(" AND pi1.BarCode LIKE @BarCode ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BarCode", string.Format("%{0}%", model.BarCode)));
            }
            //物品编号
            if (!string.IsNullOrEmpty(model.ProdNo))
            {
                sb.AppendLine(" AND pi1.ProdNo=@ProdNo ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProdNo", model.ProdNo));
            }
            //品名
            if (!string.IsNullOrEmpty(model.ProductName))
            {
                sb.AppendLine(" AND pi1.ProductName LIKE @ProductName ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", string.Format("%{0}%", model.ProductName)));
            }
            //规格
            if (!string.IsNullOrEmpty(model.Specification))
            {
                sb.AppendLine(" AND pi1.Specification LIKE @Specification ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Specification", string.Format("%{0}%", model.Specification)));
            }
            //批次
            if (!string.IsNullOrEmpty(BatchNo))
            {
                sb.AppendLine(" AND ssd.BatchNo LIKE @BatchNo ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BatchNo", string.Format("%{0}%", BatchNo)));
            }
            if (bExt)
            {
                sb.AppendLine(" AND pi1.ExtField" + EFIndex + " LIKE @EFDesc");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + EFDesc + "%"));
            }
            if (!bTotal)
            {
                sb.AppendLine(@"GROUP BY ssd.DailyDate,ssd.DeptID,di.DeptName");
                sbLast.AppendLine(@"GROUP BY ssd1.DailyDate,ssd1.DeptID");
                if (bExt)
                {
                    sb.AppendLine(",pi1.ExtField" + EFIndex);
                    sbLast.AppendLine(",pi1.ExtField" + EFIndex);
                }
                if (!SumModel)
                {
                    sb.AppendLine(",pi1.ProdNo,pi1.ProductName,pi1.Specification,ssd.BatchNo,ssd.ProductID");
                    sbLast.AppendLine(",pi1.ProdNo,pi1.ProductName,pi1.Specification,ssd1.BatchNo");
                }
            }

            //指定命令的SQL文
            if (bTotal)
            {// 求总量模式
                comm.CommandText = sb.ToString();
            }
            else
            {
                comm.CommandText = string.Format(sb.ToString(), sbLast.ToString());
            }
            return comm;
        }
    }
}

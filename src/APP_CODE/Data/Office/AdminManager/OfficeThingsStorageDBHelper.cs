/**********************************************
 * 类作用：   办公用品入库库存数据库层处理
 * 建立人：   lysong
 * 建立时间： 2009/05/08
 ***********************************************/
using System;
using XBase.Model.Office.AdminManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
namespace XBase.Data.Office.AdminManager
{
    /// <summary>
    /// 类名：OfficeThingsStorageDBHelper
    /// 描述：办公用品入库库存数据库层处理
    /// 作者：lysong
    /// 创建时间：2009/05/08
    /// </summary>
   public class OfficeThingsStorageDBHelper
    {
        #region 添加入库库存信息
        /// <summary>
        /// 添加入库库存信息
        /// </summary>
        /// <param name="OfficeThingsBuyM">入库单主信息</param>
        /// <param name="OfficeThingsInStorageInfos">入库详细信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool InserInstorageInfo(OfficeThingsBuyModel OfficeThingsBuyM, string OfficeThingsInStorageInfos)
        {
            try
            {
                #region 添加入库库存信息sql语句
                StringBuilder OfficeThingsBuySql = new StringBuilder();
                OfficeThingsBuySql.AppendLine("INSERT INTO officedba.OfficeThingsBuy");
                OfficeThingsBuySql.AppendLine("(CompanyCD");
                OfficeThingsBuySql.AppendLine(",BuyRecordNo     ");
                OfficeThingsBuySql.AppendLine(",FromType     ");
                
                OfficeThingsBuySql.AppendLine(",Title");
                OfficeThingsBuySql.AppendLine(",BuyerID   ");
                OfficeThingsBuySql.AppendLine(",BuyDeptID  ");
                OfficeThingsBuySql.AppendLine(",BuyDate    ");
                OfficeThingsBuySql.AppendLine(",ToWarehouseDate    ");
                OfficeThingsBuySql.AppendLine(",TotalCount    ");
                OfficeThingsBuySql.AppendLine(",TotalPrice    ");
                OfficeThingsBuySql.AppendLine(",BillStatus");
                OfficeThingsBuySql.AppendLine(",Creator");
                OfficeThingsBuySql.AppendLine(",CreateDate");
                OfficeThingsBuySql.AppendLine(",Remark");
                OfficeThingsBuySql.AppendLine(",ModifiedDate");
                OfficeThingsBuySql.AppendLine(",ModifiedUserID)");
                OfficeThingsBuySql.AppendLine(" values ");
                OfficeThingsBuySql.AppendLine("(@CompanyCD");
                OfficeThingsBuySql.AppendLine(",@BuyRecordNo     ");
                OfficeThingsBuySql.AppendLine(",@FromType     ");
                OfficeThingsBuySql.AppendLine(",@Title");
                OfficeThingsBuySql.AppendLine(",@BuyerID   ");
                OfficeThingsBuySql.AppendLine(",@BuyDeptID  ");
                OfficeThingsBuySql.AppendLine(",@BuyDate    ");
                OfficeThingsBuySql.AppendLine(",@ToWarehouseDate    ");
                OfficeThingsBuySql.AppendLine(",@TotalCount    ");
                OfficeThingsBuySql.AppendLine(",@TotalPrice    ");
                OfficeThingsBuySql.AppendLine(",@BillStatus");
                OfficeThingsBuySql.AppendLine(",@Creator");
                OfficeThingsBuySql.AppendLine(",@CreateDate");
                OfficeThingsBuySql.AppendLine(",@Remark");
                OfficeThingsBuySql.AppendLine(",@ModifiedDate");
                OfficeThingsBuySql.AppendLine(",@ModifiedUserID)");
                #endregion

                #region 设置参数
                SqlParameter[] paramgas = new SqlParameter[16];
                paramgas[0] = SqlHelper.GetParameter("@CompanyCD", OfficeThingsBuyM.CompanyCD);
                paramgas[1] = SqlHelper.GetParameter("@BuyRecordNo", OfficeThingsBuyM.BuyRecordNo);
                paramgas[2] = SqlHelper.GetParameter("@Title", OfficeThingsBuyM.Title);
                paramgas[3] = SqlHelper.GetParameter("@BuyerID", OfficeThingsBuyM.BuyerID);
                paramgas[4] = SqlHelper.GetParameter("@BuyDeptID", OfficeThingsBuyM.BuyDeptID);
                paramgas[5] = SqlHelper.GetParameter("@BuyDate", OfficeThingsBuyM.BuyDate);
                paramgas[6] = SqlHelper.GetParameter("@ToWarehouseDate", OfficeThingsBuyM.ToWarehouseDate);
                paramgas[7] = SqlHelper.GetParameter("@TotalCount", OfficeThingsBuyM.TotalCount);
                paramgas[8] = SqlHelper.GetParameter("@TotalPrice", OfficeThingsBuyM.TotalPrice);
                paramgas[9] = SqlHelper.GetParameter("@BillStatus", OfficeThingsBuyM.BillStatus);
                paramgas[10] = SqlHelper.GetParameter("@Creator", OfficeThingsBuyM.Creator);
                paramgas[11] = SqlHelper.GetParameter("@CreateDate", OfficeThingsBuyM.CreateDate);
                paramgas[12] = SqlHelper.GetParameter("@Remark", OfficeThingsBuyM.Remark);
                paramgas[13] = SqlHelper.GetParameter("@ModifiedDate", OfficeThingsBuyM.ModifiedDate);
                paramgas[14] = SqlHelper.GetParameter("@ModifiedUserID", OfficeThingsBuyM.ModifiedUserID);
                paramgas[15] = SqlHelper.GetParameter("@FromType", OfficeThingsBuyM.FromType);
           
                #endregion
                return InsertAll(OfficeThingsBuySql.ToString(), OfficeThingsInStorageInfos, OfficeThingsBuyM.BuyRecordNo, OfficeThingsBuyM.CompanyCD, OfficeThingsBuyM.ModifiedUserID, paramgas);
            }
            catch 
            {
                return false;
            }
        }

        /// <summary>
        /// 添加入库库存和入库单明细信息
        /// </summary>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool InsertAll(string OfficeThingsBuySql, string OfficeThingsInStorageInfos, string BuyRecordNo, string CompanyCD, string UserID, SqlParameter[] paramgas)
        {
            try
            {
                OfficeThingsBuyDetailModel OfficeThingsBuyDetailM = new OfficeThingsBuyDetailModel();
                string[] OfficeThingsInStorageArrary = OfficeThingsInStorageInfos.Split('|');
                SqlCommand[] comms = new SqlCommand[OfficeThingsInStorageArrary.Length]; //申明cmd数组(主表，明细表，每条明细生成一条库存记录)
                SqlCommand OfficeThingsBuyCom = new SqlCommand(OfficeThingsBuySql.ToString());
                OfficeThingsBuyCom.Parameters.AddRange(paramgas);
                comms[0] = OfficeThingsBuyCom; 

                string recorditems = "";
                string[] gasfield = null;

                for (int i = 1; i < OfficeThingsInStorageArrary.Length; i++) //循环数组
                {
                    recorditems = OfficeThingsInStorageArrary[i].ToString();
                    gasfield = recorditems.Split(',');

                    string ThingNo = gasfield[0].ToString();//用品编号
                    string BuyCount = gasfield[1].ToString();//采购数量
                    string UnitPrice = gasfield[2].ToString();//采购单价
                    string BuyMoney = gasfield[3].ToString();//采购金额
                    string Provider = gasfield[4].ToString();//供应商

                    string FromBillID = gasfield[5].ToString();//采购金额
                    string FromSortNo = gasfield[6].ToString();//供应商


                    OfficeThingsBuyDetailM.BuyCount = Convert.ToDecimal(BuyCount);
                    OfficeThingsBuyDetailM.BuyMoney = Convert.ToDecimal(BuyMoney);
                    OfficeThingsBuyDetailM.BuyRecordNo = BuyRecordNo;
                    OfficeThingsBuyDetailM.CompanyCD = CompanyCD;
                    OfficeThingsBuyDetailM.ModifiedDate = System.DateTime.Now;
                    OfficeThingsBuyDetailM.ModifiedUserID = UserID;
                    OfficeThingsBuyDetailM.Provider = Provider;
                    OfficeThingsBuyDetailM.ThingNo = ThingNo;
                    OfficeThingsBuyDetailM.UnitPrice =Convert.ToDecimal(UnitPrice);

                    OfficeThingsBuyDetailM.FromBillID = FromBillID;
                    OfficeThingsBuyDetailM.FromSortNo = FromSortNo;

                    #region 拼写添加入库明细信息sql语句
                    StringBuilder OfficeThingsBuyDetailSql = new StringBuilder();
                    OfficeThingsBuyDetailSql.AppendLine("INSERT INTO officedba.OfficeThingsBuyDetail");
                    OfficeThingsBuyDetailSql.AppendLine("(CompanyCD");
                    OfficeThingsBuyDetailSql.AppendLine(",BuyRecordNo     ");
                    OfficeThingsBuyDetailSql.AppendLine(",ThingNo");
                    OfficeThingsBuyDetailSql.AppendLine(",Provider   ");
                    OfficeThingsBuyDetailSql.AppendLine(",UnitPrice  ");
                    OfficeThingsBuyDetailSql.AppendLine(",BuyCount    ");
                    OfficeThingsBuyDetailSql.AppendLine(",BuyMoney    ");

                    OfficeThingsBuyDetailSql.AppendLine(",FromBillID    ");
                    OfficeThingsBuyDetailSql.AppendLine(",FromSortNo    ");


                    OfficeThingsBuyDetailSql.AppendLine(",ModifiedDate");
                    OfficeThingsBuyDetailSql.AppendLine(",ModifiedUserID)");
                    OfficeThingsBuyDetailSql.AppendLine(" values ");
                    OfficeThingsBuyDetailSql.AppendLine("(@CompanyCD");
                    OfficeThingsBuyDetailSql.AppendLine(",@BuyRecordNo     ");
                    OfficeThingsBuyDetailSql.AppendLine(",@ThingNo");
                    OfficeThingsBuyDetailSql.AppendLine(",@Provider   ");
                    OfficeThingsBuyDetailSql.AppendLine(",@UnitPrice  ");
                    OfficeThingsBuyDetailSql.AppendLine(",@BuyCount    ");
                    OfficeThingsBuyDetailSql.AppendLine(",@BuyMoney    ");

                    OfficeThingsBuyDetailSql.AppendLine(",@FromBillID    ");
                    OfficeThingsBuyDetailSql.AppendLine(",@FromSortNo    ");

                    OfficeThingsBuyDetailSql.AppendLine(",@ModifiedDate");
                    OfficeThingsBuyDetailSql.AppendLine(",@ModifiedUserID)");
                    #endregion
                    #region 设置参数
                    SqlParameter[] OfficeThingsBuyDetailParams = new SqlParameter[11];
                    OfficeThingsBuyDetailParams[0] = SqlHelper.GetParameter("@CompanyCD", OfficeThingsBuyDetailM.CompanyCD);
                    OfficeThingsBuyDetailParams[1] = SqlHelper.GetParameter("@BuyRecordNo", OfficeThingsBuyDetailM.BuyRecordNo);
                    OfficeThingsBuyDetailParams[2] = SqlHelper.GetParameter("@ThingNo", OfficeThingsBuyDetailM.ThingNo);
                    OfficeThingsBuyDetailParams[3] = SqlHelper.GetParameter("@Provider", OfficeThingsBuyDetailM.Provider);
                    OfficeThingsBuyDetailParams[4] = SqlHelper.GetParameter("@UnitPrice", OfficeThingsBuyDetailM.UnitPrice);
                    OfficeThingsBuyDetailParams[5] = SqlHelper.GetParameter("@BuyCount", OfficeThingsBuyDetailM.BuyCount);
                    OfficeThingsBuyDetailParams[6] = SqlHelper.GetParameter("@BuyMoney", OfficeThingsBuyDetailM.BuyMoney);
                    OfficeThingsBuyDetailParams[7] = SqlHelper.GetParameter("@ModifiedDate", OfficeThingsBuyDetailM.ModifiedDate);
                    OfficeThingsBuyDetailParams[8] = SqlHelper.GetParameter("@ModifiedUserID", OfficeThingsBuyDetailM.ModifiedUserID);


                    OfficeThingsBuyDetailParams[9] = SqlHelper.GetParameter("@FromBillID", OfficeThingsBuyDetailM.FromBillID);
                    OfficeThingsBuyDetailParams[10] = SqlHelper.GetParameter("@FromSortNo", OfficeThingsBuyDetailM.FromSortNo);


                    SqlCommand OfficeThingsBuyDetailComInfo = new SqlCommand(OfficeThingsBuyDetailSql.ToString());
                    OfficeThingsBuyDetailComInfo.Parameters.AddRange(OfficeThingsBuyDetailParams);
                    comms[i] = OfficeThingsBuyDetailComInfo;
                    #endregion
                    
                    //int RetOfficeThingsVal = OfficeThingsStorageIsExist(ThingNo, CompanyCD);//库存是否有此用品编号的记录，有更新库存，无插入新的库存记录
                    //OfficeThingsStorageModel OfficeThingsStorageM = new OfficeThingsStorageModel();
                    //if (RetOfficeThingsVal == 0)//无此编号的库存，插入库存信息
                    //{
                    //    #region 拼写添加入库库存信息sql语句
                    //    StringBuilder OfficeThingsStorageSql = new StringBuilder();
                    //    OfficeThingsStorageSql.AppendLine("INSERT INTO officedba.OfficeThingsStorage");
                    //    OfficeThingsStorageSql.AppendLine("(CompanyCD");
                    //    OfficeThingsStorageSql.AppendLine(",ThingNo     ");
                    //    OfficeThingsStorageSql.AppendLine(",TotalCount");
                    //    OfficeThingsStorageSql.AppendLine(",SurplusCount   ");
                    //    OfficeThingsStorageSql.AppendLine(",UsedCount  ");
                    //    OfficeThingsStorageSql.AppendLine(",ModifiedDate");
                    //    OfficeThingsStorageSql.AppendLine(",ModifiedUserID)");
                    //    OfficeThingsStorageSql.AppendLine(" values ");
                    //    OfficeThingsStorageSql.AppendLine("(@CompanyCD");
                    //    OfficeThingsStorageSql.AppendLine(",@ThingNo     ");
                    //    OfficeThingsStorageSql.AppendLine(",@TotalCount");
                    //    OfficeThingsStorageSql.AppendLine(",@SurplusCount   ");
                    //    OfficeThingsStorageSql.AppendLine(",@UsedCount  ");
                    //    OfficeThingsStorageSql.AppendLine(",@ModifiedDate");
                    //    OfficeThingsStorageSql.AppendLine(",@ModifiedUserID)");
                    //    #endregion
                    //    #region 设置参数
                    //    SqlParameter[] OfficeThingsStorageParams = new SqlParameter[7];
                    //    OfficeThingsStorageParams[0] = SqlHelper.GetParameter("@CompanyCD", OfficeThingsBuyDetailM.CompanyCD);
                    //    OfficeThingsStorageParams[1] = SqlHelper.GetParameter("@ThingNo", OfficeThingsBuyDetailM.ThingNo);
                    //    OfficeThingsStorageParams[2] = SqlHelper.GetParameter("@TotalCount", OfficeThingsBuyDetailM.BuyCount);
                    //    OfficeThingsStorageParams[3] = SqlHelper.GetParameter("@SurplusCount", OfficeThingsBuyDetailM.BuyCount);
                    //    OfficeThingsStorageParams[4] = SqlHelper.GetParameter("@UsedCount", 0);
                    //    OfficeThingsStorageParams[5] = SqlHelper.GetParameter("@ModifiedDate", OfficeThingsBuyDetailM.ModifiedDate);
                    //    OfficeThingsStorageParams[6] = SqlHelper.GetParameter("@ModifiedUserID", OfficeThingsBuyDetailM.ModifiedUserID);
                    //    #endregion
                    //    SqlCommand OfficeThingsStorageComInfo = new SqlCommand(OfficeThingsStorageSql.ToString());
                    //    OfficeThingsStorageComInfo.Parameters.AddRange(OfficeThingsStorageParams);
                    //    comms[i+1] = OfficeThingsStorageComInfo;
                    //}
                    //else //有此编号，更新库存信息
                    //{
                    //    #region 拼写添加入库库存信息sql语句
                    //    StringBuilder OfficeThingsStorageSql = new StringBuilder();
                    //    OfficeThingsStorageSql.AppendLine("UPDATE officedba.OfficeThingsStorage");
                    //    OfficeThingsStorageSql.AppendLine("SET TotalCount=TotalCount+@TotalCount");
                    //    OfficeThingsStorageSql.AppendLine(",SurplusCount=SurplusCount+@TotalCount1   ");
                    //    OfficeThingsStorageSql.AppendLine(",ModifiedDate=@ModifiedDate");
                    //    OfficeThingsStorageSql.AppendLine(",ModifiedUserID=@ModifiedUserID");
                    //    OfficeThingsStorageSql.AppendLine(" WHERE ThingNo=@ThingNo AND CompanyCD=@CompanyCD ");
                    //    #endregion
                    //    #region 设置参数
                    //    SqlParameter[] OfficeThingsStorageParams = new SqlParameter[6];
                    //    OfficeThingsStorageParams[0] = SqlHelper.GetParameter("@TotalCount", OfficeThingsBuyDetailM.BuyCount);
                    //    OfficeThingsStorageParams[1] = SqlHelper.GetParameter("@TotalCount1", OfficeThingsBuyDetailM.BuyCount);
                    //    OfficeThingsStorageParams[2] = SqlHelper.GetParameter("@ModifiedDate", OfficeThingsBuyDetailM.ModifiedDate);
                    //    OfficeThingsStorageParams[3] = SqlHelper.GetParameter("@ModifiedUserID", OfficeThingsBuyDetailM.ModifiedUserID);
                    //    OfficeThingsStorageParams[4] = SqlHelper.GetParameter("@ThingNo", OfficeThingsBuyDetailM.ThingNo);
                    //    OfficeThingsStorageParams[5] = SqlHelper.GetParameter("@CompanyCD", OfficeThingsBuyDetailM.CompanyCD);
                       

                    //    #endregion
                    //    SqlCommand OfficeThingsStorageComInfo = new SqlCommand(OfficeThingsStorageSql.ToString());
                    //    OfficeThingsStorageComInfo.Parameters.AddRange(OfficeThingsStorageParams);
                    //    comms[i + 1] = OfficeThingsStorageComInfo;
                    //}
                }
                //执行
                SqlHelper.ExecuteTransForList(comms);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch 
            {
                return false;
            }
        }
        #endregion
        #region 修改入库库存信息
        /// <summary>
        /// 修改入库库存信息
        /// </summary>
        /// <param name="OfficeThingsBuyM">入库单主信息</param>
        /// <param name="OfficeThingsInStorageInfos">入库详细信息</param>
        /// <returns>更新是否成功 false:失败，true:成功</returns>
        public static bool UpdateInstorageInfo(OfficeThingsBuyModel OfficeThingsBuyM, string OfficeThingsInStorageInfos)
        {
            try
            {
                #region 添加入库库存信息sql语句
                StringBuilder OfficeThingsBuySql = new StringBuilder();
                OfficeThingsBuySql.AppendLine("UPDATE officedba.OfficeThingsBuy");
                OfficeThingsBuySql.AppendLine("SET Title=@Title");
                OfficeThingsBuySql.AppendLine(",FromType=@FromType   ");
                
                OfficeThingsBuySql.AppendLine(",BuyerID=@BuyerID   ");
                OfficeThingsBuySql.AppendLine(",BuyDeptID=@BuyDeptID  ");
                OfficeThingsBuySql.AppendLine(",BuyDate=@BuyDate    ");
                OfficeThingsBuySql.AppendLine(",ToWarehouseDate=@ToWarehouseDate    ");
                OfficeThingsBuySql.AppendLine(",TotalCount=@TotalCount    ");
                OfficeThingsBuySql.AppendLine(",TotalPrice=@TotalPrice    ");
                OfficeThingsBuySql.AppendLine(",BillStatus=@BillStatus");
                //OfficeThingsBuySql.AppendLine(",Creator=@Creator");
                //OfficeThingsBuySql.AppendLine(",CreateDate=@CreateDate");
                OfficeThingsBuySql.AppendLine(",Remark=@Remark");
                OfficeThingsBuySql.AppendLine(",ModifiedDate=@ModifiedDate");
                OfficeThingsBuySql.AppendLine(",ModifiedUserID=@ModifiedUserID");
                OfficeThingsBuySql.AppendLine(" WHERE  CompanyCD=@CompanyCD AND BuyRecordNo=@BuyRecordNo");
                #endregion

                #region 设置参数
                SqlParameter[] paramgas = new SqlParameter[14];
                paramgas[0] = SqlHelper.GetParameter("@Title", OfficeThingsBuyM.Title);
                paramgas[1] = SqlHelper.GetParameter("@BuyerID", OfficeThingsBuyM.BuyerID);
                paramgas[2] = SqlHelper.GetParameter("@BuyDeptID", OfficeThingsBuyM.BuyDeptID);
                paramgas[3] = SqlHelper.GetParameter("@BuyDate", OfficeThingsBuyM.BuyDate);
                paramgas[4] = SqlHelper.GetParameter("@ToWarehouseDate", OfficeThingsBuyM.ToWarehouseDate);
                paramgas[5] = SqlHelper.GetParameter("@TotalCount", OfficeThingsBuyM.TotalCount);
                paramgas[6] = SqlHelper.GetParameter("@TotalPrice", OfficeThingsBuyM.TotalPrice);
                paramgas[7] = SqlHelper.GetParameter("@BillStatus", OfficeThingsBuyM.BillStatus);
                //paramgas[8] = SqlHelper.GetParameter("@Creator", OfficeThingsBuyM.Creator);
               // paramgas[9] = SqlHelper.GetParameter("@CreateDate", OfficeThingsBuyM.CreateDate);
                paramgas[8] = SqlHelper.GetParameter("@Remark", OfficeThingsBuyM.Remark);
                paramgas[9] = SqlHelper.GetParameter("@ModifiedDate", OfficeThingsBuyM.ModifiedDate);
                paramgas[10] = SqlHelper.GetParameter("@ModifiedUserID", OfficeThingsBuyM.ModifiedUserID);
                paramgas[11] = SqlHelper.GetParameter("@CompanyCD", OfficeThingsBuyM.CompanyCD);
                paramgas[12] = SqlHelper.GetParameter("@BuyRecordNo", OfficeThingsBuyM.BuyRecordNo);
                paramgas[13] = SqlHelper.GetParameter("@FromType", OfficeThingsBuyM.FromType);
                #endregion
                return UpdateAll(OfficeThingsBuySql.ToString(), OfficeThingsInStorageInfos, OfficeThingsBuyM.BuyRecordNo, OfficeThingsBuyM.CompanyCD, OfficeThingsBuyM.ModifiedUserID, paramgas);
            }
            catch 
            {
                return false;
            }
        }

        /// <summary>
        /// 修改入库库存和入库单明细信息
        /// </summary>
        /// <returns>更新是否成功 false:失败，true:成功</returns>
        public static bool UpdateAll(string OfficeThingsBuySql, string OfficeThingsInStorageInfos, string BuyRecordNo, string CompanyCD, string UserID, SqlParameter[] paramgas)
        {
            try
            {
                OfficeThingsBuyDetailModel OfficeThingsBuyDetailM = new OfficeThingsBuyDetailModel();
                string[] OfficeThingsInStorageArrary = OfficeThingsInStorageInfos.Split('|');
                SqlCommand[] comms = new SqlCommand[OfficeThingsInStorageArrary.Length+1]; //申明cmd数组(主表，明细表，每条明细生成一条库存记录)
                SqlCommand OfficeThingsBuyCom = new SqlCommand(OfficeThingsBuySql.ToString());
                OfficeThingsBuyCom.Parameters.AddRange(paramgas);
                comms[0] = OfficeThingsBuyCom;

                #region 首先删除此入库单的明细
                string OfficeThingsDelBuyDetailSql = "DELETE FROM officedba.OfficeThingsBuyDetail WHERE BuyRecordNo='" + BuyRecordNo + "' AND CompanyCD='"+CompanyCD+"'";
                //SqlParameter[] OfficeThingsDelBuyDetailParams = new SqlParameter[1];
               // OfficeThingsDelBuyDetailParams[0] = SqlHelper.GetParameter("@BuyRecordNo1", BuyRecordNo);
                //OfficeThingsDelBuyDetailParams[0] = SqlHelper.GetParameter("@CompanyCD1", CompanyCD);
                SqlCommand OfficeThingsDelBuyDetailCom = new SqlCommand(OfficeThingsDelBuyDetailSql.ToString());
                //OfficeThingsBuyCom.Parameters.AddRange(OfficeThingsDelBuyDetailParams);
                comms[1] = OfficeThingsDelBuyDetailCom;
                #endregion
               
                string recorditems = "";
                string[] gasfield = null;

                for (int i = 1; i < OfficeThingsInStorageArrary.Length; i++) //循环数组
                {
                    recorditems = OfficeThingsInStorageArrary[i].ToString();
                    gasfield = recorditems.Split(',');

                    string ThingNo = gasfield[0].ToString();//用品编号
                    string BuyCount = gasfield[1].ToString();//采购数量
                    string UnitPrice = gasfield[2].ToString();//采购单价
                    string BuyMoney = gasfield[3].ToString();//采购金额
                    string Provider = gasfield[4].ToString();//供应商

                    string FromBillID = gasfield[5].ToString();//采购金额
                    string FromSortNo = gasfield[6].ToString();//供应商

                    OfficeThingsBuyDetailM.BuyCount = Convert.ToDecimal(BuyCount);
                    OfficeThingsBuyDetailM.BuyMoney = Convert.ToDecimal(BuyMoney);
                    OfficeThingsBuyDetailM.BuyRecordNo = BuyRecordNo;
                    OfficeThingsBuyDetailM.CompanyCD = CompanyCD;
                    OfficeThingsBuyDetailM.ModifiedDate = System.DateTime.Now;
                    OfficeThingsBuyDetailM.ModifiedUserID = UserID;
                    OfficeThingsBuyDetailM.Provider = Provider;
                    OfficeThingsBuyDetailM.ThingNo = ThingNo;
                    OfficeThingsBuyDetailM.UnitPrice = Convert.ToDecimal(UnitPrice);

                    OfficeThingsBuyDetailM.FromBillID = FromBillID;
                    OfficeThingsBuyDetailM.FromSortNo = FromSortNo;
                    #region 拼写添加入库明细信息sql语句
                    StringBuilder OfficeThingsBuyDetailSql = new StringBuilder();
                    OfficeThingsBuyDetailSql.AppendLine("INSERT INTO officedba.OfficeThingsBuyDetail");
                    OfficeThingsBuyDetailSql.AppendLine("(CompanyCD");
                    OfficeThingsBuyDetailSql.AppendLine(",BuyRecordNo     ");
                    OfficeThingsBuyDetailSql.AppendLine(",ThingNo");
                    OfficeThingsBuyDetailSql.AppendLine(",Provider   ");
                    OfficeThingsBuyDetailSql.AppendLine(",UnitPrice  ");
                    OfficeThingsBuyDetailSql.AppendLine(",BuyCount    ");
                    OfficeThingsBuyDetailSql.AppendLine(",BuyMoney    ");

                    OfficeThingsBuyDetailSql.AppendLine(",FromBillID    ");
                    OfficeThingsBuyDetailSql.AppendLine(",FromSortNo    ");

                    OfficeThingsBuyDetailSql.AppendLine(",ModifiedDate");
                    OfficeThingsBuyDetailSql.AppendLine(",ModifiedUserID)");
                    OfficeThingsBuyDetailSql.AppendLine(" values ");
                    OfficeThingsBuyDetailSql.AppendLine("(@CompanyCD");
                    OfficeThingsBuyDetailSql.AppendLine(",@BuyRecordNo     ");
                    OfficeThingsBuyDetailSql.AppendLine(",@ThingNo");
                    OfficeThingsBuyDetailSql.AppendLine(",@Provider   ");
                    OfficeThingsBuyDetailSql.AppendLine(",@UnitPrice  ");
                    OfficeThingsBuyDetailSql.AppendLine(",@BuyCount    ");
                    OfficeThingsBuyDetailSql.AppendLine(",@BuyMoney    ");

                    OfficeThingsBuyDetailSql.AppendLine(",@FromBillID    ");
                    OfficeThingsBuyDetailSql.AppendLine(",@FromSortNo    ");
                    OfficeThingsBuyDetailSql.AppendLine(",@ModifiedDate");
                    OfficeThingsBuyDetailSql.AppendLine(",@ModifiedUserID)");
                    #endregion
                    #region 设置参数
                    SqlParameter[] OfficeThingsBuyDetailParams = new SqlParameter[11];
                    OfficeThingsBuyDetailParams[0] = SqlHelper.GetParameter("@CompanyCD", OfficeThingsBuyDetailM.CompanyCD);
                    OfficeThingsBuyDetailParams[1] = SqlHelper.GetParameter("@BuyRecordNo", OfficeThingsBuyDetailM.BuyRecordNo);
                    OfficeThingsBuyDetailParams[2] = SqlHelper.GetParameter("@ThingNo", OfficeThingsBuyDetailM.ThingNo);
                    OfficeThingsBuyDetailParams[3] = SqlHelper.GetParameter("@Provider", OfficeThingsBuyDetailM.Provider);
                    OfficeThingsBuyDetailParams[4] = SqlHelper.GetParameter("@UnitPrice", OfficeThingsBuyDetailM.UnitPrice);
                    OfficeThingsBuyDetailParams[5] = SqlHelper.GetParameter("@BuyCount", OfficeThingsBuyDetailM.BuyCount);
                    OfficeThingsBuyDetailParams[6] = SqlHelper.GetParameter("@BuyMoney", OfficeThingsBuyDetailM.BuyMoney);
                    OfficeThingsBuyDetailParams[7] = SqlHelper.GetParameter("@ModifiedDate", OfficeThingsBuyDetailM.ModifiedDate);
                    OfficeThingsBuyDetailParams[8] = SqlHelper.GetParameter("@ModifiedUserID", OfficeThingsBuyDetailM.ModifiedUserID);
                    OfficeThingsBuyDetailParams[9] = SqlHelper.GetParameter("@FromBillID", OfficeThingsBuyDetailM.FromBillID);
                    OfficeThingsBuyDetailParams[10] = SqlHelper.GetParameter("@FromSortNo", OfficeThingsBuyDetailM.FromSortNo);
                    SqlCommand OfficeThingsBuyDetailComInfo = new SqlCommand(OfficeThingsBuyDetailSql.ToString());
                    OfficeThingsBuyDetailComInfo.Parameters.AddRange(OfficeThingsBuyDetailParams);
                    comms[i+1] = OfficeThingsBuyDetailComInfo;
                    #endregion

                    //int RetOfficeThingsVal = OfficeThingsStorageIsExist(ThingNo, CompanyCD);//库存是否有此用品编号的记录，有更新库存，无插入新的库存记录
                    //OfficeThingsStorageModel OfficeThingsStorageM = new OfficeThingsStorageModel();
                    //if (RetOfficeThingsVal == 0)//无此编号的库存，插入库存信息
                    //{
                    //    #region 拼写添加入库库存信息sql语句
                    //    StringBuilder OfficeThingsStorageSql = new StringBuilder();
                    //    OfficeThingsStorageSql.AppendLine("INSERT INTO officedba.OfficeThingsStorage");
                    //    OfficeThingsStorageSql.AppendLine("(CompanyCD");
                    //    OfficeThingsStorageSql.AppendLine(",ThingNo     ");
                    //    OfficeThingsStorageSql.AppendLine(",TotalCount");
                    //    OfficeThingsStorageSql.AppendLine(",SurplusCount   ");
                    //    OfficeThingsStorageSql.AppendLine(",UsedCount  ");
                    //    OfficeThingsStorageSql.AppendLine(",ModifiedDate");
                    //    OfficeThingsStorageSql.AppendLine(",ModifiedUserID)");
                    //    OfficeThingsStorageSql.AppendLine(" values ");
                    //    OfficeThingsStorageSql.AppendLine("(@CompanyCD");
                    //    OfficeThingsStorageSql.AppendLine(",@ThingNo     ");
                    //    OfficeThingsStorageSql.AppendLine(",@TotalCount");
                    //    OfficeThingsStorageSql.AppendLine(",@SurplusCount   ");
                    //    OfficeThingsStorageSql.AppendLine(",@UsedCount  ");
                    //    OfficeThingsStorageSql.AppendLine(",@ModifiedDate");
                    //    OfficeThingsStorageSql.AppendLine(",@ModifiedUserID)");
                    //    #endregion
                    //    #region 设置参数
                    //    SqlParameter[] OfficeThingsStorageParams = new SqlParameter[7];
                    //    OfficeThingsStorageParams[0] = SqlHelper.GetParameter("@CompanyCD", OfficeThingsBuyDetailM.CompanyCD);
                    //    OfficeThingsStorageParams[1] = SqlHelper.GetParameter("@ThingNo", OfficeThingsBuyDetailM.ThingNo);
                    //    OfficeThingsStorageParams[2] = SqlHelper.GetParameter("@TotalCount", OfficeThingsBuyDetailM.BuyCount);
                    //    OfficeThingsStorageParams[3] = SqlHelper.GetParameter("@SurplusCount", OfficeThingsBuyDetailM.BuyCount);
                    //    OfficeThingsStorageParams[4] = SqlHelper.GetParameter("@UsedCount", 0);
                    //    OfficeThingsStorageParams[5] = SqlHelper.GetParameter("@ModifiedDate", OfficeThingsBuyDetailM.ModifiedDate);
                    //    OfficeThingsStorageParams[6] = SqlHelper.GetParameter("@ModifiedUserID", OfficeThingsBuyDetailM.ModifiedUserID);
                    //    #endregion
                    //    SqlCommand OfficeThingsStorageComInfo = new SqlCommand(OfficeThingsStorageSql.ToString());
                    //    OfficeThingsStorageComInfo.Parameters.AddRange(OfficeThingsStorageParams);
                    //    comms[i + 2] = OfficeThingsStorageComInfo;
                    //}
                    //else //有此编号，更新库存信息
                    //{
                    //    #region 拼写添加入库库存信息sql语句
                    //    StringBuilder OfficeThingsStorageSql = new StringBuilder();
                    //    OfficeThingsStorageSql.AppendLine("UPDATE officedba.OfficeThingsStorage");
                    //    OfficeThingsStorageSql.AppendLine("SET TotalCount=TotalCount+@TotalCount");
                    //    OfficeThingsStorageSql.AppendLine(",SurplusCount=SurplusCount+@TotalCount1   ");
                    //    OfficeThingsStorageSql.AppendLine(",ModifiedDate=@ModifiedDate");
                    //    OfficeThingsStorageSql.AppendLine(",ModifiedUserID=@ModifiedUserID");
                    //    OfficeThingsStorageSql.AppendLine(" WHERE ThingNo=@ThingNo AND CompanyCD=@CompanyCD ");
                    //    #endregion
                    //    #region 设置参数
                    //    SqlParameter[] OfficeThingsStorageParams = new SqlParameter[6];
                    //    OfficeThingsStorageParams[0] = SqlHelper.GetParameter("@TotalCount", OfficeThingsBuyDetailM.BuyCount);
                    //    OfficeThingsStorageParams[1] = SqlHelper.GetParameter("@TotalCount1", OfficeThingsBuyDetailM.BuyCount);
                    //    OfficeThingsStorageParams[2] = SqlHelper.GetParameter("@ModifiedDate", OfficeThingsBuyDetailM.ModifiedDate);
                    //    OfficeThingsStorageParams[3] = SqlHelper.GetParameter("@ModifiedUserID", OfficeThingsBuyDetailM.ModifiedUserID);
                    //    OfficeThingsStorageParams[4] = SqlHelper.GetParameter("@ThingNo", OfficeThingsBuyDetailM.ThingNo);
                    //    OfficeThingsStorageParams[5] = SqlHelper.GetParameter("@CompanyCD", OfficeThingsBuyDetailM.CompanyCD);


                    //    #endregion
                    //    SqlCommand OfficeThingsStorageComInfo = new SqlCommand(OfficeThingsStorageSql.ToString());
                    //    OfficeThingsStorageComInfo.Parameters.AddRange(OfficeThingsStorageParams);
                    //    comms[i + 2] = OfficeThingsStorageComInfo;
                    //}
                }
                //执行
                SqlHelper.ExecuteTransForList(comms);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch 
            {
                return false;
            }
        }
        #endregion

        #region 入库确认
        /// <summary>
        /// 添加入库库存信息
        /// </summary>
        /// <param name="OfficeThingsBuyM">入库单主信息</param>
        /// <param name="OfficeThingsInStorageInfos">入库详细信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool ConfirmInstorageInfo(OfficeThingsBuyModel OfficeThingsBuyM, string OfficeThingsInStorageInfos)
        {
            try
            {
                #region 添加入库库存信息sql语句
                StringBuilder OfficeThingsBuySql = new StringBuilder();
                OfficeThingsBuySql.AppendLine("Update officedba.OfficeThingsBuy");
                OfficeThingsBuySql.AppendLine("SET BillStatus=@BillStatus");
                OfficeThingsBuySql.AppendLine(",Confirmor=@Confirmor     ");
                OfficeThingsBuySql.AppendLine(",ConfirmDate=@ConfirmDate     ");
                OfficeThingsBuySql.AppendLine(",ModifiedDate=@ModifiedDate");
                OfficeThingsBuySql.AppendLine(",ModifiedUserID=@ModifiedUserID");
                OfficeThingsBuySql.AppendLine("  WHERE  BuyRecordNo=@BuyRecordNo AND CompanyCD=@CompanyCD");
                #endregion

                #region 设置参数
                SqlParameter[] paramgas = new SqlParameter[7];
                paramgas[0] = SqlHelper.GetParameter("@BillStatus", OfficeThingsBuyM.BillStatus);
                paramgas[1] = SqlHelper.GetParameter("@Confirmor", OfficeThingsBuyM.Confirmor);
                paramgas[2] = SqlHelper.GetParameter("@ConfirmDate", OfficeThingsBuyM.ConfirmDate);
                paramgas[3] = SqlHelper.GetParameter("@ModifiedDate", OfficeThingsBuyM.ModifiedDate);
                paramgas[4] = SqlHelper.GetParameter("@ModifiedUserID", OfficeThingsBuyM.ModifiedUserID);
                paramgas[5] = SqlHelper.GetParameter("@BuyRecordNo", OfficeThingsBuyM.BuyRecordNo);
                paramgas[6] = SqlHelper.GetParameter("@CompanyCD", OfficeThingsBuyM.CompanyCD);
                #endregion
                return ConfirmAll(OfficeThingsBuySql.ToString(), OfficeThingsInStorageInfos, OfficeThingsBuyM.BuyRecordNo, OfficeThingsBuyM.CompanyCD, OfficeThingsBuyM.ModifiedUserID, paramgas);
            }
            catch 
            {
                return false;
            }
        }

        /// <summary>
        /// 入库确认
        /// </summary>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool ConfirmAll(string OfficeThingsBuySql, string OfficeThingsInStorageInfos, string BuyRecordNo, string CompanyCD, string UserID, SqlParameter[] paramgas)
        {
            try
            {
                ArrayList listADD = new ArrayList();
                OfficeThingsBuyDetailModel OfficeThingsBuyDetailM = new OfficeThingsBuyDetailModel();
                string[] OfficeThingsInStorageArrary = OfficeThingsInStorageInfos.Split('|');
                SqlCommand[] comms = new SqlCommand[OfficeThingsInStorageArrary.Length]; //申明cmd数组(主表，明细表，每条明细生成一条库存记录)
                SqlCommand OfficeThingsBuyCom = new SqlCommand(OfficeThingsBuySql.ToString());
                OfficeThingsBuyCom.Parameters.AddRange(paramgas);
                //comms[0] = OfficeThingsBuyCom;
                listADD.Add(OfficeThingsBuyCom);
                string recorditems = "";
                string[] gasfield = null;

                for (int i = 1; i < OfficeThingsInStorageArrary.Length; i++) //循环数组
                {
                    recorditems = OfficeThingsInStorageArrary[i].ToString();
                    gasfield = recorditems.Split(',');

                    string ThingNo = gasfield[0].ToString();//用品编号
                    string BuyCount = gasfield[1].ToString();//采购数量
                    string UnitPrice = gasfield[2].ToString();//采购单价
                    string BuyMoney = gasfield[3].ToString();//采购金额
                    string Provider = gasfield[4].ToString();//供应商


                    string FromBillID = gasfield[5].ToString();//采购金额
                    string FromSortNo = gasfield[6].ToString();//供应商


                    OfficeThingsBuyDetailM.BuyCount = Convert.ToDecimal(BuyCount);
                    OfficeThingsBuyDetailM.BuyMoney = Convert.ToDecimal(BuyMoney);
                    OfficeThingsBuyDetailM.BuyRecordNo = BuyRecordNo;
                    OfficeThingsBuyDetailM.CompanyCD = CompanyCD;
                    OfficeThingsBuyDetailM.ModifiedDate = System.DateTime.Now;
                    OfficeThingsBuyDetailM.ModifiedUserID = UserID;
                    OfficeThingsBuyDetailM.Provider = Provider;
                    OfficeThingsBuyDetailM.ThingNo = ThingNo;
                    OfficeThingsBuyDetailM.UnitPrice = Convert.ToDecimal(UnitPrice);

                    int RetOfficeThingsVal = OfficeThingsStorageIsExist(ThingNo, CompanyCD);//库存是否有此用品编号的记录，有更新库存，无插入新的库存记录
                    OfficeThingsStorageModel OfficeThingsStorageM = new OfficeThingsStorageModel();
                    if (RetOfficeThingsVal == 0)//无此编号的库存，插入库存信息
                    {
                        #region 拼写添加入库库存信息sql语句
                        StringBuilder OfficeThingsStorageSql = new StringBuilder();
                        OfficeThingsStorageSql.AppendLine("INSERT INTO officedba.OfficeThingsStorage");
                        OfficeThingsStorageSql.AppendLine("(CompanyCD");
                        OfficeThingsStorageSql.AppendLine(",ThingNo     ");
                        OfficeThingsStorageSql.AppendLine(",TotalCount");
                        OfficeThingsStorageSql.AppendLine(",SurplusCount   ");
                        OfficeThingsStorageSql.AppendLine(",UsedCount  ");
                        OfficeThingsStorageSql.AppendLine(",ModifiedDate");
                        OfficeThingsStorageSql.AppendLine(",ModifiedUserID)");
                        OfficeThingsStorageSql.AppendLine(" values ");
                        OfficeThingsStorageSql.AppendLine("(@CompanyCD");
                        OfficeThingsStorageSql.AppendLine(",@ThingNo     ");
                        OfficeThingsStorageSql.AppendLine(",@TotalCount");
                        OfficeThingsStorageSql.AppendLine(",@SurplusCount   ");
                        OfficeThingsStorageSql.AppendLine(",@UsedCount  ");
                        OfficeThingsStorageSql.AppendLine(",@ModifiedDate");
                        OfficeThingsStorageSql.AppendLine(",@ModifiedUserID)");
                        #endregion
                        #region 设置参数
                        SqlParameter[] OfficeThingsStorageParams = new SqlParameter[7];
                        OfficeThingsStorageParams[0] = SqlHelper.GetParameter("@CompanyCD", OfficeThingsBuyDetailM.CompanyCD);
                        OfficeThingsStorageParams[1] = SqlHelper.GetParameter("@ThingNo", OfficeThingsBuyDetailM.ThingNo);
                        OfficeThingsStorageParams[2] = SqlHelper.GetParameter("@TotalCount", OfficeThingsBuyDetailM.BuyCount);
                        OfficeThingsStorageParams[3] = SqlHelper.GetParameter("@SurplusCount", OfficeThingsBuyDetailM.BuyCount);
                        OfficeThingsStorageParams[4] = SqlHelper.GetParameter("@UsedCount", 0);
                        OfficeThingsStorageParams[5] = SqlHelper.GetParameter("@ModifiedDate", OfficeThingsBuyDetailM.ModifiedDate);
                        OfficeThingsStorageParams[6] = SqlHelper.GetParameter("@ModifiedUserID", OfficeThingsBuyDetailM.ModifiedUserID);
                        #endregion
                        SqlCommand OfficeThingsStorageComInfo = new SqlCommand(OfficeThingsStorageSql.ToString());
                        OfficeThingsStorageComInfo.Parameters.AddRange(OfficeThingsStorageParams);
                        //comms[i] = OfficeThingsStorageComInfo;

                        listADD.Add(OfficeThingsStorageComInfo);
                    }
                    else //有此编号，更新库存信息
                    {
                        #region 拼写添加入库库存信息sql语句
                        StringBuilder OfficeThingsStorageSql = new StringBuilder();
                        OfficeThingsStorageSql.AppendLine("UPDATE officedba.OfficeThingsStorage");
                        OfficeThingsStorageSql.AppendLine("SET TotalCount=TotalCount+@TotalCount");
                        OfficeThingsStorageSql.AppendLine(",SurplusCount=SurplusCount+@TotalCount1   ");
                        OfficeThingsStorageSql.AppendLine(",ModifiedDate=@ModifiedDate");
                        OfficeThingsStorageSql.AppendLine(",ModifiedUserID=@ModifiedUserID");
                        OfficeThingsStorageSql.AppendLine(" WHERE ThingNo=@ThingNo AND CompanyCD=@CompanyCD ");
                        #endregion
                        #region 设置参数
                        SqlParameter[] OfficeThingsStorageParams = new SqlParameter[6];
                        OfficeThingsStorageParams[0] = SqlHelper.GetParameter("@TotalCount", OfficeThingsBuyDetailM.BuyCount);
                        OfficeThingsStorageParams[1] = SqlHelper.GetParameter("@TotalCount1", OfficeThingsBuyDetailM.BuyCount);
                        OfficeThingsStorageParams[2] = SqlHelper.GetParameter("@ModifiedDate", OfficeThingsBuyDetailM.ModifiedDate);
                        OfficeThingsStorageParams[3] = SqlHelper.GetParameter("@ModifiedUserID", OfficeThingsBuyDetailM.ModifiedUserID);
                        OfficeThingsStorageParams[4] = SqlHelper.GetParameter("@ThingNo", OfficeThingsBuyDetailM.ThingNo);
                        OfficeThingsStorageParams[5] = SqlHelper.GetParameter("@CompanyCD", OfficeThingsBuyDetailM.CompanyCD);


                        #endregion
                        SqlCommand OfficeThingsStorageComInfo = new SqlCommand(OfficeThingsStorageSql.ToString());
                        OfficeThingsStorageComInfo.Parameters.AddRange(OfficeThingsStorageParams);
                       // comms[i] = OfficeThingsStorageComInfo;
                        listADD.Add(OfficeThingsStorageComInfo);
                    }


                    if (Convert .ToInt32 ( FromBillID) >0)// 回写信息
                    {
                        #region 拼写添加入库库存信息sql语句
                        StringBuilder OfficeThingsStorageSql = new StringBuilder();
                        OfficeThingsStorageSql.AppendLine("UPDATE officedba.OfficeThingsPurchaseApplyDetail ");
                        OfficeThingsStorageSql.AppendLine("SET InCount=isnull(InCount,0)+@InCount ");
                        OfficeThingsStorageSql.AppendLine(" WHERE ID=@FromBillID AND CompanyCD=@CompanyCD  ");
                        #endregion
                        #region 设置参数
                        SqlParameter[] OfficeThingsStorageParams = new SqlParameter[3];
                        OfficeThingsStorageParams[0] = SqlHelper.GetParameter("@FromBillID", FromBillID);
                        OfficeThingsStorageParams[1] = SqlHelper.GetParameter("@InCount", OfficeThingsBuyDetailM.BuyCount);
                        OfficeThingsStorageParams[2] = SqlHelper.GetParameter("@CompanyCD", OfficeThingsBuyDetailM.CompanyCD);

                        #endregion
                        SqlCommand OfficeThingsStorageComInfo = new SqlCommand(OfficeThingsStorageSql.ToString());
                        OfficeThingsStorageComInfo.Parameters.AddRange(OfficeThingsStorageParams);
                       // comms[(i+1)] = OfficeThingsStorageComInfo;

                        listADD.Add(OfficeThingsStorageComInfo);
                    }

                }
                //执行
             //   SqlHelper.ExecuteTransForList(comms);
                SqlHelper.ExecuteTransWithArrayList(listADD);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch 
            {
                return false;
            }
        }
        #endregion
        #region 获取办公用品入库列表
        /// <summary>
        /// 获取办公用品入库列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetOfficeThingsInstorageInfoList(string BuyRecordNo, string Title, string ThingName, string txtJoinUser, string BuyDeptID, string StartDate, string EndDate, string CompanyID, int pageIndex, int pageCount, string ord, ref int TotalCount, out decimal TotalMoeny, out decimal SumCount)
        {
            string sql = "select distinct a.ID,a.BuyRecordNo,a.Title,convert(varchar(10),a.BuyDate,120)BuyDate,"
                           +"a.TotalCount,a.TotalPrice,convert(varchar(10),a.CreateDate,120)CreateDate,"
                          +"CASE a.BillStatus WHEN 1 THEN '未入库' "
                          +"WHEN 2 THEN '已入库' "
                          +"END IfInStorage,"
                          +"CASE a.BillStatus WHEN 1 THEN '制单' "
                          +"WHEN 2 THEN '结单' "
                          + "END BillStatus,isnull(b.EmployeeName,'')EmployeeName,c.EmployeeName AS Creater,d.DeptName "
                          +"from  officedba.OfficeThingsBuy a "
                          +"LEFT OUTER JOIN "
                          +"officedba.EmployeeInfo b "
                          +"ON a.BuyerID=b.ID and a.CompanyCD=b.CompanyCD "
                          +"LEFT OUTER JOIN "
                          +"officedba.EmployeeInfo c "
                          +"ON a.Creator=c.ID and a.CompanyCD=c.CompanyCD "
                          +"LEFT OUTER JOIN "
                          +"officedba.DeptInfo d "
                          +"ON a.BuyDeptID=d.ID and a.CompanyCD=d.CompanyCD "
					      +"LEFT OUTER JOIN "
						  +"officedba.OfficeThingsBuyDetail e "
						  +"ON a.BuyRecordNo=e.BuyRecordNo and a.CompanyCD=e.CompanyCD "
						  +"LEFT OUTER JOIN officedba.OfficeThingsInfo f "
					      +"ON e.ThingNo=f.ThingNo and e.CompanyCD=f.CompanyCD "
                          + " WHERE a.CompanyCD='" + CompanyID + "'  ";
            if (BuyRecordNo != "")
                sql += " and a.BuyRecordNo LIKE '%" + BuyRecordNo + "%'";
            if (Title != "")
                sql += " and a.Title LIKE '%" + Title + "%'";
            if (txtJoinUser != "")
                sql += " and a.BuyerID = " + txtJoinUser + "";
            if (BuyDeptID != "")
                sql += " and a.BuyDeptID = " + BuyDeptID + "";
            if (ThingName != "")
                sql += " and f.ThingName like '%" + ThingName + "%'";
            if (StartDate != "")
                sql += " and a.BuyDate> = '" + StartDate + "'";
            if (EndDate != "")
                sql += " and a.BuyDate <= '" + EndDate + "'";
            DataTable dt = GetOfficeThingsInstorageTotalMoney(BuyRecordNo, Title, ThingName, txtJoinUser, BuyDeptID, StartDate, EndDate,CompanyID);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    TotalMoeny = Convert.ToDecimal(dt.Rows[0]["TotalMoney"].ToString().Trim());
                    SumCount = Convert.ToDecimal(dt.Rows[0]["SumCount"].ToString().Trim());
                }
                else
                {
                    TotalMoeny = 0;
                    SumCount = 0;
                }
            }
            else
            {
                TotalMoeny = 0;
                SumCount = 0;
            }
            return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);

            //return SqlHelper.ExecuteSql(sql);
        }
        #endregion

        #region 获取办公用品入库列表中金额,数量合计
        /// <summary>
        /// 获取办公用品入库列表中金额合计
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetOfficeThingsInstorageTotalMoney(string BuyRecordNo, string Title, string ThingName, string txtJoinUser, string BuyDeptID, string StartDate, string EndDate, string CompanyID)
        {
            string sql = "select SUM(e.BuyCount)SumCount,SUM(e.BuyMoney) TotalMoney "
                          + "from  officedba.OfficeThingsBuy a "
                          + "LEFT OUTER JOIN "
                          + "officedba.EmployeeInfo b "
                          + "ON a.BuyerID=b.ID and a.CompanyCD=b.CompanyCD "
                          + "LEFT OUTER JOIN "
                          + "officedba.EmployeeInfo c "
                          + "ON a.Creator=c.ID and a.CompanyCD=c.CompanyCD "
                          + "LEFT OUTER JOIN "
                          + "officedba.DeptInfo d "
                          + "ON a.BuyDeptID=d.ID and a.CompanyCD=d.CompanyCD "
                          + "LEFT OUTER JOIN "
                          + "officedba.OfficeThingsBuyDetail e "
                          + "ON a.BuyRecordNo=e.BuyRecordNo and a.CompanyCD=e.CompanyCD "
                          + "LEFT OUTER JOIN officedba.OfficeThingsInfo f "
                          + "ON e.ThingNo=f.ThingNo and e.CompanyCD=f.CompanyCD "
                          + " WHERE a.CompanyCD='" + CompanyID + "'  ";
            if (BuyRecordNo != "")
                sql += " and a.BuyRecordNo LIKE '%" + BuyRecordNo + "%'";
            if (Title != "")
                sql += " and a.Title LIKE '%" + Title + "%'";
            if (txtJoinUser != "")
                sql += " and a.BuyerID = " + txtJoinUser + "";
            if (BuyDeptID != "")
                sql += " and a.BuyDeptID = " + BuyDeptID + "";
            if (ThingName != "")
                sql += " and f.ThingName like '%" + ThingName + "%'";
            if (StartDate != "")
                sql += " and a.BuyDate> = '" + StartDate + "'";
            if (EndDate != "")
                sql += " and a.BuyDate <= '" + EndDate + "'";
            //return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);

            return SqlHelper.ExecuteSql(sql);
        }
        #endregion

        #region  根据入库ID获取入库信息查看或修改
        /// <summary>
        /// 根据入库ID获取入库信息查看或修改
        /// </summary>
        /// <param name="ID">入库单ID</param>
        /// <returns>DataTable</returns>
        public static DataTable GetOfficeThingsStorageInfoByID(string ID, string CompanyID)
        {
            string sql = "select a.*,b.*,isnull(sd.ApplyNo,'') as fromBillNo,c.EmployeeName,d.EmployeeName AS Createperson,e.EmployeeName AS ConfirmPerson,"
                            + "f.DeptName,g.ThingName,g.ThingType,g.CodeName,g.TypeName  from officedba.OfficeThingsBuy a "
                            +"INNER JOIN officedba.OfficeThingsBuyDetail b "
                            +"on a.BuyRecordNo=b.BuyRecordNo " 
                          +"  left outer join officedba.OfficeThingsPurchaseApplyDetail sd on b.frombillID=sd.id  "
                            +"LEFT OUTER JOIN  officedba.EmployeeInfo  c "
                            +"ON a.BuyerID=c.ID "
                            +"LEFT OUTER JOIN  officedba.EmployeeInfo  d "
                            +"ON a.Creator=d.ID "
                            +"LEFT OUTER JOIN  officedba.EmployeeInfo  e "
                            +"ON a.Confirmor=e.ID "
                            +"LEFT OUTER JOIN " 
                            +"officedba.DeptInfo f "
                            +"ON a.BuyDeptID=f.ID  "
                            +"LEFT OUTER JOIN "
                            +"(select a.ID,a.ThingNo,a.ThingName,a.ThingType,"
                            +"a.MinCount,convert(varchar(10),a.CreateDate,120)CreateDate,"
					        +"b.CodeName,c.TypeName "
						    +"from officedba.OfficeThingsInfo a "
						    +"LEFT OUTER JOIN officedba.CodeEquipmentType b "
						    +"ON a.TypeID=b.ID "
						    +"LEFT OUTER JOIN officedba.CodePublicType c "
                            + "ON a.UnitID=c.ID where a.CompanyCD='" + CompanyID + "') g "
                            +"ON b.ThingNo=g.ThingNo "
                            +"where a.id="+ID+"";
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion

        #region  根据入库NO获取入库信息查看或修改
        /// <summary>
        /// 根据入库ID获取入库信息查看或修改
        /// </summary>
        /// <param name="NO">入库单NO</param>
        /// <returns>DataTable</returns>
        public static DataTable GetOfficeThingsStorageInfoByNO(string NO, string CompanyID)
        {
            string sql = "select a.*,b.*,c.EmployeeName,d.EmployeeName AS Createperson,e.EmployeeName AS ConfirmPerson,"
                            + "f.DeptName,g.ThingName,g.ThingType,g.CodeName,g.TypeName  from officedba.OfficeThingsBuy a "
                            + "INNER JOIN officedba.OfficeThingsBuyDetail b "
                            + "on a.BuyRecordNo=b.BuyRecordNo "
                            + "LEFT OUTER JOIN  officedba.EmployeeInfo  c "
                            + "ON a.BuyerID=c.ID "
                            + "LEFT OUTER JOIN  officedba.EmployeeInfo  d "
                            + "ON a.Creator=d.ID "
                            + "LEFT OUTER JOIN  officedba.EmployeeInfo  e "
                            + "ON a.Confirmor=e.ID "
                            + "LEFT OUTER JOIN "
                            + "officedba.DeptInfo f "
                            + "ON a.BuyDeptID=f.ID  "
                            + "LEFT OUTER JOIN "
                            + "(select a.ID,a.ThingNo,a.ThingName,a.ThingType,"
                            + "a.MinCount,convert(varchar(10),a.CreateDate,120)CreateDate,"
                            + "b.CodeName,c.TypeName "
                            + "from officedba.OfficeThingsInfo a "
                            + "LEFT OUTER JOIN officedba.CodeEquipmentType b "
                            + "ON a.TypeID=b.ID "
                            + "LEFT OUTER JOIN officedba.CodePublicType c "
                            + "ON a.UnitID=c.ID where a.CompanyCD='" + CompanyID + "') g "
                            + "ON b.ThingNo=g.ThingNo "
                            + "where a.CompanyCD='" + CompanyID + "' and a.BuyRecordNO='" + NO + "'";
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion

        #region 库存是否有此用品编号的记录，有更新库存，无插入新的库存记录
        /// <summary>
        /// 库存是否有此用品编号的记录，有更新库存，无插入新的库存记录
        /// </summary>
        /// <param name="ThingsNo">用品编号</param>
        /// <param name="CompanyID">公司ID</param>
        /// <returns></returns>
        public static int OfficeThingsStorageIsExist(string ThingsNo, string CompanyID)
        {
            string sql = "SELECT COUNT(*) AS IndexCount FROM Officedba.OfficeThingsStorage"
                          +" WHERE ThingNo='"+ThingsNo+"' AND CompanyCD='"+CompanyID+"'";
            DataTable IndexCount = SqlHelper.ExecuteSql(sql);
            if (IndexCount != null && (int)IndexCount.Rows[0][0] > 0)
            {
                return (int)IndexCount.Rows[0][0];
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region 能否删除用品库存信息
        /// <summary>
        /// 删除用品库存信息
        /// </summary>
        /// <param name="EmployeeID">员工ID</param>
        /// <param name="CompanyID">公司代码</param>
        /// <returns></returns>
        public static bool IfDeleteOfficeThingsStorageInfo(string OfficethingsinstorageIDs, string CompanyID)
        {
            string[] NOS = null;
            NOS = OfficethingsinstorageIDs.Split(',');
            bool Flag = true;

            for (int i = 0; i < NOS.Length; i++)
            {
                if (IsExistInfo(NOS[i], CompanyID))
                {
                    Flag = false;
                    break;
                }
            }
            return Flag;
        }
        #endregion
        #region 能否删除用品库存信息
        /// <summary>
        /// 删除用品库存信息
        /// </summary>
        /// <param name="EmployeeID">员工ID</param>
        /// <param name="CompanyID">公司代码</param>
        /// <returns></returns>
        public static bool IsExistInfo(string ID, string CompanyID)
        {

            string sql = "SELECT * FROM officedba.OfficeThingsBuy WHERE BuyRecordNo='" + ID + "' AND CompanyCD='" + CompanyID + "' AND BillStatus=2";
            return SqlHelper.ExecuteSql(sql).Rows.Count > 0 ? true : false;
        }
        #endregion
        #region 删除用品库存信息
        /// <summary>
        /// 删除用品库存信息
        /// </summary>
        /// <param name="EquipmentIDS">设备IDS</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool DeleteOfficeThingsStorageInfo(string ApplyIDS, string CompanyID)
        {
            string allApplyID = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string[] Delsql = new string[2];
            try
            {
                string[] IDS = null;
                IDS = ApplyIDS.Split(',');

                for (int i = 0; i < IDS.Length; i++)
                {
                    IDS[i] = "'" + IDS[i] + "'";
                    sb.Append(IDS[i]);
                }

                allApplyID = sb.ToString().Replace("''", "','");
                Delsql[0] = "DELETE FROM officedba.OfficeThingsBuy WHERE BuyRecordNo IN (" + allApplyID + ") AND CompanyCD='" + CompanyID + "'";
                Delsql[1] = "DELETE FROM officedba.OfficeThingsBuyDetail WHERE BuyRecordNo IN (" + allApplyID + ") AND CompanyCD='" + CompanyID + "'";
                SqlHelper.ExecuteTransForListWithSQL(Delsql);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch 
            {
                return false;
            }
        }
        #endregion
    }

}

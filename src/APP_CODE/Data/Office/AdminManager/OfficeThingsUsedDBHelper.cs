/**********************************************
 * 类作用：   办公用品领用数据库层处理
 * 建立人：   lysong
 * 建立时间： 2009/05/09
 ***********************************************/
using System;
using XBase.Model.Office.AdminManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
namespace XBase.Data.Office.AdminManager
{
    /// <summary>
    /// 类名：OfficeThingsUsedDBHelper
    /// 描述：办公用品领用数据库层处理
    /// 作者：lysong
    /// 创建时间：2009/05/09
    /// </summary>
   public class OfficeThingsUsedDBHelper
    {
        #region 添加办公用品领用信息
        /// <summary>
        /// 添加办公用品领用信息
        /// </summary>
        /// <param name="OfficeThingsUsedM">领用单主信息</param>
        /// <param name="OfficeThingsUsedInfos">领用详细信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool InserOfficeThingsUsedInfo(OfficeThingsUsedModel OfficeThingsUsedM, string OfficeThingsUsedInfos)
        {
            try
            {
                #region 添加入库库存信息sql语句
                StringBuilder OfficeThingsUsedSql = new StringBuilder();
                OfficeThingsUsedSql.AppendLine("INSERT INTO officedba.OfficeThingsUsed");
                OfficeThingsUsedSql.AppendLine("(CompanyCD");
                OfficeThingsUsedSql.AppendLine(",ApplyNo     ");
                OfficeThingsUsedSql.AppendLine(",Title");
                OfficeThingsUsedSql.AppendLine(",EmployeeID   ");
                OfficeThingsUsedSql.AppendLine(",DeptID  ");
                OfficeThingsUsedSql.AppendLine(",UsedDate    ");
                OfficeThingsUsedSql.AppendLine(",CountTotal    ");
                OfficeThingsUsedSql.AppendLine(",BillStatus    ");
                OfficeThingsUsedSql.AppendLine(",Creator    ");
                OfficeThingsUsedSql.AppendLine(",CreateDate");
                OfficeThingsUsedSql.AppendLine(",Remark");
                OfficeThingsUsedSql.AppendLine(",ModifiedDate");
                OfficeThingsUsedSql.AppendLine(",ModifiedUserID)");
                OfficeThingsUsedSql.AppendLine(" values ");
                OfficeThingsUsedSql.AppendLine("(@CompanyCD");
                OfficeThingsUsedSql.AppendLine(",@ApplyNo     ");
                OfficeThingsUsedSql.AppendLine(",@Title");
                OfficeThingsUsedSql.AppendLine(",@EmployeeID   ");
                OfficeThingsUsedSql.AppendLine(",@DeptID  ");
                OfficeThingsUsedSql.AppendLine(",@UsedDate    ");
                OfficeThingsUsedSql.AppendLine(",@CountTotal    ");
                OfficeThingsUsedSql.AppendLine(",@BillStatus    ");
                OfficeThingsUsedSql.AppendLine(",@Creator    ");
                OfficeThingsUsedSql.AppendLine(",@CreateDate");
                OfficeThingsUsedSql.AppendLine(",@Remark");
                OfficeThingsUsedSql.AppendLine(",@ModifiedDate");
                OfficeThingsUsedSql.AppendLine(",@ModifiedUserID)");
                #endregion

                #region 设置参数
                SqlParameter[] paramgas = new SqlParameter[13];
                paramgas[0] = SqlHelper.GetParameter("@CompanyCD", OfficeThingsUsedM.CompanyCD);
                paramgas[1] = SqlHelper.GetParameter("@ApplyNo", OfficeThingsUsedM.ApplyNo);
                paramgas[2] = SqlHelper.GetParameter("@Title", OfficeThingsUsedM.Title);
                paramgas[3] = SqlHelper.GetParameter("@EmployeeID", OfficeThingsUsedM.EmployeeID);
                paramgas[4] = SqlHelper.GetParameter("@DeptID", OfficeThingsUsedM.DeptID);
                paramgas[5] = SqlHelper.GetParameter("@UsedDate", OfficeThingsUsedM.UsedDate);
                paramgas[6] = SqlHelper.GetParameter("@CountTotal", OfficeThingsUsedM.CountTotal);
                paramgas[7] = SqlHelper.GetParameter("@BillStatus", OfficeThingsUsedM.BillStatus);
                paramgas[8] = SqlHelper.GetParameter("@Creator", OfficeThingsUsedM.Creator);
                paramgas[9] = SqlHelper.GetParameter("@CreateDate", OfficeThingsUsedM.CreateDate);
                paramgas[10] = SqlHelper.GetParameter("@Remark", OfficeThingsUsedM.Remark);
                paramgas[11] = SqlHelper.GetParameter("@ModifiedDate", OfficeThingsUsedM.ModifiedDate);
                paramgas[12] = SqlHelper.GetParameter("@ModifiedUserID", OfficeThingsUsedM.ModifiedUserID);
                #endregion
                return InsertAll(OfficeThingsUsedSql.ToString(), OfficeThingsUsedInfos, OfficeThingsUsedM.ApplyNo, OfficeThingsUsedM.CompanyCD, OfficeThingsUsedM.ModifiedUserID, paramgas);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 添加办公用品领用信息
        /// </summary>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool InsertAll(string OfficeThingsUsedSql, string OfficeThingsUsedInfos, string ApplyNo, string CompanyCD, string UserID, SqlParameter[] paramgas)
        {
            try
            {
                OfficeThingsUsedDetailModel OfficeThingsUsedDetailM = new OfficeThingsUsedDetailModel();
                string[] OfficeThingsUsedArrary = OfficeThingsUsedInfos.Split('|');
                SqlCommand[] comms = new SqlCommand[OfficeThingsUsedArrary.Length]; //申明cmd数组(主表，明细表，每条明细生成一条库存记录)
                SqlCommand OfficeThingsUsedCom = new SqlCommand(OfficeThingsUsedSql.ToString());
                OfficeThingsUsedCom.Parameters.AddRange(paramgas);
                comms[0] = OfficeThingsUsedCom;

                string recorditems = "";
                string[] gasfield = null;

                for (int i = 1; i < OfficeThingsUsedArrary.Length; i++) //循环数组
                {
                    recorditems = OfficeThingsUsedArrary[i].ToString();
                    gasfield = recorditems.Split(',');

                    string ThingNo = gasfield[0].ToString();//用品编号
                    string BuyCount = gasfield[1].ToString();//明细领用数量

                    OfficeThingsUsedDetailM.ApplyNo = ApplyNo;
                    OfficeThingsUsedDetailM.CompanyCD = CompanyCD;
                    OfficeThingsUsedDetailM.Count = Convert.ToDecimal(BuyCount);
                    OfficeThingsUsedDetailM.ModifiedDate = System.DateTime.Now;
                    OfficeThingsUsedDetailM.ModifiedUserID = UserID;
                    OfficeThingsUsedDetailM.ThingNo = ThingNo;

                    #region 拼写添加入库明细信息sql语句
                    StringBuilder OfficeThingsUsedDetailSql = new StringBuilder();
                    OfficeThingsUsedDetailSql.AppendLine("INSERT INTO officedba.OfficeThingsUsedDetail");
                    OfficeThingsUsedDetailSql.AppendLine("(CompanyCD");
                    OfficeThingsUsedDetailSql.AppendLine(",ApplyNo     ");
                    OfficeThingsUsedDetailSql.AppendLine(",ThingNo");
                    OfficeThingsUsedDetailSql.AppendLine(",Count   ");
                    OfficeThingsUsedDetailSql.AppendLine(",ModifiedDate");
                    OfficeThingsUsedDetailSql.AppendLine(",ModifiedUserID)");
                    OfficeThingsUsedDetailSql.AppendLine(" values ");
                    OfficeThingsUsedDetailSql.AppendLine("(@CompanyCD");
                    OfficeThingsUsedDetailSql.AppendLine(",@ApplyNo     ");
                    OfficeThingsUsedDetailSql.AppendLine(",@ThingNo");
                    OfficeThingsUsedDetailSql.AppendLine(",@Count   ");
                    OfficeThingsUsedDetailSql.AppendLine(",@ModifiedDate");
                    OfficeThingsUsedDetailSql.AppendLine(",@ModifiedUserID)");
                    #endregion
                    #region 设置参数
                    SqlParameter[] OfficeThingsUsedDetailParams = new SqlParameter[6];
                    OfficeThingsUsedDetailParams[0] = SqlHelper.GetParameter("@CompanyCD", OfficeThingsUsedDetailM.CompanyCD);
                    OfficeThingsUsedDetailParams[1] = SqlHelper.GetParameter("@ApplyNo", OfficeThingsUsedDetailM.ApplyNo);
                    OfficeThingsUsedDetailParams[2] = SqlHelper.GetParameter("@ThingNo", OfficeThingsUsedDetailM.ThingNo);
                    OfficeThingsUsedDetailParams[3] = SqlHelper.GetParameter("@Count", OfficeThingsUsedDetailM.Count);
                    OfficeThingsUsedDetailParams[4] = SqlHelper.GetParameter("@ModifiedDate", OfficeThingsUsedDetailM.ModifiedDate);
                    OfficeThingsUsedDetailParams[5] = SqlHelper.GetParameter("@ModifiedUserID", OfficeThingsUsedDetailM.ModifiedUserID);

                    SqlCommand OfficeThingsUsedDetailComInfo = new SqlCommand(OfficeThingsUsedDetailSql.ToString());
                    OfficeThingsUsedDetailComInfo.Parameters.AddRange(OfficeThingsUsedDetailParams);
                    comms[i] = OfficeThingsUsedDetailComInfo;
                    #endregion

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
        #region 修改用品领用单信息
        /// <summary>
        /// 修改用品领用单信息
        /// </summary>
        /// <param name="OfficeThingsBuyM">领用单主信息</param>
        /// <param name="OfficeThingsUsedInfos">领用单详细信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool UpdateOfficeThingsUsedInfo(OfficeThingsUsedModel OfficeThingsUsedM, string OfficeThingsUsedInfos)
        {
            try
            {
                #region 添加领用单存信息sql语句
                StringBuilder OfficeThingsUsedSql = new StringBuilder();
                OfficeThingsUsedSql.AppendLine("UPDATE officedba.OfficeThingsUsed");
                OfficeThingsUsedSql.AppendLine(" SET Title=@Title");
                OfficeThingsUsedSql.AppendLine(",EmployeeID=@EmployeeID   ");
                OfficeThingsUsedSql.AppendLine(",DeptID=@DeptID  ");
                OfficeThingsUsedSql.AppendLine(",UsedDate=@UsedDate    ");
                OfficeThingsUsedSql.AppendLine(",CountTotal=@CountTotal    ");
                OfficeThingsUsedSql.AppendLine(",BillStatus=@BillStatus    ");
                OfficeThingsUsedSql.AppendLine(",Remark=@Remark");
                OfficeThingsUsedSql.AppendLine(",ModifiedDate=@ModifiedDate");
                OfficeThingsUsedSql.AppendLine(",ModifiedUserID=@ModifiedUserID");
                OfficeThingsUsedSql.AppendLine(" WHERE  CompanyCD=@CompanyCD AND ApplyNo=@ApplyNo");
                #endregion

                #region 设置参数
                SqlParameter[] paramgas = new SqlParameter[11];
                paramgas[0] = SqlHelper.GetParameter("@Title", OfficeThingsUsedM.Title);
                paramgas[1] = SqlHelper.GetParameter("@EmployeeID", OfficeThingsUsedM.EmployeeID);
                paramgas[2] = SqlHelper.GetParameter("@DeptID", OfficeThingsUsedM.DeptID);
                paramgas[3] = SqlHelper.GetParameter("@UsedDate", OfficeThingsUsedM.UsedDate);
                paramgas[4] = SqlHelper.GetParameter("@CountTotal", OfficeThingsUsedM.CountTotal);
                paramgas[5] = SqlHelper.GetParameter("@BillStatus", OfficeThingsUsedM.BillStatus);
                paramgas[6] = SqlHelper.GetParameter("@Remark", OfficeThingsUsedM.Remark);
                paramgas[7] = SqlHelper.GetParameter("@ModifiedDate", OfficeThingsUsedM.ModifiedDate);
                paramgas[8] = SqlHelper.GetParameter("@ModifiedUserID", OfficeThingsUsedM.ModifiedUserID);
                paramgas[9] = SqlHelper.GetParameter("@CompanyCD", OfficeThingsUsedM.CompanyCD);
                paramgas[10] = SqlHelper.GetParameter("@ApplyNo", OfficeThingsUsedM.ApplyNo);
                #endregion
                return UpdateAll(OfficeThingsUsedSql.ToString(), OfficeThingsUsedInfos, OfficeThingsUsedM.ApplyNo, OfficeThingsUsedM.CompanyCD, OfficeThingsUsedM.ModifiedUserID, paramgas);
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
        public static bool UpdateAll(string OfficeThingsUsedSql, string OfficeThingsUsedInfos, string ApplyNo, string CompanyCD, string UserID, SqlParameter[] paramgas)
        {
            try
            {
                OfficeThingsUsedDetailModel OfficeThingsUsedDetailM = new OfficeThingsUsedDetailModel();
                string[] OfficeThingsUsedArrary = OfficeThingsUsedInfos.Split('|');
                SqlCommand[] comms = new SqlCommand[OfficeThingsUsedArrary.Length + 1]; //申明cmd数组(主表，明细表，每条明细生成一条库存记录)
                SqlCommand OfficeThingsUsedCom = new SqlCommand(OfficeThingsUsedSql.ToString());
                OfficeThingsUsedCom.Parameters.AddRange(paramgas);
                comms[0] = OfficeThingsUsedCom;

                #region 首先删除此入库单的明细
                string OfficeThingsDelUsedDetailSql = "DELETE FROM officedba.OfficeThingsUsedDetail WHERE ApplyNo='" + ApplyNo + "' AND CompanyCD='" + CompanyCD + "'";
                SqlCommand OfficeThingsDelUsedDetailCom = new SqlCommand(OfficeThingsDelUsedDetailSql.ToString());
                comms[1] = OfficeThingsDelUsedDetailCom;
                #endregion

                string recorditems = "";
                string[] gasfield = null;

                for (int i = 1; i < OfficeThingsUsedArrary.Length; i++) //循环数组
                {
                    recorditems = OfficeThingsUsedArrary[i].ToString();
                    gasfield = recorditems.Split(',');

                    string ThingNo = gasfield[0].ToString();//用品编号
                    string BuyCount = gasfield[1].ToString();//明细领用数量

                    OfficeThingsUsedDetailM.ApplyNo = ApplyNo;
                    OfficeThingsUsedDetailM.CompanyCD = CompanyCD;
                    OfficeThingsUsedDetailM.Count = Convert.ToDecimal(BuyCount);
                    OfficeThingsUsedDetailM.ModifiedDate = System.DateTime.Now;
                    OfficeThingsUsedDetailM.ModifiedUserID = UserID;
                    OfficeThingsUsedDetailM.ThingNo = ThingNo;

                    #region 拼写添加入库明细信息sql语句
                    StringBuilder OfficeThingsUsedDetailSql = new StringBuilder();
                    OfficeThingsUsedDetailSql.AppendLine("INSERT INTO officedba.OfficeThingsUsedDetail");
                    OfficeThingsUsedDetailSql.AppendLine("(CompanyCD");
                    OfficeThingsUsedDetailSql.AppendLine(",ApplyNo     ");
                    OfficeThingsUsedDetailSql.AppendLine(",ThingNo");
                    OfficeThingsUsedDetailSql.AppendLine(",Count   ");
                    OfficeThingsUsedDetailSql.AppendLine(",ModifiedDate");
                    OfficeThingsUsedDetailSql.AppendLine(",ModifiedUserID)");
                    OfficeThingsUsedDetailSql.AppendLine(" values ");
                    OfficeThingsUsedDetailSql.AppendLine("(@CompanyCD");
                    OfficeThingsUsedDetailSql.AppendLine(",@ApplyNo     ");
                    OfficeThingsUsedDetailSql.AppendLine(",@ThingNo");
                    OfficeThingsUsedDetailSql.AppendLine(",@Count   ");
                    OfficeThingsUsedDetailSql.AppendLine(",@ModifiedDate");
                    OfficeThingsUsedDetailSql.AppendLine(",@ModifiedUserID)");
                    #endregion
                    #region 设置参数
                    SqlParameter[] OfficeThingsUsedDetailParams = new SqlParameter[6];
                    OfficeThingsUsedDetailParams[0] = SqlHelper.GetParameter("@CompanyCD", OfficeThingsUsedDetailM.CompanyCD);
                    OfficeThingsUsedDetailParams[1] = SqlHelper.GetParameter("@ApplyNo", OfficeThingsUsedDetailM.ApplyNo);
                    OfficeThingsUsedDetailParams[2] = SqlHelper.GetParameter("@ThingNo", OfficeThingsUsedDetailM.ThingNo);
                    OfficeThingsUsedDetailParams[3] = SqlHelper.GetParameter("@Count", OfficeThingsUsedDetailM.Count);
                    OfficeThingsUsedDetailParams[4] = SqlHelper.GetParameter("@ModifiedDate", OfficeThingsUsedDetailM.ModifiedDate);
                    OfficeThingsUsedDetailParams[5] = SqlHelper.GetParameter("@ModifiedUserID", OfficeThingsUsedDetailM.ModifiedUserID);

                    SqlCommand OfficeThingsUsedDetailComInfo = new SqlCommand(OfficeThingsUsedDetailSql.ToString());
                    OfficeThingsUsedDetailComInfo.Parameters.AddRange(OfficeThingsUsedDetailParams);
                    comms[i + 1] = OfficeThingsUsedDetailComInfo;
                    #endregion
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
        #region 用品领用确认
        /// <summary>
        /// 用品领用确认
        /// </summary>
        /// <param name="OfficeThingsUsedM">领用单主信息</param>
        /// <param name="OfficeThingsUsedInfos">领用单详细信息</param>
        /// <returns>确认是否成功 false:失败，true:成功</returns>
        public static bool ConfirmUsedInfo(OfficeThingsUsedModel OfficeThingsUsedM, string OfficeThingsUsedInfos)
        {
            try
            {
                #region 添加入库库存信息sql语句
                StringBuilder OfficeThingsUsedSql = new StringBuilder();
                OfficeThingsUsedSql.AppendLine("Update officedba.OfficeThingsUsed");
                OfficeThingsUsedSql.AppendLine("SET BillStatus=@BillStatus");
                OfficeThingsUsedSql.AppendLine(",Confirmor=@Confirmor     ");
                OfficeThingsUsedSql.AppendLine(",ConfirmDate=@ConfirmDate     ");
                OfficeThingsUsedSql.AppendLine(",ModifiedDate=@ModifiedDate");
                OfficeThingsUsedSql.AppendLine(",ModifiedUserID=@ModifiedUserID");
                OfficeThingsUsedSql.AppendLine("  WHERE  ApplyNo=@ApplyNo AND CompanyCD=@CompanyCD");
                #endregion

                #region 设置参数
                SqlParameter[] paramgas = new SqlParameter[7];
                paramgas[0] = SqlHelper.GetParameter("@BillStatus", OfficeThingsUsedM.BillStatus);
                paramgas[1] = SqlHelper.GetParameter("@Confirmor", OfficeThingsUsedM.Confirmor);
                paramgas[2] = SqlHelper.GetParameter("@ConfirmDate", OfficeThingsUsedM.ConfirmDate);
                paramgas[3] = SqlHelper.GetParameter("@ModifiedDate", OfficeThingsUsedM.ModifiedDate);
                paramgas[4] = SqlHelper.GetParameter("@ModifiedUserID", OfficeThingsUsedM.ModifiedUserID);
                paramgas[5] = SqlHelper.GetParameter("@ApplyNo", OfficeThingsUsedM.ApplyNo);
                paramgas[6] = SqlHelper.GetParameter("@CompanyCD", OfficeThingsUsedM.CompanyCD);
                #endregion
                return ConfirmAll(OfficeThingsUsedSql.ToString(), OfficeThingsUsedInfos, OfficeThingsUsedM.ApplyNo, OfficeThingsUsedM.CompanyCD, OfficeThingsUsedM.ModifiedUserID, paramgas);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 用品领用确认
        /// </summary>
        /// <returns>领用是否成功 false:失败，true:成功</returns>
        public static bool ConfirmAll(string OfficeThingsUsedSql, string OfficeThingsUsedInfos, string ApplyNo, string CompanyCD, string UserID, SqlParameter[] paramgas)
        {
            try
            {
                OfficeThingsUsedDetailModel OfficeThingsUsedDetailM = new OfficeThingsUsedDetailModel();
                string[] OfficeThingsUsedArrary = OfficeThingsUsedInfos.Split('|');
                SqlCommand[] comms = new SqlCommand[OfficeThingsUsedArrary.Length]; //申明cmd数组(主表，明细表，每条明细生成一条库存记录)
                SqlCommand OfficeThingsUsedCom = new SqlCommand(OfficeThingsUsedSql.ToString());
                OfficeThingsUsedCom.Parameters.AddRange(paramgas);
                comms[0] = OfficeThingsUsedCom;

                string recorditems = "";
                string[] gasfield = null;

                for (int i = 1; i < OfficeThingsUsedArrary.Length; i++) //循环数组
                {
                    recorditems = OfficeThingsUsedArrary[i].ToString();
                    gasfield = recorditems.Split(',');

                    string ThingNo = gasfield[0].ToString();//用品编号
                    string BuyCount = gasfield[1].ToString();//领用数量

                    OfficeThingsUsedDetailM.ThingNo = ThingNo;
                    OfficeThingsUsedDetailM.Count = Convert.ToDecimal(BuyCount);
                    OfficeThingsUsedDetailM.ModifiedDate = System.DateTime.Now;
                    OfficeThingsUsedDetailM.ModifiedUserID = UserID;
                    OfficeThingsUsedDetailM.CompanyCD = CompanyCD;

                    OfficeThingsStorageModel OfficeThingsStorageM = new OfficeThingsStorageModel();
                    
                    #region 拼写更新入库库存信息sql语句
                    StringBuilder OfficeThingsStorageSql = new StringBuilder();
                    OfficeThingsStorageSql.AppendLine("UPDATE officedba.OfficeThingsStorage");
                    OfficeThingsStorageSql.AppendLine("SET SurplusCount=SurplusCount-@ApplyCount");
                    OfficeThingsStorageSql.AppendLine(",UsedCount=UsedCount+@ApplyCount1  ");
                    OfficeThingsStorageSql.AppendLine(",ModifiedDate=@ModifiedDate");
                    OfficeThingsStorageSql.AppendLine(",ModifiedUserID=@ModifiedUserID");
                    OfficeThingsStorageSql.AppendLine(" WHERE ThingNo=@ThingNo AND CompanyCD=@CompanyCD ");
                    #endregion
                    #region 设置参数
                    SqlParameter[] OfficeThingsStorageParams = new SqlParameter[6];
                    OfficeThingsStorageParams[0] = SqlHelper.GetParameter("@ApplyCount", OfficeThingsUsedDetailM.Count);
                    OfficeThingsStorageParams[1] = SqlHelper.GetParameter("@ApplyCount1", OfficeThingsUsedDetailM.Count);
                    OfficeThingsStorageParams[2] = SqlHelper.GetParameter("@ModifiedDate", OfficeThingsUsedDetailM.ModifiedDate);
                    OfficeThingsStorageParams[3] = SqlHelper.GetParameter("@ModifiedUserID", OfficeThingsUsedDetailM.ModifiedUserID);
                    OfficeThingsStorageParams[4] = SqlHelper.GetParameter("@ThingNo", OfficeThingsUsedDetailM.ThingNo);
                    OfficeThingsStorageParams[5] = SqlHelper.GetParameter("@CompanyCD", OfficeThingsUsedDetailM.CompanyCD);


                    #endregion
                    SqlCommand OfficeThingsStorageComInfo = new SqlCommand(OfficeThingsStorageSql.ToString());
                    OfficeThingsStorageComInfo.Parameters.AddRange(OfficeThingsStorageParams);
                    comms[i] = OfficeThingsStorageComInfo;
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
        #region 获取办公用品领用列表
        /// <summary>
        /// 获取办公用品领用列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetOfficeThingsUsedInfoList(string BuyRecordNo, string Title, string txtJoinUser, string BuyDeptID, string StartDate, string EndDate, string CompanyID,int pageIndex,int pageCount,string ord, ref int TotalCount)
        {
            string sql = "select a.ID,a.ApplyNo,a.Title,convert(varchar(10),a.UsedDate,120)UsedDate,"
                            +"convert(varchar(10),a.CreateDate,120)CreateDate,CASE a.BillStatus WHEN 1 THEN '制单' WHEN 2 THEN '结单' "
                            +"END BillStatus,isnull(b.EmployeeName,'')EmployeeName,isnull(c.EmployeeName,'') AS Creater,d.DeptName,f.[Count],"
                            +"g.ThingNo,g.ThingName,ISNULL(g.ThingType,'')ThingType,g.TypeName "
                            +"from  officedba.OfficeThingsUsed a "
                            +"LEFT OUTER JOIN officedba.EmployeeInfo b "
                            +"ON a.EmployeeID=b.ID "
                            +"LEFT OUTER JOIN officedba.EmployeeInfo  c "
                            +"ON a.Creator=c.ID "
                            +"LEFT OUTER JOIN officedba.DeptInfo d ON a.DeptID=d.ID "
                            + "LEFT OUTER JOIN officedba.OfficeThingsUsedDetail f ON a.CompanyCD='" + CompanyID + "' and a.ApplyNo=f.ApplyNo  AND a.CompanyCD=f.CompanyCD "
                            +"LEFT OUTER JOIN (select a.ID,a.ThingNo,a.CompanyCD,a.ThingName,a.ThingType,c.TypeName "
                            +"from officedba.OfficeThingsInfo a "
                            + "LEFT OUTER JOIN officedba.CodeEquipmentType b ON a.CompanyCD='" + CompanyID + "' and a.TypeID=b.ID and a.CompanyCD=b.CompanyCD  "
                            + "LEFT OUTER JOIN officedba.CodePublicType c ON a.CompanyCD='" + CompanyID + "' and a.UnitID=c.ID and a.CompanyCD=c.CompanyCD) g "
                            + "ON f.ThingNo=g.ThingNo and f.CompanyCD=g.CompanyCD "
                            + " WHERE a.CompanyCD='" + CompanyID + "'  ";
            if (BuyRecordNo != "")
                sql += " and a.ApplyNo LIKE '%" + BuyRecordNo + "%'";
            if (Title != "")
                sql += " and a.Title LIKE '%" + Title + "%'";
            if (txtJoinUser != "")
                sql += " and a.EmployeeID = " + txtJoinUser + "";
            if (BuyDeptID != "")
                sql += " and a.DeptID = " + BuyDeptID + "";
            if (StartDate != "")
                sql += " and a.UsedDate> = '" + StartDate + "'";
            if (EndDate != "")
                sql += " and a.UsedDate <= '" + EndDate + "'";
            return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);

            //return SqlHelper.ExecuteSql(sql);
        }
        #endregion

        #region  根据领用单ID获取入库信息查看或修改
        /// <summary>
        /// 根据领用单ID获取入库信息查看或修改
        /// </summary>
        /// <param name="ID">领用单ID</param>
        /// <returns>DataTable</returns>
        public static DataTable GetOfficeThingsUsedInfoByID(string ID)
        {
            string sql = "select a.*,b.EmployeeName,c.EmployeeName AS Creater,d.DeptName,e.EmployeeName as ConfirmerPerson,f.[Count],"
                           + "g.ThingNo,g.ThingName,ISNULL(g.ThingType,'')ThingType,g.CodeName,g.TypeName,h.SurplusCount "
                           +"from  officedba.OfficeThingsUsed a "
                           +"LEFT OUTER JOIN  officedba.EmployeeInfo  b "
                           +"ON a.EmployeeID=b.ID "
                           +"LEFT OUTER JOIN  officedba.EmployeeInfo  c "
                           +"ON a.Creator=c.ID "
                           +"LEFT OUTER JOIN "
                           +"officedba.DeptInfo d "
                           +"ON a.DeptID=d.ID "
                           +"LEFT OUTER JOIN  officedba.EmployeeInfo e "
                           +"ON a.Confirmor=e.ID "
						   +"Inner join officedba.OfficeThingsUsedDetail f "
					       +"ON a.ApplyNo=f.ApplyNo "
                           +"inner join "
                           + "(select a.ID,a.CompanyCD,a.ThingNo,a.ThingName,a.ThingType,b.CodeName,c.TypeName "
                           +"from officedba.OfficeThingsInfo a "
                           +"LEFT OUTER JOIN officedba.CodeEquipmentType b "
                           + "ON a.TypeID=b.ID  and a.CompanyCD=b.CompanyCD "
                           +"LEFT OUTER JOIN officedba.CodePublicType c "
                           + "ON a.UnitID=c.ID and a.CompanyCD=c.CompanyCD ) g "
                           + "ON f.ThingNo=g.ThingNo and f.Companycd=g.CompanyCD "
                           +"inner join "
                           +"officedba.OfficeThingsStorage h "
                           + "ON f.ThingNo=h.ThingNo and f.Companycd=h.Companycd "
                           + "where a.id=" + ID + "";
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion

        #region  根据领用单NO获取入库信息查看或修改
        /// <summary>
        /// 根据领用单NO获取入库信息查看或修改
        /// </summary>
        /// <param name="ID">领用单ID</param>
        /// <returns>DataTable</returns>
        public static DataTable GetOfficeThingsUsedInfoByNO(string NO,string CompanyCD)
        {
            string sql = "select a.*,b.EmployeeName,c.EmployeeName AS Creater,d.DeptName,e.EmployeeName as ConfirmerPerson,f.[Count],"
                           + "g.ThingNo,g.ThingName,ISNULL(g.ThingType,'')ThingType,g.CodeName,g.TypeName,h.SurplusCount "
                           + "from  officedba.OfficeThingsUsed a "
                           + "LEFT OUTER JOIN  officedba.EmployeeInfo  b "
                           + "ON a.EmployeeID=b.ID "
                           + "LEFT OUTER JOIN  officedba.EmployeeInfo  c "
                           + "ON a.Creator=c.ID "
                           + "LEFT OUTER JOIN "
                           + "officedba.DeptInfo d "
                           + "ON a.DeptID=d.ID "
                           + "LEFT OUTER JOIN  officedba.EmployeeInfo e "
                           + "ON a.Confirmor=e.ID "
                           + "Inner join officedba.OfficeThingsUsedDetail f "
                           + "ON a.ApplyNo=f.ApplyNo "
                           + "inner join "
                           + "(select a.ID,a.ThingNo,a.ThingName,a.ThingType,b.CodeName,c.TypeName "
                           + "from officedba.OfficeThingsInfo a "
                           + "LEFT OUTER JOIN officedba.CodeEquipmentType b "
                           + "ON a.TypeID=b.ID "
                           + "LEFT OUTER JOIN officedba.CodePublicType c "
                           + "ON a.UnitID=c.ID) g "
                           + "ON f.ThingNo=g.ThingNo  "
                           + "inner join "
                           + "officedba.OfficeThingsStorage h "
                           + "ON f.ThingNo=h.ThingNo  "
                           + "where a.ApplyNO='" + NO + "' and a.CompanyCD='" + CompanyCD + "'";
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion

        #region 能否删除用品领用信息
        /// <summary>
        /// 能否删除用品领用信息
        /// </summary>
        /// <param name="EmployeeID">员工ID</param>
        /// <param name="CompanyID">公司代码</param>
        /// <returns></returns>
        public static bool IfDeleteOfficeThingsUsedInfo(string OfficethingsUsedNos, string CompanyID)
        {
            string[] NOS = null;
            NOS = OfficethingsUsedNos.Split(',');
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
        #region 能否删除用品领用信息
        /// <summary>
        /// 能否删除用品领用信息
        /// </summary>
        /// <param name="EmployeeID">员工ID</param>
        /// <param name="CompanyID">公司代码</param>
        /// <returns></returns>
        public static bool IsExistInfo(string No, string CompanyID)
        {

            string sql = "SELECT * FROM officedba.OfficeThingsUsed WHERE ApplyNo='" + No + "' AND CompanyCD='" + CompanyID + "' AND BillStatus=2";
            return SqlHelper.ExecuteSql(sql).Rows.Count > 0 ? true : false;
        }
        #endregion
        #region 删除用品领用信息
        /// <summary>
        /// 删除用品领用信息
        /// </summary>
        /// <param name="EquipmentIDS">设备IDS</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool DeleteOfficeThingsUsedInfo(string ApplyIDS, string CompanyID)
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
                Delsql[0] = "DELETE FROM officedba.OfficeThingsUsed WHERE ApplyNo IN (" + allApplyID + ") AND CompanyCD='" + CompanyID + "'";
                Delsql[1] = "DELETE FROM officedba.OfficeThingsUsedDetail WHERE ApplyNo IN (" + allApplyID + ") AND CompanyCD='" + CompanyID + "'";
                SqlHelper.ExecuteTransForListWithSQL(Delsql);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 获取用品领用信息列表
        /// <summary>
        /// 获取用品领用信息列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetOfficeThingsUsedSumInfo(string StartDate, string EndDate, string ThingName, string TypeID, string CompanyID, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string sql = "SELECT distinct b.ID,a.ThingNo,b.ThingName,b.CodeName,a.UsedCount FROM "
                            +"(SELECT ThingNo,CompanyCD,SUM([Count]) AS UsedCount "
                            +"FROM officedba.OfficeThingsUsedDetail GROUP BY ThingNo,CompanyCD) a "
                            +"LEFT OUTER JOIN "
                            + "(SELECT  a.ID,a.ThingNo,a.CompanyCD,a.ThingName,b.ID as ID1,b.CodeName "
                            +"FROM  officedba.OfficeThingsInfo AS a LEFT OUTER JOIN "
                            +"officedba.CodeEquipmentType AS b "
                            +"ON a.TypeID = b.ID AND a.CompanyCD = b.CompanyCD) b "
                            +"ON a.ThingNo=b.ThingNo AND a.CompanyCD=b.CompanyCD "
                            +"LEFT OUTER JOIN "
                            +"(SELECT a.CompanyCD, a.UsedDate, b.ThingNo "
                            +"FROM officedba.OfficeThingsUsed AS a "
                            +"LEFT OUTER JOIN "
                            +"officedba.OfficeThingsUsedDetail AS b "
                            +"ON a.ApplyNo = b.ApplyNo AND a.CompanyCD = b.CompanyCD) c "
                            +"ON a.ThingNo=c.ThingNo AND a.CompanyCD=c.CompanyCD"
                            + " where a.CompanyCD='" + CompanyID + "'";
            if (ThingName != "")
                sql += " and b.ThingName like '%" + ThingName + "%' ";
            if (StartDate != "")
                sql += " and c.UsedDate>='" + StartDate + "'";
            if (EndDate != "")
                sql += " and c.UsedDate<='" + EndDate + "'";
            if (TypeID != "")
                sql += " and b.ID1=" + TypeID + "";
            return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
        }
        #endregion
        #region 获取用品领用信息列表打印
        /// <summary>
        /// 获取用品领用信息列表打印
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetOfficeThingsUsedSumInfoPrint(string StartDate, string EndDate, string ThingName, string TypeID, string CompanyID, string ord)
        {
            string sql = "SELECT distinct b.ID,a.ThingNo,b.ThingName,b.CodeName,a.UsedCount FROM "
                            + "(SELECT ThingNo,CompanyCD,SUM([Count]) AS UsedCount "
                            + "FROM officedba.OfficeThingsUsedDetail GROUP BY ThingNo,CompanyCD) a "
                            + "LEFT OUTER JOIN "
                            + "(SELECT  a.ID,a.ThingNo,a.CompanyCD,a.ThingName,b.ID as ID1,b.CodeName "
                            + "FROM  officedba.OfficeThingsInfo AS a LEFT OUTER JOIN "
                            + "officedba.CodeEquipmentType AS b "
                            + "ON a.TypeID = b.ID AND a.CompanyCD = b.CompanyCD) b "
                            + "ON a.ThingNo=b.ThingNo AND a.CompanyCD=b.CompanyCD "
                            + "LEFT OUTER JOIN "
                            + "(SELECT a.CompanyCD, a.UsedDate, b.ThingNo "
                            + "FROM officedba.OfficeThingsUsed AS a "
                            + "LEFT OUTER JOIN "
                            + "officedba.OfficeThingsUsedDetail AS b "
                            + "ON a.ApplyNo = b.ApplyNo AND a.CompanyCD = b.CompanyCD) c "
                            + "ON a.ThingNo=c.ThingNo AND a.CompanyCD=c.CompanyCD"
                            + " where a.CompanyCD='" + CompanyID + "'";
            if (ThingName != "")
                sql += " and b.ThingName like '%" + ThingName + "%' ";
            if (StartDate != "")
                sql += " and c.UsedDate>='" + StartDate + "'";
            if (EndDate != "")
                sql += " and c.UsedDate<='" + EndDate + "'";
            if (TypeID != "")
                sql += " and b.ID1=" + TypeID + "";
            sql = sql + ord;
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion
        #region 获取用品领用明细信息列表
        /// <summary>
        /// 获取用品领用明细信息列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetOfficeThingsUsedDetailInfo(string StartDate, string EndDate, string ThingName, string TypeID, string CompanyID,string ApplyUserID,string ApplyDeptID, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string sql = "SELECT distinct b.ID,a.ThingNo,b.ThingName,b.CodeName,c.EmployeeName,c.DeptName,c.UsedDate,SUM(a.UsedCount) UsedCount FROM "
                            +"(SELECT ThingNo,CompanyCD,ApplyNo,SUM([Count]) AS UsedCount "
                            +"FROM officedba.OfficeThingsUsedDetail GROUP BY ThingNo,ApplyNo,CompanyCD) a "
                            +"LEFT OUTER JOIN "
                            +"(SELECT  a.ID,a.ThingNo,a.CompanyCD,a.ThingName,b.ID as ID1,b.CodeName "
                            +"FROM  officedba.OfficeThingsInfo AS a LEFT OUTER JOIN "
                            +"officedba.CodeEquipmentType AS b "
                            +"ON a.TypeID = b.ID AND a.CompanyCD = b.CompanyCD) b "
                            +"ON a.ThingNo=b.ThingNo AND a.CompanyCD=b.CompanyCD "
                            +"LEFT OUTER JOIN "
                            + "(SELECT a.CompanyCD,a.EmployeeID,a.DeptID,a.ApplyNo,c.EmployeeName,d.DeptName, a.UsedDate, b.ThingNo "
                            +"FROM officedba.OfficeThingsUsed AS a "
                            +"LEFT OUTER JOIN "
                            +"officedba.OfficeThingsUsedDetail AS b "
                            +"ON a.ApplyNo = b.ApplyNo AND a.CompanyCD = b.CompanyCD "
							+"LEFT OUTER JOIN officedba.EmployeeInfo c "
							+"ON a.EmployeeID=c.ID "
							+"LEFT OUTER JOIN officedba.DeptInfo d "
							+"ON a.DeptID=d.ID) c "
                            +"ON a.ThingNo=c.ThingNo and a.ApplyNo=c.ApplyNo AND a.CompanyCD=c.CompanyCD "
                            + " where a.CompanyCD='" + CompanyID + "'";
            if (ThingName != "")
                sql += " and b.ThingName like '%" + ThingName + "%' ";
            if (StartDate != "")
                sql += " and c.UsedDate>='" + StartDate + "'";
            if (EndDate != "")
                sql += " and c.UsedDate<='" + EndDate + "'";
            if (TypeID != "")
                sql += " and b.ID1=" + TypeID + "";
            if (ApplyUserID!="")
                sql += " and c.EmployeeID=" + ApplyUserID + "";
            if (ApplyDeptID != "")
                sql += " and c.DeptID=" + ApplyDeptID + "";
            sql += " GROUP BY b.ID,a.ThingNo,b.ThingName,b.CodeName,c.EmployeeName,c.DeptName,c.UsedDate";
            return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
        }
        #endregion
        #region 获取用品领用明细信息列表
        /// <summary>
        /// 获取用品领用明细信息列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetOfficeThingsUsedDetailPrint(string StartDate, string EndDate, string ThingName, string TypeID, string CompanyID, string ApplyUserID, string ApplyDeptID,string ord)
        {
            string sql = "SELECT distinct b.ID,a.ThingNo,b.ThingName,b.CodeName,c.EmployeeName,c.DeptName,c.UsedDate,SUM(a.UsedCount) UsedCount FROM "
                            + "(SELECT ThingNo,CompanyCD,SUM([Count]) AS UsedCount "
                            + "FROM officedba.OfficeThingsUsedDetail GROUP BY ThingNo,CompanyCD) a "
                            + "LEFT OUTER JOIN "
                            + "(SELECT  a.ID,a.ThingNo,a.CompanyCD,a.ThingName,b.ID as ID1,b.CodeName "
                            + "FROM  officedba.OfficeThingsInfo AS a LEFT OUTER JOIN "
                            + "officedba.CodeEquipmentType AS b "
                            + "ON a.TypeID = b.ID AND a.CompanyCD = b.CompanyCD) b "
                            + "ON a.ThingNo=b.ThingNo AND a.CompanyCD=b.CompanyCD "
                            + "LEFT OUTER JOIN "
                            + "(SELECT a.CompanyCD,a.EmployeeID,a.DeptID,c.EmployeeName,d.DeptName, a.UsedDate, b.ThingNo "
                            + "FROM officedba.OfficeThingsUsed AS a "
                            + "LEFT OUTER JOIN "
                            + "officedba.OfficeThingsUsedDetail AS b "
                            + "ON a.ApplyNo = b.ApplyNo AND a.CompanyCD = b.CompanyCD "
                            + "LEFT OUTER JOIN officedba.EmployeeInfo c "
                            + "ON a.EmployeeID=c.ID "
                            + "LEFT OUTER JOIN officedba.DeptInfo d "
                            + "ON a.DeptID=d.ID) c "
                            + "ON a.ThingNo=c.ThingNo AND a.CompanyCD=c.CompanyCD "
                            + " where a.CompanyCD='" + CompanyID + "'";
            if (ThingName != "")
                sql += " and b.ThingName like '%" + ThingName + "%' ";
            if (StartDate != "")
                sql += " and c.UsedDate>='" + StartDate + "'";
            if (EndDate != "")
                sql += " and c.UsedDate<='" + EndDate + "'";
            if (TypeID != "")
                sql += " and b.ID1=" + TypeID + "";
            if (ApplyUserID != "")
                sql += " and c.EmployeeID=" + ApplyUserID + "";
            if (ApplyDeptID != "")
                sql += " and c.DeptID=" + ApplyDeptID + "";
            sql += " GROUP BY b.ID,a.ThingNo,b.ThingName,b.CodeName,c.EmployeeName,c.DeptName,c.UsedDate ";
            sql = sql + ord;
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion

        #region 获取用品领用统计信息列表
        /// <summary>
        /// 获取用品领用统计信息列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetOfficeThingsUsedCount(string StartDate, string EndDate, string SelectValue, string CompanyID, string ApplyDeptID, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            if (SelectValue == "0")
                SelectValue = "ThingName";
            else
                SelectValue = "CodeName";
            string sql = "SELECT D.DeptName,A.CompanyCD,C." + SelectValue + ",SUM(B.Count)AS UsedCount "
                            +"FROM officedba.OfficeThingsUsed A "
                            +"LEFT OUTER JOIN  "
                            +"officedba.OfficeThingsUsedDetail B "
                            +"ON A.CompanyCD=B.CompanyCD AND A.ApplyNo=B.ApplyNo "
                            +"LEFT OUTER JOIN "
                            +"("
                            +"SELECT  a.ThingNo,a.CompanyCD,a.ThingName,b.CodeName "
                            +"FROM  officedba.OfficeThingsInfo AS a LEFT OUTER JOIN "
                            +"officedba.CodeEquipmentType AS b "
                            +"ON a.TypeID = b.ID AND a.CompanyCD = b.CompanyCD "
                            +")C "
                            +"ON B.ThingNo=c.ThingNo AND B.CompanyCD=C.CompanyCD "
                            +"LEFT OUTER JOIN "
                            +"officedba.DeptInfo D "
                            +"ON A.DeptID=D.ID "
                            + " where  a.BillStatus<>'1' and a.CompanyCD='" + CompanyID + "'";
            if (StartDate != "")
                sql += " and a.UsedDate>='" + StartDate + "'";
            if (EndDate != "")
                sql += " and a.UsedDate<='" + EndDate + "'";
            //if (ApplyUserID != "")
            //    sql += " and c.EmployeeID=" + ApplyUserID + "";
            if (ApplyDeptID != "")
                sql += " and A.DeptID=" + ApplyDeptID + "";
            sql += " GROUP BY D.DeptName,A.CompanyCD,C." + SelectValue + "  ";
            return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
        }
        #endregion

        #region 获取用品领用统计信息打印列表
        /// <summary>
        /// 获取用品领用统计信息打印列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetOfficeThingsUsedCountPrint(string StartDate, string EndDate, string SelectValue, string CompanyID, string ApplyDeptID, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            if (SelectValue == "0")
                SelectValue = "ThingName";
            else
                SelectValue = "CodeName";
            string sql = "SELECT D.DeptName,A.CompanyCD,C." + SelectValue + ",SUM(B.Count)AS UsedCount "
                            + "FROM officedba.OfficeThingsUsed A "
                            + "LEFT OUTER JOIN  "
                            + "officedba.OfficeThingsUsedDetail B "
                            + "ON A.CompanyCD=B.CompanyCD AND A.ApplyNo=B.ApplyNo "
                            + "LEFT OUTER JOIN "
                            + "("
                            + "SELECT  a.ThingNo,a.CompanyCD,a.ThingName,b.CodeName "
                            + "FROM  officedba.OfficeThingsInfo AS a LEFT OUTER JOIN "
                            + "officedba.CodeEquipmentType AS b "
                            + "ON a.TypeID = b.ID AND a.CompanyCD = b.CompanyCD "
                            + ")C "
                            + "ON B.ThingNo=c.ThingNo AND B.CompanyCD=C.CompanyCD "
                            + "LEFT OUTER JOIN "
                            + "officedba.DeptInfo D "
                            + "ON A.DeptID=D.ID "
                            + " where a.CompanyCD='" + CompanyID + "'";
            if (StartDate != "")
                sql += " and a.UsedDate>='" + StartDate + "'";
            if (EndDate != "")
                sql += " and a.UsedDate<='" + EndDate + "'";
            //if (ApplyUserID != "")
            //    sql += " and c.EmployeeID=" + ApplyUserID + "";
            if (ApplyDeptID != "")
                sql += " and A.DeptID=" + ApplyDeptID + "";
            sql += " GROUP BY D.DeptName,A.CompanyCD,C." + SelectValue + "  ";
            sql = sql + ord;
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion
    }
}

/**********************************************
 * 类作用：   车辆管理数据库层处理
 * 建立人：   lysong
 * 建立时间： 2009/04/25
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
    /// 类名：CarInfoDBHelper
    /// 描述：车辆管理数据库层处理
    /// 作者：lysong
    /// 创建时间：2009/04/25
    /// </summary>
    public class CarInfoDBHelper
    {
        #region 添加车辆信息
        /// <summary>
        /// 添加车辆信息
        /// </summary>
        /// <param name="CarInfoM">车辆信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool AddCarInfo(CarInfoModel CarInfoM)
        {
            try
            {
                #region 添加车辆信息SQL拼写
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO officedba.CarInfo");
                sql.AppendLine("		(CompanyCD      ");
                sql.AppendLine("		,CarNo        ");
                sql.AppendLine("		,CarName        ");
                sql.AppendLine("		,CarMark        ");
                sql.AppendLine("		,CarType        ");
                sql.AppendLine("		,Factory        ");
                sql.AppendLine("		,Displacement        ");
                sql.AppendLine("		,SeatCount        ");
                sql.AppendLine("		,Carrying        ");
                sql.AppendLine("		,FuelType        ");
                sql.AppendLine("		,EngineNo        ");
                sql.AppendLine("		,BuyMoney        ");
                sql.AppendLine("		,BuyDate        ");
                sql.AppendLine("		,Motorman        ");
                sql.AppendLine("		,VendorName        ");
                sql.AppendLine("		,VendorAddress        ");
                sql.AppendLine("		,Contact        ");
                sql.AppendLine("		,ContactTel        ");
                sql.AppendLine("		,Status        ");
                sql.AppendLine("		,Creator        ");
                sql.AppendLine("		,CreateDate        ");
                sql.AppendLine("		,Remark        ");
                sql.AppendLine("		,ModifiedDate        ");
                sql.AppendLine("		,ModifiedUserID)        ");
                sql.AppendLine("VALUES                  ");
                sql.AppendLine("		(@CompanyCD   ");
                sql.AppendLine("		,@CarNo       ");
                sql.AppendLine("		,@CarName       ");
                sql.AppendLine("		,@CarMark       ");
                sql.AppendLine("		,@CarType       ");
                sql.AppendLine("		,@Factory       ");
                sql.AppendLine("		,@Displacement       ");
                sql.AppendLine("		,@SeatCount       ");
                sql.AppendLine("		,@Carrying       ");
                sql.AppendLine("		,@FuelType       ");
                sql.AppendLine("		,@EngineNo       ");
                sql.AppendLine("		,@BuyMoney       ");
                sql.AppendLine("		,@BuyDate       ");
                sql.AppendLine("		,@Motorman       ");
                sql.AppendLine("		,@VendorName       ");
                sql.AppendLine("		,@VendorAddress       ");
                sql.AppendLine("		,@Contact       ");
                sql.AppendLine("		,@ContactTel       ");
                sql.AppendLine("		,@Status       ");
                sql.AppendLine("		,@Creator       ");
                sql.AppendLine("		,@CreateDate       ");
                sql.AppendLine("		,@Remark       ");
                sql.AppendLine("		,@ModifiedDate       ");
                sql.AppendLine("		,@ModifiedUserID)       ");
                #endregion
                #region 添加车辆信息参数设置
                SqlParameter[] param;
                param = new SqlParameter[24];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CarInfoM.CompanyCD);
                param[1] = SqlHelper.GetParameter("@CarNo", CarInfoM.CarNo);
                param[2] = SqlHelper.GetParameter("@CarName", CarInfoM.CarName);
                param[3] = SqlHelper.GetParameter("@CarMark", CarInfoM.CarMark);
                param[4] = SqlHelper.GetParameter("@CarType", CarInfoM.CarType);
                param[5] = SqlHelper.GetParameter("@Factory", CarInfoM.Factory);
                param[6] = SqlHelper.GetParameter("@Displacement",Convert.ToDecimal(CarInfoM.Displacement));
                param[7] = SqlHelper.GetParameter("@SeatCount", CarInfoM.SeatCount);
                param[8] = SqlHelper.GetParameter("@Carrying", CarInfoM.Carrying);
                param[9] = SqlHelper.GetParameter("@FuelType", CarInfoM.FuelType);
                param[10] = SqlHelper.GetParameter("@EngineNo", CarInfoM.EngineNo);
                param[11] = SqlHelper.GetParameter("@BuyMoney", CarInfoM.BuyMoney);
                param[12] = SqlHelper.GetParameter("@BuyDate", CarInfoM.BuyDate);
                param[13] = SqlHelper.GetParameter("@Motorman", CarInfoM.Motorman);
                param[14] = SqlHelper.GetParameter("@VendorName", CarInfoM.VendorName);
                param[15] = SqlHelper.GetParameter("@VendorAddress", CarInfoM.VendorAddress);
                param[16] = SqlHelper.GetParameter("@Contact", CarInfoM.Contact);
                param[17] = SqlHelper.GetParameter("@ContactTel", CarInfoM.ContactTel);
                param[18] = SqlHelper.GetParameter("@Status", CarInfoM.Status);
                param[19] = SqlHelper.GetParameter("@Creator", CarInfoM.Creator);
                param[20] = SqlHelper.GetParameter("@CreateDate", CarInfoM.CreateDate);
                param[21] = SqlHelper.GetParameter("@Remark", CarInfoM.Remark);
                param[22] = SqlHelper.GetParameter("@ModifiedDate", CarInfoM.ModifiedDate);
                param[23] = SqlHelper.GetParameter("@ModifiedUserID", CarInfoM.ModifiedUserID);
                #endregion
                SqlHelper.ExecuteTransSql(sql.ToString(), param);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch
            {
                return false;
            }
        }
        #endregion
        #region 修改车辆信息
        /// <summary>
        /// 修改车辆信息
        /// </summary>
        /// <param name="CarInfoM">车辆信息</param>
        /// <returns>更新是否成功 false:失败，true:成功</returns>
        public static bool UpdateCarInfo(CarInfoModel CarInfoM)
        {
            try
            {
                #region 修改车辆信息SQL拼写
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE officedba.CarInfo SET ");
                sql.AppendLine("		 CarName=@CarName        ");
                sql.AppendLine("		,CarMark=@CarMark        ");
                sql.AppendLine("		,CarType=@CarType        ");
                sql.AppendLine("		,Factory=@Factory        ");
                sql.AppendLine("		,Displacement=@Displacement        ");
                sql.AppendLine("		,SeatCount=@SeatCount        ");
                sql.AppendLine("		,Carrying=@Carrying        ");
                sql.AppendLine("		,FuelType=@FuelType        ");
                sql.AppendLine("		,EngineNo=@EngineNo        ");
                sql.AppendLine("		,BuyMoney=@BuyMoney        ");
                sql.AppendLine("		,BuyDate=@BuyDate        ");
                sql.AppendLine("		,Motorman=@Motorman        ");
                sql.AppendLine("		,VendorName=@VendorName        ");
                sql.AppendLine("		,VendorAddress=@VendorAddress        ");
                sql.AppendLine("		,Contact=@Contact        ");
                sql.AppendLine("		,ContactTel=@ContactTel        ");
                sql.AppendLine("		,Status=@Status        ");
                //sql.AppendLine("		,Creator=@Creator        ");
                //sql.AppendLine("		,CreateDate=@CreateDate        ");
                sql.AppendLine("		,Remark=@Remark        ");
                sql.AppendLine("		,ModifiedDate=@ModifiedDate        ");
                sql.AppendLine("		,ModifiedUserID=@ModifiedUserID        ");
                sql.AppendLine(" WHERE ");
                sql.AppendLine("CarNo = @CarNo ");
                sql.AppendLine(" AND CompanyCD = @CompanyCD ");
                #endregion
                #region 修改车辆信息参数设置
                SqlParameter[] param;
                param = new SqlParameter[22];
                param[0] = SqlHelper.GetParameter("@CarName", CarInfoM.CarName);
                param[1] = SqlHelper.GetParameter("@CarMark", CarInfoM.CarMark);
                param[2] = SqlHelper.GetParameter("@CarType", CarInfoM.CarType);
                param[3] = SqlHelper.GetParameter("@Factory", CarInfoM.Factory);
                param[4] = SqlHelper.GetParameter("@Displacement", Convert.ToDecimal(CarInfoM.Displacement));
                param[5] = SqlHelper.GetParameter("@SeatCount", CarInfoM.SeatCount);
                param[6] = SqlHelper.GetParameter("@Carrying", CarInfoM.Carrying);
                param[7] = SqlHelper.GetParameter("@FuelType", CarInfoM.FuelType);
                param[8] = SqlHelper.GetParameter("@EngineNo", CarInfoM.EngineNo);
                param[9] = SqlHelper.GetParameter("@BuyMoney", CarInfoM.BuyMoney);
                param[10] = SqlHelper.GetParameter("@BuyDate", CarInfoM.BuyDate);
                param[11] = SqlHelper.GetParameter("@Motorman", CarInfoM.Motorman);
                param[12] = SqlHelper.GetParameter("@VendorName", CarInfoM.VendorName);
                param[13] = SqlHelper.GetParameter("@VendorAddress", CarInfoM.VendorAddress);
                param[14] = SqlHelper.GetParameter("@Contact", CarInfoM.Contact);
                param[15] = SqlHelper.GetParameter("@ContactTel", CarInfoM.ContactTel);
                param[16] = SqlHelper.GetParameter("@Status", CarInfoM.Status);
                //param[17] = SqlHelper.GetParameter("@Creator", CarInfoM.Creator);
                //param[18] = SqlHelper.GetParameter("@CreateDate", CarInfoM.CreateDate);
                param[17] = SqlHelper.GetParameter("@Remark", CarInfoM.Remark);
                param[18] = SqlHelper.GetParameter("@ModifiedDate", CarInfoM.ModifiedDate);
                param[19] = SqlHelper.GetParameter("@ModifiedUserID", CarInfoM.ModifiedUserID);
                param[20] = SqlHelper.GetParameter("@CarNo", CarInfoM.CarNo);
                param[21] = SqlHelper.GetParameter("@CompanyCD", CarInfoM.CompanyCD);
                #endregion
                SqlHelper.ExecuteTransSql(sql.ToString(), param);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch
            {
                return false;
            }
        }
        #endregion
        #region 查询车辆列表
        /// <summary>
        /// 查询车辆列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetCarInfoList(string CarNo, string CarName, string CarMark, string CarType, string CompanyID,int pageIndex,int pageCount,string ord, ref int TotalCount)
        {
            string sql = "SELECT a.ID, a.CarNo, a.CarName, a.CarMark," 
	                      +"CASE a.CarType WHEN 1 THEN '小客'"
	                      +"WHEN 2 THEN '大客'"
	                      +"WHEN 3 THEN '小货'"
	                      +"WHEN 4 THEN '大货'"
	                      +"WHEN 5 THEN '其他' End CarType, a.Factory, Displacement,"
                          + "convert(varchar(10),a.CreateDate,120)CreateDate,isnull(b.EmployeeName,'') AS Motorman,"
                          + "isnull(b.DeptName,'')DeptName,isnull(c.EmployeeName,'') AS Creator "
                          +"FROM  officedba.CarInfo a "
                          +"LEFT OUTER JOIN "
                          +"(select x.ID,x.EmployeeName,x.EmployeeNo,z.DeptName "
                          +"from officedba.EmployeeInfo x "
                          +"left outer join officedba.DeptInfo z "
                          +"on x.DeptID=z.ID) b "
                          +"ON a.Motorman=b.ID "
                          +"LEFT OUTER JOIN   "
                          +"(select x.ID,x.EmployeeName,x.EmployeeNo,z.DeptName "
                          +"from officedba.EmployeeInfo x "
                          +"LEFT OUTER JOIN officedba.DeptInfo z "
                          +"on x.DeptID=z.ID) c "
                          +"ON a.Creator=c.ID "
                          + " WHERE CompanyCD='" + CompanyID + "'  ";
            if (CarNo != "")
                sql += " and a.CarNo LIKE '%" + CarNo + "%'";
            if (CarName != "")
                sql += " and a.CarName LIKE '%" + CarName + "%'";
            if (CarMark != "")
                sql += " and a.CarMark LIKE '%" + CarMark + "%'";
            if (CarType != "")
                sql += " and a.CarType LIKE '%" + CarType + "%'";
            return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);

            //return SqlHelper.ExecuteSql(sql);
        }
        #endregion
        #region 由车辆编号获取信息，查看或修改
        /// <summary>
        /// 由车辆编号获取信息，查看或修改
        /// </summary>
        /// <param name="CarNo">车辆编号</param>
        /// <returns>DataTable</returns>
        public static DataTable GetCarInfoByCarNo(string CarNo, string CompanyID)
        {
            string sql = "SELECT a.* ,b.EmployeeName AS MotormanName,b.DeptName,c.EmployeeName AS CreatorName "
                         +"FROM  officedba.CarInfo a "
                         +"INNER JOIN "
                         +"(select x.ID,x.EmployeeName,x.EmployeeNo,z.DeptName "
                         +"from officedba.EmployeeInfo x "
                         +"LEFT OUTER JOIN officedba.DeptInfo z "
                         +"on x.DeptID=z.ID) b "
                         +"ON a.Motorman=b.ID "
                         +"LEFT OUTER JOIN  "
                         + "(select x.ID,x.EmployeeName,x.EmployeeNo,z.DeptName "
                         + "from officedba.EmployeeInfo x "
                         + "LEFT OUTER JOIN officedba.DeptInfo z "
                         + "on x.DeptID=z.ID) c "
                         + "ON a.Creator=c.ID WHERE a.CarNo='" + CarNo + "' AND a.CompanyCD='" + CompanyID + "'";
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion
        #region 判断能否删除车辆信息
        /// <summary>
        /// 判断能否删除车辆信息
        /// </summary>
        /// <param name="EmployeeID">员工ID</param>
        /// <param name="CompanyID">公司代码</param>
        /// <returns></returns>
        public static bool IfDeleteCarInfo(string CarInfoNos, string CompanyID)
        {
            string[] NOS = null;
            NOS = CarInfoNos.Split(',');
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
        #region 判断能否删除车辆信息
        /// <summary>
        /// 判断能否删除车辆信息
        /// </summary>
        /// <param name="EmployeeID">员工ID</param>
        /// <param name="CompanyID">公司代码</param>
        /// <returns></returns>
        public static bool IsExistInfo(string CarNo, string CompanyID)
        {

            string sql = "SELECT * FROM officedba.CarApply WHERE CarNo='" + CarNo + "' AND CompanyCD='" + CompanyID + "'";
            return SqlHelper.ExecuteSql(sql).Rows.Count > 0 ? true : false;
        }
        #endregion
        #region 删除车辆信息
        /// <summary>
        /// 删除车辆信息
        /// </summary>
        /// <param name="EquipmentIDS">设备IDS</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool DeleteCarInfo(string ApplyIDS, string CompanyID)
        {
            string allApplyID = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string[] Delsql = new string[1];
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
                Delsql[0] = "DELETE FROM officedba.CarInfo WHERE CarNo IN (" + allApplyID + ") AND CompanyCD='" + CompanyID + "'";
                SqlHelper.ExecuteTransForListWithSQL(Delsql);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 查询车辆列表
        /// <summary>
        /// 查询车辆列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetCarInfoBrowseList(string StartDate,string EndDate, string CarType, string CompanyID,int pageIndex,int pageCount,string ord, ref int TotalCount)
        {
            string sql = "SELECT a.ID, a.CarNo, a.CarName, a.CarMark,"
                          + "CASE a.CarType WHEN 1 THEN '小客' "
                          +"WHEN 2 THEN '大客' "
                          +"WHEN 3 THEN '小货' "
                          +"WHEN 4 THEN '大货' "
                          +"WHEN 5 THEN '其他' End CarType, "
                          +"isnull(b.EmployeeName,'') AS Motorman, "
                          + "Convert(varchar(10),a.BuyDate,120)BuyDate,a.BuyMoney,"
						  +"CASE a.Status "
						  +"WHEN '1' THEN '正常' "
						  +"WHEN '2' THEN '停用' "
					      +"END Status "
                          +"FROM  officedba.CarInfo a "
                          +"LEFT OUTER JOIN "
                          +"(select x.ID,x.EmployeeName,x.EmployeeNo,z.DeptName "
                          +"from officedba.EmployeeInfo x "
                          +"left outer join officedba.DeptInfo z "
                          +"on x.DeptID=z.ID) b "
                          +"ON a.Motorman=b.ID "
                          +"LEFT OUTER JOIN "
                          +"(select x.ID,x.EmployeeName,x.EmployeeNo,z.DeptName "
                          +"from officedba.EmployeeInfo x "
                          +"LEFT OUTER JOIN officedba.DeptInfo z "
                          +"on x.DeptID=z.ID) c "
                          +"ON a.Creator=c.ID"
                          + " WHERE CompanyCD='" + CompanyID + "'  ";
            if (StartDate != "")
                sql += " and a.BuyDate >= '" + StartDate + "'";
            if (EndDate != "")
                sql += " and a.BuyDate <= '" + EndDate + "'";
            if (CarType != "")
                sql += " and a.CarType = '" + CarType + "'";
            return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            //return SqlHelper.ExecuteSql(sql);
        }
        #endregion

        #region 查询车辆列表
        /// <summary>
        /// 查询车辆列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetCarInfoBrowsePrintList(string StartDate, string EndDate, string CarType, string CompanyID,string ord)
        {
            string sql = "SELECT a.ID, a.CarNo, a.CarName, a.CarMark,"
                          + "CASE a.CarType WHEN 1 THEN '小客' "
                          + "WHEN 2 THEN '大客' "
                          + "WHEN 3 THEN '小货' "
                          + "WHEN 4 THEN '大货' "
                          + "WHEN 5 THEN '其他' End CarType, "
                          + "isnull(b.EmployeeName,'') AS Motorman, "
                          + "Convert(varchar(10),a.BuyDate,120)BuyDate,a.BuyMoney,"
                          + "CASE a.Status "
                          + "WHEN '1' THEN '正常' "
                          + "WHEN '2' THEN '停用' "
                          + "END Status "
                          + "FROM  officedba.CarInfo a "
                          + "LEFT OUTER JOIN "
                          + "(select x.ID,x.EmployeeName,x.EmployeeNo,z.DeptName "
                          + "from officedba.EmployeeInfo x "
                          + "left outer join officedba.DeptInfo z "
                          + "on x.DeptID=z.ID) b "
                          + "ON a.Motorman=b.ID "
                          + "LEFT OUTER JOIN "
                          + "(select x.ID,x.EmployeeName,x.EmployeeNo,z.DeptName "
                          + "from officedba.EmployeeInfo x "
                          + "LEFT OUTER JOIN officedba.DeptInfo z "
                          + "on x.DeptID=z.ID) c "
                          + "ON a.Creator=c.ID"
                          + " WHERE CompanyCD='" + CompanyID + "'  ";
            if (StartDate != "")
                sql += " and a.BuyDate >= '" + StartDate + "'";
            if (EndDate != "")
                sql += " and a.BuyDate <= '" + EndDate + "'";
            if (CarType != "")
                sql += " and a.CarType = '" + CarType + "'";
            sql = sql + ord;
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion

        #region 车辆使用状况月报表
        /// <summary>
        /// 车辆使用状况月报表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetCarUsedMonthList(string StartDate, string EndDate, string CarMark, string CompanyID, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string sql = "select A.CompanyCD,A.CarNo,B.TotalMileage,C.CurrMileage,D.TotalGasFee,E.CurrGasFee,"
                            +"(ISNULL(F.TotalMaintainFee,0)+ISNULL(X.TotalRepairFee,0))TotalMaintainFee,"
                            +"(ISNULL(G.CurrMaintainFee,0)+ISNULL(Y.CurrRepairFee,0))CurrMaintainFee,"
                            +"H.TotalPeccanceyTime,I.CurrPeccanceyTime,J.TotalAccidentTime,K.CurrAccidentTime,"
                            +"A.CarMark,CASE A.CarType WHEN '1' THEN '小客' WHEN '2' THEN '大客' WHEN '3' THEN '小货' WHEN '4' THEN '大货' WHEN '5' "
                            +"THEN '其他' END CarType,A.CarName,ISNULL(L.EmployeeName,'')EmployeeName "
                            +"from officedba.CarInfo A "
                            +"left join (select CompanyCD,CarNo,ISNULL(SUM(RealMileage) ,0)TotalMileage "
                             +"from officedba.CarDispatch "
                            +"GROUP BY CompanyCD,CarNo)B "
                            +"ON A.CarNo=B.CarNo and A.CompanyCD=B.CompanyCD "
                            +"left join "
                            +"(select CompanyCD,CarNo,ISNULL(SUM(RealMileage),0)CurrMileage from officedba.CarDispatch "
                            + "WHERE BackDate>='" + StartDate + "' AND BackDate<='" + EndDate + "' GROUP BY CompanyCD,CarNo) C "
                            +"ON A.CompanyCD=C.CompanyCD AND A.CarNo=C.CarNo "
                            +"LEFT JOIN "
                            +"(select CompanyCD,CarNo,Sum(Fee)TotalGasFee from officedba.CarAddGas  Group BY CompanyCD,CarNo)D "
                            +"ON A.CompanyCD=D.CompanyCD AND A.CarNo=D.CarNo "
                            +"left join "
                            +"(select CompanyCD,CarNo,Sum(Fee)CurrGasFee from officedba.CarAddGas "
                            + "WHERE HappenDate>='" + StartDate + "' AND HappenDate<='" + EndDate + "' Group BY CompanyCD,CarNo) E "
                            +"ON A.CompanyCD=E.CompanyCD AND A.CarNo=E.CarNo "
                            +"LEFT JOIN "
                            +"(SELECT CompanyCD,CarNo,Sum(Fee)TotalMaintainFee FROM officedba.CarMaintain "
                            +"GROUP BY CompanyCD,CarNo)F "
                            +"ON A.CompanyCD=F.CompanyCD AND A.CarNo=F.CarNo "
                            +"LEFT JOIN "
                            + "(SELECT CompanyCD,CarNo,Sum(Fee)CurrMaintainFee FROM officedba.CarMaintain WHERE HappenDate>='" + StartDate + "' AND HappenDate<='" + EndDate + "' "
                            +"GROUP BY CompanyCD,CarNo)G ON A.CompanyCD=G.CompanyCD AND A.CarNo=G.CarNo "
                            +"LEFT JOIN "
                            +"(SELECT CompanyCD,CarNo,Sum(Fee)TotalRepairFee FROM officedba.CarRepair "
                            +"GROUP BY CompanyCD,CarNo)X "
                            +"ON A.CompanyCD=X.CompanyCD AND A.CarNo=X.CarNo "
                            +"LEFT JOIN "
                            + "(SELECT CompanyCD,CarNo,Sum(Fee)CurrRepairFee FROM officedba.CarRepair WHERE HappenDate>='" + StartDate + "' AND HappenDate<='" + EndDate + "' "
                            +"GROUP BY CompanyCD,CarNo)Y ON A.CompanyCD=Y.CompanyCD AND A.CarNo=Y.CarNo "
                            +"LEFT JOIN "
                            +"(SELECT CompanyCD,CarNo,COUNT(*)TotalPeccanceyTime FROM officedba.CarPeccancy "
                            +"GROUP BY CompanyCD,CarNo) H ON A.CompanyCD=H.CompanyCD AND A.CarNo=H.CarNo "
                            +"LEFT JOIN "
                            + "(SELECT CompanyCD,CarNo,COUNT(*)CurrPeccanceyTime FROM officedba.CarPeccancy WHERE HappenDate>='" + StartDate + "' AND HappenDate<='" + EndDate + "' "
                            +"GROUP BY CompanyCD,CarNo) I on A.CompanyCD=I.CompanyCD AND A.CarNo=I.CarNo "
                            +"LEFT JOIN "
                            +"(SELECT CompanyCD,CarNo,COUNT(*)TotalAccidentTime FROM officedba.CarAccident GROUP BY CompanyCD,CarNo) J "
                             +"ON A.CompanyCD=J.CompanyCD AND A.CarNo=J.CarNo "
                            +"LEFT JOIN "
                            + "(SELECT CompanyCD,CarNo,COUNT(*)CurrAccidentTime FROM officedba.CarAccident WHERE HappenDate>='" + StartDate + "' "
                            + "AND HappenDate<='" + EndDate + "' GROUP BY CompanyCD,CarNo) K on A.CompanyCD=K.CompanyCD AND A.CarNo=K.CarNo "
                            +"LEFT OUTER JOIN officedba.EmployeeInfo L ON A.CompanyCD=L.CompanyCD and A.Motorman=L.ID"
                            + " WHERE A.CompanyCD='" + CompanyID + "'  ";
            if (CarMark != "")
                sql += " and A.CarMark like '%" + CarMark + "%'";
            return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            //return SqlHelper.ExecuteSql(sql);
        }
        #endregion
        #region 车辆使用状况月报表打印
        /// <summary>
        /// 车辆使用状况月报表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetCarUsedMonthList(string StartDate, string EndDate, string CarMark, string CompanyID,string ord)
        {
            string sql = "select A.CompanyCD,A.CarNo,B.TotalMileage,C.CurrMileage,D.TotalGasFee,E.CurrGasFee,"
                            + "(ISNULL(F.TotalMaintainFee,0)+ISNULL(X.TotalRepairFee,0))TotalMaintainFee,"
                            + "(ISNULL(G.CurrMaintainFee,0)+ISNULL(Y.CurrRepairFee,0))CurrMaintainFee,"
                            + "H.TotalPeccanceyTime,I.CurrPeccanceyTime,J.TotalAccidentTime,K.CurrAccidentTime,"
                            + "A.CarMark,CASE A.CarType WHEN '1' THEN '小客' WHEN '2' THEN '大客' WHEN '3' THEN '小货' WHEN '4' THEN '大货' WHEN '5' "
                            + "THEN '其他' END CarType,A.CarName,ISNULL(L.EmployeeName,'')EmployeeName "
                            + "from officedba.CarInfo A "
                            + "left join (select CompanyCD,CarNo,ISNULL(SUM(RealMileage) ,0)TotalMileage "
                             + "from officedba.CarDispatch "
                            + "GROUP BY CompanyCD,CarNo)B "
                            + "ON A.CarNo=B.CarNo and A.CompanyCD=B.CompanyCD "
                            + "left join "
                            + "(select CompanyCD,CarNo,ISNULL(SUM(RealMileage),0)CurrMileage from officedba.CarDispatch "
                            + "WHERE BackDate>='" + StartDate + "' AND BackDate<='" + EndDate + "' GROUP BY CompanyCD,CarNo) C "
                            + "ON A.CompanyCD=C.CompanyCD AND A.CarNo=C.CarNo "
                            + "LEFT JOIN "
                            + "(select CompanyCD,CarNo,Sum(Fee)TotalGasFee from officedba.CarAddGas  Group BY CompanyCD,CarNo)D "
                            + "ON A.CompanyCD=D.CompanyCD AND A.CarNo=D.CarNo "
                            + "left join "
                            + "(select CompanyCD,CarNo,Sum(Fee)CurrGasFee from officedba.CarAddGas "
                            + "WHERE HappenDate>='" + StartDate + "' AND HappenDate<='" + EndDate + "' Group BY CompanyCD,CarNo) E "
                            + "ON A.CompanyCD=E.CompanyCD AND A.CarNo=E.CarNo "
                            + "LEFT JOIN "
                            + "(SELECT CompanyCD,CarNo,Sum(Fee)TotalMaintainFee FROM officedba.CarMaintain "
                            + "GROUP BY CompanyCD,CarNo)F "
                            + "ON A.CompanyCD=F.CompanyCD AND A.CarNo=F.CarNo "
                            + "LEFT JOIN "
                            + "(SELECT CompanyCD,CarNo,Sum(Fee)CurrMaintainFee FROM officedba.CarMaintain WHERE HappenDate>='" + StartDate + "' AND HappenDate<='" + EndDate + "' "
                            + "GROUP BY CompanyCD,CarNo)G ON A.CompanyCD=G.CompanyCD AND A.CarNo=G.CarNo "
                            + "LEFT JOIN "
                            + "(SELECT CompanyCD,CarNo,Sum(Fee)TotalRepairFee FROM officedba.CarRepair "
                            + "GROUP BY CompanyCD,CarNo)X "
                            + "ON A.CompanyCD=X.CompanyCD AND A.CarNo=X.CarNo "
                            + "LEFT JOIN "
                            + "(SELECT CompanyCD,CarNo,Sum(Fee)CurrRepairFee FROM officedba.CarRepair WHERE HappenDate>='" + StartDate + "' AND HappenDate<='" + EndDate + "' "
                            + "GROUP BY CompanyCD,CarNo)Y ON A.CompanyCD=Y.CompanyCD AND A.CarNo=Y.CarNo "
                            + "LEFT JOIN "
                            + "(SELECT CompanyCD,CarNo,COUNT(*)TotalPeccanceyTime FROM officedba.CarPeccancy "
                            + "GROUP BY CompanyCD,CarNo) H ON A.CompanyCD=H.CompanyCD AND A.CarNo=H.CarNo "
                            + "LEFT JOIN "
                            + "(SELECT CompanyCD,CarNo,COUNT(*)CurrPeccanceyTime FROM officedba.CarPeccancy WHERE HappenDate>='" + StartDate + "' AND HappenDate<='" + EndDate + "' "
                            + "GROUP BY CompanyCD,CarNo) I on A.CompanyCD=I.CompanyCD AND A.CarNo=I.CarNo "
                            + "LEFT JOIN "
                            + "(SELECT CompanyCD,CarNo,COUNT(*)TotalAccidentTime FROM officedba.CarAccident GROUP BY CompanyCD,CarNo) J "
                             + "ON A.CompanyCD=J.CompanyCD AND A.CarNo=J.CarNo "
                            + "LEFT JOIN "
                            + "(SELECT CompanyCD,CarNo,COUNT(*)CurrAccidentTime FROM officedba.CarAccident WHERE HappenDate>='" + StartDate + "' "
                            + "AND HappenDate<='" + EndDate + "' GROUP BY CompanyCD,CarNo) K on A.CompanyCD=K.CompanyCD AND A.CarNo=K.CarNo "
                            + "LEFT OUTER JOIN officedba.EmployeeInfo L ON A.CompanyCD=L.CompanyCD and A.Motorman=L.ID"
                            + " WHERE A.CompanyCD='" + CompanyID + "'  ";
            if (CarMark != "")
                sql += " and A.CarMark like '%" + CarMark + "%'";
            sql = sql + ord;
            return SqlHelper.ExecuteSql(sql);
            //return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            //return SqlHelper.ExecuteSql(sql);
        }
        #endregion
        #region 车辆费用支出月报表
        /// <summary>
        /// 车辆费用支出月报表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetCarUsedCostMonthList(string StartDate, string EndDate, string CarMark, string CompanyID, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string sql = "select A.CompanyCD,A.CarNo,C.CurrMileage,E.CurrGasFee,H.CurrInsuranceFee,"
                            + "(ISNULL(G.CurrMaintainFee,0)+ISNULL(Y.CurrRepairFee,0))CurrMaintainFee,"
                            +"(Convert(decimal(20, 2),ISNULL(C.CurrMileage,0))+Convert(decimal(20, 2),Isnull(E.CurrGasFee,0))+Convert(decimal(20, 2),isnull(H.CurrInsuranceFee,0))+Convert(decimal(20, 2),isnull(G.CurrMaintainFee,0))++Convert(decimal(20, 2),isnull(Y.CurrRepairFee,0))) TotalCost,"
                            +"A.CarMark,CASE A.CarType WHEN '1' THEN '小客' WHEN '2' THEN '大客' WHEN '3' THEN '小货' WHEN '4' THEN '大货' WHEN '5' "
                            +"THEN '其他' END CarType,A.CarName,ISNULL(L.EmployeeName,'')EmployeeName "
                            +"from officedba.CarInfo A "
                            +"left join "
                            +"(select CompanyCD,CarNo,ISNULL(SUM(RealMileage),0)CurrMileage from officedba.CarDispatch "
                            + "WHERE BackDate>='" + StartDate + "' AND BackDate<='" + EndDate + "' GROUP BY CompanyCD,CarNo) C "
                            +"ON A.CompanyCD=C.CompanyCD AND A.CarNo=C.CarNo "
                            +"left join "
                            +"(select CompanyCD,CarNo,Sum(Fee)CurrGasFee from officedba.CarAddGas "
                            + "WHERE  HappenDate>='" + StartDate + "' AND HappenDate<='" + EndDate + "' Group BY CompanyCD,CarNo) E "
                            +"ON A.CompanyCD=E.CompanyCD AND A.CarNo=E.CarNo " 
                            +"LEFT JOIN "
                            + "(SELECT CompanyCD,CarNo,Sum(Fee)CurrMaintainFee FROM officedba.CarMaintain WHERE HappenDate>='" + StartDate + "' AND HappenDate<='"+EndDate+"' "
                             +"GROUP BY CompanyCD,CarNo)G ON A.CompanyCD=G.CompanyCD AND A.CarNo=G.CarNo "
                            +"LEFT JOIN "
                            + "(SELECT CompanyCD,CarNo,Sum(Fee)CurrRepairFee FROM officedba.CarRepair WHERE HappenDate>='" + StartDate + "' AND HappenDate<='" + EndDate + "' "
                             +"GROUP BY CompanyCD,CarNo)Y ON A.CompanyCD=Y.CompanyCD AND A.CarNo=Y.CarNo "
                            +"LEFT JOIN "
                            + "(SELECT CompanyCD,CarNo,Sum(Fee)CurrInsuranceFee FROM officedba.CarInsurance WHERE HappenDate>='" + StartDate + "' AND HappenDate<='" + EndDate + "' "
                             +"GROUP BY CompanyCD,CarNo)H ON A.CompanyCD=H.CompanyCD AND A.CarNo=H.CarNo "
                            +"LEFT OUTER JOIN officedba.EmployeeInfo L ON A.CompanyCD=L.CompanyCD and A.Motorman=L.ID "
                            + " WHERE A.CompanyCD='" + CompanyID + "'  ";
            if (CarMark != "")
                sql += " and A.CarMark like '%" + CarMark + "%'";
            return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            //return SqlHelper.ExecuteSql(sql);
        }
        #endregion
        #region 车辆使用状况月报表打印
        /// <summary>
        /// 车辆使用状况月报表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetCarUsedCostMonthPrint(string StartDate, string EndDate, string CarMark, string CompanyID, string ord)
        {
            string sql = "select A.CompanyCD,A.CarNo,C.CurrMileage,E.CurrGasFee,H.CurrInsuranceFee,"
                            + "(ISNULL(G.CurrMaintainFee,0)+ISNULL(Y.CurrRepairFee,0))CurrMaintainFee,"
                            + "(Convert(decimal(20, 2),ISNULL(C.CurrMileage,0))+Convert(decimal(20, 2),Isnull(E.CurrGasFee,0))+Convert(decimal(20, 2),isnull(H.CurrInsuranceFee,0))+Convert(decimal(20, 2),isnull(G.CurrMaintainFee,0))++Convert(decimal(20, 2),isnull(Y.CurrRepairFee,0))) TotalCost,"
                            + "A.CarMark,CASE A.CarType WHEN '1' THEN '小客' WHEN '2' THEN '大客' WHEN '3' THEN '小货' WHEN '4' THEN '大货' WHEN '5' "
                            + "THEN '其他' END CarType,A.CarName,ISNULL(L.EmployeeName,'')EmployeeName "
                            + "from officedba.CarInfo A "
                            + "left join "
                            + "(select CompanyCD,CarNo,ISNULL(SUM(RealMileage),0)CurrMileage from officedba.CarDispatch "
                            + "WHERE BackDate>='" + StartDate + "' AND BackDate<='" + EndDate + "' GROUP BY CompanyCD,CarNo) C "
                            + "ON A.CompanyCD=C.CompanyCD AND A.CarNo=C.CarNo "
                            + "left join "
                            + "(select CompanyCD,CarNo,Sum(Fee)CurrGasFee from officedba.CarAddGas "
                            + "WHERE  HappenDate>='" + StartDate + "' AND HappenDate<='" + EndDate + "' Group BY CompanyCD,CarNo) E "
                            + "ON A.CompanyCD=E.CompanyCD AND A.CarNo=E.CarNo "
                            + "LEFT JOIN "
                            + "(SELECT CompanyCD,CarNo,Sum(Fee)CurrMaintainFee FROM officedba.CarMaintain WHERE HappenDate>='" + StartDate + "' AND HappenDate<='" + EndDate + "' "
                             + "GROUP BY CompanyCD,CarNo)G ON A.CompanyCD=G.CompanyCD AND A.CarNo=G.CarNo "
                            + "LEFT JOIN "
                            + "(SELECT CompanyCD,CarNo,Sum(Fee)CurrRepairFee FROM officedba.CarRepair WHERE HappenDate>='" + StartDate + "' AND HappenDate<='" + EndDate + "' "
                             + "GROUP BY CompanyCD,CarNo)Y ON A.CompanyCD=Y.CompanyCD AND A.CarNo=Y.CarNo "
                            + "LEFT JOIN "
                            + "(SELECT CompanyCD,CarNo,Sum(Fee)CurrInsuranceFee FROM officedba.CarInsurance WHERE HappenDate>='" + StartDate + "' AND HappenDate<='" + EndDate + "' "
                             + "GROUP BY CompanyCD,CarNo)H ON A.CompanyCD=H.CompanyCD AND A.CarNo=H.CarNo "
                            + "LEFT OUTER JOIN officedba.EmployeeInfo L ON A.CompanyCD=L.CompanyCD and A.Motorman=L.ID "
                            + " WHERE A.CompanyCD='" + CompanyID + "'  ";
            if (CarMark != "")
                sql += " and A.CarMark like '%" + CarMark + "%'";
            sql = sql + ord;
            return SqlHelper.ExecuteSql(sql);
            //return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            //return SqlHelper.ExecuteSql(sql);
        }
        #endregion
    }
}

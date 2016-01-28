/**********************************************
 * 类作用：   设备维修添加数据库层处理
 * 建立人：   lysong
 * 建立时间： 2009/03/26
 ***********************************************/
using System;
using XBase.Model.Office.AdminManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using XBase.Common;

namespace XBase.Data.Office.AdminManager
{
   public class EquipmentRepairDBHelper
   { 
       #region 设备维修添加操作
       /// <summary>
        /// 设备维修添加操作
        /// </summary>
       /// <param name="EquipRepairModel">设备维修信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool AddEquipmentRepairInfo(EquipmentRepairModel EquipRepairModel, out int RetValID, int CreateUserID, string CreateDate)
        {
            try
            {
                #region 添加设备领用SQL拼写
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO officedba.EquipmentRepair");
                sql.AppendLine("		(RecordNo      ");
                sql.AppendLine("		,EquipmentNo        ");
                sql.AppendLine("		,CompanyCD        ");
                sql.AppendLine("		,ReportUserID        ");
                sql.AppendLine("		,DeptID        ");
                sql.AppendLine("		,Trouble        ");
                sql.AppendLine("		,Date        ");
                sql.AppendLine("		,TroubleLevel        ");
                sql.AppendLine("		,HopeRepairDate        ");
                sql.AppendLine("		,RepairType        ");
                sql.AppendLine("		,ToRepairDate        ");
                sql.AppendLine("		,PlanDate        ");
                sql.AppendLine("		,SolveDate        ");
                sql.AppendLine("		,RepairUser        ");
                sql.AppendLine("		,RepairHours        ");
                sql.AppendLine("		,TroubleStatus        ");
                sql.AppendLine("		,PlanCost        ");
                sql.AppendLine("		,CreateUser        ");
                sql.AppendLine("		,CreateDate        ");
                //sql.AppendLine("		,FactCost        ");
               // sql.AppendLine("		,PartsCheck        ");
                sql.AppendLine("		,ModifiedDate        ");
                sql.AppendLine("		,ModifiedUserID        ");
                sql.AppendLine("		,Remark)        ");
                sql.AppendLine("VALUES                  ");
                sql.AppendLine("		(@RecordNo   ");
                sql.AppendLine("		,@EquipmentNo       ");
                sql.AppendLine("		,@CompanyCD       ");
                sql.AppendLine("		,@ReportUserID       ");
                sql.AppendLine("		,@DeptID       ");
                sql.AppendLine("		,@Trouble       ");
                sql.AppendLine("		,@Date       ");
                sql.AppendLine("		,@TroubleLevel       ");
                sql.AppendLine("		,@HopeRepairDate       ");
                sql.AppendLine("		,@RepairType       ");
                sql.AppendLine("		,@ToRepairDate       ");
                sql.AppendLine("		,@PlanDate       ");
                sql.AppendLine("		,@SolveDate       ");
                sql.AppendLine("		,@RepairUser       ");
                sql.AppendLine("		,@RepairHours       ");
                sql.AppendLine("		,@TroubleStatus       ");
                sql.AppendLine("		,@PlanCost       ");
                sql.AppendLine("		,@CreateUser       ");
                sql.AppendLine("		,@CreateDate       ");
                //sql.AppendLine("		,@FactCost       ");
                //sql.AppendLine("		,@PartsCheck       ");
                sql.AppendLine("		,@ModifiedDate       ");
                sql.AppendLine("		,@ModifiedUserID       ");
                sql.AppendLine("		,@Remark)       ");
                sql.AppendLine("set @ID=@@IDENTITY");
                #endregion
                #region 添加设备领用参数设置
                SqlParameter[] param;
                param = new SqlParameter[23];
                param[0] = SqlHelper.GetParameter("@RecordNo", EquipRepairModel.RecordNo);
                param[1] = SqlHelper.GetParameter("@EquipmentNo", EquipRepairModel.EquipmentNo);
                param[2] = SqlHelper.GetParameter("@CompanyCD", EquipRepairModel.CompanyCD);
                param[3] = SqlHelper.GetParameter("@ReportUserID", EquipRepairModel.ReportUserID);
                param[4] = SqlHelper.GetParameter("@DeptID", EquipRepairModel.DeptID);
                param[5] = SqlHelper.GetParameter("@Trouble", EquipRepairModel.Trouble);
                param[6] = SqlHelper.GetParameter("@Date", EquipRepairModel.Date);
                param[7] = SqlHelper.GetParameter("@TroubleLevel", EquipRepairModel.TroubleLevel);
                param[8] = SqlHelper.GetParameter("@HopeRepairDate", EquipRepairModel.HopeRepairDate);
                param[9] = SqlHelper.GetParameter("@RepairType", EquipRepairModel.RepairType);
                param[10] = SqlHelper.GetParameter("@ToRepairDate", EquipRepairModel.ToRepairDate);
                param[11] = SqlHelper.GetParameter("@PlanDate", EquipRepairModel.PlanDate);
                param[12] = SqlHelper.GetParameter("@SolveDate", EquipRepairModel.SolveDate);
                param[13] = SqlHelper.GetParameter("@RepairUser", EquipRepairModel.RepairUser);
                param[14] = SqlHelper.GetParameter("@RepairHours", EquipRepairModel.RepairHours);
                param[15] = SqlHelper.GetParameter("@TroubleStatus", EquipRepairModel.TroubleStatus);
                param[16] = SqlHelper.GetParameter("@PlanCost", EquipRepairModel.PlanCost);
                param[17] = SqlHelper.GetParameter("@CreateUser", CreateUserID);
                param[18] = SqlHelper.GetParameter("@CreateDate", Convert.ToDateTime(CreateDate));
               // param[19] = SqlHelper.GetParameter("@FactCost", EquipRepairModel.FactCost);
                //param[20] = SqlHelper.GetParameter("@PartsCheck", EquipRepairModel.PartsCheck);
                param[19] = SqlHelper.GetParameter("@ModifiedDate", EquipRepairModel.ModifiedDate);
                param[20] = SqlHelper.GetParameter("@ModifiedUserID", EquipRepairModel.ModifiedUserID);
                param[21] = SqlHelper.GetParameter("@Remark", EquipRepairModel.Remark);
                param[22] = SqlHelper.GetOutputParameter("@ID", SqlDbType.Int);
                #endregion
                SqlHelper.ExecuteTransSql(sql.ToString(), param);
                RetValID = Convert.ToInt32(param[22].Value.ToString());
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch 
            {
                RetValID = 0;
                return false;
            }
        }
        #endregion
       #region 设备维修更新操作
        /// <summary>
        /// 设备维修修改操作
        /// </summary>
        /// <param name="EquipRepairModel">设备维修修改操作</param>
        /// <returns>修改是否成功 false:失败，true:成功</returns>
        public static bool UpdateEquipmentRepairInfo(EquipmentRepairModel EquipRepairModel)
        {
            try
            {
                #region 更新设备申请信息SQL拼写
                StringBuilder EquipRepairSql = new StringBuilder();
                EquipRepairSql.AppendLine("UPDATE officedba.EquipmentRepair SET ");
                EquipRepairSql.AppendLine("EquipmentNo=           @EquipmentNo, ");
                EquipRepairSql.AppendLine("CompanyCD =        @CompanyCD , ");
                EquipRepairSql.AppendLine("ReportUserID =        @ReportUserID , ");
                EquipRepairSql.AppendLine("DeptID =        @DeptID , ");
                EquipRepairSql.AppendLine("Trouble =        @Trouble , ");
                EquipRepairSql.AppendLine("Date =        @Date , ");
                EquipRepairSql.AppendLine("TroubleLevel =        @TroubleLevel , ");
                EquipRepairSql.AppendLine("HopeRepairDate =        @HopeRepairDate , ");
                EquipRepairSql.AppendLine("RepairType =        @RepairType , ");
                EquipRepairSql.AppendLine("ToRepairDate =        @ToRepairDate , ");
                EquipRepairSql.AppendLine("PlanDate =        @PlanDate , ");
                EquipRepairSql.AppendLine("SolveDate =        @SolveDate , ");
                EquipRepairSql.AppendLine("RepairUser =        @RepairUser , ");
                EquipRepairSql.AppendLine("RepairHours =        @RepairHours , ");
                EquipRepairSql.AppendLine("TroubleStatus =        @TroubleStatus , ");
                EquipRepairSql.AppendLine("PlanCost =        @PlanCost, ");
                //EquipRepairSql.AppendLine("RepairParts =        @RepairParts, ");
               // EquipRepairSql.AppendLine("CompleteDate =        @CompleteDate, ");
               // EquipRepairSql.AppendLine("FactCost =        @FactCost, ");
               // EquipRepairSql.AppendLine("PartsCheck =        @PartsCheck, ");
                EquipRepairSql.AppendLine("ModifiedDate =        @ModifiedDate, ");
                EquipRepairSql.AppendLine("ModifiedUserID =        @ModifiedUserID, ");
                EquipRepairSql.AppendLine("Remark =        @Remark ");
                EquipRepairSql.AppendLine(" WHERE ");
                EquipRepairSql.AppendLine("RecordNo = @RecordNo ");
                #endregion
                #region 更新设备申请信息参数设置
                SqlParameter[] param;
                param = new SqlParameter[20];
                param[0] = SqlHelper.GetParameter("@EquipmentNo", EquipRepairModel.EquipmentNo);
                param[1] = SqlHelper.GetParameter("@CompanyCD", EquipRepairModel.CompanyCD);
                param[2] = SqlHelper.GetParameter("@ReportUserID", EquipRepairModel.ReportUserID);
                param[3] = SqlHelper.GetParameter("@DeptID", EquipRepairModel.DeptID);
                param[4] = SqlHelper.GetParameter("@Trouble", EquipRepairModel.Trouble);
                param[5] = SqlHelper.GetParameter("@Date", EquipRepairModel.Date);
                param[6] = SqlHelper.GetParameter("@TroubleLevel", EquipRepairModel.TroubleLevel);
                param[7] = SqlHelper.GetParameter("@HopeRepairDate", EquipRepairModel.HopeRepairDate);
                param[8] = SqlHelper.GetParameter("@RepairType", EquipRepairModel.RepairType);
                param[9] = SqlHelper.GetParameter("@ToRepairDate", EquipRepairModel.ToRepairDate);
                param[10] = SqlHelper.GetParameter("@PlanDate", EquipRepairModel.PlanDate);
                param[11] = SqlHelper.GetParameter("@SolveDate", EquipRepairModel.SolveDate);
                param[12] = SqlHelper.GetParameter("@RepairUser", EquipRepairModel.RepairUser);
                param[13] = SqlHelper.GetParameter("@RepairHours", EquipRepairModel.RepairHours);
                param[14] = SqlHelper.GetParameter("@TroubleStatus", EquipRepairModel.TroubleStatus);
                param[15] = SqlHelper.GetParameter("@PlanCost", EquipRepairModel.PlanCost);
               // param[16] = SqlHelper.GetParameter("@RepairParts", EquipRepairModel.RepairParts);
               // param[17] = SqlHelper.GetParameter("@CompleteDate", EquipRepairModel.CompleteDate);
               // param[18] = SqlHelper.GetParameter("@FactCost", EquipRepairModel.FactCost);
              //  param[19] = SqlHelper.GetParameter("@PartsCheck", EquipRepairModel.PartsCheck);
                param[16] = SqlHelper.GetParameter("@ModifiedDate", EquipRepairModel.ModifiedDate);
                param[17] = SqlHelper.GetParameter("@ModifiedUserID", EquipRepairModel.ModifiedUserID);
                param[18] = SqlHelper.GetParameter("@Remark", EquipRepairModel.Remark);
                param[19] = SqlHelper.GetParameter("@RecordNo", EquipRepairModel.RecordNo);
                #endregion
                SqlHelper.ExecuteTransSql(EquipRepairSql.ToString(), param);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch 
            {
                return false;
            }
        }
        #endregion
       #region 删除设备领用信息
        /// <summary>
        /// 删除设备领用信息
        /// </summary>
        /// <param name="EquipReceiveNos">设备维修IDS</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool DelEquipRepairInfo(string EquipRepairNos)
        {
            string allEquipID = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string[] Delsql = new string[1];
            try
            {
                string[] EquipIDS = null;
                EquipIDS = EquipRepairNos.Split(',');

                for (int i = 0; i < EquipIDS.Length; i++)
                {
                    EquipIDS[i] = "'" + EquipIDS[i] + "'";
                    sb.Append(EquipIDS[i]);
                }

                allEquipID = sb.ToString().Replace("''", "','");
                Delsql[0] = "DELETE FROM officedba.EquipmentRepair WHERE RecordNo IN (" + allEquipID + ")";
                SqlHelper.ExecuteTransForListWithSQL(Delsql);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch 
            {
                return false;
            }
        }
        #endregion
       #region 查询设备维修列表
        /// <summary>
        /// 查询设备维修列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetEquipmentRepairInfoBycondition(EquipmentRepairModel EquipmentRepairM, string EquipName, string EquipIndex, string FlowStatus,int pageIndex,int pageCount,string ord, ref int TotalCount)
        {
            string sql = "SELECT distinct b.ID,isnull(a.EquipmentIndex,'暂无') AS EquipmentIndex,"
                           +"isnull(a.EquipmentName,'暂无') AS EquipmentName,"
	                       +"isnull(a.EquipmentNo,'暂无') as EquipmentNo, isnull(b.RecordNo,0) AS RecordNo,"
                           +"isnull(b.CompanyCD,'暂无') AS CompanyCD, isnull(b.ReportUserID,0) AS ReportUserID,"
                           + "isnull(b.DeptID,0) AS DeptID,"
                           + "isnull(b.Trouble,'暂无') AS Trouble, isnull(b.Date,'1900-1-1') AS Date, case b.TroubleLevel when 1 then '轻微' when 2 then '一般' when 3 then '严重' else '暂无' end TroubleLevel,"
                           +"isnull(b.HopeRepairDate,'1900-1-1') AS HopeRepairDate, isnull(b.RepairType,'-1') AS RepairType,"
                           +"isnull(b.ToRepairDate,'1900-1-1') AS ToRepairDate, isnull(b.PlanDate,'1900-1-1') AS PlanDate,"
                           +"isnull(b.SolveDate,'1900-1-1') AS SolveDate, isnull(b.RepairUser,'暂无') AS RepairUser,"
                           +"isnull(b.RepairHours,'0.00') AS RepairHours, isnull(b.RepairParts,'暂无') AS RepairParts,"
                           + "isnull(b.PartsCheck,'-1') AS PartsCheck, isnull(b.Remark,'暂无') AS Remark,case a.Status when 0 then '空闲' when 1 then '使用' when 3 then '维修' when 5 then '报废' end TroubleStatus,"
                           +"isnull(b.PlanCost,'0.00') AS PlanCost, isnull(b.FactCost,'0.00') AS FactCost, isnull(b.CompleteDate,'1900-1-1') AS CompleteDate,"
                           +"isnull(b.TroubleDescription,'暂无') AS TroubleDescription, isnull(b.ModifiedUserID,'0') AS ModifiedUserID,"
                           +"isnull(b.ModifiedDate,'1900-1-1') AS ModifiedDate,"
                           + "isnull(c.EmployeeName,'')EmployeeName,isnull(d.DeptName,'')DeptName,"
                           + "CASE isnull(e.FlowStatus,0) WHEN 0 THEN '' "
                           + "WHEN 1 THEN '待审批' "
                           + "WHEN 2 THEN '审批中' "
                           + "WHEN 3 THEN '审批通过' "
                           + "WHEN 4 THEN '审批不通过' "
                           + "WHEN 5 THEN '撤销审批' "
                           + "END FlowStatus "
                           + " FROM  officedba.EquipmentRepair  b  LEFT OUTER JOIN "
                           + " officedba.EquipmentInfo a "
                           + " ON a.EquipmentNo = b.EquipmentNo AND a.CompanyCD=b.CompanyCD "
                           + "LEFT OUTER JOIN "
                           + "officedba.EmployeeInfo c "
                           + "ON b.ReportUserID=c.ID  AND b.CompanyCD=c.CompanyCD "
                           + "LEFT OUTER JOIN "
                           + "officedba.DeptInfo d "
                           + "ON b.DeptID=d.ID and b.CompanyCD=d.CompanyCD "
                           +" LEFT OUTER JOIN"
                           + "(select max(m.id)ID,m.BillID,m.BillNo,m.CompanyCD from officedba.FlowInstance m,"
						   +"officedba.EquipmentRepair n  "
                           + "where m.BillTypeFlag='" + ConstUtil.CODING_RULE_EQUIPMENT + "' AND m.BillTypeCode='4' and  m.BillID=n.ID group by m.BillID,m.BillNo,m.CompanyCD) f  "
                           + "on b.RecordNo=f.BillNo and b.CompanyCD=f.CompanyCD "
                           +"LEFT OUTER JOIN officedba.FlowInstance e "
                           + "ON f.ID=e.ID  and f.CompanyCD=e.CompanyCD  "
                           + " WHERE b.CompanyCD='" + EquipmentRepairM .CompanyCD+ "' ";
            if (EquipName != "")
                sql += " and a.EquipmentName like '%" + EquipName + "%'";
            if (EquipIndex != "")
                sql += " and a.EquipmentIndex like '%" + EquipIndex + "%'";
            if (EquipmentRepairM.RecordNo != "")
                sql += " and b.RecordNo like '%" + EquipmentRepairM.RecordNo + "%'";
            if (EquipmentRepairM.EquipmentNo != "")
                sql += " and b.EquipmentNo like '%" + EquipmentRepairM.EquipmentNo + "%'";
            if (EquipmentRepairM.TroubleLevel != "")
                sql += " and b.TroubleLevel='" + EquipmentRepairM.TroubleLevel + "'";
            if (EquipmentRepairM.Date.ToString() != "" && EquipmentRepairM.Date.ToString() != "1900-01-01 00:00:00" && EquipmentRepairM.Date.ToString() != "1900-1-1 0:00:00")
                sql += " and b.Date>'" + EquipmentRepairM.Date + "'";
            if (EquipmentRepairM.CompleteDate.ToString() != "" && EquipmentRepairM.CompleteDate.ToString() != "1900-01-01 00:00:00" && EquipmentRepairM.CompleteDate.ToString() != "1900-1-1 0:00:00")
                sql += " and b.CompleteDate>'" + EquipmentRepairM.CompleteDate + "'";
            if (EquipmentRepairM.ReportUserID.ToString() != "" && EquipmentRepairM.ReportUserID != 0)
                sql += " and b.ReportUserID=" + EquipmentRepairM.ReportUserID + "";
            if (EquipmentRepairM.TroubleStatus != "")
                sql += " and a.Status='" + EquipmentRepairM.TroubleStatus + "'";
            if (FlowStatus != "" && FlowStatus != "0")
                sql += " and e.FlowStatus = '" + FlowStatus + "'";
            if (FlowStatus == "0")
                sql += " and e.FlowStatus IS NULL";
            //return SqlHelper.ExecuteSql(sql);
            return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);

        }
        #endregion
       #region 查询某设备是否可以维修
        /// <summary>
        /// 查询某设备是否可以维修
        /// </summary>
        /// <param name="RecordNo">设备单据号</param>
        /// <returns>DataTable</returns>
        public static DataTable IfRepair(string EquipmentNo, string RecordNo, string CompanyCD)
        {
            string sql = "select * from officedba.EquipmentInfo A "
                        + "LEFT OUTER JOIN officedba.EquipmentRepair B "
                        + "ON A.EquipmentNo=B.EquipmentNo AND A.CompanyCD=B.CompanyCD "
                        + "LEFT OUTER JOIN officedba.FlowInstance C "
                        + "ON B.RecordNo=C.BillNo "
                        + "where A.EquipmentNo='" + EquipmentNo + "'  AND (A.Status='0' OR A.Status='1') AND "
                        + "B.RecordNo='" + RecordNo + "' and A.CompanyCD='" + CompanyCD + "'  "
                        + "AND C.BillTypeFlag='" + ConstUtil.CODING_RULE_EQUIPMENT + "' "
                        + "AND C.BillTypeCode='" + ConstUtil.CODING_RULE_EQUIPMENT_REPAIR + "'  and  C.FlowStatus='3' ";
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion
       #region 维修设备动作
        /// <summary>
        /// 维修设备动作
        /// </summary>
        /// <param name="EquipReceiveNos">设备维修IDS</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool UpdateRepair(string EquipmentNo,string recordnos, string CompanyCD)
        {
            try
            {
                string CurrentStatus = GetCurrentStatusByEquipmentNo(EquipmentNo, CompanyCD);
                if (CurrentStatus == "-1")
                    return false;
                else
                {
                    string[] UpdateSql = new string[2];
                    UpdateSql[0] = "UPDATE officedba.EquipmentRepair SET TroubleStatus='" + CurrentStatus + "' WHERE RecordNo='" + recordnos + "' AND CompanyCD='" + CompanyCD + "'";
                    UpdateSql[1] = "UPDATE officedba.EquipmentInfo SET Status=3 WHERE EquipmentNo='" + EquipmentNo + "' and CompanyCD='" + CompanyCD + "'";
                    SqlHelper.ExecuteTransForListWithSQL(UpdateSql);
                    return SqlHelper.Result.OprateCount > 0 ? true : false;
                }
            }
            catch 
            {
                return false;
            }
        }
        #endregion
       #region 根据设备编号获取当前状态
        public static string GetCurrentStatusByEquipmentNo(string EquipmentNo, string CompanyCD) 
        {
            string sql = "select * from officedba.EquipmentInfo A "
                        + "where A.EquipmentNo='" + EquipmentNo + "'"
                        + " AND A.CompanyCD='" + CompanyCD + "'  ";
            DataTable dt=SqlHelper.ExecuteSql(sql);
            return dt!=null?dt.Rows[0]["Status"].ToString():"-1";
        }
        #endregion
       #region 查询某设备是否可以完成维修操作
        /// <summary>
        /// 查询某设备是否可以完成维修操作
        /// </summary>
        /// <param name="RecordNo">设备单据号</param>
        /// <returns>DataTable</returns>
        public static DataTable IfCompleteRepair(string EquipmentNo, string RecordNo, string CompanyCD)
        {
            //string sql = "SELECT * FROM officedba.EquipmentRepair WHERE RecordNo='" + RecordNo + "' and CompanyCD='" + CompanyCD + "' and TroubleStatus=1";
            string sql = "select * from officedba.EquipmentInfo A "
                        + "LEFT OUTER JOIN officedba.EquipmentRepair B "
                        + "ON A.EquipmentNo=B.EquipmentNo AND A.CompanyCD=B.CompanyCD "
                        + "LEFT OUTER JOIN officedba.FlowInstance C "
                        + "ON B.RecordNo=C.BillNo "
                        + "where A.EquipmentNo='" + EquipmentNo + "'  AND A.Status='3' AND "
                        + "B.RecordNo='" + RecordNo + "' and A.CompanyCD='" + CompanyCD + "'  "
                        + "AND C.BillTypeFlag='" + ConstUtil.CODING_RULE_EQUIPMENT + "' "
                        + "AND C.BillTypeCode='" + ConstUtil.CODING_RULE_EQUIPMENT_REPAIR + "'  and  C.FlowStatus='3' ";
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion
       #region 根据领用单据号获取设备维修信息以查看或修改
        /// <summary>
        /// 根据领用单据号获取设备维修信息以查看或修改
        /// </summary>
        /// <param name="EquipmnetNO">设备单据号</param>
        /// <returns>DataTable</returns>
        public static DataTable GetEquipmentReceiveInfoByRecordNo(string RecordNo, string CompanyID)
        {
            string sql = "SELECT a.*,u.EmployeeName as CreateUserName,c.EmployeeName,d.DeptName,"
                            + "CASE isnull(h.FlowStatus,0) WHEN 0 THEN '' "
                            + "WHEN 1 THEN '待审批' "
                            + "WHEN 2 THEN '审批中' "
                            + "WHEN 3 THEN '审批通过'"
                            + "WHEN 4 THEN '审批不通过' "
                            + " WHEN 5 THEN '撤销审批' "
                            + "END FlowStatus,"
                            + "case x.Status when 0 then '空闲' when 1 then '使用' when 3 then '维修' "
                            + "when 5 then '报废' end  Status "
                            +"FROM officedba.EquipmentRepair a "
                            +"LEFT OUTER JOIN officedba.EmployeeInfo c "
                            +"ON a.ReportUserID=c.ID "
                            + "LEFT OUTER JOIN officedba.EmployeeInfo u "
                            + "ON a.CreateUser=u.ID "
                            +"LEFT OUTER JOIN "
                            + "officedba.DeptInfo d ON a.DeptID=d.ID "
                            + "LEFT OUTER JOIN "
                            + "(select max(m.id)ID,m.BillID,m.BillNo,m.CompanyCD from officedba.FlowInstance m,officedba.EquipmentRepair n  "
                            + "where m.BillTypeFlag='" + ConstUtil.CODING_RULE_EQUIPMENT + "' AND "
                            + "m.BillTypeCode='" + ConstUtil.CODING_RULE_EQUIPMENT_REPAIR + "' and  m.BillID=n.ID and Billid=" + RecordNo + " group by m.BillID,m.BillNo,m.CompanyCD) g "
                            + "on a.RecordNo=g.BillNo and a.CompanyCD=g.CompanyCD "
                            + "LEFT OUTER JOIN officedba.FlowInstance h "
                            + "ON g.ID=h.ID and g.CompanyCD=h.CompanyCD "
                            + "LEFT OUTER JOIN officedba.EquipmentInfo x "
                            + "on a.EquipmentNo=x.EquipmentNo and a.CompanyCD=x.CompanyCD "
                            +" WHERE a.ID='" + RecordNo + "' AND a.CompanyCD='"+CompanyID+"'";
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion
       #region 设备维修更新操作
        /// <summary>
        /// 设备维修修改操作
        /// </summary>
        /// <param name="EquipRepairModel">设备维修修改操作</param>
        /// <returns>修改是否成功 false:失败，true:成功</returns>
        public static bool UpdateEquipmentCompleteRepairInfo(EquipmentRepairModel EquipRepairModel)
        {
            try
            {
                #region 更新设备申请信息SQL拼写
                StringBuilder EquipRepairSql = new StringBuilder();
                EquipRepairSql.AppendLine("UPDATE officedba.EquipmentRepair SET  ");
                //EquipRepairSql.AppendLine("TroubleStatus =        @TroubleStatus , ");
                EquipRepairSql.AppendLine("RepairParts =        @RepairParts, ");
                EquipRepairSql.AppendLine("CompleteDate =        @CompleteDate, ");
                EquipRepairSql.AppendLine("FactCost =        @FactCost, ");
                EquipRepairSql.AppendLine("PartsCheck =        @PartsCheck, ");
                EquipRepairSql.AppendLine("ModifiedDate =        @ModifiedDate, ");
                EquipRepairSql.AppendLine("ModifiedUserID =        @ModifiedUserID ");
                EquipRepairSql.AppendLine(" WHERE ");
                EquipRepairSql.AppendLine("RecordNo = @RecordNo AND  CompanyCD =@CompanyCD ");
                #endregion
                #region 更新设备申请信息参数设置
                SqlParameter[] param;
                param = new SqlParameter[8];
                //param[0] = SqlHelper.GetParameter("@TroubleStatus", EquipRepairModel.TroubleStatus);
                param[0] = SqlHelper.GetParameter("@RepairParts", EquipRepairModel.RepairParts);
                param[1] = SqlHelper.GetParameter("@CompleteDate", EquipRepairModel.CompleteDate);
                param[2] = SqlHelper.GetParameter("@FactCost", EquipRepairModel.FactCost);
                param[3] = SqlHelper.GetParameter("@PartsCheck", EquipRepairModel.PartsCheck);
                param[4] = SqlHelper.GetParameter("@ModifiedDate", EquipRepairModel.ModifiedDate);
                param[5] = SqlHelper.GetParameter("@ModifiedUserID", EquipRepairModel.ModifiedUserID);
                param[6] = SqlHelper.GetParameter("@RecordNo", EquipRepairModel.RecordNo);
                param[7] = SqlHelper.GetParameter("@CompanyCD", EquipRepairModel.CompanyCD);
                string updatesql= "UPDATE officedba.EquipmentInfo SET Status='" + EquipRepairModel.TroubleStatus + "' WHERE EquipmentNo='" + EquipRepairModel.EquipmentNo + "' and CompanyCD='" + EquipRepairModel.CompanyCD + "'";
                SqlHelper.ExecuteTransSql(updatesql);
                #endregion
                SqlHelper.ExecuteTransSql(EquipRepairSql.ToString(), param);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch 
            {
                return false;
            }
        }
        #endregion
       #region 设备维修单据打印
        /// <summary>
        /// 设备维修单据打印
        /// </summary>
        /// <param name="EquipmnetNO">设备单据号</param>
        /// <returns>DataTable</returns>
        public static DataTable GetEquipmentRepairByRecordNoForPrint(string RecordNo, string CompanyID)
        {
            string sql = "SELECT a.*,Case a.TroubleLevel "
                            + "WHEN '1' THEN '轻微' WHEN '2' THEN '一般' WHEN '3' THEN '严重' END TroubleStatus1,"
                            + "CASE a.RepairType "
                            + "WHEN '1' THEN '自修' WHEN '2' THEN '送修' END RepairType1,"
                            + "CASE a.PartsCheck "
                            + "WHEN '1' THEN '验收通过' WHEN '2' THEN '验收不通过' END PartsCheck1,"
                            + "c.EmployeeName,d.DeptName,"
                            + "case x.Status when 0 then '空闲' when 1 then '使用' when 3 then '维修' "
                            + "when 5 then '报废' end  Status "
                            + "FROM officedba.EquipmentRepair a " 
                            + "LEFT OUTER JOIN officedba.EmployeeInfo c "
                            + "ON a.CompanyCD='" + CompanyID + "' AND  a.ReportUserID=c.ID "
                            + "LEFT OUTER JOIN "
                            + "officedba.DeptInfo d ON a.CompanyCD='" + CompanyID + "' AND a.DeptID=d.ID "
                            + "LEFT OUTER JOIN officedba.EquipmentInfo x "
                            + "on a.CompanyCD='" + CompanyID + "' AND a.EquipmentNo=x.EquipmentNo and a.CompanyCD=x.CompanyCD "
                            + " WHERE a.RecordNo='" + RecordNo + "' AND a.CompanyCD='" + CompanyID + "'";
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion
        #region 查询设备维修列表
        /// <summary>
        /// 查询设备维修列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetEquipmentRepairInfoForExp(EquipmentRepairModel EquipmentRepairM, string EquipName, string EquipIndex, string FlowStatus,string ord)
        {
            string sql = "SELECT distinct b.ID,isnull(a.EquipmentIndex,'暂无') AS EquipmentIndex,"
                           + "isnull(a.EquipmentName,'暂无') AS EquipmentName,"
                           + "isnull(a.EquipmentNo,'暂无') as EquipmentNo, isnull(b.RecordNo,0) AS RecordNo,"
                           + "isnull(b.CompanyCD,'暂无') AS CompanyCD, isnull(b.ReportUserID,0) AS ReportUserID,"
                           + "isnull(b.DeptID,0) AS DeptID,"
                           + "isnull(b.Trouble,'暂无') AS Trouble, isnull(b.Date,'1900-1-1') AS Date, case b.TroubleLevel when 1 then '轻微' when 2 then '一般' when 3 then '严重' else '暂无' end TroubleLevel,"
                           + "isnull(b.HopeRepairDate,'1900-1-1') AS HopeRepairDate, isnull(b.RepairType,'-1') AS RepairType,"
                           + "isnull(b.ToRepairDate,'1900-1-1') AS ToRepairDate, isnull(b.PlanDate,'1900-1-1') AS PlanDate,"
                           + "isnull(b.SolveDate,'1900-1-1') AS SolveDate, isnull(b.RepairUser,'暂无') AS RepairUser,"
                           + "isnull(b.RepairHours,'0.00') AS RepairHours, isnull(b.RepairParts,'暂无') AS RepairParts,"
                           + "isnull(b.PartsCheck,'-1') AS PartsCheck, isnull(b.Remark,'暂无') AS Remark,case a.Status when 0 then '空闲' when 1 then '使用' when 3 then '维修' when 5 then '报废' end TroubleStatus,"
                           + "isnull(b.PlanCost,'0.00') AS PlanCost, isnull(b.FactCost,'0.00') AS FactCost, isnull(b.CompleteDate,'1900-1-1') AS CompleteDate,"
                           + "isnull(b.TroubleDescription,'暂无') AS TroubleDescription, isnull(b.ModifiedUserID,'0') AS ModifiedUserID,"
                           + "isnull(b.ModifiedDate,'1900-1-1') AS ModifiedDate,"
                           + "isnull(c.EmployeeName,'')EmployeeName,isnull(d.DeptName,'')DeptName,"
                           + "CASE isnull(e.FlowStatus,0) WHEN 0 THEN '' "
                           + "WHEN 1 THEN '待审批' "
                           + "WHEN 2 THEN '审批中' "
                           + "WHEN 3 THEN '审批通过' "
                           + "WHEN 4 THEN '审批不通过' "
                           + "WHEN 5 THEN '撤销审批' "
                           + "END FlowStatus "
                           + " FROM  officedba.EquipmentRepair  b  LEFT OUTER JOIN "
                           + " officedba.EquipmentInfo a "
                           + " ON a.EquipmentNo = b.EquipmentNo AND a.CompanyCD=b.CompanyCD "
                           + "LEFT OUTER JOIN "
                           + "officedba.EmployeeInfo c "
                           + "ON b.ReportUserID=c.ID  AND b.CompanyCD=c.CompanyCD "
                           + "LEFT OUTER JOIN "
                           + "officedba.DeptInfo d "
                           + "ON b.DeptID=d.ID and b.CompanyCD=d.CompanyCD "
                           + " LEFT OUTER JOIN"
                           + "(select max(m.id)ID,m.BillID,m.BillNo,m.CompanyCD from officedba.FlowInstance m,"
                           + "officedba.EquipmentRepair n  "
                           + "where m.BillTypeFlag='" + ConstUtil.CODING_RULE_EQUIPMENT + "' AND m.BillTypeCode='4' and  m.BillID=n.ID group by m.BillID,m.BillNo,m.CompanyCD) f  "
                           + "on b.RecordNo=f.BillNo and b.CompanyCD=f.CompanyCD "
                           + "LEFT OUTER JOIN officedba.FlowInstance e "
                           + "ON f.ID=e.ID  and f.CompanyCD=e.CompanyCD  "
                           + " WHERE b.CompanyCD='" + EquipmentRepairM.CompanyCD + "' ";
            if (EquipName != "")
                sql += " and a.EquipmentName like '%" + EquipName + "%'";
            if (EquipIndex != "")
                sql += " and a.EquipmentIndex like '%" + EquipIndex + "%'";
            if (EquipmentRepairM.RecordNo != "")
                sql += " and b.RecordNo like '%" + EquipmentRepairM.RecordNo + "%'";
            if (EquipmentRepairM.EquipmentNo != "")
                sql += " and b.EquipmentNo like '%" + EquipmentRepairM.EquipmentNo + "%'";
            if (EquipmentRepairM.TroubleLevel != "")
                sql += " and b.TroubleLevel='" + EquipmentRepairM.TroubleLevel + "'";
            if (EquipmentRepairM.Date.ToString() != "" && EquipmentRepairM.Date.ToString() != "1900-1-1 0:00:00")
                sql += " and b.Date>'" + EquipmentRepairM.Date + "'";
            if (EquipmentRepairM.CompleteDate.ToString() != "" && EquipmentRepairM.CompleteDate.ToString() != "1900-1-1 0:00:00")
                sql += " and b.CompleteDate>'" + EquipmentRepairM.CompleteDate + "'";
            if (EquipmentRepairM.ReportUserID.ToString() != "" && EquipmentRepairM.ReportUserID != 0)
                sql += " and b.ReportUserID=" + EquipmentRepairM.ReportUserID + "";
            if (EquipmentRepairM.TroubleStatus != "")
                sql += " and a.Status='" + EquipmentRepairM.TroubleStatus + "'";
            if (FlowStatus != "" && FlowStatus != "0")
                sql += " and e.FlowStatus = '" + FlowStatus + "'";
            if (FlowStatus == "0")
                sql += " and e.FlowStatus IS NULL ";
            sql = sql + ord;
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion
    }
}

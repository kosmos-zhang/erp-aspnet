/**********************************************
 * 类作用：   设备报废数据库层处理
 * 建立人：   lysong
 * 建立时间： 2009/03/28
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
   public class EquipmentUselessDBHelper
    {
        #region 设备报废添加操作
        /// <summary>
        /// 设备报废添加操作
        /// </summary>
        /// <param name="EquipRepairModel">设备报废信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool AddEquipmentUselessInfo(EquipmentUselessModel EquipUselessModel, out int RetValID, int CreateUserID, string CreateDate)
        {
            try
            {
                #region 添加设备报废SQL拼写
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO officedba.EquipmentUseless");
                sql.AppendLine("		(RecordNo      ");
                sql.AppendLine("		,EquipmentNo        ");
                sql.AppendLine("		,CompanyCD        ");
                sql.AppendLine("		,ApplyUserID        ");
                sql.AppendLine("		,DeptID        ");
                sql.AppendLine("		,ApplyDate        ");
                sql.AppendLine("		,UselessDate        ");
                sql.AppendLine("		,UsedDescription        ");
                sql.AppendLine("		,UselessReason        ");
                sql.AppendLine("		,Cost        ");
                sql.AppendLine("		,UselessStatus        ");
                sql.AppendLine("		,Remark        ");
                sql.AppendLine("		,CreateUser        ");
                sql.AppendLine("		,CreateDate        ");
                sql.AppendLine("		,ModifiedDate        ");
                sql.AppendLine("		,ModifiedUserID)        ");
                sql.AppendLine("VALUES                  ");
                sql.AppendLine("		(@RecordNo   ");
                sql.AppendLine("		,@EquipmentNo       ");
                sql.AppendLine("		,@CompanyCD       ");
                sql.AppendLine("		,@ApplyUserID       ");
                sql.AppendLine("		,@DeptID       ");
                sql.AppendLine("		,@ApplyDate       ");
                sql.AppendLine("		,@UselessDate       ");
                sql.AppendLine("		,@UsedDescription       ");
                sql.AppendLine("		,@UselessReason       ");
                sql.AppendLine("		,@Cost       ");
                sql.AppendLine("		,@UselessStatus       ");
                sql.AppendLine("		,@Remark       ");
                sql.AppendLine("		,@CreateUser       ");
                sql.AppendLine("		,@CreateDate       ");
                sql.AppendLine("		,@ModifiedDate       ");
                sql.AppendLine("		,@ModifiedUserID)       ");
                sql.AppendLine("set @ID=@@IDENTITY");
                #endregion
                #region 添加设备报废参数设置
                SqlParameter[] param;
                param = new SqlParameter[17];
                param[0] = SqlHelper.GetParameter("@RecordNo", EquipUselessModel.RecordNo);
                param[1] = SqlHelper.GetParameter("@EquipmentNo", EquipUselessModel.EquipmentNo);
                param[2] = SqlHelper.GetParameter("@CompanyCD", EquipUselessModel.CompanyCD);
                param[3] = SqlHelper.GetParameter("@ApplyUserID", EquipUselessModel.ApplyUserID);
                param[4] = SqlHelper.GetParameter("@DeptID", EquipUselessModel.DeptID);
                param[5] = SqlHelper.GetParameter("@ApplyDate", EquipUselessModel.ApplyDate);
                param[6] = SqlHelper.GetParameter("@UselessDate", EquipUselessModel.UselessDate);
                param[7] = SqlHelper.GetParameter("@UsedDescription", EquipUselessModel.UsedDescription);
                param[8] = SqlHelper.GetParameter("@UselessReason", EquipUselessModel.UselessReason);
                param[9] = SqlHelper.GetParameter("@Cost", EquipUselessModel.Cost);
                param[10] = SqlHelper.GetParameter("@UselessStatus", EquipUselessModel.UselessStatus);
                param[11] = SqlHelper.GetParameter("@Remark", EquipUselessModel.Remark);
                param[12] = SqlHelper.GetParameter("@CreateUser", CreateUserID);
                param[13] = SqlHelper.GetParameter("@CreateDate", Convert.ToDateTime(CreateDate));
                param[14] = SqlHelper.GetParameter("@ModifiedDate", EquipUselessModel.ModifiedDate);
                param[15] = SqlHelper.GetParameter("@ModifiedUserID", EquipUselessModel.ModifiedUserID);
                param[16] = SqlHelper.GetOutputParameter("@ID", SqlDbType.Int);
                #endregion
                SqlHelper.ExecuteTransSql(sql.ToString(), param);
                RetValID = Convert.ToInt32(param[16].Value.ToString());
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch 
            {
                RetValID = 0;
                return false;
            }
        }
        #endregion
        #region 设备报废修改操作
        /// <summary>
        /// 设备报废修改操作
        /// </summary>
        /// <param name="EquipRepairModel">设备报废修改操作</param>
        /// <returns>修改是否成功 false:失败，true:成功</returns>
        public static bool UpdateEquipmentUserlessInfo(EquipmentUselessModel EquipUselessModel)
        {
            try
            {
                #region 更新设备报废SQL拼写
                StringBuilder EquipUselessSql = new StringBuilder();
                EquipUselessSql.AppendLine("UPDATE officedba.EquipmentUseless SET ");
                EquipUselessSql.AppendLine("EquipmentNo=           @EquipmentNo, ");
                EquipUselessSql.AppendLine("CompanyCD =        @CompanyCD , ");
                EquipUselessSql.AppendLine("ApplyUserID =        @ApplyUserID , ");
                EquipUselessSql.AppendLine("DeptID =        @DeptID , ");
                EquipUselessSql.AppendLine("ApplyDate =        @ApplyDate , ");
                EquipUselessSql.AppendLine("UselessDate =        @UselessDate , ");
                EquipUselessSql.AppendLine("UsedDescription =        @UsedDescription , ");
                EquipUselessSql.AppendLine("UselessReason =        @UselessReason , ");
                EquipUselessSql.AppendLine("Cost =        @Cost , ");
                EquipUselessSql.AppendLine("UselessStatus =        @UselessStatus , ");
                EquipUselessSql.AppendLine("Remark =        @Remark , ");
                EquipUselessSql.AppendLine("ModifiedDate =        @ModifiedDate , ");
                EquipUselessSql.AppendLine("ModifiedUserID =        @ModifiedUserID ");
                EquipUselessSql.AppendLine(" WHERE ");
                EquipUselessSql.AppendLine("RecordNo = @RecordNo ");
                #endregion
                #region 更新设备申请信息参数设置
                SqlParameter[] param;
                param = new SqlParameter[14];
                param[0] = SqlHelper.GetParameter("@EquipmentNo", EquipUselessModel.EquipmentNo);
                param[1] = SqlHelper.GetParameter("@CompanyCD", EquipUselessModel.CompanyCD);
                param[2] = SqlHelper.GetParameter("@ApplyUserID", EquipUselessModel.ApplyUserID);
                param[3] = SqlHelper.GetParameter("@DeptID", EquipUselessModel.DeptID);
                param[4] = SqlHelper.GetParameter("@ApplyDate", EquipUselessModel.ApplyDate);
                param[5] = SqlHelper.GetParameter("@UselessDate", EquipUselessModel.UselessDate);
                param[6] = SqlHelper.GetParameter("@UsedDescription", EquipUselessModel.UsedDescription);
                param[7] = SqlHelper.GetParameter("@UselessReason", EquipUselessModel.UselessReason);
                param[8] = SqlHelper.GetParameter("@Cost", EquipUselessModel.Cost);
                param[9] = SqlHelper.GetParameter("@UselessStatus", EquipUselessModel.UselessStatus);
                param[10] = SqlHelper.GetParameter("@Remark", EquipUselessModel.Remark);
                param[11] = SqlHelper.GetParameter("@ModifiedDate", EquipUselessModel.ModifiedDate);
                param[12] = SqlHelper.GetParameter("@ModifiedUserID", EquipUselessModel.ModifiedUserID);
                param[13] = SqlHelper.GetParameter("@RecordNo", EquipUselessModel.RecordNo);
                #endregion
                SqlHelper.ExecuteTransSql(EquipUselessSql.ToString(), param);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch 
            {
                return false;
            }
        }
        #endregion
        #region 查询设备报废列表
        /// <summary>
        /// 查询设备报废列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetEquipmentUselessInfoBycondition(EquipmentUselessModel EquipmentUselessM, string EquipName, string EquipIndex,string FlowStatus, string CompanyID,int pageIndex,int pageCount,string ord, ref int TotalCount)
        {
            string sql = "SELECT distinct a.ID,a.RecordNo, a.CompanyCD, a.EquipmentNo," 
                      +"a.ApplyUserID, a.DeptID, a.ApplyDate, "
                      +"isnull(a.UselessDate,'1900-1-1')UselessDate, isnull(a.UsedDescription,'暂无')UsedDescription, "
				      +"isnull(a.UselessReason,'暂无')UselessReason, "
                      + "isnull(a.Cost,0)Cost, case b.Status when 0 then '空闲' when 1 then '使用' when 3 then '维修' when 5 then '报废' end  UselessStatus, "
				      +"isnull(a.Remark,'暂无')Remark, "
                      +"isnull(a.ModifiedDate,'1900-1-1')ModifiedDate, isnull(a.ModifiedUserID,'-1')ModifiedUserID,"
				      +"isnull(b.EquipmentIndex,'暂无')EquipmentIndex, isnull(b.EquipmentName,'暂无')EquipmentName,"
                      + "isnull(c.EmployeeName,'')EmployeeName,isnull(d.DeptName,'')DeptName, "
                      + "CASE isnull(e.FlowStatus,0) WHEN 0 THEN '' "
                      + "WHEN 1 THEN '待审批' "
                      + "WHEN 2 THEN '审批中' "
                      + "WHEN 3 THEN '审批通过' "
                      + "WHEN 4 THEN '审批不通过' "
                      + "WHEN 5 THEN '撤销审批' "
                      + "END FlowStatus  "
                      +" FROM officedba.EquipmentUseless a LEFT OUTER JOIN "
                      +" officedba.EquipmentInfo b"
                      +" ON a.EquipmentNo = b.EquipmentNo and a.CompanyCD=b.CompanyCD "
                      +"LEFT OUTER JOIN "
                      + "officedba.EmployeeInfo c "
                      + "ON a.ApplyUserID=c.ID AND a.CompanyCD=c.CompanyCD "
                      + "LEFT OUTER JOIN "
                      + "officedba.DeptInfo d "
                      + "ON a.DeptID=d.ID AND a.CompanyCD=d.CompanyCD "
                      + "LEFT OUTER JOIN "
                      + "(select max(m.id)ID,m.BillID,m.BillNo,m.CompanyCD from officedba.FlowInstance m,officedba.EquipmentUseless n  "
                      + "where m.BillTypeFlag='" + ConstUtil.CODING_RULE_EQUIPMENT + "' AND m.BillTypeCode='" + ConstUtil.CODING_RULE_EQUIPMENT_USELESS + "' and  m.BillID=n.ID group by m.BillID,m.BillNo,m.CompanyCD) f  "
                      + "on a.RecordNo=f.BillNo and a.CompanyCD=f.CompanyCD "
                      + "LEFT OUTER JOIN officedba.FlowInstance e "
                      + "ON f.ID=e.ID  AND f.CompanyCD=e.CompanyCD "
                      + " WHERE a.CompanyCD='" + CompanyID + "'";
            if (EquipName != "")
                sql += " and b.EquipmentName like '%" + EquipName + "%'";
            if (EquipIndex != "")
                sql += " and b.EquipmentIndex like '%" + EquipIndex + "%'";
            if (EquipmentUselessM.RecordNo != "")
                sql += " and a.RecordNo like '%" + EquipmentUselessM.RecordNo + "%'";
            if (EquipmentUselessM.EquipmentNo != "")
                sql += " and b.EquipmentNo like '%" + EquipmentUselessM.EquipmentNo + "%'";
            if (EquipmentUselessM.UselessStatus != "")
                sql += " and b.Status='" + EquipmentUselessM.UselessStatus + "'";
            if (EquipmentUselessM.ApplyDate.ToString() != "" && EquipmentUselessM.ApplyDate.ToString() != "1900-1-1 0:00:00" && EquipmentUselessM.ApplyDate.ToString() != "1900-01-01 00:00:00")
                sql += " and a.ApplyDate>='" + EquipmentUselessM.ApplyDate + "'";
            if (EquipmentUselessM.UselessDate.ToString() != "" && EquipmentUselessM.UselessDate.ToString() != "1900-1-1 0:00:00" && EquipmentUselessM.UselessDate.ToString() != "1900-01-01 00:00:00")
                sql += " and a.UselessDate>='" + EquipmentUselessM.UselessDate + "'";
            if (EquipmentUselessM.ApplyUserID.ToString() != "" && EquipmentUselessM.ApplyUserID != 0)
                sql += " and a.ApplyUserID=" + EquipmentUselessM.ApplyUserID + "";
            if (FlowStatus != "" && FlowStatus != "0")
                sql += " and e.FlowStatus = '" + FlowStatus + "'";
            if (FlowStatus == "0")
                sql += " and e.FlowStatus IS NULL";
            return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);

            //return SqlHelper.ExecuteSql(sql);
        }
        #endregion
        #region 根据报废单据号获取设备报废信息以查看或修改
        /// <summary>
        /// 根据报废单据号获取设备报废信息以查看或修改
        /// </summary>
        /// <param name="RecordNo">设备单据号</param>
        /// <returns>DataTable</returns>
        public static DataTable GetEquipmentUselessInfoByRecordNo(string RecordNo, string CompanyID)
        {
            string sql = "SELECT a.*,c.EmployeeName,d.DeptName,"
                            + "CASE isnull(h.FlowStatus,0) WHEN 0 THEN '' "
                            + "WHEN 1 THEN '待审批' "
                            + "WHEN 2 THEN '审批中' "
                            + "WHEN 3 THEN '审批通过'"
                            + "WHEN 4 THEN '审批不通过' "
                            + " WHEN 5 THEN '撤销审批' "
                            + "END FlowStatus,"
                            + "case x.Status when 0 then '空闲' when 1 then '使用' when 3 then '维修' "
                            + "when 5 then '报废' end  Status "
                           +"FROM officedba.EquipmentUseless a "
                           +"LEFT OUTER JOIN officedba.EmployeeInfo  c "
                           +"ON a.ApplyUserID=c.ID "
                           +"LEFT OUTER JOIN "
                           +"officedba.DeptInfo d "
                           +"ON a.DeptID=d.ID "
                            + "LEFT OUTER JOIN "
                            + "(select max(m.id)ID,m.BillID,m.BillNo,m.CompanyCD from officedba.FlowInstance m,officedba.EquipmentUseless n  "
                            + "where m.BillTypeFlag='" + ConstUtil.CODING_RULE_EQUIPMENT + "' AND "
                            + "m.BillTypeCode='" + ConstUtil.CODING_RULE_EQUIPMENT_USELESS + "' and  m.BillID=n.ID and Billid=" + RecordNo + " group by m.BillID,m.BillNo,m.CompanyCD) g "
                            + "on a.RecordNo=g.BillNo and a.CompanyCD=g.CompanyCD "
                            + "LEFT OUTER JOIN officedba.FlowInstance h "
                            + "ON g.ID=h.ID and g.CompanyCD=h.CompanyCD "
                            + "LEFT OUTER JOIN officedba.EquipmentInfo x "
                            + "on a.EquipmentNo=x.EquipmentNo and a.CompanyCD=x.CompanyCD "
                           +"WHERE a.ID=" + RecordNo + " AND a.CompanyCD='"+CompanyID+"'";
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion
        #region 删除设备报废信息
        /// <summary>
        /// 删除设备报废信息
        /// </summary>
        /// <param name="EquipReceiveNos">设备维修IDS</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool DelEquipUselessInfo(string EquipUselessNos)
        {
            string allEquipID = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string[] Delsql = new string[1];
            try
            {
                string[] EquipIDS = null;
                EquipIDS = EquipUselessNos.Split(',');

                for (int i = 0; i < EquipIDS.Length; i++)
                {
                    EquipIDS[i] = "'" + EquipIDS[i] + "'";
                    sb.Append(EquipIDS[i]);
                }

                allEquipID = sb.ToString().Replace("''", "','");
                Delsql[0] = "DELETE FROM officedba.EquipmentUseless WHERE RecordNo IN (" + allEquipID + ")";
                SqlHelper.ExecuteTransForListWithSQL(Delsql);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch 
            {
                return false;
            }
        }
        #endregion
        #region 查询某设备是否可以完成报废操作
        /// <summary>
        /// 查询某设备是否可以完成维修操作
        /// </summary>
        /// <param name="RecordNo">设备单据号</param>
        /// <returns>DataTable</returns>
        public static DataTable IfCompleteUseless(string EquipmentNo, string RecordNo, string CompanyCD)
        {
            //string sql = "SELECT * FROM officedba.EquipmentRepair WHERE RecordNo='" + RecordNo + "' and CompanyCD='" + CompanyCD + "' and TroubleStatus=1";
            string sql = "select * from officedba.EquipmentInfo A "
                        + "LEFT OUTER JOIN officedba.EquipmentUseless B "
                        + "ON A.EquipmentNo=B.EquipmentNo AND A.CompanyCD=B.CompanyCD "
                        + "LEFT OUTER JOIN officedba.FlowInstance C "
                        + "ON B.RecordNo=C.BillNo "
                        + "where A.EquipmentNo=" + EquipmentNo + "  AND A.Status in ('0','1','3') AND "
                        + "B.RecordNo='" + RecordNo + "' and A.CompanyCD='" + CompanyCD + "'  "
                        + "AND C.BillTypeFlag='" + ConstUtil.CODING_RULE_EQUIPMENT + "' "
                        + "AND C.BillTypeCode='" + ConstUtil.CODING_RULE_EQUIPMENT_USELESS + "'  and  C.FlowStatus='3' ";
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion
        #region 完成报废动作
        /// <summary>
        /// 完成报废动作
        /// </summary>
        /// <param name="EquipReceiveNos">设备维修IDS</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool UpdateUseless(string EquipmentNo,string CompanyCD)
        {
            try
            {
                   string sql="UPDATE officedba.EquipmentInfo SET Status=5 WHERE EquipmentNo=" + EquipmentNo + " and CompanyCD='" + CompanyCD + "'";
                    SqlHelper.ExecuteTransSql(sql.ToString());
                    return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch 
            {
                return false;
            }
        }
        #endregion
        #region 报废单据打印
        /// <summary>
        /// 报废单据打印
        /// </summary>
        /// <param name="RecordNo">设备单据号</param>
        /// <returns>DataTable</returns>
        public static DataTable GetEquipmentUselessInfoByRecordNoForPrint(string RecordNo, string CompanyID)
        {
            string sql = "SELECT a.*,c.EmployeeName,d.DeptName,"                      
                          +"case x.Status when 0 then '空闲' when 1 then '使用' when 3 then '维修' "
                          +"when 5 then '报废' end  Status "
                          +"FROM officedba.EquipmentUseless a " 
                          +"LEFT OUTER JOIN officedba.EmployeeInfo  c "
                          +"ON a.ApplyUserID=c.ID "
                          +"LEFT OUTER JOIN "
                          +"officedba.DeptInfo d "
                          + "ON a.CompanyCD='" + CompanyID + "' AND a.DeptID=d.ID AND a.CompanyCD=d.CompanyCD "
                          +"LEFT OUTER JOIN officedba.EquipmentInfo x "
                          + "on a.CompanyCD='" + CompanyID + "' AND a.EquipmentNo=x.EquipmentNo and a.CompanyCD=x.CompanyCD "
                          + "WHERE a.RecordNo='" + RecordNo + "' AND a.CompanyCD='" + CompanyID + "'";
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion
        #region 查询设备报废列表
        /// <summary>
        /// 查询设备报废列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetEquipmentUselessInfoForPrint(EquipmentUselessModel EquipmentUselessM, string EquipName, string EquipIndex, string FlowStatus, string CompanyID, string ord)
        {
            string sql = "SELECT distinct a.ID,a.RecordNo, a.CompanyCD, a.EquipmentNo,"
                      + "a.ApplyUserID, a.DeptID, a.ApplyDate, "
                      + "isnull(a.UselessDate,'1900-1-1')UselessDate, isnull(a.UsedDescription,'暂无')UsedDescription, "
                      + "isnull(a.UselessReason,'暂无')UselessReason, "
                      + "isnull(a.Cost,0)Cost, case b.Status when 0 then '空闲' when 1 then '使用' when 3 then '维修' when 5 then '报废' end  UselessStatus, "
                      + "isnull(a.Remark,'暂无')Remark, "
                      + "isnull(a.ModifiedDate,'1900-1-1')ModifiedDate, isnull(a.ModifiedUserID,'-1')ModifiedUserID,"
                      + "isnull(b.EquipmentIndex,'暂无')EquipmentIndex, isnull(b.EquipmentName,'暂无')EquipmentName,"
                      + "isnull(c.EmployeeName,'')EmployeeName,isnull(d.DeptName,'')DeptName, "
                      + "CASE isnull(e.FlowStatus,0) WHEN 0 THEN '' "
                      + "WHEN 1 THEN '待审批' "
                      + "WHEN 2 THEN '审批中' "
                      + "WHEN 3 THEN '审批通过' "
                      + "WHEN 4 THEN '审批不通过' "
                      + "WHEN 5 THEN '撤销审批' "
                      + "END FlowStatus  "
                      + " FROM officedba.EquipmentUseless a LEFT OUTER JOIN "
                      + " officedba.EquipmentInfo b"
                      + " ON a.EquipmentNo = b.EquipmentNo and a.CompanyCD=b.CompanyCD "
                      + "LEFT OUTER JOIN "
                      + "officedba.EmployeeInfo c "
                      + "ON a.ApplyUserID=c.ID AND a.CompanyCD=c.CompanyCD "
                      + "LEFT OUTER JOIN "
                      + "officedba.DeptInfo d "
                      + "ON a.DeptID=d.ID AND a.CompanyCD=d.CompanyCD "
                      + "LEFT OUTER JOIN "
                      + "(select max(m.id)ID,m.BillID,m.BillNo,m.CompanyCD from officedba.FlowInstance m,officedba.EquipmentUseless n  "
                      + "where m.BillTypeFlag='" + ConstUtil.CODING_RULE_EQUIPMENT + "' AND m.BillTypeCode='" + ConstUtil.CODING_RULE_EQUIPMENT_USELESS + "' and  m.BillID=n.ID group by m.BillID,m.BillNo,m.CompanyCD) f  "
                      + "on a.RecordNo=f.BillNo and a.CompanyCD=f.CompanyCD "
                      + "LEFT OUTER JOIN officedba.FlowInstance e "
                      + "ON f.ID=e.ID  AND f.CompanyCD=e.CompanyCD "
                      + " WHERE a.CompanyCD='" + CompanyID + "'";
            if (EquipName != "")
                sql += " and b.EquipmentName like '%" + EquipName + "%'";
            if (EquipIndex != "")
                sql += " and b.EquipmentIndex like '%" + EquipIndex + "%'";
            if (EquipmentUselessM.RecordNo != "")
                sql += " and a.RecordNo like '%" + EquipmentUselessM.RecordNo + "%'";
            if (EquipmentUselessM.EquipmentNo != "")
                sql += " and b.EquipmentNo like '%" + EquipmentUselessM.EquipmentNo + "%'";
            if (EquipmentUselessM.UselessStatus != "")
                sql += " and b.Status='" + EquipmentUselessM.UselessStatus + "'";
            if (EquipmentUselessM.ApplyDate.ToString() != "" && EquipmentUselessM.ApplyDate.ToString() != "1900-1-1 0:00:00")
                sql += " and a.ApplyDate>='" + EquipmentUselessM.ApplyDate + "'";
            if (EquipmentUselessM.UselessDate.ToString() != "" && EquipmentUselessM.UselessDate.ToString() != "1900-1-1 0:00:00")
                sql += " and a.UselessDate>='" + EquipmentUselessM.UselessDate + "'";
            if (EquipmentUselessM.ApplyUserID.ToString() != "" && EquipmentUselessM.ApplyUserID != 0)
                sql += " and a.ApplyUserID=" + EquipmentUselessM.ApplyUserID + "";
            if (FlowStatus != "" && FlowStatus != "0")
                sql += " and e.FlowStatus = '" + FlowStatus + "'";
            if (FlowStatus == "0")
                sql += " and e.FlowStatus IS NULL";
            sql = sql + ord;
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion
    }
}

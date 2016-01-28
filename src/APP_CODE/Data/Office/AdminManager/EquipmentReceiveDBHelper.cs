/**********************************************
 * 类作用：   设备领用添加数据库层处理
 * 建立人：   lysong
 * 建立时间： 2009/03/12
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
   public class EquipmentReceiveDBHelper
   {
       #region 添加设备领用
       /// <summary>
        /// 设备领用添加操作
        /// </summary>
        /// <param name="EquipReceiveModel">设备领用信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool AddEquipmentReceiveInfo(EquipmentReceiveModel EquipReceiveModel, out int RetValID,int CreateUserID,string CreateDate)
        {
            try
            {
                #region 添加设备领用SQL拼写
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO officedba.EquipmentUsed");
                sql.AppendLine("		(RecordNo      ");
                sql.AppendLine("		,EquipmentNo        ");
                sql.AppendLine("		,CompanyCD        ");
                sql.AppendLine("		,CreateUser        ");
                sql.AppendLine("		,ProposerID        ");
                sql.AppendLine("		,ProposerDeptID        ");
                sql.AppendLine("		,Critical        ");
                sql.AppendLine("		,UsedDate        ");
                sql.AppendLine("		,UsedLong        ");
                sql.AppendLine("		,CreateDate        ");
                sql.AppendLine("		,UsedType        ");
                sql.AppendLine("		,UsedDemand        ");
                sql.AppendLine("		,ApplyReason        ");
                sql.AppendLine("		,ApplyDate        ");
                sql.AppendLine("		,UseStartDate        ");
                sql.AppendLine("		,ModifiedDate        ");
                sql.AppendLine("		,ModifiedUserID)        ");
                sql.AppendLine("VALUES                  ");
                sql.AppendLine("		(@RecordNo   ");
                sql.AppendLine("		,@EquipmentNo       ");
                sql.AppendLine("		,@CompanyCD       ");
                sql.AppendLine("		,@CreateUser       ");
                sql.AppendLine("		,@ProposerID       ");
                sql.AppendLine("		,@ProposerDeptID       ");
                sql.AppendLine("		,@Critical       ");
                sql.AppendLine("		,@UsedDate       ");
                sql.AppendLine("		,@UsedLong       ");
                sql.AppendLine("		,@CreateDate       ");
                sql.AppendLine("		,@UsedType       ");
                sql.AppendLine("		,@UsedDemand       ");
                sql.AppendLine("		,@ApplyReason       ");
                sql.AppendLine("		,@ApplyDate       ");
                sql.AppendLine("		,@UseStartDate       ");
                sql.AppendLine("		,@ModifiedDate       ");
                sql.AppendLine("		,@ModifiedUserID)       ");
                sql.AppendLine("set @ID=@@IDENTITY");
                #endregion
                #region 添加设备领用参数设置
                SqlParameter[] param;
                param = new SqlParameter[18];
                param[0] = SqlHelper.GetParameter("@RecordNo", EquipReceiveModel.RecordNo);
                param[1] = SqlHelper.GetParameter("@EquipmentNo", EquipReceiveModel.EquipmentNo);
                param[2] = SqlHelper.GetParameter("@CompanyCD", EquipReceiveModel.CompanyCD);
                param[3] = SqlHelper.GetParameter("@CreateUser", CreateUserID);
                param[4] = SqlHelper.GetParameter("@ProposerID", EquipReceiveModel.ProposerID);
                param[5] = SqlHelper.GetParameter("@ProposerDeptID", EquipReceiveModel.ProposerDeptID);
                param[6] = SqlHelper.GetParameter("@Critical", EquipReceiveModel.Critical);
                param[7] = SqlHelper.GetParameter("@UsedDate", EquipReceiveModel.UsedDate);
                param[8] = SqlHelper.GetParameter("@UsedLong", EquipReceiveModel.UsedLong);
                param[9] = SqlHelper.GetParameter("@CreateDate", Convert.ToDateTime(CreateDate));
                param[10] = SqlHelper.GetParameter("@UsedType", EquipReceiveModel.UsedType);
                param[11] = SqlHelper.GetParameter("@UsedDemand", EquipReceiveModel.UsedDemand);
                param[12] = SqlHelper.GetParameter("@ApplyReason", EquipReceiveModel.ApplyReason);
                param[13] = SqlHelper.GetParameter("@ApplyDate", EquipReceiveModel.ApplyDate);
                param[14] = SqlHelper.GetParameter("@UseStartDate", EquipReceiveModel.UseStartDate);
                param[15] = SqlHelper.GetParameter("@ModifiedDate", EquipReceiveModel.ModifiedDate);
                param[16] = SqlHelper.GetParameter("@ModifiedUserID", EquipReceiveModel.ModifiedUserID);
                param[17] = SqlHelper.GetOutputParameter("@ID", SqlDbType.Int);
                #endregion
                SqlHelper.ExecuteTransSql(sql.ToString(), param);
                RetValID = Convert.ToInt32(param[17].Value.ToString());
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch 
            {
                RetValID = 0;
                return false;
            }
        }
       #endregion
       #region 更新设备领用信息
       /// <summary>
       /// 更新设备和配件操作
       /// </summary>
       /// <param name="model">设备信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool UpdateEquipMnetAndInfo(EquipmentReceiveModel EquipReceiveModel)
       {
           try
           {
               #region 更新设备领用SQL拼写
               StringBuilder EquipReceiveSql = new StringBuilder();
               EquipReceiveSql.AppendLine("UPDATE officedba.EquipmentUsed SET ");
               EquipReceiveSql.AppendLine("EquipmentNo=           @EquipmentNo, ");
               EquipReceiveSql.AppendLine("CompanyCD =        @CompanyCD , ");
               //EquipReceiveSql.AppendLine("Status =        @Status , ");
               EquipReceiveSql.AppendLine("ProposerID =        @ProposerID , ");
               EquipReceiveSql.AppendLine("ProposerDeptID =        @ProposerDeptID , ");
               EquipReceiveSql.AppendLine("Critical =        @Critical , ");
               EquipReceiveSql.AppendLine("UsedDate =        @UsedDate , ");
               EquipReceiveSql.AppendLine("UsedLong =        @UsedLong , ");
               //EquipReceiveSql.AppendLine("UsedCount =        @UsedCount , ");
               EquipReceiveSql.AppendLine("UsedType =        @UsedType , ");
               EquipReceiveSql.AppendLine("UsedDemand =        @UsedDemand , ");
               EquipReceiveSql.AppendLine("ApplyReason =        @ApplyReason , ");
               EquipReceiveSql.AppendLine("ApplyDate =        @ApplyDate , ");
               EquipReceiveSql.AppendLine("UseStartDate =        @UseStartDate , ");
               EquipReceiveSql.AppendLine("ModifiedDate =        @ModifiedDate , ");
               EquipReceiveSql.AppendLine("ModifiedUserID =        @ModifiedUserID ");
               EquipReceiveSql.AppendLine(" WHERE ");
               EquipReceiveSql.AppendLine("RecordNo = @RecordNo ");
               #endregion
               #region 更新设备领用参数设置
               SqlParameter[] param;
               param = new SqlParameter[15];
               param[0] = SqlHelper.GetParameter("@ModifiedUserID", EquipReceiveModel.ModifiedUserID);
               param[1] = SqlHelper.GetParameter("@EquipmentNo", EquipReceiveModel.EquipmentNo);
               param[2] = SqlHelper.GetParameter("@CompanyCD", EquipReceiveModel.CompanyCD);
              // param[3] = SqlHelper.GetParameter("@Status", EquipReceiveModel.Status);
               param[3] = SqlHelper.GetParameter("@ProposerID", EquipReceiveModel.ProposerID);
               param[4] = SqlHelper.GetParameter("@ProposerDeptID", EquipReceiveModel.ProposerDeptID);
               param[5] = SqlHelper.GetParameter("@Critical", EquipReceiveModel.Critical);
               param[6] = SqlHelper.GetParameter("@UsedDate", EquipReceiveModel.UsedDate);
               param[7] = SqlHelper.GetParameter("@UsedLong", EquipReceiveModel.UsedLong);
               //param[9] = SqlHelper.GetParameter("@UsedCount", EquipReceiveModel.UsedCount);
               param[8] = SqlHelper.GetParameter("@UsedType", EquipReceiveModel.UsedType);
               param[9] = SqlHelper.GetParameter("@UsedDemand", EquipReceiveModel.UsedDemand);
               param[10] = SqlHelper.GetParameter("@ApplyReason", EquipReceiveModel.ApplyReason);
               param[11] = SqlHelper.GetParameter("@ApplyDate", EquipReceiveModel.ApplyDate);
               param[12] = SqlHelper.GetParameter("@UseStartDate", EquipReceiveModel.UseStartDate);
               param[13] = SqlHelper.GetParameter("@ModifiedDate", EquipReceiveModel.ModifiedDate);
               param[14] = SqlHelper.GetParameter("@RecordNo", EquipReceiveModel.RecordNo);
               #endregion

               SqlHelper.ExecuteTransSql(EquipReceiveSql.ToString(), param);
               return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch 
           {
               return false;
           }
       }
       #endregion
       #region 查询设备领用信息列表
       /// <summary>
       /// 查询设备信息列表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetEquipmentReceiveInfoBycondition(EquipmentReceiveModel EquipmentReceiveM, string EquipName, string EquipIndex, string FlowStatus,int pageIndex,int pageCount,string ord, ref int TotalCount)
       {
           string sql = "SELECT distinct b.ID, isnull(a.EquipmentNo,'')EquipmentNo, "
                        + "isnull(a.EquipmentIndex,'无') EquipmentIndex, "
                        +"isnull(a.CompanyCD,'无')CompanyCD,isnull(a.EquipmentName,'')EquipmentName,"
                        +"isnull(a.Norm,'无') Norm, isnull(a.BuyDate,'1900-1-1')BuyDate,isnull(a.Provider,'无')Provider, "
                        +"isnull(a.Type,0) Type, isnull(a.Warranty,'无') Warranty,isnull(a.ExaminePeriod,'1900-1-1') ExaminePeriod, "
                        +"isnull(a.CurrentUser,0)CurrentUser, isnull(a.CurrentDepartment,0)CurrentDepartment,"
                        +"isnull(a.FittingFlag,'无')FittingFlag, isnull(a.Unit,'')Unit, isnull(a.EquipmentDetail,'无') EquipmentDetail,"
                        +"isnull(b.RecordNo,'无') RecordNo,"
                        +"case a.Status when 0 then '空闲' when 1 then '使用' when 3 then '维修' when 5 then '报废' end  Status, "
                        +"isnull(b.ProposerID,-1)ProposerID, isnull(b.ProposerDeptID,-1)ProposerDeptID,"
                        +"CASE b.Critical WHEN 1 THEN '宽松' WHEN 2 THEN '一般' WHEN 3 THEN '较急' WHEN 4 THEN '紧急' WHEN 5 THEN '特急' "
                        +"END critical,isnull(b.UsedDate,'1900-1-1') UsedDate, isnull(b.UsedLong,-1)UsedLong, "
                        +"isnull(b.UsedCount,-1)UsedCount,case b.UsedType when 0 then '未选择类型' "
                        +"when 1 then '领用' when 2 then '借用'  end UsedType, isnull(b.UsedDemand,'无')UsedDemand, isnull(b.ApplyReason,'无') "
                        +"ApplyReason, isnull(b.ApplyDate,'1900-1-1')ApplyDate, isnull(b.UseStartDate,'1900-1-1')UseStartDate, "
                        +"isnull(b.UseEndDate,'1900-1-1')UseEndDate,isnull(b.ReturnUserID,-1)ReturnUserID,isnull(b.ReturnDeptID,-1)ReturnDeptID, "
                        +"isnull(b.ReturnReason,'无')ReturnReason, isnull(b.ReturnRemark,'无')ReturnRemark,isnull(b.ModifiedDate,'1900-1-1') "
                        +"ModifiedDate, isnull(b.ModifiedUserID,-1)ModifiedUserID,isnull(c.CodeName,'')CodeName,CASE isnull(h.FlowStatus,0) "
                        +"WHEN 0 THEN '' WHEN 1 THEN '待审批'  WHEN 2 THEN '审批中' WHEN 3 THEN '审批通过'WHEN 4 THEN '审批不通过' "
                        +"WHEN 5 THEN '撤销审批'  END FlowStatus, isnull(f.EmployeeName,'')EmployeeName,isnull(e.DeptName,'')DeptName  "
                        +"FROM officedba.EquipmentUsed AS b LEFT OUTER JOIN "
                        +"officedba.EquipmentInfo AS a ON a.EquipmentNo = b.EquipmentNo AND a.CompanyCD=b.CompanyCD "
                        +"LEFT OUTER JOIN  (select m.CodeName,n.EquipmentNo,m.CompanyCD from officedba.EquipmentInfo n "
                        +"left outer join officedba.CodeEquipmentType m on m.id=n.type AND m.CompanyCD=n.CompanyCD) c "
                        +"ON b.EquipmentNo=c.EquipmentNo  and b.CompanyCD=c.CompanyCD "
                        +"LEFT OUTER JOIN officedba.EmployeeInfo f ON b.ProposerID=f.ID and b.CompanyCD=f.CompanyCD "
                        +"LEFT OUTER JOIN  officedba.DeptInfo e  ON b.ProposerDeptID=e.ID  and b.CompanyCD=e.CompanyCD "
                        +"LEFT OUTER JOIN "
                        +"(select max(m.id)ID,m.BillID,m.BillNo,m.CompanyCD from officedba.FlowInstance m,"
                        + "officedba.EquipmentUsed n  where m.BillTypeFlag='" + ConstUtil.CODING_RULE_EQUIPMENT + "' AND m.BillTypeCode='" + ConstUtil.CODING_RULE_EQUIPMENT_APPLY + "' and  "
                        +"m.BillID=n.ID group by m.BillID,m.BillNo,m.CompanyCD) g  on b.RecordNo=g.BillNo and b.CompanyCD=g.CompanyCD "
                        +"LEFT OUTER JOIN officedba.FlowInstance h ON g.ID=h.ID and g.CompanyCD=h.CompanyCD "
                        +"WHERE b.CompanyCD = '" + EquipmentReceiveM .CompanyCD+ "' ";
           if (EquipName != "")
               sql += " and a.EquipmentName like '%" + EquipName + "%'";
           if (EquipIndex != "")
               sql += " and a.EquipmentIndex like '%" + EquipIndex + "%'";
           if (EquipmentReceiveM.RecordNo != "")
               sql += " and b.RecordNo like '%" + EquipmentReceiveM.RecordNo + "%'";
           if (EquipmentReceiveM.EquipmentNo != "")
               sql += " and a.EquipmentNo like '%" + EquipmentReceiveM.EquipmentNo + "%'";
           if (EquipmentReceiveM.Critical != "")
               sql += " and b.Critical='" + EquipmentReceiveM.Critical + "'";
           if (EquipmentReceiveM.Status != "")
               sql += " and a.Status='" + EquipmentReceiveM.Status + "'";
           if (EquipmentReceiveM.UsedDate.ToString() != "" && EquipmentReceiveM.UsedDate.ToString() != "1900-1-1 0:00:00")
               sql += " and b.UsedDate>'" + EquipmentReceiveM.UsedDate + "'";
           if (EquipmentReceiveM.ProposerID.ToString() != "" && EquipmentReceiveM.ProposerID!=0)
               sql += " and b.ProposerID=" + EquipmentReceiveM.ProposerID + "";
           if (FlowStatus != "" && FlowStatus != "0")
               sql += " and h.FlowStatus = '" + FlowStatus + "'";
           if (FlowStatus == "0")
               sql += " and h.FlowStatus IS NULL";
           return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);

           //return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 删除设备领用信息
       /// <summary>
       /// 删除设备领用信息
       /// </summary>
       /// <param name="EquipReceiveNos">设备领用IDS</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool DelEquipReceiveInfo(string EquipReceiveNos)
       {
           string allEquipID = "";
           System.Text.StringBuilder sb = new System.Text.StringBuilder();
           string[] Delsql = new string[1];
           try
           {
               string[] EquipIDS = null;
               EquipIDS = EquipReceiveNos.Split(',');

               for (int i = 0; i < EquipIDS.Length; i++)
               {
                   EquipIDS[i] = "'" + EquipIDS[i] + "'";
                   sb.Append(EquipIDS[i]);
               }

               allEquipID = sb.ToString().Replace("''", "','");
               Delsql[0] = "DELETE FROM officedba.EquipmentUsed WHERE RecordNo IN (" + allEquipID + ")";
               SqlHelper.ExecuteTransForListWithSQL(Delsql);
               return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch 
           {
               return false;
           }
       }
       #endregion
       #region 根据设备编号查询设备信息
       /// <summary>
       /// 根据领用单据号获取设备领用信息
       /// </summary>
       /// <param name="EquipmnetNO">设备单据号</param>
       /// <returns>DataTable</returns>
       public static DataTable GetEquipmentReceiveInfoByRecordNo(string RecordNo,string CompanyCD)
       {
           string sql = "SELECT a.*,c.EmployeeName,d.DeptName,e.EmployeeName as RetrunUser,u.EmployeeName as CreateUserName,f.DeptName as ReturnDeptName, "
               +"CASE isnull(h.FlowStatus,0) WHEN 0 THEN '' "
                         +"WHEN 1 THEN '待审批' "
                        +" WHEN 2 THEN '审批中' "
                         +"WHEN 3 THEN '审批通过'"
                         +"WHEN 4 THEN '审批不通过' "
                        +" WHEN 5 THEN '撤销审批' "
                        +"END FlowStatus,"
						+"case x.Status when 0 then '空闲' when 1 then '使用' when 3 then '维修' "
                        + "when 5 then '报废' end  Status "
                        +"FROM officedba.EquipmentUsed a "
                        +"LEFT OUTER JOIN officedba.EmployeeInfo c "
                        +"ON a.ProposerID=c.ID "
                        +"LEFT OUTER JOIN "
                        +"officedba.DeptInfo d "
				        +"ON a.ProposerDeptID=d.ID "
					    +"LEFT OUTER JOIN officedba.EmployeeInfo e "
                        +"ON a.ReturnUserID=e.ID "
                        + "LEFT OUTER JOIN officedba.EmployeeInfo u "
                        + "ON a.CreateUser=u.ID "
                        +"LEFT OUTER JOIN "
                        +"officedba.DeptInfo f "
				        +"ON a.ReturnDeptID=f.ID  "
                        +"LEFT OUTER JOIN "
+"(select max(m.id)ID,m.BillID,m.BillNo,m.CompanyCD from officedba.FlowInstance m,officedba.EquipmentUsed n  "
                        + "where m.BillTypeFlag='" + ConstUtil.CODING_RULE_EQUIPMENT + "' AND "
                        + "m.BillTypeCode='" + ConstUtil.CODING_RULE_EQUIPMENT_APPLY + "' and  m.BillID=n.ID and Billid=" + RecordNo + " group by m.BillID,m.BillNo,m.CompanyCD) g "
+"on a.RecordNo=g.BillNo and a.CompanyCD=g.CompanyCD "
+"LEFT OUTER JOIN officedba.FlowInstance h "
                        +"ON g.ID=h.ID and g.CompanyCD=h.CompanyCD "
+"LEFT OUTER JOIN officedba.EquipmentInfo x "
+"on a.EquipmentNo=x.EquipmentNo and a.CompanyCD=x.CompanyCD "
           +"WHERE a.ID=" + RecordNo + " AND a.CompanyCD='" + CompanyCD + "'";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 查询某设备是否可以领用
       /// <summary>
       /// 查询某设备是否可以领用
       /// </summary>
       /// <param name="RecordNo">设备单据号</param>
       /// <returns>DataTable</returns>
       public static DataTable IfApply(string EquipmentNo,string RecordNo,string CompanyCD)
       {
           string sql = "select * from officedba.EquipmentInfo A "
                        + "LEFT OUTER JOIN officedba.EquipmentUsed B "
                        + "ON A.EquipmentNo=B.EquipmentNo AND A.CompanyCD=B.CompanyCD "
                        + "LEFT OUTER JOIN officedba.FlowInstance C "
                        + "ON B.RecordNo=C.BillNo "
                        + "where A.EquipmentNo='" + EquipmentNo + "'  AND A.Status='0' AND "
                        + "B.RecordNo='" + RecordNo + "' and A.CompanyCD='" + CompanyCD + "'  "
                        + "AND C.BillTypeFlag='" + ConstUtil.CODING_RULE_EQUIPMENT + "' "
                        + "AND C.BillTypeCode='" + ConstUtil.CODING_RULE_EQUIPMENT_APPLY + "'  and  C.FlowStatus='3' ";
           //SELECT a.* FROM officedba.EquipmentInfo a "
           //                 +"LEFT OUTER JOIN officedba.FlowInstance b "
           //                 + "ON a.RecordNo=b.BillNo  "
           //                 + "WHERE a.RecordNo='" + RecordNo + "' and a.CompanyCD='" + CompanyCD + "' and a.Status=0 AND b.BillTypeFlag='" + ConstUtil.CODING_RULE_EQUIPMENT + "' "
           //                 + "AND b.BillTypeCode='" + ConstUtil.CODING_RULE_EQUIPMENT_APPLY + "'  and  b.FlowStatus='3'";
 
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 领用时获取使用人和使用部门用以更新设备表的当前使用人和使用部门
       /// <summary>
       /// 领用时获取使用人和使用部门用以更新设备表的当前使用人和使用部门
       /// </summary>
       /// <param name="RecordNo">设备单据号</param>
       /// <returns>DataTable</returns>
       public static DataTable GetApplyInfo(string RecordNo, string CompanyCD)
       {
           string sql = "select * from officedba.EquipmentUsed  where RecordNo='"+RecordNo+"' AND CompanyCD='"+CompanyCD+"'";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 查询某设备是否可以归还
       /// <summary>
       /// 查询某设备是否可以归还
       /// </summary>
       /// <param name="RecordNo">设备单据号</param>
       /// <returns>DataTable</returns>
       public static DataTable IfRetrun(string EquipmentNo, string RecordNo, string CompanyCD)
       {
           string sql = "select * from officedba.EquipmentInfo A "
                        + "LEFT OUTER JOIN officedba.EquipmentUsed B "
                        + "ON A.EquipmentNo=B.EquipmentNo AND A.CompanyCD=B.CompanyCD "
                        + "LEFT OUTER JOIN officedba.FlowInstance C "
                        + "ON B.RecordNo=C.BillNo "
                        + "where A.EquipmentNo='" + EquipmentNo + "'  AND A.Status='1' AND "
                        + "B.RecordNo='" + RecordNo + "' and A.CompanyCD='" + CompanyCD + "'  "
                        + "AND C.BillTypeFlag='" + ConstUtil.CODING_RULE_EQUIPMENT + "' "
                        + "AND C.BillTypeCode='" + ConstUtil.CODING_RULE_EQUIPMENT_APPLY + "'  and  C.FlowStatus='3' ";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion

       #region 领用设备动作
       /// <summary>
       /// 领用设备动作
       /// </summary>
       /// <param name="EquipReceiveNos">设备领用IDS</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool UpdateApply(string RecordNo,string EquipmentNos, string CompanyCD,string UserID,string DeptID)
       {
           try
           {
               string[] DealSql = new string[2];
               DealSql[0] = "UPDATE officedba.EquipmentInfo SET Status=1,CurrentUser=" + UserID + ",CurrentDepartment=" + DeptID + " WHERE EquipmentNo='" + EquipmentNos + "' and CompanyCD='" + CompanyCD + "' ";
               DealSql[1] = "UPDATE officedba.EquipmentUsed SET ApplyFactDate='" + System.DateTime.Now.ToShortDateString() + "' WHERE RecordNo='" + RecordNo + "' and CompanyCD='" + CompanyCD + "' ";
               SqlHelper.ExecuteTransForListWithSQL(DealSql);
               return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch 
           {
               return false;
           }
       }
       #endregion

       #region 设备归还
       /// <summary>
       /// 设备归还
       /// </summary>
       /// <param name="EquipReceiveModel">归还信息</param>
       /// <returns>更新是否成功 false:失败，true:成功</returns>
       public static bool UpdateEquipMnetReturnInfo(EquipmentReceiveModel EquipReceiveModel)
       {
           try
           {
               #region 更新设备领用SQL拼写
               StringBuilder EquipReceiveSql = new StringBuilder();
               EquipReceiveSql.AppendLine("UPDATE officedba.EquipmentUsed SET ");
               EquipReceiveSql.AppendLine("Status =        @Status , ");
               EquipReceiveSql.AppendLine("UseEndDate =        @UseEndDate , ");
               EquipReceiveSql.AppendLine("ReturnUserID =        @ReturnUserID , ");
               EquipReceiveSql.AppendLine("ReturnDeptID =        @ReturnDeptID , ");
               EquipReceiveSql.AppendLine("ReturnReason =        @ReturnReason , ");
               EquipReceiveSql.AppendLine("ReturnRemark =        @ReturnRemark , ");
               EquipReceiveSql.AppendLine("ModifiedDate =        @ModifiedDate , ");
               EquipReceiveSql.AppendLine("ModifiedUserID =        @ModifiedUserID ");
               EquipReceiveSql.AppendLine(" WHERE ");
               EquipReceiveSql.AppendLine("RecordNo = @RecordNo AND  CompanyCD =@CompanyCD");
               #endregion
               #region 更新设备领用参数设置
               SqlParameter[] param;
               param = new SqlParameter[10];
               param[0] = SqlHelper.GetParameter("@Status", EquipReceiveModel.Status);
               param[1] = SqlHelper.GetParameter("@UseEndDate", EquipReceiveModel.UseEndDate);
               param[2] = SqlHelper.GetParameter("@ReturnUserID", EquipReceiveModel.ReturnUserID);
               param[3] = SqlHelper.GetParameter("@ReturnDeptID", EquipReceiveModel.ReturnDeptID);
               param[4] = SqlHelper.GetParameter("@ReturnReason", EquipReceiveModel.ReturnReason);
               param[5] = SqlHelper.GetParameter("@ReturnRemark", EquipReceiveModel.ReturnRemark);
               param[6] = SqlHelper.GetParameter("@ModifiedDate", EquipReceiveModel.ModifiedDate);
               param[7] = SqlHelper.GetParameter("@ModifiedUserID", EquipReceiveModel.ModifiedUserID);
               param[8] = SqlHelper.GetParameter("@RecordNo", EquipReceiveModel.RecordNo);
               param[9] = SqlHelper.GetParameter("@CompanyCD", EquipReceiveModel.CompanyCD);
               string updatesql = "UPDATE officedba.EquipmentInfo SET Status='0' WHERE EquipmentNo='" + EquipReceiveModel.EquipmentNo + "' and CompanyCD='" + EquipReceiveModel.CompanyCD + "'";
               SqlHelper.ExecuteTransSql(updatesql);
               #endregion

               SqlHelper.ExecuteTransSql(EquipReceiveSql.ToString(), param);
               return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch 
           {
               return false;
           }
       }
       #endregion


       #region 设备领用统计报表列表
       /// <summary>
       /// 设备领用统计报表列表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetEquipmentUsedDetailInfo(string StartDate, string EndDate, string EquipType, string ReceiveDept, string CompanyID, int pageIndex, int pageCount, string ord, ref int TotalCount)
       {
           string sql = "SELECT a.ID,a.EquipmentNo,Convert(varchar(10),ApplyFactDate,120)UsedDate,"
                        + "Convert(varchar(10),UseEndDate,120)UseEndDate,b.EquipmentName,c.CodeName,"
                        +"d.EmployeeName,e.DeptName "
                        +"FROM officedba.EquipmentUsed a "
                        +"LEFT OUTER JOIN officedba.EquipmentInfo b "
                        +"ON a.EquipmentNo=b.EquipmentNo AND a.CompanyCD=b.CompanyCD "
                        +"LEFT OUTER JOIN officedba.CodeEquipmentType c "
                        +"ON b.Type=c.ID "
                        +"LEFT OUTER JOIN officedba.EmployeeInfo d "
                        +"ON a.ProposerID=d.ID "
                        +"LEFT OUTER JOIN officedba.DeptInfo e "
                        +"ON a.ProposerDeptID=e.ID"
           + " where a.CompanyCD='" + CompanyID + "' and a.ApplyFactDate is not null ";
           if (EquipType != "")
               sql += " and b.Type=" + EquipType + "";
           if (StartDate!="")
               sql += " and a.UsedDate>='" + StartDate + "'";
           if (EndDate != "")
               sql += " and UsedDate<='" + EndDate + "'";
           if (ReceiveDept != "")
               sql += " and a.ProposerDeptID=" + ReceiveDept + "";
           return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
       }
       #endregion

       #region 设备领用统计报表打印
       /// <summary>
       /// 设备领用统计报表打印
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetEquipmentUsedDetailReportInfo(string StartDate, string EndDate, string EquipType, string ReceiveDept, string CompanyID, string ord)
       {
           string sql = "SELECT a.ID,a.EquipmentNo,Convert(varchar(10),ApplyFactDate,120)UsedDate,"
                        + "Convert(varchar(10),UseEndDate,120)UseEndDate,b.EquipmentName,c.CodeName,"
                        + "d.EmployeeName,e.DeptName "
                        + "FROM officedba.EquipmentUsed a "
                        + "LEFT OUTER JOIN officedba.EquipmentInfo b "
                        + "ON a.EquipmentNo=b.EquipmentNo AND a.CompanyCD=b.CompanyCD "
                        + "LEFT OUTER JOIN officedba.CodeEquipmentType c "
                        + "ON b.Type=c.ID "
                        + "LEFT OUTER JOIN officedba.EmployeeInfo d "
                        + "ON a.ProposerID=d.ID "
                        + "LEFT OUTER JOIN officedba.DeptInfo e "
                        + "ON a.ProposerDeptID=e.ID"
           + " where a.CompanyCD='" + CompanyID + "' and a.ApplyFactDate is not null ";
           if (EquipType != "")
               sql += " and b.Type=" + EquipType + "";
           if (StartDate != "")
               sql += " and a.UsedDate>='" + StartDate + "'";
           if (EndDate != "")
               sql += " and UsedDate<='" + EndDate + "'";
           if (ReceiveDept != "")
               sql += " and a.ProposerDeptID=" + ReceiveDept + "";
           sql = sql + ord;
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion

       #region 设备领用单据打印
       /// <summary>
       /// 设备领用单据打印
       /// </summary>
       /// <param name="RecordNo">设备领用单据号</param>
       /// <returns>DataTable</returns>
       public static DataTable GetEquipmentReceiveInfoByRecordNoForPrint(string RecordNo, string CompanyCD)
       {
           string sql = "SELECT A.RecordNo,A.EquipmentNo,A.ApplyDate,A.UsedLong,A.UsedDate,"
                        + "CASE A.Critical WHEN '1' THEN '宽松' WHEN '2' THEN '一般' WHEN '3' THEN '较急' WHEN '4' THEN '紧急' WHEN '5' THEN '特急' END Critical,"
                        +"CASE A.UsedType WHEN '1' THEN '领用' WHEN '2' THEN '借用' END UsedType,"
                        +"A.ApplyReason,A.UsedDemand,B.EmployeeName,C.DeptName "
                        +"FROM officedba.EquipmentUsed A "
                        +"LEFT OUTER JOIN officedba.EmployeeInfo B "
                        + "ON A.CompanyCD='" + CompanyCD + "' AND A.ProposerID=B.ID AND A.CompanyCD=B.CompanyCD "
                        +"LEFT OUTER JOIN officedba.DeptInfo C "
                        + "ON A.CompanyCD='" + CompanyCD + "' AND A.ProposerDeptID=C.ID AND A.CompanyCD=B.CompanyCD "
                        + "WHERE a.RecordNo='" + RecordNo + "' AND a.CompanyCD='" + CompanyCD + "'";
                        return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 设备领用导出
       /// <summary>
       /// 设备领用导出
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetEquipmentReceiveInfoForExp(EquipmentReceiveModel EquipmentReceiveM, string EquipName, string EquipIndex, string FlowStatus, string ord)
       {
           string sql = "SELECT distinct b.ID, isnull(a.EquipmentNo,'')EquipmentNo, "
                        + "isnull(a.EquipmentIndex,'无') EquipmentIndex, "
                        + "isnull(a.CompanyCD,'无')CompanyCD,isnull(a.EquipmentName,'')EquipmentName,"
                        + "isnull(a.Norm,'无') Norm, isnull(a.BuyDate,'1900-1-1')BuyDate,isnull(a.Provider,'无')Provider, "
                        + "isnull(a.Type,0) Type, isnull(a.Warranty,'无') Warranty,isnull(a.ExaminePeriod,'1900-1-1') ExaminePeriod, "
                        + "isnull(a.CurrentUser,0)CurrentUser, isnull(a.CurrentDepartment,0)CurrentDepartment,"
                        + "isnull(a.FittingFlag,'无')FittingFlag, isnull(a.Unit,'')Unit, isnull(a.EquipmentDetail,'无') EquipmentDetail,"
                        + "isnull(b.RecordNo,'无') RecordNo,"
                        + "case a.Status when 0 then '空闲' when 1 then '使用' when 3 then '维修' when 5 then '报废' end  Status, "
                        + "isnull(b.ProposerID,-1)ProposerID, isnull(b.ProposerDeptID,-1)ProposerDeptID,"
                        + "CASE b.Critical WHEN 1 THEN '宽松' WHEN 2 THEN '一般' WHEN 3 THEN '较急' WHEN 4 THEN '紧急' WHEN 5 THEN '特急' "
                        + "END critical,isnull(b.UsedDate,'1900-1-1') UsedDate, isnull(b.UsedLong,-1)UsedLong, "
                        + "isnull(b.UsedCount,-1)UsedCount,case b.UsedType when 0 then '未选择类型' "
                        + "when 1 then '领用' when 2 then '借用'  end UsedType, isnull(b.UsedDemand,'无')UsedDemand, isnull(b.ApplyReason,'无') "
                        + "ApplyReason, isnull(b.ApplyDate,'1900-1-1')ApplyDate, isnull(b.UseStartDate,'1900-1-1')UseStartDate, "
                        + "isnull(b.UseEndDate,'1900-1-1')UseEndDate,isnull(b.ReturnUserID,-1)ReturnUserID,isnull(b.ReturnDeptID,-1)ReturnDeptID, "
                        + "isnull(b.ReturnReason,'无')ReturnReason, isnull(b.ReturnRemark,'无')ReturnRemark,isnull(b.ModifiedDate,'1900-1-1') "
                        + "ModifiedDate, isnull(b.ModifiedUserID,-1)ModifiedUserID,isnull(c.CodeName,'')CodeName,CASE isnull(h.FlowStatus,0) "
                        + "WHEN 0 THEN '' WHEN 1 THEN '待审批'  WHEN 2 THEN '审批中' WHEN 3 THEN '审批通过'WHEN 4 THEN '审批不通过' "
                        + "WHEN 5 THEN '撤销审批'  END FlowStatus, isnull(f.EmployeeName,'')EmployeeName,isnull(e.DeptName,'')DeptName  "
                        + "FROM officedba.EquipmentUsed AS b LEFT OUTER JOIN "
                        + "officedba.EquipmentInfo AS a ON a.EquipmentNo = b.EquipmentNo AND a.CompanyCD=b.CompanyCD "
                        + "LEFT OUTER JOIN  (select m.CodeName,n.EquipmentNo,m.CompanyCD from officedba.EquipmentInfo n "
                        + "left outer join officedba.CodeEquipmentType m on m.id=n.type AND m.CompanyCD=n.CompanyCD) c "
                        + "ON b.EquipmentNo=c.EquipmentNo  and b.CompanyCD=c.CompanyCD "
                        + "LEFT OUTER JOIN officedba.EmployeeInfo f ON b.ProposerID=f.ID and b.CompanyCD=f.CompanyCD "
                        + "LEFT OUTER JOIN  officedba.DeptInfo e  ON b.ProposerDeptID=e.ID  and b.CompanyCD=e.CompanyCD "
                        + "LEFT OUTER JOIN "
                        + "(select max(m.id)ID,m.BillID,m.BillNo,m.CompanyCD from officedba.FlowInstance m,"
                        + "officedba.EquipmentUsed n  where m.BillTypeFlag='" + ConstUtil.CODING_RULE_EQUIPMENT + "' AND m.BillTypeCode='" + ConstUtil.CODING_RULE_EQUIPMENT_APPLY + "' and  "
                        + "m.BillID=n.ID group by m.BillID,m.BillNo,m.CompanyCD) g  on b.RecordNo=g.BillNo and b.CompanyCD=g.CompanyCD "
                        + "LEFT OUTER JOIN officedba.FlowInstance h ON g.ID=h.ID and g.CompanyCD=h.CompanyCD "
                        + "WHERE b.CompanyCD = '" + EquipmentReceiveM.CompanyCD + "' ";
           if (EquipName != "")
               sql += " and a.EquipmentName like '%" + EquipName + "%'";
           if (EquipIndex != "")
               sql += " and a.EquipmentIndex like '%" + EquipIndex + "%'";
           if (EquipmentReceiveM.RecordNo != "")
               sql += " and b.RecordNo like '%" + EquipmentReceiveM.RecordNo + "%'";
           if (EquipmentReceiveM.EquipmentNo != "")
               sql += " and a.EquipmentNo like '%" + EquipmentReceiveM.EquipmentNo + "%'";
           if (EquipmentReceiveM.Critical != "")
               sql += " and b.Critical='" + EquipmentReceiveM.Critical + "'";
           if (EquipmentReceiveM.Status != "")
               sql += " and a.Status='" + EquipmentReceiveM.Status + "'";
           if (EquipmentReceiveM.UsedDate.ToString() != "" && EquipmentReceiveM.UsedDate.ToString() != "1900-1-1 0:00:00")
               sql += " and b.UsedDate>'" + EquipmentReceiveM.UsedDate + "'";
           if (EquipmentReceiveM.ProposerID.ToString() != "" && EquipmentReceiveM.ProposerID != 0)
               sql += " and b.ProposerID=" + EquipmentReceiveM.ProposerID + "";
           if (FlowStatus != "" && FlowStatus != "0")
               sql += " and h.FlowStatus = '" + FlowStatus + "'";
           if (FlowStatus == "0")
               sql += " and h.FlowStatus IS NULL ";
           sql = sql + ord;
           return SqlHelper.ExecuteSql(sql);
       }

       #endregion
   }
}

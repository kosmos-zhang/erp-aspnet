/**********************************************
 * 类作用：   车辆申请数据库层处理
 * 建立人：   lysong
 * 建立时间： 2009/04/27
 ***********************************************/
using System;
using XBase.Model.Office.AdminManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using XBase.Common;
using XBase.Data.Common;

namespace XBase.Data.Office.AdminManager
{
    /// <summary>
    /// 类名：CarApplyDBHelper
    /// 描述：车辆管理数据库层处理
    /// 作者：lysong
    /// 创建时间：2009/04/27
    /// </summary>
   public class CarApplyDBHelper
   {
       #region 获取车编号下拉列表
        /// <summary>
        /// 获取车编号下拉列表
        /// </summary>
        /// <returns>DataTable</returns>
       public static DataTable GetCarNo(string CompanyCD)
        {
            string sql = "select ID,CarNo from officedba.CarInfo WHERE CompanyCD='" + CompanyCD + "' AND Status='1'";
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion

       #region 车辆申请添加操作
       /// <summary>
       /// 车辆申请添加操作
       /// </summary>
       /// <param name="CarApplyM">车辆申请信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool InsertCarApplyInfoData(CarApplyModel CarApplyM, out int RetValID)
       {
           try
           {
               #region 车辆申请信息SQL拼写
               StringBuilder sql = new StringBuilder();
               sql.AppendLine("INSERT INTO officedba.CarApply");
               sql.AppendLine("		(CompanyCD      ");
               sql.AppendLine("		,RecordNo        ");
               sql.AppendLine("		,Title        ");
               sql.AppendLine("		,CarNo        ");
               sql.AppendLine("		,ApplyDate        ");
               sql.AppendLine("		,Appler        ");
               sql.AppendLine("		,Reason        ");
               sql.AppendLine("		,LoadHumans        ");
               sql.AppendLine("		,LoadGoods        ");
               sql.AppendLine("		,PlanDate        ");
               sql.AppendLine("		,PlanTime        ");
               sql.AppendLine("		,ReturnDate        ");
               sql.AppendLine("		,ReturnTime        ");
               sql.AppendLine("		,PlanMileage        ");
               sql.AppendLine("		,BillStatus        ");
               sql.AppendLine("		,Creator        ");
               sql.AppendLine("		,CreateDate        ");
               sql.AppendLine("		,Remark        ");
               sql.AppendLine("		,ModifiedDate        ");
               sql.AppendLine("		,ModifiedUserID)        ");
               sql.AppendLine("VALUES                  ");
               sql.AppendLine("		(@CompanyCD   ");
               sql.AppendLine("		,@RecordNo       ");
               sql.AppendLine("		,@Title       ");
               sql.AppendLine("		,@CarNo       ");
               sql.AppendLine("		,@ApplyDate       ");
               sql.AppendLine("		,@Appler       ");
               sql.AppendLine("		,@Reason       ");
               sql.AppendLine("		,@LoadHumans       ");
               sql.AppendLine("		,@LoadGoods       ");
               sql.AppendLine("		,@PlanDate       ");
               sql.AppendLine("		,@PlanTime       ");
               sql.AppendLine("		,@ReturnDate       ");
               sql.AppendLine("		,@ReturnTime       ");
               sql.AppendLine("		,@PlanMileage       ");
               sql.AppendLine("		,@BillStatus       ");
               sql.AppendLine("		,@Creator       ");
               sql.AppendLine("		,@CreateDate       ");
               sql.AppendLine("		,@Remark       ");
               sql.AppendLine("		,@ModifiedDate       ");
               sql.AppendLine("		,@ModifiedUserID)       ");
               sql.AppendLine("set @ID=@@IDENTITY");

               #endregion
               #region 车辆申请信息参数设置
               SqlParameter[] param;
               param = new SqlParameter[21];
               param[0] = SqlHelper.GetParameter("@CompanyCD", CarApplyM.CompanyCD);
               param[1] = SqlHelper.GetParameter("@RecordNo", CarApplyM.RecordNo);
               param[2] = SqlHelper.GetParameter("@Title", CarApplyM.Title);
               param[3] = SqlHelper.GetParameter("@CarNo", CarApplyM.CarNo);
               param[4] = SqlHelper.GetParameter("@ApplyDate", CarApplyM.ApplyDate);
               param[5] = SqlHelper.GetParameter("@Appler", CarApplyM.Appler);
               param[6] = SqlHelper.GetParameter("@Reason", CarApplyM.Reason);
               param[7] = SqlHelper.GetParameter("@LoadHumans", CarApplyM.LoadHumans);
               param[8] = SqlHelper.GetParameter("@LoadGoods", CarApplyM.LoadGoods);
               param[9] = SqlHelper.GetParameter("@PlanDate", CarApplyM.PlanDate);
               param[10] = SqlHelper.GetParameter("@PlanTime", CarApplyM.PlanTime);
               param[11] = SqlHelper.GetParameter("@ReturnDate", CarApplyM.ReturnDate);
               param[12] = SqlHelper.GetParameter("@ReturnTime", CarApplyM.ReturnTime);
               param[13] = SqlHelper.GetParameter("@PlanMileage", CarApplyM.PlanMileage);
               param[14] = SqlHelper.GetParameter("@BillStatus", CarApplyM.BillStatus);
               param[15] = SqlHelper.GetParameter("@Creator", CarApplyM.Creator);
               param[16] = SqlHelper.GetParameter("@CreateDate", CarApplyM.CreateDate);
               param[17] = SqlHelper.GetParameter("@Remark", CarApplyM.Remark);
               param[18] = SqlHelper.GetParameter("@ModifiedDate", CarApplyM.ModifiedDate);
               param[19] = SqlHelper.GetParameter("@ModifiedUserID", CarApplyM.ModifiedUserID);
               param[20] = SqlHelper.GetOutputParameter("@ID", SqlDbType.Int);
               #endregion
               SqlHelper.ExecuteTransSql(sql.ToString(), param);
               RetValID =Convert.ToInt32(param[20].Value.ToString());
               return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch
           {
               RetValID = 0;
               return false;
           }
       }
       #endregion

       #region 车辆申请修改操作
       /// <summary>
       /// 车辆申请修改操作
       /// </summary>
       /// <param name="CarApplyM">车辆申请信息</param>
       /// <returns>修改是否成功 false:失败，true:成功</returns>
       public static bool UpdateCarApplyInfoData(CarApplyModel CarApplyM)
       {
           try
           {
               #region 车辆申请信息SQL拼写
               StringBuilder sql = new StringBuilder();
               sql.AppendLine("UPDATE officedba.CarApply");
               //sql.AppendLine("		SET CompanyCD=@CompanyCD      ");
               //sql.AppendLine("		,RecordNo        ");
               sql.AppendLine("		SET Title=@Title        ");
               sql.AppendLine("		,CarNo=@CarNo        ");
               sql.AppendLine("		,ApplyDate=@ApplyDate        ");
               sql.AppendLine("		,Appler=@Appler       ");
               sql.AppendLine("		,Reason=@Reason        ");
               sql.AppendLine("		,LoadHumans=@LoadHumans        ");
               sql.AppendLine("		,LoadGoods=@LoadGoods        ");
               sql.AppendLine("		,PlanDate=@PlanDate        ");
               sql.AppendLine("		,PlanTime=@PlanTime        ");
               sql.AppendLine("		,ReturnDate=@ReturnDate        ");
               sql.AppendLine("		,ReturnTime=@ReturnTime        ");
               sql.AppendLine("		,PlanMileage=@PlanMileage        ");
               sql.AppendLine("		,BillStatus=@BillStatus        ");
               sql.AppendLine("		,Creator=@Creator        ");
               sql.AppendLine("		,CreateDate=@CreateDate        ");
               sql.AppendLine("		,Remark=@Remark        ");
               sql.AppendLine("		,ModifiedDate=@ModifiedDate        ");
               sql.AppendLine("		,ModifiedUserID=@ModifiedUserID        ");
               sql.AppendLine("WHERE                  ");
               sql.AppendLine("		CompanyCD=@CompanyCD AND RecordNo=@RecordNo   ");

               #endregion
               #region 车辆申请信息参数设置
               SqlParameter[] param;
               param = new SqlParameter[20];
               param[0] = SqlHelper.GetParameter("@Title", CarApplyM.Title);
               param[1] = SqlHelper.GetParameter("@CarNo", CarApplyM.CarNo);
               param[2] = SqlHelper.GetParameter("@ApplyDate", CarApplyM.ApplyDate);
               param[3] = SqlHelper.GetParameter("@Appler", CarApplyM.Appler);
               param[4] = SqlHelper.GetParameter("@Reason", CarApplyM.Reason);
               param[5] = SqlHelper.GetParameter("@LoadHumans", CarApplyM.LoadHumans);
               param[6] = SqlHelper.GetParameter("@LoadGoods", CarApplyM.LoadGoods);
               param[7] = SqlHelper.GetParameter("@PlanDate", CarApplyM.PlanDate);
               param[8] = SqlHelper.GetParameter("@PlanTime", CarApplyM.PlanTime);
               param[9] = SqlHelper.GetParameter("@ReturnDate", CarApplyM.ReturnDate);
               param[10] = SqlHelper.GetParameter("@ReturnTime", CarApplyM.ReturnTime);
               param[11] = SqlHelper.GetParameter("@PlanMileage", CarApplyM.PlanMileage);
               param[12] = SqlHelper.GetParameter("@BillStatus", CarApplyM.BillStatus);
               param[13] = SqlHelper.GetParameter("@Creator", CarApplyM.Creator);
               param[14] = SqlHelper.GetParameter("@CreateDate", CarApplyM.CreateDate);
               param[15] = SqlHelper.GetParameter("@Remark", CarApplyM.Remark);
               param[16] = SqlHelper.GetParameter("@ModifiedDate", CarApplyM.ModifiedDate);
               param[17] = SqlHelper.GetParameter("@ModifiedUserID", CarApplyM.ModifiedUserID);
               param[18] = SqlHelper.GetParameter("@CompanyCD", CarApplyM.CompanyCD);
               param[19] = SqlHelper.GetParameter("@RecordNo", CarApplyM.RecordNo);
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

       #region 查询车辆申请列表
       /// <summary>
       /// 查询车辆申请列表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetCarApplyList(string CompanyID, string RecordNo, string ApplyTitle, string CarNo, string CarMark, string JoinUser, string FlowStatus,int pageIndex,int pageCount,string ord, ref int TotalCount)
       {
           string sql = "SELECT  a.ID, a.RecordNo, a.Title, a.CarNo, CONVERT(VARCHAR(10),a.ApplyDate,120)ApplyDate, a.Appler,"
                         + "isnull(a.Reason,'')Reason, CONVERT(VARCHAR(10),a.PlanDate,120)PlanDate, a.PlanTime, CONVERT(VARCHAR(10),a.ReturnDate,120)ReturnDate,"
		                 +"a.ReturnTime, CASE a.BillStatus "
                         +"WHEN 1 THEN '制单'"
                         +"WHEN 2 THEN '结单'"
                         +"END BillStatus,"
                         + "isnull(b.EmployeeName,'')EmployeeName,isnull(e.DeptName,'')DeptName,CASE isnull(h.FlowStatus,0) "
                         +"WHEN 0 THEN '' "
                         +"WHEN 1 THEN '待审批' "
                         +"WHEN 2 THEN '审批中' "
                         +"WHEN 3 THEN '审批通过' "
                         +"WHEN 4 THEN '审批不通过' "
                         + "WHEN 5 THEN '撤销审批' "
                         +"END FlowStatus "
                         +"FROM officedba.CarApply a "
                         +"LEFT OUTER JOIN  "
                         +"officedba.EmployeeInfo b "
                         +"ON a.Appler=b.ID "
                         + "LEFT OUTER JOIN  officedba.DeptInfo e "
                         + "ON a.ApplyDept=e.ID "
                         //+"LEFT OUTER JOIN officedba.FlowInstance c "
                         //+ "ON a.RecordNo=c.BillNo AND c.BillTypeFlag='" + ConstUtil.CODING_RULE_EQUIPMENT + "' "
                         //+ "AND c.BillTypeCode='" + ConstUtil.CODING_RULE_CAR_APPLY + "' "
                         + " LEFT OUTER JOIN "
                         + "(select max(m.id)ID,m.BillID,m.BillNo,m.CompanyCD from officedba.FlowInstance m,officedba.CarApply n  "
                         + "where m.BillTypeFlag='" + ConstUtil.CODING_RULE_EQUIPMENT + "' AND m.CompanyCD='" + CompanyID + "' AND "
                         + "m.BillTypeCode='" + ConstUtil.CODING_RULE_CAR_APPLY + "' and  m.BillID=n.ID group by m.BillID,m.BillNo,m.CompanyCD) g  "
                         + "on a.RecordNo=g.BillNo "
                         + "LEFT OUTER JOIN officedba.FlowInstance h "
                         + "ON g.ID=h.ID "
                         +"INNER JOIN "
                         +"officedba.CarInfo d "
                         + "ON a.CarNo=d.CarNo  and a.companycd=d.companycd "
                         + "WHERE a.CompanyCD='" + CompanyID + "'  ";
           if (RecordNo != "")
               sql += " and a.RecordNo LIKE '%" + RecordNo + "%'";
           if (ApplyTitle != "")
               sql += " and a.Title LIKE '%" + ApplyTitle + "%'";
           if (CarNo != "")
               sql += " and a.CarNo LIKE '%" + CarNo + "%'";
           if (CarMark != "")
               sql += " and d.CarMark LIKE '%" + CarMark + "%'";
           if (JoinUser != "")
               sql += " and b.EmployeeName LIKE '%" + JoinUser + "%'";
           if (FlowStatus != "" && FlowStatus != "0")
               sql += " and h.FlowStatus = '" + FlowStatus + "'";
           if (FlowStatus == "0")
               sql += " and h.FlowStatus IS NULL";
           return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);

           //return SqlHelper.ExecuteSql(sql);
       }
       #endregion

       #region 由车辆编号获取信息，查看或修改
       /// <summary>
       /// 由车辆申请单据编号获取信息，查看或修改
       /// </summary>
       /// <param name="CarApplyNo">车辆申请单据编号</param>
       /// <returns>DataTable</returns>
       public static DataTable GetCarApplyByCarApplyNo(string CarApplyNo,string NO, string CompanyID)
       {
           string sql = "select a.*,b.EmployeeName as ApplerName,c.DeptName,d.EmployeeName AS CreatorName,"
                        + "e.EmployeeName AS ComfirmName, "
                        + "CASE isnull(h.FlowStatus,0) WHEN 0 THEN '' "
                        + "WHEN 1 THEN '待审批' "
                        + " WHEN 2 THEN '审批中' "
                        + "WHEN 3 THEN '审批通过'"
                        + "WHEN 4 THEN '审批不通过' "
                        + " WHEN 5 THEN '撤销审批' "
                        + "END FlowStatus "
                        + "from officedba.CarApply a "
                        + "LEFT OUTER JOIN  officedba.EmployeeInfo b "
                        + "ON a.Appler=b.ID "
                        + "LEFT OUTER JOIN "
                        + "officedba.DeptInfo c "
                        + "ON a.ApplyDept=c.ID "
                        + "LEFT OUTER JOIN officedba.EmployeeInfo d "
                        + "ON a.Creator=d.ID "
                        + "LEFT OUTER JOIN officedba.EmployeeInfo e "
                        + "ON a.Confirmor=e.ID  "
                        + "LEFT OUTER JOIN "
                        + "(select max(m.id)ID,m.BillID,m.BillNo,m.CompanyCD from officedba.FlowInstance m,officedba.CarApply n  "
                        + "where m.BillTypeFlag='" + ConstUtil.CODING_RULE_EQUIPMENT + "' AND "
                        + "m.BillTypeCode='" + ConstUtil.CODING_RULE_CAR_APPLY + "' and  m.BillID=n.ID and BillNo='" + NO + "' group by m.BillID,m.BillNo,m.CompanyCD) g "
                        + "on a.RecordNo=g.BillNo and a.CompanyCD=g.CompanyCD "
                        + "LEFT OUTER JOIN officedba.FlowInstance h "
                        + "ON g.ID=h.ID and g.CompanyCD=h.CompanyCD "
                        + "WHERE a.ID=" + CarApplyNo + " AND a.CompanyCD='" + CompanyID + "'";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion

       #region 更新车辆申请确认信息
       /// <summary>
       /// 更新车辆申请确认信息
       /// </summary>
       /// <param name="CarApplyM">车辆申请信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool UpdateCarApplyConfirm(string BillStatus, string Confirmor, string ConfirmDate, string ID, string userID)
       {
           try
           {
               #region 车辆申请信息SQL拼写
               StringBuilder sql = new StringBuilder();
               sql.AppendLine("UPDATE officedba.CarApply");
               sql.AppendLine("		SET BillStatus=@BillStatus        ");
               sql.AppendLine("		,Confirmor=@Confirmor        ");
               sql.AppendLine("		,ConfirmDate=@ConfirmDate        ");
               sql.AppendLine("		,ModifiedDate=@ModifiedDate        ");
               sql.AppendLine("		,ModifiedUserID=@ModifiedUserID        ");
               sql.AppendLine("WHERE                  ");
               sql.AppendLine("		ID=@ID   ");

               #endregion
               #region 车辆申请信息参数设置
               SqlParameter[] param;
               param = new SqlParameter[6];
               param[0] = SqlHelper.GetParameter("@BillStatus", BillStatus);
               param[1] = SqlHelper.GetParameter("@Confirmor",Confirmor);
               param[2] = SqlHelper.GetParameter("@ConfirmDate", ConfirmDate);
               param[3] = SqlHelper.GetParameter("@ModifiedDate", System.DateTime.Now);
               param[4] = SqlHelper.GetParameter("@ModifiedUserID", userID);
               param[5] = SqlHelper.GetParameter("@ID", ID);

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
       #region 取消车辆申请确认信息
       /// <summary>
       /// 取消车辆申请确认信息
       /// </summary>
       /// <param name="CarApplyM">车辆申请信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool UpdateCarApplyCancelConfirm(string BillStatus, string Confirmor, string ConfirmDate, string ID, string userID, string CompanyID)
       {
           try
           {
               #region 车辆申请信息SQL拼写
               StringBuilder sql = new StringBuilder();
               sql.AppendLine("UPDATE officedba.CarApply");
               sql.AppendLine("		SET BillStatus=@BillStatus        ");
               sql.AppendLine("		,Confirmor=@Confirmor        ");
               sql.AppendLine("		,ConfirmDate=@ConfirmDate        ");
               sql.AppendLine("		,ModifiedDate=@ModifiedDate        ");
               sql.AppendLine("		,ModifiedUserID=@ModifiedUserID        ");
               sql.AppendLine("WHERE                  ");
               sql.AppendLine("		ID=@ID   ");

               #endregion
               #region 车辆申请信息参数设置
               SqlParameter[] param;
               param = new SqlParameter[6];
               param[0] = SqlHelper.GetParameter("@BillStatus", BillStatus);
               param[1] = SqlHelper.GetParameter("@Confirmor", Confirmor);
               param[2] = SqlHelper.GetParameter("@ConfirmDate", ConfirmDate);
               param[3] = SqlHelper.GetParameter("@ModifiedDate", System.DateTime.Now);
               param[4] = SqlHelper.GetParameter("@ModifiedUserID", userID);
               param[5] = SqlHelper.GetParameter("@ID", ID);
               #endregion
               //SqlHelper.ExecuteTransSql(sql.ToString(), param);
               TransactionManager tran = new TransactionManager();
               tran.BeginTransaction();
               try
               {
                   FlowDBHelper.OperateCancelConfirm(CompanyID, 3, 8, Convert.ToInt32(ID), userID, tran);//取消确认
                   SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, sql.ToString(), param);
                   tran.Commit();
                   return true;
               }
               catch
               {
                   tran.Rollback();
                   return false;
               }
           }
           catch
           {
               return false;
           }
       }
       #endregion
       #region 能否取消确认请车单信息
       /// <summary>
       /// 能否取消确认请车单信息
       /// </summary>
       /// <param name="EmployeeID">员工ID</param>
       /// <param name="CompanyID">公司代码</param>
       /// <returns></returns>
       public static bool IfCancelCarApplyInf(string ID)
       {

           string sql = "select * from officedba.CarDispatch WHERE ApplyID=" + ID + "";
           return SqlHelper.ExecuteSql(sql).Rows.Count > 0 ? true : false;
       }
       #endregion
       #region 能否删除请车单信息
       /// <summary>
       /// 能否删除请车单信息
       /// </summary>
       /// <param name="EmployeeID">员工ID</param>
       /// <param name="CompanyID">公司代码</param>
       /// <returns></returns>
       public static bool IfDeleteCarApply(string AttendanceApplyIDS, string BillTypeFlag, string BillTypeCode)
       {
           string[] IDS = null;
           IDS = AttendanceApplyIDS.Split(',');
           bool Flag = true;

           for (int i = 0; i < IDS.Length; i++)
           {
               if (IsExistInfo(IDS[i], BillTypeFlag, BillTypeCode))
               {
                   Flag = false;
                   break;
               }
           }
           return Flag;
       }
       #endregion
       #region 能否删除请车单信息
       /// <summary>
       /// 能否删除请车单信息
       /// </summary>
       /// <param name="EmployeeID">员工ID</param>
       /// <param name="CompanyID">公司代码</param>
       /// <returns></returns>
       public static bool IsExistInfo(string ID, string BillTypeFlag, string BillTypeCode)
       {

           string sql = "SELECT * FROM officedba.FlowInstance WHERE BillID=" + ID + " AND BillTypeFlag='" + BillTypeFlag + "' AND BillTypeCode='" + BillTypeCode + "'";
           return SqlHelper.ExecuteSql(sql).Rows.Count > 0 ? true : false;
       }
       #endregion
       #region 删除请车单信息
       /// <summary>
       /// 删除请车单信息
       /// </summary>
       /// <param name="EquipmentIDS">请车单IDS</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool DeleteCarApply(string ApplyIDS)
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
               Delsql[0] = "DELETE FROM officedba.CarApply WHERE ID IN (" + allApplyID + ")";
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

/**********************************************
 * 类作用：   车辆派单数据库层处理
 * 建立人：   lysong
 * 建立时间： 2009/04/29
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
    /// <summary>
    /// 类名：CarDispatchDBHelper
    /// 描述：车辆派单数据库层处理
    /// 作者：lysong
    /// 创建时间：2009/04/29
    /// </summary>
   public class CarDispatchDBHelper
    {
       #region 获取请车单据号下拉列表(出去未归还的车辆)
        /// <summary>
        /// 获取请车单据号下拉列表
        /// </summary>
        /// <returns></returns>
       public static DataTable GetCarApplyNo(string CompanyCD, int EmployeeID)
        {
            string sql = "select a.ID,a.RecordNo from officedba.CarApply a where a.billstatus='2' "
                             + " and a.CompanyCD='"+CompanyCD+"' AND a.CarNo not in( "
                             + "select CarNo from officedba.CarDispatch where isreturn='0')";
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion
       #region 获取请车单据号下拉列表（出车和未出车的都包括）
       /// <summary>
       /// 获取请车单据号下拉列表
       /// </summary>
       /// <returns></returns>
       public static DataTable GetAllCarApplyNo(string CompanyCD)
       {
           string sql = "select a.ID,a.RecordNo "
                           + "from officedba.CarApply a "
                           + "LEFT OUTER JOIN  "
                           + "officedba.CarDispatch b "
                           + "ON a.ID=b.ApplyID WHERE a.CompanyCD='" + CompanyCD + "' AND a.BillStatus='2'";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 添加车辆派送信息
       /// <summary>
       /// 添加车辆派送信息
       /// </summary>
       /// <param name="CarDispatchM">车辆派送信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool InsertCarDispatchInfoData(CarDispatchModel CarDispatchM, out int RetValID)
       {
           try
           {
               #region 车辆申请信息SQL拼写
               StringBuilder sql = new StringBuilder();
               sql.AppendLine("INSERT INTO officedba.CarDispatch");
               sql.AppendLine("		(CompanyCD      ");
               sql.AppendLine("		,RecordNo        ");
               sql.AppendLine("		,Title        ");
               sql.AppendLine("		,ApplyID        ");
               sql.AppendLine("		,CarNo        ");
               sql.AppendLine("		,ApplyDate        ");
               sql.AppendLine("		,Appler        ");
               sql.AppendLine("		,ApplyDept        ");
               sql.AppendLine("		,Reason        ");
               sql.AppendLine("		,LoadHumans        ");
               sql.AppendLine("		,LoadGoods        ");
               sql.AppendLine("		,RequireDate        ");
               sql.AppendLine("		,RequireTime        ");
               sql.AppendLine("		,PlanReturnDate        ");
               sql.AppendLine("		,PlanReturnTime        ");
               sql.AppendLine("		,PlanMileage        ");
               sql.AppendLine("		,isReturn        ");
               sql.AppendLine("		,Creator        ");
               sql.AppendLine("		,CreateDate        ");
               sql.AppendLine("		,Remark        ");
               sql.AppendLine("		,ModifiedDate        ");
               sql.AppendLine("		,ModifiedUserID)        ");
               sql.AppendLine("VALUES                  ");
               sql.AppendLine("		(@CompanyCD      ");
               sql.AppendLine("		,@RecordNo        ");
               sql.AppendLine("		,@Title        ");
               sql.AppendLine("		,@ApplyID        ");
               sql.AppendLine("		,@CarNo        ");
               sql.AppendLine("		,@ApplyDate        ");
               sql.AppendLine("		,@Appler        ");
               sql.AppendLine("		,@ApplyDept        ");
               sql.AppendLine("		,@Reason        ");
               sql.AppendLine("		,@LoadHumans        ");
               sql.AppendLine("		,@LoadGoods        ");
               sql.AppendLine("		,@RequireDate        ");
               sql.AppendLine("		,@RequireTime        ");
               sql.AppendLine("		,@PlanReturnDate        ");
               sql.AppendLine("		,@PlanReturnTime        ");
               sql.AppendLine("		,@PlanMileage        ");
               sql.AppendLine("		,@isReturn        ");
               sql.AppendLine("		,@Creator        ");
               sql.AppendLine("		,@CreateDate        ");
               sql.AppendLine("		,@Remark        ");
               sql.AppendLine("		,@ModifiedDate        ");
               sql.AppendLine("		,@ModifiedUserID)        ");
               sql.AppendLine("set @ID=@@IDENTITY");

               #endregion
               #region 车辆申请信息参数设置
               SqlParameter[] param;
               param = new SqlParameter[23];
               param[0] = SqlHelper.GetParameter("@CompanyCD", CarDispatchM.CompanyCD);
               param[1] = SqlHelper.GetParameter("@RecordNo", CarDispatchM.RecordNo);
               param[2] = SqlHelper.GetParameter("@Title", CarDispatchM.Title);
               param[3] = SqlHelper.GetParameter("@ApplyID", CarDispatchM.ApplyID);
               param[4] = SqlHelper.GetParameter("@CarNo", CarDispatchM.CarNo);
               param[5] = SqlHelper.GetParameter("@ApplyDate", CarDispatchM.ApplyDate);
               param[6] = SqlHelper.GetParameter("@Appler", CarDispatchM.Appler);
               param[7] = SqlHelper.GetParameter("@Reason", CarDispatchM.Reason);
               param[8] = SqlHelper.GetParameter("@LoadHumans", CarDispatchM.LoadHumans);
               param[9] = SqlHelper.GetParameter("@LoadGoods", CarDispatchM.LoadGoods);
               param[10] = SqlHelper.GetParameter("@RequireDate", CarDispatchM.RequireDate);
               param[11] = SqlHelper.GetParameter("@RequireTime", CarDispatchM.RequireTime);
               param[12] = SqlHelper.GetParameter("@PlanReturnDate", CarDispatchM.PlanReturnDate);
               param[13] = SqlHelper.GetParameter("@PlanReturnTime", CarDispatchM.PlanReturnTime);
               param[14] = SqlHelper.GetParameter("@PlanMileage", CarDispatchM.PlanMileage);
               param[15] = SqlHelper.GetParameter("@isReturn", CarDispatchM.isReturn);
               param[16] = SqlHelper.GetParameter("@Creator", CarDispatchM.Creator);
               param[17] = SqlHelper.GetParameter("@CreateDate", CarDispatchM.CreateDate);
               param[18] = SqlHelper.GetParameter("@Remark", CarDispatchM.Remark);
               param[19] = SqlHelper.GetParameter("@ModifiedDate", CarDispatchM.ModifiedDate);
               param[20] = SqlHelper.GetParameter("@ModifiedUserID", CarDispatchM.ModifiedUserID);
               param[21] = SqlHelper.GetParameter("@ApplyDept", CarDispatchM.ApplyDept);
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
       #region 获取车辆派送数据
       /// <summary>
       /// 获取车辆派送数据
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetCarDispatchList(string CompanyID, string RecordNo, string DispatchTitle, string ApplyID, string CarNo, string CarMark, string IfReturn,int pageIndex,int pageCount,string ord, ref int TotalCount)
       {
           string sql = "select a.ID,a.RecordNo,a.Title,a.CarNo,CASE a.isReturn WHEN '0' THEN '否' WHEN '1' THEN '是' "
						 +"END isReturn,Isnull(a.Reason,'')Reason,"
                         + "convert(varchar(10),a.RequireDate,120)RequireDate,a.RequireTime,Convert(datetime,(a.RequireDate+a.RequireTime))RequireDateTime,"
                         + "convert(varchar(10),a.PlanReturnDate,120) PlanReturnDate,a.PlanReturnTime,Convert(datetime,(a.PlanReturnDate+a.PlanReturnTime))PlanReturnDateTime,"
						 +"Isnull(b.RecordNo,'直接派车') as ApplyNo,"
                         + "isnull(d.DeptName,'')DeptName,isnull(c.EmployeeName,'')EmployeeName "
                         +"from officedba.CarDispatch a "
                         +"LEFT OUTER JOIN officedba.CarApply b "
                         +"ON a.ApplyID=b.ID "
                         +"LEFT OUTER JOIN "
                         +"officedba.EmployeeInfo c "
                         +"ON a.Appler=c.ID "
						 +"LEFT OUTER JOIN officedba.DeptInfo d "
						 +"ON a.ApplyDept=d.ID "
                         +"INNER JOIN " 
                         +"officedba.CarInfo e "
                         + "ON a.CarNo=e.CarNo   and a.companycd=e.companycd "
                         + "WHERE a.CompanyCD='" + CompanyID + "'  ";
           if (RecordNo != "")
               sql += " and a.RecordNo LIKE '%" + RecordNo + "%'";
           if (DispatchTitle != "")
               sql += " and a.Title LIKE '%" + DispatchTitle + "%'";
           if (ApplyID != "")
               sql += " and a.ApplyID = " + ApplyID + "";
           if (CarNo != "")
               sql += " and e.CarNo = '" + CarNo + "'";
           if (CarMark != "")
               sql += " and e.CarMark LIKE '%" + CarMark + "%'";
           if (IfReturn != "")
               sql += " and a.isReturn ='" + IfReturn + "'";
           return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);

           //return SqlHelper.ExecuteSql(sql);
       }
       #endregion


       #region  根据派车ID获取派车信息
       /// <summary>
       /// 根据派车ID获取派车信息
       /// </summary>
       /// <param name="DispatchID">派车ID</param>
       /// <returns>DataTable</returns>
       public static DataTable GetCarDispatchByID(string DispatchID)
       {
           string sql = "select a.*,b.EmployeeName as ApplerName,d.DeptName,"
                         +"c.EmployeeName as CreatorName "
                         +"from officedba.CarDispatch a "
                         +"LEFT OUTER JOIN  "
                         +"officedba.EmployeeInfo b "
                         +"ON a.Appler=b.ID "
                         +"LEFT OUTER JOIN "
                         +"officedba.EmployeeInfo c "
                         +"ON a.Creator=c.ID "
						 +"LEFT OUTER JOIN officedba.DeptInfo d "
						 +"ON a.ApplyDept=d.ID "
                        + "WHERE a.ID=" + DispatchID + "";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion

       #region  根据派车ID获取派车信息_报表
       /// <summary>
       /// 根据派车ID获取派车信息_报表
       /// </summary>
       /// <param name="DispatchID">派车ID</param>
       /// <returns>DataTable</returns>
       public static DataTable GetCarDispatchByIDPrint(string DispatchID)
       {
           string sql = "SELECT     a.ID, a.CompanyCD, a.RecordNo, a.Title,"
                         + " (case when a.ApplyID=0 then ' 直接派车' else e.RecordNO end) ApplyID,  "
                         + " a.CarNo, a.ApplyDate, a.Appler, a.ApplyDept, a.Reason, a.LoadHumans, a.LoadGoods, "
                         + " a.RequireDate, a.RequireTime, a.PlanReturnDate, a.PlanReturnTime, a.PlanMileage, a.OutDate, a.OutTime, a.BackDate, a.BackTime,  "
                         + " (case when a.isReturn=1 then '结单' else '制单' end) isReturn, "
                         + " a.RealMileage, a.Creator, a.CreateDate, a.Remark, a.ModifiedDate, a.ModifiedUserID, b.EmployeeName AS ApplerName, d.DeptName,  "
                         + " c.EmployeeName AS CreatorName "
                         + " FROM  officedba.CarDispatch AS a LEFT OUTER JOIN "
                         + " officedba.EmployeeInfo AS b ON a.Appler = b.ID LEFT OUTER JOIN "
                         + " officedba.EmployeeInfo AS c ON a.Creator = c.ID LEFT OUTER JOIN "
                         + " officedba.DeptInfo AS d ON a.ApplyDept = d.ID Left Join "
                         + "officedba.CarApply as e on a.ApplyID=e.Id "
                        + " WHERE a.ID=" + DispatchID + "";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion

       #region 更新车辆归还信息
       /// <summary>
       /// 更新车辆归还信息
       /// </summary>
       /// <param name="CarDispatchM">车辆归还信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool UpdateCarDispatchInfoData(CarDispatchModel CarDispatchM, string DispatchID)
       {
           try
           {
               #region 派车单归还信息SQL拼写
               StringBuilder sql = new StringBuilder();
               sql.AppendLine("UPDATE officedba.CarDispatch");
               sql.AppendLine("		SET OutDate=@OutDate        ");
               sql.AppendLine("		,OutTime=@OutTime        ");
               sql.AppendLine("		,BackDate=@BackDate        ");
               sql.AppendLine("		,BackTime=@BackTime       ");
               sql.AppendLine("		,isReturn=@isReturn        ");
               sql.AppendLine("		,RealMileage=@RealMileage        ");
               sql.AppendLine("		,ModifiedDate=@ModifiedDate        ");
               sql.AppendLine("		,ModifiedUserID=@ModifiedUserID        ");
               sql.AppendLine("WHERE                  ");
               sql.AppendLine("		ID=@DispatchID   ");

               #endregion
               #region 派车单归还信息参数设置
               SqlParameter[] param;
               param = new SqlParameter[9];
               param[0] = SqlHelper.GetParameter("@OutDate", CarDispatchM.OutDate);
               param[1] = SqlHelper.GetParameter("@OutTime", CarDispatchM.OutTime);
               param[2] = SqlHelper.GetParameter("@BackDate", CarDispatchM.BackDate);
               param[3] = SqlHelper.GetParameter("@BackTime", CarDispatchM.BackTime);
               param[4] = SqlHelper.GetParameter("@isReturn", CarDispatchM.isReturn);
               param[5] = SqlHelper.GetParameter("@RealMileage", CarDispatchM.RealMileage);
               param[6] = SqlHelper.GetParameter("@ModifiedDate", CarDispatchM.ModifiedDate);
               param[7] = SqlHelper.GetParameter("@ModifiedUserID", CarDispatchM.ModifiedUserID);
               param[8] = SqlHelper.GetParameter("@DispatchID",Convert.ToInt32(DispatchID));

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

       #region 更新车派送还信息
       /// <summary>
       /// 更新车辆归还信息
       /// </summary>
       /// <param name="CarDispatchM">车辆归还信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool UpdateCarDispatchInfoData(CarDispatchModel CarDispatchM)
       {
           try
           {
               #region 派车单归还信息SQL拼写
               StringBuilder sql = new StringBuilder();
               sql.AppendLine("UPDATE officedba.CarDispatch");
               sql.AppendLine("		SET Title=@Title        ");
               sql.AppendLine("		,ApplyID=@ApplyID        ");
               sql.AppendLine("		,CarNo=@CarNo        ");
               sql.AppendLine("		,ApplyDate=@ApplyDate        ");
               sql.AppendLine("		,Appler=@Appler        ");
               sql.AppendLine("		,ApplyDept=@ApplyDept        ");
               sql.AppendLine("		,Reason=@Reason       ");
               sql.AppendLine("		,LoadHumans=@LoadHumans        ");
               sql.AppendLine("		,LoadGoods=@LoadGoods        ");
               sql.AppendLine("		,RequireDate=@RequireDate        ");
               sql.AppendLine("		,RequireTime=@RequireTime        ");
               sql.AppendLine("		,PlanReturnDate=@PlanReturnDate        ");
               sql.AppendLine("		,PlanReturnTime=@PlanReturnTime        ");
               sql.AppendLine("		,PlanMileage=@PlanMileage        ");
               sql.AppendLine("		,isReturn=@isReturn        ");
               sql.AppendLine("		,Creator=@Creator        ");
               sql.AppendLine("		,CreateDate=@CreateDate        ");
               sql.AppendLine("		,Remark=@Remark        ");
               sql.AppendLine("		,ModifiedDate=@ModifiedDate        ");
               sql.AppendLine("		,ModifiedUserID=@ModifiedUserID        ");
               sql.AppendLine("WHERE                  ");
               sql.AppendLine("		CompanyCD=@CompanyCD AND  RecordNo=@RecordNo  ");

               #endregion
               #region 派车单归还信息参数设置
               SqlParameter[] param;
               param = new SqlParameter[22];
               param[0] = SqlHelper.GetParameter("@Title", CarDispatchM.Title);
               param[1] = SqlHelper.GetParameter("@ApplyID", CarDispatchM.ApplyID);
               param[2] = SqlHelper.GetParameter("@CarNo", CarDispatchM.CarNo);
               param[3] = SqlHelper.GetParameter("@ApplyDate", CarDispatchM.ApplyDate);
               param[4] = SqlHelper.GetParameter("@Appler", CarDispatchM.Appler);
               param[5] = SqlHelper.GetParameter("@Reason", CarDispatchM.Reason);
               param[6] = SqlHelper.GetParameter("@LoadHumans", CarDispatchM.LoadHumans);
               param[7] = SqlHelper.GetParameter("@LoadGoods", CarDispatchM.LoadGoods);
               param[8] = SqlHelper.GetParameter("@RequireDate", CarDispatchM.RequireDate);
               param[9] = SqlHelper.GetParameter("@RequireTime", CarDispatchM.RequireTime);
               param[10] = SqlHelper.GetParameter("@PlanReturnDate", CarDispatchM.PlanReturnDate);
               param[11] = SqlHelper.GetParameter("@PlanReturnTime", CarDispatchM.PlanReturnTime);
               param[12] = SqlHelper.GetParameter("@PlanMileage", CarDispatchM.PlanMileage);
               param[13] = SqlHelper.GetParameter("@isReturn", CarDispatchM.isReturn);
               param[14] = SqlHelper.GetParameter("@Creator", CarDispatchM.Creator);
               param[15] = SqlHelper.GetParameter("@CreateDate", CarDispatchM.CreateDate);
               param[16] = SqlHelper.GetParameter("@Remark", CarDispatchM.Remark);
               param[17] = SqlHelper.GetParameter("@ModifiedDate", CarDispatchM.ModifiedDate);
               param[18] = SqlHelper.GetParameter("@ModifiedUserID", CarDispatchM.ModifiedUserID);
               param[19] = SqlHelper.GetParameter("@CompanyCD", CarDispatchM.CompanyCD);
               param[20] = SqlHelper.GetParameter("@RecordNo", CarDispatchM.RecordNo);
               param[21] = SqlHelper.GetParameter("@ApplyDept", CarDispatchM.ApplyDept);


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

       #region 删除派车单信息
       /// <summary>
       /// 删除派车单信息
       /// </summary>
       /// <param name="EquipmentIDS">设备IDS</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool DelCarDispatchByID(string CarDispatchIDS)
       {
           string allApplyID = "";
           System.Text.StringBuilder sb = new System.Text.StringBuilder();
           string[] Delsql = new string[1];
           try
           {
               string[] IDS = null;
               IDS = CarDispatchIDS.Split(',');

               for (int i = 0; i < IDS.Length; i++)
               {
                   IDS[i] = "'" + IDS[i] + "'";
                   sb.Append(IDS[i]);
               }

               allApplyID = sb.ToString().Replace("''", "','");
               Delsql[0] = "DELETE FROM officedba.CarDispatch WHERE ID IN (" + allApplyID + ")";
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

/**********************************************
 * 类作用：   办公用品数据库层处理
 * 建立人：   lysong
 * 建立时间： 2009/05/07
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
    /// 类名：OfficeThingsInfoDBHelper
    /// 描述：办公用品数据库层处理
    /// 作者：lysong
    /// 创建时间：2009/05/07
    /// </summary>
    public class OfficeThingsInfoDBHelper
    {
        #region 添加办公用品信息
        /// <summary>
        /// 添加办公用品信息
        /// </summary>
        /// <param name="OfficeThingsInfoM">办公用品信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool AddOfficeThingsInfo(OfficeThingsInfoModel OfficeThingsInfoM)
        {
            try
            {
                #region 添加设备报废SQL拼写
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO officedba.OfficeThingsInfo");
                sql.AppendLine("		(CompanyCD      ");
                sql.AppendLine("		,ThingNo        ");
                sql.AppendLine("		,ThingName        ");
                sql.AppendLine("		,TypeID        ");
                sql.AppendLine("		,ThingType        ");
                sql.AppendLine("		,UnitID        ");
                sql.AppendLine("		,MinCount        ");
                sql.AppendLine("		,Creator        ");
                sql.AppendLine("		,CreateDate        ");
                sql.AppendLine("		,Remark        ");
                sql.AppendLine("		,ModifiedDate        ");
                sql.AppendLine("		,ModifiedUserID)        ");
                sql.AppendLine("VALUES                  ");
                sql.AppendLine("		(@CompanyCD      ");
                sql.AppendLine("		,@ThingNo        ");
                sql.AppendLine("		,@ThingName        ");
                sql.AppendLine("		,@TypeID        ");
                sql.AppendLine("		,@ThingType        ");
                sql.AppendLine("		,@UnitID        ");
                sql.AppendLine("		,@MinCount        ");
                sql.AppendLine("		,@Creator        ");
                sql.AppendLine("		,@CreateDate        ");
                sql.AppendLine("		,@Remark        ");
                sql.AppendLine("		,@ModifiedDate        ");
                sql.AppendLine("		,@ModifiedUserID)        ");
                #endregion
                #region 添加设备报废参数设置
                SqlParameter[] param;
                param = new SqlParameter[12];
                param[0] = SqlHelper.GetParameter("@CompanyCD", OfficeThingsInfoM.CompanyCD);
                param[1] = SqlHelper.GetParameter("@ThingNo", OfficeThingsInfoM.ThingNo);
                param[2] = SqlHelper.GetParameter("@ThingName", OfficeThingsInfoM.ThingName);
                param[3] = SqlHelper.GetParameter("@TypeID", OfficeThingsInfoM.TypeID);
                param[4] = SqlHelper.GetParameter("@ThingType", OfficeThingsInfoM.ThingType);
                param[5] = SqlHelper.GetParameter("@UnitID", OfficeThingsInfoM.UnitID);
                param[6] = SqlHelper.GetParameter("@MinCount", OfficeThingsInfoM.MinCount);
                param[7] = SqlHelper.GetParameter("@Creator", OfficeThingsInfoM.Creator);
                param[8] = SqlHelper.GetParameter("@CreateDate", OfficeThingsInfoM.CreateDate);
                param[9] = SqlHelper.GetParameter("@Remark", OfficeThingsInfoM.Remark);
                param[10] = SqlHelper.GetParameter("@ModifiedDate", OfficeThingsInfoM.ModifiedDate);
                param[11] = SqlHelper.GetParameter("@ModifiedUserID", OfficeThingsInfoM.ModifiedUserID);
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

        #region 查询办公用品档案列表
        /// <summary>
        /// 查询办公用品档案列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetOfficeThingsInfoList(string ThingNo, string ThingName, string TypeIDHidden, string CompanyID, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string sql = "select a.ID,a.ThingNo,a.ThingName,a.ThingType,"
                          + "a.MinCount,convert(varchar(10),a.CreateDate,120)CreateDate,b.CodeName,c.TypeName,d.EmployeeName "
						  +"from officedba.OfficeThingsInfo a "
						  +"LEFT OUTER JOIN officedba.CodeEquipmentType b "
						  +"ON a.TypeID=b.ID and a.CompanyCD=b.CompanyCD "
						  +"LEFT OUTER JOIN officedba.CodePublicType c "
						  +"ON a.UnitID=c.ID and a.CompanyCD=c.CompanyCD "
						  +"LEFT OUTER JOIN "
						  +"officedba.EmployeeInfo d "
						  +"ON a.Creator=d.ID and a.CompanyCD=d.CompanyCD "
                          + " WHERE a.CompanyCD='" + CompanyID + "'  ";
            if (ThingNo != "")
                sql += " and a.ThingNo LIKE '%" + ThingNo + "%'";
            if (ThingName != "")
                sql += " and a.ThingName LIKE '%" + ThingName + "%'";
            if (TypeIDHidden != "")
                sql += " and a.TypeID = " + TypeIDHidden + "";
            return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            //return SqlHelper.ExecuteSql(sql);
        }
        #endregion
        #region 获取办公用品档案列表(只取库存中有的，供领用时用)
        /// <summary>
        /// 获取办公用品档案列表(只取库存中有的，供领用时用)
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetOfficeThingsInstorageInfoList(string ThingNo, string ThingName, string TypeIDHidden, string CompanyID)
        {
            string sql = "select a.ID,a.ThingNo,a.ThingName,a.ThingType,"
                          + "a.MinCount,convert(varchar(10),a.CreateDate,120)CreateDate,ISNULL(b.CodeName,'')CodeName,c.TypeName,ISNULL(d.EmployeeName,'')EmployeeName,f.SurPlusCount,f.UsedCount  "
                          + "from officedba.OfficeThingsInfo a "
                          + "LEFT OUTER JOIN officedba.CodeEquipmentType b "
                          + "ON a.TypeID=b.ID and a.CompanyCD=b.CompanyCD "
                          + "LEFT OUTER JOIN officedba.CodePublicType c "
                          + "ON a.UnitID=c.ID and a.CompanyCD=c.CompanyCD "
                          + "LEFT OUTER JOIN  officedba.EmployeeInfo  d "
                          + "ON a.Creator=d.ID and a.CompanyCD=d.CompanyCD "
                          +"inner join "
                          +"officedba.OfficeThingsStorage f "
                          +"on a.ThingNo=f.ThingNo and a.CompanyCD=f.CompanyCD "
                          + " WHERE a.CompanyCD='" + CompanyID + "'  ";
            if (ThingNo != "")
                sql += " and a.ThingNo LIKE '%" + ThingNo + "%'";
            if (ThingName != "")
                sql += " and a.ThingName LIKE '%" + ThingName + "%'";
            if (TypeIDHidden != "")
                sql += " and a.TypeID = " + TypeIDHidden + "";
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion

        #region  根据办公用品ID获取办公用品档案
        /// <summary>
        /// 根据办公用品ID获取办公用品档案
        /// </summary>
        /// <param name="ID">用品ID</param>
        /// <returns>DataTable</returns>
        public static DataTable GetOfficeThingsInfoById(string ID)
        {
            string sql = "select a.*,b.CodeName,c.TypeName,d.EmployeeName "
                          + "from officedba.OfficeThingsInfo a "
                          + "LEFT OUTER JOIN officedba.CodeEquipmentType b "
                          + "ON a.TypeID=b.ID "
                          + "LEFT OUTER JOIN officedba.CodePublicType c "
                          + "ON a.UnitID=c.ID "
                          + "LEFT OUTER JOIN  officedba.EmployeeInfo  d "
                          + "ON a.Creator=d.ID "
                          + " WHERE a.ID=" + ID + "  ";
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion

        #region  根据办公用品NO获取办公用品档案
        /// <summary>
        /// 根据办公用品NO获取办公用品档案
        /// </summary>
        /// <param name="ID">用品NO</param>
        /// <returns>DataTable</returns>
        public static DataTable GetOfficeThingsInfoByNO(string NO,string CompanyCD)
        {
            string sql = "select a.*,b.CodeName,c.TypeName,d.EmployeeName "
                          + "from officedba.OfficeThingsInfo a "
                          + "LEFT OUTER JOIN officedba.CodeEquipmentType b "
                          + "ON a.TypeID=b.ID "
                          + "LEFT OUTER JOIN officedba.CodePublicType c "
                          + "ON a.UnitID=c.ID "
                          + "LEFT OUTER JOIN  officedba.EmployeeInfo  d "
                          + "ON a.Creator=d.ID "
                          + " where a.CompanyCD='" + CompanyCD + "' and a.ThingNO='" + NO + "'";
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion


        #region 修改用品档案信息
        /// <summary>
        /// 修改用品档案信息
        /// </summary>
        /// <param name="OfficeThingsInfoM">用品档案信息</param>
        /// <returns>更新是否成功 false:失败，true:成功</returns>
        public static bool UpdateOfficeThingsInfo(OfficeThingsInfoModel OfficeThingsInfoM)        
        {
            try
            {
                #region 添加设备报废SQL拼写
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE officedba.OfficeThingsInfo");
                sql.AppendLine("		SET ThingName=@ThingName        ");
                sql.AppendLine("		,TypeID=@TypeID        ");
                sql.AppendLine("		,ThingType=@ThingType        ");
                sql.AppendLine("		,UnitID=@UnitID        ");
                sql.AppendLine("		,MinCount=@MinCount        ");
                sql.AppendLine("		,Remark=@Remark        ");
                sql.AppendLine("		,ModifiedDate=@ModifiedDate        ");
                sql.AppendLine("		,ModifiedUserID=@ModifiedUserID        ");
                sql.AppendLine("WHERE CompanyCD=@CompanyCD AND ThingNo=@ThingNo         ");
                #endregion
                #region 添加设备报废参数设置
                SqlParameter[] param;
                param = new SqlParameter[10];
                param[0] = SqlHelper.GetParameter("@ThingName", OfficeThingsInfoM.ThingName);
                param[1] = SqlHelper.GetParameter("@TypeID", OfficeThingsInfoM.TypeID);
                param[2] = SqlHelper.GetParameter("@ThingType", OfficeThingsInfoM.ThingType);
                param[3] = SqlHelper.GetParameter("@UnitID", OfficeThingsInfoM.UnitID);
                param[4] = SqlHelper.GetParameter("@MinCount", OfficeThingsInfoM.MinCount);
                param[5] = SqlHelper.GetParameter("@Remark", OfficeThingsInfoM.Remark);
                param[6] = SqlHelper.GetParameter("@ModifiedDate", OfficeThingsInfoM.ModifiedDate);
                param[7] = SqlHelper.GetParameter("@ModifiedUserID", OfficeThingsInfoM.ModifiedUserID);
                param[8] = SqlHelper.GetParameter("@CompanyCD", OfficeThingsInfoM.CompanyCD);
                param[9] = SqlHelper.GetParameter("@ThingNo", OfficeThingsInfoM.ThingNo);

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
        #region 用品库存查询
        /// <summary>
        /// 用品库存查询
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable SearchInstorageInfoList(string ThingName, string TypeIDHidden, string CompanyID,int pageIndex,int pageCount,string ord, ref int TotalCount)
        {
            string sql = "select b.ID,a.ThingNo,a.TotalCount,a.SurplusCount,b.MinCount,(b.MinCount-a.SurplusCount) as AlarmCount,d.ThingName,d.ThingType,d.CodeName,d.TypeName "
                            +"from officedba.OfficeThingsStorage a "
                            +"inner join "
                            +"officedba.OfficeThingsInfo b "
                            +"on a.ThingNo=b.ThingNo and a.CompanyCD=b.CompanyCD "
                            +"inner join  "
                            + "(select a.ID,a.ThingNo,a.CompanyCD,a.ThingName,a.ThingType,b.CodeName,c.TypeName "
                            +"from officedba.OfficeThingsInfo a "
                            +"LEFT OUTER JOIN officedba.CodeEquipmentType b "
                            + "ON a.CompanyCD='" + CompanyID + "' and a.TypeID=b.ID and a.CompanyCD=b.CompanyCD LEFT OUTER JOIN officedba.CodePublicType c  "
                            +" ON a.UnitID=c.ID and a.CompanyCD=c.CompanyCD) d "
                            +"on a.CompanyCD=d.CompanyCD and a.ThingNo=d.ThingNo "
                            + " WHERE a.CompanyCD='" + CompanyID + "'  ";
            if (ThingName != "")
                sql += " and b.ThingName LIKE '%" + ThingName + "%'";
            if (TypeIDHidden != "")
                sql += " and b.TypeID = " + TypeIDHidden + "";
            return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            //return SqlHelper.ExecuteSql(sql);
        }
        #endregion
        #region 用品预警查询
        /// <summary>
        /// 用品库存查询
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable SearchAlarmInfoList(string ThingName, string TypeIDHidden, string CompanyID,int pageIndex,int pageCount,string ord, ref int TotalCount)
        {
            string sql = "select b.ID,a.ThingNo,a.TotalCount,a.SurplusCount,b.MinCount,(b.MinCount-a.SurplusCount) as AlarmCount,d.ThingName,d.ThingType,d.CodeName,d.TypeName "
                            +"from officedba.OfficeThingsStorage a "
                            +"inner join "
                            +"officedba.OfficeThingsInfo b "
                            +"on a.ThingNo=b.ThingNo and a.CompanyCD=b.CompanyCD "
                            +"inner join  "
                            +"(select a.ID,a.ThingNo,a.CompanyCD,a.ThingName,a.ThingType,b.CodeName,c.TypeName "
                            +"from officedba.OfficeThingsInfo a "
                            + " LEFT OUTER JOIN officedba.CodeEquipmentType b ON a.TypeID=b.ID "
                            +"and a.CompanyCD=b.CompanyCD LEFT OUTER JOIN officedba.CodePublicType c  "
                            +"ON a.UnitID=c.ID and a.CompanyCD=c.CompanyCD) d "
                            + "on a.CompanyCD='" + CompanyID + "' and a.ThingNo=d.ThingNo and a.CompanyCD=d.CompanyCD"
                            + " WHERE a.CompanyCD='" + CompanyID + "' AND a.SurplusCount<=b.MinCount ";
            if (ThingName != "")
                sql += " and b.ThingName LIKE '%" + ThingName + "%'";
            if (TypeIDHidden != "")
                sql += " and b.TypeID = " + TypeIDHidden + "";
            //return SqlHelper.ExecuteSql(sql);
            return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);

        }
        #endregion

        #region 判断能否删除用品档案信息
        /// <summary>
        /// 判断能否删除用品档案信息
        /// </summary>
        /// <param name="EmployeeID">员工ID</param>
        /// <param name="CompanyID">公司代码</param>
        /// <returns></returns>
        public static bool IfDeleteOfficeThingsInfo(string OfficethingsNos, string CompanyID)
        {
            string[] NOS = null;
            NOS = OfficethingsNos.Split(',');
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
        #region 判断能否删除用品档案信息
        /// <summary>
        /// 判断能否删除用品档案信息
        /// </summary>
        /// <param name="EmployeeID">员工ID</param>
        /// <param name="CompanyID">公司代码</param>
        /// <returns></returns>
        public static bool IsExistInfo(string ThingNo, string CompanyID)
        {

            string sql = "SELECT * FROM officedba.OfficeThingsStorage WHERE ThingNo='" + ThingNo + "' AND CompanyCD='" + CompanyID + "'";
            return SqlHelper.ExecuteSql(sql).Rows.Count > 0 ? true : false;
        }
        #endregion
        #region 删除用品档案信息
        /// <summary>
        /// 判断能否删除用品档案信息
        /// </summary>
        /// <param name="EquipmentIDS">设备IDS</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
        public static bool DeleteOfficeThingsInfo(string ApplyIDS, string CompanyID)
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
                Delsql[0] = "DELETE FROM officedba.OfficeThingsInfo WHERE ThingNo IN (" + allApplyID + ") AND CompanyCD='"+CompanyID+"'";
                SqlHelper.ExecuteTransForListWithSQL(Delsql);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch 
            {
                return false;
            }
        }
        #endregion

        #region 获取用品购买信息列表
        /// <summary>
        /// 获取用品购买信息列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetOfficeThingsBuySumInfo(string StartDate, string EndDate, string ThingName,string TypeID, string CompanyID, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string sql = "SELECT distinct b.ID,a.ThingNo,b.ThingName,a.BuyCount,a.BuyMoney,b.CodeName,c.UsedCount,c.SurplusCount "
                            +"FROM (SELECT ThingNo,CompanyCD,SUM(BuyCount)BuyCount,SUM(BuyMoney)BuyMoney "
                            +"FROM officedba.OfficeThingsBuyDetail GROUP BY ThingNo,CompanyCD) a "
                            +"LEFT OUTER JOIN  "
                            +"("
                            + "SELECT a.ID,a.ThingNo,a.CompanyCD,a.ThingName,b.ID as ID1,b.CodeName "
                            +"FROM officedba.OfficeThingsInfo a "
                            +"LEFT OUTER JOIN officedba.CodeEquipmentType b "
                            +"ON a.TypeID=b.ID AND a.CompanyCD=b.CompanyCD "
                            +") b "
                            +"ON a.ThingNo=b.ThingNo AND a.CompanyCD=b.CompanyCD "
                            +"LEFT OUTER JOIN officedba.OfficeThingsStorage c "
                            +"ON a.CompanyCD=c.CompanyCD AND a.ThingNo=c.ThingNo "
                            +"LEFT OUTER JOIN "
                            +"(select a.CompanyCD,a.BuyDate,b.ThingNo from officedba.OfficeThingsBuy a "
                            +"LEFT OUTER JOIN   officedba.OfficeThingsBuyDetail b "
                            +"ON a.BuyRecordNo=b.BuyRecordNo AND a.CompanyCD=b.CompanyCD) d "
                            +"ON a.CompanyCD=d.CompanyCD AND a.ThingNo=d.ThingNo"
                            + " where a.CompanyCD='" + CompanyID + "'";
            if (ThingName != "")
                sql += " and b.ThingName like '%" + ThingName + "%' ";
            if (StartDate != "")
                sql += " and d.BuyDate>='" + StartDate + "'";
            if (EndDate != "")
                sql += " and d.BuyDate<='" + EndDate + "'";
            if (TypeID != "")
                sql += " and b.ID1=" + TypeID + "";
            return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
        }
        #endregion
        #region 获取用品购买信息列表打印
        /// <summary>
        /// 获取用品购买信息列表打印
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetOfficeThingsBuySumInfoPrint(string StartDate, string EndDate, string ThingName, string TypeID, string CompanyID, string ord)
        {
            string sql = "SELECT distinct b.ID,a.ThingNo,b.ThingName,a.BuyCount,a.BuyMoney,b.CodeName,c.UsedCount,c.SurplusCount "
                            + "FROM (SELECT ThingNo,CompanyCD,SUM(BuyCount)BuyCount,SUM(BuyMoney)BuyMoney "
                            + "FROM officedba.OfficeThingsBuyDetail GROUP BY ThingNo,CompanyCD) a "
                            + "LEFT OUTER JOIN  "
                            + "("
                            + "SELECT a.ID,a.ThingNo,a.CompanyCD,a.ThingName,b.ID as ID1,b.CodeName "
                            + "FROM officedba.OfficeThingsInfo a "
                            + "LEFT OUTER JOIN officedba.CodeEquipmentType b "
                            + "ON a.TypeID=b.ID AND a.CompanyCD=b.CompanyCD "
                            + ") b "
                            + "ON a.ThingNo=b.ThingNo AND a.CompanyCD=b.CompanyCD "
                            + "LEFT OUTER JOIN officedba.OfficeThingsStorage c "
                            + "ON a.CompanyCD=c.CompanyCD AND a.ThingNo=c.ThingNo "
                            + "LEFT OUTER JOIN "
                            + "(select a.CompanyCD,a.BuyDate,b.ThingNo from officedba.OfficeThingsBuy a "
                            + "LEFT OUTER JOIN   officedba.OfficeThingsBuyDetail b "
                            + "ON a.BuyRecordNo=b.BuyRecordNo AND a.CompanyCD=b.CompanyCD) d "
                            + "ON a.CompanyCD=d.CompanyCD AND a.ThingNo=d.ThingNo"
                            + " where a.CompanyCD='" + CompanyID + "'";
            if (ThingName != "")
                sql += " and b.ThingName like '%" + ThingName + "%' ";
            if (StartDate != "")
                sql += " and d.BuyDate>='" + StartDate + "'";
            if (EndDate != "")
                sql += " and d.BuyDate<='" + EndDate + "'";
            if (TypeID != "")
                sql += " and b.ID1=" + TypeID + "";
            sql = sql + ord;
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion

        #region 新报表
        public static DataTable ThingByDetails(string BeginDate, string EndDate, string TypeID,string ThingNO, string CompanyCD,int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select c.ThingNO,c.ThingName,b.BuyCount,b.BuyMoney,d.CodeName  from officedba.OfficeThingsBuyDetail b left join officedba.OfficeThingsBuy a ");
            sql.AppendLine(" on a.BuyRecordNo=b.BuyRecordNo AND a.CompanyCD=b.CompanyCD left join officedba.OfficeThingsInfo c ");
            sql.AppendLine(" on b.ThingNO=c.ThingNO and b.CompanyCD=c.CompanyCD left join  officedba.CodeEquipmentType d ");
            sql.AppendLine(" on c.TypeID=d.ID AND c.CompanyCD=d.CompanyCD ");
            sql.AppendLine(" where 1=1  and a.BillStatus<>'1'  ");
            if (CompanyCD != "")
            {
                sql.Append(" and b.CompanyCD='");
                sql.Append(CompanyCD);
                sql.Append("' ");
            }
            if (BeginDate != "")
            {
                sql.Append(" and a.BuyDate>Convert(datetime,'");
                sql.Append(BeginDate);
                sql.Append("')");

            }
            if (EndDate != "")
            {
                sql.Append(" and a.BuyDate< DATEADD(day, 1,Convert(datetime,'");
                sql.Append(EndDate);
                sql.Append("'))");
            }
            if (TypeID != "")
            {
                sql.Append(" and c.TypeID=");
                sql.Append(TypeID);
            }
            if (ThingNO!="")
            {
                sql.Append(" and c.ThingNO='");
                sql.Append(ThingNO);
                sql.Append("' ");
            }
            return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
        }

        public static DataTable ThingByDetails1(string BeginDate, string EndDate, string DeptID, string TypeID, string CompanyCD, string DateType, string DateValue, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select f.EmployeeName,c.ThingNO,c.ThingName,e.CodeName,d.DeptName,b.[Count] UsedCount,a.UsedDate,a.DeptID, ");
            sql.AppendLine("dateName(year,a.UsedDate)+'年' as BackYear,");
            sql.AppendLine("dateName(year,a.UsedDate)+'年-'+dateName(month,a.UsedDate)+'月' as BackMonth,");
            sql.AppendLine("dateName(year,a.UsedDate)+'年-'+dateName(week,a.UsedDate)+'周' as BackWeek ");
            sql.AppendLine(" from  officedba.OfficeThingsUsedDetail b left join officedba.OfficeThingsUsed a ");
            sql.AppendLine(" on b.ApplyNO=a.ApplyNO and a.CompanyCD=b.CompanyCD left join officedba.OfficeThingsInfo c  ");
            sql.AppendLine(" on b.ThingNO=c.ThingNO and b.CompanyCD=c.CompanyCD left join officedba.DeptInfo d  ");
            sql.AppendLine(" on a.DeptID=d.ID  and a.CompanyCD=d.CompanyCD left join  officedba.CodeEquipmentType e  ");
            sql.AppendLine(" on c.TypeID=e.ID AND c.CompanyCD=e.CompanyCD left join officedba.EmployeeInfo f ");
            sql.AppendLine(" on a.EmployeeID=f.ID ");
            sql.Append(" where 1=1  and a.BillStatus<>'1' and   b.companyCD='");
            sql.Append(CompanyCD);
            sql.Append("' ");

            if (DeptID != "")
            {
                sql.Append(" and a.DeptID=");
                sql.Append(DeptID);
            }
            if (TypeID != "")
            {
                sql.Append(" and c.TypeID=");
                sql.Append(TypeID);
            }

            if (BeginDate != "")
            {
                sql.Append(" and a.UsedDate>Convert(datetime,'");
                sql.Append(BeginDate);
                sql.Append("')");

            }
            if (EndDate != "")
            {
                sql.Append(" and a.UsedDate< DATEADD(day, 1,Convert(datetime,'");
                sql.Append(EndDate);
                sql.Append("'))");
            }
            if (DateValue != "")
            {

                if (DateType == "1")
                {
                    sql.Append("and (dateName(year,a.UsedDate)+'年')='");
                    sql.Append(DateValue);
                    sql.Append("'");
                }
                else if (DateType == "2")
                {
                    sql.Append("and (dateName(year,a.UsedDate)+'年-'+dateName(month,a.UsedDate)+'月')='");
                    sql.Append(DateValue);
                    sql.Append("'");
                }
                else
                {
                    sql.Append("and (dateName(year,a.UsedDate)+'年-'+dateName(week,a.UsedDate)+'周')='");
                    sql.Append(DateValue);
                    sql.Append("'");
                }
            }
            return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
        }

        public static DataTable ThingsByBuy(string BeginDate, string EndDate, string TypeID, string CompanyCD)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select min(c.ThingName)name, SUM(BuyCount)counts,SUM(BuyMoney)counts1,b.ThingNO from officedba.OfficeThingsBuyDetail b left join officedba.OfficeThingsBuy a ");
            sql.AppendLine(" on a.BuyRecordNo=b.BuyRecordNo AND a.CompanyCD=b.CompanyCD left join officedba.OfficeThingsInfo c ");
            sql.AppendLine(" on b.ThingNO=c.ThingNO and b.CompanyCD=c.CompanyCD where 1=1 and a.BillStatus<>'1' ");
            if (CompanyCD != "")
            {
                sql.Append(" and b.CompanyCD='");
                sql.Append(CompanyCD);
                sql.Append("' ");
            }
            if (BeginDate != "")
            {
                sql.Append(" and a.BuyDate>Convert(datetime,'");
                sql.Append(BeginDate);
                sql.Append("')");

            }
            if (EndDate != "")
            {
                sql.Append(" and a.BuyDate< DATEADD(day, 1,Convert(datetime,'");
                sql.Append(EndDate);
                sql.Append("'))");
            }
            if (TypeID != "")
            {
                sql.Append(" and c.TypeID=");
                sql.Append(TypeID);
            }
            sql.Append(" group by b.ThingNO ");
            return SqlHelper.ExecuteSql(sql.ToString());
        }
        public static DataTable ThingsByDept(string BeginDate, string EndDate, string TypeID, string CompanyCD)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select min(d.DeptName) name,sum(b.Count) counts,a.deptID from officedba.OfficeThingsUsedDetail b left join officedba.OfficeThingsUsed a ");
            sql.AppendLine(" on b.ApplyNO=a.ApplyNO and a.CompanyCD=b.CompanyCD left join officedba.OfficeThingsInfo c ");
            sql.AppendLine(" on b.ThingNO=c.ThingNO and b.CompanyCD=c.CompanyCD left join officedba.DeptInfo d ");
            sql.AppendLine(" on a.DeptID=d.ID  and a.CompanyCD=d.CompanyCD where 1=1  and a.BillStatus<>'1' ");
            if (CompanyCD != "")
            {
                sql.Append(" and b.CompanyCD='");
                sql.Append(CompanyCD);
                sql.Append("' ");
            }
            if (BeginDate != "")
            {
                sql.Append(" and a.UsedDate>Convert(datetime,'");
                sql.Append(BeginDate);
                sql.Append("')");

            }
            if (EndDate != "")
            {
                sql.Append(" and a.UsedDate< DATEADD(day, 1,Convert(datetime,'");
                sql.Append(EndDate);
                sql.Append("'))");
            }
            if (TypeID != "")
            {
                sql.Append(" and c.TypeID=");
                sql.Append(TypeID);
            }

            sql.AppendLine("  group by a.DeptID ");
            return SqlHelper.ExecuteSql(sql.ToString());
        }
        public static DataTable ThingsByTrend(string BeginDate, string EndDate, string DeptID, string TypeID,string DateType,string CompanyCD)
        {
            string SearchField = "";
            if (DateType == "1")
            {
                SearchField = "dateName(year,a.UsedDate)+'年'";
            }
            else if (DateType == "2")
            {
                SearchField = "dateName(year,a.UsedDate)+'年-'+dateName(month,a.UsedDate)+'月'";
            }
            else
            {
                SearchField = "dateName(year,a.UsedDate)+'年-'+dateName(week,a.UsedDate)+'周'";
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(" select ");
            sb.Append(SearchField);
            sb.Append(" as Name,sum(b.Count) counts  ");
            sb.Append(" from officedba.OfficeThingsUsedDetail b left join officedba.OfficeThingsUsed a ");
            sb.Append(" on b.ApplyNO=a.ApplyNO and a.CompanyCD=b.CompanyCD left join officedba.OfficeThingsInfo c ");
            sb.Append(" on b.ThingNO=c.ThingNO and b.CompanyCD=c.CompanyCD left join officedba.DeptInfo d ");
            sb.Append(" on a.DeptID=d.ID  and a.CompanyCD=d.CompanyCD ");
            sb.Append(" where 1=1 and a.BillStatus<>'1' and  b.companyCD='");
            sb.Append(CompanyCD);
            sb.Append("' ");

            if (DeptID != "")
            {
                sb.Append(" and a.DeptID=");
                sb.Append(DeptID);
            }
            if (TypeID != "")
            {
                sb.Append(" and c.TypeID=");
                sb.Append(TypeID);
            }

            if (BeginDate != "")
            {
                sb.Append(" and a.UsedDate>Convert(datetime,'");
                sb.Append(BeginDate);
                sb.Append("')");

            }
            if (EndDate != "")
            {
                sb.Append(" and a.UsedDate< DATEADD(day, 1,Convert(datetime,'");
                sb.Append(EndDate);
                sb.Append("'))");
            }
            sb.Append("  group by  ");
            sb.Append(SearchField);

            return SqlHelper.ExecuteSql(sb.ToString());
        }

        #endregion
    }
}

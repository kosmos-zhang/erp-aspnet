/**********************************************
 * 类作用：   设备添加数据库层处理
 * 建立人：   lysong
 * 建立时间： 2009/02/26
 ***********************************************/
using System;
using XBase.Model.Office.AdminManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using XBase.Common;



namespace XBase.Data.Office.AdminManager
{
    /// <summary>
    /// 类名：EquipMentInfoDBHelper
    /// 描述：设备添加数据库层处理
    /// 
    /// 作者：lysong
    /// 创建时间：2009/02/26
    /// </summary>
   public class EquipMentInfoDBHelper
   {
       #region 获取所有设备信息列表
       /// <summary>
        /// 获取所有设备信息列表
        /// </summary>
        /// <returns>DataTable</returns>
       public static DataTable GetEquipmentTable()
        {
            string sql = "select * from officedba.EquipmentInfo";
            return SqlHelper.ExecuteSql(sql);
        }
       #endregion

       #region 查询设备明细信息列表
       /// <summary>
       /// 查询设备明细信息列表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetEquipmentTableBycondition(EquipMnetInfoModel equip_M,int pageIndex,int pageCount,string ord, ref int TotalCount)
       {
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           string sql = "select a.ID,a.EquipmentNo,a.EquipmentIndex,a.CompanyCD,Convert(numeric(18,"+userInfo.SelPoint+"),[Money]) as moneys,"
           + "a.EquipmentName,a.Norm,a.Precision,a.BuyDate,a.Provider,a.Type,a.Warranty,"
           + "a.ExaminePeriod,a.CurrentUser,a.CurrentDepartment,isnull(a.Unit,'')Unit,"
           + "a.EquipmentDetail,"
           + "case FittingFlag when 0 then '无' when 1 then '有' else '其他' end FittingFlag, "
           + "case (a.Status) when 0 then '空闲' when 1 then '使用' when 3 then '维修'  when 5 then '报废' end Status,b.CodeName,"
           + "isnull(c.EmployeeName,'')EmployeeName,isnull(d.DeptName,'')DeptName "
           + " from officedba.EquipmentInfo a inner join officedba.CodeEquipmentType b on a.Type=b.ID "
           + "LEFT OUTER JOIN officedba.EmployeeInfo c "
           + "ON a.CurrentUser=c.ID  and a.CompanyCD=c.CompanyCD "
           + "LEFT OUTER JOIN "
           + "officedba.DeptInfo d "
           + "ON a.CurrentDepartment=d.ID and a.CompanyCD=d.CompanyCD "
           +" where a.CompanyCD='"+equip_M.CompanyCD+"'";
           if (equip_M.EquipmentNo != "")
               sql += " and EquipmentNo like '%"+equip_M.EquipmentNo+"%'";
           if (equip_M.EquipmentName != "")
               sql += " and EquipmentName like '%"+equip_M.EquipmentName+"%'";
           if (equip_M.EquipmentIndex != "")
               sql += " and EquipmentIndex like '%" + equip_M.EquipmentIndex + "%'";
           if (equip_M.Provider != "")
               sql += " and Provider like '%"+equip_M.Provider+"%'";
           if (equip_M.Type != 0)
               sql += " and [Type]=" + equip_M.Type + "";
           if (equip_M.BuyDate.ToString() != "" && equip_M.BuyDate.ToString() != "1900-1-1 0:00:00")
               sql += " and BuyDate>='" + equip_M.BuyDate + "'";
           if (equip_M.CurrentDepartment!= 0)
               sql += " and CurrentDepartment=" + equip_M.CurrentDepartment + "";
           if (equip_M.CurrentUser!= 0)
               sql += " and CurrentUser=" + equip_M.CurrentUser + "";
           if (equip_M.Status.ToString() != "")
               sql += " and Status='" + equip_M.Status + "'";
           return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);

           //return SqlHelper.ExecuteSql(sql);
       }

       #endregion

       #region 查询设备明细信息列表(金额合计)
       /// <summary>
       /// 查询设备明细信息列表(金额合计)
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetEquipmentTotalTableBycondition(EquipMnetInfoModel equip_M)
       {
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           StringBuilder searchSql = new StringBuilder();
           searchSql.AppendLine("select Convert(numeric(18,"+userInfo.SelPoint+"),sum(money)) as TotalMoney ");
           searchSql.AppendLine(" from officedba.EquipmentInfo a inner join officedba.CodeEquipmentType b on a.Type=b.ID ");
           searchSql.AppendLine("LEFT OUTER JOIN officedba.EmployeeInfo c ");
           searchSql.AppendLine("ON a.CurrentUser=c.ID  and a.CompanyCD=c.CompanyCD ");
           searchSql.AppendLine("LEFT OUTER JOIN ");
           searchSql.AppendLine("officedba.DeptInfo d ");
           searchSql.AppendLine("ON a.CurrentDepartment=d.ID and a.CompanyCD=d.CompanyCD ");
           searchSql.AppendLine(" where a.CompanyCD=@CompanyCD");

           SqlCommand comm = new SqlCommand();
           if (equip_M.EquipmentNo != "")
           {
               searchSql.AppendLine(" and EquipmentNo like @EquipmentNo");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@EquipmentNo", "%" + equip_M.EquipmentNo + "%"));
           }
           if (equip_M.EquipmentName != "")
           {
               searchSql.AppendLine(" and EquipmentName like @EquipmentName");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@EquipmentName", "%" + equip_M.EquipmentName + "%"));
           }
           if (equip_M.EquipmentIndex != "")
           {
               searchSql.AppendLine(" and EquipmentIndex like @EquipmentIndex");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@EquipmentIndex", "%" + equip_M.EquipmentIndex + "%"));
           }
           if (equip_M.Provider != "")
           {
               searchSql.AppendLine(" and Provider like @Provider");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@Provider", "%" + equip_M.Provider + "%"));
           }
           if (equip_M.Type != 0)
           {
               searchSql.AppendLine(" and [Type]=@types");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@types", equip_M.Type.ToString()));
           }
           if (equip_M.BuyDate.ToString() != "" && equip_M.BuyDate.ToString() != "1900-1-1 0:00:00")
           {
               searchSql.AppendLine(" and BuyDate>=@BuyDate");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@BuyDate", equip_M.BuyDate.ToString()));
           }
           if (equip_M.CurrentDepartment != 0)
           {
               searchSql.AppendLine(" and CurrentDepartment>=@CurrentDepartment");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@CurrentDepartment", equip_M.CurrentDepartment.ToString()));
           }
           if (equip_M.CurrentUser != 0)
           {
               searchSql.AppendLine(" and CurrentUser>=@CurrentUser");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@CurrentUser", equip_M.CurrentUser.ToString()));
           }
           if (equip_M.Status.ToString() != "")
           {
               searchSql.AppendLine(" and Status>=@Status");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@Status", equip_M.Status));
           }


           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", equip_M.CompanyCD));

           //指定命令的SQL文

           comm.CommandText = searchSql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);
       }
       #endregion

       #region 根据flag获取不同申请下需要显示的设备
       /// <summary>
       /// 根据flag获取不同申请下需要显示的设备
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetEquipmentTableBycondition(EquipMnetInfoModel equip_M, int pageIndex, int pageCount, string ord, ref int TotalCount,string flag)
       {

           string sql = "select a.ID,a.EquipmentNo,a.EquipmentIndex,a.CompanyCD,"
                           + "a.EquipmentName,a.Norm,a.Precision,a.BuyDate,a.Provider,a.Type,a.Warranty,"
                           + "a.ExaminePeriod,a.CurrentUser,a.CurrentDepartment,isnull(a.Unit,'')Unit,"
                           + "a.EquipmentDetail,"
                           + "case FittingFlag when 0 then '无' when 1 then '有' else '其他' end FittingFlag, "
                           + "case (a.Status) when 0 then '空闲' when 1 then '使用' when 3 then '维修'  when 5 then '报废' end Status,b.CodeName,"
                           + "isnull(c.EmployeeName,'')EmployeeName,isnull(d.DeptName,'')DeptName "
                           + " from officedba.EquipmentInfo a inner join officedba.CodeEquipmentType b on a.Type=b.ID "
                           + "LEFT OUTER JOIN officedba.EmployeeInfo c "
                           + "ON a.CurrentUser=c.ID  "
                           + "LEFT OUTER JOIN "
                           + "officedba.DeptInfo d "
                           + "ON a.CurrentDepartment=d.ID "
                           + " where a.CompanyCD='" + equip_M.CompanyCD + "'";
           if (flag == "Repair")
               sql += " and (a.Status= '0' or a.Status='1') ";
           if (flag == "Receive")
               sql += " and (a.Status= '0') ";
           if (flag == "Useless")
               sql += " and (a.Status= '0' or a.Status='1' or a.Status='3') ";
           if (equip_M.EquipmentNo != "")
               sql += " and EquipmentNo like '%" + equip_M.EquipmentNo + "%'";
           if (equip_M.EquipmentName != "")
               sql += " and EquipmentName like '%" + equip_M.EquipmentName + "%'";
           if (equip_M.EquipmentIndex != "")
               sql += " and EquipmentIndex like '%" + equip_M.EquipmentIndex + "%'";
           if (equip_M.Provider != "")
               sql += " and Provider like '%" + equip_M.Provider + "%'";
           if (equip_M.Type != 0)
               sql += " and [Type]=" + equip_M.Type + "";
           if (equip_M.BuyDate.ToString() != "" && equip_M.BuyDate.ToString() != "1900-1-1 0:00:00")
               sql += " and BuyDate>='" + equip_M.BuyDate + "'";
           if (equip_M.CurrentDepartment != 0)
               sql += " and CurrentDepartment=" + equip_M.CurrentDepartment + "";
           if (equip_M.CurrentUser != 0)
               sql += " and CurrentUser=" + equip_M.CurrentUser + "";
           if (equip_M.Status.ToString() != "")
               sql += " and Status='" + equip_M.Status + "'";
           return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);

           //return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 查询设备汇总信息列表
       /// <summary>
       /// 查询设备汇总信息列表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetEquipmentTotalTableBycondition(EquipMnetInfoModel equip_M,string EquipPre,int EpLength,int pageIndex,int pageCount,string ord, ref int TotalCount)
       {
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           EpLength = EpLength+1;
           string sql = "select sum(TotalCount)TotalCount,Status,CodeName,sum(FreeCount)FreeCount,sum(WarningLimit)WarningLimit,Type,Convert(numeric(20,"+userInfo.SelPoint+"),sum(moneys)) as moneys  "
                        + "from (select CompanyCD,isnull(m.TotalCount,'') TotalCount,isnull(m.Type,'') Type,isnull(m.status,'') Status,"
                         + "isnull(m.CodeName,'') CodeName,isnull(m.WarningLimit,'') WarningLimit,"
                         + "isnull(n.FreeCount,'') FreeCount,moneys "
                         + " from (select a.*,b.CodeName,b.WarningLimit from (select CompanyCD,count(*)  as TotalCount ,[Type],SUM(MONEY) AS moneys,"
                         + "case Status when 0 then '空闲' when 1 then '使用'"
                         + " when 3 then '维修'"
                         + " when 5 then '报废' end Status"
                         + " from officedba.EquipmentInfo a"
                         + " group by [Type],Status,CompanyCD) a inner join officedba.CodeEquipmentType b"
                         + " on a.[Type]=b.ID AND a.CompanyCD=b.CompanyCD) m"
                         + " left outer join "
                         + "(select a.*,b.CodeName,b.WarningLimit from (select count(*)  as FreeCount ,[Type],"
                         + "case Status when 0 then '空闲' when 1 then '使用'"
                         + " when 3 then '维修'"
                         + " when 5 then '报废' end Status"
                         + " from officedba.EquipmentInfo a where 1=1 and a.status='0'"
                         + " group by [Type],Status) a inner join officedba.CodeEquipmentType b"
                         + " on a.[Type]=b.ID) n"
                         + " on  m.Type=n.Type where m.CompanyCD='" + equip_M.CompanyCD + "') x "
                         + " where x.CompanyCD='" + equip_M.CompanyCD + "'  ";
           if (equip_M.Type != 0)
               sql += " and x.Type=" + equip_M.Type + "";
           //if (EquipPre != "")
           //    sql += " and x.EpPre='" + EquipPre + "'";
           if (equip_M.Status != "")
               sql += " and x.Status='" + equip_M.Status + "'";
           sql += " group by Status,CodeName,Type";
           //return SqlHelper.ExecuteSql(sql);
           return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
       }
       #endregion

       #region 查询设备汇总信息列表(金额合计)
       /// <summary>
       /// 查询设备汇总信息列表(金额合计)
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetEquipmentTotalSumTableBycondition(EquipMnetInfoModel equip_M)
       {
           StringBuilder searchSql = new StringBuilder();
           searchSql.AppendLine("select sum(moneys) as moneys ");
           searchSql.AppendLine("from (select CompanyCD,isnull(m.TotalCount,'') TotalCount,isnull(m.Type,'') Type,isnull(m.status,'') Status,");
           searchSql.AppendLine("isnull(m.CodeName,'') CodeName,isnull(m.WarningLimit,'') WarningLimit,");
           searchSql.AppendLine("isnull(n.FreeCount,'') FreeCount,moneys ");
           searchSql.AppendLine(" from (select a.*,b.CodeName,b.WarningLimit from (select CompanyCD,count(*)  as TotalCount ,[Type],SUM(MONEY) AS moneys,");
           searchSql.AppendLine("case Status when 0 then '空闲' when 1 then '使用'");
           searchSql.AppendLine(" when 3 then '维修'");
           searchSql.AppendLine(" when 5 then '报废' end Status");
           searchSql.AppendLine(" from officedba.EquipmentInfo a");
           searchSql.AppendLine(" group by [Type],Status,CompanyCD) a inner join officedba.CodeEquipmentType b");
           searchSql.AppendLine(" on a.[Type]=b.ID AND a.CompanyCD=b.CompanyCD) m");
           searchSql.AppendLine(" left outer join ");
           searchSql.AppendLine("(select a.*,b.CodeName,b.WarningLimit from (select count(*)  as FreeCount ,[Type],");
           searchSql.AppendLine("case Status when 0 then '空闲' when 1 then '使用'");
           searchSql.AppendLine(" when 3 then '维修'");
           searchSql.AppendLine(" when 5 then '报废' end Status");
           searchSql.AppendLine(" from officedba.EquipmentInfo a where 1=1 and a.status='0'");
           searchSql.AppendLine(" group by [Type],Status) a inner join officedba.CodeEquipmentType b");
           searchSql.AppendLine(" on a.[Type]=b.ID) n");
           searchSql.AppendLine(" on  m.Type=n.Type where m.CompanyCD=@CompanyCD) x ");
           searchSql.AppendLine(" where x.CompanyCD=@CompanyCD  ");

           SqlCommand comm = new SqlCommand();
           if (equip_M.Type != 0)
           {
               searchSql.AppendLine(" and x.Type=@Type");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@Type", equip_M.Type.ToString()));
           }
           if (equip_M.Status != "")
           {
               searchSql.AppendLine(" and x.Status=@Status");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@Status", equip_M.Status));
           }



           //添加公司代码参数
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", equip_M.CompanyCD));

           //指定命令的SQL文

           comm.CommandText = searchSql.ToString();
           //执行查询
           return SqlHelper.ExecuteSearch(comm);
       }
       #endregion

       #region 根据设备编号查询设备信息
       /// <summary>
       /// 根据设备编号查询设备信息
       /// </summary>
       /// <param name="EquipmnetNO">设备编号</param>
       /// <returns>DataTable</returns>
       public static DataTable GetEquipmentInfoByEquipmentNo(string EquipmnetNO, string CompanyID)
       {
           string sql = "select a.*,b.*,f.CodeName,c.EmployeeName,d.DeptName,isnull(e.EmployeeName,'') as CreatorPerson from officedba.EquipmentInfo a left outer join "
                    +"officedba.EquipmentFittings b on "
                    + "a.equipmentno=b.equipmentno "
		            +"LEFT OUTER JOIN "
                    +"officedba.EmployeeInfo c "
                    +"ON a.CurrentUser=c.ID   "
                    +"LEFT OUTER JOIN  officedba.DeptInfo d  ON a.CurrentDepartment=d.ID "
                    +"left outer join "
                    +"officedba.EmployeeInfo e "
                    +"ON a.Creator=e.id "
                    +"LEFT OUTER JOIN officedba.CodeEquipmentType f "
					+"ON a.Type=f.ID "
                    +"where a.equipmentno='" + EquipmnetNO + "' AND a.CompanyCD='"+CompanyID+"'";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 根据设备编号查询设备信息(打印)
       /// <summary>
       /// 根据设备编号查询设备信息(打印)
       /// </summary>
       /// <param name="EquipmnetNO">设备编号</param>
       /// <returns>DataTable</returns>
       public static DataTable GetEquipmentInfoByEquipmentNoForPrint(string EquipmnetNO, string CompanyID)
       {
           string sql = "select a.EquipmentNO,a.EquipmentIndex,a.EquipmentName,a.Precision,"
                        + "a.BuyDate,a.ExaminePeriod,a.Provider,a.Norm,a.Warranty,a.PYShort,a.EquiFrom,"
                        +"CASE a.FittingFlag WHEN '0' THEN '无' WHEN '1' THEN '有' END FittingFlag,"
                        +"CASE a.Status WHEN '0' THEN '空闲' WHEN '1' THEN '使用' WHEN '3' THEN '维修' WHEN '5' THEN '报废' END Status,"
                        +"a.Money,a.CreateDate,a.EquipmentDetail,"
                        +"f.CodeName,c.EmployeeName,g.TypeName,"
                        +"d.DeptName,isnull(e.EmployeeName,'') as CreatorPerson "
                        +"from officedba.EquipmentInfo a "
                        +" LEFT OUTER JOIN "
                        +"officedba.EmployeeInfo c "
                        + " ON a.CompanyCD='" + CompanyID + "' AND a.CurrentUser=c.ID AND a.CompanyCD=c.CompanyCD  "
                        +"LEFT OUTER JOIN  "
                        +"officedba.DeptInfo d  ON a.CurrentDepartment=d.ID "
                        +" left outer join "
                        +"officedba.EmployeeInfo e "
                        + " ON a.CompanyCD='" + CompanyID + "' AND a.Creator=e.id and a.CompanyCD=e.CompanyCD "
                        +"LEFT OUTER JOIN officedba.CodeEquipmentType f "
                        + " ON a.CompanyCD='" + CompanyID + "' AND a.Type=f.ID and a.CompanyCD=f.CompanyCD "
                        +"left outer join officedba.CodePublicType g "
                        + "on a.CompanyCD='" + CompanyID + "' AND a.unit=g.id and g.typeflag='3' and typecode='4' and a.CompanyCD=g.CompanyCD "
                    + "where a.equipmentno='" + EquipmnetNO + "' AND a.CompanyCD='" + CompanyID + "'";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 根据设备编号查询设备配件信息(打印)
       /// <summary>
       /// 根据设备编号查询设备配件信息(打印)
       /// </summary>
       /// <param name="EquipmnetNO">设备编号</param>
       /// <returns>DataTable</returns>
       public static DataTable GetEquipmentFittingInfoByEquipmentNoForPrint(string EquipmnetNO, string CompanyID)
       {
           string sql = "select b.* from officedba.EquipmentInfo a "
                        +"left outer join officedba.EquipmentFittings b "
                        + "on a.CompanyCD='" + CompanyID + "' and a.EquipmentNO=b.EquipmentNo and a.CompanyCD=b.CompanyCD "
                        + "where a.equipmentno='" + EquipmnetNO + "' AND a.CompanyCD='" + CompanyID + "'";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 设备添加操作（无配件）
       /// <summary>
        /// 设备添加操作（无配件）
        /// </summary>
        /// <param name="model">设备信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool AddEquipMnetInfo(EquipMnetInfoModel EquipModel)
       {
           try
           {
               ArrayList lstInsert = new ArrayList();


               for (int i = 0; i <EquipModel.EquipmentCount; i++)
               {
                   #region 参数设置
                   StringBuilder sql = new StringBuilder();
                   sql.AppendLine("INSERT INTO officedba.EquipmentInfo");
                   sql.AppendLine("		(EquipmentNo      ");
                   sql.AppendLine("		,EquipmentIndex         ");
                   sql.AppendLine("		,CompanyCD       ");
                   sql.AppendLine("		,EquipmentName       ");
                   sql.AppendLine("		,Norm       ");
                   sql.AppendLine("		,[Precision]       ");
                   sql.AppendLine("		,BuyDate      ");
                   sql.AppendLine("		,Provider   ");
                   sql.AppendLine("		,[Type] ");
                   sql.AppendLine("		,Warranty        ");
                   sql.AppendLine("		,ExaminePeriod        ");
                   sql.AppendLine("		,CurrentUser        ");
                   sql.AppendLine("		,CurrentDepartment        ");
                   sql.AppendLine("		,FittingFlag        ");
                   sql.AppendLine("		,Unit        ");
                   sql.AppendLine("		,PYShort        ");
                   sql.AppendLine("		,Money        ");
                   sql.AppendLine("		,EquiFrom        ");
                   sql.AppendLine("		,Attachment        ");
                   sql.AppendLine("		,Status        ");
                   sql.AppendLine("		,Creator        ");
                   sql.AppendLine("		,CreateDate        ");
                   sql.AppendLine("		,ModifiedUserid        ");
                   sql.AppendLine("		,ModifiedDate        ");
                   sql.AppendLine("		,EquipmentCount        ");
                   sql.AppendLine("		,EquipmentDetail)        ");
                   sql.AppendLine("VALUES                  ");
                   sql.AppendLine("		(@EquipmentNo     ");
                   sql.AppendLine("		,@EquipmentIndex        ");
                   sql.AppendLine("		,@CompanyCD      ");
                   sql.AppendLine("		,@EquipmentName      ");
                   sql.AppendLine("		,@Norm      ");
                   sql.AppendLine("		,@Precision      ");
                   sql.AppendLine("		,@BuyDate     ");
                   sql.AppendLine("		,@Provider  ");
                   sql.AppendLine("		,@Type");
                   sql.AppendLine("		,@Warranty       ");
                   sql.AppendLine("		,@ExaminePeriod       ");
                   sql.AppendLine("		,@CurrentUser       ");
                   sql.AppendLine("		,@CurrentDepartment       ");
                   sql.AppendLine("		,@FittingFlag       ");
                   sql.AppendLine("		,@Unit        ");
                   sql.AppendLine("		,@PYShort        ");
                   sql.AppendLine("		,@Money        ");
                   sql.AppendLine("		,@EquiFrom        ");
                   sql.AppendLine("		,@Attachment        ");
                   sql.AppendLine("		,@Status       ");
                   sql.AppendLine("		,@Creator        ");
                   sql.AppendLine("		,@CreateDate        ");
                   sql.AppendLine("		,@ModifiedUserid        ");
                   sql.AppendLine("		,@ModifiedDate        ");
                   sql.AppendLine("		,@EquipmentCount        ");
                   sql.AppendLine("		,@EquipmentDetail)       ");
                   //设置参数
                   SqlCommand comm = new SqlCommand();
                   comm.CommandText = sql.ToString();
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@EquipmentNo", EquipModel.EquipmentNo.Split(',')[i]));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@EquipmentIndex", EquipModel.EquipmentIndex));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", EquipModel.CompanyCD));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@EquipmentName", EquipModel.EquipmentName));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@Norm", EquipModel.Norm));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@Precision", EquipModel.Precision));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@BuyDate", EquipModel.BuyDate.ToString()));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@Provider", EquipModel.Provider));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@Type", EquipModel.Type.ToString()));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@Warranty", EquipModel.Warranty));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@ExaminePeriod", EquipModel.ExaminePeriod));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@CurrentUser", EquipModel.CurrentUser.ToString()));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@CurrentDepartment", EquipModel.CurrentDepartment.ToString()));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@FittingFlag", EquipModel.FittingFlag));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@Unit", EquipModel.Unit.ToString()));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@PYShort", EquipModel.PYShort));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@Money", EquipModel.Money.ToString()));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@EquiFrom", EquipModel.EquiFrom));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@Attachment", EquipModel.Attachment));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@Status", EquipModel.Status));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@EquipmentDetail", EquipModel.EquipmentDetail));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator", EquipModel.Creator.ToString()));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateDate", EquipModel.CreateDate.ToString()));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserid", EquipModel.ModifiedUserid));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@EquipmentCount", EquipModel.EquipmentCount.ToString()));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedDate", EquipModel.ModifiedDate.ToString()));
                   #endregion
                   lstInsert.Add(comm);//数组加入插入基表的command
               }

               return SqlHelper.ExecuteTransWithArrayList(lstInsert);
           }
           catch 
           {
               return false;
           }
       }
       #endregion
       #region  设备修改操作（无配件）
       /// <summary>
       /// 设备修改操作（无配件）
       /// </summary>
       /// <param name="model">设备信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool UpdateEquipMnetInfo(EquipMnetInfoModel EquipModel)
       {
           try
           {
               StringBuilder sql = new StringBuilder();
               sql.AppendLine("UPDATE officedba.EquipmentInfo			     ");
               sql.AppendLine("SET                                      ");
               sql.AppendLine("		EquipmentIndex = @EquipmentIndex,             ");
               sql.AppendLine("		EquipmentName = @EquipmentName,             ");
               sql.AppendLine("		Norm = @Norm,             ");
               sql.AppendLine("		[Precision] = @Precision,             ");
               sql.AppendLine("		BuyDate = @BuyDate,             ");
               sql.AppendLine("		Provider = @Provider,             ");
               sql.AppendLine("		[Type] = @Type,             ");
               sql.AppendLine("		Warranty = @Warranty,             ");
               sql.AppendLine("		ExaminePeriod = @ExaminePeriod,             ");
               sql.AppendLine("		CurrentUser = @CurrentUser,             ");
               sql.AppendLine("		CurrentDepartment = @CurrentDepartment,             ");
               sql.AppendLine("		FittingFlag = @FittingFlag,             ");
               sql.AppendLine("		Unit=@Unit,        ");
               sql.AppendLine("		PYShort=@PYShort,        ");
               sql.AppendLine("		Money=@Money,        ");
               sql.AppendLine("		EquiFrom=@EquiFrom,        ");
               sql.AppendLine("		Attachment=@Attachment,        ");
               sql.AppendLine("		Status = @Status,             ");
               sql.AppendLine("		ModifiedUserid = @ModifiedUserid,             ");
               sql.AppendLine("		ModifiedDate = @ModifiedDate,             ");
               sql.AppendLine("		EquipmentDetail = @EquipmentDetail             ");
               sql.AppendLine("WHERE                                    ");
               sql.AppendLine("		EquipmentNo = @EquipmentNo AND CompanyCD = @CompanyCD ");
               //设置参数
               SqlParameter[] param;
               param = new SqlParameter[23];
               param[0] = SqlHelper.GetParameter("@EquipmentIndex", EquipModel.EquipmentIndex);
               param[1] = SqlHelper.GetParameter("@EquipmentName", EquipModel.EquipmentName);
               param[2] = SqlHelper.GetParameter("@Norm", EquipModel.Norm);
               param[3] = SqlHelper.GetParameter("@Precision", EquipModel.Precision);
               param[4] = SqlHelper.GetParameter("@BuyDate", EquipModel.BuyDate);
               param[5] = SqlHelper.GetParameter("@Provider", EquipModel.Provider);
               param[6] = SqlHelper.GetParameter("@Type", EquipModel.Type);
               param[7] = SqlHelper.GetParameter("@Warranty", EquipModel.Warranty);
               param[8] = SqlHelper.GetParameter("@ExaminePeriod", EquipModel.ExaminePeriod);
               param[9] = SqlHelper.GetParameter("@CurrentUser", EquipModel.CurrentUser);
               param[10] = SqlHelper.GetParameter("@CurrentDepartment", EquipModel.CurrentDepartment);
               param[11] = SqlHelper.GetParameter("@FittingFlag", EquipModel.FittingFlag);
               param[12] = SqlHelper.GetParameter("@Unit", Convert.ToInt32(EquipModel.Unit));
               param[13] = SqlHelper.GetParameter("@PYShort", EquipModel.PYShort);
               param[14] = SqlHelper.GetParameter("@Money", EquipModel.Money);
               param[15] = SqlHelper.GetParameter("@EquiFrom", EquipModel.EquiFrom);
               param[16] = SqlHelper.GetParameter("@Attachment", EquipModel.Attachment);
               param[17] = SqlHelper.GetParameter("@Status", EquipModel.Status);
               param[18] = SqlHelper.GetParameter("@EquipmentDetail", EquipModel.EquipmentDetail);
               param[19] = SqlHelper.GetParameter("@EquipmentNo", EquipModel.EquipmentNo);
               param[20] = SqlHelper.GetParameter("@CompanyCD", EquipModel.CompanyCD);
               param[21] = SqlHelper.GetParameter("@ModifiedUserid", EquipModel.ModifiedUserid);
               param[22] = SqlHelper.GetParameter("@ModifiedDate", EquipModel.ModifiedDate);

               string fitsql = "DELETE officedba.EquipmentFittings WHERE EquipmentNo = '" + EquipModel.EquipmentNo + "' AND CompanyCD='" + EquipModel.CompanyCD + "'";

               SqlHelper.ExecuteTransSql(sql.ToString(), param);
               bool flag=SqlHelper.Result.OprateCount > 0 ? true : false;
               if (flag)
               {
                   SqlHelper.ExecuteTransSql(fitsql.ToString());
                   return flag;
               }
               else return flag;
           }
           catch 
           {
               return false;
           }
       }
       #endregion
       #region 添加设备和配件操作
       /// <summary>
        /// 添加设备和配件操作
        /// </summary>
        /// <param name="model">设备信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool AddEquipMnetAndFitInfo(EquipMnetInfoModel EquipModel, string FitInfo)
       {
           try
           {
               ArrayList lstInsert = new ArrayList();
               for (int i = 0; i < EquipModel.EquipmentCount; i++)
               {
                   #region 参数设置
                   StringBuilder sql = new StringBuilder();
                   sql.AppendLine("INSERT INTO officedba.EquipmentInfo");
                   sql.AppendLine("		(EquipmentNo      ");
                   sql.AppendLine("		,EquipmentIndex         ");
                   sql.AppendLine("		,CompanyCD       ");
                   sql.AppendLine("		,EquipmentName       ");
                   sql.AppendLine("		,Norm       ");
                   sql.AppendLine("		,[Precision]       ");
                   sql.AppendLine("		,BuyDate      ");
                   sql.AppendLine("		,Provider   ");
                   sql.AppendLine("		,[Type] ");
                   sql.AppendLine("		,Warranty        ");
                   sql.AppendLine("		,ExaminePeriod        ");
                   sql.AppendLine("		,CurrentUser        ");
                   sql.AppendLine("		,CurrentDepartment        ");
                   sql.AppendLine("		,FittingFlag        ");
                   sql.AppendLine("		,Unit        ");
                   sql.AppendLine("		,PYShort        ");
                   sql.AppendLine("		,Money        ");
                   sql.AppendLine("		,EquiFrom        ");
                   sql.AppendLine("		,Attachment        ");
                   sql.AppendLine("		,Status        ");
                   sql.AppendLine("		,Creator        ");
                   sql.AppendLine("		,CreateDate        ");
                   sql.AppendLine("		,ModifiedUserid        ");
                   sql.AppendLine("		,ModifiedDate        ");
                   sql.AppendLine("		,EquipmentCount        ");
                   sql.AppendLine("		,EquipmentDetail)        ");
                   sql.AppendLine("VALUES                  ");
                   sql.AppendLine("		(@EquipmentNo     ");
                   sql.AppendLine("		,@EquipmentIndex        ");
                   sql.AppendLine("		,@CompanyCD      ");
                   sql.AppendLine("		,@EquipmentName      ");
                   sql.AppendLine("		,@Norm      ");
                   sql.AppendLine("		,@Precision      ");
                   sql.AppendLine("		,@BuyDate     ");
                   sql.AppendLine("		,@Provider  ");
                   sql.AppendLine("		,@Type");
                   sql.AppendLine("		,@Warranty       ");
                   sql.AppendLine("		,@ExaminePeriod       ");
                   sql.AppendLine("		,@CurrentUser       ");
                   sql.AppendLine("		,@CurrentDepartment       ");
                   sql.AppendLine("		,@FittingFlag       ");
                   sql.AppendLine("		,@Unit        ");
                   sql.AppendLine("		,@PYShort        ");
                   sql.AppendLine("		,@Money        ");
                   sql.AppendLine("		,@EquiFrom        ");
                   sql.AppendLine("		,@Attachment        ");
                   sql.AppendLine("		,@Status       ");
                   sql.AppendLine("		,@Creator        ");
                   sql.AppendLine("		,@CreateDate        ");
                   sql.AppendLine("		,@ModifiedUserid        ");
                   sql.AppendLine("		,@ModifiedDate        ");
                   sql.AppendLine("		,@EquipmentCount        ");
                   sql.AppendLine("		,@EquipmentDetail)       ");
                   //设置参数
                   SqlCommand comm = new SqlCommand();
                   comm.CommandText = sql.ToString();
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@EquipmentNo", EquipModel.EquipmentNo.Split(',')[i]));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@EquipmentIndex", EquipModel.EquipmentIndex));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", EquipModel.CompanyCD));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@EquipmentName", EquipModel.EquipmentName));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@Norm", EquipModel.Norm));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@Precision", EquipModel.Precision));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@BuyDate", EquipModel.BuyDate.ToString()));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@Provider", EquipModel.Provider));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@Type", EquipModel.Type.ToString()));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@Warranty", EquipModel.Warranty));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@ExaminePeriod", EquipModel.ExaminePeriod));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@CurrentUser", EquipModel.CurrentUser.ToString()));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@CurrentDepartment", EquipModel.CurrentDepartment.ToString()));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@FittingFlag", EquipModel.FittingFlag));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@Unit", EquipModel.Unit.ToString()));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@PYShort", EquipModel.PYShort));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@Money", EquipModel.Money.ToString()));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@EquiFrom", EquipModel.EquiFrom));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@Attachment", EquipModel.Attachment));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@Status", EquipModel.Status));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@EquipmentDetail", EquipModel.EquipmentDetail));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator", EquipModel.Creator.ToString()));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateDate", EquipModel.CreateDate.ToString()));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserid", EquipModel.ModifiedUserid));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@EquipmentCount", EquipModel.EquipmentCount.ToString()));
                   comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedDate", EquipModel.ModifiedDate.ToString()));
                   #endregion
                   lstInsert.Add(comm);//数组加入插入基表的command
               }
               #region 明细
               for (int i = 0; i < EquipModel.EquipmentCount; i++)
               {
                   #region 参数设置
               string[] strarray = null;
                   string recorditems = "";
                   string[] inseritems = null;
                   strarray = FitInfo.Split('|');
                   for (int j = 0; j < strarray.Length; j++)
                   {
                       StringBuilder fitsql = new StringBuilder();
                       recorditems = strarray[j];
                       inseritems = recorditems.Split(',');
                       if (recorditems.Length != 0)
                       {
                           string fitname = inseritems[0].ToString();//配件名称
                           string fitdesc = inseritems[1].ToString();//描述
                           fitsql.AppendLine("INSERT INTO officedba.EquipmentFittings");
                           fitsql.AppendLine("		(CompanyCD      ");
                           fitsql.AppendLine("		,EquipmentNo        ");
                           fitsql.AppendLine("		,FittingName        ");
                           fitsql.AppendLine("		,FittingDescription)        ");
                           fitsql.AppendLine("VALUES                  ");
                           fitsql.AppendLine("		(@CompanyCD    ");
                           fitsql.AppendLine("		,@EquipmentNo       ");
                           fitsql.AppendLine("		,@FittingName       ");
                           fitsql.AppendLine("		,@FittingDescription )       ");

                           SqlCommand commdetail = new SqlCommand();
                           commdetail.CommandText = fitsql.ToString();
                           commdetail.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", EquipModel.CompanyCD));
                           commdetail.Parameters.Add(SqlHelper.GetParameterFromString("@EquipmentNo", EquipModel.EquipmentNo.Split(',')[i]));
                           commdetail.Parameters.Add(SqlHelper.GetParameterFromString("@FittingName", inseritems[0].ToString()));
                           commdetail.Parameters.Add(SqlHelper.GetParameterFromString("@FittingDescription", inseritems[1].ToString()));
                           lstInsert.Add(commdetail);//数组加入插入基表的command
                       }
                   }
                   #endregion

               }
               #endregion

               return SqlHelper.ExecuteTransWithArrayList(lstInsert);
           }
           catch 
           {
               return false;
           }
       }

       /// <summary>
       /// 设备和配件添加操作
       /// </summary>
       /// <param name="model">设备信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool InsertAll(string EquipInfoSql, string FitInfo, string EquipmentNo, string CompanyCD)
       {
           EquipmentFitModel EquipFitM = new EquipmentFitModel();
           string[] strarray = null;
           string recorditems = "";
           string[] inseritems = null;
           try
           {
                   strarray = FitInfo.Split('|');
                   string[] sqlarray = new string[strarray.Length];
                   sqlarray[0] = EquipInfoSql;
                   for (int i = 0; i < strarray.Length; i++)
                   {
                       StringBuilder fitsql = new StringBuilder();
                       recorditems = strarray[i];
                       inseritems = recorditems.Split(',');
                       if (recorditems.Length != 0)
                       {
                           
                           string fitname = inseritems[0].ToString();//配件名称
                           string fitdesc = inseritems[1].ToString();//描述

                           fitsql.AppendLine("INSERT INTO officedba.EquipmentFittings");
                           fitsql.AppendLine("		(CompanyCD      ");
                           fitsql.AppendLine("		,EquipmentNo        ");
                           fitsql.AppendLine("		,FittingName        ");
                           fitsql.AppendLine("		,FittingDescription)        ");
                           fitsql.AppendLine("VALUES                  ");
                           fitsql.AppendLine("		('" + CompanyCD + "'     ");
                           fitsql.AppendLine("		,'" + EquipmentNo + "'       ");
                           fitsql.AppendLine("		,'" + fitname + "'       ");
                           fitsql.AppendLine("		,'" + fitdesc + "')       ");
                          sqlarray[i] = fitsql.ToString();
                       }
                   }
                   SqlHelper.ExecuteTransForListWithSQL(sqlarray);
                   return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch 
           {
               return false;
           }
       }
       #endregion
       #region 更新设备和配件操作
       /// <summary>
       /// 更新设备和配件操作
       /// </summary>
       /// <param name="model">设备信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool UpdateEquipMnetAndFitInfo(EquipMnetInfoModel EquipModel, string FitInfo)
       {
           try
           {
               StringBuilder UpdateEquipInfoSql = new StringBuilder();
               UpdateEquipInfoSql.AppendLine("UPDATE officedba.EquipmentInfo			     ");
               UpdateEquipInfoSql.AppendLine("SET                                      ");
               UpdateEquipInfoSql.AppendLine("		EquipmentIndex = '" + EquipModel.EquipmentIndex+ "',             ");
               UpdateEquipInfoSql.AppendLine("		EquipmentName = '" + EquipModel .EquipmentName+ "',             ");
               UpdateEquipInfoSql.AppendLine("		Norm = '" + EquipModel .Norm+ "',             ");
               UpdateEquipInfoSql.AppendLine("		[Precision] = '" + EquipModel .Precision+ "',             ");
               UpdateEquipInfoSql.AppendLine("		BuyDate = '" + EquipModel .BuyDate+ "',             ");
               UpdateEquipInfoSql.AppendLine("		Provider = '" + EquipModel .Provider+ "',             ");
               UpdateEquipInfoSql.AppendLine("		[Type] = " + EquipModel .Type+ ",             ");
               UpdateEquipInfoSql.AppendLine("		Warranty = '" + EquipModel .Warranty+ "',             ");
               UpdateEquipInfoSql.AppendLine("		ExaminePeriod = '" + EquipModel .ExaminePeriod+ "',             ");
               UpdateEquipInfoSql.AppendLine("		CurrentUser = " + EquipModel .CurrentUser+ ",             ");
               UpdateEquipInfoSql.AppendLine("		CurrentDepartment = " + EquipModel .CurrentDepartment+ ",             ");
               UpdateEquipInfoSql.AppendLine("		FittingFlag = '" + EquipModel .FittingFlag+ "',             ");
               UpdateEquipInfoSql.AppendLine("		Unit=" + EquipModel.Unit + ",        ");
               UpdateEquipInfoSql.AppendLine("		PYShort='" + EquipModel.PYShort + "',        ");
               UpdateEquipInfoSql.AppendLine("		Money=" + EquipModel.Money + ",        ");
               UpdateEquipInfoSql.AppendLine("		EquiFrom='" + EquipModel.EquiFrom + "',        ");
               UpdateEquipInfoSql.AppendLine("		Attachment='" + EquipModel.Attachment + "',        ");
               UpdateEquipInfoSql.AppendLine("		Status = '" + EquipModel.Status + "',             ");
               UpdateEquipInfoSql.AppendLine("		ModifiedUserid = '" + EquipModel.ModifiedUserid + "',             ");
               UpdateEquipInfoSql.AppendLine("		ModifiedDate = '" + EquipModel.ModifiedDate + "',             ");
               UpdateEquipInfoSql.AppendLine("		EquipmentDetail = '" + EquipModel.EquipmentDetail + "'             ");
               UpdateEquipInfoSql.AppendLine("WHERE                                    ");
               UpdateEquipInfoSql.AppendLine("		EquipmentNo = '" + EquipModel.EquipmentNo + "' AND CompanyCD = '" + EquipModel .CompanyCD+ "' ");
               if (FitInfo != "")
                   return UpdateAll(UpdateEquipInfoSql.ToString(), FitInfo, EquipModel.EquipmentNo, EquipModel.CompanyCD);
               else
               {
                   SqlHelper.ExecuteTransSql(UpdateEquipInfoSql.ToString());
                   return SqlHelper.Result.OprateCount > 0 ? true : false;
               }
           }
           catch
           {
               return false;
           }
       }

       /// <summary>
       /// 设备和配件添加操作
       /// </summary>
       /// <param name="model">设备信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool UpdateAll(string UpdateEquipInfoSql, string FitInfo, string EquipmentNo, string CompanyCD)
       {
           EquipmentFitModel EquipFitM = new EquipmentFitModel();
           string[] strarray = null;
           string recorditems = "";
           string[] inseritems = null;
           try
           {
               strarray = FitInfo.Split('|');
               string[] sqlarray = new string[strarray.Length+1];
               sqlarray[0] = UpdateEquipInfoSql;
               sqlarray[1] = "DELETE officedba.EquipmentFittings WHERE EquipmentNo = '" + EquipmentNo + "'";
               for (int i = 1; i < strarray.Length; i++)
               {
                   StringBuilder fitsql = new StringBuilder();
                   recorditems = strarray[i];
                   inseritems = recorditems.Split(',');
                   if (recorditems.Length != 0)
                   {
                       string fitname = inseritems[0].ToString();//配件名称
                       string fitdesc = inseritems[1].ToString();//描述


                       fitsql.AppendLine("INSERT INTO officedba.EquipmentFittings");
                       fitsql.AppendLine("		(CompanyCD      ");
                       fitsql.AppendLine("		,EquipmentNo        ");
                       fitsql.AppendLine("		,FittingName        ");
                       fitsql.AppendLine("		,FittingDescription)        ");
                       fitsql.AppendLine("VALUES                  ");
                       fitsql.AppendLine("		('" + CompanyCD + "'     ");
                       fitsql.AppendLine("		,'" + EquipmentNo + "'       ");
                       fitsql.AppendLine("		,'" + fitname + "'       ");
                       fitsql.AppendLine("		,'" + fitdesc + "')       ");
                       sqlarray[i+1] = fitsql.ToString();
                   }
               }
               SqlHelper.ExecuteTransForListWithSQL(sqlarray);
               return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch
           {
               return false;
           }
       }
       #endregion
       #region  唯一性判断
       /// <summary>
       /// 唯一性判断
       /// </summary>
       /// <param name="EquipCode">设备编号</param>
       /// <param name="TableName">表名</param>
       /// <param name="ColName">列名</param>
       /// <returns></returns>
       public static int GetEquipInfoByIndex(string EquipCode, string TableName, string ColName,string CompanyCD)
       {
           //查询语句
           string sql = "SELECT COUNT(*) AS IndexCount FROM " + TableName + " WHERE " + ColName + " = " + EquipCode + " AND CompanyCD='" + CompanyCD + "'";
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
       #region 删除设备和配件操作
       /// <summary>
       /// 删除设备和配件操作
       /// </summary>
       /// <param name="EquipmentIDS">设备IDS</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool DelEquipMnetAndFitInfo(string EquipmentIDS)
       {
           string allEquipID = "";
           System.Text.StringBuilder sb = new System.Text.StringBuilder();
           string[] Delsql = new string[2];
           try
           {
               string[] EquipIDS = null;
               EquipIDS = EquipmentIDS.Split(',');

               for (int i = 0; i < EquipIDS.Length; i++)
               {
                   EquipIDS[i] = "'" + EquipIDS[i] + "'";
                   sb.Append(EquipIDS[i]);
               }

               allEquipID = sb.ToString().Replace("''", "','");
               Delsql[0] = "DELETE FROM officedba.EquipmentInfo WHERE EquipmentNo IN (" + allEquipID + ")";
               Delsql[1] = "DELETE FROM officedba.EquipmentFittings WHERE EquipmentNo IN (" + allEquipID + ")";
               SqlHelper.ExecuteTransForListWithSQL(Delsql);
               return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch 
           {
               return false;
           }
       }
       #endregion
       #region 获取设备类型
       /// <summary>
       /// 获取设备类型
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetEquipType()
       {
           string sql = "select ID,CodeName from officedba.CodeEquipmentType where TypeFlag=1 AND UsedStatus=1";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
       #region 获取设备编号下拉列表
       /// <summary>
       /// 获取设备编号下拉列表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetEquipNo()
       {
           string sql = "select EquipmentNo from officedba.EquipmentInfo";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion

       #region 能否删除设备信息
       /// <summary>
       /// 能否删除设备信息
       /// </summary>
       /// <param name="EmployeeID">员工ID</param>
       /// <param name="CompanyID">公司代码</param>
       /// <returns></returns>
       public static bool IfDeleteEquipmentInfo(string EquipmentNos, string CompanyID)
       {
           string[] NOS = null;
           NOS = EquipmentNos.Split(',');
           bool Flag = true;

           for (int i = 0; i < NOS.Length; i++)
           {
               if (IsExistInfo(NOS[i], CompanyID, "officedba.EquipmentUsed", "EquipmentNo"))
               {
                   Flag = false;
                   break;
               }
               else 
               {
                   if (IsExistInfo(NOS[i], CompanyID, "officedba.EquipmentRepair", "EquipmentNo"))
                   {
                       Flag = false;
                       break;
                   }
                   else 
                   {
                       if (IsExistInfo(NOS[i], CompanyID, "officedba.EquipmentUseless", "EquipmentNo"))
                       {
                           Flag = false;
                           break;
                       }
                   }
               }
           }
           return Flag;
       }
       #endregion
       #region 能否删除设备信息
       /// <summary>
       /// 能否删除设备信息
       /// </summary>
       /// <param name="EmployeeID">员工ID</param>
       /// <param name="CompanyID">公司代码</param>
       /// <returns></returns>
       public static bool IsExistInfo(string EquipmentNo, string CompanyID,string TableName,string ColName)
       {

           string sql = "SELECT * FROM " + TableName + " WHERE " + ColName + "='" + EquipmentNo + "' "
           +"AND CompanyCD='" + CompanyID + "' ";
           return SqlHelper.ExecuteSql(sql).Rows.Count > 0 ? true : false;
       }
       #endregion

       #region 根据状态和设备编号判断此设备是否空闲
        /// <summary>
       /// 根据状态和设备编号判断此设备是否空闲
        /// </summary>
        /// <param name="EquipNo">设备编号</param>
        /// <param name="IsFree">空闲状态0</param>
        /// <returns></returns>
       public static int EquipIsFree(string EquipNo, string IsFree, string CompanyID)
       {
           string sql = "SELECT COUNT(*) AS IndexCount FROM officedba.EquipmentInfo WHERE EquipmentNo = '" + EquipNo.Replace(",", "").Trim() + "' and Status='" + IsFree + "' and CompanyCD='" + CompanyID + "'";
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

       #region 查询设备明细信息列表
       /// <summary>
       /// 查询设备明细信息列表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetEquipmentReportInfo(EquipMnetInfoModel equip_M,string EndDate,int pageIndex,int pageCount,string ord, ref int TotalCount)
       {
           string sql = "select a.ID,a.EquipmentNo,a.EquipmentIndex,a.CompanyCD,"
                     + "a.EquipmentName,a.Norm,a.Precision,a.BuyDate,a.Provider,a.Type,a.Warranty,"
                     + "a.ExaminePeriod,a.CurrentUser,a.CurrentDepartment,isnull(a.Unit,'')Unit,a.Money,"
                     + "a.EquipmentDetail,"
                     + "case FittingFlag when 0 then '无' when 1 then '有' else '其他' end FittingFlag, "
                     + "case (a.Status) when 0 then '空闲' when 1 then '使用' when 3 then '维修' when 5 then '报废' end Status,b.CodeName,"
                     + "isnull(c.EmployeeName,'')EmployeeName,isnull(d.DeptName,'')DeptName "
                     + "from officedba.EquipmentInfo a "
                     + "left outer join officedba.CodeEquipmentType b on a.Type=b.ID "
                     + "LEFT OUTER JOIN "
                     + "officedba.EmployeeInfo c "
                     + "ON a.CurrentUser=c.ID  "
                     + " LEFT OUTER JOIN "
                     + "officedba.DeptInfo d on a.CurrentDepartment=d.ID "
                     + " where a.CompanyCD='" + equip_M.CompanyCD + "'";
           if (equip_M.EquipmentName != "")
               sql += " and EquipmentName like '%" + equip_M.EquipmentName + "%'";
           if (equip_M.Type != 0)
               sql += " and [Type]=" + equip_M.Type + "";
           if (equip_M.BuyDate.ToString() != "" && equip_M.BuyDate.ToString() != "1900-1-1 0:00:00")
               sql += " and BuyDate>='" + equip_M.BuyDate + "'";
           if (EndDate!="")
               sql += " and BuyDate<='" + EndDate + "'";
           if (equip_M.CurrentDepartment != 0)
               sql += " and CurrentDepartment=" + equip_M.CurrentDepartment + "";
           if (equip_M.Status.ToString() != "")
               sql += " and Status='" + equip_M.Status + "'";
           return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);

           //return SqlHelper.ExecuteSql(sql);
       }
       #endregion

       #region 查询设备明细信息报表
       /// <summary>
       /// 查询设备明细信息列表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetEquipmentDetailInfoAll(EquipMnetInfoModel equip_M, string ord, string BuyEndDate)
       {
           string sql = "select a.ID,a.EquipmentNo,a.EquipmentIndex,a.CompanyCD,"
                     + "a.EquipmentName,a.Norm,a.Precision,a.BuyDate,a.Provider,a.Type,a.Warranty,"
                     +"a.ExaminePeriod,a.CurrentUser,a.CurrentDepartment,isnull(a.Unit,'')Unit,a.Money,"
                     +"a.EquipmentDetail,"
                     +"case FittingFlag when 0 then '无' when 1 then '有' else '其他' end FittingFlag, "
                     +"case (a.Status) when 0 then '空闲' when 1 then '使用' when 3 then '维修' when 5 then '报废' end Status,b.CodeName,"
                     +"isnull(c.EmployeeName,'')EmployeeName,isnull(d.DeptName,'')DeptName "
                     +"from officedba.EquipmentInfo a "
		             +"left outer join officedba.CodeEquipmentType b on a.Type=b.ID "
                     +"LEFT OUTER JOIN "
                     +"officedba.EmployeeInfo c "
                     +"ON a.CurrentUser=c.ID  "
                     +" LEFT OUTER JOIN "
                     + "officedba.DeptInfo d on a.CurrentDepartment=d.ID "
                     + " where a.CompanyCD='" + equip_M.CompanyCD + "'";
           if (equip_M.EquipmentName != "")
               sql += " and EquipmentName like '%" + equip_M.EquipmentName + "%'";
           if (equip_M.Type != 0)
               sql += " and [Type]=" + equip_M.Type + "";
           if (equip_M.BuyDate.ToString() != "" && equip_M.BuyDate.ToString() != "1900-1-1 0:00:00")
               sql += " and BuyDate>='" + equip_M.BuyDate + "'";
           if (BuyEndDate != "")
               sql += " and BuyDate<='" + BuyEndDate + "'";
           if (equip_M.CurrentDepartment != 0)
               sql += " and CurrentDepartment=" + equip_M.CurrentDepartment + "";
           if (equip_M.Status.ToString() != "")
               sql += " and Status='" + equip_M.Status + "'";
           sql = sql + ord;
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion

       #region 设备汇总查询导出
       /// <summary>
       /// 设备汇总查询导出
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetEquipmentTotalTableForExp(string EquipType,string CompanyCD,string Equipmentstatus,string ord)
       {
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           string sql = "select sum(TotalCount)TotalCount,Status,CodeName,sum(FreeCount)FreeCount,sum(WarningLimit)WarningLimit,Type,Convert(numeric(20,"+userInfo.SelPoint+"),sum(moneys)) as moneys  "
                        + "from (select CompanyCD,isnull(m.TotalCount,'') TotalCount,isnull(m.Type,'') Type,isnull(m.status,'') Status,"
                         + "isnull(m.CodeName,'') CodeName,isnull(m.WarningLimit,'') WarningLimit,"
                         + "isnull(n.FreeCount,'') FreeCount,moneys "
                         + " from (select a.*,b.CodeName,b.WarningLimit from (select CompanyCD,count(*)  as TotalCount ,[Type],SUM(MONEY) AS moneys,"
                         + "case Status when 0 then '空闲' when 1 then '使用'"
                         + " when 3 then '维修'"
                         + " when 5 then '报废' end Status "
                         + " from officedba.EquipmentInfo a"
                         + " group by [Type],Status,CompanyCD) a inner join officedba.CodeEquipmentType b"
                         + " on a.[Type]=b.ID AND a.CompanyCD=b.CompanyCD) m"
                         + " left outer join "
                         + "(select a.*,b.CodeName,b.WarningLimit from (select count(*)  as FreeCount ,[Type],"
                         + "case Status when 0 then '空闲' when 1 then '使用'"
                         + " when 3 then '维修'"
                         + " when 5 then '报废' end Status"
                         + " from officedba.EquipmentInfo a where 1=1 and a.status='0'"
                         + " group by [Type],Status) a inner join officedba.CodeEquipmentType b"
                         + " on a.[Type]=b.ID) n"
                         + " on  m.Type=n.Type where m.CompanyCD='" + CompanyCD + "') x "
                         + " where x.CompanyCD='" + CompanyCD + "'  ";
           if (EquipType != "")
               sql += " and x.Type=" + EquipType + "";
           if (Equipmentstatus != "")
               sql += " and x.Status='" + Equipmentstatus + "'";
           sql += " group by Status,CodeName,Type ";
           sql = sql + ord;
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion

       #region 设备明细导出
       /// <summary>
       /// 设备明细导出
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetEquipmentTableForPrint(EquipMnetInfoModel equip_M,string ord)
       {
           UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
           string sql = "select a.ID,a.EquipmentNo,a.EquipmentIndex,a.CompanyCD,Convert(numeric(20,"+userInfo.SelPoint+"),[Money]) as moneys,"
           + "a.EquipmentName,a.Norm,a.Precision,a.BuyDate,a.Provider,a.Type,a.Warranty,"
           + "a.ExaminePeriod,a.CurrentUser,a.CurrentDepartment,isnull(a.Unit,'')Unit,"
           + "a.EquipmentDetail,"
           + "case FittingFlag when 0 then '无' when 1 then '有' else '其他' end FittingFlag, "
           + "case (a.Status) when 0 then '空闲' when 1 then '使用' when 3 then '维修'  when 5 then '报废' end Status,b.CodeName,"
           + "isnull(c.EmployeeName,'')EmployeeName,isnull(d.DeptName,'')DeptName "
           + " from officedba.EquipmentInfo a inner join officedba.CodeEquipmentType b on a.Type=b.ID "
           + "LEFT OUTER JOIN officedba.EmployeeInfo c "
           + "ON a.CurrentUser=c.ID  and a.CompanyCD=c.CompanyCD "
           + "LEFT OUTER JOIN "
           + "officedba.DeptInfo d "
           + "ON a.CurrentDepartment=d.ID and a.CompanyCD=d.CompanyCD "
           + " where a.CompanyCD='" + equip_M.CompanyCD + "'";
           if (equip_M.EquipmentNo != "")
               sql += " and EquipmentNo like '%" + equip_M.EquipmentNo + "%'";
           if (equip_M.EquipmentName != "")
               sql += " and EquipmentName like '%" + equip_M.EquipmentName + "%'";
           if (equip_M.EquipmentIndex != "")
               sql += " and EquipmentIndex like '%" + equip_M.EquipmentIndex + "%'";
           if (equip_M.Provider != "")
               sql += " and Provider like '%" + equip_M.Provider + "%'";
           if (equip_M.Type != 0)
               sql += " and [Type]=" + equip_M.Type + "";
           if (equip_M.BuyDate.ToString() != "" && equip_M.BuyDate.ToString() != "1900-1-1 0:00:00")
               sql += " and BuyDate>='" + equip_M.BuyDate + "'";
           if (equip_M.CurrentDepartment != 0)
               sql += " and CurrentDepartment=" + equip_M.CurrentDepartment + "";
           if (equip_M.CurrentUser != 0)
               sql += " and CurrentUser=" + equip_M.CurrentUser + "";
           if (equip_M.Status.ToString() != "")
               sql += " and Status='" + equip_M.Status + "' ";
           sql = sql + ord;
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion

       #region 新报表

       public static DataTable EquipmentByDetail(string CompanyCD, string EquipmentName,string Type,string DeptID,string Status,string BeginDate,string EndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
       {
           string sql = "select a.ID,a.EquipmentNo,a.EquipmentIndex,a.CompanyCD,"
                     + "a.EquipmentName,a.Norm,a.Precision,Convert(varchar,a.BuyDate,23) BuyDate,a.Provider,a.Type,a.Warranty,"
                     + "a.ExaminePeriod,a.CurrentUser,a.CurrentDepartment,isnull(a.Unit,'')Unit,a.Money,"
                     + "a.EquipmentDetail,"
                     + "case FittingFlag when 0 then '无' when 1 then '有' else '其他' end FittingFlag, "
                     + "case (a.Status) when 0 then '空闲' when 1 then '使用' when 3 then '维修' when 5 then '报废' end Status,b.CodeName,"
                     + "isnull(c.EmployeeName,'')EmployeeName,isnull(d.DeptName,'')DeptName,d.ID as DeptID,a.Status as Status1 "
                     + "from officedba.EquipmentInfo a "
                     + "left outer join officedba.CodeEquipmentType b on a.Type=b.ID "
                     + "LEFT OUTER JOIN "
                     + "officedba.EmployeeInfo c "
                     + "ON a.CurrentUser=c.ID  "
                     + " LEFT OUTER JOIN "
                     + "officedba.DeptInfo d on a.CurrentDepartment=d.ID "
                     + " where a.CompanyCD='" + CompanyCD + "'";
           if (EquipmentName != "")
               sql += " and EquipmentName like '%" + EquipmentName + "%'";
           if (Type !="")
               sql += " and [Type]=" + Type + "";
           if (BeginDate != "")
               sql += " and BuyDate>='" + BeginDate + "'";
           if (EndDate != "")
               sql += " and BuyDate<dateadd(day,1,'" + EndDate + "') ";
           if (DeptID != "")
               sql += " and CurrentDepartment=" + DeptID + "";
           if (Status.ToString() != "")
               sql += " and a.Status='" + Status + "'";
           return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);

       }

       #region 设备领用统计报表列表
       /// <summary>
       /// 设备领用统计报表列表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable EquipmentUsedByDetail(string StartDate, string EndDate, string EquipType, string ReceiveDept, string CompanyID, string DateType, string DateValue, int pageIndex, int pageCount, string ord, ref int TotalCount)
       {
           string sql = "SELECT a.ID,a.EquipmentNo,Convert(varchar(10),ApplyFactDate,120)UsedDate,"
                        + "Convert(varchar(10),UseEndDate,120)UseEndDate,b.EquipmentName,c.CodeName,"
                        +"dateName(year,a.UsedDate)+'年' as BackYear,"
                        +"dateName(year,a.UsedDate)+'年-'+dateName(month,a.UsedDate)+'月' as BackMonth,"
                        + "dateName(year,a.UsedDate)+'年-'+dateName(week,a.UsedDate)+'周' as BackWeek,ProposerDeptID as DeptID,"
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
               sql += " and a.UsedDate<='" + EndDate + "'";
           if (ReceiveDept != "")
               sql += " and a.ProposerDeptID=" + ReceiveDept + "";
             if (DateValue != "")
            {

                if (DateType == "1")
                {
                    sql += "and (dateName(year,a.UsedDate)+'年')='";
                    sql += DateValue;
                    sql += "'";
                }
                else if (DateType == "2")
                {
                    sql += "and (dateName(year,a.UsedDate)+'年-'+dateName(month,a.UsedDate)+'月')='";
                    sql += DateValue;
                    sql += "'";
                }
                else
                {
                    sql += "and (dateName(year,a.UsedDate)+'年-'+dateName(week,a.UsedDate)+'周')='";
                    sql += DateValue;
                    sql += "'";
                }
            }
           return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
       }
       #endregion


       /// <summary>
       /// 设备状况分布
       /// </summary>
       public static DataTable EquipmentByStatus(string CompanyCD, string EquipmentName, string Type,string DeptID, string BeginDate, string EndDate)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine(" select ");
           sql.AppendLine(" case (a.Status) when 0 then '空闲' when 1 then '使用' ");
           sql.AppendLine(" when 2 then '申请维修' when 3 then '维修'  ");
           sql.AppendLine(" when 4 then '申请报废' when 5 then '报废' end name,");
           sql.AppendLine("  count(1) counts,sum(a.Money) counts1,a.Status ");
           sql.AppendLine(" from officedba.EquipmentInfo a ");
           sql.Append(" where 1=1 and CompanyCD='");
           sql.Append(CompanyCD);
           sql.Append("' ");

           if (EquipmentName != "")
           {
               sql.Append(" and EquipmentName like '%" + EquipmentName + "%' ");
           }
           if (Type != "")
           {
               sql.Append(" and [Type]=");
               sql.Append(Type);
           }
           if (DeptID != "")
           {
               sql.Append(" and CurrentDepartment=");
               sql.Append(DeptID);
           }

           if (BeginDate != "")
           {
               sql.Append(" and BuyDate>='");
               sql.Append(BeginDate);
               sql.Append("'");
           }
           if (EndDate != "")
           {
               sql.Append(" and BuyDate<dateadd(day,1,'");
               sql.Append(EndDate);
               sql.Append("') ");
           }

           sql.AppendLine(" group by a.Status ");
           return SqlHelper.ExecuteSql(sql.ToString());
       }


       /// <summary>
       /// 设备部门分布
       /// </summary>
       public static DataTable EquipmentByDept(string CompanyCD, string EquipmentName, string Type, string Status,string BeginDate, string EndDate)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine(" select min(b.DeptName) as name,count(1) counts,sum(a.Money) counts1,a.CurrentDepartment as DeptID");
           sql.AppendLine(" from officedba.EquipmentInfo a ");
           sql.AppendLine(" inner join officedba.Deptinfo b on a.CurrentDepartment=b.ID ");
           sql.Append(" where 1=1 and A.CompanyCD='");
           sql.Append(CompanyCD);
           sql.Append("' ");

           if (EquipmentName != "")
           {
               sql.Append(" and a.EquipmentName like '%" + EquipmentName + "%'");
           }
           if (Type != "")
           {
               sql.Append(" and a.[Type]=");
               sql.Append(Type);
           }

           if (Status != "")
           {
               sql.Append(" and a.Status='");
               sql.Append(Status);
               sql.Append("'");
           }
           if (BeginDate != "")
           {
               sql.Append(" and CreateDate>='");
               sql.Append(BeginDate);
               sql.Append("'");
           }
           if (EndDate != "")
           {
               sql.Append(" and CreateDate<dateadd(day,1,'");
               sql.Append(EndDate);
               sql.Append("') ");
           }

           sql.AppendLine(" group by a.CurrentDepartment ");
           return SqlHelper.ExecuteSql(sql.ToString());
       }

       /// <summary>
       /// 部门领用分析
       /// </summary>
       public static DataTable EquipmentUsedByDept(string CompanyCD,string Type,string BeginDate,string EndDate)
       {
           StringBuilder sql = new StringBuilder();
           sql.Append(" SELECT min(b.DeptName) name  ,count(1) counts ,ProposerDeptID as DeptID ");
           sql.Append(" FROM officedba.EquipmentUsed a  ");
           sql.Append(" LEFT  JOIN officedba.DeptInfo b ON a.ProposerDeptID=b.ID ");
           sql.Append(" LEFT  JOIN officedba.EquipmentInfo c ON a.EquipmentNo=c.EquipmentNo AND a.CompanyCD=c.CompanyCD ");
           sql.Append(" where 1=1 and A.CompanyCD='");
           sql.Append(CompanyCD);
           sql.Append("' ");
           if (Type != "")
           {
               sql.Append(" and c.Type=");
               sql.Append(Type);
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
           sql.Append(" group by ProposerDeptID ");
           return SqlHelper.ExecuteSql(sql.ToString());
       }

       /// <summary>
       /// 部门领用走势
       /// </summary>
       public static DataTable EquipmentUsedByTrend(string CompanyCD,string DeptID,string Type,string DateType,string BeginDate,string EndDate)
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
           sb.Append(" as Name,count(1) as counts ");
           sb.Append(" FROM officedba.EquipmentUsed a ");
           sb.Append(" LEFT  JOIN officedba.EquipmentInfo c ON a.EquipmentNo=c.EquipmentNo AND a.CompanyCD=c.CompanyCD ");
           sb.Append(" where a.ApplyFactDate is not null  and  a.companyCD='");
           sb.Append(CompanyCD);
           sb.Append("' ");

           if (DeptID != "")
           {
               sb.Append(" and a.ProposerDeptID=");
               sb.Append(DeptID);
           }
           if (Type != "")
           {
               sb.Append(" and c.Type=");
               sb.Append(Type);
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

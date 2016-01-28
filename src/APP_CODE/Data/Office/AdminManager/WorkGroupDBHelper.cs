/**********************************************
 * 类作用：   考勤班组设置数据库层处理
 * 建立人：   lysong
 * 建立时间： 2009/04/08
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
    /// 类名：WorkGroupDBHelper
    /// 描述：考勤班组设置数据库层处理
    /// 作者：lysong
    /// 创建时间：2009/04/08
    /// </summary>
   public class WorkGroupDBHelper
    {
        #region 班组信息添加
        /// <summary>
        /// 添加班次信息
        /// </summary>
        /// <param name="WorkGroupInfos">班组信息</param>
        /// <param name="userID">用户ID</param>
        /// <param name="CompanyID">公司代码</param>
        /// <returns>成功：true,失败:false</returns>
       public static string AddWorkGroupInfo(string WorkGroupInfos, string userID, string CompanyID)
        {
            string[] strarray = null;
            string recorditems = "";
            string[] inseritems = null;
            string ErrMsg = "";
            try
            {
                strarray = WorkGroupInfos.Split('|');
                for (int i = 0; i < strarray.Length; i++)
                {
                    StringBuilder WorkGroupSql = new StringBuilder();
                    recorditems = strarray[i];
                    inseritems = recorditems.Split(',');
                    if (recorditems.Length != 0)
                    {
                        string WorkGroupNo = inseritems[0].ToString().Trim();//班组编号
                        string WorkGroupName = inseritems[1].ToString().Trim();//班组名称
                        string WorkGroupType = inseritems[2].ToString().Trim();//班组类型
                        string WorkShiftID = inseritems[3].ToString().Trim();//班次ID
                        if (WorkGroupIsExist(WorkGroupNo,CompanyID) == 0) //插入
                        {
                            #region 添加SQL
                            WorkGroupSql.AppendLine("INSERT INTO officedba.WorkGroup");
                            WorkGroupSql.AppendLine("		(CompanyCD      ");
                            WorkGroupSql.AppendLine("		,WorkGroupNo        ");
                            WorkGroupSql.AppendLine("		,WorkGroupName        ");
                            WorkGroupSql.AppendLine("		,WorkGroupType        ");
                            WorkGroupSql.AppendLine("		,WorkShiftNo        ");
                            WorkGroupSql.AppendLine("		,ModifiedDate        ");
                            WorkGroupSql.AppendLine("		,ModifiedUserID)        ");
                            WorkGroupSql.AppendLine("VALUES                  ");
                            WorkGroupSql.AppendLine("		('" + CompanyID + "'     ");
                            WorkGroupSql.AppendLine("		,'" + WorkGroupNo + "'       ");
                            WorkGroupSql.AppendLine("		,'" + WorkGroupName + "'       ");
                            WorkGroupSql.AppendLine("		,'" + WorkGroupType + "'       ");
                            WorkGroupSql.AppendLine("		,'" + WorkShiftID + "'       ");
                            WorkGroupSql.AppendLine("		,'" + System.DateTime.Now + "'       ");
                            WorkGroupSql.AppendLine("		,'" + userID + "')       ");
                            #endregion
                        }
                        else//更新
                        {
                            #region 更新SQL
                            WorkGroupSql.AppendLine("UPDATE officedba.WorkGroup");
                            WorkGroupSql.AppendLine("	SET	      ");
                            WorkGroupSql.AppendLine("		 WorkGroupName='" + WorkGroupName + "'        ");
                            WorkGroupSql.AppendLine("		,WorkGroupType='" + WorkGroupType + "'      ");
                            WorkGroupSql.AppendLine("		,WorkShiftNo='" + WorkShiftID + "'        ");
                            WorkGroupSql.AppendLine("		,ModifiedDate='" + System.DateTime.Now + "'        ");
                            WorkGroupSql.AppendLine("		,ModifiedUserID='" + userID + "'        ");
                            WorkGroupSql.AppendLine("		WHERE WorkGroupNo='" + WorkGroupNo + "' AND   CompanyCD='" + CompanyID + "'   ");
                            #endregion
                        }
                        SqlHelper.ExecuteTransSql(WorkGroupSql.ToString());
                        if (SqlHelper.Result.OprateCount <= 0)
                            ErrMsg = WorkGroupNo + ',';
                    }
                }
                return ErrMsg;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        #endregion
        #region 根据班组编号判断此班组是否存在
        /// <summary>
       /// 根据班组编号判断此班组是否存在
        /// </summary>
       /// <param name="CompanyID">公司ID</param>
        /// <returns></returns>
        public static int WorkGroupIsExist(string WorkGroupNo,string CompanyID)
        {
            string sql = "SELECT COUNT(*) AS IndexCount FROM officedba.WorkGroup WHERE WorkGroupNo ='" + WorkGroupNo + "' AND CompanyCD='" + CompanyID + "'";
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
        #region 查询班组列表
        /// <summary>
        /// 查询班组列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetWorkGroupInfo(string WorkGroupNo, string WorkGroupName, string CompanyID)
        {
            string sql = "select a.ID,a.CompanyCD,a.WorkGroupNo,a.WorkGroupName,a.WorkGroupType as WGTNO,case a.WorkGroupType when 0 then '排班' when 1 then '正常班' end WorkGroupType,a.WorkShiftNo,"
                            + "isnull(b.WorkShiftName,'0') WorkShiftName from officedba.WorkGroup a "
                            +"left join officedba.WorkshiftSet b "
                            + "on a.WorkShiftNo=b.WorkShiftNo "
                            + "WHERE a.CompanyCD='" + CompanyID + "' ";
            if (WorkGroupNo != "")
                sql += " and WorkGroupNo LIKE '%" + WorkGroupNo + "%'";
            if (WorkGroupName != "")
                sql += " and WorkGroupName LIKE '%" + WorkGroupName + "%'";
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion
        #region 删除设班次信息
        /// <summary>
        /// 删除班组信息
        /// </summary>
        /// <param name="WorkGroupIds">班组编号</param>
        /// <returns>删除是否成功 false:失败，true:成功</returns>
        public static bool DelWorkGroupInfo(string WorkGroupIds, string CompanyID)
        {
            string allEquipID = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string[] Delsql = new string[1];
            try
            {
                string[] EquipIDS = null;
                EquipIDS = WorkGroupIds.Split(',');

                for (int i = 0; i < EquipIDS.Length; i++)
                {
                    EquipIDS[i] = "'" + EquipIDS[i] + "'";
                    sb.Append(EquipIDS[i]);
                }

                allEquipID = sb.ToString().Replace("''", "','");
                Delsql[0] = "DELETE FROM officedba.WorkGroup WHERE CompanyCD='" + CompanyID + "' AND  WorkGroupNo IN (" + allEquipID + ")";
                SqlHelper.ExecuteTransForListWithSQL(Delsql);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch 
            {
                return false;
            }
        }
        #endregion
        #region 班组下拉列表
        /// <summary>
        /// 班组下拉列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetWorkGroupList(string CompanyCD)
        {
            string sql = "select ID,WorkGroupNo,WorkGroupName "
                         + "from officedba.WorkGroup where CompanyCD='" + CompanyCD + "' AND WorkGroupType='0'";
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion
        #region 班组下拉列表(包括正常班)
        /// <summary>
        /// 班组下拉列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetWorkGroupList1(string CompanyCD)
        {
            string sql = "select ID,WorkGroupNo,WorkGroupName "
                         + "from officedba.WorkGroup where CompanyCD='" + CompanyCD + "'";
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion
        #region 班组列表导出
        /// <summary>
        /// 班组列表导出
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetWorkGroupInfoForExp(string WorkGroupNo, string WorkGroupName, string CompanyID,string ord)
        {
            string sql = "select a.ID,a.CompanyCD,a.WorkGroupNo,a.WorkGroupName,a.WorkGroupType as WGTNO,case a.WorkGroupType when 0 then '排班' when 1 then '正常班' end WorkGroupType,a.WorkShiftNo,"
                            + "isnull(b.WorkShiftName,'0') WorkShiftName from officedba.WorkGroup a "
                            + "left join officedba.WorkshiftSet b "
                            + "on a.WorkShiftNo=b.WorkShiftNo "
                            + "WHERE a.CompanyCD='" + CompanyID + "' ";
            if (WorkGroupNo != "")
                sql += " and WorkGroupNo LIKE '%" + WorkGroupNo + "%'";
            if (WorkGroupName != "")
                sql += " and WorkGroupName LIKE '%" + WorkGroupName + "%' ";
            sql += ord;
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion
    }
}

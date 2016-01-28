/**********************************************
 * 类作用：   考勤年休假设置数据库层处理
 * 建立人：   lysong
 * 建立时间： 2009/03/31
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
    /// 类名：YearHolidayDBHelper
    /// 描述：考勤年休假设置数据库层处理
    /// 作者：lysong
    /// 创建时间：2009/03/31
    /// </summary>
   public class YearHolidayDBHelper
    {
        #region 获取人员
        /// <summary>
        /// 获取人员
        /// </summary>
        /// <returns>DataTable</returns>
       public static DataTable GetUserList()
        {
            string sql = "select a.ID,a.EmployeeName from officedba.EmployeeInfo a ";
            return SqlHelper.ExecuteSql(sql);
        }
        #endregion
        #region 年休假设置信息添加
       /// <summary>
       /// 年休假设置信息添加
       /// </summary>
       /// <param name="YearHolidayM">YearHolidayM</param>
       /// <param name="StrYearHoliday">年休假设置信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static string  AddYearHolidayInfoSet(YearHolidayModel YearHolidayM, string StrYearHoliday)
       {
           string[] strarray = null;
           string recorditems = "";
           string[] inseritems = null;
           string ErrMsg = "";
           try
           {
               strarray = StrYearHoliday.Split('|');
               for (int i = 0; i < strarray.Length; i++)
               {
                   StringBuilder StrYearHolidaySql = new StringBuilder();
                   recorditems = strarray[i];
                   inseritems = recorditems.Split(',');
                   if (recorditems.Length != 0)
                   {
                       string EmployeeID = inseritems[0].ToString().Trim();//员工ID
                       string HolidayHours = inseritems[1].ToString().Trim();//时长
                       string UserName=inseritems[2].ToString().Trim();//姓名
                       if (YearHolidayIsExist(EmployeeID) == 0) //插入
                       {
                           #region 添加SQL
                           StrYearHolidaySql.AppendLine("INSERT INTO officedba.YearHoliday");
                           StrYearHolidaySql.AppendLine("		(CompanyCD      ");
                           StrYearHolidaySql.AppendLine("		,EmployeeID        ");
                           StrYearHolidaySql.AppendLine("		,HolidayHours        ");
                           StrYearHolidaySql.AppendLine("		,ModifiedDate        ");
                           StrYearHolidaySql.AppendLine("		,ModifiedUserID)        ");
                           StrYearHolidaySql.AppendLine("VALUES                  ");
                           StrYearHolidaySql.AppendLine("		('" + YearHolidayM.CompanyCD + "'     ");
                           StrYearHolidaySql.AppendLine("		," + Convert.ToInt32(EmployeeID) + "       ");
                           StrYearHolidaySql.AppendLine("		," + Convert.ToDecimal(HolidayHours) + "       ");
                           StrYearHolidaySql.AppendLine("		,'" + YearHolidayM.ModifiedDate + "'       ");
                           StrYearHolidaySql.AppendLine("		,'" + YearHolidayM.ModifiedUserID + "')       ");
                           #endregion
                       }
                       else//更新
                       {
                           #region 更新SQL
                           StrYearHolidaySql.AppendLine("UPDATE officedba.YearHoliday");
                           StrYearHolidaySql.AppendLine("	SET	CompanyCD='" + YearHolidayM.CompanyCD + "'      ");
                           StrYearHolidaySql.AppendLine("		,HolidayHours=" + Convert.ToDecimal(HolidayHours) + "        ");
                           StrYearHolidaySql.AppendLine("		,ModifiedDate='" + YearHolidayM.ModifiedDate + "'        ");
                           StrYearHolidaySql.AppendLine("		,ModifiedUserID='" + YearHolidayM.ModifiedUserID + "'      ");
                           StrYearHolidaySql.AppendLine("		WHERE EmployeeID=" + Convert.ToInt32(EmployeeID) + "      ");
                           #endregion
                       }
                       SqlHelper.ExecuteTransSql(StrYearHolidaySql.ToString());
                       if (SqlHelper.Result.OprateCount <= 0)
                           ErrMsg = UserName+',';
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
        #region 根据员工ID判断是否已经存在此员工的年休假设置
       /// <summary>
       /// 判断是否已经存在此员工的年休假设置
       /// </summary>
       /// <param name="EmployeeID">员工ID</param>
       /// <returns></returns>
       public static int YearHolidayIsExist(string EmployeeID)
       {
           string sql = "SELECT COUNT(*) AS IndexCount FROM officedba.YearHoliday WHERE EmployeeID =" +Convert.ToInt32(EmployeeID) + "";
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
        #region 获取年休假信息
       /// <summary>
       /// 获取年休假信息
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetYearHolidayInfo(string UserName,string EmployNo,string CompanyID)
       {
           string sql = "select a.ID,a.EmployeeID,a.HolidayHours,isnull(b.EmployeeName,'')EmployeeName,"
                         + "isnull(b.EmployeeNo,'')EmployeeNo,isnull(b.DeptName,'')DeptName,isnull(b.QuarterName,'')QuarterName "
                         +"from officedba.YearHoliday a "
                         +"LEFT OUTER JOIN  "
                         +"(select m.ID,m.EmployeeName,m.EmployeeNo,l.DeptName,n.QuarterName  "
                         +"from officedba.EmployeeInfo m "
                         +"left outer join officedba.DeptInfo l "
                         +"on m.DeptID=l.ID and m.CompanyCD=l.CompanyCD "
					     +"left outer join officedba.DeptQuarter n "
						 +"on m.QuarterID=n.ID and m.CompanyCD=n.CompanyCD "
                         +") b "
                         + "on a.EmployeeID=b.ID WHERE a.CompanyCD='" + CompanyID + "'";
           if (UserName != "")
               sql += " and b.EmployeeName like '%" + UserName + "%'";
           if (EmployNo!="")
               sql += " and b.EmployeeNo like '%" + EmployNo + "%'";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion

       #region 删除年休假信息
       /// <summary>
       /// 删除年休假信息
       /// </summary>
       /// <param name="EquipmentIDS">设备IDS</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool DelYearHolidayByID(string YearHolidayEmpIDS)
       {
           string allApplyID = "";
           System.Text.StringBuilder sb = new System.Text.StringBuilder();
           string[] Delsql = new string[1];
           try
           {
               string[] IDS = null;
               IDS = YearHolidayEmpIDS.Split(',');

               for (int i = 0; i < IDS.Length; i++)
               {
                   IDS[i] = "'" + IDS[i] + "'";
                   sb.Append(IDS[i]);
               }

               allApplyID = sb.ToString().Replace("''", "','");
               Delsql[0] = "DELETE FROM officedba.YearHoliday WHERE ID IN (" + allApplyID + ")";
               SqlHelper.ExecuteTransForListWithSQL(Delsql);
               return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch 
           {
               return false;
           }
       }
       #endregion

       #region 年休假导出
       /// <summary>
       /// 年休假导出
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetYearHolidayInfoForExp(string UserName, string EmployNo, string CompanyID,string ord)
       {
           string sql = "select a.ID,a.EmployeeID,a.HolidayHours,isnull(b.EmployeeName,'')EmployeeName,"
                         + "isnull(b.EmployeeNo,'')EmployeeNo,isnull(b.DeptName,'')DeptName,isnull(b.QuarterName,'')QuarterName "
                         + "from officedba.YearHoliday a "
                         + "LEFT OUTER JOIN  "
                         + "(select m.ID,m.EmployeeName,m.EmployeeNo,l.DeptName,n.QuarterName  "
                         + "from officedba.EmployeeInfo m "
                         + "left outer join officedba.DeptInfo l "
                         + "on m.DeptID=l.ID and m.CompanyCD=l.CompanyCD "
                         + "left outer join officedba.DeptQuarter n "
                         + "on m.QuarterID=n.ID and m.CompanyCD=n.CompanyCD "
                         + ") b "
                         + "on a.EmployeeID=b.ID WHERE a.CompanyCD='" + CompanyID + "'";
           if (UserName != "")
               sql += " and b.EmployeeName like '%" + UserName + "%'";
           if (EmployNo != "")
               sql += " and b.EmployeeNo like '%" + EmployNo + "%'";
           sql = sql + ord;
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
    }
}

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Globalization;

using XBase.Common;
using XBase.Business.Office.AdminManager;
using XBase.Model.Office.AdminManager;

public partial class DeskTop : BasePage, System.Web.SessionState.IRequiresSessionState
{
    private  ChineseLunisolarCalendar chineseDate = new ChineseLunisolarCalendar();
    string CompanyCD = "";
    int Employeeid;
    string date = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            LoadBillTypeList();
            BindWorkShift();
            this.inputTime.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.lblshowyear.InnerHtml = "公元" + DateTime.Now.Year + "年";
            this.lblmonth.InnerHtml = DateTime.Now.Month + "月";
            this.spanday.InnerHtml = DateTime.Now.ToString("dd");
            this.lblweek.InnerHtml = ReturnWeek(  DateTime.Now.DayOfWeek.ToString());
            this.lblnongli.InnerHtml = "";
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
             if (userInfo.IsRoot == "1") {
                 this.signIO.Visible = false;
             }
            // this.lblnongli.InnerHtml = "农历" + chineseDate.GetYear(DateTime.Now).ToString() + "年" + chineseDate.GetMonth(DateTime.Now).ToString() + "月" + chineseDate.GetDayOfMonth(DateTime.Now).ToString()+"日";
           
        }
    }
   private string ReturnWeek( string day){
       switch (day){
           case "Monday": return "星期一";
         case "Tuesday": return "星期二";
         case "Wednesday": return "星期三";
         case "Thursday": return "星期四";
         case "Friday": return "星期五";
         case "Saturday": return "星期六";
         case "Sunday": return "星期日";
         default: return "";
       }
  }

   #region 绑定班次
   private void BindWorkShift()
   {
       CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
       Employeeid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
       date = System.DateTime.Now.ToShortDateString();
       string xh = GetWorkShiftIndex();
       DataTable DtHoliday = HolidayDBHelper.GetHolidayInfo(CompanyCD);//是否节假日

       if (DtHoliday != null)
       {
           if (DtHoliday.Rows.Count != 0)
           {
               for (int j = 0; j < DtHoliday.Rows.Count; j++)
               {
                   if (Convert.ToDateTime(date) >= Convert.ToDateTime(DtHoliday.Rows[j]["StartDate"].ToString()) && Convert.ToDateTime(date) <= Convert.ToDateTime(DtHoliday.Rows[j]["EndDate"].ToString()))
                   {
                       HiddenEmployeeAttendanceSetID.Value = "节假日";
                       return;
                   }
               }
           }
       }
       if (xh.Trim() != "" && xh.Trim() != "-1")
       {
           DataTable workshifttable = new DataTable();
           if (!xh.EndsWith(","))
           {
               DataTable IsRestDT = DailyAttendanceDBHelper.IsRestDay(CompanyCD, Employeeid, date, xh);//是否休息日
               if (IsRestDT != null)
               {
                   if (IsRestDT.Rows.Count > 0)
                   {
                       if (IsRestDT.Rows[0]["WorkShiftNo"].ToString() == "休息")
                       {
                           HiddenEmployeeAttendanceSetID.Value = "休息";
                           return;
                       }
                   }
               }
               workshifttable = DailyAttendanceDBHelper.GetWorkShiftInfo(CompanyCD, Employeeid, date, Convert.ToInt32(xh));//获取人员当前日期下的考勤班次下拉列表
           }
           else
               workshifttable = DailyAttendanceDBHelper.GetWorkShiftInfo(CompanyCD, xh.TrimEnd(','));//获取人员当前日期下的考勤班次下拉列表

           DataTable EmployeeAttendanceSetInfo = DailyAttendanceDBHelper.GetEmployeeAttendanceSetInfo(CompanyCD, Employeeid, date);//获取人员当前日期下的考勤设置信息
           if (workshifttable.Rows.Count > 0)
           {
               ddlworkshift.DataTextField = "ShiftTimeName";
               ddlworkshift.DataValueField = "ID";
               ddlworkshift.DataSource = workshifttable;
               ddlworkshift.DataBind();
           }
           if (EmployeeAttendanceSetInfo.Rows.Count > 0)
           {
               HiddenEmployeeAttendanceSetID.Value = EmployeeAttendanceSetInfo.Rows[0]["ID"].ToString() + "," + EmployeeAttendanceSetInfo.Rows[0]["AttendanceType"].ToString();
           }
       }
   }
   #endregion
   private string GetWorkShiftIndex()
   {
       //获取班组类型（正常班还是排班）
       DataTable WorkGroupTypeDT = DailyAttendanceDBHelper.GetWorkGroupType(CompanyCD, Employeeid, date);
       if (WorkGroupTypeDT != null)
       {
           if (WorkGroupTypeDT.Rows.Count > 0)
           {
               if (WorkGroupTypeDT.Rows[0]["WorkGroupType"].ToString().Trim() != "1")
               {
                   DataTable dt = DailyAttendanceDBHelper.GetStartDate(CompanyCD, Employeeid, date);
                   int day = 0;
                   if (dt.Rows.Count > 0)
                   {
                       string startdate = dt.Rows[0]["WorkPlanStartDate"].ToString();
                       TimeSpan span = DateTime.Parse(date) - DateTime.Parse(startdate);
                       day = span.Days;//相差天数
                       int xh = (day % dt.Rows.Count) + 1;//获取序号
                       return xh.ToString();
                   }
                   else
                   {
                       return "-1";
                   }
               }
               else
               {
                   return WorkGroupTypeDT.Rows[0]["WorkGroupNo"].ToString().Trim() + ",";
               }
           }
           else return "";
       }
       else return "";

   }


    private void LoadBillTypeList()
    {
        System.Text.RegularExpressions.Regex noblacks = new System.Text.RegularExpressions.Regex("[\\s]+");

        DataTable billTypeList = XBase.Business.SystemManager.BillType.GetBillTypeList();
        ArrayList TypeFlags = new ArrayList();

        DataTable tTable = billTypeList.Clone();
        foreach (DataRow row in billTypeList.Rows)
        {
            if (row["TypeLabel"].ToString() != "0")
                continue;
            if (row["AuditFlag"].ToString() != "1")
                continue;
            if (!TypeFlags.Contains(row["TypeFlag"].ToString()))
            {
                tTable.ImportRow(row);
                TypeFlags.Add(row["TypeFlag"].ToString());
            }
        }
        DataTable tTable2 = billTypeList.Clone();
        foreach (DataRow row in tTable.Rows)
        {

            //BillType.Items.Add(new ListItem(row["ModuleName"].ToString(), "-1"));

            DataRow[] rows = billTypeList.Select("TypeFlag='" + row["TypeFlag"].ToString() + "'");
            foreach (DataRow row2 in rows)
            {
                if (row2["TypeLabel"].ToString() != "0")
                    continue;
                if (row2["AuditFlag"].ToString() != "1")
                    continue;

                tTable2.ImportRow(row2);
                // BillType.Items.Add(new ListItem("|--"+row2["TypeName"].ToString(),  row2["TypeFlag"].ToString()+row2["TypeCode"].ToString()));
            }
        }

        StringBuilder billTypesScript = new StringBuilder();

        billTypesScript.Append("var billFlags=[");
        bool first = true;
        foreach (DataRow row in tTable.Rows)
        {
            if (first)
            {
                first = false;
            }
            else
            {
                billTypesScript.Append(",");
            }
            billTypesScript.Append("{t:\"" + noblacks.Replace(row["ModuleName"].ToString(), "") + "\",v:\"" + row["TypeFlag"].ToString() + "\"}");
        }
        billTypesScript.AppendLine("];");

        billTypesScript.Append("var billTypes=[");
        first = true;
        foreach (DataRow row in tTable2.Rows)
        {
            if (first)
            {
                first = false;
            }
            else
            {
                billTypesScript.Append(",");
            }
            string url = row["PageUrl"].ToString();
            if (url + "" == "")
            {
                url = "#";
            }
            billTypesScript.Append("{u:\"" + url + "\",p:\"" + row["TypeFlag"].ToString() + "\",t:\"" + noblacks.Replace(row["TypeName"].ToString(), "") + "\",v:\"" + row["TypeCode"].ToString() + "\"}");
        }
        billTypesScript.AppendLine("];");

        ClientScript.RegisterClientScriptBlock(this.GetType(), "billTypes", billTypesScript.ToString(), true);




    }
}

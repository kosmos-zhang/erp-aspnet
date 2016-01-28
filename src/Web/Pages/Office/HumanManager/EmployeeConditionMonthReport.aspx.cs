using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;
using XBase.Common;
using XBase.Business.Common;
using System.Collections.Generic;
using System.Collections;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


public partial class Pages_Office_HumanManager_EmployeeConditionMonthReport :BasePage 
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataBindToDept();
            DataBindToTaskYear();
        }
        if (IsPostBack)
        {
            Search();

        }
    }

    public void DataBindToTaskYear()
    {
        int i = 2007;
        //ddlTaskYear.Items.Add(new System.Web.UI.WebControls.ListItem("--请选择--", " "));
        for (int temp = i; temp < i +14; temp++)
        {
          
            
                ddlTaskYear.Items.Add(new System.Web.UI.WebControls.ListItem(temp.ToString() , temp.ToString()));
             
        }

        ddlTaskYear.SelectedValue = DateTime.Now.Year.ToString();


    }
    public void DataBindToDept()
    {  
        DataTable dt = PerformanceQueryBus.SearchDeptInfo();

        ddlDeptName.DataSource = dt;
        ddlDeptName.DataTextField = "DeptName";
        ddlDeptName.DataValueField = "ID";
        ddlDeptName.DataBind();
      ddlDeptName.Items.Add(new System.Web.UI.WebControls.ListItem("--请选择--", ""));
        ddlDeptName.SelectedValue = "";
    }



    public static bool IsLeapYear(int year)
    {
        return DateTime.IsLeapYear(year);
    }
    public void Search()
    {



        ReportDocument oRpt = new ReportDocument();
        CrystalReportSource1.ReportDocument.Load(Server.MapPath(@"~/OperatingModel/CrystalReport/HumanManager/EmployeeConditionMonthReport.rpt"));
        // SetDatabaseLogon 拉模式中必须用这个方法来设置登录信息，参数一：用户名；参数二：密码；参数三：服务器；参数四：数据库名
        CrystalReportViewer1.LogOnInfo.Add(ReportUtil.GetTableLogOnInfo("officedba.PerformanceSummary"));
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        EmployeeTestSearchModel searchModel = new EmployeeTestSearchModel();
        string DeptID = ddlDeptName.SelectedValue;
        string dds = Request.Form.ToString();
        string year = Request.Form["ddlTaskYear"] == null ? "" : Request.Form["ddlTaskYear"].ToString();
        string month = Request.Form["ddlTaskNum"] == null ? "" : Request.Form["ddlTaskNum"].ToString();  
       string day = "";
       if (month == "01" || month == "03" || month == "05" || month == "07" || month == "08" || month == "10" || month == "12")
       {
           day = "31";
       }
       else if (month == "04" || month == "06" || month == "09" || month == "11")
       {
           day = "30";
       }
       else
       {
           if (IsLeapYear(Convert.ToInt32(year)))
           {
               day = "29";
           }
           else
           {
               day = "28";
           }
       }

       string monthStartDate = year + "-" + month + "-" + "01";
       string monthEndDate = year + "-" + month + "-" + day;

        //查询数据

        CrystalReportSource1.ReportDocument.DataDefinition.FormulaFields["Creator"].Text = "\"" + "制表人:" + userInfo.EmployeeName + "\"";
        CrystalReportSource1.ReportDocument.DataDefinition.FormulaFields["Month"].Text = "\"" + "月份:" + year+"-"+month  + "\"";
        DataTable dt = new DataTable();
        DataColumn newCol1 = new DataColumn("UsedName");//部门
        DataColumn newCol2 = new DataColumn("Attachmentname");//招聘人数
        DataColumn newCol3 = new DataColumn("EmployeeName");//面试人数
        DataColumn newCol4 = new DataColumn("NameEn");//报道人数
        DataColumn newCol5 = new DataColumn("AccountNature");//迟到人数
        DataColumn newCol6 = new DataColumn("Height");//早退人数
        DataColumn newCol7 = new DataColumn("Weight");//旷工次数
        DataColumn newCol8 = new DataColumn("Sight"); //请假次数
        DataColumn newCol9 = new DataColumn("EmployeeNo"); //迁调人数
        DataColumn newCol10 = new DataColumn("CompanyCD"); //迁调人数
        
        dt.Columns.Add(newCol1);
        dt.Columns.Add(newCol2);
        dt.Columns.Add(newCol3);
        dt.Columns.Add(newCol4);
        dt.Columns.Add(newCol5);
        dt.Columns.Add(newCol6);
        dt.Columns.Add(newCol7);
        dt.Columns.Add(newCol8);
        dt.Columns.Add(newCol9);
        dt.Columns.Add(newCol10);
       // 部门招聘人数
        DataTable GetRequireNum = EmployeeTestBus.GetRequireNum(DeptID , monthStartDate, monthEndDate);
        // 部门面试人数
        DataTable GetInterviewNum = EmployeeTestBus.GetInterviewNum(DeptID, monthStartDate, monthEndDate);
        // 部门报道人数
        DataTable GetReportedNum = EmployeeTestBus.GetReportedNum(DeptID, monthStartDate, monthEndDate);
        // 部门迟到人数
        DataTable GetLatedNum = EmployeeTestBus.GetLatedNum(DeptID, monthStartDate, monthEndDate);
        // 部门早退人数
        DataTable GetLeaveEarlyNum = EmployeeTestBus.GetLeaveEarlyNum(DeptID, monthStartDate, monthEndDate);
        // 部门旷工人次
        DataTable GetAbsenteeismNum = EmployeeTestBus.GetAbsenteeismNum(DeptID, monthStartDate, monthEndDate);
        // 部门请假次数
        DataTable GetLeaveNum = EmployeeTestBus.GetLeaveNum(DeptID, monthStartDate, monthEndDate);
        // 部门调出人数
        DataTable GetDeptOutNum = EmployeeTestBus.GetDeptOutNum(DeptID, monthStartDate, monthEndDate);
        // 部门调入人数
        DataTable GetDeptInNum = EmployeeTestBus.GetDeptInNum(DeptID, monthStartDate, monthEndDate);
        // 部门离职人数
        DataTable GetSeparationNum = EmployeeTestBus.GetSeparationNum(DeptID, monthStartDate, monthEndDate);
        //获取某公司的所有部门
        DataTable dtData = EmployeeTestBus.GetDeptInfo (DeptID );
        if (dtData.Rows.Count > 0)
        {
             for (int i = 0; i < dtData.Rows.Count; i++)
            {
                DataRow sk = dt.NewRow();
                string deptName = dtData.Rows[i]["DeptName"] == null ? "" : dtData.Rows[i]["DeptName"].ToString();
                sk["UsedName"] = deptName;
                string ID = dtData.Rows[i]["ID"] == null ? "" : dtData.Rows[i]["ID"].ToString();
                // 部门招聘人数
                DataTable GetRequireNum2 = GetNewDataTable(GetRequireNum, "applyDept='" + ID  + "'");
                if (GetRequireNum2.Rows.Count > 0)
                {
                    sk["Attachmentname"] = GetRequireNum2.Rows[0]["PersonCount"] == null ? "" : GetRequireNum2.Rows[0]["PersonCount"].ToString();
                }
                // 部门面试人数
                DataTable GetInterviewNum2 = GetNewDataTable(GetInterviewNum, "DeptID='" + ID + "'");
                if (GetInterviewNum2.Rows.Count > 0)
                {
                    sk["EmployeeName"] = GetInterviewNum2.Rows[0]["InterviewNum"] == null ? "" : GetInterviewNum2.Rows[0]["InterviewNum"].ToString();
                }
                // 部门报道人数
                DataTable GetReportedNum2 = GetNewDataTable(GetReportedNum, "DeptID='" + ID + "'");
                if (GetReportedNum2.Rows.Count > 0)
                {
                sk["NameEn"] = GetReportedNum2.Rows[0]["ReportedNum"] == null ? "" : GetReportedNum2.Rows[0]["ReportedNum"].ToString();
                     }

                // 部门迟到人数
                DataTable GetLatedNum2 = GetNewDataTable(GetLatedNum, "DeptID='" + ID + "'");
                if (GetLatedNum2.Rows.Count > 0)
                {
                    sk["AccountNature"] = GetLatedNum2.Rows[0]["DelayManCount"] == null ? "" : GetLatedNum2.Rows[0]["DelayManCount"].ToString();
                }

                // 部门早退人数
                DataTable GetLeaveEarlyNum2 = GetNewDataTable(GetLeaveEarlyNum, "DeptID='" + ID + "'");
                if (GetLeaveEarlyNum2.Rows.Count > 0)
                {
                    sk["Height"] = GetLeaveEarlyNum2.Rows[0]["DelayManCount"] == null ? "" : GetLeaveEarlyNum2.Rows[0]["DelayManCount"].ToString();
                }

                // 部门旷工人次
                DataTable GetAbsenteeismNum2 = GetNewDataTable(GetAbsenteeismNum, "DeptID='" + ID + "'");
                if (GetAbsenteeismNum2.Rows.Count > 0)
                {
                    sk["Weight"] = GetAbsenteeismNum2.Rows[0]["Absentee"] == null ? "" : GetAbsenteeismNum2.Rows[0]["Absentee"].ToString();
                }
                // 部门请假次数
                DataTable GetLeaveNum2 = GetNewDataTable(GetLeaveNum, "DeptID='" + ID + "'");
                if (GetLeaveNum2.Rows.Count > 0)
                {
                    sk["Sight"] = GetLeaveNum2.Rows[0]["leaveCount"] == null ? "" : GetLeaveNum2.Rows[0]["leaveCount"].ToString();
                }
                 //部门迁出人数
                DataTable GetDeptOutNum2 = GetNewDataTable(GetDeptOutNum, "NowDeptID='" + ID + "'");
                string outNum = "0";
                 if (GetDeptOutNum2.Rows.Count > 0)
                {
                    outNum = GetDeptOutNum2.Rows[0]["countQian"] == null ? "0" : GetDeptOutNum2.Rows[0]["countQian"].ToString();
                }
                 //部门迁入人数
                 DataTable GetDeptInNum2 = GetNewDataTable(GetDeptInNum, "NewDeptID='" + ID + "'");
                 string intNum = "0";
                 if (GetDeptInNum2.Rows.Count > 0)
                 {
                     intNum = GetDeptInNum2.Rows[0]["countQian"] == null ? "0" : GetDeptInNum2.Rows[0]["countQian"].ToString();
                 }
                 string qianNum = (Convert.ToInt32(outNum) + Convert.ToInt32(intNum)).ToString();

                 if (qianNum == "0")
                 {
                     qianNum = "";
                 }
                 sk["EmployeeNo"] = qianNum;
                 
                       // 部门离职人次
                 DataTable GetSeparationNum2 = GetNewDataTable(GetSeparationNum, "DeptID='" + ID + "'");
                 if (GetSeparationNum2.Rows.Count > 0)
                {
                    sk["CompanyCD"] = GetSeparationNum2.Rows[0]["separateNum"] == null ? "" : GetSeparationNum2.Rows[0]["separateNum"].ToString();
                }

                dt.Rows.Add(sk);
            }

        }


        CrystalReportSource1.ReportDocument.SetDataSource(dt);
        CrystalReportSource1.DataBind();
        // CrystalReportViewer1是水晶报表浏览器，下面是给该浏览器赋上对像
        CrystalReportViewer1.ReportSource = CrystalReportSource1;
        // CrystalReportViewer1.DataBind(); 
    }
    private DataTable GetNewDataTable(DataTable dt, string condition)
    {
        DataTable newdt = new DataTable();
        newdt = dt.Clone();
        DataRow[] dr = dt.Select(condition);
        for (int i = 0; i < dr.Length; i++)
        {
            newdt.ImportRow((DataRow)dr[i]);
        }
        return newdt;//返回的查询结果
    }

 
    protected void btnExport_Click(object sender, EventArgs e)
    {
        //CrystalDecisions.Shared.DiskFileDestinationOptions DiskOpts = new CrystalDecisions.Shared.DiskFileDestinationOptions();
        //oRpt.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
        //switch (ddlFormat.SelectedItem.Text)
        //{
        //    case "Rich Text (RTF)":
        //        oRpt.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.RichText;//
        //        DiskOpts.DiskFileName = "c:\\Output.rtf";//
        //        break;
        //    case "Portable Document (PDF)":
        //        oRpt.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;//
        //        DiskOpts.DiskFileName = "c:\\Output.pdf";//
        //        break;
        //    case "MS Word (DOC)":
        //        oRpt.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.WordForWindows;//
        //        DiskOpts.DiskFileName = "c:\\Output.doc";//
        //        break;
        //    case "MS Excel (XLS)":
        //        oRpt.ExportOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.Excel;//
        //        DiskOpts.DiskFileName = "c:\\Output.xls";//
        //        break;
        //    default:
        //        break;
        //}
        //oRpt.ExportOptions.DestinationOptions = DiskOpts;
        //oRpt.Export();
    }

    protected void btnQuery_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Search();
    }
}

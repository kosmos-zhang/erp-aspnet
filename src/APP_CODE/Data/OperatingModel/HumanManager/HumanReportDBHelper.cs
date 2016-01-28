using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Model.Office.HumanManager ;
using XBase.Data.Office.HumanManager ;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using XBase.Common;
using System.Data.SqlTypes;
using XBase.Business.Common;
using XBase.Model.Common;
using XBase.Data.Common;
namespace XBase.Data.Office.HumanManager
{
     public  class HumanReportDBHelper
     {
         #region 人员状况明细月报搜索
         /// <summary>
         /// 人员状况明细月报
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
         public static DataTable EmployeeExaminationSelect(string CompanyCD, string DeptID,string monthStartDate ,string monthEndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                StringBuilder Sql = new StringBuilder();  
                                                                                                                                           //部门招聘人数                                             面试人数                                  报道人数                    
                Sql.AppendLine("select AA.DeptName, AA.ID as DeptID, isnull( A.PersonCount,0) as PersonCount ,isnull( B.InterviewNum,0) as InterviewNum,isnull(C.ReportedNum,0) as ReportedNum");
                                                                                      //    迟到人数                                                          早退人数                                                      旷工人次                                                请假次数             调出人数 
Sql.AppendLine(",isnull(D.DelayManCount,'') as DelayManCount,isnull(E.EalyManCount,'') as EalyManCount, isnull(F.Absentee,0) as Absentee, isnull(G.leaveCount,0) as leaveCount, isnull(H.countQianOut,0) as countQianOut");
                                                 //调入人数              离职人数
Sql.AppendLine(",  isnull( I. countQianIn,0) as countQianIn,isnull(J.separateNum,0) as separateNum,AA.ID ");
                Sql.AppendLine  ( "from ");

                Sql.AppendLine("(select distinct DeptName, ID from officedba.DeptInfo where  CompanyCD='" + CompanyCD + "'");
                if (!string.IsNullOrEmpty(DeptID))
                {
                    Sql.AppendLine(" AND   ID='" + DeptID + "'                  ");
                }
                 Sql.AppendLine("    ) AS AA");

               Sql.AppendLine  ( "FULL JOIN ");

              Sql.AppendLine  ( "(select  B.applyDept  as DeptID,sum(PersonCount) as  PersonCount from officedba.RectPlan A    ");
              Sql.AppendLine  ( "	left outer join officedba.RectGoal B                                      ");
              Sql.AppendLine  ( "  ON A.CompanyCD=B.CompanyCD AND A.PlanNo=B.PlanNo    ");
              Sql.AppendLine(" WHERE B.CompanyCD='"+CompanyCD+"'                     ");
              if (!string.IsNullOrEmpty(DeptID))
              {
                  Sql.AppendLine(" AND   B.applyDept='" + DeptID + "'                  ");
              
              }
              if (!string.IsNullOrEmpty(monthStartDate))
              {
                  Sql.AppendLine("  AND  A.StartDate>='"+monthStartDate+"'     and A.StartDate<='"+monthEndDate+"'              ");
                 
              }
              Sql.AppendLine(" GROUP BY B.applyDept     ) as A     ");                                                                                    //--部门招聘人数
              Sql.AppendLine  ( "ON A.DeptID=AA.ID");

              Sql.AppendLine  ( "full join ");


             Sql.AppendLine  ( "  ( select   E.DeptID  as DeptID ,COUNT(D.DeptID) as InterviewNum    from officedba.RectInterview C    ");
             Sql.AppendLine  ( "	LEFT OUTER JOIN officedba.EmployeeInfo D                            ");
             Sql.AppendLine  ( " ON D.CompanyCD=C.CompanyCD AND  C.StaffName=D.ID    ");
             Sql.AppendLine("    left outer join officedba.DeptQuarter E");
             Sql.AppendLine("On e.CompanyCD=C.CompanyCD AND  E.ID=  C.QuarterID");
             Sql.AppendLine("    WHERE C.CompanyCD='"+CompanyCD+"'   ");
             if (!string.IsNullOrEmpty(DeptID))
             {
                 Sql.AppendLine("  and    E.DeptID='" + DeptID + "'                  "); 
             }
             if (!string.IsNullOrEmpty(monthStartDate))
             {
                 Sql.AppendLine("  and   C.InterviewDate>='"+monthStartDate+"'     and C.InterviewDate<='"+monthEndDate+"'              "); 
             }
             Sql.AppendLine("GROUP BY E.DeptID    ) as B    ");                                                                                                 //--部门面试人数
             Sql.AppendLine  ( "on  B.DeptID=AA.ID  ");

             Sql.AppendLine  ( "full join");
             Sql.AppendLine  ( "(  select     ");
             Sql.AppendLine  ( "   DeptID                    ");
             Sql.AppendLine  ( "      ,count(DeptID) as ReportedNum  ");
             Sql.AppendLine(" from officedba. EmployeeInfo"); 
             Sql.AppendLine("     where     CompanyCD='"+CompanyCD+"' AND  Flag='1' ");
             if (!string.IsNullOrEmpty(DeptID))
             {
                 Sql.AppendLine(" AND   DeptID='"+DeptID+"'                  "); 
             }
             if (!string.IsNullOrEmpty(monthStartDate))
             {
                 Sql.AppendLine(" AND   EnterDate>='"+monthStartDate+"'     and EnterDate<='"+monthEndDate+"'              "); 
             }
             Sql.AppendLine("     GROUP BY  DeptID ) as C");                                                                                                    // --        部门报道人数
             Sql.AppendLine  ( "on  C.DeptID=AA.ID    ");
       
            Sql.AppendLine  ( "Full join (");

            Sql.AppendLine  ( "SELECT B.DeptID,COUNT(*) AS DelayManCount FROM (SELECT Date,CompanyCD,EmployeeID FROM officedba.DailyAttendance   ");
            Sql.AppendLine("                WHERE  CompanyCD='"+CompanyCD+"' and IsDelay='1' AND Date>='"+monthStartDate+"' AND Date<='"+monthEndDate+"'   "); 
            Sql.AppendLine  ( "          GROUP BY Date,CompanyCD,EmployeeID) A  LEFT OUTER JOIN officedba.EmployeeInfo B ON A.CompanyCD=B.CompanyCD  AND  A.EmployeeID=B.ID          ");
            Sql.AppendLine("where A.CompanyCD='"+CompanyCD+"'                   ");
            if (!string.IsNullOrEmpty(DeptID))
            {
                Sql.AppendLine(" AND   B.DeptID='" + DeptID + "'                  "); 
            }
            Sql.AppendLine("          GROUP BY  B.DeptID ) as D           ");                                                                                                //--部门迟到人数  
            Sql.AppendLine  ( "on  D.DeptID=AA.ID ");

            Sql.AppendLine  ( "full join (");

           Sql.AppendLine  ( " SELECT B.DeptID,COUNT(*) AS EalyManCount FROM (SELECT Date,CompanyCD,EmployeeID FROM officedba.DailyAttendance   ");
           Sql.AppendLine("   WHERE CompanyCD='"+CompanyCD+"'  and IsForwarOff='1' AND Date>='"+monthStartDate+"' AND Date<='"+monthEndDate+"'"); 
           Sql.AppendLine  ( "         GROUP BY Date,CompanyCD,EmployeeID) A  LEFT OUTER JOIN officedba.EmployeeInfo B ON A.CompanyCD=B.CompanyCD  AND  A.EmployeeID=B.ID ");
           Sql.AppendLine("where A.CompanyCD='"+CompanyCD+"'                  ");
           if (!string.IsNullOrEmpty(DeptID))
           {
               Sql.AppendLine("  AND  B.DeptID='" + DeptID + "'                  "); 
           }
           Sql.AppendLine("     GROUP BY  B.DeptID )   as E");                                                                                                              // --部门早退人数
           Sql.AppendLine  ( "on    E.DeptID=AA.ID    ");

           Sql.AppendLine  ( "full join ");

           Sql.AppendLine  ( "(");
           Sql.AppendLine("SELECT   A.DeptID,COUNT(*) AS Absentee FROM (SELECT X.ID,X.EmployeeName,X.CompanyCD,X.EmployeeNo,X.DeptID,X.EveryDay,Y.Date,Y.StartTime FROM");
           Sql.AppendLine("  (SELECT A.*,B.EveryDay   FROM  (select distinct b.ID,b.EmployeeName,b.DeptID,b.CompanyCD,b.EmployeeNo,b.QuarterID from officedba.EmployeeAttendanceSet a ");
           Sql.AppendLine(" LEFT OUTER JOIN  officedba.EmployeeInfo b  ON a.CompanyCD=b.CompanyCD and a.EmployeeID=b.ID WHERE a.CompanyCD ='"+CompanyCD+"'  and  b.Flag='1') A  ");
           Sql.AppendLine(" CROSS JOIN officedba.AttendanceEveryDay B   WHERE  A.CompanyCD='"+CompanyCD+"'  AND    B.EveryDay>='"+monthStartDate+"' AND B.EveryDay<='"+monthEndDate+"'");
           Sql.AppendLine(") X LEFT OUTER JOIN  officedba.DailyAttendance Y  ON X.CompanyCD=Y.CompanyCD AND  X.ID=Y.EmployeeID AND X.EveryDay=Y.Date) A  ");
           Sql.AppendLine(" WHERE   A.CompanyCD='"+CompanyCD+"' and  StartTime is null      ");
           if (!string.IsNullOrEmpty(DeptID))
           {
               Sql.AppendLine(" AND   A.DeptID='"+DeptID+"'                  "); 
           }
           Sql.AppendLine("GROUP BY A.CompanyCD,A.DeptID  )as F        ");                                                                            //--   部门旷工人次   
           Sql.AppendLine  ( "ON F.DeptID=AA.ID");

           Sql.AppendLine  ( "FULL JOIN (");

          Sql.AppendLine  ( "SELECT A.DeptID, COUNT(*) AS leaveCount  FROM(select a.*,b.DeptID from officedba.AttendanceApply  a left outer join officedba.EmployeeInfo  b ");
          Sql.AppendLine("ON  a.CompanyCD=b.CompanyCD AND a.EmployeeID=b.ID  WHERE   A.CompanyCD='"+CompanyCD+"'    AND a.Flag='1' )A ");
          Sql.AppendLine(" where  A.CompanyCD ='"+CompanyCD+"'   and   A.ApplyDate>='"+monthStartDate+"' and A.ApplyDate<='"+monthEndDate+"' ");
          if (!string.IsNullOrEmpty(DeptID))
          {
              Sql.AppendLine(" AND   A.DeptID='" + DeptID + "'                  "); 
          }
         Sql.AppendLine("GROUP BY A.CompanyCD, A.DeptID) as G");                                                                                                 // --部门请假次数
         Sql.AppendLine  ( "On G.DeptID=AA.ID");

         Sql.AppendLine  ( "full join(");

         Sql.AppendLine  ( "select NowDeptID,count(NowDeptID) as countQianOut from officedba.EmplApplyNotify  ");
         Sql.AppendLine("where CompanyCD='"+CompanyCD+"'   and BillStatus='2'  and OutDate>='"+monthStartDate+"' and  OutDate<='"+monthEndDate+"'");
         if (!string.IsNullOrEmpty(DeptID))
         {
             Sql.AppendLine(" AND    NowDeptID='"+DeptID+"'                  "); 
         }
         Sql.AppendLine("group by NowDeptID  ) as H                      ");                                                                                                        // -- 部门调出人数    
         Sql.AppendLine  ( "On h.NowDeptID=AA.ID      ");
 
         Sql.AppendLine  ( "full join(");

         Sql.AppendLine  ( "select NewDeptID,count(NewDeptID) as countQianIn from officedba.EmplApplyNotify  ");
         Sql.AppendLine("where CompanyCD='"+CompanyCD+"'   and BillStatus='2'  and IntDate>='"+monthStartDate+"' and  IntDate<='"+monthEndDate+"'");
         if (!string.IsNullOrEmpty(DeptID))
         {
             Sql.AppendLine(" AND    NewDeptID='"+DeptID+"'                  "); 
         }
         Sql.AppendLine  ( "group by NewDeptID ) as I");
         Sql.AppendLine  ( "on I.NewDeptID=AA.ID                          ");                                                                                                                  //   -- 部门调入人数 

         Sql.AppendLine  ( "full join(");

         Sql.AppendLine  ( "select b.DeptID,count(b.DeptID) as separateNum from officedba.MoveNotify a left outer join officedba.EmployeeInfo b on b.CompanyCD=a.CompanyCD and a.EmployeeID=b.ID");
         Sql.AppendLine("where a.CompanyCD='"+CompanyCD+"'   and  a.BillStatus='2' and OutDate>='"+monthStartDate+"' and  OutDate<='"+monthEndDate+"'");
         if (!string.IsNullOrEmpty(DeptID))
         {
             Sql.AppendLine(" AND   b.DeptID ='" + DeptID + "'                  "); 
         }
         Sql.AppendLine  ( "group by b.DeptID ) as J");
         Sql.AppendLine  ( "on J.DeptID=AA.ID                ");                                                                                                                                       //      --部门离职人数
         Sql.AppendLine("where AA.ID IS NOT NULL            ");         
         //return SqlHelper.CreateSqlByPageExcuteSql(Sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
         return SqlHelper.ExecuteSql(Sql.ToString());
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        #endregion


         #region 人员状况明细月报搜索
         /// <summary>
         /// 人员状况明细月报
         /// </summary>
         /// <param name="CompanyCD"></param>
         /// <param name="DeptID"></param>
         /// <param name="pageIndex"></param>
         /// <param name="pageCount"></param>
         /// <param name="ord"></param>
         /// <param name="TotalCount"></param>
         /// <returns></returns>
         public static DataTable EmployeeExamination2Select(string CompanyCD, string DeptID, string monthStartDate, string monthEndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
         {
             try
             {
                 StringBuilder Sql = new StringBuilder();
                 //部门招聘人数                                             面试人数                                  报道人数                    
                 Sql.AppendLine("select AA.DeptName, AA.ID as DeptID, isnull( A.PersonCount,0) as PersonCount ,isnull( B.InterviewNum,0) as InterviewNum,isnull(C.ReportedNum,0) as ReportedNum");
                 //    迟到人数                                                          早退人数                                                      旷工人次                                                请假次数             调出人数 
                 Sql.AppendLine(",isnull(D.DelayManCount,'') as DelayManCount,isnull(E.EalyManCount,'') as EalyManCount, isnull(F.Absentee,0) as Absentee, isnull(G.leaveCount,0) as leaveCount, isnull(H.countQianOut,0) as countQianOut");
                 //调入人数              离职人数
                 Sql.AppendLine(",  isnull( I. countQianIn,0) as countQianIn,isnull(J.separateNum,0) as separateNum,AA.ID ");
                 Sql.AppendLine("from ");

                 Sql.AppendLine("(select distinct DeptName, ID from officedba.DeptInfo where  CompanyCD='" + CompanyCD + "'");
                 if (!string.IsNullOrEmpty(DeptID))
                 {
                     Sql.AppendLine(" AND   ID='" + DeptID + "'                  ");
                 }
                 Sql.AppendLine("    ) AS AA");

                 Sql.AppendLine("FULL JOIN ");

                 Sql.AppendLine("(select  B.applyDept  as DeptID,sum(PersonCount) as  PersonCount from officedba.RectPlan A    ");
                 Sql.AppendLine("	left outer join officedba.RectGoal B                                      ");
                 Sql.AppendLine("  ON A.CompanyCD=B.CompanyCD AND A.PlanNo=B.PlanNo    ");
                 Sql.AppendLine(" WHERE B.CompanyCD='" + CompanyCD + "'                     ");
                 if (!string.IsNullOrEmpty(DeptID))
                 {
                     Sql.AppendLine(" AND   B.applyDept='" + DeptID + "'                  ");

                 }
                 if (!string.IsNullOrEmpty(monthStartDate))
                 {
                     Sql.AppendLine("  AND  A.StartDate>='" + monthStartDate + "'     and A.StartDate<='" + monthEndDate + "'              ");

                 }
                 Sql.AppendLine(" GROUP BY B.applyDept     ) as A     ");                                                                                    //--部门招聘人数
                 Sql.AppendLine("ON A.DeptID=AA.ID");

                 Sql.AppendLine("full join ");


                 Sql.AppendLine("  ( select   E.DeptID  as DeptID ,COUNT(D.DeptID) as InterviewNum    from officedba.RectInterview C    ");
                 Sql.AppendLine("	LEFT OUTER JOIN officedba.EmployeeInfo D                            ");
                 Sql.AppendLine(" ON D.CompanyCD=C.CompanyCD AND  C.StaffName=D.ID    ");
                 Sql.AppendLine("    left outer join officedba.DeptQuarter E");
                 Sql.AppendLine("On e.CompanyCD=C.CompanyCD AND  E.ID=  C.QuarterID");
                 Sql.AppendLine("    WHERE C.CompanyCD='" + CompanyCD + "'   ");
                 if (!string.IsNullOrEmpty(DeptID))
                 {
                     Sql.AppendLine("  and    E.DeptID='" + DeptID + "'                  ");
                 }
                 if (!string.IsNullOrEmpty(monthStartDate))
                 {
                     Sql.AppendLine("  and   C.InterviewDate>='" + monthStartDate + "'     and C.InterviewDate<='" + monthEndDate + "'              ");
                 }
                 Sql.AppendLine("GROUP BY E.DeptID    ) as B    ");                                                                                                 //--部门面试人数
                 Sql.AppendLine("on  B.DeptID=AA.ID  ");

                 Sql.AppendLine("full join");
                 Sql.AppendLine("(  select     ");
                 Sql.AppendLine("   DeptID                    ");
                 Sql.AppendLine("      ,count(DeptID) as ReportedNum  ");
                 Sql.AppendLine(" from officedba. EmployeeInfo");
                 Sql.AppendLine("     where     CompanyCD='" + CompanyCD + "' AND  Flag='1' ");
                 if (!string.IsNullOrEmpty(DeptID))
                 {
                     Sql.AppendLine(" AND   DeptID='" + DeptID + "'                  ");
                 }
                 if (!string.IsNullOrEmpty(monthStartDate))
                 {
                     Sql.AppendLine(" AND   EnterDate>='" + monthStartDate + "'     and EnterDate<='" + monthEndDate + "'              ");
                 }
                 Sql.AppendLine("     GROUP BY  DeptID ) as C");                                                                                                    // --        部门报道人数
                 Sql.AppendLine("on  C.DeptID=AA.ID    ");

                 Sql.AppendLine("Full join (");

                 Sql.AppendLine("SELECT B.DeptID,COUNT(*) AS DelayManCount FROM (SELECT Date,CompanyCD,EmployeeID FROM officedba.DailyAttendance   ");
                 Sql.AppendLine("                WHERE  CompanyCD='" + CompanyCD + "' and IsDelay='1' AND Date>='" + monthStartDate + "' AND Date<='" + monthEndDate + "'   ");
                 Sql.AppendLine("          GROUP BY Date,CompanyCD,EmployeeID) A  LEFT OUTER JOIN officedba.EmployeeInfo B ON A.CompanyCD=B.CompanyCD  AND  A.EmployeeID=B.ID          ");
                 Sql.AppendLine("where A.CompanyCD='" + CompanyCD + "'                   ");
                 if (!string.IsNullOrEmpty(DeptID))
                 {
                     Sql.AppendLine(" AND   B.DeptID='" + DeptID + "'                  ");
                 }
                 Sql.AppendLine("          GROUP BY  B.DeptID ) as D           ");                                                                                                //--部门迟到人数  
                 Sql.AppendLine("on  D.DeptID=AA.ID ");

                 Sql.AppendLine("full join (");

                 Sql.AppendLine(" SELECT B.DeptID,COUNT(*) AS EalyManCount FROM (SELECT Date,CompanyCD,EmployeeID FROM officedba.DailyAttendance   ");
                 Sql.AppendLine("   WHERE CompanyCD='" + CompanyCD + "'  and IsForwarOff='1' AND Date>='" + monthStartDate + "' AND Date<='" + monthEndDate + "'");
                 Sql.AppendLine("         GROUP BY Date,CompanyCD,EmployeeID) A  LEFT OUTER JOIN officedba.EmployeeInfo B ON A.CompanyCD=B.CompanyCD  AND  A.EmployeeID=B.ID ");
                 Sql.AppendLine("where A.CompanyCD='" + CompanyCD + "'                  ");
                 if (!string.IsNullOrEmpty(DeptID))
                 {
                     Sql.AppendLine("  AND  B.DeptID='" + DeptID + "'                  ");
                 }
                 Sql.AppendLine("     GROUP BY  B.DeptID )   as E");                                                                                                              // --部门早退人数
                 Sql.AppendLine("on    E.DeptID=AA.ID    ");

                 Sql.AppendLine("full join ");

                 Sql.AppendLine("(");
                 Sql.AppendLine("SELECT   A.DeptID,COUNT(*) AS Absentee FROM (SELECT X.ID,X.EmployeeName,X.CompanyCD,X.EmployeeNo,X.DeptID,X.EveryDay,Y.Date,Y.StartTime FROM");
                 Sql.AppendLine("  (SELECT A.*,B.EveryDay   FROM  (select distinct b.ID,b.EmployeeName,b.DeptID,b.CompanyCD,b.EmployeeNo,b.QuarterID from officedba.EmployeeAttendanceSet a ");
                 Sql.AppendLine(" LEFT OUTER JOIN  officedba.EmployeeInfo b  ON a.CompanyCD=b.CompanyCD and a.EmployeeID=b.ID WHERE a.CompanyCD ='" + CompanyCD + "'  and  b.Flag='1') A  ");
                 Sql.AppendLine(" CROSS JOIN officedba.AttendanceEveryDay B   WHERE  A.CompanyCD='" + CompanyCD + "'  AND    B.EveryDay>='" + monthStartDate + "' AND B.EveryDay<='" + monthEndDate + "'");
                 Sql.AppendLine(") X LEFT OUTER JOIN  officedba.DailyAttendance Y  ON X.CompanyCD=Y.CompanyCD AND  X.ID=Y.EmployeeID AND X.EveryDay=Y.Date) A  ");
                 Sql.AppendLine(" WHERE   A.CompanyCD='" + CompanyCD + "' and  StartTime is null      ");
                 if (!string.IsNullOrEmpty(DeptID))
                 {
                     Sql.AppendLine(" AND   A.DeptID='" + DeptID + "'                  ");
                 }
                 Sql.AppendLine("GROUP BY A.CompanyCD,A.DeptID  )as F        ");                                                                            //--   部门旷工人次   
                 Sql.AppendLine("ON F.DeptID=AA.ID");

                 Sql.AppendLine("FULL JOIN (");

                 Sql.AppendLine("SELECT A.DeptID, COUNT(*) AS leaveCount  FROM(select a.*,b.DeptID from officedba.AttendanceApply  a left outer join officedba.EmployeeInfo  b ");
                 Sql.AppendLine("ON  a.CompanyCD=b.CompanyCD AND a.EmployeeID=b.ID  WHERE   A.CompanyCD='" + CompanyCD + "'    AND a.Flag='1' )A ");
                 Sql.AppendLine(" where  A.CompanyCD ='" + CompanyCD + "'   and   A.ApplyDate>='" + monthStartDate + "' and A.ApplyDate<='" + monthEndDate + "' ");
                 if (!string.IsNullOrEmpty(DeptID))
                 {
                     Sql.AppendLine(" AND   A.DeptID='" + DeptID + "'                  ");
                 }
                 Sql.AppendLine("GROUP BY A.CompanyCD, A.DeptID) as G");                                                                                                 // --部门请假次数
                 Sql.AppendLine("On G.DeptID=AA.ID");

                 Sql.AppendLine("full join(");

                 Sql.AppendLine("select NowDeptID,count(NowDeptID) as countQianOut from officedba.EmplApplyNotify  ");
                 Sql.AppendLine("where CompanyCD='" + CompanyCD + "'   and BillStatus='2'  and OutDate>='" + monthStartDate + "' and  OutDate<='" + monthEndDate + "'");
                 if (!string.IsNullOrEmpty(DeptID))
                 {
                     Sql.AppendLine(" AND    NowDeptID='" + DeptID + "'                  ");
                 }
                 Sql.AppendLine("group by NowDeptID  ) as H                      ");                                                                                                        // -- 部门调出人数    
                 Sql.AppendLine("On h.NowDeptID=AA.ID      ");

                 Sql.AppendLine("full join(");

                 Sql.AppendLine("select NewDeptID,count(NewDeptID) as countQianIn from officedba.EmplApplyNotify  ");
                 Sql.AppendLine("where CompanyCD='" + CompanyCD + "'   and BillStatus='2'  and IntDate>='" + monthStartDate + "' and  IntDate<='" + monthEndDate + "'");
                 if (!string.IsNullOrEmpty(DeptID))
                 {
                     Sql.AppendLine(" AND    NewDeptID='" + DeptID + "'                  ");
                 }
                 Sql.AppendLine("group by NewDeptID ) as I");
                 Sql.AppendLine("on I.NewDeptID=AA.ID                          ");                                                                                                                  //   -- 部门调入人数 

                 Sql.AppendLine("full join(");

                 Sql.AppendLine("select b.DeptID,count(b.DeptID) as separateNum from officedba.MoveNotify a left outer join officedba.EmployeeInfo b on b.CompanyCD=a.CompanyCD and a.EmployeeID=b.ID");
                 Sql.AppendLine("where a.CompanyCD='" + CompanyCD + "'   and  a.BillStatus='2' and OutDate>='" + monthStartDate + "' and  OutDate<='" + monthEndDate + "'");
                 if (!string.IsNullOrEmpty(DeptID))
                 {
                     Sql.AppendLine(" AND   b.DeptID ='" + DeptID + "'                  ");
                 }
                 Sql.AppendLine("group by b.DeptID ) as J");
                 Sql.AppendLine("on J.DeptID=AA.ID                ");                                                                                                                                       //      --部门离职人数
                 Sql.AppendLine("where AA.ID IS NOT NULL            ");
                 return SqlHelper.CreateSqlByPageExcuteSql(Sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
               //  return SqlHelper.ExecuteSql(Sql.ToString());
             }
             catch (Exception ex)
             {
                 string sss = ex.Message;
                 return null;
             }
         }
         #endregion
         #region 人员状况明细月报  招聘 明细
         /// <summary>
         /// 人员状况明细月报   面试明细
         /// </summary>
         /// <param name="CompanyCD"></param>
         /// <param name="DeptID"></param>
         /// <param name="pageIndex"></param>
         /// <param name="pageCount"></param>
         /// <param name="ord"></param>
         /// <param name="TotalCount"></param>
         /// <returns></returns>
         public static DataTable EmployeeConditionByZhaoPing(string CompanyCD, string DeptID, string monthStartDate, string monthEndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
         {
             try
             {
              StringBuilder Sql = new StringBuilder();
              Sql.AppendLine(" Select ");
              Sql.AppendLine("isnull( B.applyDept,'0')  as DeptID");
             Sql.AppendLine(",isnull(C.DeptName,'') as DeptName");//招聘部门
             Sql.AppendLine(",isnull(B.PersonCount,'0')  as  PersonCount");//招聘人数
             Sql.AppendLine(",convert(varchar(10),A.StartDate,21) AS StartDate");//招聘开始日期
             Sql.AppendLine(",convert(varchar(10),A.EndDate,21) AS EndDate");//招聘结束日期
             Sql.AppendLine(",isnull(D.EmployeeName,'') as EmployeeName");//负责人
             Sql.AppendLine("from officedba.RectPlan A    ");
             Sql.AppendLine("left outer join officedba.RectGoal B                                  ");    
             Sql.AppendLine("ON A.CompanyCD=B.CompanyCD AND A.PlanNo=B.PlanNo ");
             Sql.AppendLine("LEFT OUTer JOIN   OFFICEDBA.DEPTINFO C");
             Sql.AppendLine("ON C.CompanyCD=A.CompanyCD AND C.ID=B.applyDept");
             Sql.AppendLine("LEFT OUTER JOIN OFFICEDBA.EMPLOYEEINFO D");
             Sql.AppendLine("ON D.CompanyCD=A.CompanyCD AND A.Principal=D.ID");
             Sql.AppendLine("WHERE A.CompanyCD=@CompanyCD and B.applyDept is not null ");
             //定义查询的命令
             SqlCommand comm = new SqlCommand();
             //添加公司代码参数
             comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

                 if (!string.IsNullOrEmpty(DeptID))
                 {
                     Sql.AppendLine(" AND   B.applyDept=@DeptID               ");
                     comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
                 }
                 if (!string.IsNullOrEmpty(monthStartDate))
                 {
                     Sql.AppendLine("  AND  A.StartDate>=@monthStartDate   and A.StartDate<=@monthEndDate            ");
                     comm.Parameters.Add(SqlHelper.GetParameterFromString("@monthStartDate", monthStartDate));
                     comm.Parameters.Add(SqlHelper.GetParameterFromString("@monthEndDate", monthEndDate));
                 }
                 comm.CommandText = Sql.ToString();
                 return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
             }
             catch (Exception ex)
             {
                 string sss = ex.Message;
                 return null;
             }
         }
         #endregion
         
                     #region 人员状况明细月报  面试人数明细
         /// <summary>
         /// 人员状况明细月报   面试人数明细
         /// </summary>
         /// <param name="CompanyCD"></param>
         /// <param name="DeptID"></param>
         /// <param name="pageIndex"></param>
         /// <param name="pageCount"></param>
         /// <param name="ord"></param>
         /// <param name="TotalCount"></param>
         /// <returns></returns>
         public static DataTable EmployeeConditionByMianShi(string CompanyCD, string DeptID, string monthStartDate, string monthEndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
         {
             try
             {
              StringBuilder Sql = new StringBuilder(); 
             Sql.AppendLine("select ");  
             Sql.AppendLine("E.DeptID  as DeptID ");//
             Sql.AppendLine(",isnull(D.EmployeeName,'') as EmployeeName");//面试者
             Sql.AppendLine(",isnull(E.QuarterName,'') as QuarterName");//招聘岗位 
             Sql.AppendLine(",isnull(C.TestScore,'0') AS TestScore");//初试分数
             Sql.AppendLine(",isnull(F.DeptName,'') as DeptName");//招聘岗位的相关部门
             Sql.AppendLine(",CASE WHEN C.InterviewResult='1' THEN '列入考虑' ");
             Sql.AppendLine("     WHEN C.InterviewResult='2' THEN '不予考虑' ");
             Sql.AppendLine("    WHEN C.InterviewResult IS NULL THEN '' ");
             Sql.AppendLine("END AS InterviewResultContent");//初试结果
             Sql.AppendLine(",CASE WHEN C.FinalResult='0' THEN '不予考虑' ");
             Sql.AppendLine("   WHEN C.FinalResult='1' THEN '拟予试用' ");
             Sql.AppendLine("    WHEN C.FinalResult IS NULL THEN '' ");
             Sql.AppendLine(" END AS FinalResultContent");//复试结果
             Sql.AppendLine(",isnull(convert(varchar(10),C.InterviewDate,21),'') as InterviewDate");//初试时间
             Sql.AppendLine(",isnull(convert(varchar(10),C.CheckDate,21),'') as CheckDate");//复试时间
             Sql.AppendLine("from officedba.RectInterview C    ");
             Sql.AppendLine("LEFT OUTER JOIN officedba.EmployeeInfo D                            ");
             Sql.AppendLine("ON D.CompanyCD=C.CompanyCD AND  C.StaffName=D.ID  ");
             Sql.AppendLine("left outer join officedba.DeptQuarter E");
             Sql.AppendLine("On e.CompanyCD=C.CompanyCD AND  E.ID=  C.QuarterID");
             Sql.AppendLine("left outer join officedba.DeptInfo F ");
             Sql.AppendLine("on F.CompanyCD=C.CompanyCD and F.id=E.DeptID ");
             Sql.AppendLine("WHERE C.CompanyCD=@CompanyCD  and  E.DeptID is not null    ");


             //定义查询的命令
             SqlCommand comm = new SqlCommand();
             //添加公司代码参数
             comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
    

                 if (!string.IsNullOrEmpty(DeptID))
                 {
                     Sql.AppendLine(" AND    E.DeptID=@DeptID               ");
                     comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
                 }
                 if (!string.IsNullOrEmpty(monthStartDate))
                 {
                     Sql.AppendLine("  AND  C.InterviewDate>=@monthStartDate   and  C.InterviewDate<=@monthEndDate            ");
                     comm.Parameters.Add(SqlHelper.GetParameterFromString("@monthStartDate", monthStartDate));
                     comm.Parameters.Add(SqlHelper.GetParameterFromString("@monthEndDate", monthEndDate));
                 }
                 comm.CommandText = Sql.ToString();
                 return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
             }
             catch (Exception ex)
             {
                 string sss = ex.Message;
                 return null;
             }
         }
         #endregion

         #region 人员状况明细月报  报道人数明细
         /// <summary>
         /// 人员状况明细月报   报道人数明细
         /// </summary>
         /// <param name="CompanyCD"></param>
         /// <param name="DeptID"></param>
         /// <param name="pageIndex"></param>
         /// <param name="pageCount"></param>
         /// <param name="ord"></param>
         /// <param name="TotalCount"></param>
         /// <returns></returns>
         public static DataTable EmployeeConditionByBaoDao(string CompanyCD, string DeptID, string monthStartDate, string monthEndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
         {
             try
             {
              StringBuilder Sql = new StringBuilder(); 
                Sql.AppendLine("select  ");
                Sql.AppendLine("A.DeptID ");
                Sql.AppendLine(",A.ID AS EmployeeID  ");
                Sql.AppendLine(",ISNULL(A.EmployeeName,'') as EmployeeName ");
                Sql.AppendLine(",ISNULL(B.DeptName,'') as DeptName ");
                Sql.AppendLine(",isnull(convert(varchar(10),A.EnterDate,21),'') as EnterDate ");
                Sql.AppendLine("from officedba. EmployeeInfo A ");
                Sql.AppendLine("LEFT OUTER JOIN officedba. DeptInfo B ");
                Sql.AppendLine("on A.CompanyCD=B.CompanyCD AND A.deptID=B.ID  ");
                Sql.AppendLine("WHERE A.CompanyCD=@CompanyCD  and  A.DeptID is not null and   A.Flag='1'    ");


             //定义查询的命令
             SqlCommand comm = new SqlCommand();
             //添加公司代码参数
             comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
    

                 if (!string.IsNullOrEmpty(DeptID))
                 {
                     Sql.AppendLine(" AND    A.DeptID=@DeptID               ");
                     comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
                 }
                 if (!string.IsNullOrEmpty(monthStartDate))
                 {
                     Sql.AppendLine("  AND  A.EnterDate>=@monthStartDate   and  A.EnterDate<=@monthEndDate            ");
                     comm.Parameters.Add(SqlHelper.GetParameterFromString("@monthStartDate", monthStartDate));
                     comm.Parameters.Add(SqlHelper.GetParameterFromString("@monthEndDate", monthEndDate));
                 }
                 comm.CommandText = Sql.ToString();
                 return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
             }
             catch (Exception ex)
             {
                 string sss = ex.Message;
                 return null;
             }
         }
         #endregion

         #region 人员状况明细月报  迟到人数明细
         /// <summary>
         /// 人员状况明细月报   迟到人数明细
         /// </summary>
         /// <param name="CompanyCD"></param>
         /// <param name="DeptID"></param>
         /// <param name="pageIndex"></param>
         /// <param name="pageCount"></param>
         /// <param name="ord"></param>
         /// <param name="TotalCount"></param>
         /// <returns></returns>
         public static DataTable EmployeeConditionByChiDao(string CompanyCD, string DeptID, string monthStartDate, string monthEndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
         {
             try
             {
              StringBuilder Sql = new StringBuilder(); 
                Sql.AppendLine("SELECT");
                Sql.AppendLine("convert(varchar(10),A.Date,21) as Date");
                Sql.AppendLine(",isnull(A.DelayTimeLong,'0') as DelayTimeLong");
                Sql.AppendLine(",isnull(A.StartTime,'') as StartTime");
                Sql.AppendLine(",A.EmployeeID");
                Sql.AppendLine(",B.DeptID");
                Sql.AppendLine(",isnull(B.EmployeeName,'')as EmployeeName");
                Sql.AppendLine(",isnull(C.DeptName,'')as DeptName");
                Sql.AppendLine("FROM officedba.DailyAttendance  A ");
                Sql.AppendLine("Left outer join officedba.EmployeeInfo B");
                Sql.AppendLine("ON A.CompanyCD=B.CompanyCD AND B.ID=A.EmployeeID");
                Sql.AppendLine("LEFT OUTER JOIN officedba.DeptInfo C");
                Sql.AppendLine("on C.CompanyCD=B.CompanyCD and C.ID=B.DeptID");
                Sql.AppendLine("WHERE  A.CompanyCD=@CompanyCD AND A.IsDelay='1' AND B.DeptID IS NOT NULL  ");


             //定义查询的命令
             SqlCommand comm = new SqlCommand();
             //添加公司代码参数
             comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
    

                 if (!string.IsNullOrEmpty(DeptID))
                 {
                     Sql.AppendLine(" AND    B.DeptID=@DeptID               ");
                     comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
                 }
                 if (!string.IsNullOrEmpty(monthStartDate))
                 {
                     Sql.AppendLine("  AND  A.Date>=@monthStartDate   and  A.Date<=@monthEndDate            ");
                     comm.Parameters.Add(SqlHelper.GetParameterFromString("@monthStartDate", monthStartDate));
                     comm.Parameters.Add(SqlHelper.GetParameterFromString("@monthEndDate", monthEndDate));
                 }
                 comm.CommandText = Sql.ToString();
                 return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
             }
             catch (Exception ex)
             {
                 string sss = ex.Message;
                 return null;
             }
         }
         #endregion
         
             #region 人员状况明细月报  迟到人数明细
         /// <summary>
         /// 人员状况明细月报   迟到人数明细
         /// </summary>
         /// <param name="CompanyCD"></param>
         /// <param name="DeptID"></param>
         /// <param name="pageIndex"></param>
         /// <param name="pageCount"></param>
         /// <param name="ord"></param>
         /// <param name="TotalCount"></param>
         /// <returns></returns>
         public static DataTable EmployeeConditionByZaoTui(string CompanyCD, string DeptID, string monthStartDate, string monthEndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
         {
             try
             {
              StringBuilder Sql = new StringBuilder(); 
                Sql.AppendLine("SELECT");
                Sql.AppendLine("convert(varchar(10),A.Date,21) as Date");
                Sql.AppendLine(",isnull(A.ForWardOffTimeLong,'0') as DelayTimeLong");
                Sql.AppendLine(",isnull(A.EndTime,'') as StartTime");
                Sql.AppendLine(",A.EmployeeID");
                Sql.AppendLine(",B.DeptID");
                Sql.AppendLine(",isnull(B.EmployeeName,'')as EmployeeName");
                Sql.AppendLine(",isnull(C.DeptName,'')as DeptName");
                Sql.AppendLine("FROM officedba.DailyAttendance  A ");
                Sql.AppendLine("Left outer join officedba.EmployeeInfo B");
                Sql.AppendLine("ON A.CompanyCD=B.CompanyCD AND B.ID=A.EmployeeID");
                Sql.AppendLine("LEFT OUTER JOIN officedba.DeptInfo C");
                Sql.AppendLine("on C.CompanyCD=B.CompanyCD and C.ID=B.DeptID");
                Sql.AppendLine("WHERE  A.CompanyCD=@CompanyCD AND A.IsForwarOff='1' AND B.DeptID IS NOT NULL  ");


             //定义查询的命令
             SqlCommand comm = new SqlCommand();
             //添加公司代码参数
             comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
    

                 if (!string.IsNullOrEmpty(DeptID))
                 {
                     Sql.AppendLine(" AND    B.DeptID=@DeptID               ");
                     comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
                 }
                 if (!string.IsNullOrEmpty(monthStartDate))
                 {
                     Sql.AppendLine("  AND  A.Date>=@monthStartDate   and  A.Date<=@monthEndDate            ");
                     comm.Parameters.Add(SqlHelper.GetParameterFromString("@monthStartDate", monthStartDate));
                     comm.Parameters.Add(SqlHelper.GetParameterFromString("@monthEndDate", monthEndDate));
                 }
                 comm.CommandText = Sql.ToString();
                 return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
             }
             catch (Exception ex)
             {
                 string sss = ex.Message;
                 return null;
             }
         }
         #endregion 
         #region 人员状况明细月报  旷工人数明细
         /// <summary>
         /// 人员状况明细月报   旷工人数明细
         /// </summary>
         /// <param name="CompanyCD"></param>
         /// <param name="DeptID"></param>
         /// <param name="pageIndex"></param>
         /// <param name="pageCount"></param>
         /// <param name="ord"></param>
         /// <param name="TotalCount"></param>
         /// <returns></returns>
         public static DataTable EmployeeConditionByKuangGong(string CompanyCD, string DeptID, string monthStartDate, string monthEndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
         {
             try
             {
                 SqlCommand comm = new SqlCommand();
                    StringBuilder Sql = new StringBuilder();
                    Sql.AppendLine("SELECT   A.*,ISNULL(B.DeptName,'') AS DeptName FROM  (");
                    Sql.AppendLine("SELECT X.ID as EmployeeID,isnull(X.EmployeeName,'') as EmployeeName,X.CompanyCD,X.EmployeeNo,X.DeptID,ISNULL(CONVERT(VARCHAR(10), X.EveryDay,21),'') AS EveryDay ,Y.Date,Y.StartTime ");
                   Sql.AppendLine("FROM");
                   Sql.AppendLine(" (  SELECT A.*,B.EveryDay ");
                   Sql.AppendLine("    FROM  (");
                  Sql.AppendLine("		select distinct b.ID,b.EmployeeName,b.DeptID,b.CompanyCD,b.EmployeeNo,b.QuarterID");
                  Sql.AppendLine("		from officedba.EmployeeAttendanceSet a ");
                  Sql.AppendLine("		 LEFT OUTER JOIN  officedba.EmployeeInfo b ");
                  Sql.AppendLine("		 ON a.CompanyCD=b.CompanyCD and a.EmployeeID=b.ID ");
                  Sql.AppendLine("		WHERE a.CompanyCD =@CompanyCD and  b.Flag='1'");
                   Sql.AppendLine("         ) A  ");
                  Sql.AppendLine("     CROSS JOIN officedba.AttendanceEveryDay B  ");
                  Sql.AppendLine("     WHERE  A.CompanyCD=@CompanyCD  ");
                  if (!string.IsNullOrEmpty(monthStartDate))
                  {
                      Sql.AppendLine("  AND    B.EveryDay>=@monthStartDate AND B.EveryDay<=@monthEndDate       ");
                      comm.Parameters.Add(SqlHelper.GetParameterFromString("@monthStartDate", monthStartDate));
                      comm.Parameters.Add(SqlHelper.GetParameterFromString("@monthEndDate", monthEndDate));
                  }
                  Sql.AppendLine("  ) X ");
                  Sql.AppendLine("LEFT OUTER JOIN  officedba.DailyAttendance Y  ");
                  Sql.AppendLine("ON X.CompanyCD=Y.CompanyCD AND  X.ID=Y.EmployeeID AND X.EveryDay=Y.Date");
                  Sql.AppendLine("                                             ) A  ");
                  Sql.AppendLine("Left outer join officedba.DeptInfo B");
                  Sql.AppendLine("On A.CompanyCD=B.CompanyCD and A.DeptID=B.ID ");
                  Sql.AppendLine(" WHERE   A.CompanyCD=@CompanyCD  and  StartTime is null      ");


             //定义查询的命令
      
             //添加公司代码参数
             comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
    

                 if (!string.IsNullOrEmpty(DeptID))
                 {
                     Sql.AppendLine(" AND  A.DeptID=@DeptID               ");
                     comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
                 }
            
                 comm.CommandText = Sql.ToString();
                 return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
             }
             catch (Exception ex)
             {
                 string sss = ex.Message;
                 return null;
             }
         }
         #endregion

         #region 人员状况明细月报  请假人数明细
         /// <summary>
         /// 人员状况明细月报   请假人数明细
         /// </summary>
         /// <param name="CompanyCD"></param>
         /// <param name="DeptID"></param>
         /// <param name="pageIndex"></param>
         /// <param name="pageCount"></param>
         /// <param name="ord"></param>
         /// <param name="TotalCount"></param>
         /// <returns></returns>
         public static DataTable EmployeeConditionByQingjia(string CompanyCD, string DeptID, string monthStartDate, string monthEndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
         {
             try
             {
                 SqlCommand comm = new SqlCommand();
                 StringBuilder Sql = new StringBuilder();
                 Sql.AppendLine("SELECT ");
                Sql.AppendLine("A.DeptID");
                Sql.AppendLine(",isnull(convert(varchar(10),A.ApplyDate,21),'') as ApplyDate");
                Sql.AppendLine(", isnull(B.DeptName,'') as DeptName ");
                Sql.AppendLine(",A.EmployeeName");
                Sql.AppendLine(",isnull(convert(varchar(10),A.StartDate,21),'')+' '+isnull(A.StartTime,'')   as StartDate");
                Sql.AppendLine(",isnull(convert(varchar(10),A.EndDate,21),'')+'  '+isnull(A.EndTime,'')   as EndDate");
               Sql.AppendLine("   ,case    when A.LeaveType='1' THEN '调休'");
		        Sql.AppendLine("when A.LeaveType='2' THEN '公假'");
		        Sql.AppendLine(" when A.LeaveType='3' THEN '病假'");
		        Sql.AppendLine("when A.LeaveType='4' THEN '事假'");
	       	    Sql.AppendLine("when A.LeaveType='5' THEN '其他'");
                Sql.AppendLine("END AS LeveTypeName");
                Sql.AppendLine("               FROM(");
                Sql.AppendLine("select a.*,b.DeptID ,isnull(b.EmployeeName,'')as EmployeeName");
                Sql.AppendLine("from officedba.AttendanceApply  a ");
                Sql.AppendLine("left outer join officedba.EmployeeInfo  b ");
                Sql.AppendLine("ON  a.CompanyCD=b.CompanyCD AND a.EmployeeID=b.ID  WHERE   A.CompanyCD=@CompanyCD  AND a.Flag='1'");
                Sql.AppendLine("                       )A ");
                Sql.AppendLine("left outer join officedba.deptInfo B");
                Sql.AppendLine("on  A.CompanyCD=B.CompanyCD AND A.DeptID=B.ID");
                Sql.AppendLine(" where  A.CompanyCD =@CompanyCD     ");

                 //定义查询的命令

                 //添加公司代码参数
                 comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));


                 if (!string.IsNullOrEmpty(DeptID))
                 {
                     Sql.AppendLine(" AND  A.DeptID=@DeptID               ");
                     comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
                 }
                 if (!string.IsNullOrEmpty(monthStartDate))
                 {
                     Sql.AppendLine("  AND    A.ApplyDate>=@monthStartDate AND A.ApplyDate<=@monthEndDate       ");
                     comm.Parameters.Add(SqlHelper.GetParameterFromString("@monthStartDate", monthStartDate));
                     comm.Parameters.Add(SqlHelper.GetParameterFromString("@monthEndDate", monthEndDate));
                 }
                 comm.CommandText = Sql.ToString();
                 return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
             }
             catch (Exception ex)
             {
                 string sss = ex.Message;
                 return null;
             }
         }
         #endregion

         #region 人员状况明细月报  迁出人数明细
         /// <summary>
         /// 人员状况明细月报   迁出人数明细
         /// </summary>
         /// <param name="CompanyCD"></param>
         /// <param name="DeptID"></param>
         /// <param name="pageIndex"></param>
         /// <param name="pageCount"></param>
         /// <param name="ord"></param>
         /// <param name="TotalCount"></param>
         /// <returns></returns>
         public static DataTable EmployeeConditionByQianchu(string CompanyCD, string DeptID, string monthStartDate, string monthEndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
         {
             try
             {
                 SqlCommand comm = new SqlCommand();
                 StringBuilder Sql = new StringBuilder();
                 Sql.AppendLine("select ");
                 Sql.AppendLine("A.NowDeptID AS DeptID");
                 Sql.AppendLine(",A.EmployeeID");
                 Sql.AppendLine(",ISNULL(B.EmployeeName,'') as EmployeeName");
                 Sql.AppendLine(",ISNULL(C.DeptName,'') as OldDeptName");
                 Sql.AppendLine(",ISNULL(D.DeptName,'') as NewDeptName");
                 Sql.AppendLine("                 ,ISNULL(E.QuarterName,'') as OldQuarterName");
                 Sql.AppendLine(",ISNULL(F.QuarterName,'') as NewQuarterName");
                 Sql.AppendLine(",ISNULL(convert(varchar(10),A.OutDate,21),'') AS OutDate");
                  Sql.AppendLine("from officedba.EmplApplyNotify  A");
                 Sql.AppendLine("left outer join officedba.EmployeeInfo B");
                 Sql.AppendLine("on A.CompanyCD=B.CompanyCD AND  A.EmployeeID=B.ID");
                 Sql.AppendLine("left outer join officedba.DeptInfo C");
                 Sql.AppendLine("on A.CompanyCD=C.CompanyCD AND  A.NowDeptID=C.ID");
                 Sql.AppendLine("left outer join officedba.DeptInfo D");
                 Sql.AppendLine("on A.CompanyCD=D.CompanyCD AND  A.NewDeptID=D.ID");
                  Sql.AppendLine("                left outer join officedba.DeptQuarter E ");
                 Sql.AppendLine("on  A.CompanyCD=E.CompanyCD AND  A.NowQuarterID=E.ID");
                 Sql.AppendLine("left outer join officedba.DeptQuarter F ");
                 Sql.AppendLine("on  A.CompanyCD=F.CompanyCD AND  A.NewQuarterID=F.ID");
                 Sql.AppendLine("where A.CompanyCD=@CompanyCD   and A.BillStatus='2'  ");

                 //定义查询的命令

                 //添加公司代码参数
                 comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));


                 if (!string.IsNullOrEmpty(DeptID))
                 {
                     Sql.AppendLine(" AND  A.NowDeptID=@DeptID               ");
                     comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
                 }
                 if (!string.IsNullOrEmpty(monthStartDate))
                 {
                     Sql.AppendLine("  AND    A.OutDate>=@monthStartDate AND A.OutDate<=@monthEndDate       ");
                     comm.Parameters.Add(SqlHelper.GetParameterFromString("@monthStartDate", monthStartDate));
                     comm.Parameters.Add(SqlHelper.GetParameterFromString("@monthEndDate", monthEndDate));
                 }
                 comm.CommandText = Sql.ToString();
                 return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
             }
             catch (Exception ex)
             {
                 string sss = ex.Message;
                 return null;
             }
         }
         #endregion
         #region 人员状况明细月报  迁入人数明细
         /// <summary>
         /// 人员状况明细月报   迁出人数明细
         /// </summary>
         /// <param name="CompanyCD"></param>
         /// <param name="DeptID"></param>
         /// <param name="pageIndex"></param>
         /// <param name="pageCount"></param>
         /// <param name="ord"></param>
         /// <param name="TotalCount"></param>
         /// <returns></returns>
         public static DataTable EmployeeConditionByQianRu(string CompanyCD, string DeptID, string monthStartDate, string monthEndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
         {
             try
             {
                 SqlCommand comm = new SqlCommand();
                 StringBuilder Sql = new StringBuilder();
                 Sql.AppendLine("select ");
                 Sql.AppendLine("A.NowDeptID AS DeptID");
                 Sql.AppendLine(",A.EmployeeID");
                 Sql.AppendLine(",ISNULL(B.EmployeeName,'') as EmployeeName");
                 Sql.AppendLine(",ISNULL(C.DeptName,'') as OldDeptName");
                 Sql.AppendLine(",ISNULL(D.DeptName,'') as NewDeptName");
                 Sql.AppendLine("                 ,ISNULL(E.QuarterName,'') as OldQuarterName");
                 Sql.AppendLine(",ISNULL(F.QuarterName,'') as NewQuarterName");
                 Sql.AppendLine(",ISNULL(convert(varchar(10),A.IntDate,21),'') AS OutDate");
                 Sql.AppendLine("from officedba.EmplApplyNotify  A");
                 Sql.AppendLine("left outer join officedba.EmployeeInfo B");
                 Sql.AppendLine("on A.CompanyCD=B.CompanyCD AND  A.EmployeeID=B.ID");
                 Sql.AppendLine("left outer join officedba.DeptInfo C");
                 Sql.AppendLine("on A.CompanyCD=C.CompanyCD AND  A.NowDeptID=C.ID");
                 Sql.AppendLine("left outer join officedba.DeptInfo D");
                 Sql.AppendLine("on A.CompanyCD=D.CompanyCD AND  A.NewDeptID=D.ID");
                 Sql.AppendLine("                left outer join officedba.DeptQuarter E ");
                 Sql.AppendLine("on  A.CompanyCD=E.CompanyCD AND  A.NowQuarterID=E.ID");
                 Sql.AppendLine("left outer join officedba.DeptQuarter F ");
                 Sql.AppendLine("on  A.CompanyCD=F.CompanyCD AND  A.NewQuarterID=F.ID");
                 Sql.AppendLine("where A.CompanyCD=@CompanyCD   and A.BillStatus='2'  ");

                 //定义查询的命令

                 //添加公司代码参数
                 comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));


                 if (!string.IsNullOrEmpty(DeptID))
                 {
                     Sql.AppendLine(" AND  A.NewDeptID=@DeptID               ");
                     comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
                 }
                 if (!string.IsNullOrEmpty(monthStartDate))
                 {
                     Sql.AppendLine("  AND    A.IntDate>=@monthStartDate AND A.IntDate<=@monthEndDate       ");
                     comm.Parameters.Add(SqlHelper.GetParameterFromString("@monthStartDate", monthStartDate));
                     comm.Parameters.Add(SqlHelper.GetParameterFromString("@monthEndDate", monthEndDate));
                 }
                 comm.CommandText = Sql.ToString();
                 return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
             }
             catch (Exception ex)
             {
                 string sss = ex.Message;
                 return null;
             }
         }
         #endregion

    #region 人员状况明细月报  迁入人数明细
         /// <summary>
         /// 人员状况明细月报   迁出人数明细
         /// </summary>
         /// <param name="CompanyCD"></param>
         /// <param name="DeptID"></param>
         /// <param name="pageIndex"></param>
         /// <param name="pageCount"></param>
         /// <param name="ord"></param>
         /// <param name="TotalCount"></param>
         /// <returns></returns>
         public static DataTable EmployeeConditionByOldDepartQianRu(string CompanyCD, string DeptID, string monthStartDate, string monthEndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
         {
             try
             {
                 SqlCommand comm = new SqlCommand();
                 StringBuilder Sql = new StringBuilder();
                 Sql.AppendLine("select ");
                 Sql.AppendLine("A.NewDeptID AS DeptID");
                 Sql.AppendLine(",A.EmployeeID");
                 Sql.AppendLine(",ISNULL(B.EmployeeName,'') as EmployeeName");
                 Sql.AppendLine(",ISNULL(C.DeptName,'') as OldDeptName");
                 Sql.AppendLine(",ISNULL(D.DeptName,'') as NewDeptName");
                 Sql.AppendLine("                 ,ISNULL(E.QuarterName,'') as OldQuarterName");
                 Sql.AppendLine(",ISNULL(F.QuarterName,'') as NewQuarterName");
                 Sql.AppendLine(",ISNULL(convert(varchar(10),A.IntDate,21),'') AS OutDate");
                 Sql.AppendLine("from officedba.EmplApplyNotify  A");
                 Sql.AppendLine("left outer join officedba.EmployeeInfo B");
                 Sql.AppendLine("on A.CompanyCD=B.CompanyCD AND  A.EmployeeID=B.ID");
                 Sql.AppendLine("left outer join officedba.DeptInfo C");
                 Sql.AppendLine("on A.CompanyCD=C.CompanyCD AND  A.NowDeptID=C.ID");
                 Sql.AppendLine("left outer join officedba.DeptInfo D");
                 Sql.AppendLine("on A.CompanyCD=D.CompanyCD AND  A.NewDeptID=D.ID");
                 Sql.AppendLine("                left outer join officedba.DeptQuarter E ");
                 Sql.AppendLine("on  A.CompanyCD=E.CompanyCD AND  A.NowQuarterID=E.ID");
                 Sql.AppendLine("left outer join officedba.DeptQuarter F ");
                 Sql.AppendLine("on  A.CompanyCD=F.CompanyCD AND  A.NewQuarterID=F.ID");
                 Sql.AppendLine("where A.CompanyCD=@CompanyCD   and A.BillStatus='2'  ");

                 //定义查询的命令

                 //添加公司代码参数
                 comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));


                 if (!string.IsNullOrEmpty(DeptID))
                 {
                     Sql.AppendLine(" AND  A.NewDeptID=@DeptID               ");
                     comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
                 }
                 if (!string.IsNullOrEmpty(monthStartDate))
                 {
                     Sql.AppendLine("  AND    A.IntDate>=@monthStartDate AND A.IntDate<=@monthEndDate       ");
                     comm.Parameters.Add(SqlHelper.GetParameterFromString("@monthStartDate", monthStartDate));
                     comm.Parameters.Add(SqlHelper.GetParameterFromString("@monthEndDate", monthEndDate));
                 }
                 comm.CommandText = Sql.ToString();
                 return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
             }
             catch (Exception ex)
             {
                 string sss = ex.Message;
                 return null;
             }
         }
         #endregion
         #region 人员状况明细月报  离职人数明细
         /// <summary>
         /// 人员状况明细月报   离职人数明细
         /// </summary>
         /// <param name="CompanyCD"></param>
         /// <param name="DeptID"></param>
         /// <param name="pageIndex"></param>
         /// <param name="pageCount"></param>
         /// <param name="ord"></param>
         /// <param name="TotalCount"></param>
         /// <returns></returns>
         public static DataTable EmployeeConditionByLiZhi(string CompanyCD, string DeptID, string monthStartDate, string monthEndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
         {
             try
             {
                 SqlCommand comm = new SqlCommand();
                 StringBuilder Sql = new StringBuilder();
                 Sql.AppendLine("select ");
                Sql.AppendLine("b.DeptID as DeptID ");
                Sql.AppendLine(",isnull(C.DeptName,'')as DeptName");
                Sql.AppendLine(",isnull(b.EmployeeName,'')as EmployeeName ");
                Sql.AppendLine(",isnull(convert(varchar(10),a.OutDate,21),'') as OutDate");
                Sql.AppendLine("from officedba.MoveNotify a");
                Sql.AppendLine("left outer join officedba.EmployeeInfo b");
                Sql.AppendLine("on b.CompanyCD=a.CompanyCD and a.EmployeeID=b.ID");
                Sql.AppendLine("Left outer join officedba.DeptInfo C");
                Sql.AppendLine("on C.CompanyCD=a.CompanyCD and b.DeptID=C.ID");
                Sql.AppendLine("where a.CompanyCD=@CompanyCD   and  a.BillStatus='2'  ");

                 //定义查询的命令

                 //添加公司代码参数
                 comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));


                 if (!string.IsNullOrEmpty(DeptID))
                 {
                     Sql.AppendLine(" AND  b.DeptID=@DeptID               ");
                     comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
                 }
                 if (!string.IsNullOrEmpty(monthStartDate))
                 {
                     Sql.AppendLine("  AND    a.OutDate>=@monthStartDate AND a.OutDate<=@monthEndDate       ");
                     comm.Parameters.Add(SqlHelper.GetParameterFromString("@monthStartDate", monthStartDate));
                     comm.Parameters.Add(SqlHelper.GetParameterFromString("@monthEndDate", monthEndDate));
                 }
                 comm.CommandText = Sql.ToString();
                 return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
             }
             catch (Exception ex)
             {
                 string sss = ex.Message;
                 return null;
             }
         }
         #endregion
         #region  工资标准明细
         /// <summary>
         /// 工资标准明细
         /// </summary>
         /// <param name="CompanyCD"></param>
         /// <param name="DeptID"></param>
         /// <param name="pageIndex"></param>
         /// <param name="pageCount"></param>
         /// <param name="ord"></param>
         /// <param name="TotalCount"></param>
         /// <returns></returns>
         public static DataTable SalaryStandardSelect(SalaryStandardModel model, int pageIndex, int pageCount, string ord, ref int TotalCount)
         {
             try
             {
           
                 #region 查询语句
                 StringBuilder searchSql = new StringBuilder();
                 searchSql.AppendLine(" SELECT                                     ");
                 searchSql.AppendLine(" 	 ISNULL(A.QuarterID, '') AS QuarterID     ,ISNULL(A.AdminLevel, '') AS AdminLevel   ,ISNULL(D.ItemName, '') AS ItemName   ,ISNULL(C.TypeName, '') AS TypeName  ,sum(A.UnitPrice)AS UnitPrice , ISNULL( b.QuarterName, '') AS QuarterName ");
                 searchSql.AppendLine(" FROM                                       ");
                 searchSql.AppendLine(" 	officedba.SalaryStandard A                ");
                 searchSql.AppendLine(" 	LEFT JOIN officedba.DeptQuarter B         ");
                 searchSql.AppendLine(" 		ON B.companyCD=A.companyCD AND B.ID = A.QuarterID                 ");
                 searchSql.AppendLine(" 	LEFT JOIN officedba.CodePublicType C      ");
                 searchSql.AppendLine(" 		ON C.companyCD=A.companyCD AND C.ID = A.AdminLevel                ");
                 searchSql.AppendLine(" 	LEFT JOIN officedba.SalaryItem D          ");
                 searchSql.AppendLine(" 		ON D.companyCD=A.companyCD AND D.ItemNo = A.ItemNo                ");
                 searchSql.AppendLine(" WHERE                                      ");
                 searchSql.AppendLine(" 	A.CompanyCD = '" + model.CompanyCD + "'                 ");
                 #endregion
                 //岗位ID
                 if (!string.IsNullOrEmpty(model.QuarterID))
                 {
                     searchSql.AppendLine("	AND A.QuarterID ='"+model.QuarterID+"'  ");
                 }
                 //岗位职等
                 if (!string.IsNullOrEmpty(model.AdminLevel))
                 {
                     searchSql.AppendLine("	AND A.AdminLevel = '"+model.AdminLevel+"' ");
                 }
                 //启用状态
                 if (!string.IsNullOrEmpty(model.UsedStatus))
                 {
                     searchSql.AppendLine("	AND A.UsedStatus ='"+model.UsedStatus+"'  ");
                 }
              
                 searchSql.AppendLine("	group by A.QuarterID,A.AdminLevel,A.ItemNo,D.ItemName ,C.TypeName,b.QuarterName");


                 return SqlHelper.CreateSqlByPageExcuteSql(searchSql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
             }
             catch (Exception ex)
             {
                 string sss = ex.Message;
                 return null;
             }
         }
           #endregion

         #region  部门工资明细统计
         /// <summary>
         /// 部门工资明细统计
         /// </summary>
         /// <param name="CompanyCD"></param>
         /// <param name="DeptID"></param>
         /// <param name="pageIndex"></param>
         /// <param name="pageCount"></param>
         /// <param name="ord"></param>
         /// <param name="TotalCount"></param>
         /// <returns></returns>
         public static DataTable SalarySummerySelect(SalaryStandardModel model, int pageIndex, int pageCount, string ord, ref int TotalCount)
         {
             try
             {
           
                 #region 查询语句
                 StringBuilder searchSql = new StringBuilder();
                 searchSql.AppendLine(" select c.DeptID as Remark,Substring(a.ReportMonth, 1, 4) + '年'+ Substring(a.ReportMonth, 5, 2) + '月' as itemNo,count( distinct(b.EmployeeID)) as CompanyCD,sum(b.SalaryMoney) as UnitPrice                                    ");
                 searchSql.AppendLine(" from officedba.SalaryReport a left outer join officedba. SalaryReportSummary  b ");
                 searchSql.AppendLine(" on b.companyCD=a.companyCD and a.ReprotNo =b.ReprotNo                                   ");
                 searchSql.AppendLine(" 	left outer join officedba.EmployeeInfo c             ");
                 searchSql.AppendLine(" 	on  c.companyCD=a.companyCD and b.EmployeeID=C.ID         ");
                 searchSql.AppendLine(" 	left outer join officedba.DeptInfo d                ");
                 searchSql.AppendLine(" on d.companyCD=a.companyCD and c.DeptID=d.ID    ");
                 searchSql.AppendLine(" 	where   a. CompanyCD='"+model .CompanyCD +"'   and     a.Status='3'      ");

                 #endregion



                 //定义查询的命令
                 SqlCommand comm = new SqlCommand();

                 #region 设置参数
                 //岗位ID
                 if (!string.IsNullOrEmpty(model.QuarterID))
                 {
                     searchSql.AppendLine("	AND c.DeptID ='" + model.QuarterID + "'  "); 
                 }
                 if (!string.IsNullOrEmpty(model.AdminLevel))//开始月份
                 {
                     searchSql.AppendLine("	AND Substring(a.ReportMonth, 5, 2)>='" + model.AdminLevel + "' "); 
                 }
                 if (!string.IsNullOrEmpty(model.AdminLevelName))//结束月份
                 {
                     searchSql.AppendLine("	AND Substring(a.ReportMonth, 5, 2)<='" + model.AdminLevelName + "'"); 
                 }
                 if (!string.IsNullOrEmpty(model.UnitPrice))//年度
                 {
                     searchSql.AppendLine("	AND Substring(a.ReportMonth, 1, 4)='" + model.UnitPrice + "'"); 
                 }

                 #endregion
                 searchSql.AppendLine(" 	group by c.DeptID,a.ReportMonth        ");



                 return SqlHelper.CreateSqlByPageExcuteSql(searchSql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
             }
             catch (Exception ex)
             {
                 string sss = ex.Message;
                 return null;
             }
         }
           #endregion


         #region 部门计件工资走势分析
         /// <summary>
         /// 部门计件工资走势分析
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
         public static DataTable DeptPieceSelect(string CompanyCD, string DeptID, string year )
        {
            try
            {
                SqlCommand comm = new SqlCommand();
                StringBuilder Sql = new StringBuilder();
       Sql.AppendLine("  select Substring(a.ReportMonth, 1, 4) + '年'+ Substring(a.ReportMonth, 5, 2) + '月'   ReportMonth, AVG(isnull(B.WorkMoney,0)) WorkMoney from officedba.SalaryReport A left join officedba.SalaryReportSummary B");
       Sql.AppendLine (" on A.ReprotNo =B.ReprotNo and A.companyCD=B.CompanyCD");
       Sql.AppendLine (" left join officedba.employeeInfo C ");
       Sql.AppendLine (" on B.EmployeeID=C.ID");
       Sql.AppendLine(" where  A.CompanyCD=@CompanyCD and A.Status=3  and B.WorkMoney is not null and B.WorkMoney>0  ");
       if (!string.IsNullOrEmpty(DeptID))
       {
           Sql.AppendLine("	AND  deptID=@DeptID ");
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
       }
       if (!string.IsNullOrEmpty(year))//年度
       {
           Sql.AppendLine("	AND Substring(a.ReportMonth, 1, 4)=@year");
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@year", year));
       }
       Sql.AppendLine(" group by ReportMonth                                                                             ");

       comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

       //指定命令的SQL文
       comm.CommandText = Sql.ToString();
       //执行查询
       return SqlHelper.ExecuteSearch(comm);


         
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        #endregion


         #region 计件工资走势分析明细页面
         /// <summary>
         /// 计件工资走势分析明细页面
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
         public static DataTable DeptPieceReportSelect(SalaryStandardModel searchModel, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            try
            {
                SqlCommand comm = new SqlCommand();
                StringBuilder Sql = new StringBuilder();
                Sql.AppendLine("  select Substring(a.ReportMonth, 1, 4) + '年'+ Substring(a.ReportMonth, 5, 2) + '月'   ReportMonth, isnull(B.WorkMoney,0) as WorkMoney ,isnull( C.EmployeeName,'')as EmployeeName ,isnull(d.DeptName,'')as DeptName,B.EmployeeID   from officedba.SalaryReport A left join officedba.SalaryReportSummary B");
       Sql.AppendLine (" on A.ReprotNo =B.ReprotNo and A.companyCD=B.CompanyCD");
       Sql.AppendLine (" left join officedba.employeeInfo C ");
       Sql.AppendLine (" on B.EmployeeID=C.ID");
       Sql.AppendLine ("           left outer join officedba.DeptInfo d  ");
       Sql.AppendLine("      	on d.companyCD=c.companyCD and  c.DeptID=d.ID ");
       Sql.AppendLine(" where  A.CompanyCD=@CompanyCD and A.Status=3 and B.WorkMoney is not null and B.WorkMoney>0  ");
       if (!string.IsNullOrEmpty(searchModel.QuarterID))
       {
           Sql.AppendLine("	AND  deptID=@DeptID ");
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", searchModel.QuarterID));
       }
       if (!string.IsNullOrEmpty(searchModel.UnitPrice))//年度
       {
           Sql.AppendLine("	AND Substring(a.ReportMonth, 1, 4)=@year");
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@year", searchModel.UnitPrice));
       }
       if (!string.IsNullOrEmpty(searchModel.AdminLevel))//年度
       {
           Sql.AppendLine("	AND  Substring(a.ReportMonth, 5, 2)=@month");
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@month", searchModel.AdminLevel));
       }




       comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", searchModel.CompanyCD ));

       //指定命令的SQL文
       comm.CommandText = Sql.ToString();
       //执行查询
       //return SqlHelper.ExecuteSearch(comm);

       return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref totalCount);
         
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        #endregion

          #region 部门计时工资走势分析
         /// <summary>
         /// 部门计时工资走势分析
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
         public static DataTable DepTimeSelect(string CompanyCD, string DeptID, string year)
        {
            try
            {
                SqlCommand comm = new SqlCommand();
                StringBuilder Sql = new StringBuilder();
                Sql.AppendLine("  select Substring(a.ReportMonth, 1, 4) + '年'+ Substring(a.ReportMonth, 5, 2) + '月'   ReportMonth, AVG(isnull(B.TimeMoney,0)) WorkMoney from officedba.SalaryReport A left join officedba.SalaryReportSummary B");
       Sql.AppendLine (" on A.ReprotNo =B.ReprotNo and A.companyCD=B.CompanyCD");
       Sql.AppendLine (" left join officedba.employeeInfo C ");
       Sql.AppendLine (" on B.EmployeeID=C.ID");
       Sql.AppendLine(" where  A.CompanyCD=@CompanyCD and A.Status=3  and B.TimeMoney is not null and B.TimeMoney>0");
       if (!string.IsNullOrEmpty(DeptID))
       {
           Sql.AppendLine("	AND  deptID=@DeptID ");
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
       }
       if (!string.IsNullOrEmpty(year))//年度
       {
           Sql.AppendLine("	AND Substring(a.ReportMonth, 1, 4)=@year");
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@year", year));
       }
       Sql.AppendLine(" group by ReportMonth                                                                             ");

       comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

       //指定命令的SQL文
       comm.CommandText = Sql.ToString();
       //执行查询
       return SqlHelper.ExecuteSearch(comm);


         
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        #endregion

    #region 计时工资走势分析明细页面
         /// <summary>
         /// 计时工资走势分析明细页面
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
         public static DataTable DeptTimeReportPrintSelect(SalaryStandardModel searchModel, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            try
            {
                SqlCommand comm = new SqlCommand();
                StringBuilder Sql = new StringBuilder();
                Sql.AppendLine("  select Substring(a.ReportMonth, 1, 4) + '年'+ Substring(a.ReportMonth, 5, 2) + '月'   ReportMonth, isnull(B.TimeMoney,0) as WorkMoney ,isnull( C.EmployeeName,'')as EmployeeName ,isnull(d.DeptName,'')as DeptName,B.EmployeeID   from officedba.SalaryReport A left join officedba.SalaryReportSummary B");
       Sql.AppendLine (" on A.ReprotNo =B.ReprotNo and A.companyCD=B.CompanyCD");
       Sql.AppendLine (" left join officedba.employeeInfo C ");
       Sql.AppendLine (" on B.EmployeeID=C.ID");
       Sql.AppendLine ("           left outer join officedba.DeptInfo d  ");
       Sql.AppendLine("      	on d.companyCD=c.companyCD and  c.DeptID=d.ID ");
       Sql.AppendLine(" where  A.CompanyCD=@CompanyCD and A.Status=3 and B.TimeMoney is not null and B.TimeMoney>0 ");
       if (!string.IsNullOrEmpty(searchModel.QuarterID))
       {
           Sql.AppendLine("	AND  deptID=@DeptID ");
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", searchModel.QuarterID));
       }
       if (!string.IsNullOrEmpty(searchModel.UnitPrice))//年度
       {
           Sql.AppendLine("	AND Substring(a.ReportMonth, 1, 4)=@year");
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@year", searchModel.UnitPrice));
       }
       if (!string.IsNullOrEmpty(searchModel.AdminLevel))//年度
       {
           Sql.AppendLine("	AND  Substring(a.ReportMonth, 5, 2)=@month");
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@month", searchModel.AdminLevel));
       }




       comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", searchModel.CompanyCD ));

       //指定命令的SQL文
       comm.CommandText = Sql.ToString();
       //执行查询
       //return SqlHelper.ExecuteSearch(comm);

       return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref totalCount);
         
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        #endregion

         #region 工资月份走势
         /// <summary>
         /// 工资月份走势
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
         public static DataTable DeptRealMoneySelect(string CompanyCD, string DeptID, string year)
        {
            try
            {
                SqlCommand comm = new SqlCommand();
                StringBuilder Sql = new StringBuilder();
                Sql.AppendLine("  select Substring(a.ReportMonth, 1, 4) + '年'+ Substring(a.ReportMonth, 5, 2) + '月'   ReportMonth, AVG(isnull(B.SalaryMoney,0)) WorkMoney from officedba.SalaryReport A left join officedba.SalaryReportSummary B");
       Sql.AppendLine (" on A.ReprotNo =B.ReprotNo and A.companyCD=B.CompanyCD");
       Sql.AppendLine (" left join officedba.employeeInfo C ");
       Sql.AppendLine (" on B.EmployeeID=C.ID");
       Sql.AppendLine(" where  A.CompanyCD=@CompanyCD and A.Status=3 and B.SalaryMoney is not null and B.SalaryMoney>0  ");
       if (!string.IsNullOrEmpty(DeptID))
       {
           Sql.AppendLine("	AND  deptID=@DeptID ");
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
       }
       if (!string.IsNullOrEmpty(year))//年度
       {
           Sql.AppendLine("	AND Substring(a.ReportMonth, 1, 4)=@year");
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@year", year));
       }
       Sql.AppendLine(" group by ReportMonth                                                                             ");

       comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

       //指定命令的SQL文
       comm.CommandText = Sql.ToString();
       //执行查询
       return SqlHelper.ExecuteSearch(comm);


         
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        #endregion

            #region 人员工资汇总统计
         /// <summary>
         /// 人员工资汇总统计
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
         public static DataTable DeptMoneySummarySelect(string CompanyCD, string DeptID, string year)
        {
            try
            {
                SqlCommand comm = new SqlCommand();
                StringBuilder Sql = new StringBuilder();
                Sql.AppendLine("  select Substring(a.ReportMonth, 1, 4) + '年'+ Substring(a.ReportMonth, 5, 2) + '月'   ReportMonth, sum(isnull(B.SalaryMoney,0)) WorkMoney from officedba.SalaryReport A left join officedba.SalaryReportSummary B");
       Sql.AppendLine (" on A.ReprotNo =B.ReprotNo and A.companyCD=B.CompanyCD");
       Sql.AppendLine (" left join officedba.employeeInfo C ");
       Sql.AppendLine (" on B.EmployeeID=C.ID");
       Sql.AppendLine(" where  A.CompanyCD=@CompanyCD and A.Status=3 and B.SalaryMoney is not null and B.SalaryMoney>0  ");
       if (!string.IsNullOrEmpty(DeptID))
       {
           Sql.AppendLine("	AND  deptID=@DeptID ");
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", DeptID));
       }
       if (!string.IsNullOrEmpty(year))//年度
       {
           Sql.AppendLine("	AND Substring(a.ReportMonth, 1, 4)=@year");
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@year", year));
       }
       Sql.AppendLine(" group by ReportMonth                                                                             ");

       comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));

       //指定命令的SQL文
       comm.CommandText = Sql.ToString();
       //执行查询
       return SqlHelper.ExecuteSearch(comm);


         
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        #endregion
             #region 工资走势月份分析明细页面
         /// <summary>
         /// 工资走势月份分析明细页面
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
         public static DataTable DeptRealMoneyReportPrintSelect(SalaryStandardModel searchModel, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            try
            {
                SqlCommand comm = new SqlCommand();
                StringBuilder Sql = new StringBuilder();
                Sql.AppendLine("  select Substring(a.ReportMonth, 1, 4) + '年'+ Substring(a.ReportMonth, 5, 2) + '月'   ReportMonth, isnull(B.SalaryMoney,0) as WorkMoney ,isnull( C.EmployeeName,'')as EmployeeName ,isnull(d.DeptName,'')as DeptName,B.EmployeeID   from officedba.SalaryReport A left join officedba.SalaryReportSummary B");
       Sql.AppendLine (" on A.ReprotNo =B.ReprotNo and A.companyCD=B.CompanyCD");
       Sql.AppendLine (" left join officedba.employeeInfo C ");
       Sql.AppendLine (" on B.EmployeeID=C.ID");
       Sql.AppendLine ("           left outer join officedba.DeptInfo d  ");
       Sql.AppendLine("      	on d.companyCD=c.companyCD and  c.DeptID=d.ID ");
       Sql.AppendLine(" where  A.CompanyCD=@CompanyCD and A.Status=3  and B.SalaryMoney is not null and B.SalaryMoney>0");
       if (!string.IsNullOrEmpty(searchModel.QuarterID))
       {
           Sql.AppendLine("	AND  deptID=@DeptID ");
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", searchModel.QuarterID));
       }
       if (!string.IsNullOrEmpty(searchModel.UnitPrice))//年度
       {
           Sql.AppendLine("	AND Substring(a.ReportMonth, 1, 4)=@year");
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@year", searchModel.UnitPrice));
       }
       if (!string.IsNullOrEmpty(searchModel.AdminLevel))//年度
       {
           Sql.AppendLine("	AND  Substring(a.ReportMonth, 5, 2)=@month");
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@month", searchModel.AdminLevel));
       }




       comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", searchModel.CompanyCD ));

       //指定命令的SQL文
       comm.CommandText = Sql.ToString();
       //执行查询
       //return SqlHelper.ExecuteSearch(comm);

       return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref totalCount);
         
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        #endregion



         #region 绩效考核按等级分布明细
         /// <summary>
         /// 绩效考核按等级分布明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
         public static DataTable PerformanceDetailsByLTPrintSelect(PerformanceTaskModel model, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            try
            {
                #region 查询语句
                //查询SQL拼写  TaskFlag
                StringBuilder searchSql = new StringBuilder();
                searchSql.AppendLine("    SELECT    ");
                searchSql.AppendLine("   	CASE when d.LevelType='1' then '达到要求'");
                searchSql.AppendLine("   	           when d.LevelType='2' then '超过要求'");
                searchSql.AppendLine("               when d.LevelType='3' then '表现突出'");
                searchSql.AppendLine("               when d.LevelType='4' then '需要改进'");
                searchSql.AppendLine("               when d.LevelType='5' then '不合格'");
                searchSql.AppendLine("               when d.LevelType is null then ''");
                searchSql.AppendLine("    end as LevelType ");
                searchSql.AppendLine("   ,	CASE when a.TaskFlag='1' then '月考核'");
                searchSql.AppendLine("   	           when a.TaskFlag='2' then '季考核'");
                searchSql.AppendLine("               when a.TaskFlag='3' then '半年考核'");
                searchSql.AppendLine("               when a.TaskFlag='4' then '年考核'"); 
                searchSql.AppendLine("               when a.TaskFlag is null then ''");
                searchSql.AppendLine("    end as FlagName ");
                searchSql.AppendLine("   ,isnull(e.employeename,'')as EmployeeName");
                searchSql.AppendLine("    ,isnull(h.deptname,'')as DeptName");
                searchSql.AppendLine("    ,d.EmployeeID");
                searchSql.AppendLine("     ,isnull(a.TaskNo,'' ) as TaskNo");
                searchSql.AppendLine("   	 ,isnull(a.Title,'' ) as Title ");
                searchSql.AppendLine("      FROM    officedba.PerformanceTask  a left outer join ");
                searchSql.AppendLine("   officedba.EmployeeInfo b");
                searchSql.AppendLine("    on b.CompanyCD=a.CompanyCD and a.Creator=b.ID ");
                searchSql.AppendLine("   left outer join  officedba.EmployeeInfo c ");
                searchSql.AppendLine("   on c.CompanyCD=a.CompanyCD and a.Summaryer=c.ID");
                searchSql.AppendLine("    left outer join officedba.PerformanceSummary d ");
                searchSql.AppendLine("   on  d.CompanyCD=a.CompanyCD   and a.TaskNo=d.TaskNo");
                searchSql.AppendLine("   left outer join  officedba.EmployeeInfo e");
                searchSql.AppendLine("   on e.CompanyCD=a.CompanyCD and d.EmployeeID=e.ID ");
                searchSql.AppendLine("   left outer join  officedba.EmployeeInfo f ");
                searchSql.AppendLine("   on f.CompanyCD=a.CompanyCD and  d.Evaluater=f.ID ");
                searchSql.AppendLine("   left outer join  officedba.PerformanceTemplate g");
                searchSql.AppendLine("   on  g.CompanyCD=a.CompanyCD  and d.TemplateNo= g.TemplateNo");
                searchSql.AppendLine("   left outer join officedba.DeptInfo h");
                searchSql.AppendLine("   on h.CompanyCD=a.CompanyCD and e.deptID=h.ID");

                searchSql.AppendLine("   where a.CompanyCD=@CompanyCD and a.Status='3'");
                #endregion

                //定义查询的命令
                SqlCommand comm = new SqlCommand();
                //添加公司代码参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
                if (!string.IsNullOrEmpty(model.EditFlag))//被考核人
                {
                    searchSql.AppendLine(" AND e.DeptID=@DeptID ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", model.EditFlag));
                }
                if (!string.IsNullOrEmpty(model.TaskNo))//考核任务编号
                {
                    searchSql.AppendLine(" AND a.TaskNo=@TaskNo ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", model.TaskNo));
                }
                if (!string.IsNullOrEmpty(model.CompleteDate))//考核类型
                {
                    searchSql.AppendLine(" AND g.TypeID=@TypeID ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@TypeID", model.CompleteDate));
                }
                if (!string.IsNullOrEmpty(model.TaskNum))//考核期间 
                {
                    searchSql.AppendLine(" AND a.TaskNum=@TaskNum ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNum", model.TaskNum));
                }
                if (!string.IsNullOrEmpty(model.Summaryer))//考核等级
                {
                    searchSql.AppendLine(" AND d.LevelType=@LevelType ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@LevelType", model.Summaryer));
                }
                if (!string.IsNullOrEmpty(model.Title))//考核建议
                {
                    searchSql.AppendLine(" AND d.AdviceType=@AdviceType ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@AdviceType", model.Title));
                }
                if (!string.IsNullOrEmpty(model.TaskDate))//考核建议
                {
                    searchSql.AppendLine(" AND a.TaskDate=@TaskDate ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskDate", model.TaskDate));
                }
                if (!string.IsNullOrEmpty(model.TaskFlag))//考核期间类型
                {
                    searchSql.AppendLine(" AND a.TaskFlag=@TaskFlag ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskFlag", model.TaskFlag));
                }
                if (!string.IsNullOrEmpty(model.SummaryDate))//部门
                {
                    searchSql.AppendLine(" AND e.deptID=@deptID ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@deptID", model.SummaryDate));
                }
           
                //指定命令的SQL文
                comm.CommandText = searchSql.ToString();
         

       return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref totalCount);
         
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        #endregion
        
             #region 绩效考核按建议分布明细
         /// <summary>
         /// 绩效考核按建议分布明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
         public static DataTable PerformanceDetailsByLAPrintSelect(PerformanceTaskModel model, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            try
            {
                #region 查询语句
                //查询SQL拼写  TaskFlag
                StringBuilder searchSql = new StringBuilder();
                searchSql.AppendLine("    SELECT    ");
                searchSql.AppendLine("   	CASE when d.AdviceType='1' then '不做处理'");
                searchSql.AppendLine("   	     when d.AdviceType='2' then '调整薪资'");
                searchSql.AppendLine("        when d.AdviceType='3' then '晋升'");
                searchSql.AppendLine("        when d.AdviceType='4' then '调职'");
                searchSql.AppendLine("        when d.AdviceType='5' then '辅导'");
                searchSql.AppendLine("        when d.AdviceType='6' then '培训'");
                searchSql.AppendLine("        when d.AdviceType='7' then '辞退'");
                searchSql.AppendLine("        when d.AdviceType is null then ''");
                searchSql.AppendLine("    end as LevelType ");
                searchSql.AppendLine("   ,	CASE when a.TaskFlag='1' then '月考核'");
                searchSql.AppendLine("   	           when a.TaskFlag='2' then '季考核'");
                searchSql.AppendLine("               when a.TaskFlag='3' then '半年考核'");
                searchSql.AppendLine("               when a.TaskFlag='4' then '年考核'"); 
                searchSql.AppendLine("               when a.TaskFlag is null then ''");
                searchSql.AppendLine("    end as FlagName ");
                searchSql.AppendLine("   ,isnull(e.employeename,'')as EmployeeName");
                searchSql.AppendLine("    ,isnull(h.deptname,'')as DeptName");
                searchSql.AppendLine("    ,d.EmployeeID");
                searchSql.AppendLine("     ,isnull(a.TaskNo,'' ) as TaskNo");
                searchSql.AppendLine("   	 ,isnull(a.Title,'' ) as Title ");
                searchSql.AppendLine("      FROM    officedba.PerformanceTask  a left outer join ");
                searchSql.AppendLine("   officedba.EmployeeInfo b");
                searchSql.AppendLine("    on b.CompanyCD=a.CompanyCD and a.Creator=b.ID ");
                searchSql.AppendLine("   left outer join  officedba.EmployeeInfo c ");
                searchSql.AppendLine("   on c.CompanyCD=a.CompanyCD and a.Summaryer=c.ID");
                searchSql.AppendLine("    left outer join officedba.PerformanceSummary d ");
                searchSql.AppendLine("   on  d.CompanyCD=a.CompanyCD   and a.TaskNo=d.TaskNo");
                searchSql.AppendLine("   left outer join  officedba.EmployeeInfo e");
                searchSql.AppendLine("   on e.CompanyCD=a.CompanyCD and d.EmployeeID=e.ID ");
                searchSql.AppendLine("   left outer join  officedba.EmployeeInfo f ");
                searchSql.AppendLine("   on f.CompanyCD=a.CompanyCD and  d.Evaluater=f.ID ");
                searchSql.AppendLine("   left outer join  officedba.PerformanceTemplate g");
                searchSql.AppendLine("   on  g.CompanyCD=a.CompanyCD  and d.TemplateNo= g.TemplateNo");
                searchSql.AppendLine("   left outer join officedba.DeptInfo h");
                searchSql.AppendLine("   on h.CompanyCD=a.CompanyCD and e.deptID=h.ID");

                searchSql.AppendLine("   where a.CompanyCD=@CompanyCD and a.Status='3'");
                #endregion

                //定义查询的命令
                SqlCommand comm = new SqlCommand();
                //添加公司代码参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
                if (!string.IsNullOrEmpty(model.EditFlag))//被考核人
                {
                    searchSql.AppendLine(" AND e.DeptID=@DeptID ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeptID", model.EditFlag));
                }
                if (!string.IsNullOrEmpty(model.TaskNo))//考核任务编号
                {
                    searchSql.AppendLine(" AND a.TaskNo=@TaskNo ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", model.TaskNo));
                }
                if (!string.IsNullOrEmpty(model.CompleteDate))//考核类型
                {
                    searchSql.AppendLine(" AND g.TypeID=@TypeID ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@TypeID", model.CompleteDate));
                }
                if (!string.IsNullOrEmpty(model.TaskNum))//考核期间 
                {
                    searchSql.AppendLine(" AND a.TaskNum=@TaskNum ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNum", model.TaskNum));
                }
                if (!string.IsNullOrEmpty(model.Summaryer))//考核等级
                {
                    searchSql.AppendLine(" AND d.LevelType=@LevelType ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@LevelType", model.Summaryer));
                }
                if (!string.IsNullOrEmpty(model.Title))//考核建议
                {
                    searchSql.AppendLine(" AND d.AdviceType=@AdviceType ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@AdviceType", model.Title));
                }
                if (!string.IsNullOrEmpty(model.TaskDate))//考核建议
                {
                    searchSql.AppendLine(" AND a.TaskDate=@TaskDate ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskDate", model.TaskDate));
                }
                if (!string.IsNullOrEmpty(model.TaskFlag))//考核期间类型
                {
                    searchSql.AppendLine(" AND a.TaskFlag=@TaskFlag ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskFlag", model.TaskFlag));
                }
                if (!string.IsNullOrEmpty(model.SummaryDate))//部门
                {
                    searchSql.AppendLine(" AND e.deptID=@deptID ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@deptID", model.SummaryDate));
                }
           
                //指定命令的SQL文
                comm.CommandText = searchSql.ToString();
         

       return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref totalCount);
         
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        #endregion




         #region 员工考试次数分析明细
         /// <summary>
         /// 员工考试次数分析明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
         public static DataTable EmployeeTestCountPrintSelect(EmployeeTestSearchModel model, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            try
            {
              
                #region 查询语句
                //查询SQL拼写
                StringBuilder searchSql = new StringBuilder();
                searchSql.AppendLine("Select");
                searchSql.AppendLine("C.DeptID");
                searchSql.AppendLine(",B.EmployeeID");
                searchSql.AppendLine(",isnull( C.EmployeeName,'') as EmployeeName");
                searchSql.AppendLine(",B.Flag   ");
                searchSql.AppendLine(",CASE when B.Flag ='0' then '否' ");
                searchSql.AppendLine("            when B.Flag ='1' then '是'  ");
                searchSql.AppendLine("            when B.Flag  is null then '' ");
                searchSql.AppendLine(" end as FlagName  ");
                searchSql.AppendLine(",isnull(A.Title,'')as Title");
                searchSql.AppendLine(",Isnull(D.deptName,'') as DeptName");
                searchSql.AppendLine(" from officedba. EmployeeTest A                                ");                          
                searchSql.AppendLine(" left outer join officedba. EmployeeTestScore B              ");                               
                searchSql.AppendLine(" on B.CompanyCD=A.CompanyCD AND B.TestNo=A.TestNo           ");                              
                searchSql.AppendLine(" LEFT OUTER JOIN officedba.EmployeeInfo c                                        ");
                searchSql.AppendLine(" ON C.CompanyCD=A.CompanyCD AND  B.EmployeeID=C.ID                  ");
                 searchSql.AppendLine("  left outer join officedba.deptInfo D                                       ");
                 searchSql.AppendLine(" on D.CompanyCD=A.CompanyCD AND c.deptID =D.ID                   ");


                 searchSql.AppendLine(" where  A.CompanyCD =@CompanyCD and A.Status='1' ");

                #endregion

                //定义查询的命令
                SqlCommand comm = new SqlCommand();
                //添加公司代码参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));


                //部门ID
                if (!string.IsNullOrEmpty(model.Addr))
                {
                    searchSql.AppendLine(" AND  C.DeptID=@Addr ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Addr", model.Addr));
                }

                //开始时间
                if (!string.IsNullOrEmpty(model.StartDate))
                {
                    searchSql.AppendLine(" AND A.StartDate >= @StartDate ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", model.StartDate));
                }
                if (!string.IsNullOrEmpty(model.StartToDate))
                {
                    searchSql.AppendLine(" AND A.StartDate <= @StartToDate ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartToDate", model.StartToDate));
                }
                //结束时间
                if (!string.IsNullOrEmpty(model.EndDate))
                {
                    searchSql.AppendLine(" AND A.EndDate >= @EndDate ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", model.EndDate));
                }
                if (!string.IsNullOrEmpty(model.EndToDate))
                {
                    searchSql.AppendLine(" AND A.EndDate <= @EndToDate ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndToDate", model.EndToDate));
                }
                //考试状态
                if (!string.IsNullOrEmpty(model.StatusName))
                {
                    searchSql.AppendLine(" AND B.EmployeeID=@EmployeeID ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", model.StatusName));
                }
             
                //指定命令的SQL文
                comm.CommandText = searchSql.ToString();
         

       return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref totalCount);
         
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        #endregion
         
         #region 员工培训次数分析明细
         /// <summary>
         /// 员工培训次数分析明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="DeptID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
         public static DataTable TrainningCountAnalysePrintSelect(string CompanyCD, string DeptID, string JoinID, string BeginDate, string EndDate, string BDate, string EDate, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            try
            {
              
                #region 查询语句
                //查询SQL拼写
                StringBuilder searchSql = new StringBuilder();
               searchSql.AppendLine(" select ");
               searchSql.AppendLine("isnull(C.DeptName,'') as DeptName");
               searchSql.AppendLine(",isnull(E.EmployeeName,'') as EmployeeName");
               searchSql.AppendLine(",ISNULL(B.TrainingName,'') as Title");  
               searchSql.AppendLine(",E.DeptID ");
               searchSql.AppendLine(",A.JoinID as EmployeeID ");
               searchSql.AppendLine(",CONVERT(VARCHAR(10),B.StartDate,21) AS StartDate ");
               searchSql.AppendLine(",CONVERT(VARCHAR(10),B.EndDate,21) AS EndDate  ");
               searchSql.AppendLine(",isnull(B.TrainingTeacher,'') as TeacherName");   
               searchSql.AppendLine("from officedba.TrainingUser A");
               searchSql.AppendLine(" left join officedba.EmployeeInfo E on E.id = A.JoinID");
               searchSql.AppendLine(" left join officedba.EmployeeTraining B on B.TrainingNo = A.TrainingNo ");
               searchSql.AppendLine("left join officedba.DeptInfo C on C.id = E.DeptID ");
               searchSql.AppendLine(" left join officedba.EmployeeInfo G on G.id = B.TrainingTeacher");
               searchSql.AppendLine("where A.CompanyCD = @CompanyCD and E.DeptID <> 0 and E.DeptID is not null");


                #endregion

                //定义查询的命令
                SqlCommand comm = new SqlCommand();
                //添加公司代码参数
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD",CompanyCD));


                //部门ID
                if (!string.IsNullOrEmpty(DeptID))
                {
                    searchSql.AppendLine(" AND  E.DeptID=@Addr ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Addr", DeptID));
                }

                //开始时间
                if (!string.IsNullOrEmpty(BeginDate))
                {
                    searchSql.AppendLine(" AND B.StartDate >= @StartDate ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartDate", BeginDate));
                }
                if (!string.IsNullOrEmpty(EndDate))
                {
                    searchSql.AppendLine(" AND B.StartDate <= @StartToDate ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartToDate", EndDate));
                }
                //结束时间
                if (!string.IsNullOrEmpty(BDate))
                {
                    searchSql.AppendLine(" AND B.EndDate >= @EndDate ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndDate", BDate));
                }
                if (!string.IsNullOrEmpty(EDate))
                {
                    searchSql.AppendLine(" AND B.EndDate <= @EndToDate ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndToDate", EDate));
                }
                // 
                if (!string.IsNullOrEmpty(JoinID))
                {
                    searchSql.AppendLine(" AND A.JoinID=@EmployeeID ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@EmployeeID", JoinID));
                }
             
                //指定命令的SQL文
                comm.CommandText = searchSql.ToString();
         

       return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref totalCount);
         
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        #endregion
    }
}

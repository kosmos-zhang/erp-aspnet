using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using XBase.Common;


namespace XBase.Data.OperatingModel.AimManager
{
    /// <summary>
    /// 执行分析
    /// </summary>
    public class PersonalAnalysisDBHelper
    {
        /// <summary>
        /// 获得日志状态
        /// </summary>
        /// <param name="userInfo">当前用户信息</param>
        /// <param name="userList">查询人员列表</param>
        /// <param name="dtS">开始时间</param>
        /// <param name="dtE">结束时间</param>
        /// <returns></returns>
        public static DataTable GetPersonalNote(UserInfoUtil userInfo, string userList, Nullable<DateTime> dtS, Nullable<DateTime> dtE)
        {
            string sql = @"SELECT  pn.Creator AS UserID, SUBSTRING(CONVERT(VARCHAR, pn.NoteDate, 120), 0, 11) AS MainDate
		                    , CASE pn.[Status]
			                    WHEN 0 THEN '草稿'
			                    WHEN 1 THEN '已提交'
			                    WHEN 2 THEN '已点评'
					            ELSE '状态错误'
		                      END AS ReportStatus
		                    ,pn.[Status]
                            FROM   officedba.PersonalNote pn
                            WHERE pn.CompanyCD=@CompanyCD 
                                AND pn.Creator IN ({0}) 
                                AND DATEDIFF(DAY, @DateStart, pn.NoteDate) >= 0 
                                AND DATEDIFF(DAY, pn.NoteDate, @DateEnd) >= 0
                            GROUP BY pn.Creator,pn.NoteDate,pn.[Status]";
            SqlParameter[] parms = 
                           {
                               new SqlParameter("@CompanyCD",userInfo.CompanyCD),
                               new SqlParameter("@DateStart",dtS.Value),
                               new SqlParameter("@DateEnd",dtE.Value)
                           };

            return SqlHelper.ExecuteSql(String.Format(sql, userList), parms);
        }

        /// <summary>
        /// 获得任务状态
        /// </summary>
        /// <param name="userInfo">当前用户信息</param>
        /// <param name="userList">查询人员列表</param>
        /// <param name="dtS">开始时间</param>
        /// <param name="dtE">结束时间</param>
        /// <returns></returns>
        public static DataTable GetTask(UserInfoUtil userInfo, string userList, Nullable<DateTime> dtS, Nullable<DateTime> dtE)
        {
            string sql = @"SELECT tab1.UserID, tab1.MainDate, tab1.ReportStatus, tab1.TaskType,COUNT(*) AS Num  FROM (
                                SELECT t.Principal AS UserID
                                  ,SUBSTRING(CONVERT(VARCHAR ,t.CompleteDate ,120) ,0 ,11) AS MainDate
                                  ,CASE t.[Status]
			                            WHEN 1 THEN '待下达'
			                            WHEN 2 THEN '未完成'
			                            WHEN 3 THEN '已完成'
			                            WHEN 4 THEN '已撤销'
			                            WHEN 5 THEN '已考评'
					                    ELSE '状态错误'
		                            END AS ReportStatus
                                  ,t.TaskType
                            FROM   officedba.Task t
                            WHERE  t.CompanyCD=@CompanyCD 
                                AND t.Principal IN ({0})
                                AND DATEDIFF(DAY, @DateStart, t.CompleteDate) >= 0 
                                AND DATEDIFF(DAY, t.CreateDate, @DateEnd) >= 0
) AS tab1
GROUP BY tab1.MainDate,tab1.ReportStatus,tab1.UserID,tab1.TaskType";
            SqlParameter[] parms = 
                           {
                               new SqlParameter("@CompanyCD",userInfo.CompanyCD),
                               new SqlParameter("@DateStart",dtS.Value),
                               new SqlParameter("@DateEnd",dtE.Value)
                           };

            return SqlHelper.ExecuteSql(String.Format(sql, userList), parms);
        }

        /// <summary>
        /// 获得日程状态
        /// </summary>
        /// <param name="userInfo">当前用户信息</param>
        /// <param name="userList">查询人员列表</param>
        /// <param name="dtS">开始时间</param>
        /// <param name="dtE">结束时间</param>
        /// <returns></returns>
        public static DataTable GetArrange(UserInfoUtil userInfo, string userList, Nullable<DateTime> dtS, Nullable<DateTime> dtE)
        {
            string sql = @"SELECT pda.Creator
	                              ,pda.StartDate
                                  ,pda.EndDate
                                  ,ISNULL(pda.[Status],0) AS Status
                                  ,CASE ISNULL(pda.[Status],0)
                                        WHEN 0 THEN '草稿'
                                        WHEN 1 THEN '已提交'
                                        WHEN 2 THEN '已点评'
					                    ELSE '状态错误'
                                   END AS ReportStatus
                            FROM   officedba.PersonalDateArrange pda
                            WHERE  pda.CompanyCD=@CompanyCD 
                                AND pda.Creator IN ({0})
                                AND DATEDIFF(DAY, @DateStart, pda.EndDate) >= 0 
                                AND DATEDIFF(DAY, pda.StartDate, @DateEnd) >= 0";
            SqlParameter[] parms = 
                           {
                               new SqlParameter("@CompanyCD",userInfo.CompanyCD),
                               new SqlParameter("@DateStart",dtS.Value),
                               new SqlParameter("@DateEnd",dtE.Value)
                           };

            return SqlHelper.ExecuteSql(String.Format(sql, userList), parms);
        }

        /// <summary>
        /// 获得目标状态
        /// </summary>
        /// <param name="userInfo">当前用户信息</param>
        /// <param name="userList">查询人员列表</param>
        /// <param name="dtS">开始时间</param>
        /// <param name="dtE">结束时间</param>
        /// <returns></returns>
        public static DataTable GetAim(UserInfoUtil userInfo, string userList, Nullable<DateTime> dtS, Nullable<DateTime> dtE)
        {
            string sql = @"SELECT tab1.MainDate,tab1.ReportStatus,tab1.UserID,tab1.AimFlag,COUNT(*) AS Num
                            FROM 
                            (SELECT CASE pa.AimFlag
                                    WHEN '1' THEN pa.AimDate
                                    WHEN '2' THEN pa.AimDate + '年'+convert(varchar,pa.AimNum) + '周'
                                    WHEN '3' THEN pa.AimDate + '年'+convert(varchar,pa.AimNum) + '月'
                                    WHEN '4' THEN pa.AimDate + '年'+convert(varchar,pa.AimNum) + '季'
                                    WHEN '5' THEN pa.AimDate + '年'
                               END AS MainDate
                              ,CASE pa.IsDirection
                               WHEN '0' THEN CASE pa.[Status]
					                        WHEN '0' THEN '未开始'
					                        WHEN '1' THEN '未完成'
					                        WHEN '2' THEN '已完成'
					                        WHEN '3' THEN '已撤销'
					                        WHEN '4' THEN '已总结'
					                        WHEN '5' THEN '已点评'
					                        WHEN '6' THEN '已审批'
					                        ELSE '状态错误'
					                        END
                               WHEN '1' THEN CASE fi.FlowStatus
					                        WHEN '1' THEN '待审批'
					                        WHEN '2' THEN '审批中'
					                        WHEN '3' THEN '审批通过'
					                        WHEN '4' THEN '审批不通过'
					                        WHEN '5' THEN '撤销审批'
					                        ELSE '未提交审批'
					                        END 
	                          END AS ReportStatus
                              ,pa.IsDirection
                              ,pa.PrincipalID AS UserID
                              ,pa.AimFlag
                              ,pa.AimDate
                              ,pa.AimNum
                        FROM   officedba.PlanAim pa
                        LEFT JOIN officedba.FlowInstance fi ON fi.BillTypeFlag=1 AND fi.BillTypeCode=1 AND fi.BillID=pa.ID AND fi.BillNo=pa.AimNo
                        WHERE  pa.CompanyCD=@CompanyCD AND pa.PrincipalID IN ({0})
		                        AND (
			                        (pa.AimFlag='1' AND DATEDIFF(DAY,@DateStart,pa.AimDate)>=0 AND DATEDIFF(DAY,pa.AimDate,@DateEnd)>=0 )
			                        OR (pa.AimFlag='2' AND DATEDIFF(week,pa.AimDate,@DateStart)<= pa.AimNum AND DATEDIFF(week,pa.AimDate,@DateEnd)>=pa.AimNum)
			                        OR (pa.AimFlag='3'  AND DATEDIFF(MONTH,pa.AimDate,@DateStart)<= pa.AimNum-1 AND DATEDIFF(MONTH,pa.AimDate,@DateEnd)>=pa.AimNum-1)
			                        OR (pa.AimFlag='4' AND DATEDIFF(quarter,pa.AimDate,@DateStart)<= pa.AimNum-1 AND DATEDIFF(quarter,pa.AimDate,@DateEnd)>=pa.AimNum-1)
			                        OR (pa.AimFlag='5' AND DATEDIFF(YEAR,@DateStart,pa.AimDate)>=0 AND DATEDIFF(YEAR,pa.AimDate,@DateEnd)>=0 )
		                        )
                        ) AS tab1
                        GROUP BY tab1.MainDate,tab1.ReportStatus,tab1.UserID,tab1.AimFlag";
            SqlParameter[] parms = 
                           {
                               new SqlParameter("@CompanyCD",userInfo.CompanyCD),
                               new SqlParameter("@DateStart",dtS.Value),
                               new SqlParameter("@DateEnd",dtE.Value)
                           };

            return SqlHelper.ExecuteSql(String.Format(sql, userList), parms);
        }


        /// <summary>
        /// 获得人员岗位权限
        /// </summary>
        /// <param name="userInfo">当前用户信息</param>
        /// <param name="userList">查询人员列表</param>
        /// <returns></returns>
        public static DataTable GetQuarter(UserInfoUtil userInfo, string userList)
        {
            string sql = @"SELECT qms.ModuleID,isnull(qms.TypeID,-1) AS TypeID,ei.ID
                            FROM officedba.EmployeeInfo ei
                            LEFT JOIN officedba.DeptQuarter dq ON dq.ID=ei.QuarterID AND dq.DeptID=ei.DeptID
                            LEFT JOIN officedba.QuterModuleSet qms ON dq.DeptID=qms.DeptID AND dq.QuarterNo=qms.QuarterNo
                            WHERE  ei.CompanyCD=@CompanyCD AND ei.ID IN ({0}) ";
            SqlParameter[] parms = 
                           {
                               new SqlParameter("@CompanyCD",userInfo.CompanyCD)
                           };

            return SqlHelper.ExecuteSql(String.Format(sql, userList), parms);
        }
    }
}

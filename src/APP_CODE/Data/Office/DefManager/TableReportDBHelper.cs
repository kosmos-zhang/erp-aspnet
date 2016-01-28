using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using XBase.Model.Office.DefManager;

namespace XBase.Data.Office.DefManager
{
    public class TableReportDBHelper
    {
        /// <summary>
        /// 返回自定义表列表
        /// </summary>
        /// <returns></returns>
        public static DataSet GetTableList(XBase.Common.UserInfoUtil userinfo)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@companyCD", SqlDbType.VarChar,200)
					};
            parameters[0].Value = userinfo.CompanyCD;
            return SqlHelper.ExecuteSqlX("select *,AliasTableName+'(define.'+CompanyCD+'_'+CustomTableName+')' buildTableName from defdba.CustomTable where CompanyCD=@companyCD order by ID", parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="report"></param>
        /// <param name="flag">判断是添加还是修改</param>
        /// <returns></returns>
        public static int InsertReport(ReportTableModel report,XBase.Common.UserInfoUtil userinfo,string ID,string useridlist)
        {
            int num = 0;
            StringBuilder strSql = new StringBuilder();
            if (ID == "0")  //添加
            {
                strSql.Append("insert into defdba.ReportTable(Menuname,CompanyCD,sqlstr,timeFlag,tablelist,excelhead) values(@Menuname,@CompanyCD,@SqlStr,@timeFlag,@tablelist,@excelhead)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
					new SqlParameter("@Menuname", SqlDbType.VarChar,200),
                    new SqlParameter("@CompanyCD", SqlDbType.VarChar,200),
					new SqlParameter("@SqlStr", SqlDbType.Text),
					new SqlParameter("@timeFlag", SqlDbType.Int,4),
                    new SqlParameter("@tablelist", SqlDbType.VarChar,100),
                    new SqlParameter("@excelhead", SqlDbType.VarChar,100)
					};
                parameters[0].Value = report.Menuname;
                parameters[1].Value = userinfo.CompanyCD;
                parameters[2].Value = report.SqlStr;
                parameters[3].Value = report.timeFlag;
                parameters[4].Value = report.Tablelist;
                parameters[5].Value = report.Excelhead;
                num = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql.ToString(), parameters));
            }
            else  //修改
            {
                strSql.Append("update defdba.ReportTable set Menuname=@menuname,timeFlag=@timeFlag,sqlstr=@SqlStr,tablelist=@tablelist,excelhead=@excelhead where ID=@id");
                SqlParameter[] parameters = {
					new SqlParameter("@Menuname", SqlDbType.VarChar,200),
                    new SqlParameter("@CompanyCD", SqlDbType.VarChar,200),
					new SqlParameter("@SqlStr", SqlDbType.Text),
					new SqlParameter("@timeFlag", SqlDbType.Int,4),
                    new SqlParameter("@tablelist", SqlDbType.VarChar,100),
                    new SqlParameter("@ID", SqlDbType.VarChar,100),
                    new SqlParameter("@excelhead", SqlDbType.VarChar,500)
					};
                parameters[0].Value = report.Menuname;
                parameters[1].Value = userinfo.CompanyCD;
                parameters[2].Value = report.SqlStr;
                parameters[3].Value = report.timeFlag;
                parameters[4].Value = report.Tablelist;
                parameters[5].Value = ID;
                parameters[6].Value = report.Excelhead;
                try
                {
                    SqlHelper.ExecuteTransSql(strSql.ToString(),parameters);
                    num = Convert.ToInt32(ID);
                }
                catch { return 0; }
            }
            //添加菜单
            CreateMenu(report.Menuname, num.ToString(), userinfo, useridlist);
            return num;

        }

        public static int CreateMenu(string menuname,string reportid, XBase.Common.UserInfoUtil userinfo,string useridlist)
        {
            useridlist = useridlist.Trim();
            
            try
            {
                string ModuleID = string.Empty;
                string oldModuleID = string.Empty;
                String sqlstr = "select * from defdba.CustomModule where PropertyValue = '" + "Pages/Office/DefManager/ReportTableList.aspx?reportid=" + reportid + "'";
                DataTable dt = SqlHelper.ExecuteSql(sqlstr);
                if (dt != null && dt.Rows.Count > 0)
                {
                    //修改菜单
                    sqlstr = "update defdba.CustomModule set PropertyValue='Pages/Office/DefManager/ReportTableList.aspx?reportid=" + reportid + "',ModuleName='" + menuname + "',userdUserList='"+useridlist+"' where ModuleID=" + dt.Rows[0]["ModuleID"].ToString()+ " and CompanyCD='"+userinfo.CompanyCD+"'";
                    SqlHelper.ExecuteSql(sqlstr);
                    return 1;
                }
                else
                {
                    //添加菜单
                    string menuid = System.Configuration.ConfigurationManager.ConnectionStrings["Intelligent"].ToString();
                    try
                    {
                        Convert.ToInt32(menuid);
                    }
                    catch { return 0; }
                    string submenu = menuid + "00";
                    sqlstr = "select isnull(Max(ModuleID)," + submenu + ") from defdba.CustomModule where parentID='" + menuid + "' and CompanyCD='" +userinfo.CompanyCD + "'";
                    dt = SqlHelper.ExecuteSql(sqlstr);
                    if (dt != null)
                    {
                        ModuleID = Convert.ToString(Convert.ToInt32(dt.Rows[0][0].ToString()) + 1);
                    }
                    else
                    {
                        //ModuleID = "22001";
                        ModuleID = menuid + "01";
                    }

                    StringBuilder sqlStr = new StringBuilder();
                    sqlStr.Append("insert into defdba.CustomModule(CompanyCD,ModuleID,ModuleName,ParentID,ModuleType,PropertyType,PropertyValue,userdUserList) values(@companycd,@moduleID,@menuname, @menuid ,'M','link',@pathpage,@userdUserList)");
                    SqlParameter[] parameters = { 
                                      new SqlParameter("@companycd",SqlDbType.VarChar,100),
                                      new SqlParameter("@moduleID",SqlDbType.VarChar,100),
                                      new SqlParameter("@menuname",SqlDbType.VarChar,100),
                                      new SqlParameter("@menuid",SqlDbType.VarChar,100),
                                      new SqlParameter("@pathpage",SqlDbType.VarChar,200),
                                      new SqlParameter("@userdUserList",SqlDbType.VarChar,1000)
                                      };
                    parameters[0].Value = userinfo.CompanyCD;
                    parameters[1].Value = ModuleID;
                    parameters[2].Value = menuname;
                    parameters[3].Value = menuid;
                    parameters[4].Value = "Pages/Office/DefManager/ReportTableList.aspx?reportid=" + reportid;
                    parameters[5].Value = useridlist;
                    
                    SqlHelper.ExecuteTransSql(sqlStr.ToString(), parameters);
                    return 1;
                }
            }
            catch
            {
                return 0;
            }
        }

        public static DataTable GetReportList(XBase.Common.UserInfoUtil userinfo,string menuname,int pageindex,int pagecount,string OrderBy, ref int totalCount)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine("select ID,Menuname,CompanyCD,timeflag,tablelist,excelhead ,case when timeflag=0 then '否' else '是' end timeflagCn from defdba.ReportTable where CompanyCD=@CompanyCD");
            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD",userinfo.CompanyCD));
            if (!string.IsNullOrEmpty(menuname))
            {
                sqlstr.AppendLine(" and Menuname like  @menuname");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@menuname","%"+menuname+"%"));
            }
            comm.CommandText = sqlstr.ToString();
            return SqlHelper.PagerWithCommand(comm,pageindex,pagecount,OrderBy,ref totalCount);
        }

        public static DataSet GetRePortByID(string ID)
        {
            SqlParameter[] param = {
                                       new SqlParameter("@ID",ID),
                                       new SqlParameter("@ID1","Pages/Office/DefManager/ReportTableList.aspx?reportid="+ID)
                                   };
            string sqlstr = "select * from defdba.ReportTable where ID=@ID;select * from defdba.CustomModule where PropertyValue=@ID1";
            return SqlHelper.ExecuteSqlX(sqlstr, param);
        }

        public static DataTable GetReportTableByID(string ID,string begindate,string enddate)
        {
            DataSet ds = GetRePortByID(ID);
            StringBuilder sqlstr = new StringBuilder();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string str = ds.Tables[0].Rows[0]["sqlstr"].ToString();

                if (ds.Tables[0].Rows[0]["timeflag"].ToString() == "1")
                {
                    //有时间排序
                    str = ds.Tables[0].Rows[0]["sqlstr"].ToString();
                    str = str.Replace("@begindate", "'"+begindate+"'");
                    str = str.Replace("@enddate", "'"+enddate+"'");
                }
                sqlstr.AppendLine(str);
                try
                {
                    return SqlHelper.ExecuteSql(sqlstr.ToString());
                }
                catch
                { return null; }
            }
            else
            {
                return null;
            }
        }

        public static int DelReportList(string idlist)
        {
            //删除报表设置
            string sqlstr = "delete from defdba.ReportTable where ID in ( "+idlist+" )";
            //删除菜单
            
            string[] arr = idlist.Split(',');
            for (int i = 0; i < arr.Length; i++)
            {
                sqlstr += ";" + "delete from defdba.CustomModule where PropertyValue='Pages/Office/DefManager/ReportTableList.aspx?reportid=" + arr[i] + "'";
            }

            TransactionManager tran = new TransactionManager();
            tran.BeginTransaction();
            try
            {
                int num = SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, sqlstr);
                tran.Commit();
                return num;
            }
            catch
            {
                tran.Rollback();
                return 0;
            }
        }

        public static string GetUserNameList(string useridlist)
        {
            string sqlstr = string.Empty;
            if (useridlist.TrimEnd(',').Length < 1)
            {
                return "";
            }
            else
            {
                DataSet ds = SqlHelper.ExecuteSqlX("select * from officedba.EmployeeInfo where ID in (" + useridlist + ")");
                string returnstr = string.Empty;
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        returnstr += "," + ds.Tables[0].Rows[i]["EmployeeName"].ToString();
                    }
                }
                returnstr = returnstr.TrimStart(',');
                return returnstr;
            }
        }
    }
}

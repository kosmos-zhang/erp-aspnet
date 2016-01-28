using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using XBase.Data.DBHelper;
using XBase.Model.Office.ProjectBudget;

namespace XBase.Data.Office.ProjectBudget
{
    public class ProjectBudgetDBHelper
    {
        public static void BindUnit(System.Web.UI.WebControls.DropDownList ddl, XBase.Common.UserInfoUtil userinfo)
        {
            string sqlstr = "select ID,CodeName from officedba.CodeUnitType where UsedStatus=1 and CompanyCD=@CompanyCD";

            SqlParameter[] param = {
                                     new SqlParameter("@CompanyCD",SqlDbType.VarChar)
                                 };
            param[0].Value = userinfo.CompanyCD;
            DataSet ds = SqlHelper.ExecuteSqlX(sqlstr, param);
            ddl.DataTextField = "CodeName";
            ddl.DataValueField = "ID";
            ddl.DataSource = ds;
            ddl.DataBind();
            ddl.DataSource = null;
            ddl.Items.Insert(0, new System.Web.UI.WebControls.ListItem("---请选择单位---", "0"));  
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="userinfo"></param>
        /// <param name="flag">true表示加项目预算确认筛选，flase表示显示全部项目</param>
        public static void BindProject(System.Web.UI.WebControls.DropDownList ddl, XBase.Common.UserInfoUtil userinfo, bool flag)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine("select A.ID,A.ProjectName,B.status from officedba.ProjectInfo A left join officedba.ProjectBudget B ");
            sqlstr.AppendLine("on A.ID=B.ProjectID");
            sqlstr.AppendLine("where A.CompanyCD=@CompanyCD");
            if (flag)
            {
                sqlstr.AppendLine("and isnull(B.status,1)!=2");
            }

            SqlParameter[] param = {
                                     new SqlParameter("@CompanyCD",SqlDbType.VarChar)
                                 };
            param[0].Value = userinfo.CompanyCD;
            DataSet ds = SqlHelper.ExecuteSqlX(sqlstr.ToString(), param);
            ddl.DataTextField = "ProjectName";
            ddl.DataValueField = "ID";
            ddl.DataSource = ds;
            ddl.DataBind();
            ddl.DataSource = null;
            ddl.Items.Insert(0, new System.Web.UI.WebControls.ListItem("---请选择项目---", "0"));
        }

        public static void BindProject(System.Web.UI.WebControls.DropDownList ddl, XBase.Common.UserInfoUtil userinfo)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine("select isnull(B.userlist," + userinfo.EmployeeID + ") userlist,A.* from officedba.ProjectInfo A left join officedba.projectBudget B");
            sqlstr.AppendLine("on A.ID = B.ProjectID  where A.companyCD=@companyCD");

            SqlParameter[] param = {
                                     new SqlParameter("@CompanyCD",SqlDbType.VarChar)
                                 };
            param[0].Value = userinfo.CompanyCD;
            DataSet ds = SqlHelper.ExecuteSqlX(sqlstr.ToString(), param);
            DataTable dt = (DataTable)ds.Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString().Length > 0)
                    {
                        string[] userlist = dt.Rows[i][0].ToString().Split(',');
                        int temp = 0;
                        for (int j = 0; j < userlist.Length; j++)
                        {
                            if (userlist[j] == userinfo.EmployeeID.ToString())
                            {
                                break;
                            }
                            else
                            {
                                temp++;
                            }
                            if (temp == userlist.Length)
                            {
                                dt.Rows[i].Delete();
                            }
                        }
                    }
                }
                dt.AcceptChanges();
            }
            ddl.DataTextField = "ProjectName";
            ddl.DataValueField = "ID";
            ddl.DataSource = dt;
            ddl.DataBind();
            ddl.DataSource = null;
            ddl.Items.Insert(0, new System.Web.UI.WebControls.ListItem("---请选择项目---", "0"));
        }

        #region 绑定项目(同时绑定对应的项目编号)
        /// <summary>
        /// 绑定项目(同时绑定对应的项目编号)
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="userinfo"></param>
        /// <param name="bindProjectNo">用来重载的任意字符串</param>
        public static void BindProject(System.Web.UI.WebControls.DropDownList ddl, XBase.Common.UserInfoUtil userinfo,string bindProjectNo)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine("select isnull(B.userlist," + userinfo.EmployeeID + ") userlist,A.* from officedba.ProjectInfo A left join officedba.projectBudget B");
            sqlstr.AppendLine("on A.ID = B.ProjectID  where A.companyCD=@companyCD");

            SqlParameter[] param = {
                                     new SqlParameter("@CompanyCD",SqlDbType.VarChar)
                                 };
            param[0].Value = userinfo.CompanyCD;
            DataSet ds = SqlHelper.ExecuteSqlX(sqlstr.ToString(), param);
            DataTable dt = (DataTable)ds.Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString().Length > 0)
                    {
                        string[] userlist = dt.Rows[i][0].ToString().Split(',');
                        int temp = 0;
                        for (int j = 0; j < userlist.Length; j++)
                        {
                            if (userlist[j] == userinfo.EmployeeID.ToString())
                            {
                                break;
                            }
                            else
                            {
                                temp++;
                            }
                            if (temp == userlist.Length)
                            {
                                dt.Rows[i].Delete();
                            }
                        }
                    }
                }
                dt.AcceptChanges();
            }
            ddl.DataTextField = "ProjectName";
            ddl.DataValueField = "ID";
            //ddl.Attributes.Add("title","ProjectNo");
            ddl.DataSource = dt;
            ddl.DataBind();
            ddl.DataSource = null;
            ddl.Items.Insert(0, new System.Web.UI.WebControls.ListItem("---请选择项目---", "0"));
        }
        #endregion

        public static int AddBudgetInfo(BudgetSummary budgetSummary, XBase.Common.UserInfoUtil userinfo)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append(@"insert into officedba.budgetSummary(CompanyCD,BudgetName,budgetUnit,budgetArea,projectID,seq,subBudgetID) 
                            values(@CompanyCD,@BudgetName,@budgetUnit,@budgetArea,@projectID,@seq,@subBudgetID)");
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar,50),
                                       new SqlParameter("@BudgetName",SqlDbType.VarChar,500),
                                       new SqlParameter("@budgetUnit",SqlDbType.Int,4),
                                       new SqlParameter("@budgetArea",SqlDbType.Decimal),
                                       new SqlParameter("@projectID",SqlDbType.Int),
                                       new SqlParameter("@seq",SqlDbType.Int),
                                       new SqlParameter("@subBudgetID",SqlDbType.Int)
                                   };
            param[0].Value = userinfo.CompanyCD;
            param[1].Value = budgetSummary.BudgetName;
            param[2].Value = budgetSummary.budgetUnit;
            param[3].Value = budgetSummary.budgetArea;
            param[4].Value = budgetSummary.projectid;
            param[5].Value = budgetSummary.seq;
            param[6].Value = budgetSummary.SubBudgetID;
            TransactionManager tran = new TransactionManager();
            int num = 0;
            tran.BeginTransaction();
            try
            {
                num = SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, sqlstr.ToString(), param);
                tran.Commit();
            }
            catch
            {
                tran.Rollback();
            }
            return num;
        }

        public static int EditBudgetInfo(BudgetSummary budgetSummary, XBase.Common.UserInfoUtil userinfo)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append(@"update officedba.budgetSummary set 
                            BudgetName=@BudgetName,budgetUnit=@budgetUnit,budgetArea=@budgetArea,
                            projectID=@projectID,seq=@seq,subBudgetID=@subBudgetID where budgetID=@budgetID");
            SqlParameter[] param = {
                                       new SqlParameter("@budgetID",SqlDbType.VarChar,50),
                                       new SqlParameter("@BudgetName",SqlDbType.VarChar,500),
                                       new SqlParameter("@budgetUnit",SqlDbType.Int,4),
                                       new SqlParameter("@budgetArea",SqlDbType.Decimal),
                                       new SqlParameter("@projectID",SqlDbType.Int),
                                       new SqlParameter("@seq",SqlDbType.Int),
                                       new SqlParameter("@subBudgetID",SqlDbType.Int)
                                   };
            param[0].Value = budgetSummary.budgetID;
            param[1].Value = budgetSummary.BudgetName;
            param[2].Value = budgetSummary.budgetUnit;
            param[3].Value = budgetSummary.budgetArea;
            param[4].Value = budgetSummary.projectid;
            param[5].Value = budgetSummary.seq;
            param[6].Value = budgetSummary.SubBudgetID;
            TransactionManager tran = new TransactionManager();
            int num = 0;
            tran.BeginTransaction();
            try
            {
                num = SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, sqlstr.ToString(), param);
                tran.Commit();
            }
            catch
            {
                tran.Rollback();
            }
            return num;
        }

        public static DataSet ReadEexcel(string FilePath, string companycd)
        {
            string connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;data source=" + FilePath;
            string sql = "SELECT distinct * FROM [Sheet1$]  order by [流水号]";
            DataSet ds = new DataSet();
            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter(sql, connStr);
            da.Fill(ds);
            //删除历史记录
            SqlParameter[] paramter = { new SqlParameter("@companycd", companycd) };
            sql = "delete from officedba.budgetSummary_temp where CompanyCD=@companycd";
            SqlHelper.ExecuteTransSql(sql, paramter);
            //传到临时表中
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                SqlParameter[] param = 
                {
                    new SqlParameter("@companycd",companycd),
                    new SqlParameter("@id",ds.Tables[0].Rows[i][0].ToString()),
                    new SqlParameter("@BudgetName",ds.Tables[0].Rows[i][1].ToString()),
                    new SqlParameter("@budgetUnit",ds.Tables[0].Rows[i][2].ToString()),
                    new SqlParameter("@budgetArea",ds.Tables[0].Rows[i][3].ToString())
                };

                sql = "insert into officedba.budgetSummary_temp values(@id,@companycd,@BudgetName,@budgetUnit,@budgetArea)";
                SqlHelper.ExecuteTransSql(sql, param);
            }
            sql = "select * from officedba.budgetSummary_temp where CompanyCD=@companycd and budgetID !=0 order by budgetID";
            ds = new DataSet();
            SqlParameter[] paramter1 = { new SqlParameter("@companycd", companycd) };
            DataTable dt = SqlHelper.ExecuteSql(sql, paramter1);
            ds.Tables.Add(dt);
            return ds;
        }

        /// <summary>
        /// 判断给定单位名称和公司名，是否存在该单位
        /// zxb 2009-08-18
        /// </summary>
        /// <param name="codename"></param>
        /// <param name="compid"></param>
        /// <returns></returns>
        public static bool ChargeCodeUnit(string codename, string compid)
        {
            SqlParameter[] parameters = { new SqlParameter("@codename", SqlDbType.VarChar, 50),
                                          new SqlParameter("@companyid",SqlDbType.VarChar,50)};
            parameters[0].Value = codename;
            parameters[1].Value = compid;
            object obj = SqlHelper.ExecuteScalar("select count(1) from officedba.CodeUnitType where CodeName=@codename and CompanyCD=@companyid", parameters);
            return Convert.ToInt32(obj) > 0 ? true : false;
        }

        public static int ListInput(string companycd, string projectid)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine("insert into officedba.budgetSummary(CompanyCD,BudgetName,budgetUnit,budgetArea,projectID)");
            sqlstr.AppendLine("select A.CompanyCD,A.BudgetName,B.ID,A.budgetArea," + projectid + "  projectid from officedba.budgetSummary_temp A");
            sqlstr.AppendLine("left join ");
            sqlstr.AppendLine("(");
            sqlstr.AppendLine("    select * from officedba.CodeUnitType where CompanyCD=@company");
            sqlstr.AppendLine(")B on A.budgetUnit=B.CodeName");
            sqlstr.AppendLine("where A.CompanyCD=@company");
            SqlParameter[] param = {
                                new SqlParameter("@company",SqlDbType.VarChar,50)
                                    };
            param[0].Value = companycd;
            int num = 0;
            TransactionManager tran = new TransactionManager();
            tran.BeginTransaction();
            try
            {
                num = SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, sqlstr.ToString(), param);
                tran.Commit();
            }
            catch { tran.Rollback(); }
            return num;
        }

        public static int AddBudgetPriceInfo(BudgetPrice budgetPrice, XBase.Common.UserInfoUtil userinfo)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("insert into officedba.budgetPrice(CompanyCD,BudgetPriceName,UnitPrice,Formula,projectID,codeType) values(@CompanyCD,@BudgetPriceName,@UnitPrice,@Formula,@projectID,@codeType)");
            sqlstr.Append(" ;select @@IDENTITY ");

            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar,50),
                                       new SqlParameter("@BudgetPriceName",SqlDbType.VarChar,500),
                                       new SqlParameter("@UnitPrice",SqlDbType.Decimal),
                                       new SqlParameter("@Formula",SqlDbType.VarChar,50),
                                       new SqlParameter("@projectID",SqlDbType.Int),
                                       new SqlParameter("@codeType",SqlDbType.Int)
                                   };
            param[0].Value = userinfo.CompanyCD;
            param[1].Value = budgetPrice.BudgetPriceName;
            param[2].Value = budgetPrice.UnitPrice;
            param[3].Value = budgetPrice.Formula;
            param[4].Value = budgetPrice.projectID;
            param[5].Value = budgetPrice.codeType;
            TransactionManager tran = new TransactionManager();
            int num = 0;
            tran.BeginTransaction();
            try
            {
                //num = SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, sqlstr.ToString(), param);
                num = Convert.ToInt32(SqlHelper.ExecuteScalar(sqlstr.ToString(), param));
                tran.Commit();
            }
            catch
            {
                tran.Rollback();
            }
            return num;
        }
        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="budgetPrice">BudgetPrice实体</param>
        /// <returns></returns>
        public static int UpdateBudgetPriceInfo(BudgetPrice budgetPrice)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("update officedba.budgetPrice set ");
            sqlstr.Append("BudgetPriceName=@BudgetPriceName,UnitPrice=@UnitPrice,Formula=@Formula,");
            sqlstr.Append(" projectID=@projectID,codeType=@codeType  ");
            sqlstr.Append(" where CompanyCD=@CompanyCD and budgetpriceID=@budgetpriceID ");
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar,50),
                                       new SqlParameter("@BudgetPriceName",SqlDbType.VarChar,500),
                                       new SqlParameter("@UnitPrice",SqlDbType.Decimal),
                                       new SqlParameter("@Formula",SqlDbType.VarChar,50),
                                       new SqlParameter("@projectID",SqlDbType.Int),
                                       new SqlParameter("@codeType",SqlDbType.Int),
                                       new SqlParameter("@budgetpriceID",SqlDbType.Int),
                                   };
            param[0].Value = budgetPrice.CompanyCD;
            param[1].Value = budgetPrice.BudgetPriceName;
            param[2].Value = budgetPrice.UnitPrice;
            param[3].Value = budgetPrice.Formula;
            param[4].Value = budgetPrice.projectID;
            param[5].Value = budgetPrice.codeType;
            param[6].Value = budgetPrice.budgetpriceID;
            TransactionManager tran = new TransactionManager();
            int num = 0;
            tran.BeginTransaction();
            try
            {
                num = SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, sqlstr.ToString(), param);
                tran.Commit();
            }
            catch
            {
                tran.Rollback();
            }
            return num;
        }
        #endregion

        #region 判断同一项目中是否有同名的 预算价格摘要名称
        /// <summary>
        /// 判断同一项目中是否有同名的 预算价格摘要名称
        /// </summary>
        /// <param name="name">预算价格摘要名称</param>
        /// <param name="projectID">所属项目ID</param>
        /// <returns>true：有重复，false:无重复的</returns>
        public static bool IsRepeatedNameOneProject(string budgetpriceID, string name, string projectID)
        {
            bool isRept = false;
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select count(1) from officedba.BudgetPrice ");
            strSql.AppendLine(" where BudgetPriceName=@Name and projectID=@ProjectID ");
            if (budgetpriceID != "0")
            {
                strSql.AppendLine(" and budgetpriceID!=@budgetpriceID ");
            }
            SqlParameter[] param = { 
                                    new SqlParameter("@Name",name),
                                    new SqlParameter("@ProjectID",projectID),
                                    new SqlParameter("@BudgetPriceID",budgetpriceID)
                                   };
            int iCount = 0;
            iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql.ToString(), param));
            if (iCount > 0)
            {
                isRept = true;
            }
            return isRept;
        }
        #endregion

        public static DataSet GetBudgetItem(string projectid, XBase.Common.UserInfoUtil userinfo)
        {
            string sqlstr = "select * from officedba.budgetPrice where codetype=0 and projectID=@projectid and companycd=@companycd";
            SqlParameter[] param = {
                                       new SqlParameter("@projectid",SqlDbType.VarChar,50),
                                       new SqlParameter("@companycd",SqlDbType.VarChar,50)
                                   };
            param[0].Value = projectid;
            param[1].Value = userinfo.CompanyCD;
            return SqlHelper.ExecuteSqlX(sqlstr, param);
        }

        public static DataSet GetBudgetSummary(string projectid, XBase.Common.UserInfoUtil userinfo, int dotnum)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine("select A.BudgetID,A.CompanyCD,A.BudgetName,A.BudgetUnit,Convert(decimal(22," + dotnum + "),A.BudgetArea) BudgetArea,B.CodeName from officedba.budgetSummary A left join officedba.CodeUnitType B on A.budgetUnit = B.ID where A.projectID=@projectid and A.companycd=@companycd order by seq");
            sqlstr.AppendLine("select * from officedba.projectBudget where CompanyCD=@CompanyCD and ProjectID=@ProjectID");
            SqlParameter[] param = {
                                       new SqlParameter("@projectid",SqlDbType.VarChar,50),
                                       new SqlParameter("@companycd",SqlDbType.VarChar,50)
                                   };
            param[0].Value = projectid;
            param[1].Value = userinfo.CompanyCD;
            return SqlHelper.ExecuteSqlX(sqlstr.ToString(), param);
        }

        public static DataSet GetBudgetSummary(string projectid, XBase.Common.UserInfoUtil userinfo, int dotnum, string subid)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine("select A.BudgetID,A.CompanyCD,A.BudgetName,A.BudgetUnit,Convert(decimal(22," + dotnum + "),A.BudgetArea) BudgetArea,B.CodeName,convert(decimal(18," + userinfo.SelPoint + "),isnull(C.BaseNum,0)) BaseNum from officedba.budgetSummary A ");
            sqlstr.AppendLine("left join officedba.CodeUnitType B on A.budgetUnit = B.ID ");
            sqlstr.AppendLine("left join officedba.ProjectBaseNum C on A.BudgetID=C.SummaryID");
            sqlstr.AppendLine("where A.projectID=@projectid and A.companycd=@companycd and subBudgetID=@subBudgetID order by seq");

            sqlstr.AppendLine("select * from officedba.projectBudget where CompanyCD=@CompanyCD and ProjectID=@ProjectID");
            SqlParameter[] param = {
                                       new SqlParameter("@projectid",SqlDbType.VarChar,50),
                                       new SqlParameter("@companycd",SqlDbType.VarChar,50),
                                       new SqlParameter("@subBudgetID",SqlDbType.VarChar,50)
                                   };
            param[0].Value = projectid;
            param[1].Value = userinfo.CompanyCD;
            param[2].Value = subid;
            return SqlHelper.ExecuteSqlX(sqlstr.ToString(), param);
        }

        public static DataSet GetPriceData(string projectID, XBase.Common.UserInfoUtil userinfo)
        {
            string sqlstr = "select budgetpriceID,CompanyCD,BudgetPriceName,UnitPrice,Formula,ProjectID,CodeType from officedba.budgetPrice where CompanyCD=@companyCD and projectID=@projectID order by codetype";
            SqlParameter[] param = {
                                       new SqlParameter("@companyCD",SqlDbType.VarChar,50),
                                       new SqlParameter("@projectID",SqlDbType.VarChar,50)
                                   };
            param[0].Value = userinfo.CompanyCD;
            param[1].Value = projectID;
            return SqlHelper.ExecuteSqlX(sqlstr, param);
        }

        public static int AddProjectBudgetInfo(ProjectBudgetInfo projectBudgetInfo, XBase.Common.UserInfoUtil userinfo, string valuelist, string basevaluelist)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine("delete from officedba.ProjectBudget where CompanyCD=@CompanyCD and ProjectID=@ProjectID");
            sqlstr.AppendLine("insert into officedba.ProjectBudget(CompanyCD,ProjectID,TotalPrice,BudgetPerson,BudgetTime,status,userlist) values(@CompanyCD,@ProjectID,@TotalPrice,@BudgetPerson,@BudgetTime,1,@userlist)");
            sqlstr.AppendLine("delete from officedba.ProjectBudgetDetails where companyCD=@CompanyCD and ProjectID=@ProjectID");
            if (valuelist.Length > 0)
            {
                string[] arr = valuelist.Split(',');
                for (int i = 0; i < arr.Length; i++)
                {
                    string[] item = arr[i].Split('#');
                    for (int j = 0; j < 3; j++)
                    {
                        if (item[j] == "" || item[j].Trim().Length < 1)
                        {
                            item[j] = "0";
                        }
                    }
                    sqlstr.AppendLine("insert into officedba.ProjectBudgetDetails(CompanyCD,ProjectID,SummaryID,PriceID,budgetValue) values(@CompanyCD,@ProjectID," + int.Parse(item[0]) + "," + int.Parse(item[1]) + "," + decimal.Parse(item[2]) + ")");
                }
            }

            sqlstr.AppendLine("delete from officedba.ProjectBaseNum where CompanyCD=@CompanyCD and ProjectID=@ProjectID");
            if (basevaluelist.Length > 0)
            {
                string[] arr = basevaluelist.Split(',');
                for (int i = 0; i < arr.Length; i++)
                {
                    string[] item = arr[i].Split('#');
                    if (item[1].Length < 1)
                    {
                        item[1] = "0";
                    }
                    sqlstr.AppendLine("insert into officedba.ProjectBaseNum(CompanyCD,ProjectID,SummaryID,BaseNum) values(@CompanyCD,@ProjectID," + int.Parse(item[0]) + "," + decimal.Parse(item[1]) + ")");
                }
            }

            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar,50),
                                       new SqlParameter("@ProjectID",SqlDbType.Int),
                                       new SqlParameter("@TotalPrice",SqlDbType.Decimal),
                                       new SqlParameter("@BudgetPerson",SqlDbType.Int),
                                       new SqlParameter("@BudgetTime",SqlDbType.DateTime),
                                       new SqlParameter("@userlist",SqlDbType.VarChar)
                                   };
            param[0].Value = userinfo.CompanyCD;
            param[1].Value = projectBudgetInfo.ProjectID;
            param[2].Value = projectBudgetInfo.TotalPrice;
            param[3].Value = projectBudgetInfo.BudgetPerson;
            param[4].Value = projectBudgetInfo.BudgetTime;
            if (projectBudgetInfo.Userlist.Length > 0)
            {
                List<string> arr = new List<string>();
                foreach (string i in projectBudgetInfo.Userlist.Split(','))
                {
                    arr.Add(i);
                }
                if(!arr.Contains(userinfo.EmployeeID.ToString()))
                {
                    projectBudgetInfo.Userlist += "," + userinfo.EmployeeID;
                }

                
            }
            param[5].Value = projectBudgetInfo.Userlist;

            //删除该项目明细

            TransactionManager tran = new TransactionManager();
            int num = 0;
            tran.BeginTransaction();
            try
            {
                num = SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, sqlstr.ToString(), param);
                tran.Commit();
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                tran.Rollback();
            }
            return num;
        }

        public static int CheckInsertData(string projectID, XBase.Common.UserInfoUtil userinfo)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine("select * from officedba.ProjectBudget where CompanyCD=@companyCD and projectID=@projectID and status=2");
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar,50),
                                       new SqlParameter("@ProjectID",SqlDbType.Int)
                                   };
            param[0].Value = userinfo.CompanyCD;
            param[1].Value = projectID;
            DataTable dt = SqlHelper.ExecuteSql(sqlstr.ToString(), param);
            if (dt != null && dt.Rows.Count > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public static string GetItemData(string projectID, XBase.Common.UserInfoUtil userinfo)
        {
            string sqlstr = "select SummaryID,PriceID,budgetValue from officedba.ProjectBudgetDetails where CompanyCD=@CompanyCD and ProjectID=@ProjectID";
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar,50),
                                       new SqlParameter("@ProjectID",SqlDbType.Int)
                                   };
            param[0].Value = userinfo.CompanyCD;
            param[1].Value = projectID;
            string itemlist = string.Empty;
            DataTable dt = SqlHelper.ExecuteSql(sqlstr, param);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    itemlist += "," + dt.Rows[i][0].ToString() + "#" + dt.Rows[i][1].ToString() + "#" + dt.Rows[i][2].ToString();
                }
            }
            return itemlist.TrimStart(',');
        }

        public static DataTable GetProjectBudgetList(int pageindex, int pagecount, string projectid, string summaryname, string OrderBy, XBase.Common.UserInfoUtil userinfo, ref int totalCount)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine("select A.budgetID,A.CompanyCD,A.BudgetName,B.CodeName,convert(decimal(22,"+userinfo.SelPoint+"),budgetArea) budgetArea,C.ProjectName,D.status,A.seq,A.subBudgetID,sb.BudgetName AS subBudgetName from officedba.budgetSummary A ");
            sqlstr.AppendLine("left join officedba.CodeUnitType B on A.budgetUnit=B.ID");
            sqlstr.AppendLine("left join officedba.projectInfo C on A.ProjectID=C.ID");
            sqlstr.AppendLine("left join officedba.ProjectBudget D on A.ProjectID=D.ProjectID");
            sqlstr.AppendLine("LEFT JOIN officedba.SubBudget sb ON A.subBudgetID=sb.ID");
            sqlstr.AppendLine("where A.CompanyCD=@companyCD");

            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", userinfo.CompanyCD));
            if (projectid != "0")
            {
                sqlstr.AppendLine(" and A.ProjectID=@projectID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@projectID", projectid));
            }

            if (!string.IsNullOrEmpty(summaryname))
            {
                if (!string.IsNullOrEmpty(projectid))
                {
                    sqlstr.AppendLine(" and A.BudgetName=@BudgetName");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@BudgetName", summaryname));
                }
            }
            comm.CommandText = sqlstr.ToString();
            return SqlHelper.PagerWithCommand(comm, pageindex, pagecount, OrderBy, ref totalCount);
        }

        public static DataSet GetBudgetSummaryDetails(string itemID)
        {
            string sqlstr = @"select budgetID,CompanyCD,BudgetName,budgetUnit,budgetArea,ProjectID,seq,subBudgetID 
                                from officedba.budgetSummary 
                                where budgetID=@budgetID";
            SqlParameter[] param = {
                                       new SqlParameter("@budgetID",SqlDbType.VarChar,50)
                                   };
            param[0].Value = itemID;
            return SqlHelper.ExecuteSqlX(sqlstr, param);
        }

        public static int DeLProjectBudgetInfo(string budgetID)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine("delete from officedba.budgetSummary where budgetID in (" + budgetID + ")");
            sqlstr.AppendLine("delete from officedba.ProjectBaseNum where SummaryID in (" + budgetID + ")");
            sqlstr.AppendLine("delete from officedba.ProjectBudgetDetails where SummaryID in (" + budgetID + ")");
            //删除该项目明细
            TransactionManager tran = new TransactionManager();
            int num = 0;
            tran.BeginTransaction();
            try
            {
                num = SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, sqlstr.ToString());
                tran.Commit();
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                tran.Rollback();
            }
            return num;
        }

        #region  获取预算价格摘要列表
        /// <summary>
        /// 获取预算价格摘要列表
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagecount"></param>
        /// <param name="projectid"></param>
        /// <param name="summaryname"></param>
        /// <param name="OrderBy"></param>
        /// <param name="userinfo"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetProjectBudgetPriceList(int pageindex, int pagecount, string projectid, string summaryname, string OrderBy, XBase.Common.UserInfoUtil userinfo, ref int totalCount)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine("select A.budgetpriceID,A.CompanyCD,A.BudgetPriceName,convert(decimal(22,"+userinfo.SelPoint+"),A.UnitPrice) UnitPrice,isnull(A.Formula,'') as Formula,A.codeType,A.projectID,");
            sqlstr.AppendLine("case A.codeType when 0 then '基本元素' when 1 then '关系元素' end as CodeTypeText,C.ProjectName,D.status ");
            sqlstr.AppendLine(" from officedba.BudgetPrice A ");
            sqlstr.AppendLine(" left join officedba.projectInfo C on A.ProjectID=C.ID ");
            sqlstr.AppendLine(" left join officedba.ProjectBudget D on A.ProjectID=D.ProjectID and A.companyCD=D.CompanyCD ");
            sqlstr.AppendLine(" where A.CompanyCD=@companyCD");

            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", userinfo.CompanyCD));
            if (projectid != "0")
            {
                sqlstr.AppendLine(" and A.ProjectID=@projectID");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@projectID", projectid));
            }

            if (!string.IsNullOrEmpty(summaryname))
            {
                sqlstr.AppendLine(" and A.BudgetPriceName=@BudgetName");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BudgetName", summaryname));
            }
            comm.CommandText = sqlstr.ToString();
            return SqlHelper.PagerWithCommand(comm, pageindex, pagecount, OrderBy, ref totalCount);
        }

        /// <summary>
        /// 项目预算分析列表查询函数
        /// </summary>
        /// <param name="pageindex">页数</param>
        /// <param name="pagecount">行数</param>
        /// <param name="projectName">项目名称</param>
        /// <param name="OrderBy">排序</param>
        /// <param name="userinfo">登录信息</param>
        /// <param name="totalCount">返回总数</param>
        /// <returns></returns>
        public static DataTable GetProjectBudgetPriceList(int pageindex, int pagecount, string projectName, string OrderBy, XBase.Common.UserInfoUtil userinfo, ref int totalCount)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine("SELECT pb.ID ,pb.ProjectID, convert(decimal(22," + userinfo.SelPoint + "),pb.TotalPrice) TotalPrice, pb.BudgetPerson, pb.BudgetTime, pb.[status],pi1.ProjectName,ei.EmployeeName");
            sqlstr.AppendLine("FROM officedba.ProjectBudget pb");
            sqlstr.AppendLine("LEFT JOIN officedba.ProjectInfo pi1 ON pi1.ID = pb.ProjectID");
            sqlstr.AppendLine("LEFT JOIN officedba.EmployeeInfo ei  ON ei.ID = pb.BudgetPerson");
            sqlstr.AppendLine("WHERE pb.CompanyCD=@CompanyCD AND (ISNULL(pb.userlist,'')='' OR ','+pb.userlist+',' LIKE @UserID )  ");

            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", userinfo.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UserID", string.Format("%,{0},%", userinfo.EmployeeID)));

            if (!string.IsNullOrEmpty(projectName))
            {
                sqlstr.AppendLine(" AND pi1.ProjectName LIKE @ProjectName");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProjectName", String.Format("%{0}%", projectName)));
            }
            comm.CommandText = sqlstr.ToString();
            return SqlHelper.PagerWithCommand(comm, pageindex, pagecount, OrderBy, ref totalCount);
        }

        /// <summary>
        /// 删除项目预算分析
        /// </summary>
        /// <param name="ID">ID集合</param>
        /// <returns></returns>
        public static int DeletProjectBudgetPrice(string ID)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"declare @ProjectID INT 
                                SELECT @ProjectID=pb.ProjectID FROM officedba.ProjectBudget pb
                                WHERE pb.ID=@ID
                                DELETE FROM officedba.ProjectBudgetDetails WHERE ProjectID=@ProjectID
                                DELETE FROM officedba.ProjectBaseNum WHERE ProjectID=@ProjectID
                                DELETE FROM officedba.ProjectBudget WHERE ID=@ID");

            //删除该项目明细
            TransactionManager tran = new TransactionManager();
            int num = 0;
            tran.BeginTransaction();
            try
            {
                foreach (string item in ID.Split(','))
                {
                    SqlParameter[] param = {
                                       new SqlParameter("@ID",item)
                                   };
                    num = SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, sqlstr.ToString(), param);
                }
                tran.Commit();
            }
            catch
            {
                tran.Rollback();
            }
            return num;
        }


        #endregion

        #region 删除预算价格摘要
        /// <summary>
        /// 删除预算价格摘要
        /// </summary>
        /// <param name="budgetID"></param>
        /// <returns></returns>
        public static int DeLProjectBudgetPriceInfo(string budgetpriceID)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine("delete from officedba.BudgetPrice where budgetpriceID in (" + budgetpriceID + ")");
            SqlParameter[] param = {
                                       new SqlParameter("@budgetpriceID",budgetpriceID)
                                   };
            //删除该项目明细
            TransactionManager tran = new TransactionManager();
            int num = 0;
            tran.BeginTransaction();
            try
            {
                //num = SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, sqlstr.ToString(), param);
                num = SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, sqlstr.ToString());
                tran.Commit();
            }
            catch
            {
                tran.Rollback();
            }
            return num;
        }
        #endregion

        public static string GetEmployeeByID(string userlist)
        {
            string sqlstr = "select * from officedba.EmployeeInfo where ID in (" + userlist + ")";
            DataTable dt = SqlHelper.ExecuteSql(sqlstr);
            string userIDlist = string.Empty;
            string userNamelist = string.Empty;

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    userIDlist += "," + dt.Rows[i]["EmployeeName"].ToString();
                    userNamelist += "," + dt.Rows[i]["id"].ToString();
                }
                userIDlist = userIDlist.TrimStart(',');
                userNamelist = userNamelist.TrimStart(',');
            }
            return userIDlist + "#" + userNamelist;
        }

        public static int OPDataSure(string projectID, XBase.Common.UserInfoUtil userinfo, string status)
        {
            StringBuilder sqlstr = new StringBuilder();
            int num=0;
            if (!IsSure(projectID, userinfo.CompanyCD))
            {
                sqlstr.AppendLine("update officedba.ProjectBudget set status=" + status + " where CompanyCD=@CompanyCD and projectID=@ProjectID");
                SqlParameter[] param = {
                                           new SqlParameter("@CompanyCD",SqlDbType.VarChar,50),
                                           new SqlParameter("@ProjectID",SqlDbType.Int)
                                       };
                param[0].Value = userinfo.CompanyCD;
                param[1].Value = projectID;
                num = SqlHelper.ExecuteTransSql(sqlstr.ToString(), param);
            }
            else
            {
                num = 0;
            }
            return num;
        }
        #region 取消确认
        /// <summary>
        /// 取消确认
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="userInfo"></param>
        /// <param name="status"></param>
        /// <param name="typeflag"></param>
        /// <param name="typecode"></param>
        /// <returns></returns>
        public static int OPDataUnSure(string projectid, XBase.Common.UserInfoUtil userInfo, string status, int typeflag, int typecode)
        {
            int num = 0;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine("update officedba.ProjectBudget set status=@Status where CompanyCD=@CompanyCD and projectID=@ProjectID");
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD",userInfo.CompanyCD),
                                       new SqlParameter("@ProjectID",projectid),
                                       new SqlParameter("@Status",status)
                                   };
            TransactionManager tran = new TransactionManager();
            tran.BeginTransaction();
            try
            {
                XBase.Data.Common.FlowDBHelper.OperateCancelConfirm(userInfo.CompanyCD, typeflag, typecode,int.Parse(projectid), userInfo.UserID, tran);//撤销审批
                num = SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, sqlstr.ToString(), param);
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                num = 0;
                throw ex;
            }
            return num;
        }
        #endregion

        #region 判断该项目是否已经确认
        /// <summary>
        /// 判断该项目是否已经确认
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="strCompanyCD"></param>
        /// <returns>已经确认返回true，未确认返回false</returns>
        private static bool IsSure(string projectID, string strCompanyCD)
        {
            bool isSured = false;
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select count(1) from officedba.ProjectBudget");
            strSql.AppendLine(" where CompanyCD=@CompanyCD and projectID=@ProjectID and Status=@Status");
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD",strCompanyCD),
                                       new SqlParameter("@ProjectID",projectID),
                                       new SqlParameter("@Status",'2')
                                   };
            int iCount = 0;
            iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql.ToString(), param));
            if (iCount > 0)
            {
                isSured = true;
            }
            return isSured;
        }
        #endregion
        public static string ProjectBudgetState(string projectID, XBase.Common.UserInfoUtil userinfo)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine("select * from officedba.ProjectBudget where CompanyCD=@CompanyCD and projectID=@ProjectID");
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar,50),
                                       new SqlParameter("@ProjectID",SqlDbType.Int)
                                   };
            param[0].Value = userinfo.CompanyCD;
            param[1].Value = projectID;
            DataTable dt = SqlHelper.ExecuteSql(sqlstr.ToString(), param);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0]["status"].ToString();
            }
            else
            {
                return "1";
            }
        }

        public static DataTable GetBudgetPrice(string budgetpriceID, XBase.Common.UserInfoUtil userinfo)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine("select * from officedba.budgetPrice where CompanyCD=@companyCD and budgetpriceID=@budgetpriceID");
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar,50),
                                       new SqlParameter("@budgetpriceID",SqlDbType.Int)
                                   };
            param[0].Value = userinfo.CompanyCD;
            param[1].Value = budgetpriceID;
            DataTable dt = SqlHelper.ExecuteSql(sqlstr.ToString(), param);
            return dt;
        }

        public static int ChargeProjectSummaryName(string projectID, string subBudgetID, string projectName, XBase.Common.UserInfoUtil userinfo)
        {
            StringBuilder sqlstr = new StringBuilder();
            if (subBudgetID == "0")
            {
                sqlstr.AppendLine(@"select count(1) 
                                from officedba.budgetSummary 
                                where CompanyCD=@companyCD and budgetName=@budgetName 
                                        and ProjectID=@ProjectID");
            }
            else
            {
                sqlstr.AppendLine(@"select count(1) 
                                from officedba.budgetSummary 
                                where CompanyCD=@companyCD and budgetName=@budgetName 
                                        and ProjectID=@ProjectID AND subBudgetID=@subBudgetID");
            }
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar,50),
                                       new SqlParameter("@budgetName",SqlDbType.VarChar,200),
                                       new SqlParameter("@ProjectID",SqlDbType.Int),
                                       new SqlParameter("@subBudgetID",SqlDbType.Int)
                                   };
            param[0].Value = userinfo.CompanyCD;
            param[1].Value = projectName;
            param[2].Value = int.Parse(projectID);
            param[3].Value = int.Parse(subBudgetID);
            DataTable dt = SqlHelper.ExecuteSql(sqlstr.ToString(), param);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == "0")
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 0;
            }
        }

        
        public static DataSet GetSummaryList(string projectID, string targetprojectid, XBase.Common.UserInfoUtil userinfo)
        {
            StringBuilder sqlstr = new StringBuilder();

            sqlstr.AppendLine("select * from officedba.budgetSummary where CompanyCD=@companyCD and ProjectId=@ProjectId ");// and BudgetName not in");
            //sqlstr.AppendLine("(");
            //sqlstr.AppendLine("    select budgetName from officedba.budgetSummary where CompanyCD=@companyCD and ProjectId=@targetprojectid)");

            sqlstr.AppendLine("select * from officedba.budgetPrice where CompanyCD=@companyCD and codetype=0 and projectID=@ProjectId and BudgetPriceName not in");
            sqlstr.AppendLine("(");
            sqlstr.AppendLine("    select BudgetPriceName from officedba.budgetPrice where CompanyCD=@companyCD and codetype=0 and projectID=@targetprojectid)");

            SqlParameter[] param = {
                                       new SqlParameter("@companyCD",SqlDbType.VarChar,50),
                                       new SqlParameter("@ProjectId",SqlDbType.Int,4),
                                       new SqlParameter("@targetprojectid",SqlDbType.Int,4)
                                   };
            param[0].Value = userinfo.CompanyCD;
            param[1].Value = int.Parse(projectID);
            param[2].Value = int.Parse(targetprojectid);
            DataSet ds = SqlHelper.ExecuteSqlX(sqlstr.ToString(), param);
            return ds;
        }


        public static int CopySourceData(string projectid, string summarylist, string subBudgetID, string budgetNameList, XBase.Common.UserInfoUtil userinfo)
        {
            StringBuilder sqlstr = new StringBuilder();

            sqlstr.AppendLine(@"INSERT INTO officedba.budgetSummary(CompanyCd,BudgetName,budgetUnit,BudgetArea,ProjectID,subBudgetID)
                                SELECT CompanyCD,budgetName,budgetUnit,BudgetArea," + projectid + "," + subBudgetID
                                + " FROM officedba.budgetSummary WHERE CompanyCD=@companyCD AND budgetID in(" + summarylist + @")
                                    AND budgetName NOT IN(    
                                                SELECT budgetName
                                                FROM officedba.budgetSummary 
                                                WHERE budgetName IN(" + budgetNameList + ")  AND ProjectID = " + projectid + " AND subBudgetID=" + subBudgetID + ")");

            SqlParameter[] param = {
                                       new SqlParameter("@companyCD",userinfo.CompanyCD)
                                   };
            //删除该项目明细
            TransactionManager tran = new TransactionManager();
            int num = -1;
            tran.BeginTransaction();
            try
            {
                num = SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, sqlstr.ToString(), param);
                tran.Commit();
            }
            catch
            {
                tran.Rollback();
            }
            return num;
        }


        /// <summary>
        /// 根据项目获得所有分项预算概要
        /// </summary>
        /// <param name="userinfo">用户信息</param>
        /// <param name="projectID">项目代码</param>
        public static DataTable GetSubBedget(XBase.Common.UserInfoUtil userinfo, int projectID)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append(@"SELECT * FROM officedba.SubBudget sb
                            WHERE sb.CompanyCD=@CompanyCD AND sb.projectid=@projectid");
            SqlParameter[] param = {
                                     new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                     new SqlParameter("@projectid",SqlDbType.Int)
                                 };
            param[0].Value = userinfo.CompanyCD;
            param[1].Value = projectID;
            return SqlHelper.ExecuteSql(sqlstr.ToString(), param);
        }

        public static DataTable GetSubBedgetGroup(string projectID, XBase.Common.UserInfoUtil userinfo)
        {
            StringBuilder sqlstr = new StringBuilder();


            sqlstr.AppendLine("select * from officedba.subbudget where projectid=@ProjectID and CompanyCD=@CompanyCD");
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar,50),
                                       new SqlParameter("@ProjectID",SqlDbType.Int)
                                   };
            param[0].Value = userinfo.CompanyCD;
            param[1].Value = projectID;
            DataTable dt = SqlHelper.ExecuteSql(sqlstr.ToString(), param);
            return dt;
        }

        public static string GetBedgetList(string projectID, XBase.Common.UserInfoUtil userinfo)
        {
            StringBuilder sqlstr = new StringBuilder();


            sqlstr.AppendLine("select distinct Convert(varchar(100),subBudgetID) +'#'+ dbo.getSubBudgetName(subBudgetID,ProjectID) budgetIDList from officedba.budgetSummary where projectID = @projectID and CompanyCD=@CompanyCD");
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar,50),
                                       new SqlParameter("@ProjectID",SqlDbType.Int)
                                   };
            param[0].Value = userinfo.CompanyCD;
            param[1].Value = projectID;
            DataTable dt = SqlHelper.ExecuteSql(sqlstr.ToString(), param);
            string returnstr = string.Empty;
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    returnstr += "," + dt.Rows[i][0].ToString().TrimEnd('$');
                }
            }
            return returnstr.TrimStart(',');
        }

        #region 获取审批流程
        /// <summary>
        /// 获取审批流程
        /// </summary>
        /// <param name="ProjectID">项目ID</param>
        /// <param name="userInfo">Session用户信息</param>
        /// <returns></returns>
        public static DataTable GetBudgetFlowStatus(string ProjectID, XBase.Common.UserInfoUtil userInfo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select f.FlowStatus ");
            strSql.AppendLine(" from officedba.FlowInstance f ");
            strSql.AppendLine(" where f.CompanyCD=@CompanyCD and f.BillTypeFlag=13 ");
            strSql.AppendLine(" and f.BillTypeCode=2 and f.BillNo=@ProjectID ");
            strSql.AppendLine(" and f.ID=(select max(ID) from officedba.FlowInstance as f2 where f2.BillID=@ProjectID and f2.BillTypeFlag=13 and f2.BillTypeCode=2 )");

            SqlParameter[] param = { 
                                    new SqlParameter("@ProjectID",ProjectID),
                                    new SqlParameter("@CompanyCD",userInfo.CompanyCD)
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        #region 根据项目ID获取项目编号
        /// <summary>
        /// 根据项目ID获取项目编号
        /// </summary>
        /// <param name="projectID">项目ID</param>
        /// <returns>datatable</returns>
        public static DataTable GetProjectNoByID(string projectID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select ID,ProjectNo,ProjectName ");
            strSql.AppendLine(" from officedba.projectInfo where ID=@BillID ");
            SqlParameter[] param = { 
                                    new SqlParameter("@BillID",projectID)
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        public static DataTable GetPriceList(string projectid,string summaryid,string baseNum,XBase.Common.UserInfoUtil userinfo)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select A.ID,A.CompanyCD,A.ProjectID,A.SummaryID,A.PriceID,A.budgetValue,B.BudgetPriceName,convert(decimal(22,"+userinfo.SelPoint+"),"+baseNum+") baseNum from officedba.ProjectBudgetDetails A left join officedba.budgetPrice B");
            sb.AppendLine("on A.priceID = B.budgetPriceID  where A.projectid=@projectid and A.CompanyCD=@companyCD and A.summaryID=@summaryID and isnull(B.BudgetPriceName,'0') !='0'");

            SqlParameter[] param = {
                                       new SqlParameter("@companyCD",SqlDbType.VarChar,50),
                                       new SqlParameter("@projectid",SqlDbType.VarChar),
                                       new SqlParameter("@summaryID",SqlDbType.VarChar)
                                   };
            param[0].Value = userinfo.CompanyCD;
            param[1].Value = projectid;
            param[2].Value = summaryid;
            return SqlHelper.ExecuteSql(sb.ToString(), param);
        }

        public static DataTable GetSubBedgetGroupList(string projectID, XBase.Common.UserInfoUtil userinfo)
        {
            StringBuilder sqlstr = new StringBuilder();

             sqlstr.AppendLine("select D.*,convert(decimal(22,"+userinfo.SelPoint+"),isnull(E.subtotal,0)) subtotal from officedba.subbudget D left join");
             sqlstr.AppendLine("(");
             sqlstr.AppendLine("   select subBudgetID,sum((isnull(budgetValue,0)+isnull(budgetArea,0))*baseNum) subtotal from officedba.budgetSummary A left join officedba.ProjectBaseNum B ");
             sqlstr.AppendLine("   on A.budgetID=B.summaryID");
             sqlstr.AppendLine("   left join ");
             sqlstr.AppendLine("   (");
             sqlstr.AppendLine("       select summaryID,sum(isnull(budgetValue,0)) budgetValue from officedba.ProjectBudgetDetails where ProjectID=@ProjectID");
	         sqlstr.AppendLine("       group by summaryID");
             sqlstr.AppendLine("   )C on A.budgetID=C.summaryID");
             sqlstr.AppendLine("   group by subbudgetID");
            sqlstr.AppendLine(") E on D.ID=E.subBudgetID");
            sqlstr.AppendLine("where D.projectID=@ProjectID and CompanyCD=@CompanyCD");
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar,50),
                                       new SqlParameter("@ProjectID",SqlDbType.Int)
                                   };
            param[0].Value = userinfo.CompanyCD;
            param[1].Value = projectID;
            DataTable dt = SqlHelper.ExecuteSql(sqlstr.ToString(), param);
            return dt;
        }

        public static string getProjectName(string projectID)
        {
            SqlParameter[] param = { new SqlParameter("@projectid", SqlDbType.VarChar, 50) };
            param[0].Value = projectID;
            DataTable dt = SqlHelper.ExecuteSql("select * from officedba.ProjectInfo where ID=@projectid",param);
            if (dt != null)
            {
                return dt.Rows[0]["projectName"].ToString();
            }
            else
            {
                return "";
            }
        }

    }
}
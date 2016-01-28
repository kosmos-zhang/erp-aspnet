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
    public class DefFormDBHelper
    {
        /// <summary>
        /// 添加自定义表单
        /// </summary>
        /// <returns></returns>
        public static int AddTable(CustomTableModel model, List<StructTable> sonModel, out string strMsg)
        {
            int TableID = 0;
            strMsg = "";
            //判断单据编号是否存在
            if (!Exists(model.CompanyCD, model.CustomTableName))
            {
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    TableID = AddCustomTable(model, tran);
                    AddStructTable(sonModel, TableID, tran);
                    tran.Commit();
                    strMsg = "保存成功！";
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    strMsg = "保存失败，请联系系统管理员！";
                    throw ex;
                }
            }
            else
            {
                strMsg = "该表名已被使用，请输入未使用的表名！";
            }
            return TableID;
        }

        /// <summary>
        /// 修改自定义表单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sonModel"></param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public static bool EditTable(CustomTableModel model, List<StructTable> sonModel, out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            strMsg = "";
            string strSql = "delete from defdba.StructTable where  TableID=@TableID ";
            SqlParameter[] paras = { new SqlParameter("@TableID", model.ID) };
            TransactionManager tran = new TransactionManager();
            tran.BeginTransaction();
            try
            {

                EidtCustomTable(model, tran);
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), paras);
                AddStructTable(sonModel, Convert.ToInt32(model.ID), tran);
                tran.Commit();
                isSucc = true;
                strMsg = "保存成功！";
            }
            catch (Exception ex)
            {
                tran.Rollback();
                strMsg = "保存失败，请联系系统管理员！";
                throw ex;
            }
            return isSucc;
        }

        /// <summary>
        /// 删除自定义表单
        /// </summary>
        /// <param name="id">数据ID</param>
        /// <param name="tableName">物理表名</param>
        /// <param name="CompanyCD">机构代码</param>
        public static void DelTable(string id, string tableName, string CompanyCD)
        {
            TransactionManager tran = new TransactionManager();
            tran.BeginTransaction();
            try
            {
                if (!string.IsNullOrEmpty(tableName))
                {// 删除物理表
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DROP TABLE " + tableName, null);
                }
                // 删除表
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "delete from defdba.CustomTable where ID =" + id, null);
                // 删除表结构
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "delete from defdba.StructTable where TableID =" + id, null);
                // 删除模板表
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "delete from defdba.ModuleTable where TableID =" + id, null);

                // 删除菜单
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text
                    , string.Format("DELETE FROM defdba.CustomModule WHERE PropertyValue ='Pages/Office/DefManager/DefTableList.aspx?tableid={0}' AND CompanyCD='{1}' ", id, CompanyCD), null);

                tran.Commit();
            }
            catch
            {
                tran.Rollback();
            }
        }

        /// <summary>
        /// 根据表名判断表中是否存在数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static bool ExistHasData(string tableName)
        {
            DataTable dt = SqlHelper.ExecuteSql("select COUNT(*) from " + tableName);
            return int.Parse(dt.Rows[0][0].ToString()) > 0;
        }

        /// <summary>
        /// 获取自定义表单列表
        /// </summary>
        /// <param name="AliasTableName"></param>
        /// <param name="CompanyCD"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetTableList(string AliasTableName, string CompanyCD, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@" SELECT ct.ID,ct.CompanyCD,ct.CustomTableName,ct.AliasTableName,ct.STATUS,ct.IsDic,ct.ParentId,ISNULL(mt.ID,0) ModuleID 
                            FROM defdba.CustomTable ct
                            LEFT JOIN defdba.ModuleTable mt ON ct.ID=mt.TableID 
                            WHERE ct.CompanyCD=@CompanyCD ");
            if (!string.IsNullOrEmpty(AliasTableName))
            {
                sb.AppendLine(" AND ct.AliasTableName like '%" + AliasTableName + "%' ");
            }

            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", CompanyCD));

            return SqlHelper.CreateSqlByPageExcuteSqlArr(sb.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
        }

        /// <summary>
        /// 根据ID自定义表单明细
        /// </summary>
        /// <param name="TableId"></param>
        /// <returns></returns>
        public static DataSet GetTableById(string TableId)
        {
            DataSet ds = new DataSet();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" select * from defdba.CustomTable where ID=" + TableId);
            ds.Tables.Add(SqlHelper.ExecuteSql(sb.ToString()));

            StringBuilder sb1 = new StringBuilder();
            sb1.AppendLine(" select * from defdba.StructTable where TableID=" + TableId);
            ds.Tables.Add(SqlHelper.ExecuteSql(sb1.ToString()));

            return ds;
        }

        /// <summary>
        /// 添加表结构
        /// </summary>
        /// <param name="sonModel"></param>
        /// <param name="TableID"></param>
        /// <param name="tran"></param>
        public static void AddStructTable(List<StructTable> sonModel, int TableID, TransactionManager tran)
        {
            foreach (StructTable model in sonModel)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into defdba.StructTable(");
                strSql.Append("ccode,cname,type,length,isshow,seq,isempty,ismultiline,typeflag,TableID,isSearch,IsKeyword,IsTotal,ControlLength,isheadshow)");
                strSql.Append(" values (");
                strSql.Append("@ccode,@cname,@type,@length,@isshow,@seq,@isempty,@ismultiline,@typeflag,@TableID,@isSearch,@IsKeyword,@IsTotal,@ControlLength,@isheadshow)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
					new SqlParameter("@ccode", SqlDbType.VarChar,100),
					new SqlParameter("@cname", SqlDbType.VarChar,100),
					new SqlParameter("@type", SqlDbType.VarChar,100),
					new SqlParameter("@length", SqlDbType.Int,4),
					new SqlParameter("@isshow", SqlDbType.TinyInt,1),
					new SqlParameter("@seq", SqlDbType.Int,4),
					new SqlParameter("@isempty", SqlDbType.TinyInt,1),
					new SqlParameter("@ismultiline", SqlDbType.TinyInt,1),
					new SqlParameter("@typeflag", SqlDbType.Int,4),
					new SqlParameter("@TableID", SqlDbType.Int,4),
                    new SqlParameter("@isSearch", SqlDbType.Int,4),
                    new SqlParameter("@IsKeyword", SqlDbType.Int,4),
                    new SqlParameter("@IsTotal", SqlDbType.Int,4),
                    new SqlParameter("@ControlLength", SqlDbType.VarChar,50),
                    new SqlParameter("@isheadshow", SqlDbType.Int,4),
                                            };
                parameters[0].Value = model.ccode;
                parameters[1].Value = model.cname;
                parameters[2].Value = model.type;
                parameters[3].Value = model.length;
                parameters[4].Value = model.isshow;
                parameters[5].Value = model.seq;
                parameters[6].Value = model.isempty;
                parameters[7].Value = model.ismultiline;
                parameters[8].Value = model.typeflag;
                parameters[9].Value = TableID;
                parameters[10].Value = model.isSearch;
                parameters[11].Value = model.IsKeyword;
                parameters[12].Value = model.IsTotal;
                parameters[13].Value = model.ControlLength;
                parameters[14].Value = model.isheadshow;
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), parameters);
            }
        }

        /// <summary>
        /// 添加表信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static int AddCustomTable(CustomTableModel model, TransactionManager tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into defdba.CustomTable(");
            strSql.Append("CompanyCD,CustomTableName,AliasTableName,ParentId,ColumnNumber,IsDic,totalFlag)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@CustomTableName,@AliasTableName,@ParentId,@ColumnNumber,@IsDic,@totalFlag)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,50),
					new SqlParameter("@CustomTableName", SqlDbType.VarChar,100),
					new SqlParameter("@AliasTableName", SqlDbType.VarChar,300),
                    new SqlParameter("@ParentId", SqlDbType.Int,4),
                    new SqlParameter("@ColumnNumber", SqlDbType.Int,4),
                    new SqlParameter("@IsDic", SqlDbType.Int,4),
                    new SqlParameter("@totalFlag", SqlDbType.Int,4)};
            parameters[0].Value = model.CompanyCD;
            parameters[1].Value = model.CustomTableName;
            parameters[2].Value = model.AliasTableName;
            parameters[3].Value = model.ParentId;
            parameters[4].Value = model.ColumnNumber;
            parameters[5].Value = model.IsDic;
            parameters[6].Value = model.totalFlag;
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSql.ToString(), parameters));

        }

        /// <summary>
        /// 修改表信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="tran"></param>
        public static void EidtCustomTable(CustomTableModel model, TransactionManager tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update defdba.CustomTable set ");
            strSql.Append("CompanyCD=@CompanyCD,");
            strSql.Append("CustomTableName=@CustomTableName,");
            strSql.Append("ParentID=@ParentID,");
            strSql.Append("ColumnNumber=@ColumnNumber,");
            strSql.Append("AliasTableName=@AliasTableName,");
            strSql.Append("totalFlag=@totalFlag,");
            strSql.Append("IsDic=@IsDic");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,50),
					new SqlParameter("@CustomTableName", SqlDbType.VarChar,100),
                    new SqlParameter("@ParentID", SqlDbType.Int,4),
                    new SqlParameter("@ColumnNumber", SqlDbType.Int,4),
					new SqlParameter("@AliasTableName", SqlDbType.VarChar,300),
					new SqlParameter("@totalFlag", SqlDbType.Int),
					new SqlParameter("@IsDic", SqlDbType.Int)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.CompanyCD;
            parameters[2].Value = model.CustomTableName;
            parameters[3].Value = model.ParentId;
            parameters[4].Value = model.ColumnNumber;
            parameters[5].Value = model.AliasTableName;
            parameters[6].Value = model.totalFlag;
            parameters[7].Value = model.IsDic;
            SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        ///判断表是否存在
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="CustomTableName"></param>
        /// <returns></returns>
        public static bool Exists(string CompanyCD, string CustomTableName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from defdba.CustomTable");
            strSql.Append(" where CompanyCD=@CompanyCD and CustomTableName=@CustomTableName  ");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,50),
					new SqlParameter("@CustomTableName", SqlDbType.VarChar,100),};
            parameters[0].Value = CompanyCD;
            parameters[1].Value = CustomTableName;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新初始化值
        /// </summary>
        /// <param name="list">更新对象集合</param>
        /// <returns></returns>
        public static bool UpdateStruct(IList<StructTable> list)
        {
            ArrayList cmdList = new ArrayList();
            SqlCommand comm = null;

            string sqlStr = "UPDATE defdba.StructTable SET IsBind = @IsBind,dropdownlistValue = @dropdownlistValue WHERE id=@id ";

            foreach (StructTable info in list)
            {
                comm = new SqlCommand();
                comm.CommandText = sqlStr;
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@IsBind", SqlDbType.Int,4),
					new SqlParameter("@dropdownlistValue", SqlDbType.VarChar,8000)};
                comm.Parameters.AddRange(parameters);
                parameters[0].Value = info.id;
                if (info.IsBind.HasValue)
                {
                    parameters[1].Value = info.IsBind.Value;
                }
                else
                {
                    parameters[1].Value = DBNull.Value;
                }
                parameters[2].Value = info.DropdownlistValue;
                cmdList.Add(comm);
            }

            return SqlHelper.ExecuteTransWithArrayList(cmdList);
        }

        /// <summary>
        /// 获得字典表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetDictionary(string CompanyCD)
        {
            string sqlStr = "SELECT * FROM defdba.CustomTable WHERE CompanyCD='" + CompanyCD + "' AND Status=2 AND IsDic=1 ";

            return SqlHelper.ExecuteSql(sqlStr);
        }

        /// <summary>
        /// 表数据初始化
        /// </summary>
        /// <param name="TableID"></param>
        public static void InitTable(string TableID, string CompanyCD)
        {

            //获取主表
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(" select * from defdba.CustomTable where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = Convert.ToInt32(TableID);
            DataTable MainDT = SqlHelper.ExecuteSql(sqlStr.ToString(), parameters);

            //获取明细表
            StringBuilder sqlStr1 = new StringBuilder();
            sqlStr1.Append(" select * from defdba.StructTable where TableID=" + TableID);
            DataTable DetailDT = SqlHelper.ExecuteSql(sqlStr1.ToString());

            TransactionManager tran = new TransactionManager();
            tran.BeginTransaction();
            try
            {
                #region 创建表Sql

                //创建表Sql
                StringBuilder sqlCreate = new StringBuilder();
                if (MainDT != null && MainDT.Rows.Count > 0)
                {
                    if (DetailDT != null && DetailDT.Rows.Count > 0)
                    {
                        string IsKeyWord = "";

                        sqlCreate.Append(" create table define.");
                        sqlCreate.Append(CompanyCD + "_" + MainDT.Rows[0]["CustomTableName"].ToString());
                        sqlCreate.Append("(");
                        DataRow[] dr = DetailDT.Select(" IsKeyword=1 ");
                        //if (dr.Length == 0)
                        //{
                        sqlCreate.Append(" [ID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL, ");
                        //IsKeyWord = " [ID] asc ";
                        //}
                        sqlCreate.Append("[pid] [int] NULL, ");
                        for (int x = 0; x < DetailDT.Rows.Count; x++)
                        {
                            string isEmpty = " NULL";
                            if (DetailDT.Rows[x]["isempty"].ToString() == "0")
                            {
                                isEmpty = " NOT NULL ";
                            }
                            string isDH = ",";
                            if (x == DetailDT.Rows.Count - 1 && DetailDT.Rows[x]["IsKeyword"].ToString() == "0" && IsKeyWord == "")
                            {
                                isDH = " ";
                            }
                            if (DetailDT.Rows[x]["IsKeyword"].ToString() == "1")
                            {
                                IsKeyWord = " [" + DetailDT.Rows[x]["ccode"].ToString() + "] asc ";
                            }

                            //字符类型
                            if (DetailDT.Rows[x]["type"].ToString() == "varchar")
                            {
                                sqlCreate.Append(" [" + DetailDT.Rows[x]["ccode"].ToString() + "] [varchar](" + DetailDT.Rows[x]["length"].ToString() + ") COLLATE Chinese_PRC_CI_AS  " + isEmpty + isDH);
                            }
                            //整形
                            if (DetailDT.Rows[x]["type"].ToString() == "int")
                            {
                                sqlCreate.Append(" [" + DetailDT.Rows[x]["ccode"].ToString() + "] [int] " + isEmpty + isDH);
                            }
                            //时间类型
                            if (DetailDT.Rows[x]["type"].ToString() == "datetime")
                            {
                                sqlCreate.Append(" [" + DetailDT.Rows[x]["ccode"].ToString() + "] [datetime] " + isEmpty + isDH);
                            }
                            //浮点类型
                            if (DetailDT.Rows[x]["type"].ToString() == "decimal")
                            {
                                sqlCreate.Append(" [" + DetailDT.Rows[x]["ccode"].ToString() + "] [decimal](18, 2) " + isEmpty + isDH);
                            }
                        }

                        if (!string.IsNullOrEmpty(IsKeyWord))
                        {
                            sqlCreate.Append(" CONSTRAINT [pk_" + CompanyCD + "companyInfo" + TableID.ToString() + "] PRIMARY KEY CLUSTERED  ");
                            sqlCreate.Append("( ");
                            sqlCreate.Append(IsKeyWord);
                            sqlCreate.Append("");
                            sqlCreate.Append(" )WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]");
                        }
                        sqlCreate.Append(") ON [PRIMARY]");
                    }
                }

                #endregion

                SqlHelper.ExecuteScalar(sqlCreate.ToString(), null);

                SqlHelper.ExecuteScalar(" update defdba.CustomTable set status=2 where ID=" + TableID, null);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
        }

        /// <summary>
        /// 添加表达式
        /// </summary>
        public static void AddReguler(string Relation, int ColumnID, int TableId, int summaryFlag)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(" insert into defdba.RelationTable(relation,columnID,tableid,summaryflag) values(@relation,@columnID,@tableid,@summaryflag) ");
            sqlStr.Append(" ;update defdba.StructTable set IsTotal=1 where id=@columnID ");
            SqlParameter[] parameters = { new SqlParameter("@relation",SqlDbType.VarChar,1000),
                                      new SqlParameter("@columnID",SqlDbType.Int,4),
                                      new  SqlParameter("@tableid",SqlDbType.Int,4),
                                      new  SqlParameter("@summaryflag",SqlDbType.Int,4)
                                      };
            parameters[0].Value = Relation;
            parameters[1].Value = ColumnID;
            parameters[2].Value = TableId;
            parameters[3].Value = summaryFlag;
            SqlHelper.ExecuteTransSql(sqlStr.ToString(), parameters);
        }

        /// <summary>
        /// 更新表达式
        /// </summary>
        public static int ModReguler(string Relation, int ColumnID, int summaryFlag)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(" update defdba.RelationTable set  relation=@relation where columnID=@columnID and summaryflag=@summaryflag ");
            SqlParameter[] parameters = { new SqlParameter("@relation",SqlDbType.VarChar,1000),
                                          new SqlParameter("@columnID",SqlDbType.Int,4),
                                          new SqlParameter("@summaryflag",SqlDbType.Int,4)
                                          };
            parameters[0].Value = Relation;
            parameters[1].Value = ColumnID;
            parameters[2].Value = summaryFlag;
            return SqlHelper.ExecuteTransSql(sqlStr.ToString(), parameters);
        }


        /// <summary>
        /// 创建业务菜单
        /// </summary>
        /// <param name="tableid"></param>
        /// <param name="username"></param>
        public static int CreateMenu(string tableid, XBase.Common.UserInfoUtil userinfo, string userlist)
        {
            userlist = userlist.Trim();
            
            try
            {
                string ModuleID = string.Empty;
                string oldModuleID = string.Empty;
                string alignTableName = string.Empty;

                String sqlstr = "select ModuleID from defdba.CustomModule where PropertyValue = '" + "Pages/Office/DefManager/DefTableList.aspx?tableid=" + tableid + "'";
                DataTable dt = SqlHelper.ExecuteSql(sqlstr);
                if (dt != null && dt.Rows.Count > 0)
                {
                    //修改菜单
                    sqlstr = "update defdba.CustomModule set userdUserList='" + userlist + "', PropertyValue='Pages/Office/DefManager/DefTableList.aspx?tableid=" + tableid + "' where ModuleID=" + dt.Rows[0][0].ToString() + " and CompanyCD='" + userinfo.CompanyCD + "'";
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
                    sqlstr = "select isnull(Max(ModuleID)," + submenu + ") from defdba.CustomModule where parentID='" + menuid + "' and CompanyCD='" + userinfo.CompanyCD + "'";
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
                    sqlstr = "select AliasTableName from defdba.CustomTable where ID=" + tableid;
                    dt = SqlHelper.ExecuteSql(sqlstr);
                    alignTableName = dt.Rows[0][0].ToString();

                    StringBuilder sqlStr = new StringBuilder();
                    sqlStr.Append("insert into defdba.CustomModule(CompanyCD,ModuleID,ModuleName,ParentID,ModuleType,PropertyType,PropertyValue,userdUserList) values(@companycd,@moduleID,@alignName,@menuid,'M','link',@pathpage,@userdUserList)");
                    SqlParameter[] parameters = { 
                                      new SqlParameter("@moduleID",SqlDbType.VarChar,100),
                                      new SqlParameter("@alignName",SqlDbType.VarChar,100),
                                      new SqlParameter("@menuid",SqlDbType.VarChar,100),
                                      new SqlParameter("@tableid",SqlDbType.VarChar,100),
                                      new SqlParameter("@pathpage",SqlDbType.VarChar,200),
                                      new SqlParameter("@companycd",SqlDbType.VarChar,100),
                                      new SqlParameter("@userdUserList",SqlDbType.VarChar,100)
                                      };
                    parameters[0].Value = ModuleID;
                    parameters[1].Value = alignTableName;
                    parameters[2].Value = menuid;
                    parameters[3].Value = tableid;
                    parameters[4].Value = "Pages/Office/DefManager/DefTableList.aspx?tableid=" + tableid;
                    parameters[5].Value = userinfo.CompanyCD;
                    parameters[6].Value = userlist;
                    SqlHelper.ExecuteTransSql(sqlStr.ToString(), parameters);
                    return 1;
                }
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 获得表达式
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetRegular(int ColumnID)
        {
            string sqlStr = "SELECT * FROM defdba.RelationTable WHERE columnID=" + ColumnID;
            return SqlHelper.ExecuteSql(sqlStr);
        }

        /// <summary>
        /// 获取从属表
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetParentDT(string CompanyCD)
        {
            StringBuilder sqlStr = new StringBuilder("select * from defdba.CustomTable where parentID=0 and CompanyCD='" + CompanyCD + "'");
            return SqlHelper.ExecuteSql(sqlStr.ToString());
        }



    }
}

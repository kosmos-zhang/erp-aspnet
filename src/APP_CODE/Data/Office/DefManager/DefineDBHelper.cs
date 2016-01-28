using System;
using XBase.Model.Office.SupplyChain;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

using System.IO;
namespace XBase.Data.Office.DefManager
{
    public class DefineDBHelper
    {
        #region 获取表单头标题
        public static string GetDefineTableByCode(string tableid)
        {
            string tablehead = string.Empty;
            SqlParameter[] parameters = { new SqlParameter("@tableid", SqlDbType.VarChar, 50) };
            parameters[0].Value = tableid;
            DataTable definetable = SqlHelper.ExecuteSql("select * from defdba.CustomTable where ID=@tableid", parameters);
            try
            {
                tablehead = definetable.Rows[0]["AliasTableName"].ToString();
            }
            catch
            {
                tablehead = "";
            }
            return tablehead;
        }

        ////查询页面控件绑定
        //public static DataSet GetTableStruct(string tableid, bool isSearch)
        //{
        //    string sqlstr = "select *,cname+'('+ccode+')' cnameCn from defdba.StructTable where isshow=1 and  isSearch = '1' and TableID=@tableid order by seq";
        //    SqlParameter[] parameters = { new SqlParameter("@tableid", SqlDbType.VarChar, 50) };
        //    parameters[0].Value = tableid;
        //    try
        //    {
        //        return SqlHelper.ExecuteSqlX(sqlstr, parameters);
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        public static DataSet GetTableStruct(string tableid, bool isSearch,bool isShow)
        {
            string sqlstr = "select *,cname+'('+ccode+')' cnameCn from defdba.StructTable where isshow=1 and isSearch=1  and TableID=@tableid order by seq";
            SqlParameter[] parameters = { new SqlParameter("@tableid", SqlDbType.VarChar, 50) };
            parameters[0].Value = tableid;
            try
            {
                return SqlHelper.ExecuteSqlX(sqlstr, parameters);
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region 获取表的详细结构
        public static DataSet GetTableStruct(string tableid)
        {
            string sqlstr = "select *,cname+'('+ccode+')' cnameCn from defdba.StructTable where isshow=1 and TableID=@tableid order by seq";
            SqlParameter[] parameters = { new SqlParameter("@tableid", SqlDbType.VarChar, 50) };
            parameters[0].Value = tableid;
            try
            {
                return SqlHelper.ExecuteSqlX(sqlstr, parameters);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        /// <summary>
        /// 判断给定表ID的表是否有自定义模板
        /// </summary>
        /// <param name="tableid"></param>
        /// <returns></returns>
        public static bool GetModuleByTableID(string tableid)
        {
            string sqlstr = "select count(1) from defdba.ModuleTable where TableID=@tableid and UseStatus=1";
            SqlParameter[] parameters = { new SqlParameter("@tableid", SqlDbType.VarChar, 50) };
            parameters[0].Value = tableid;
            int num = Convert.ToInt32(SqlHelper.ExecuteScalar(sqlstr, parameters));
            return num > 0 ? true : false;
        }

        /// <summary>
        /// 根据条件获得定义的table的列表
        /// </summary>
        /// <param name="tableid"></param>
        /// <returns></returns>
        public static DataTable GetDefTableList(Hashtable hs, ref string tablename, ref int Totalcount)
        {
            SqlParameter[] parameters = { new SqlParameter("@tableid", SqlDbType.VarChar, 50) };
            parameters[0].Value = hs["tableid"] + "";
            DataTable definetable = SqlHelper.ExecuteSql("select * from defdba.CustomTable where ID=@tableid", parameters);

            string TableName = string.Empty;
            string sqlstr = string.Empty;
            if (definetable != null)
            {
                TableName = "define." + definetable.Rows[0]["CompanyCD"] + "_" + definetable.Rows[0]["CustomTableName"];
                tablename = definetable.Rows[0]["AliasTableName"]+""; 
            }
            else
            {
                return null;
            }
            string[] fieldsName = (string[])hs["Fields"];
            sqlstr = " select * from    " + TableName + " where 1=1  ";

            ArrayList al = new ArrayList();

            foreach (string nameinfo in fieldsName)
            {
                if (nameinfo.Trim() == "")
                {
                    continue;
                }
                string name = nameinfo.Split('#')[0];
                string type = nameinfo.Split('#')[1];
                string length = nameinfo.Split('#')[2];
                if (type == "datetime")
                {
                    sqlstr += "  and  " + name + ">=@" + name + "1      and   " + name + "<=@" + name + "2";
                    SqlParameter pa1 = new SqlParameter("@" + name + "1", SqlDbType.DateTime);
                    try
                    {
                        pa1.Value = Convert.ToDateTime(hs[name + "1"]);
                    }
                    catch {
                        pa1.Value = null;
                    }
                    SqlParameter pa2 = new SqlParameter("@" + name + "2", SqlDbType.DateTime);
                    pa2.Value = Convert.ToDateTime(hs[name + "2"]);
                    al.Add(pa1);
                    al.Add(pa2);
                }
                else
                {
                    if ((hs[name] + "").Trim() == "")
                    {
                        continue;
                    }
                    sqlstr += "  and  " + name + "=@" + name;

                    switch (type)
                    {
                        case "varchar":
                            {
                                SqlParameter pa = new SqlParameter("@" + name, SqlDbType.VarChar);
                                pa.Value = hs[name] + "";
                                al.Add(pa);
                                break;
                            }
                        case "int":
                            {
                                SqlParameter pa = new SqlParameter("@" + name, SqlDbType.Int);
                                pa.Value = int.Parse(hs[name] + "");
                                al.Add(pa);
                                break;
                            }
                        case "datetime":
                            {
                                SqlParameter pa1 = new SqlParameter("@" + name + "1", SqlDbType.DateTime);
                                pa1.Value = Convert.ToDateTime(hs[name + "1"]);
                                SqlParameter pa2 = new SqlParameter("@" + name + "2", SqlDbType.DateTime);
                                pa2.Value = Convert.ToDateTime(hs[name + "2"]);
                                al.Add(pa1);
                                al.Add(pa2);
                                break;
                            }
                        case "float":
                            {
                                SqlParameter pa = new SqlParameter("@" + name, SqlDbType.Float);
                                pa.Value = float.Parse(hs[name] + "");
                                al.Add(pa);
                                break;
                            }
                        case "char":
                            {
                                SqlParameter pa = new SqlParameter("@" + name, SqlDbType.Char);
                                pa.Value = char.Parse(hs[name] + "");
                                al.Add(pa);
                                break;
                            }
                        case "decimal":
                            {
                                SqlParameter pa = new SqlParameter("@" + name, SqlDbType.Decimal);
                                if ((hs[name] + "").Trim() == "")
                                {
                                    pa.Value = 0.0;
                                }
                                else
                                {
                                    pa.Value = decimal.Parse(hs[name] + "");
                                }
                                al.Add(pa);
                                break;
                            }
                        case "text":
                            {
                                SqlParameter pa = new SqlParameter("@" + name, SqlDbType.Text);
                                pa.Value = hs[name] + "";
                                al.Add(pa);
                                break;
                            }
                        case "numeric":
                            {
                                SqlParameter pa = new SqlParameter("@" + name, SqlDbType.Decimal);
                                pa.Value = decimal.Parse(hs[name] + "");
                                al.Add(pa);
                                break;
                            }
                    }
                }
            }
            SqlParameter[] param = new SqlParameter[al.Count];
            for (int i = 0; i < al.Count; i++)
            {
                param[i] = (SqlParameter)al[i];
            }
            Totalcount =0;
            try
            {
                return SqlHelper.CreateSqlByPageExcuteSql(sqlstr, int.Parse(hs["PageIndex"] + ""), int.Parse(hs["PageCount"] + ""), hs["OrderBy"] + "", param, ref Totalcount);
            }
            catch
            {
                return null;
            }
            //return SqlHelper.ExecuteSqlX(sqlstr, parameters);
        }



        /// <summary>
        /// 获取模板内容
        /// </summary>
        /// <param name="tableid"></param>
        /// <returns></returns>
        public static DataSet GetModuleValueByTableID(string tableid)
        {
            string sqlstr = "select * from defdba.ModuleTable where TableID=@tableid and UseStatus=1";
            SqlParameter[] parameters = { new SqlParameter("@tableid", SqlDbType.VarChar, 50) };
            parameters[0].Value = tableid;
            return SqlHelper.ExecuteSqlX(sqlstr, parameters);
        }


        /// <summary>
        /// 添加自定义表
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        public static int AddDefTable(Hashtable hs)
        {
            int result = 0;
            string SQLstring = @" SELECT [ID],[CompanyCD],[CustomTableName],[AliasTableName] FROM [defdba].[CustomTable] ";
            SQLstring += " where  [ID]= " + hs["TableID"] + " and [Status] = '2'  ";
            DataTable dt = new DataTable();
            dt = DBHelper.SqlHelper.ExecuteSql(SQLstring);
            if (dt == null)
            {
                result = -1; //添加失败，找不到对应的表
            }
            else if (dt.Rows.Count <= 0)
            {
                result = -1; //添加失败，找不到对应的表
            }
            else
            {
                string[] ColName = hs["Fieds"].ToString().Split(',');
                string FiedsTemp = string.Empty;
                if (hs["Fieds"].ToString().Length > 0)
                {
                    string[] arr = hs["Fieds"].ToString().Split(',');
                    for (int i = 0; i < arr.Length; i++)
                    {
                        FiedsTemp += ",[" + arr[i] + "]";
                    }
                    FiedsTemp = FiedsTemp.TrimStart(',');
                }
                SQLstring = "  insert into  [define]." + dt.Rows[0]["CompanyCD"] + "_" + dt.Rows[0]["CustomTableName"] + "  ( " + FiedsTemp + ")  values ( ";
                SQLstring += "  @" + hs["Fieds"].ToString().Replace(",", ",@") + " );";
                SQLstring += "select  @ID= @@IDENTITY ";
                try
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Parameters.AddWithValue("@ID", SqlDbType.Int);
                    comm.Parameters["@ID"].Direction = ParameterDirection.Output;
                    comm.CommandText = SQLstring;
                    foreach (string name in ColName)
                    {
                        switch (hs[name].ToString().Split('|')[1].ToLower())
                        {
                            case "varchar":
                                comm.Parameters.AddWithValue("@" + name, SqlDbType.VarChar);
                                comm.Parameters["@" + name].Value = hs[name].ToString().Split('|')[0] + "";
                                break;
                            case "int":
                                comm.Parameters.AddWithValue("@" + name, SqlDbType.Int);
                                comm.Parameters["@" + name].Value = int.Parse(hs[name].ToString().Split('|')[0] + "");
                                break;
                            case "datetime":
                                comm.Parameters.AddWithValue("@" + name, SqlDbType.DateTime);
                                if ((hs[name].ToString().Split('|')[0] + "") != "")
                                {
                                    comm.Parameters["@" + name].Value = Convert.ToDateTime(hs[name].ToString().Split('|')[0]);
                                }
                                else
                                {
                                    comm.Parameters["@" + name].Value = DBNull.Value;
                                }
                                break;
                            case "float":
                                comm.Parameters.AddWithValue("@" + name, SqlDbType.Float);
                                comm.Parameters["@" + name].Value = float.Parse(hs[name].ToString().Split('|')[0] + "");
                                break;
                            case "char":
                                comm.Parameters.AddWithValue("@" + name, SqlDbType.Char);
                                comm.Parameters["@" + name].Value = char.Parse(hs[name].ToString().Split('|')[0] + "");
                                break;
                            case "decimal":
                                comm.Parameters.AddWithValue("@" + name, SqlDbType.Decimal);
                                if ((hs[name].ToString().Split('|')[0] + "").Trim() == "")
                                {
                                    comm.Parameters["@" + name].Value = 0.0;
                                }
                                else
                                {
                                    comm.Parameters["@" + name].Value = decimal.Parse(hs[name].ToString().Split('|')[0] + "");
                                }
                                break;
                            case "text":
                                comm.Parameters.AddWithValue("@" + name, SqlDbType.Text);
                                comm.Parameters["@" + name].Value = hs[name].ToString().Split('|')[0] + "";
                                break;
                            case "numeric":
                                comm.Parameters.AddWithValue("@" + name, SqlDbType.Decimal);
                                comm.Parameters["@" + name].Value = decimal.Parse(hs[name].ToString().Split('|')[0] + "");
                                break;
                        }
                    }
                    DBHelper.SqlHelper.ExecuteTransWithCommand(comm);
                    if ((hs["isHasDetails"] + "") == "1" && (hs["TableDetailsInfo"] + "") != "")
                    {
                        AddDetailsTable(hs["TableDetailsInfo"] + "", hs["CompanyCD"] + "", int.Parse(comm.Parameters["@ID"].Value.ToString()));
                    }
                    result = int.Parse(comm.Parameters["@ID"].Value.ToString());//添加成功
                }
                catch
                {
                    result = -2;//数据保存失败
                }
            }
            return result;
        }

        public static DataTable GetDetailsTableStruct(string tableid, ref  string info)
        {
            string SQLstring = @" SELECT [ID],[CompanyCD],[CustomTableName],[AliasTableName],[Status]  FROM [defdba].[CustomTable] ";
            SQLstring += " where  [ParentId]= " + tableid + "   ";
            DataTable dt = new DataTable();
            bool isTableOk = true;
            dt = DBHelper.SqlHelper.ExecuteSql(SQLstring);
            if (dt == null)
            {
                info += "明细表没有建立";
                isTableOk = false;
            }
            else if (dt.Rows.Count <= 0)
            {
                info += "没有明细";
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if ((dr["Status"] + "") == "1")
                    {
                        info += dr["AliasTableName"] + "没有建立@HH";
                        isTableOk = false;
                    }
                }
            }

            if (isTableOk == true)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }


        public static DataSet FillPageData(string tableid, string id, ref  string info)
        {
            SqlParameter[] parameters = { new SqlParameter("@tableid", SqlDbType.VarChar, 50) };
            parameters[0].Value = tableid;
            DataTable definetable = SqlHelper.ExecuteSql(" select * from defdba.CustomTable where ID=@tableid   or ParentId =@tableid  ", parameters);
            DataSet ds = new DataSet();
            if (definetable != null)
            {
                string[] sqlstrs = new string[definetable.Rows.Count];
                for (int i = 0; i < definetable.Rows.Count; i++)
                {
                    if ((definetable.Rows[i]["ID"] + "") == tableid)
                    {
                        sqlstrs[i] = "select *,0 as ParentId, 'MainTable'  as TableName   from   define." + definetable.Rows[i]["CompanyCD"] + "_" + definetable.Rows[i]["CustomTableName"] + "  where  ID =" + id;
                    }
                    else
                    {
                        sqlstrs[i] = "select *," + tableid + " as ParentId,'" + definetable.Rows[i]["CustomTableName"] + "'  as TableName   from   define." + definetable.Rows[i]["CompanyCD"] + "_" + definetable.Rows[i]["CustomTableName"] + "  where  pid =" + id;
                    }
                }
                try
                {
                    ds = SqlHelper.ExecuteForListWithSQL(sqlstrs);
                }
                catch { ds = null; }
            }
            else
            {
                return null;
            }
            return ds;


        }


        /// <summary>
        /// 修改自定义表
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        public static int UpdateDefTable(Hashtable hs)
        {
            int result = 0;
            string SQLstring = @" SELECT [ID] ,[CompanyCD],[CustomTableName],[AliasTableName] FROM [defdba].[CustomTable] ";
            SQLstring += " where  [ID]= " + hs["TableID"] + "  and  [Status] = '2'  ";

            DataTable dt = new DataTable();
            dt = DBHelper.SqlHelper.ExecuteSql(SQLstring);
            if (dt == null)
            {
                result = 0;
            }
            else if (dt.Rows.Count <= 0)
            {
                result = 0;
            }
            else
            {
                string[] ColName = hs["Fieds"].ToString().Split(',');
                SQLstring = " update  [define]." + dt.Rows[0]["CompanyCD"] + "_" + dt.Rows[0]["CustomTableName"] + " set  ";
                try
                {
                    SqlCommand comm = new SqlCommand();
                    foreach (string name in ColName)
                    {
                        SQLstring += name + "=" + "@" + name + ",";
                        switch (hs[name].ToString().Split('|')[1].ToLower())
                        {
                            case "varchar":
                                comm.Parameters.AddWithValue("@" + name, SqlDbType.VarChar);
                                comm.Parameters["@" + name].Value = hs[name].ToString().Split('|')[0] + "";
                                break;
                            case "int":
                                comm.Parameters.AddWithValue("@" + name, SqlDbType.Int);
                                comm.Parameters["@" + name].Value = int.Parse(hs[name].ToString().Split('|')[0] + "");
                                break;
                            case "datetime":
                                comm.Parameters.AddWithValue("@" + name, SqlDbType.DateTime);
                                if ((hs[name].ToString().Split('|')[0] + "") != "")
                                {
                                    comm.Parameters["@" + name].Value = Convert.ToDateTime(hs[name].ToString().Split('|')[0]);
                                }
                                else
                                {
                                    comm.Parameters["@" + name].Value = DBNull.Value;
                                }
                                break;
                            case "float":
                                comm.Parameters.AddWithValue("@" + name, SqlDbType.Float);
                                comm.Parameters["@" + name].Value = float.Parse(hs[name].ToString().Split('|')[0] + "");
                                break;
                            case "char":
                                comm.Parameters.AddWithValue("@" + name, SqlDbType.Char);
                                comm.Parameters["@" + name].Value = char.Parse(hs[name].ToString().Split('|')[0] + "");
                                break;
                            case "decimal":
                                comm.Parameters.AddWithValue("@" + name, SqlDbType.Decimal);
                                if ((hs[name].ToString().Split('|')[0] + "").Trim() == "")
                                {
                                    comm.Parameters["@" + name].Value = 0.0;
                                }
                                else
                                {
                                    comm.Parameters["@" + name].Value = decimal.Parse(hs[name].ToString().Split('|')[0] + "");
                                }
                                break;
                            case "text":
                                comm.Parameters.AddWithValue("@" + name, SqlDbType.Text);
                                comm.Parameters["@" + name].Value = hs[name].ToString().Split('|')[0] + "";
                                break;
                            case "numeric":
                                comm.Parameters.AddWithValue("@" + name, SqlDbType.Decimal);
                                comm.Parameters["@" + name].Value = decimal.Parse(hs[name].ToString().Split('|')[0] + "");
                                break;
                        }
                    }
                    SQLstring = SQLstring.Substring(0, SQLstring.Length - 1);
                    SQLstring += "  where ID=@ID ";
                    comm.Parameters.AddWithValue("@ID", SqlDbType.Int);
                    comm.Parameters["@ID"].Value = int.Parse(hs["ID"] + "");

                    comm.CommandText = SQLstring;
                    DBHelper.SqlHelper.ExecuteTransWithCommand(comm);
                    if ((hs["isHasDetails"] + "") == "1")
                    {
                        DelDetails(hs["TableID"] + "", comm.Parameters["@ID"].Value.ToString());
                        AddDetailsTable(hs["TableDetailsInfo"] + "", hs["CompanyCD"] + "", int.Parse(comm.Parameters["@ID"].Value.ToString()));
                    }
                    result = int.Parse(hs["ID"] + "");
                }
                catch
                {
                    result = -1;
                }
            }
            return result;
        }

        /// <summary>
        /// 获取表字段之间的运算关系
        /// </summary>
        /// <param name="tableid"></param>
        /// <returns></returns>
        public static string GetRelationByTableID(string tableid)
        {
            string returnstr = string.Empty;
            SqlParameter[] parameters = { new SqlParameter("@tableid", SqlDbType.VarChar, 50) };
            parameters[0].Value = tableid;
            DataSet ds = SqlHelper.ExecuteSqlX("select * from defdba.RelationTable where summaryflag=0 and tableid=@tableid", parameters);
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    returnstr += "," + ds.Tables[0].Rows[i]["relation"].ToString();
                }
                returnstr = returnstr.TrimStart(',');
            }
            return returnstr;
        }

        /// <summary>
        /// 根据父接点找子接点的表字段之间的关系
        /// </summary>
        /// <param name="tableid"></param>
        /// <returns></returns>
        public static string GetRelationByParentTableID(string tableid,string flag)
        {
            string returnstr = string.Empty;
            SqlParameter[] parameters = { new SqlParameter("@tableid", SqlDbType.VarChar, 50),
                                          new SqlParameter("@flag", SqlDbType.VarChar, 50)};
            parameters[0].Value = tableid;
            parameters[1].Value = flag;
            StringBuilder str = new StringBuilder();
            str.AppendLine("select B.ID,B.relation+A.CustomTableName relation,B.columnID,B.tableID from defdba.CustomTable A inner join");
            str.AppendLine("(");
	        str.AppendLine("select * from defdba.RelationTable where summaryflag=@flag and tableid in");
	        str.AppendLine("(");
            str.AppendLine("select ID from defdba.CustomTable where ParentID=@tableid");
	        str.AppendLine(")");
            str.AppendLine(")B");
            str.AppendLine("on A.id=B.tableid");
            
            DataSet ds = SqlHelper.ExecuteSqlX(str.ToString(), parameters);
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    returnstr += "," + ds.Tables[0].Rows[i]["relation"].ToString();
                }
                returnstr = returnstr.TrimStart(',');
            }
            return returnstr;
        }

        /// <summary>
        /// 获取明细表
        /// </summary>
        /// <param name="tableid"></param>
        /// <returns></returns>
        public static string GetRelationTable(string tableid)
        {
            string returnstr = string.Empty;
            SqlParameter[] parameters = { new SqlParameter("@tableid", SqlDbType.VarChar, 50) };
            parameters[0].Value = tableid;
            DataSet ds = SqlHelper.ExecuteSqlX("select * from defdba.CustomTable where ParentId=@tableid", parameters);
            string relationAll = string.Empty;
            if (ds != null)
            {
                DataSet relationDS = new DataSet();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string id = ds.Tables[0].Rows[i][0].ToString();
                    string tablename = ds.Tables[0].Rows[i][2].ToString();
                    string totalflag = ds.Tables[0].Rows[i]["totalFlag"].ToString();
                    string sqlstr = "select '" + tablename + "' tablename,*,'" + totalflag + "' totalflag from defdba.StructTable where isshow=1 and TableID=" + id;
                    relationDS = SqlHelper.ExecuteSqlX(sqlstr);
                    string relation = string.Empty;
                    if (relationDS != null)
                    {
                        for (int ii = 0; ii < relationDS.Tables[0].Rows.Count; ii++)
                        {
                            relation = relation + "," + relationDS.Tables[0].Rows[ii]["tablename"].ToString() + "#" + relationDS.Tables[0].Rows[ii]["ccode"].ToString() + "#" + relationDS.Tables[0].Rows[ii]["type"].ToString() + "#" + relationDS.Tables[0].Rows[ii]["typeflag"].ToString() + "#" + relationDS.Tables[0].Rows[ii]["length"].ToString() + "#" + relationDS.Tables[0].Rows[ii]["isempty"].ToString() + "#" + relationDS.Tables[0].Rows[ii]["cname"].ToString() + "#" + relationDS.Tables[0].Rows[ii]["dropdownlistValue"].ToString() + "#" + relationDS.Tables[0].Rows[ii]["totalflag"].ToString();
                        }
                        relation = relation.TrimStart(',');
                    }
                    if (relation.Length > 0)
                    {
                        relationAll = relationAll + "@" + relation;
                    }
                }
                relationAll = relationAll.TrimStart('@');
            }
            return relationAll;
        }

        private static bool DelDetails(string Tableid, string id)
        {
            string SQLstring = @" SELECT [ID],[CompanyCD],[CustomTableName],[AliasTableName],[Status]  FROM [defdba].[CustomTable] ";
            SQLstring += " where  [ParentId]= " + Tableid + "   ";
            DataTable dt = new DataTable();
            dt = DBHelper.SqlHelper.ExecuteSql(SQLstring);
            if (dt == null)
            {
            }
            else if (dt.Rows.Count <= 0)
            {

            }
            else
            {
                string delstr = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    delstr += " delete  from   define." + dt.Rows[i]["CompanyCD"] + "_" + dt.Rows[i]["CustomTableName"] + "   where pid=" + id + "  ";
                }
                SqlCommand comm = new SqlCommand();
                comm.CommandText = delstr;
                DBHelper.SqlHelper.ExecuteTransWithCommand(comm);
            }
            return true;
        }

        private static bool AddDetailsTable(string DetailsInfoString, string CompanyCD, int pid)
        {
            string[] AllDetailsInfo = DetailsInfoString.Split('@');
            string SQLStr = "";
            SqlCommand comm = new SqlCommand();
            int tempindex = 0;
            foreach (string OntDetailsTableInfo in AllDetailsInfo)
            {
                string[] DetailsRecords = OntDetailsTableInfo.Split(',');

                SQLStr += " insert into  define." + CompanyCD + "_" + OntDetailsTableInfo.Split('#')[0] + "    ";
                string col = "";
                string val = "";
                foreach (string OneRecord in DetailsRecords)
                {
                    tempindex++;
                    string[] ColInfo = OneRecord.Split('#');
                    col += ColInfo[1] + ",";
                    val += "@" + ColInfo[0] + ColInfo[1] + tempindex.ToString() + ",";
                    #region
                    switch (ColInfo[2] + "")
                    {
                        case "varchar":
                            comm.Parameters.AddWithValue("@" + ColInfo[0] + ColInfo[1] + tempindex.ToString(), SqlDbType.VarChar);
                            string str = ColInfo[9];
                            comm.Parameters["@" + ColInfo[0] + ColInfo[1] + tempindex.ToString()].Value = ColInfo[9];
                            break;
                        case "int":
                            comm.Parameters.AddWithValue("@" + ColInfo[0] + ColInfo[1] + tempindex.ToString(), SqlDbType.Int);
                            str = ColInfo[9];
                            comm.Parameters["@" + ColInfo[0] + ColInfo[1] + tempindex.ToString()].Value = int.Parse(ColInfo[9]);
                            break;
                        case "datetime":
                            comm.Parameters.AddWithValue("@" + ColInfo[0] + ColInfo[1] + tempindex.ToString(), SqlDbType.DateTime);
                            if (ColInfo[9] != "")
                            {
                                try
                                {
                                    str = ColInfo[9];
                                    comm.Parameters["@" + ColInfo[0] + ColInfo[1] + tempindex.ToString()].Value = Convert.ToDateTime(ColInfo[9]);
                                }
                                catch
                                {
                                    str = ColInfo[9];
                                    comm.Parameters["@" + ColInfo[0] + ColInfo[1] + tempindex.ToString()].Value = DBNull.Value;
                                }
                            }
                            else
                            {
                                comm.Parameters["@" + ColInfo[0] + ColInfo[1] + tempindex.ToString()].Value = DBNull.Value;
                            }
                            break;
                        case "float":
                            comm.Parameters.AddWithValue("@" + ColInfo[0] + ColInfo[1] + tempindex.ToString(), SqlDbType.Float);
                            str = ColInfo[9];
                            comm.Parameters["@" + ColInfo[0] + ColInfo[1] + tempindex.ToString()].Value = float.Parse(ColInfo[9]);
                            break;
                        case "char":
                            comm.Parameters.AddWithValue("@" + ColInfo[0] + ColInfo[1] + tempindex.ToString(), SqlDbType.Char);
                            comm.Parameters["@" + ColInfo[0] + ColInfo[1] + tempindex.ToString()].Value = char.Parse(ColInfo[9]);
                            break;
                        case "decimal":
                            str = ColInfo[9];
                            comm.Parameters.AddWithValue("@" + ColInfo[0] + ColInfo[1] + tempindex.ToString(), SqlDbType.Decimal);
                            if (ColInfo[9] == "")
                            {
                                comm.Parameters["@" + ColInfo[0] + ColInfo[1] + tempindex.ToString()].Value = 0.0;
                            }
                            else
                            {
                                comm.Parameters["@" + ColInfo[0] + ColInfo[1] + tempindex.ToString()].Value = decimal.Parse(ColInfo[9]);
                            }
                            break;
                        case "text":
                            str = ColInfo[9];
                            comm.Parameters.AddWithValue("@" + ColInfo[0] + ColInfo[1] + tempindex.ToString(), SqlDbType.Text);
                            comm.Parameters["@" + ColInfo[0] + ColInfo[1] + tempindex.ToString()].Value = ColInfo[9];
                            break;
                        case "numeric":
                            str = ColInfo[9];
                            comm.Parameters.AddWithValue("@" + ColInfo[0] + ColInfo[1] + tempindex.ToString(), SqlDbType.Decimal);
                            comm.Parameters["@" + ColInfo[0] + ColInfo[1] + tempindex.ToString()].Value = decimal.Parse(ColInfo[9]);
                            break;
                    }
                    #endregion

                }
                col += "pid";
                val += "@pid" + tempindex;
                comm.Parameters.AddWithValue("@pid" + tempindex, SqlDbType.Int);
                comm.Parameters["@pid" + tempindex].Value = pid;
                if (col.Length > 0)
                {
                    string coltemp = string.Empty;
                    string[] arr = col.Split(',');
                    for (int i = 0; i < arr.Length; i++)
                    {
                        coltemp += ",[" + arr[i]+"]";
                    }
                    col = coltemp.TrimStart(',');
                }
                SQLStr += "(" + col + ")  values  ( " + val + ")  ";

            }
            comm.CommandText = SQLStr;
            try
            {
                DBHelper.SqlHelper.ExecuteTransWithCommand(comm);
            }
            catch { 
                
            }
            return true;
        }

        #region 获取明细表名以及行数
        public static string GetTableRows(string tableid)
        {
            string sqlstr = "select * from defdba.CustomTable where ParentId=@tableid";
            SqlParameter[] parameters = { new SqlParameter("@tableid", SqlDbType.VarChar, 50) };
            parameters[0].Value = tableid;
            DataSet ds = SqlHelper.ExecuteSqlX(sqlstr, parameters);
            string code = string.Empty;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                code = code + "," + ds.Tables[0].Rows[i]["CustomTableName"].ToString() + "#" + ds.Tables[0].Rows[i]["ColumnNumber"].ToString();
            }
            code = code.TrimStart(',');
            return code;
        }
        #endregion

        #region 获取列表头
        public static DataSet GetTableHead(string tableid)
        {
            string sqlstr = "select * from defdba.StructTable where isshow=1 and isheadshow=1 and TableID=@tableid order by seq";
            SqlParameter[] parameters = { new SqlParameter("@tableid", SqlDbType.VarChar, 50) };
            parameters[0].Value = tableid;
            try
            {
                return SqlHelper.ExecuteSqlX(sqlstr, parameters);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 获取检索字段
        public static DataSet GetTableSearch(string tableid)
        {
            string sqlstr = "select * from defdba.StructTable where isshow=1 and isSearch=1 and TableID=@tableid order by seq";
            SqlParameter[] parameters = { new SqlParameter("@tableid", SqlDbType.VarChar, 50) };
            parameters[0].Value = tableid;
            try
            {
                return SqlHelper.ExecuteSqlX(sqlstr, parameters);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 获取明细表名以及行数
        public static string GetSubTableByParentID(string tableid)
        {
            string sqlstr = "select * from defdba.CustomTable where parentid=@tableid";
            SqlParameter[] parameters = { new SqlParameter("@tableid", SqlDbType.VarChar, 50) };
            parameters[0].Value = tableid;
            DataSet ds = SqlHelper.ExecuteSqlX(sqlstr, parameters);
            string totalstr = string.Empty;
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        sqlstr = "select '" + ds.Tables[0].Rows[i]["totalFlag"].ToString() + "' totalFlag ,'" + ds.Tables[0].Rows[i]["CustomTableName"].ToString() + "'CustomTableName,'" + ds.Tables[0].Rows[i]["AliasTableName"].ToString() + "' as AliasTableName,* from defdba.StructTable where tableid=" + ds.Tables[0].Rows[i][0].ToString();
                        DataSet subds = SqlHelper.ExecuteSqlX(sqlstr);
                        if (subds != null && subds.Tables.Count > 0)
                        {
                            if (subds.Tables[0].Rows.Count > 0)
                            {
                                string code = string.Empty;
                                for (int ii = 0; ii < subds.Tables[0].Rows.Count; ii++)
                                {
                                    code = code + "," + subds.Tables[0].Rows[ii]["CustomTableName"].ToString() + "#" + subds.Tables[0].Rows[ii]["AliasTableName"].ToString() + "#" + subds.Tables[0].Rows[ii]["ccode"].ToString() + "#" + subds.Tables[0].Rows[ii]["cname"].ToString() + "#" + subds.Tables[0].Rows[ii]["type"].ToString() + "#" + subds.Tables[0].Rows[ii]["isempty"].ToString()+"#"+subds.Tables[0].Rows[ii]["totalFlag"].ToString();
                                }
                                code = code.TrimStart(',');
                                totalstr = totalstr + "@" + code;
                            }
                        }
                    }
                }
            }
            totalstr = totalstr.TrimStart('@');
            return totalstr;
        }
        #endregion

        public static int DelTableList(string tableid,string Itemlist)
        {
            SqlParameter [] param = {
                                        new SqlParameter("@tableid",SqlDbType.VarChar,50)
                                    };
            param[0].Value = tableid;
            DataSet ds = SqlHelper.ExecuteSqlX("select * from defdba.CustomTable where ID=@tableid ; select * from defdba.CustomTable where ParentId=@tableid", param);
            //取子表名
            string detailsTableName = string.Empty;//子表集合变量

            if (ds != null && ds.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    detailsTableName += "," + "define." + ds.Tables[0].Rows[0]["CompanyCD"].ToString() + "_" + ds.Tables[1].Rows[i]["CustomTableName"].ToString();
                }
                detailsTableName = detailsTableName.TrimStart(',');
            }

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                //取表名
                string tablename = "define." + ds.Tables[0].Rows[0]["CompanyCD"].ToString() + "_" + ds.Tables[0].Rows[0]["CustomTableName"].ToString();
                //取字表名

                string[] arr = Itemlist.Split(',');
                string sqlstr = string.Empty;
                for (int i = 0; i < arr.Length; i++)
                {
                    sqlstr += ";" + " delete from " + tablename + " where ID =" + arr[i];
                }

                string[] subarr = detailsTableName.Split(',');//子表数组
                for (int i = 0; i < subarr.Length; i++)
                {
                    for(int ii=0;ii<arr.Length;ii++)
                    {
                        sqlstr += ";" + "delete from " + subarr[i] + " where pid=" + arr[ii]; 
                    }
                }

                sqlstr = sqlstr.TrimStart(';');

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
            else
            {
                return 0;
            }
        }

        public static DataTable GetDataTableList(string tableid)
        {
            SqlParameter[] param = {
                                        new SqlParameter("@tableid",SqlDbType.VarChar,50)
                                    };
            param[0].Value = tableid;
            DataSet ds = SqlHelper.ExecuteSqlX("select * from defdba.CustomTable where ID=@tableid", param);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                //取表名
                string tablename = "define." + ds.Tables[0].Rows[0]["CompanyCD"].ToString() + "_" + ds.Tables[0].Rows[0]["CustomTableName"].ToString();
                string sqlstr = "select * from " + tablename;
                try
                {
                    DataTable dt = SqlHelper.ExecuteSql(sqlstr);
                    return dt;
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}

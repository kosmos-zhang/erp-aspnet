using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using XBase.Model.Office.DefManager;

namespace XBase.Data.Office.DefManager
{
    public class CreateTableViewModelDBHelper
    {
        #region 根据公司编码获取自定义格式表(启用状态下的)
        /// <summary>
        /// 根据公司编码获取自定义格式表(启用状态下的)
        /// </summary>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns></returns>
        public static DataTable GetDefTableNameList(string strCompanyCD) 
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select ID,CustomTableName,AliasTableName ");
            strSql.AppendLine(" from defdba.CustomTable ");
            strSql.AppendLine(" where CompanyCD=@CompanyCD ");
            SqlParameter[] param = { 
                                    new SqlParameter("@CompanyCD",strCompanyCD)
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        #region 根据表ID获取表字段列表
        /// <summary>
        /// 根据表ID获取表字段列表
        /// </summary>
        /// <param name="tbID">表ID</param>
        /// <returns></returns>
        public static DataTable GetTableFieldsList(string tbID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select ID,ccode,cname,cname+'('+ccode+')' as cnameCn ");
            strSql.AppendLine(" from defdba.StructTable ");
            strSql.AppendLine(" where TableID=@TableID ");
            SqlParameter[] param = { 
                                    new SqlParameter("@TableID",tbID)
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        #region 保存模板
        /// <summary>
        /// 保存模板
        /// </summary>
        /// <param name="tbModel">ModuleTableModel模板实体</param>
        /// <param name="strMsg"></param>
        public static int SaveTableModel(ModuleTableModel tbModel, out string strMsg)
        {
            StringBuilder strSql = new StringBuilder();
            strMsg = "";
            int tbID = 0;
            if (!IsRepeatedModule(tbModel.TableID, "0"))
            {
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    strSql.AppendLine(" insert into defdba.ModuleTable (CompanyCD,ModuleContent,TableID,ModuleType,UseStatus)");
                    strSql.AppendLine(" values(@CompanyCD,@ModuleContent,@TableID,@ModuleType,@UseStatus)");
                    strSql.AppendLine(" ;select @@IDENTITY ");
                    SqlParameter[] param = { 
                                    new SqlParameter("@CompanyCD",tbModel.CompanyCD ),
                                    new SqlParameter("@ModuleContent",tbModel.ModuleContent ),
                                    new SqlParameter("@TableID",tbModel.TableID ),
                                    new SqlParameter("@ModuleType",tbModel.ModuleType ),
                                    new SqlParameter("@UseStatus",tbModel.UseStatus )
                                   };
                    foreach (SqlParameter para in param)
                    {
                        if (para.Value == null)
                        {
                            para.Value = DBNull.Value;
                        }
                    }
                    tbID = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql.ToString(), param));
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
                strMsg = "保存失败，该表的模板已定义！";
            }
            
            return tbID;
        }
        #endregion

        #region 修改模板
        /// <summary>
        ///  修改模板
        /// </summary>
        /// <param name="tbModel">ModuleTableModel模板实体</param>
        /// <param name="strMsg"></param>
        public static bool UpdateTableModel(ModuleTableModel tbModel, out string strMsg)
        {
            StringBuilder strSql = new StringBuilder();
            strMsg = "";
            bool isSuc = false;
            if (!IsRepeatedModule(tbModel.TableID, tbModel.ID.ToString()))
            {
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    strSql.AppendLine(" update defdba.ModuleTable set CompanyCD=@CompanyCD,ModuleContent=@ModuleContent,TableID=@TableID,");
                    strSql.AppendLine(" ModuleType=@ModuleType,UseStatus=@UseStatus ");
                    strSql.AppendLine(" where ID=@ID ");
                    SqlParameter[] param = { 
                                        new SqlParameter("@ID",tbModel.ID ),
                                        new SqlParameter("@CompanyCD",tbModel.CompanyCD ),
                                        new SqlParameter("@ModuleContent",tbModel.ModuleContent ),
                                        new SqlParameter("@TableID",tbModel.TableID ),
                                        new SqlParameter("@ModuleType",tbModel.ModuleType ),
                                        new SqlParameter("@UseStatus",tbModel.UseStatus )
                                       };
                    SqlHelper.ExecuteScalar(strSql.ToString(), param);
                    tran.Commit();
                    strMsg = "修改成功！";
                    isSuc = true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    strMsg = "修改失败，请联系系统管理员！";
                    isSuc = false;
                    throw ex;
                }
            }
            else
            {
                strMsg = "修改失败，该表的模板已定义！";
                isSuc = false;
            }
            
            return isSuc;
            
        }
        #endregion

        #region 根据条件获取模板列表
        /// <summary>
        /// 根据条件获取模板列表
        /// </summary>
        /// <param name="model">ModuleTableModel实体</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetTableViewModelList(ModuleTableModel model, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select m.ID,m.TableID,m.ModuleType,m.UseStatus,");
            strSql.AppendLine(" case m.ModuleType when 0 then '添加模板' end as ModuleTypeText,");
            strSql.AppendLine(" case m.UseStatus when 0 then '停用' when 1 then '启用' end as UseStatusText, ");
            strSql.AppendLine(" c.AliasTableName as TableName ");
            strSql.AppendLine(" from defdba.ModuleTable m ");
            strSql.AppendLine(" left join defdba.customtable c on m.TableID=c.ID ");
            strSql.AppendLine(" where m.CompanyCD=@CompanyCD ");
            
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", model.CompanyCD));

            if (!string.IsNullOrEmpty(model.TableID))
            {
                strSql.AppendLine(" and m.TableID=@ID ");
                arr.Add(new SqlParameter("@ID", model.TableID));
            }
            if (model.ModuleType != null)
            {
                strSql.AppendLine(" and m.ModuleType=@ModuleType ");
                arr.Add(new SqlParameter("@ModuleType", model.ModuleType));
            }
            if (model.UseStatus != null)
            {
                strSql.AppendLine(" and m.UseStatus=@UseStatus ");
                arr.Add(new SqlParameter("@UseStatus", model.UseStatus));
            }
            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref totalCount);
        }
        #endregion

        #region 根据模板ID获取模板信息
        /// <summary>
        /// 根据模板ID获取模板信息
        /// </summary>
        /// <param name="tbModuleID">模板ID</param>
        /// <returns>datata模板信息</returns>
        public static DataTable GetTBModuleInfo(string tbModuleID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select m.ID,m.ModuleContent,m.TableID,m.ModuleType,m.UseStatus,");
            strSql.AppendLine(" case m.ModuleType when 0 then '添加模板' end as ModuleTypeText,");
            strSql.AppendLine(" case m.UseStatus when 0 then '停用' when 1 then '启用' end as UseStatusText, ");
            strSql.AppendLine(" c.AliasTableName as TableName ");
            strSql.AppendLine(" from defdba.ModuleTable m ");
            strSql.AppendLine(" left join defdba.customtable c on m.TableID=c.ID ");
            strSql.AppendLine(" where m.ID=@ID ");
            SqlParameter[] param = { 
                                    new SqlParameter("@ID",tbModuleID)
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        #region 根据公司和表ID获取其子表列表(启用状态下的)
        /// <summary>
        /// 根据公司和表ID获取其子表列表(启用状态下的)
        /// </summary>
        /// <param name="tbID">表ID</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns></returns>
        public static DataTable GetSubTableNameList(string tbID,string strCompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select ID,CustomTableName,AliasTableName ");
            strSql.AppendLine(" from defdba.CustomTable ");
            strSql.AppendLine(" where CompanyCD=@CompanyCD and ParentId=@ParentID ");
            SqlParameter[] param = { 
                                    new SqlParameter("@CompanyCD",strCompanyCD),
                                    new SqlParameter("@ParentID",tbID )
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        #region 判断该表对应的模板是否重复设置，若重复设置则不给予保存
        /// <summary>
        /// 判断该表对应的模板是否重复设置，若重复设置则不给予保存
        /// </summary>
        /// <param name="tbID">表ID</param>
        /// <param name="moduleID">模板ID</param>
        /// <returns>true重复，false不重复</returns>
        private static bool IsRepeatedModule(string tbID, string moduleID)
        {
            bool isRept = false;
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select count(1) from defdba.ModuleTable ");
            strSql.AppendLine(" where TableID=@TableID and ID !=@ID ");
            SqlParameter[] param = { 
                                    new SqlParameter("@TableID",tbID),
                                    new SqlParameter("@ID",moduleID)
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

        #region 获取可查看菜单人员
        /// <summary>
        /// 获取可查看菜单人员
        /// </summary>
        /// <param name="proptype">菜单列表中的指定串和表ID组成的字符串</param>
        /// <returns>可查看菜单人员ID串和名字串的datatable</returns>
        public static DataTable GetCanViewMenuUser(string proptype)
        {
            StringBuilder strSql = new StringBuilder();//[dbo].[getEmployeeNameString](exp.CanViewUser) as CanViewUserName
            strSql.AppendLine(" select cm.userdUserList as CanViewUserIDs,[dbo].[getEmployeeNameString](cm.userdUserList) as CanViewUserNames ");
            strSql.AppendLine(" from defdba.CustomModule cm  ");
            strSql.AppendLine(" where PropertyValue=@PropertyValue ");
            SqlParameter[] param = { 
                                    new SqlParameter("@PropertyValue",proptype)
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        public static string GetTableTotalFlag(string companyCD, string tablename)
        {
            string returnstr = string.Empty;
            string tdstr = "<tr><td style='background-color:#FFFFFF; width:40px; height=25' align='center'>小计</td>";
            string sqlstr = "select * from defdba.CustomTable where CompanyCD=@companyCD and CustomTableName=@tablename";
            SqlParameter[] param ={
                                    new SqlParameter("@companyCD",companyCD),
                                    new SqlParameter("@tablename",tablename)
                                  };
            DataSet ds = SqlHelper.ExecuteSqlX(sqlstr,param);
            try
            {
                int num = Convert.ToInt32(ds.Tables[0].Rows[0]["totalFlag"].ToString());
                if (num > 0)
                {
                    //获取明细字段
                    sqlstr = "select * from defdba.StructTable where tableid="+ds.Tables[0].Rows[0][0].ToString();
                    DataSet detailds = SqlHelper.ExecuteSqlX(sqlstr);
                    for (int i = 0; i < detailds.Tables[0].Rows.Count; i++)
                    {
                        tdstr = tdstr + "<td style='background-color:#FFFFFF;'><input type='text' class='tdinput' autotab='true' style='width:90%;'" + " id='db_" + tablename + "_" + detailds.Tables[0].Rows[i]["ccode"] + "_total' />";
                    }
                    tdstr = tdstr + "</tr>";
                }
                returnstr = num.ToString() + "," + tdstr;
            }
            catch { returnstr = "0,0"; }
            return returnstr;
        }

        #region 删除模板
        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="tbIDStr">要删除的模板ID串</param>
        /// <returns></returns>
        public static bool DelTableModule(string tbIDStr, out string strMsg, out string strFieldText)
        {
            bool isSucc = false;
            string allOrderNo = "";
            strMsg = "";
            strFieldText = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            TransactionManager tran = new TransactionManager();
            string prot = "Pages/Office/DefManager/DefTableList.aspx?tableid=";//指定菜单表中PropertyValue字段前面指定值
            string[] orderNoS = null;
            orderNoS = tbIDStr.Split(',');
            for (int i = 0; i < orderNoS.Length; i++)
            {
                orderNoS[i] = "'" + prot + orderNoS[i] + "'";
                sb.Append(orderNoS[i]);
            }
            allOrderNo = sb.ToString().Replace("''", "','");

            tran.BeginTransaction();
            try
            {
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "delete from defdba.ModuleTable where tableID in (" + tbIDStr + ")", null);
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "delete from defdba.CustomModule where PropertyValue in (" + allOrderNo + ")", null);

                tran.Commit();
                isSucc = true;
                strMsg = "删除成功！";

            }
            catch (Exception ex)
            {
                tran.Rollback();
                strMsg = "删除失败，请联系系统管理员！";
                isSucc = false;
                throw ex;
            }
            
            return isSucc;
        }
        #endregion

    }
}

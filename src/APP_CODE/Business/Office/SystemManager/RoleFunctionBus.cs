
/***********************************************************************
 * 
 * Module Name:XBase.Business..Office.SystemManager.RoleFunctionBus.cs
 * Current Version: 1.0 
 * Creator: jiangym
 * Auditor:2009-01-10
 * End Date:
 * Description: 角色授权功能处理
 * Version History:
 ***********************************************************************/
using XBase.Model.Office.SystemManager;
using XBase.Data.Office.SystemManager;
using System.Data;
using System.Data.SqlClient;
using XBase.Common;
using XBase.Data.DBHelper;
using System;
using System.Web.UI;
using System.Linq;
using System.Web;
using XBase.Business.SystemManager;
using System.Collections;
namespace XBase.Business.Office.SystemManager
{
    public class RoleFunctionBus
    {
        /// <summary>
        /// 批量增加角色菜单功能
        /// </summary>
        public static bool AddRoleFun(string RoleID, string ModuleID, string FunctionID,string  FunctionType)
        {
            if (string.IsNullOrEmpty(RoleID) && string.IsNullOrEmpty(ModuleID)
                && string.IsNullOrEmpty(FunctionID)) return false;
            string[] sql = null;
            try
            {
                string str1=null;
                string str2 = null;
                int sql_index = 0;
                string[] funId = FunctionID.Split(',');
                string[] roleid = RoleID.Split(',');
                string[] moduleId = ModuleID.Split(',');
                ArrayList lstDelete = new ArrayList();
               sql = new string[funId.Length];
                if (roleid.Length >= 1)
                {
                    sql = new string[funId.Length * roleid.Length];
                }
                for (int index_role = 0; index_role < roleid.Length; index_role++)
                {
                    for (int index_moduleId = 0; index_moduleId < moduleId.Length; index_moduleId++)
                    {
                            if (ModuleFunBus.IsExistByModuleId(moduleId[index_moduleId].ToString()))
                            {
                                DataTable dt = ModuleFunBus.GetModuleFunInfo(moduleId[index_moduleId].ToString(),FunctionType);
                                foreach (DataRow Row in dt.Rows)
                                {
                                    for (int i = 0; i < funId.Length; i++)
                                    {
                                        str1 = funId[i].Split('|')[0].ToString();
                                        str2 = funId[i].Split('|')[1].ToString();
                                        if (str1 == Row["FunctionID"].ToString() && (str2 == Row["ID"].ToString()))
                                        {
                                            RoleFunModel Model = new RoleFunModel();
                                            Model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//待修改
                                            Model.FunctionID = Convert.ToInt32(Row["FunctionID"]);
                                            Model.RoleID = Convert.ToInt32(roleid[index_role]);
                                            Model.ModuleID = Convert.ToInt32(moduleId[index_moduleId]);
                                            DelRoleFun(Model);
                                            System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                                            cmdsql.AppendLine(" Insert into  officedba.RoleFunction ");
                                            cmdsql.AppendLine(" (CompanyCD,RoleID,ModuleID,FunctionID ");
                                            cmdsql.AppendLine("  ,ModifiedDate,ModifiedUserID) ");
                                            cmdsql.AppendLine(" Values(@CompanyCD ");
                                            cmdsql.AppendLine("  , @roleid,@moduleId,@FunctionID");
                                            cmdsql.AppendLine(" ,getdate(),");
                                            cmdsql.AppendLine(" '" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID + "' ) ");
                                            sql[sql_index] = cmdsql.ToString();
                                            sql_index++;
                                            SqlCommand sqlcomm = new SqlCommand();
                                            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", Model.CompanyCD));
                                            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@roleid", roleid[index_role].ToString()));
                                            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@moduleId", moduleId[index_moduleId].ToString()));
                                            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@FunctionID", Row["FunctionID"].ToString()));
                                            sqlcomm.CommandText = cmdsql.ToString();
                                            lstDelete.Add(sqlcomm);
                                            if (Row["FunctionID"] != null)
                                            {
                                                SqlCommand cmd = new SqlCommand();
                                                cmd.CommandText = "delete from officedba.RoleFunction where ModuleID=@ModuleID and FunctionID is null and RoleID=@RoleID";
                                                cmd.Parameters.Add(SqlHelper.GetParameter("@ModuleID", moduleId[index_moduleId].ToString()));
                                                cmd.Parameters.Add(SqlHelper.GetParameter("@RoleID", roleid[index_role].ToString()));
                                                lstDelete.Add(cmd);
                                            }
                                        }

                                    }
                                }
 
                        }

                    }
                }
                return SqlHelper.ExecuteTransWithArrayList(lstDelete);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }



       /// <summary>
       /// 批量增加菜单
       /// </summary>
       /// <param name="RoleID"></param>
       /// <param name="ModuleID"></param>
       /// <param name="FunctionID"></param>
       /// <returns></returns>
        public static bool AddRoleFunInfo(string RoleID, string ModuleID)
        {
            if (string.IsNullOrEmpty(RoleID) && string.IsNullOrEmpty(ModuleID)) return false;
            string[] sql = null;
            try
            {
                ArrayList lstDelete = new ArrayList();
                int sql_index = 0;
                string[] roleid = RoleID.Split(',');
                string[] moduleId = ModuleID.Split(',');
                sql = new string[moduleId.Length];
                if (roleid.Length >= 1)
                {
                    sql = new string[moduleId.Length * roleid.Length];
                }
                for (int index_role = 0; index_role < roleid.Length; index_role++)
                {
                    for (int index_moduleId = 0; index_moduleId < moduleId.Length; index_moduleId++)
                    {
                        if (!string.IsNullOrEmpty(moduleId[index_moduleId]))
                        {
                            RoleFunModel Model = new RoleFunModel();
                            Model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//待修改
                            Model.RoleID = Convert.ToInt32(roleid[index_role]);
                            Model.ModuleID = Convert.ToInt32(moduleId[index_moduleId]);
                            DelRoleFunInfo(Model);
                            System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                            cmdsql.AppendLine(" Insert into  officedba.RoleFunction ");
                            cmdsql.AppendLine(" (CompanyCD,RoleID,ModuleID, ");
                            cmdsql.AppendLine("  ModifiedDate,ModifiedUserID) ");
                            cmdsql.AppendLine(" Values(@CompanyCD ");
                            cmdsql.AppendLine("  , @roleid,@moduleId");
                            cmdsql.AppendLine(" ,getdate(),");
                            cmdsql.AppendLine(" '" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID + "' ) ");
                            sql[sql_index] = cmdsql.ToString();
                            sql_index++;
                            SqlCommand sqlcomm = new SqlCommand();
                            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", Model.CompanyCD));
                            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@roleid", roleid[index_role].ToString()));
                            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@moduleId", moduleId[index_moduleId].ToString()));
                            sqlcomm.CommandText = cmdsql.ToString();
                            lstDelete.Add(sqlcomm);
                        }
                    }
                }
                return SqlHelper.ExecuteTransWithArrayList(lstDelete);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据企业编码和角色编码获取角色信息
        /// </summary>
        /// <param name="CompanyCD">企业编码</param>
        /// <param name="RoleID">角色编码</param>
        /// <returns>DataTable</returns>
        public static DataTable GetRoleFunInfo(string CompanyCD, string RoleID)
        {
            if (string.IsNullOrEmpty(CompanyCD) || string.IsNullOrEmpty(RoleID)) return null;
            try
            {
                return RoleFunctionDBHelper.GetRoleFunInfo(CompanyCD, RoleID);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 根据企业编码和角色编码获取角色信息（包括操作名称和类型）
        /// </summary>
        /// <param name="CompanyCD">企业编码</param>
        /// <param name="RoleID">角色编码</param>
        /// <returns>DataTable</returns>
        public static DataTable GetRoleFunInfo2(string CompanyCD, string RoleID)
        {
            return XBase.Data.Office.SystemManager.RoleFunctionDBHelper.GetRoleFunInfo2(CompanyCD, RoleID);
        }


        /// <summary>
        /// 删除角色菜单功能
        /// </summary>
        public static int DelRoleFun(RoleFunModel Model)
        {
            if (Model == null) return 0;
            try
            {
                return RoleFunctionDBHelper.DelRoleFun(Model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public static int DelRoleFunInfo(RoleFunModel Model)
        {
            if (Model == null) return 0;
            try
            {
                return RoleFunctionDBHelper.DelRoleFunInfo(Model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 删除功能角色
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="RoleID"></param>
        /// <param name="ModuleID"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static bool DelRoleFun(string CompanyCD, string RoleID)
        {
            if (string.IsNullOrEmpty(CompanyCD) || string.IsNullOrEmpty(RoleID))
                return false ;
            try
            {
                return RoleFunctionDBHelper.DelRoleFun(CompanyCD, RoleID);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据角色编码获取权限分配信息
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public static DataTable GetRoleFunction(int RoleId,int ID)
        {
            if (RoleId==0) return null;
            try
            {
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                string companyCD = userInfo.CompanyCD;
                return RoleFunctionDBHelper.GetRoleFunction(RoleId,companyCD, ID);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// SetFunctionBatch
        /// </summary>
        /// <param name="companyCD"></param>
        /// <param name="roleID"></param>
        /// <param name="funcList1"></param>
        /// <param name="funcList2"></param>
        /// <param name="funcList3"></param>
        /// <param name="funcList4"></param>
        public static void SetFunctionBatch(string companyCD, int roleID, string funcList1, string funcList2, string funcList3, string funcList4)
        {
             RoleFunctionDBHelper.SetFunctionBatch(companyCD, roleID, funcList1, funcList2, funcList3, funcList4);
        }
    }
}

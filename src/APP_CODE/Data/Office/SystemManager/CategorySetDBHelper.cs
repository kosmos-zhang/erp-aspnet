using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Common;
using System.Data;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using XBase.Model.Office.SystemManager;
namespace XBase.Data.Office.SystemManager
{
    public class CategorySetDBHelper
    {
        #region 设备分类
        //<summary>
        //获取设备信息
        //</summary>
        //<param name="FlowNo"></param>
        //<param name="CompanyCD"></param>
        //<returns></returns>
        public static DataTable GetCodeEquipmentType(string ComPanyCD, string flag)
        {
            string sql = "select ID,CodeName,SupperID,TypeFlag,Description,WarningLimit,UsedStatus from officedba.CodeEquipmentType where CompanyCD=@CompanyCD and TypeFlag=@TypeFlag";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ComPanyCD);
            if (string.IsNullOrEmpty(flag))
            { param[1] = SqlHelper.GetParameter("@TypeFlag", ConstUtil.CodeEquipmentType_Equipment_Flag); }
            else if (flag == "1")
            { param[1] = SqlHelper.GetParameter("@TypeFlag", ConstUtil.CodeEquipmentType_Office_Flag); }
            DataTable dt = SqlHelper.ExecuteSql(sql, param);
            return dt;
        }
        /// <summary>
        /// 插入设备分类信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int InsertCodeEquipmentInfo(CodeEquipmentTypeModel model)
        {
            //SQL拼写
            SqlParameter[] param = new SqlParameter[10];
            param[0] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
            param[1] = SqlHelper.GetParameter("@TypeFlag", model.TypeFlag);
            param[2] = SqlHelper.GetParameter("@CodeName", model.CodeName);
            param[3] = SqlHelper.GetParameter("@SupperID", model.SupperID);
            param[4] = SqlHelper.GetParameter("@Description", model.Description);
            param[5] = SqlHelper.GetParameter("@WarningLimit", model.WarningLimit);
            param[6] = SqlHelper.GetParameter("@UsedStatus", model.UsedStatus);
            param[7] = SqlHelper.GetParameter("@ModifiedDate", model.ModifiedDate);
            param[8] = SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID);
            SqlParameter paramid = new SqlParameter("@RetID", SqlDbType.Int);
            paramid.Direction = ParameterDirection.Output;
            param[9] = paramid;
            //创建命令
            SqlCommand comm = new SqlCommand();
            SqlHelper.ExecuteTransStoredProcedure("officedba.CodeEquipmentInfo_insert", comm, param);
            int contantid = Convert.ToInt32(comm.Parameters["@RetID"].Value);

            return contantid;
        }

        /// <summary>
        /// 修改设备分类信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int UpdateCodeEquipmentInfo(CodeEquipmentTypeModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("uPDATE officedba.CodeEquipmentType");
            sql.AppendLine("  SET CompanyCD =@CompanyCD                      ");
            sql.AppendLine("     ,TypeFlag = @TypeFlag                      ");
            sql.AppendLine("     ,CodeName =@CodeName                      ");
            sql.AppendLine("     ,SupperID =@SupperID                      ");
            sql.AppendLine("     ,Description =@Description                   ");
            sql.AppendLine("     ,WarningLimit =@WarningLimit                  ");
            //sql.AppendLine("     ,Path = @Path                         ");
            sql.AppendLine("     ,UsedStatus = @UsedStatus                   ");
            sql.AppendLine("     ,ModifiedDate =@ModifiedDate                  ");
            sql.AppendLine("     ,ModifiedUserID =@ModifiedUserID                ");
            sql.AppendLine("WHERE  ID=@ID                               ");
            //设置参数
            SqlParameter[] param = new SqlParameter[10];
            param[0] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
            param[1] = SqlHelper.GetParameter("@TypeFlag", model.TypeFlag);
            param[2] = SqlHelper.GetParameter("@CodeName", model.CodeName);
            param[3] = SqlHelper.GetParameter("@SupperID", model.SupperID);
            param[4] = SqlHelper.GetParameter("@Description", model.Description);
            param[5] = SqlHelper.GetParameter("@WarningLimit", model.WarningLimit);
            param[6] = SqlHelper.GetParameter("@UsedStatus", model.UsedStatus);
            param[7] = SqlHelper.GetParameter("@ModifiedDate", model.ModifiedDate);
            param[8] = SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID);
            param[9] = SqlHelper.GetParameter("@ID", model.ID);
            return SqlHelper.ExecuteTransSql(sql.ToString(), param);


        }
        /// <summary>
        /// 根据ID获取设备信息
        /// </summary>
        /// <returns></returns>
        public static CodeEquipmentTypeModel GetCodeEuipment(int id)
        {
            CodeEquipmentTypeModel model = new CodeEquipmentTypeModel();
            string sql = "select * from officedba.CodeEquipmentType where id=@id ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@id", id);
            DataTable dt = SqlHelper.ExecuteSql(sql, param);

            DataRow data = dt.Rows[0];
            if (dt.Rows.Count > 0)
            {
                model.SupperID = GetSafeData.ValidateDataRow_Int(data, "SupperID");
                model.ModifiedDate = GetSafeData.ValidateDataRow_DateTime(data, "ModifiedDate");
                model.TypeFlag = GetSafeData.ValidateDataRow_String(data, "TypeFlag");
                model.UsedStatus = GetSafeData.ValidateDataRow_String(data, "UsedStatus");
                model.WarningLimit = GetSafeData.ValidateDataRow_Int(data, "WarningLimit");
                model.ModifiedUserID = GetSafeData.ValidateDataRow_String(data, "ModifiedUserID");
                model.CodeName = GetSafeData.ValidateDataRow_String(data, "CodeName");
                model.CompanyCD = GetSafeData.ValidateDataRow_String(data, "CompanyCD");
                model.Description = GetSafeData.ValidateDataRow_String(data, "Description");
                return model;
            }
            else
            {
                return null;
            }
        }

        public static int DeleteCodeEquipmentInfo(int id)
        {
            //SQL拼写
            string sql = "delete from officedba.CodeEquipmentType where ID=@ID";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@ID", id);
            return SqlHelper.ExecuteTransSql(sql, param);
        }
        #endregion

        #region 往来单位|物品种类
        public static DataTable GetCodeBigType(string ComPanyCD, string TableName)
        {
            string sql = "";
            if (TableName == "officedba.CodeCompanyType")
            {
                sql = "select ID,CodeName,SupperID,BigType as TypeFlag,Description,UsedStatus from " + TableName + " where CompanyCD=@CompanyCD ";
            }
            else if (TableName == "officedba.CodeProductType")
            {
                sql = "select ID,CodeName,SupperID,TypeFlag,Description,UsedStatus from " + TableName + " where CompanyCD=@CompanyCD ";
            }
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ComPanyCD);
            DataTable dt = SqlHelper.ExecuteSql(sql, param);
            return dt;
        }
        /// <summary>
        /// 获取物品信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetProductType(string CompanyCD)
        {
            string sql = "select ID,TypeFlag,CodeName,SupperID from officedba.CodeProductType where UsedStatus='1'and CompanyCD=@CompanyCD";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            DataTable dt = SqlHelper.ExecuteSql(sql, param);
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static string GetProductTypeInfo(int ID, string TypeFlag)
        {
            string nev = string.Empty;
            string sql = string.Format(" exec officedba.GetProductNextCD '{0}' ", ID);
            DataTable dt = SqlHelper.ExecuteSql(sql);
            DataView dataView = dt.DefaultView;
            dataView.RowFilter = "TypeFlag='" + TypeFlag + "'";
            DataTable dtnew = new DataTable();
            dtnew = dataView.ToTable();

            for (int i = 0; i < dtnew.Rows.Count; i++)
            {
                nev += dtnew.Rows[i]["ID"].ToString() + ",";
            }
            return nev.TrimEnd(new char[] { ',' });
        }
        /// <summary>
        /// 插入分类信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int InsertCodeBigTypeInfo(CodeEquipmentTypeModel model, string TabelName)
        {
            //SQL拼写
            SqlParameter[] param = new SqlParameter[9];
            param[0] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
            param[1] = SqlHelper.GetParameter("@CodeName", model.CodeName);
            param[2] = SqlHelper.GetParameter("@SupperID", model.SupperID);
            param[3] = SqlHelper.GetParameter("@Description", model.Description);
            param[4] = SqlHelper.GetParameter("@UsedStatus", model.UsedStatus);
            param[5] = SqlHelper.GetParameter("@ModifiedDate", model.ModifiedDate);
            param[6] = SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID);
            param[7] = SqlHelper.GetParameter("@TypeFlag", model.BigType);
            SqlParameter paramid = new SqlParameter("@RetID", SqlDbType.Int);
            paramid.Direction = ParameterDirection.Output;
            param[8] = paramid;
            //创建命令
            SqlCommand comm = new SqlCommand();
            if (TabelName == "officedba.CodeCompanyType")// 往来单位
                SqlHelper.ExecuteTransStoredProcedure("officedba.CodeCompanyType_insert", comm, param);
            else if (TabelName == "officedba.CodeProductType")
                SqlHelper.ExecuteTransStoredProcedure("officedba.CodeProductType_insert", comm, param);
            int contantid = Convert.ToInt32(comm.Parameters["@RetID"].Value);
            return contantid;
        }

        /// <summary>
        /// 修改分类信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int UpdateCodeBigTypeInfo(CodeEquipmentTypeModel model, string TableName)
        {
            StringBuilder sql = new StringBuilder();
            SqlParameter[] param = new SqlParameter[9];
            if (TableName == "officedba.CodeCompanyType")// 往来单位
            {
                sql.AppendLine("uPDATE  " + TableName + "");
                sql.AppendLine("  SET CompanyCD =@CompanyCD                      ");
                sql.AppendLine("     ,CodeName =@CodeName                      ");
                sql.AppendLine("     ,SupperID =@SupperID                      ");
                sql.AppendLine("     ,BigType =@TypeFlag                      ");
                sql.AppendLine("     ,Description =@Description                   ");
                sql.AppendLine("     ,UsedStatus = @UsedStatus                   ");
                sql.AppendLine("     ,ModifiedDate =@ModifiedDate                  ");
                sql.AppendLine("     ,ModifiedUserID =@ModifiedUserID                ");
                sql.AppendLine("WHERE  ID=@ID                               ");
                //设置参数
                param[0] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
                param[1] = SqlHelper.GetParameter("@CodeName", model.CodeName);
                param[2] = SqlHelper.GetParameter("@SupperID", model.SupperID);
                param[3] = SqlHelper.GetParameter("@TypeFlag", model.BigType);
                param[4] = SqlHelper.GetParameter("@Description", model.Description);
                param[5] = SqlHelper.GetParameter("@UsedStatus", model.UsedStatus);
                param[6] = SqlHelper.GetParameter("@ModifiedDate", model.ModifiedDate);
                param[7] = SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID);
                param[8] = SqlHelper.GetParameter("@ID", model.ID);
            }
            else if (TableName == "officedba.CodeProductType")
            {
                sql.AppendLine("uPDATE  " + TableName + "");
                sql.AppendLine("  SET CompanyCD =@CompanyCD                      ");
                sql.AppendLine("     ,CodeName =@CodeName                      ");
                sql.AppendLine("     ,SupperID =@SupperID                      ");
                sql.AppendLine("     ,TypeFlag =@TypeFlag                      ");
                sql.AppendLine("     ,Description =@Description                   ");
                sql.AppendLine("     ,UsedStatus = @UsedStatus                   ");
                sql.AppendLine("     ,ModifiedDate =@ModifiedDate                  ");
                sql.AppendLine("     ,ModifiedUserID =@ModifiedUserID                ");
                sql.AppendLine("WHERE  ID=@ID                               ");
                //设置参数
                param[0] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
                param[1] = SqlHelper.GetParameter("@CodeName", model.CodeName);
                param[2] = SqlHelper.GetParameter("@SupperID", model.SupperID);
                param[3] = SqlHelper.GetParameter("@TypeFlag", model.BigType);
                param[4] = SqlHelper.GetParameter("@Description", model.Description);
                param[5] = SqlHelper.GetParameter("@UsedStatus", model.UsedStatus);
                param[6] = SqlHelper.GetParameter("@ModifiedDate", model.ModifiedDate);
                param[7] = SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID);
                param[8] = SqlHelper.GetParameter("@ID", model.ID);
            }

            return SqlHelper.ExecuteTransSql(sql.ToString(), param);

        }
        ///// <summary>
        ///// 获取分类代码
        ///// </summary>
        ///// <param name="CompanyCD"></param>
        ///// <returns></returns>
        //public static DataTable GetCodeBigType(string CompanyCD,string TableName)
        //{
        //    string sql = "select ID,CodeName,SupperID,Description,UsedStatus,TypeFlag from "+TableName+" where CompanyCD=@CompanyCD ";
        //    SqlParameter[] param = new SqlParameter[1];
        //    param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
        //    DataTable dt = SqlHelper.ExecuteSql(sql, param);
        //    return dt;
        //}
        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteCodeBigType(int id, string TableName)
        {
            string sql = "delete from " + TableName + " where ID=@ID";
            string delsql = "";
            switch (TableName)
            {
                case "officedba.CodeProductType":     //物品分类
                    delsql = "select count (*) as Sum from officedba.ProductInfo where TypeID=@ID";
                    break;
                //case "officedba.CodeEquipmentType":   //办公用品、设备分类
                //    delsql = "select count (*) as Sum from officedba.ProductInfo where TypeID=@ID";
                //    break;
                default:
                    break;

            }

            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@ID", id);
            if (delsql.Length > 0)
            {
                bool succ = SqlHelper.Exists(delsql, param);
                if (succ)
                    return -1;
            }

            return SqlHelper.ExecuteTransSql(sql, param);
        }

        /// <summary>
        /// 根据ID获取分类信息
        /// </summary>
        /// <returns></returns>
        public static CodeEquipmentTypeModel GetCodeBigTypeById(int id, string TableName)
        {
            CodeEquipmentTypeModel model = new CodeEquipmentTypeModel();
            string sql = "select * from " + TableName + " where id=@id ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@id", id);
            DataTable dt = SqlHelper.ExecuteSql(sql, param);

            DataRow data = dt.Rows[0];
            if (dt.Rows.Count > 0)
            {
                model.SupperID = GetSafeData.ValidateDataRow_Int(data, "SupperID");
                model.ModifiedDate = GetSafeData.ValidateDataRow_DateTime(data, "ModifiedDate");
                model.UsedStatus = GetSafeData.ValidateDataRow_String(data, "UsedStatus");
                model.ModifiedUserID = GetSafeData.ValidateDataRow_String(data, "ModifiedUserID");
                model.CodeName = GetSafeData.ValidateDataRow_String(data, "CodeName");
                model.CompanyCD = GetSafeData.ValidateDataRow_String(data, "CompanyCD");
                model.Description = GetSafeData.ValidateDataRow_String(data, "Description");
                if (TableName == "officedba.CodeCompanyType")// 往来单位
                    model.BigType = GetSafeData.ValidateDataRow_String(data, "BigType");
                else if (TableName == "officedba.CodeProductType")
                    model.BigType = GetSafeData.ValidateDataRow_String(data, "TypeFlag");
                return model;
            }
            else
            {
                return null;
            }
        }
        #endregion

        /// <summary>
        /// 加载物品
        /// </summary>
        /// <param name="companyCD"></param>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public static DataTable SearchDeptInfo(string companyCD, string deptID)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                    ");
            searchSql.AppendLine(" 	 A.ID                    ");
            searchSql.AppendLine(" 	 ,A.TypeFlag                   ");
            searchSql.AppendLine(" 	,A.SupperID           ");
            searchSql.AppendLine(" 	,A.CodeName              ");
            searchSql.AppendLine(" 	,(SELECT COUNT(ID)       ");
            searchSql.AppendLine(" 		FROM                 ");
            searchSql.AppendLine(" 		officedba.CodeProductType B ");
            searchSql.AppendLine(" 		WHERE                ");
            searchSql.AppendLine(" 		B.SupperID = A.ID)");
            searchSql.AppendLine(" 	AS SubCount              ");
            searchSql.AppendLine(" FROM                      ");
            searchSql.AppendLine(" 	officedba.CodeProductType A     ");
            searchSql.AppendLine(" WHERE                     ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));

            //组织机构ID未输入时，查询
            if (string.IsNullOrEmpty(deptID))
            {
                searchSql.AppendLine(" AND (A.SupperID IS NULL or A.SupperID=0)");
            }
            //获取子组织机构
            else
            {
                searchSql.AppendLine(" AND A.SupperID = @SupperID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SupperID", deptID));
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        /// <summary>
        /// 加载往来单位
        /// </summary>
        /// <param name="companyCD"></param>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public static DataTable SearchCodeCompanyInfo(string companyCD, string deptID)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                    ");
            searchSql.AppendLine(" 	 A.ID                    ");
            searchSql.AppendLine(" 	 ,A.BigType as TypeFlag                  ");
            searchSql.AppendLine(" 	,A.SupperID           ");
            searchSql.AppendLine(" 	,A.CodeName              ");
            searchSql.AppendLine(" 	,(SELECT COUNT(ID)       ");
            searchSql.AppendLine(" 		FROM                 ");
            searchSql.AppendLine(" 		officedba.CodeCompanyType B ");
            searchSql.AppendLine(" 		WHERE                ");
            searchSql.AppendLine(" 		B.SupperID = A.ID)");
            searchSql.AppendLine(" 	AS SubCount              ");
            searchSql.AppendLine(" FROM                      ");
            searchSql.AppendLine(" 	officedba.CodeCompanyType A     ");
            searchSql.AppendLine(" WHERE                     ");
            searchSql.AppendLine(" 	A.CompanyCD = @CompanyCD ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));

            //组织机构ID未输入时，查询
            if (string.IsNullOrEmpty(deptID))
            {
                searchSql.AppendLine(" AND (A.SupperID IS NULL or A.SupperID=0)");
            }
            //获取子组织机构
            else
            {
                searchSql.AppendLine(" AND A.SupperID = @SupperID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SupperID", deptID));
            }

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }


        #region 文档分类
        /// <summary>
        /// 插入文档分类信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int InsertCodeDocTypeInfo(CodeEquipmentTypeModel model)
        {
            //SQL拼写
            SqlParameter[] param = new SqlParameter[8];
            param[0] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
            param[1] = SqlHelper.GetParameter("@CodeName", model.CodeName);
            param[2] = SqlHelper.GetParameter("@SupperID", model.SupperID);
            param[3] = SqlHelper.GetParameter("@Description", model.Description);
            param[4] = SqlHelper.GetParameter("@UsedStatus", model.UsedStatus);
            param[5] = SqlHelper.GetParameter("@ModifiedDate", model.ModifiedDate);
            param[6] = SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID);
            SqlParameter paramid = new SqlParameter("@RetID", SqlDbType.Int);
            paramid.Direction = ParameterDirection.Output;
            param[7] = paramid;
            //创建命令
            SqlCommand comm = new SqlCommand();
            SqlHelper.ExecuteTransStoredProcedure("officedba.CodeDocTypeInfo_insert", comm, param);
            int contantid = Convert.ToInt32(comm.Parameters["@RetID"].Value);

            return contantid;
        }

        /// <summary>
        /// 修改文档分类信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int UpdateCodeDocTypeInfo(CodeEquipmentTypeModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("uPDATE officedba.CodeDocType");
            sql.AppendLine("  SET CompanyCD =@CompanyCD                      ");
            sql.AppendLine("     ,CodeName =@CodeName                      ");
            sql.AppendLine("     ,SupperID =@SupperID                      ");
            sql.AppendLine("     ,Description =@Description                   ");
            sql.AppendLine("     ,UsedStatus = @UsedStatus                   ");
            sql.AppendLine("     ,ModifiedDate =@ModifiedDate                  ");
            sql.AppendLine("     ,ModifiedUserID =@ModifiedUserID                ");
            sql.AppendLine("WHERE  ID=@ID                               ");
            //设置参数
            SqlParameter[] param = new SqlParameter[8];
            param[0] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
            param[1] = SqlHelper.GetParameter("@CodeName", model.CodeName);
            param[2] = SqlHelper.GetParameter("@SupperID", model.SupperID);
            param[3] = SqlHelper.GetParameter("@Description", model.Description);
            param[4] = SqlHelper.GetParameter("@UsedStatus", model.UsedStatus);
            param[5] = SqlHelper.GetParameter("@ModifiedDate", model.ModifiedDate);
            param[6] = SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID);
            param[7] = SqlHelper.GetParameter("@ID", model.ID);
            return SqlHelper.ExecuteTransSql(sql.ToString(), param);


        }
        /// <summary>
        /// 获取文档分类代码
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetCodeDocType(string CompanyCD)
        {
            string sql = "select ID,CodeName,SupperID,Description,UsedStatus from officedba.CodeDocType where CompanyCD=@CompanyCD ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            DataTable dt = SqlHelper.ExecuteSql(sql, param);
            return dt;
        }

        #region 获取文档类型的方法 zhangyy
        /// <summary>
        /// 获取文档类型的方法
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <returns>返回所有类型及类型ID</returns>
        public static DataTable GetDocType(string CompanyCD)
        {
            string sql = "select ID,CodeName,SupperID from officedba.CodeDocType where CompanyCD=@CompanyCD and UsedStatus = 1 ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            DataTable dt = SqlHelper.ExecuteSql(sql, param);
            return dt;
        }
        #endregion

        /// <summary>
        /// 删除文档分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteCodeDocType(int id)
        {
            string sql = "delete from officedba.CodeDocType where ID=@ID";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@ID", id);
            return SqlHelper.ExecuteTransSql(sql, param);
        }

        /// <summary>
        /// 根据ID获取文档信息
        /// </summary>
        /// <returns></returns>
        public static CodeEquipmentTypeModel GetodeDocType(int id)
        {
            CodeEquipmentTypeModel model = new CodeEquipmentTypeModel();
            string sql = "select * from officedba.CodeDocType where id=@id ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@id", id);
            DataTable dt = SqlHelper.ExecuteSql(sql, param);

            DataRow data = dt.Rows[0];
            if (dt.Rows.Count > 0)
            {
                model.SupperID = GetSafeData.ValidateDataRow_Int(data, "SupperID");
                model.ModifiedDate = GetSafeData.ValidateDataRow_DateTime(data, "ModifiedDate");
                model.UsedStatus = GetSafeData.ValidateDataRow_String(data, "UsedStatus");
                model.ModifiedUserID = GetSafeData.ValidateDataRow_String(data, "ModifiedUserID");
                model.CodeName = GetSafeData.ValidateDataRow_String(data, "CodeName");
                model.CompanyCD = GetSafeData.ValidateDataRow_String(data, "CompanyCD");
                model.Description = GetSafeData.ValidateDataRow_String(data, "Description");
                return model;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }

}

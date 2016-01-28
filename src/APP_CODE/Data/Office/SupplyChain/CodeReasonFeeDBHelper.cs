using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Model.Office.SupplyChain;
using XBase.Data.DBHelper;
using XBase.Common;
using System.Collections;
namespace XBase.Data.Office.SupplyChain
{
    public class CodeReasonFeeDBHelper
    {
        #region 原因分类|费用分类|计量单位代码
        public static DataTable GetThreeCodeType(string ComPanyCD, string SubNo, string TableName, string UseStatus, string Name, string Flag)
        {
            string sql = "";
            switch (TableName)
            {
                case "officedba.CodeReasonType":
                    sql = "select c.ID,c.CodeName,isnull(c.Flag,'')Flag,isnull(Description,'')as Description,c.UsedStatus,isnull(c.ModifiedDate,'') as ModifiedDate ,isnull(c.ModifiedUserID,'')as ModifiedUserID,c.Flag as Publicflag from " + TableName + " as c where c.CompanyCD=@CompanyCD ";
                    break;
                case "officedba.CodeFeeType":
                    sql = "select c.Publicflag,c.ID,c.CodeName,isnull(c.FeeSubjectsNo,'')FeeSubjectsNo,isnull(c.SubjectsName,'')SubjectsName,isnull(c.Flag,'')Flag,c.Description,c.UsedStatus,c.ModifiedDate,c.ModifiedUserID,c.CompanyCD from( select a.CompanyCD, a.ID,a.CodeName,a.FeeSubjectsNo,x.SubjectsName,a.Flag as Publicflag,isnull(b.TypeName,'') as Flag , ";
                    sql += "isnull(a.Description,'')as Description,a.UsedStatus,a.ModifiedDate,a.ModifiedUserID  ";
                    sql += "from " + TableName + " as a left join officedba.CodePublicType as b on a.Flag=b.id ";
                    sql += " left outer join officedba.AccountSubjects x on a.FeeSubjectsNo=x.SubjectsCD and a.CompanyCD=x.CompanyCD) as c  where c.CompanyCD=@CompanyCD";
                    break;
                case "officedba.CodeUnitType":
                    sql = "select c.ID,c.CodeName,isnull(c.Flag,'')Flag,isnull(CodeSymbol,'')as CodeSymbol, isnull(Description,'')as Description,c.UsedStatus,isnull(c.ModifiedDate,'') as ModifiedDate ,isnull(c.ModifiedUserID,'')as ModifiedUserID,c.Flag as Publicflag from " + TableName + " as c where c.CompanyCD=@CompanyCD";
                    break;
            }
            SqlParameter[] param = null;

            if (TableName == "officedba.CodeFeeType") param = new SqlParameter[5];
            else param = new SqlParameter[4];
            if (!string.IsNullOrEmpty(UseStatus))
            {
                sql += "  and c.UsedStatus=@UsedStatus";
            }
            if (Flag != "0")
            {
                if (TableName == "officedba.CodeFeeType")
                {
                    sql += "  and c.Publicflag=@Publicflag";

                }
                else
                {
                    sql += "  and c.Flag=@Flag";

                }

            }
            if (TableName == "officedba.CodeFeeType")
            {
                if (!string.IsNullOrEmpty(SubNo))
                    sql += "  and c.FeeSubjectsNo=@FeeSubjectsNo";

            }
            if (!string.IsNullOrEmpty(Name))
            {
                sql += "  and c.CodeName LIKE @CodeName";
            }
            param[0] = SqlHelper.GetParameter("@CompanyCD", ComPanyCD);
            param[1] = SqlHelper.GetParameter("@UsedStatus", UseStatus);
            param[2] = SqlHelper.GetParameter("@Flag", Flag);
            if (TableName == "officedba.CodeFeeType")
            {
                if (Flag != "0")
                    param[2] = SqlHelper.GetParameter("@Publicflag", Flag);
            }
            param[3] = SqlHelper.GetParameter("@CodeName", "%" + Name + "%");
            if (TableName == "officedba.CodeFeeType")
            {
                param[4] = SqlHelper.GetParameter("@FeeSubjectsNo", SubNo);
            }
            DataTable dt = SqlHelper.ExecuteSql(sql, param);
            return dt;
        }
        /// <summary>
        /// 获取计量单位信息
        /// </summary>
        public static DataTable GetCodeUnitType(string CompanyCD)
        {
            string sql = "";
            sql = "select ID,CodeName from officedba.CodeUnitType where CompanyCD=@CompanyCD and UsedStatus ='1'";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            DataTable dt = SqlHelper.ExecuteSql(sql, param);
            return dt;
        }
        /// <summary>
        /// 插入原因分类|费用分类|计量单位代码信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InsertThreeCodeInfo(CodeReasonFeeModel model, string TabelName)
        {
            SqlParameter[] param = null;
            //SQL拼写
            string sql = "";

            if (TabelName == "officedba.CodeReasonType")
            {
                sql = "Insert into " + TabelName + "(CompanyCD,CodeName,Flag,Description,UsedStatus,ModifiedDate,ModifiedUserID)" +
                                   "values(@CompanyCD,@CodeName,@Flag,@Description,@UsedStatus,@ModifiedDate,@ModifiedUserID)";
                param = new SqlParameter[7];
            }
            if (TabelName == "officedba.CodeFeeType")
            {
                sql = "Insert into " + TabelName + "(CompanyCD,CodeName,FeeSubjectsNo,Flag,Description,UsedStatus,ModifiedDate,ModifiedUserID)" +
                                   "values(@CompanyCD,@CodeName,@FeeSubjectsNo,@Flag,@Description,@UsedStatus,@ModifiedDate,@ModifiedUserID)";
                param = new SqlParameter[8];
            }
            else if (TabelName == "officedba.CodeUnitType")
            {
                param = new SqlParameter[8];
                sql = "Insert into officedba.CodeUnitType(CompanyCD,CodeName,Flag,Description,UsedStatus,CodeSymbol,ModifiedDate,ModifiedUserID)" +
                                                 "values(@CompanyCD,@CodeName,@Flag,@Description,@UsedStatus,@CodeSymbol,@ModifiedDate,@ModifiedUserID)";
            }
            param[0] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
            param[1] = SqlHelper.GetParameter("@CodeName", model.CodeName);
            param[2] = SqlHelper.GetParameter("@Flag", model.Flag);
            param[3] = SqlHelper.GetParameter("@Description", model.Description);
            param[4] = SqlHelper.GetParameter("@UsedStatus", model.UsedStatus);
            param[5] = SqlHelper.GetParameter("@ModifiedDate", model.ModifiedDate);
            param[6] = SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID);
            if (TabelName == "officedba.CodeFeeType")
            {
                param[7] = SqlHelper.GetParameter("@FeeSubjectsNo", model.FeeSubjectsNo);
            }
            if (TabelName == "officedba.CodeUnitType")
            {
                param[7] = SqlHelper.GetParameter("@CodeSymbol", model.CodeSymbol);
            }
            //创建命令
            SqlHelper.ExecuteTransSql(sql.ToString(), param);
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }

        /// <summary>
        /// 修改原因分类|费用分类|计量单位代码信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateThreeCodeInfo(CodeReasonFeeModel model, string TableName)
        {
            StringBuilder sql = new StringBuilder();
            SqlParameter[] param = null;
            if (TableName == "officedba.CodeReasonType")// 往来单位
            {
                sql.AppendLine("uPDATE  " + TableName + "");
                sql.AppendLine("  SET CompanyCD =@CompanyCD                      ");
                sql.AppendLine("     ,CodeName =@CodeName                      ");
                sql.AppendLine("     ,Flag =@Flag                      ");
                sql.AppendLine("     ,Description =@Description                      ");
                sql.AppendLine("     ,UsedStatus =@UsedStatus                   ");
                sql.AppendLine("     ,ModifiedDate =@ModifiedDate                  ");
                sql.AppendLine("     ,ModifiedUserID =@ModifiedUserID                ");
                sql.AppendLine("WHERE  ID=@ID                               ");
                param = new SqlParameter[8];
                //设置参数

            }
            if (TableName == "officedba.CodeFeeType")// 往来单位
            {
                sql.AppendLine("uPDATE  " + TableName + "");
                sql.AppendLine("  SET CompanyCD =@CompanyCD                      ");
                sql.AppendLine("     ,CodeName =@CodeName                      ");
                sql.AppendLine("     ,FeeSubjectsNo =@FeeSubjectsNo                      ");
                sql.AppendLine("     ,Flag =@Flag                      ");
                sql.AppendLine("     ,Description =@Description                      ");
                sql.AppendLine("     ,UsedStatus =@UsedStatus                   ");
                sql.AppendLine("     ,ModifiedDate =@ModifiedDate                  ");
                sql.AppendLine("     ,ModifiedUserID =@ModifiedUserID                ");
                sql.AppendLine("WHERE  ID=@ID                               ");
                param = new SqlParameter[9];
                //设置参数

            }
            else if (TableName == "officedba.CodeUnitType")
            {
                sql.AppendLine("uPDATE  officedba.CodeUnitType");
                sql.AppendLine("  SET CompanyCD =@CompanyCD                      ");
                sql.AppendLine("     ,CodeName =@CodeName                      ");
                sql.AppendLine("     ,Flag =@Flag                      ");
                sql.AppendLine("     ,Description =@Description                      ");
                sql.AppendLine("     ,UsedStatus =@UsedStatus                   ");
                sql.AppendLine("     ,CodeSymbol =@CodeSymbol                   ");
                sql.AppendLine("     ,ModifiedDate =@ModifiedDate                  ");
                sql.AppendLine("     ,ModifiedUserID =@ModifiedUserID                ");
                sql.AppendLine("WHERE  ID=@ID                               ");
                param = new SqlParameter[9];
                //设置参数
            }
            param[0] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
            param[1] = SqlHelper.GetParameter("@CodeName", model.CodeName);
            param[2] = SqlHelper.GetParameter("@Flag", model.Flag);
            param[3] = SqlHelper.GetParameter("@Description", model.Description);
            param[4] = SqlHelper.GetParameter("@UsedStatus", model.UsedStatus);
            param[5] = SqlHelper.GetParameter("@ModifiedDate", model.ModifiedDate);
            param[6] = SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID);
            param[7] = SqlHelper.GetParameter("@ID", model.ID);
            if (TableName == "officedba.CodeUnitType")
            {
                param[8] = SqlHelper.GetParameter("@CodeSymbol", model.CodeSymbol);
            }
            if (TableName == "officedba.CodeFeeType")
            {
                param[8] = SqlHelper.GetParameter("@FeeSubjectsNo", model.FeeSubjectsNo);
            }
            SqlHelper.ExecuteTransSql(sql.ToString(), param);
            return SqlHelper.Result.OprateCount > 0 ? true : false;

        }
        /// <summary>
        /// 删除原因分类|费用分类|计量单位代码信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteThreeCodeType(string ID, string TableName)
        {
            string allID = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string[] Delsql = new string[1];
            string[] sql = null;
            try
            {
                string[] IdS = null;
                ID = ID.Substring(0, ID.Length);
                IdS = ID.Split(',');

                for (int i = 0; i < IdS.Length; i++)
                {
                    IdS[i] = "'" + IdS[i] + "'";
                    sb.Append(IdS[i]);
                }
                //allUserID = sb.ToString();
                allID = sb.ToString().Replace("''", "','");
                Delsql[0] = "delete from " + TableName + " where  ID IN (" + allID + ") ";
                switch (TableName)
                {
                    case "officedba.CodeFeeType":     //费用
                        sql = new string[6];
                        sql[0] = "select count (*) as Sum from officedba.FeeReturnDetail  where FeeNameID in  (" + allID + ")";
                        sql[1] = "select count (*) as Sum from Officedba.FeeApplyDetail where ExpType in  (" + allID + ")";
                        sql[2] = "select count (*) as Sum from Officedba.SellOrderFeeDetail where FeeID in  (" + allID + ")";
                        sql[3] = "select count (*) as Sum from officedba. SellOrderFeeDetail  where FeeID in  (" + allID + ")";
                        sql[4] = "select count (*) as Sum from Officedba.FeeReturnDetail where FeeNameID in  (" + allID + ")";
                        sql[5] = "select count (*) as Sum from Officedba.FeesDetail where FeeID in  (" + allID + ")";
                        break;
                    case "officedba.CodeReasonType":  //原因
                        sql = new string[0];
                        break;
                    case "officedba.CodeUnitType":   //计量单位
                        sql = new string[3];
                        sql[0] = "select count (*) as Sum from officedba.ProductInfo where UnitID in  (" + allID + ")";
                        sql[1] = "select count (*) as Sum from Officedba.UnitGroup where BaseUnitID in  (" + allID + ")";
                        sql[2] = "select count (*) as Sum from Officedba.UnitGroupDetail where UnitID in  (" + allID + ")";
                        break;
                }
                if (sql.Length > 0)
                {
                    for (int i = 0; i < sql.Length; i++)
                    {
                        bool succ = SqlHelper.Exists(sql[i].ToString(), null);
                        if (succ)
                            return false;
                    }
                }
                SqlCommand comm = new SqlCommand();
                comm.CommandText = Delsql[0].ToString();

                //设置参数
                comm.Parameters.Add(SqlHelper.GetParameter("@ID", ID));
                ArrayList lstDelete = new ArrayList();
                comm.CommandText = Delsql[0].ToString();
                //添加基本信息更新命令
                lstDelete.Add(comm);
                return SqlHelper.ExecuteTransWithArrayList(lstDelete);
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        /// <summary>
        /// 根据ID获取信息
        /// </summary>
        /// <returns></returns>
        public static CodeReasonFeeModel GetThreeCodeById(int id, string TableName)
        {
            CodeReasonFeeModel model = new CodeReasonFeeModel();
            string sql = "select * from " + TableName + " where id=@id ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@id", id);
            DataTable dt = SqlHelper.ExecuteSql(sql, param);

            DataRow data = dt.Rows[0];
            if (dt.Rows.Count > 0)
            {
                model.CodeName = GetSafeData.ValidateDataRow_String(data, "CodeName");
                model.ModifiedDate = GetSafeData.ValidateDataRow_DateTime(data, "ModifiedDate");
                model.UsedStatus = GetSafeData.ValidateDataRow_String(data, "UsedStatus");
                model.ModifiedUserID = GetSafeData.ValidateDataRow_String(data, "ModifiedUserID");
                model.Flag = GetSafeData.ValidateDataRow_Int(data, "Flag");
                model.CompanyCD = GetSafeData.ValidateDataRow_String(data, "CompanyCD");
                model.Description = GetSafeData.ValidateDataRow_String(data, "Description");
                if (TableName == "officedba.CodeUnitType")// 往来单位
                    model.CodeSymbol = GetSafeData.ValidateDataRow_String(data, "CodeSymbol");
                return model;
            }
            else
            {
                return null;
            }
        }
        #endregion
        /// <summary>
        /// 验证类别名称唯一性
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="codeValue"></param>
        /// <param name="companyCD"></param>
        /// <returns></returns>
        public static bool CheckCodeUniq(string tableName, string companyCD, string CodeName, string Flag)
        {
            //校验SQL定义
            string checkSql = " SELECT CodeName FROM " + tableName
                                    + " WHERE CompanyCD = @CompanyCD AND CodeName = @CodeName and Flag=@Flag";

            //设置参数
            SqlParameter[] param = new SqlParameter[3];
            int i = 0;
            //公司代码
            param[i++] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //编码类型
            param[i++] = SqlHelper.GetParameter("@CodeName", CodeName);
            param[i++] = SqlHelper.GetParameter("@Flag", Flag);

            //校验存在性
            DataTable data = SqlHelper.ExecuteSql(checkSql, param);
            //数据不存在时，返回true
            if (data == null || data.Rows.Count < 1)
            {
                return true;
            }
            //数据存在时，返回false
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断费用表是否已经使用这个费用分类了
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool ChargeFee(string ID)
        {
            string allID = "";
            string[] IdS = null;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            ID = ID.Substring(0, ID.Length);
            IdS = ID.Split(',');

            for (int i = 0; i < IdS.Length; i++)
            {
                IdS[i] = "'" + IdS[i] + "'";
                sb.Append(IdS[i]);
            }
            allID = sb.ToString().Replace("''", "','");
            object obj = SqlHelper.ExecuteScalar("select count(*) from officedba.codefeetype where flag in(" + allID + ")", null);
            return Convert.ToInt32(obj) > 0 ? true : false;
        }
    }
}

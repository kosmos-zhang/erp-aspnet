using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

using XBase.Data.DBHelper;
using XBase.Common;
using XBase.Model.Office.StorageManager;

namespace XBase.Data.Office.LogisticsDistributionManager
{
    public class SubProductSellPriceDBHelper
    {
        #region 读取分店列表
        public static DataTable GetSubStore(Model.Office.HumanManager.DeptModel model)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT * FROM officedba.DeptInfo WHERE ");
            sbSql.Append(" CompanyCD=@CompanyCD and SaleFlag=1");
            SqlParameter[] Paras = { new SqlParameter("@CompanyCD", SqlDbType.VarChar) };
            Paras[0].Value = model.CompanyCD;
            return SqlHelper.ExecuteSql(sbSql.ToString(), Paras);
        }
        #endregion

        #region 添加配置价格
        public static string AddSubProductSellPrice(Model.Office.LogisticsDistributionManager.SubProductSellPrice model)
        {

            #region 验证是否添加过该产品的配送价格
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT @IsHas=COUNT(*) FROM officedba.SubProductSellPrice WHERE ProductID=@ProductID AND CompanyCD=@CompanyCD AND DeptID=@DeptID");
            SqlParameter[] paras = { 
                                       new SqlParameter("@ProductID",SqlDbType.Int),
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                       new SqlParameter("@DeptID",SqlDbType.Int),
                                       new SqlParameter("@IsHas",SqlDbType.VarChar,50)
                                   };
            paras[0].Value = model.ProductID;
            paras[1].Value = model.CompanyCD;
            paras[2].Value = model.DeptID;
            paras[3].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteSql(sbSql.ToString(), paras);
            string Flag = paras[3].Value.ToString();
            if (Flag != "0" )
                return "-1";
            #endregion







            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.SubProductSellPrice(");
            strSql.Append("CompanyCD,ProductID,DeptID,SubPriceTax,SubPrice,SubTax,Discount,Creator,CreateDate,ModifiedDate,ModifiedUserID)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@ProductID,@DeptID,@SubPriceTax,@SubPrice,@SubTax,@Discount,@Creator,@CreateDate,@ModifiedDate,@ModifiedUserID)");
            strSql.Append(";select @ID=@@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@ProductID", SqlDbType.Int,4),
					new SqlParameter("@DeptID", SqlDbType.Int,4),
					new SqlParameter("@SubPriceTax", SqlDbType.Decimal,9),
					new SqlParameter("@SubPrice", SqlDbType.Decimal,9),
					new SqlParameter("@SubTax", SqlDbType.Decimal,9),
					new SqlParameter("@Discount", SqlDbType.Decimal,5),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,10),
                    new SqlParameter("@ID",SqlDbType.Int) };
            parameters[0].Value = model.CompanyCD;
            parameters[1].Value = model.ProductID;
            parameters[2].Value = model.DeptID;
            parameters[3].Value = model.SubPriceTax;
            parameters[4].Value = model.SubPrice;
            parameters[5].Value = model.SubTax;
            parameters[6].Value = model.Discount;
            parameters[7].Value = model.Creator;
            parameters[8].Value = model.CreateDate;
            parameters[9].Value = model.ModifiedDate;
            parameters[10].Value = model.ModifiedUserID;
            parameters[11].Direction = ParameterDirection.Output;

            ArrayList SqlList = new ArrayList();
            SqlCommand SqlCmd = new SqlCommand();
            SqlCmd.CommandText = strSql.ToString();
            SqlCmd.Parameters.AddRange(parameters);
            SqlList.Add(SqlCmd);
            bool result = SqlHelper.ExecuteTransWithArrayList(SqlList);
            if (result)
            {
                return ((SqlCommand)SqlList[0]).Parameters["@ID"].Value.ToString();
            }
            else
                return string.Empty;


        }
        #endregion

        #region 更新价格配置
        public static string UpdateSubProductSellPrice(Model.Office.LogisticsDistributionManager.SubProductSellPrice model)
        {

            #region 验证是否添加过该产品的配送价格
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT top 1 @IsHas=ID FROM officedba.SubProductSellPrice WHERE ProductID=@ProductID AND CompanyCD=@CompanyCD AND DeptID=@DeptID");
            SqlParameter[] paras = { 
                                       new SqlParameter("@ProductID",SqlDbType.Int),
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                       new SqlParameter("@DeptID",SqlDbType.Int),
                                       new SqlParameter("@IsHas",SqlDbType.VarChar,50)
                                   };
            paras[0].Value = model.ProductID;
            paras[1].Value = model.CompanyCD;
            paras[2].Value = model.DeptID;
            paras[3].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteSql(sbSql.ToString(), paras);
            string Flag = paras[3].Value.ToString();
            if (Flag !=model.ID.ToString() && !string.IsNullOrEmpty(Flag))
                return "-1";
            #endregion





            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.SubProductSellPrice set ");
            strSql.Append("CompanyCD=@CompanyCD,");
            strSql.Append("ProductID=@ProductID,");
            strSql.Append("DeptID=@DeptID,");
            strSql.Append("SubPriceTax=@SubPriceTax,");
            strSql.Append("SubPrice=@SubPrice,");
            strSql.Append("SubTax=@SubTax,");
            strSql.Append("Discount=@Discount,");
            strSql.Append("ModifiedDate=@ModifiedDate,");
            strSql.Append("ModifiedUserID=@ModifiedUserID");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@ProductID", SqlDbType.Int,4),
					new SqlParameter("@DeptID", SqlDbType.Int,4),
					new SqlParameter("@SubPriceTax", SqlDbType.Decimal,9),
					new SqlParameter("@SubPrice", SqlDbType.Decimal,9),
					new SqlParameter("@SubTax", SqlDbType.Decimal,9),
					new SqlParameter("@Discount", SqlDbType.Decimal,5),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,10)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.CompanyCD;
            parameters[2].Value = model.ProductID;
            parameters[3].Value = model.DeptID;
            parameters[4].Value = model.SubPriceTax;
            parameters[5].Value = model.SubPrice;
            parameters[6].Value = model.SubTax;
            parameters[7].Value = model.Discount;
            parameters[8].Value = model.ModifiedDate;
            parameters[9].Value = model.ModifiedUserID;

            ArrayList SqlList = new ArrayList();
            SqlCommand SqlCmd = new SqlCommand();
            SqlCmd.CommandText = strSql.ToString();
            SqlCmd.Parameters.AddRange(parameters);
            SqlList.Add(SqlCmd);
            bool result = SqlHelper.ExecuteTransWithArrayList(SqlList);
            if (result)
                return "1";
            else
                return "0";

        }
        #endregion

        #region 读取配置价格列表
        public static DataTable GetSubProductSellPriceList(Hashtable htPara, ref int TotalCount)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT sp.*, ( SELECT di.DeptName FROM officedba.DeptInfo as di  WHERE di.ID=sp.DeptID )  as DetpName,");
            sbSql.Append(" pi.ProductName ,pi.ProdNo,pi.Specification,(select CodeName from officedba.CodeUnitType as ct where ct.ID=pi.UnitID  ) as UnitName ,");
            sbSql.Append(" ( SELECT ei.EmployeeName from officedba.EmployeeInfo  as ei where ei.ID=sp.Creator) as CreatorName ");
            sbSql.Append(" FROM officedba.SubProductSellPrice as sp inner join officedba.ProductInfo  as pi on pi.ID=sp.ProductID  WHERE 1=1 AND sp.CompanyCD=@CompanyCD ");
            SqlParameter[] Paras = new SqlParameter[htPara.Count-3];
            int index = 0;
            foreach (string key in htPara.Keys)
            {
                switch (key)
                {
                    case "@DeptID":
                        sbSql.Append(" AND DeptID=" + key);
                        Paras[index] = new SqlParameter(key, SqlDbType.Int);
                        Paras[index].Value = htPara[key];
                        index++;
                        break;
                    case "@ProductID":
                        sbSql.Append(" AND ProductID=" + key);
                        Paras[index] = new SqlParameter(key, SqlDbType.Int);
                        Paras[index].Value = htPara[key];
                        index++;
                        break;
                    case "@CompanyCD":
                        Paras[index] = new SqlParameter(key, SqlDbType.VarChar);
                        Paras[index++].Value = htPara[key];
                        break;
                    case "@BarCode":
                        sbSql.Append(" AND pi.BarCode=@BarCode ");
                        Paras[index++] = SqlHelper.GetParameter("@BarCode", htPara["@BarCode"]);
                        break;
                }
            }
            return SqlHelper.CreateSqlByPageExcuteSql(sbSql.ToString(), Convert.ToInt32(htPara["@PageIndex"].ToString()), Convert.ToInt32(htPara["@PageSize"]), htPara["@OrderBy"].ToString(), Paras, ref TotalCount);

        }

        /*不分页*/
        public static DataTable GetSubProductSellPriceList(Hashtable htPara)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT sp.*, ( SELECT di.DeptName FROM officedba.DeptInfo as di  WHERE di.ID=sp.DeptID )  as DetpName,");
            sbSql.Append(" pi.ProductName ,pi.ProdNo,pi.Specification,(select CodeName from officedba.CodeUnitType as ct where ct.ID=pi.UnitID  ) as UnitName ,");
            sbSql.Append(" ( SELECT ei.EmployeeName from officedba.EmployeeInfo  as ei where ei.ID=sp.Creator) as CreatorName ");
            sbSql.Append(" FROM officedba.SubProductSellPrice as sp inner join officedba.ProductInfo  as pi on pi.ID=sp.ProductID  WHERE 1=1 and sp.CompanyCD=@CompanyCD ");
            SqlParameter[] Paras = new SqlParameter[htPara.Count - 1];
            int index = 0;
            foreach (string key in htPara.Keys)
            {
                switch (key)
                {
                    case "@DeptID":
                        sbSql.Append(" AND DeptID=" + key);
                        Paras[index] = new SqlParameter(key, SqlDbType.Int);
                        Paras[index].Value = htPara[key];
                        index++;
                        break;
                    case "@ProductID":
                        sbSql.Append(" AND ProductID=" + key);
                        Paras[index] = new SqlParameter(key, SqlDbType.Int);
                        Paras[index].Value = htPara[key];
                        index++;
                        break;
                    case "@CompanyCD":
                        Paras[index] = new SqlParameter(key, SqlDbType.VarChar);
                        Paras[index++].Value = htPara[key];
                        break;
                    case "@BarCode":
                        sbSql.Append(" AND pi.BarCode=@BarCode ");
                        Paras[index++] = SqlHelper.GetParameter("@BarCode", htPara["@BarCode"]);
                        break;
                }
            }
            sbSql.Append(" ORDER BY " + htPara["@OrderBy"]);
            return SqlHelper.ExecuteSql(sbSql.ToString(), Paras);
           // return SqlHelper.CreateSqlByPageExcuteSql(sbSql.ToString(), Convert.ToInt32(htPara["@PageIndex"].ToString()), Convert.ToInt32(htPara["@PageSize"]), htPara["@OrderBy"].ToString(), Paras, ref TotalCount);

        }
        #endregion

        #region 删除配置价格
        public static bool DelSubProductSellPrice(int[] IDList)
        {
            ArrayList SqlCmdList = new ArrayList();
            for (int i = 0; i < IDList.Length; i++)
            {
                StringBuilder sbSql = new StringBuilder();
                sbSql.Append(" DELETE officedba.SubProductSellPrice WHERE ID=@ID ");
                SqlParameter[] Paras = { new SqlParameter("@ID", SqlDbType.Int) };
                Paras[0].Value = IDList[i];

                SqlCommand SqlCmd = new SqlCommand()
                {
                    CommandText = sbSql.ToString(),
                };
                SqlCmd.Parameters.AddRange(Paras);
                SqlCmdList.Add(SqlCmd);

            }

            bool res = SqlHelper.ExecuteTransWithArrayList(SqlCmdList);
            return res;
        }
        #endregion

        #region 导入销售价格

        /// <summary>
        /// 判断销售价格是否已经存在
        /// </summary>
        /// <param name="CompanyCD">公司</param>
        /// <param name="DeptName">分店</param>
        /// <param name="ProdNo">物品编号</param>
        /// <returns></returns>
        public static bool ExisitSellPrice(string CompanyCD, string DeptName, string ProdNo)
        {
            string sqlStr = @"SELECT spsp.ID FROM officedba.SubProductSellPrice spsp
                                LEFT OUTER JOIN officedba.ProductInfo pi1 ON pi1.ID=spsp.ProductID
                                LEFT OUTER JOIN officedba.DeptInfo di ON di.ID=spsp.DeptID
                                WHERE spsp.CompanyCD=@CompanyCD AND pi1.ProdNo=@ProdNo AND ISNULL(di.DeptName,'默认')=@DeptName";
            SqlParameter[] paras = { 
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                       new SqlParameter("@DeptName",SqlDbType.VarChar),
                                       new SqlParameter("@ProdNo",SqlDbType.VarChar)
                                   };
            paras[0].Value = CompanyCD;
            paras[1].Value = DeptName;
            paras[2].Value = ProdNo;
            return SqlHelper.ExecuteSql(sqlStr, paras).Rows.Count > 0;
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="dt">数据集</param>
        /// <param name="userInfo">人员信息</param>
        /// <returns></returns>
        public static bool ImportData(DataTable dt, UserInfoUtil userInfo)
        {
            ArrayList list = new ArrayList();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(GetImportInsert(dr, userInfo));
            }
            return SqlHelper.ExecuteTransWithArrayList(list);
        }

        /// <summary>
        /// 导入数据命令
        /// </summary>
        /// <param name="dr">数据集</param>
        /// <param name="userInfo">人员信息</param>
        /// <returns></returns>
        private static SqlCommand GetImportInsert(DataRow dr, UserInfoUtil userInfo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"INSERT INTO officedba.SubProductSellPrice(CompanyCD, ProductID, DeptID,SubPrice,SubPriceTax,SubTax, Discount, Creator, CreateDate,ModifiedDate, ModifiedUserID)
                                SELECT @CompanyCD
                                        ,(SELECT TOP(1) pi1.ID FROM officedba.ProductInfo pi1 WHERE pi1.CompanyCD=@CompanyCD AND pi1.ProdNo=@ProdNo)
                                        ,ISNULL((SELECT TOP(1) di.ID FROM officedba.DeptInfo di WHERE di.CompanyCD=@CompanyCD AND di.DeptName=@DeptName),0)
                                        ,@SubPrice,@SubPrice*(1+@SubTax),@SubTax,@Discount,@Creator,GETDATE(),GETDATE(),@ModifiedUserID";
            SqlParameter[] paras = { 
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                       new SqlParameter("@ProdNo",SqlDbType.VarChar),
                                       new SqlParameter("@DeptName",SqlDbType.VarChar),
                                       new SqlParameter("@SubPrice",SqlDbType.Decimal),
                                       new SqlParameter("@SubTax",SqlDbType.Decimal),
                                       new SqlParameter("@Discount",SqlDbType.Decimal),
                                       new SqlParameter("@Creator",SqlDbType.Int),
                                       new SqlParameter("@ModifiedUserID",SqlDbType.VarChar)
                                   };
            int i = 0;
            paras[i++].Value = userInfo.CompanyCD;
            paras[i++].Value = dr["物品编号"];
            paras[i++].Value = dr["分店名称"];
            paras[i++].Value = dr["去税单价"];
            paras[i++].Value = dr["税率"];
            paras[i++].Value = dr["折扣(%)"];
            paras[i++].Value = userInfo.EmployeeID;
            paras[i++].Value = userInfo.UserID;
            cmd.Parameters.AddRange(paras);
            return cmd;
        }

        #endregion

    }
}

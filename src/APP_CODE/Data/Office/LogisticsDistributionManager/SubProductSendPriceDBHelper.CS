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
    public class SubProductSendPriceDBHelper
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
        public static string AddSubProductSendPrice(Model.Office.LogisticsDistributionManager.SubProductSendPrice model)
        {

            #region 验证是否添加过该产品的配送价格
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT @IsHas=COUNT(*) FROM officedba.SubProductSendPrice WHERE ProductID=@ProductID AND CompanyCD=@CompanyCD AND DeptID=@DeptID");
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
            if (Flag != "0")
                return "-1";
            #endregion




            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.SubProductSendPrice(");
            strSql.Append("CompanyCD,ProductID,DeptID,SendPrice,SendPriceTax,SendTax,Discount,Creator,CreateDate,ModifiedDate,ModifiedUserID)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@ProductID,@DeptID,@SendPrice,@SendPriceTax,@SendTax,@Discount,@Creator,@CreateDate,@ModifiedDate,@ModifiedUserID)");
            strSql.Append(";select @ID=@@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@ProductID", SqlDbType.Int,4),
					new SqlParameter("@DeptID", SqlDbType.Int,4),
					new SqlParameter("@SendPrice", SqlDbType.Decimal,9),
					new SqlParameter("@SendPriceTax", SqlDbType.Decimal,9),
					new SqlParameter("@SendTax", SqlDbType.Decimal,9),
					new SqlParameter("@Discount", SqlDbType.Decimal,5),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50),
                    new SqlParameter("@ID",SqlDbType.Int)};
            parameters[0].Value = model.CompanyCD;
            parameters[1].Value = model.ProductID;
            parameters[2].Value = model.DeptID;
            parameters[3].Value = model.SendPrice;
            parameters[4].Value = model.SendPriceTax;
            parameters[5].Value = model.SendTax;
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
        public static string UpdateSubProductSendPrice(Model.Office.LogisticsDistributionManager.SubProductSendPrice model)
        {
            #region 验证是否添加过该产品的配送价格
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT TOP 1 @IsHas=ID FROM officedba.SubProductSendPrice WHERE ProductID=@ProductID AND CompanyCD=@CompanyCD AND DeptID=@DeptID");
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
            if (Flag != model.ID.ToString() && !string.IsNullOrEmpty(Flag))
                return "-1";
            #endregion








            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.SubProductSendPrice set ");
            strSql.Append("CompanyCD=@CompanyCD,");
            strSql.Append("ProductID=@ProductID,");
            strSql.Append("DeptID=@DeptID,");
            strSql.Append("SendPrice=@SendPrice,");
            strSql.Append("SendPriceTax=@SendPriceTax,");
            strSql.Append("SendTax=@SendTax,");
            strSql.Append("Discount=@Discount,");
            strSql.Append("ModifiedDate=@ModifiedDate,");
            strSql.Append("ModifiedUserID=@ModifiedUserID");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@ProductID", SqlDbType.Int,4),
					new SqlParameter("@DeptID", SqlDbType.Int,4),
					new SqlParameter("@SendPrice", SqlDbType.Decimal,9),
					new SqlParameter("@SendPriceTax", SqlDbType.Decimal,9),
					new SqlParameter("@SendTax", SqlDbType.Decimal,9),
					new SqlParameter("@Discount", SqlDbType.Decimal,5),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.CompanyCD;
            parameters[2].Value = model.ProductID;
            parameters[3].Value = model.DeptID;
            parameters[4].Value = model.SendPrice;
            parameters[5].Value = model.SendPriceTax;
            parameters[6].Value = model.SendTax;
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
        /// <summary>
        /// 读取配置价格列表
        /// </summary>
        /// <param name="htPara">参数</param>
        /// <param name="isProduct">是否包含库存</param>
        /// <param name="TotalCount">返回参数</param>
        /// <returns></returns>
        public static DataTable GetSubProductSendPriceList(Hashtable htPara, bool isProduct, ref int TotalCount)
        {

            int length = htPara.Count;
            string EFSql = string.Empty;
            if (htPara.ContainsKey("@EFIndex") && htPara.ContainsKey("@EFDesc"))
            {
                length = length - 2;
                EFSql = " AND pi." + htPara["@EFIndex"] + " LIKE '" + htPara["@EFDesc"] + "' ";
            }


            SqlParameter[] Paras = new SqlParameter[length - 3];
            int index = 0;
            string tmpSql = string.Empty;
            if (htPara.ContainsKey("@SubStore"))
            {
                tmpSql += " AND di.DeptName Like @SubStore";
                Paras[index] = new SqlParameter("@SubStore", SqlDbType.VarChar);
                Paras[index++].Value = htPara["@SubStore"];
            }
            StringBuilder sbSql = new StringBuilder();
            if (isProduct)
            {
                sbSql.AppendFormat(@"SELECT sp.*
                        ,( SELECT di.DeptName FROM officedba.DeptInfo as di  WHERE di.ID=sp.DeptID {0} )  as DetpName
                        ,pi.ProductName ,pi.ProdNo,pi.Specification,pi.IsBatchNo
                        ,(select CodeName from officedba.CodeUnitType as ct where ct.ID=pi.UnitID  ) as UnitName,pi.UnitID ,pi.SellPrice
                        ,( SELECT ei.EmployeeName from officedba.EmployeeInfo  as ei where ei.ID=sp.Creator) as CreatorName
                        ,sp2.BatchNo,sp2.StorageID,si.StorageName
                        FROM officedba.SubProductSendPrice as sp 
                        INNER JOIN officedba.ProductInfo  as pi on pi.ID=sp.ProductID AND pi.CheckStatus=1
                        LEFT JOIN officedba.StorageProduct sp2 ON sp.ProductID=sp2.ProductID AND sp.CompanyCD=sp2.CompanyCD
                        LEFT JOIN officedba.StorageInfo si ON si.ID=sp2.StorageID   
                        WHERE sp.CompanyCD=@CompanyCD ", tmpSql);
            }
            else
            {
                sbSql.AppendFormat(@"SELECT sp.*
                        ,( SELECT di.DeptName FROM officedba.DeptInfo as di  WHERE di.ID=sp.DeptID {0} )  as DetpName
                        ,pi.ProductName ,pi.ProdNo,pi.Specification,pi.IsBatchNo
                        ,(select CodeName from officedba.CodeUnitType as ct where ct.ID=pi.UnitID  ) as UnitName,pi.UnitID ,pi.SellPrice
                        ,( SELECT ei.EmployeeName from officedba.EmployeeInfo  as ei where ei.ID=sp.Creator) as CreatorName
                        FROM officedba.SubProductSendPrice as sp 
                        INNER JOIN officedba.ProductInfo  as pi on pi.ID=sp.ProductID AND pi.CheckStatus=1
                        WHERE sp.CompanyCD=@CompanyCD ", tmpSql);
            }

            foreach (string key in htPara.Keys)
            {
                switch (key)
                {
                    case "@DeptID":
                        sbSql.Append(" AND  ( sp.DeptID=" + key);
                        /*显示默认配置的配送价格 但排除已经配置的产品 2009-09-04  BY PDD */
                        /*START*/
                        sbSql.Append("  OR ( sp.DeptID=0 AND sp.ProductID NOT IN (SELECT ProductID  FROM officedba.SubProductSendPrice spsp WHERE spsp.DeptID=@DeptID AND spsp.CompanyCD=@CompanyCD))");
                        sbSql.Append("  )");
                        /*END*/
                        Paras[index] = new SqlParameter(key, SqlDbType.Int);
                        Paras[index++].Value = htPara[key];
                        break;
                    case "@ProductID":
                        sbSql.Append(" AND sp.ProductID=" + key);
                        Paras[index] = new SqlParameter(key, SqlDbType.Int);
                        Paras[index++].Value = htPara[key];
                        break;
                    case "@ProductName":
                        sbSql.Append(" AND pi.ProductName Like @ProductName ");
                        Paras[index] = new SqlParameter(key, SqlDbType.VarChar);
                        Paras[index++].Value = htPara[key];
                        break;
                    case "@ProdNo":
                        sbSql.Append(" AND pi.ProdNo Like @ProdNo ");
                        Paras[index] = new SqlParameter(key, SqlDbType.VarChar);
                        Paras[index++].Value = htPara[key];
                        break;
                    case "@Specification":
                        sbSql.Append(" AND pi.Specification Like @Specification ");
                        Paras[index] = new SqlParameter(key, SqlDbType.VarChar);
                        Paras[index++].Value = htPara[key];
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

            /*追加扩展属性查询条件*/
            sbSql.Append(EFSql);

            return SqlHelper.CreateSqlByPageExcuteSql(sbSql.ToString(), Convert.ToInt32(htPara["@PageIndex"].ToString()), Convert.ToInt32(htPara["@PageSize"]), htPara["@OrderBy"].ToString(), Paras, ref TotalCount);

        }


        public static DataTable GetSubProductSendPriceList(Hashtable htPara)
        {
            SqlParameter[] Paras = new SqlParameter[htPara.Count - 1];
            int index = 0;
            string tmpSql = string.Empty;
            if (htPara.ContainsKey("@SubStore"))
            {
                tmpSql += " AND di.DeptName Like @SubStore";
                Paras[index] = new SqlParameter("@SubStore", SqlDbType.VarChar);
                Paras[index++].Value = htPara["@SubStore"];
            }
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT sp.*, ( SELECT di.DeptName FROM officedba.DeptInfo as di  WHERE di.ID=sp.DeptID " + tmpSql + "  )  as DetpName,");
            sbSql.Append(" pi.ProductName ,pi.ProdNo,pi.Specification,pi.IsBatchNo,(select CodeName from officedba.CodeUnitType as ct where ct.ID=pi.UnitID  ) as UnitName ,pi.SellPrice ,");
            sbSql.Append(" ( SELECT ei.EmployeeName from officedba.EmployeeInfo  as ei where ei.ID=sp.Creator) as CreatorName ");
            sbSql.Append(" FROM officedba.SubProductSendPrice as sp inner join officedba.ProductInfo  as pi on pi.ID=sp.ProductID   WHERE 1=1 and sp.CompanyCD=@CompanyCD ");
            foreach (string key in htPara.Keys)
            {
                switch (key)
                {
                    case "@DeptID":
                        sbSql.Append(" AND( DeptID=0  OR DeptID=" + key + " )");
                        Paras[index] = new SqlParameter(key, SqlDbType.Int);
                        Paras[index++].Value = htPara[key];
                        break;
                    case "@ProductID":
                        sbSql.Append(" AND ProductID=" + key);
                        Paras[index] = new SqlParameter(key, SqlDbType.Int);
                        Paras[index++].Value = htPara[key];
                        break;
                    case "@ProductName":
                        sbSql.Append(" AND pi.ProductName Like @ProductName ");
                        Paras[index] = new SqlParameter(key, SqlDbType.VarChar);
                        Paras[index++].Value = htPara[key];
                        break;
                    case "@ProdNo":
                        sbSql.Append(" AND pi.ProdNo Like @ProdNo ");
                        Paras[index] = new SqlParameter(key, SqlDbType.VarChar);
                        Paras[index++].Value = htPara[key];
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
        public static bool DelSubProductSendPrice(int[] IDList)
        {
            ArrayList SqlCmdList = new ArrayList();
            for (int i = 0; i < IDList.Length; i++)
            {
                StringBuilder sbSql = new StringBuilder();
                sbSql.Append(" DELETE officedba.SubProductSendPrice WHERE ID=@ID ");
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

        #region 导入配送价格

        /// <summary>
        /// 判断配送价格是否已经存在
        /// </summary>
        /// <param name="CompanyCD">公司</param>
        /// <param name="DeptName">分店</param>
        /// <param name="ProdNo">物品编号</param>
        /// <returns></returns>
        public static bool ExisitSendPrice(string CompanyCD, string DeptName, string ProdNo)
        {
            string sqlStr = @"SELECT spsp.ID FROM officedba.SubProductSendPrice spsp
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
        /// 判断部门是否已经存在
        /// </summary>
        /// <param name="CompanyCD">公司</param>
        /// <param name="DeptName">分店</param>
        /// <returns></returns>
        public static bool ExisitDeptName(string CompanyCD, string DeptName)
        {
            string sqlStr = @"SELECT di.ID 
                                FROM officedba.DeptInfo di
                                WHERE di.CompanyCD=@CompanyCD AND di.SaleFlag=1 AND di.DeptName=@DeptName";
            SqlParameter[] paras = { 
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                       new SqlParameter("@DeptName",SqlDbType.VarChar)
                                   };
            paras[0].Value = CompanyCD;
            paras[1].Value = DeptName;
            return SqlHelper.ExecuteSql(sqlStr, paras).Rows.Count > 0;
        }

        /// <summary>
        /// 判断物品编号是否已经存在
        /// </summary>
        /// <param name="CompanyCD">公司</param>
        /// <param name="ProdNo">物品编号</param>
        /// <returns></returns>
        public static bool ExisitProdNo(string CompanyCD, string ProdNo)
        {
            string sqlStr = @"SELECT pi1.ID
                                FROM officedba.ProductInfo pi1
                                WHERE pi1.CompanyCD=@CompanyCD AND pi1.CheckStatus=1 AND pi1.ProdNo=@ProdNo";
            SqlParameter[] paras = { 
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                       new SqlParameter("@ProdNo",SqlDbType.VarChar)
                                   };
            paras[0].Value = CompanyCD;
            paras[1].Value = ProdNo;
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
            cmd.CommandText = @"INSERT INTO officedba.SubProductSendPrice(CompanyCD, ProductID, DeptID, SendPrice,SendPriceTax, SendTax, Discount, Creator, CreateDate, ModifiedDate,ModifiedUserID)
                                SELECT @CompanyCD
                                        ,(SELECT TOP(1) pi1.ID FROM officedba.ProductInfo pi1 WHERE pi1.CompanyCD=@CompanyCD AND pi1.ProdNo=@ProdNo)
                                        ,ISNULL((SELECT TOP(1) di.ID FROM officedba.DeptInfo di WHERE di.CompanyCD=@CompanyCD AND di.DeptName=@DeptName),0)
                                        ,@SendPrice,@SendPrice*(1+@SendTax),@SendTax,@Discount,@Creator,GETDATE(),GETDATE(),@ModifiedUserID";
            SqlParameter[] paras = { 
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar),
                                       new SqlParameter("@ProdNo",SqlDbType.VarChar),
                                       new SqlParameter("@DeptName",SqlDbType.VarChar),
                                       new SqlParameter("@SendPrice",SqlDbType.Decimal),
                                       new SqlParameter("@SendTax",SqlDbType.Decimal),
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

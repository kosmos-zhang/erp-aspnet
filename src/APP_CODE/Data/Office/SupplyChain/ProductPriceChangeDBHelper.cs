using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Model.Office.SupplyChain;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Collections;
using XBase.Common;
namespace XBase.Data.Office.SupplyChain
{
  public class ProductPriceChangeDBHelper
    {
        /// <summary>
        /// 查询物品信息
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
      public static DataTable GetProductPriceInfo(ProductPriceChangeModel model, string starttime, string endtime, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT a.ID                                    ");
            searchSql.AppendLine("      ,a.ChangeNo                              ");
            searchSql.AppendLine("      ,a.CompanyCD                             ");
            searchSql.AppendLine("      ,a.Title                                 ");
            searchSql.AppendLine("      ,isnull(b.ProdNo,'')as ProductID      ");
            searchSql.AppendLine("      ,isnull(Convert(numeric(14,"+userInfo.SelPoint+"),a.StandardSell),0)as StandardSell");
            searchSql.AppendLine("      ,isnull(Convert(numeric(14,"+userInfo.SelPoint+"),a.SellTax),0)as SellTax                                  ");
            searchSql.AppendLine("      ,isnull(Convert(numeric(14,"+userInfo.SelPoint+"),a.StandardSellNew),0)as StandardSellNew                   ");
            searchSql.AppendLine("      ,isnull(Convert(numeric(14,"+userInfo.SelPoint+"),a.TaxRateNew),0)as TaxRateNew                   ");
            searchSql.AppendLine("      ,isnull(Convert(numeric(12," + userInfo.SelPoint + "),a.DiscountNew),0)as DiscountNew                   ");
            searchSql.AppendLine("      ,isnull(Convert(numeric(12,"+userInfo.SelPoint+"),a.Discount),0)as Discount                   ");
            searchSql.AppendLine("      ,isnull(Convert(numeric(14,"+userInfo.SelPoint+"),a.TaxRate),0)as TaxRate                   ");
            searchSql.AppendLine("      ,isnull(Convert(numeric(14,"+userInfo.SelPoint+"),a.SellTaxNew),0) as  SellTaxNew                            ");
            searchSql.AppendLine("      ,isnull( CONVERT(CHAR(19), a.ChangeDate, 120),'') as ChangeDate                           ");
            searchSql.AppendLine("      ,isnull(a.Chenger,'')as Chenger                                  ");
            searchSql.AppendLine("      ,isnull(a.Remark,'')as  Remark                                    ");
            searchSql.AppendLine("      ,isnull(a.BillStatus,'') as BillStatus                            ");
            searchSql.AppendLine("      ,isnull(a.Creator,'')as    Creator                                ");
            searchSql.AppendLine("      ,isnull( CONVERT(CHAR(19), a.CreateDate, 120),'') as CreateDate                            ");
            searchSql.AppendLine("      ,isnull(a.Confirmor,'')as  Confirmor                              ");

            searchSql.AppendLine("      ,isnull( CONVERT(CHAR(19), a.ConfirmDate, 120),'') as ConfirmDate                           ");
            searchSql.AppendLine("      ,isnull(b.ProductName,'')as ProductName                           ");
            searchSql.AppendLine("      ,isnull(c.EmployeeName,'') as   ConfirmName");
            searchSql.AppendLine("  FROM officedba.ProductPriceChange as a    ");
            searchSql.AppendLine("left join officedba.ProductInfo as b on a.ProductID=b.ID and a.CompanyCD=b.CompanyCD    ");
            searchSql.AppendLine("left join officedba.EmployeeInfo as c on a.Confirmor=c.ID and a.CompanyCD=c.CompanyCD    ");
            searchSql.AppendLine("        where   a.CompanyCD=@CompanyCD       ");

            //#endregion
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            ////公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //编号
            if (!string.IsNullOrEmpty(model.ChangeNo))
            {
                searchSql.AppendLine("	and a.ChangeNo LIKE @ChangeNo ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ChangeNo", "%" + model.ChangeNo + "%"));
            }
            //名称
            if (!string.IsNullOrEmpty(model.Title))
            {
                searchSql.AppendLine("	AND a.Title LIKE @Title ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", "%" + model.Title + "%"));
            }
            if (!string.IsNullOrEmpty(model.ProductID))
            {
                string ID = model.ProductID;
                string allID = "";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string[] IdS = null;
                ID = ID.Substring(0, ID.Length);
                IdS = ID.Split(',');
                for (int i = 0; i < IdS.Length; i++)
                {
                    IdS[i] = "'" + IdS[i] + "'";
                    sb.Append(IdS[i]);
                }
                allID = sb.ToString().Replace("''", "','");
                searchSql.AppendLine("	AND  a.ProductID in  (" + allID + ") ");
            }
            if (!string.IsNullOrEmpty(model.Chenger))
            {
                string ID = model.Chenger;
                string allID = "";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string[] IdS = null;
                ID = ID.Substring(0, ID.Length);
                IdS = ID.Split(',');
                for (int i = 0; i < IdS.Length; i++)
                {
                    IdS[i] = "'" + IdS[i] + "'";
                    sb.Append(IdS[i]);
                }
                allID = sb.ToString().Replace("''", "','");
                searchSql.AppendLine("	AND  a.Chenger in  (" + allID + ") ");
            }
            //时间
            if (!string.IsNullOrEmpty(starttime))
            {
                searchSql.AppendLine("	AND a.ChangeDate >= @starttime ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@starttime", starttime));
            }
            if (!string.IsNullOrEmpty(endtime))
            {
                searchSql.AppendLine("	AND a.ChangeDate<=@endtime");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@endtime", endtime));
            }
        comm.CommandText = searchSql.ToString();
        return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
         
        }
        /// <summary>
        /// 插入物品档案信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
      public static bool InsertProductPriceInfo(ProductPriceChangeModel model, out string ID)
        {
            //SQL拼写
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("INSERT INTO officedba.ProductPriceChange");      
            sql.AppendLine("           (ChangeNo                              ");  
            sql.AppendLine("           ,CompanyCD                             ");  
            sql.AppendLine("           ,Title                                 ");  
            sql.AppendLine("           ,ProductID                             ");  
            sql.AppendLine("           ,StandardSell                          ");  
            sql.AppendLine("           ,SellTax                               ");  
            sql.AppendLine("           ,StandardSellNew                       ");  
            sql.AppendLine("           ,SellTaxNew                            ");  
            sql.AppendLine("           ,ChangeDate                            ");  
            sql.AppendLine("           ,Chenger                               ");  
            sql.AppendLine("           ,Remark                                ");  
            sql.AppendLine("           ,BillStatus                            ");  
            sql.AppendLine("           ,Creator                               ");  
            sql.AppendLine("           ,CreateDate                            ");  
            sql.AppendLine("           ,Confirmor                             ");  
            sql.AppendLine("           ,ModifiedDate                          ");
            sql.AppendLine("           ,TaxRate                               ");
            sql.AppendLine("           ,Discount                              ");
            sql.AppendLine("           ,TaxRateNew                            ");
            sql.AppendLine("           ,DiscountNew                           ");  
            sql.AppendLine("           ,ModifiedUserID)                       ");  
            sql.AppendLine("     VALUES                                       ");
            sql.AppendLine("           (@ChangeNo                             ");               
            sql.AppendLine("           ,@CompanyCD                            ");              
            sql.AppendLine("           ,@Title                                ");               
            sql.AppendLine("           ,@ProductID                           ");               
            sql.AppendLine("           ,@StandardSell                         ");               
            sql.AppendLine("           ,@SellTax                            ");               
            sql.AppendLine("           ,@StandardSellNew                      ");               
            sql.AppendLine("           ,@SellTaxNew                            ");               
            sql.AppendLine("           ,@ChangeDate                          ");              
            sql.AppendLine("           ,@Chenger                             ");               
            sql.AppendLine("           ,@Remark                              ");               
            sql.AppendLine("           ,@BillStatus                          ");               
            sql.AppendLine("           ,@Creator                              ");
            sql.AppendLine("           ,@CreateDate                           ");              
            sql.AppendLine("           ,@Confirmor                            ");               
            sql.AppendLine("           ,@ModifiedDate                         ");
            sql.AppendLine("           ,@TaxRate                               ");
            sql.AppendLine("           ,@Discount                              ");
            sql.AppendLine("           ,@TaxRateNew                            ");
            sql.AppendLine("           ,@DiscountNew                           ");    
            sql.AppendLine("           ,@ModifiedUserID   )      ");               
            sql.AppendLine("   SET @ID= @@IDENTITY  ");
            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            //设置存储过程名
            comm.CommandText = sql.ToString();
            //设置保存的参数
            SetSaveParameter(comm, model,false);
            //添加返回参数
            comm.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));

            //执行登陆操作
            bool isSucc = SqlHelper.ExecuteTransWithCommand(comm);
            //设置ID
            //model.ID = int.Parse(comm.Parameters["@ProdID"].Value);
            ID = comm.Parameters["@ID"].Value.ToString();
            return isSucc;
        }

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="comm"></param>
        /// <param name="model"></param>
        private static void SetSaveParameter(SqlCommand comm, ProductPriceChangeModel model,bool flag)
        {
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));//公司代码   
            if (!flag)
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ChangeNo", model.ChangeNo));
            }
            else
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", Convert.ToString(model.ID)));
            }
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Title", model.Title));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", model.ProductID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StandardSell", model.StandardSell));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SellTax", model.SellTax));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StandardSellNew", model.StandardSellNew));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SellTaxNew", model.SellTaxNew));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ChangeDate", model.ChangeDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Chenger", model.Chenger));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", model.Remark));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", model.BillStatus));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator", model.Creator));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateDate", model.CreateDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Confirmor", model.Confirmor));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaxRate", model.TaxRate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Discount", model.Discount));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaxRateNew", model.TaxRateNew));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DiscountNew", model.DiscountNew));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedDate", model.ModifiedDate));//最后更新日期                                      
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));//最后更新用户ID(对应操作用户U  serID)          

        }

        /// <summary>
        /// 修改物品档案
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateProductPriceInfo(ProductPriceChangeModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE officedba.ProductPriceChange ");       
            sql.AppendLine("   SET                               ");         
            sql.AppendLine("       Title = @Title                  ");        
            sql.AppendLine("      ,ProductID = @ProductID             ");    
            sql.AppendLine("      ,StandardSell = @StandardSell       ");    
            sql.AppendLine("      ,SellTax = @SellTax                 ");    
            sql.AppendLine("      ,StandardSellNew = @StandardSellNew  ");   
            sql.AppendLine("      ,SellTaxNew = @SellTaxNew            ");   
            sql.AppendLine("      ,ChangeDate = @ChangeDate           ");
            sql.AppendLine("      ,Chenger = @Chenger                      ");
            sql.AppendLine("      ,Remark = @Remark              ");         
            sql.AppendLine("      ,BillStatus = @BillStatus           ");    
            sql.AppendLine("      ,Creator = @Creator                     ");
            sql.AppendLine("      ,CreateDate = @CreateDate           ");    
            sql.AppendLine("      ,Confirmor = @Confirmor                 ");
            sql.AppendLine("      ,ModifiedDate = @ModifiedDate       ");
            sql.AppendLine("      ,TaxRate = @TaxRate");
            sql.AppendLine("      ,TaxRateNew = @TaxRateNew");
            sql.AppendLine("      ,Discount = @Discount");
            sql.AppendLine("      ,DiscountNew = @DiscountNew");
            sql.AppendLine("      ,ModifiedUserID = @ModifiedUserID");       
            sql.AppendLine(" WHERE ID=@ID and CompanyCD=@CompanyCD     ");   
            //定义更新基本信息的命令  
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql.ToString();
            SetSaveParameter(comm, model,true);//其他参数
            //执行更新并设置更新结果
            bool result = false;
            result = SqlHelper.ExecuteTransWithCommand(comm);
            return result;

        }
        /// <summary>
        /// 删除物品信息
        /// </summary>
        /// <param name="TypeFlag"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteProductPriceInfo(string ID, string CompanyCD)
        {
            string allID = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string[] Delsql = new string[1];
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
                allID = sb.ToString().Replace("''", "','");

                Delsql[0] = "delete from  officedba.ProductPriceChange where ID IN (" + allID + ") and CompanyCD = @CompanyCD";

                SqlCommand comm = new SqlCommand();
                comm.CommandText = Delsql[0].ToString();

                //设置参数
                comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
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


        public static DataTable GetProductPriceInfoByID(int ID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT a.ID                             ");
            sql.AppendLine("      ,a.CompanyCD                      ");
            sql.AppendLine("      ,a.ChangeNo                         ");
            sql.AppendLine("      ,a.Title                        ");
            sql.AppendLine("      ,b.ProductName                    ");
            sql.AppendLine("      ,a.ProductID                       ");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.TaxRate) as TaxRate");
            sql.AppendLine("      ,Convert(numeric(12," + userInfo.SelPoint + "),a.Discount) as Discount");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.TaxRateNew) as TaxRateNew");
            sql.AppendLine("      ,a.ModifiedUserID                        ");
            sql.AppendLine(" 	,CONVERT(VARCHAR(10),a.ModifiedDate  ,21) AS ModifiedDate ");
            sql.AppendLine("      ,Convert(numeric(12," + userInfo.SelPoint + "),a.DiscountNew) as DiscountNew");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.StandardSell) as StandardSell");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.SellTax) as SellTax");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.StandardSellNew) as StandardSellNew");
            sql.AppendLine("      ,Convert(numeric(14," + userInfo.SelPoint + "),a.SellTaxNew) as SellTaxNew");
            sql.AppendLine("      ,a.Chenger                        ");
            sql.AppendLine(" 	,CONVERT(VARCHAR(10),a.ChangeDate  ,21) AS ChangeDate ");
            sql.AppendLine("      ,a.Creator                      ");
            sql.AppendLine(" 	,CONVERT(VARCHAR(10),a.CreateDate ,21) AS CreateDate ");
            sql.AppendLine("      ,a.Confirmor                      ");
            sql.AppendLine(" 	,CONVERT(VARCHAR(10),a.ConfirmDate ,21) AS ConfirmDate ");
            sql.AppendLine("      ,a.Remark                     ");
            sql.AppendLine("      ,a.BillStatus                   ");
            sql.AppendLine("      ,case a.BillStatus when '1' then '制单' when '5' then '结单' end AS BillStatusName                   ");
            sql.AppendLine("      ,c.EmployeeName as ChengeName                   ");
            sql.AppendLine("      ,d.EmployeeName as CreatorName                   ");
            sql.AppendLine("      ,e.EmployeeName as ConfirmorName                   ");
            sql.AppendLine("  FROM officedba.ProductPriceChange as a");
            sql.AppendLine("left join officedba.EmployeeInfo as c on a.Chenger=c.ID     ");
            sql.AppendLine("left join officedba.EmployeeInfo as d on a.Creator=d.ID     ");
            sql.AppendLine("left join officedba.EmployeeInfo as e on a.Confirmor=e.ID     ");
            sql.AppendLine(" left join officedba.ProductInfo as b on a.ProductID=b.ID where a.ID=@ID");
            SqlParameter[] param = new SqlParameter[1];
            //人员ID
            param[0] = SqlHelper.GetParameter("@ID", ID);
            //执行查询
            DataTable data = SqlHelper.ExecuteSql(sql.ToString(), param);
            return data;
        }
        //public static bool UpdateStatus(int ProductID, string Status)
        //{
        //    string sql = "update officedba.ProductInfo set CheckStatus=@CheckStatus where ID=@ID";
        //    SqlParameter[] param = new SqlParameter[2];
        //    param[0] = SqlHelper.GetParameter("@CheckStatus", Status);
        //    param[1] = SqlHelper.GetParameter("@ID", ProductID);
        //    SqlHelper.ExecuteTransSql(sql.ToString(), param);
        //    return SqlHelper.Result.OprateCount > 0 ? true : false;
        //}
        /// <summary>
        /// 更改状态和售价
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public static bool UpdateStatus(int ID, string BillStatus, int ProductID, string StandardSell, string SellTax, string Confirmor, string ConfirmDate, string TaxRateNew, string DiscountNew, string ModifiedUserID)
        {
            string sql = "update officedba.ProductPriceChange set BillStatus=@BillStatus,Confirmor=@Confirmor,ConfirmDate=@ConfirmDate,TaxRateNew=@TaxRateNew,DiscountNew=@DiscountNew,ModifiedDate=@ModifiedDate,ModifiedUserID=@ModifiedUserID where ID=@ID";
            SqlCommand comm = new SqlCommand();
            //设置参数
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", ID));
            comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", BillStatus));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", System.DateTime.Now));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", ModifiedUserID));
            comm.Parameters.Add(SqlHelper.GetParameter("@Confirmor", Confirmor));
            comm.Parameters.Add(SqlHelper.GetParameter("@ConfirmDate", ConfirmDate));
            comm.Parameters.Add(SqlHelper.GetParameter("@TaxRateNew", TaxRateNew));
            comm.Parameters.Add(SqlHelper.GetParameter("@DiscountNew", DiscountNew));
            ArrayList lst = new ArrayList();
            comm.CommandText = sql;
            //添加基本信息更新命令
            lst.Add(comm);
            SqlCommand commsell = new SqlCommand();
            commsell.CommandText = "update officedba.ProductInfo set StandardSell=@StandardSell,SellTax=@SellTax where ID=@PID";
            commsell.Parameters.Add(SqlHelper.GetParameter("@StandardSell", StandardSell));
            commsell.Parameters.Add(SqlHelper.GetParameter("@SellTax", SellTax));
            commsell.Parameters.Add(SqlHelper.GetParameter("@PID", ProductID));
            lst.Add(commsell);
            return SqlHelper.ExecuteTransWithArrayList(lst);


        }

    }
}

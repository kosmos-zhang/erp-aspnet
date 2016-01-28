using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Model.Office.SupplyChain;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Data.SqlTypes;
using System.Data;
using XBase.Common;
using System.Collections;
namespace XBase.Data.Office.SupplyChain
{
   public class OtherCorpInfoDBHelper
    {
       /// <summary>
       /// 添加其他往来单位
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public static bool InsertOtherCorpInfo(OtherCorpInfoModel model)
       {
           //SQL拼写
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("INSERT INTO [officedba].[OtherCorpInfo]");
           sql.AppendLine("           (CompanyCD                        ");
           sql.AppendLine("           ,BigType                          ");
           sql.AppendLine("           ,CustNo                           ");
           sql.AppendLine("           ,CustName                         ");
           sql.AppendLine("           ,CorpNam                          ");
           sql.AppendLine("           ,PYShort                          ");
           sql.AppendLine("           ,CustNote                         ");
           sql.AppendLine("           ,AreaID                           ");
           sql.AppendLine("           ,CompanyType                      ");
           sql.AppendLine("           ,StaffCount                       ");
           sql.AppendLine("           ,SetupDate                        ");
           sql.AppendLine("           ,ArtiPerson                       ");
           sql.AppendLine("           ,SetupMoney                       ");
           sql.AppendLine("           ,SetupAddress                     ");
           sql.AppendLine("           ,CapitalScale                     ");
           sql.AppendLine("           ,SaleroomY                        ");
           sql.AppendLine("           ,ProfitY                          ");
           sql.AppendLine("           ,TaxCD                            ");
           sql.AppendLine("           ,BusiNumber                       ");
           sql.AppendLine("           ,isTax                            ");
           sql.AppendLine("           ,SellArea                         ");
           sql.AppendLine("           ,CountryID                        ");
           sql.AppendLine("           ,Province                         ");
           sql.AppendLine("           ,City                             ");
           sql.AppendLine("           ,Post                             ");
           sql.AppendLine("           ,ContactName                      ");
           sql.AppendLine("           ,Tel                              ");
           sql.AppendLine("           ,Fax                              ");
           sql.AppendLine("           ,Mobile                           ");
           sql.AppendLine("           ,email                            ");
           sql.AppendLine("           ,MoneyType                            ");
           sql.AppendLine("           ,Addr                             ");
           sql.AppendLine("           ,BillType                         ");
           sql.AppendLine("           ,PayType                          ");
           sql.AppendLine("           ,CurrencyType                     ");
           sql.AppendLine("           ,Remark                           ");
           sql.AppendLine("           ,UsedStatus                       ");
           sql.AppendLine("           ,Creator                          ");
           sql.AppendLine("           ,CreateDate                       ");
           sql.AppendLine("           ,ModifiedDate                     ");
           sql.AppendLine("           ,ModifiedUserID)                  ");
           sql.AppendLine("     VALUES                                  ");
           sql.AppendLine("           (@CompanyCD          ");
           sql.AppendLine("           ,@BigType            ");
           sql.AppendLine("           ,@CustNo             ");
           sql.AppendLine("           ,@CustName           ");
           sql.AppendLine("           ,@CorpNam            ");
           sql.AppendLine("           ,@PYShort            ");
           sql.AppendLine("           ,@CustNote           ");
           sql.AppendLine("           ,@AreaID             ");
           sql.AppendLine("           ,@CompanyType        ");
           sql.AppendLine("           ,@StaffCount         ");
           sql.AppendLine("           ,@SetupDate          ");
           sql.AppendLine("           ,@ArtiPerson         ");
           sql.AppendLine("           ,@SetupMoney         ");
           sql.AppendLine("           ,@SetupAddress       ");
           sql.AppendLine("           ,@CapitalScale       ");
           sql.AppendLine("           ,@SaleroomY          ");
           sql.AppendLine("           ,@ProfitY            ");
           sql.AppendLine("           ,@TaxCD              ");
           sql.AppendLine("           ,@BusiNumber         ");
           sql.AppendLine("           ,@isTax              ");
           sql.AppendLine("           ,@SellArea           ");
           sql.AppendLine("           ,@CountryID          ");
           sql.AppendLine("           ,@Province           ");
           sql.AppendLine("           ,@City               ");
           sql.AppendLine("           ,@Post               ");
           sql.AppendLine("           ,@ContactName        ");
           sql.AppendLine("           ,@Tel                ");
           sql.AppendLine("           ,@Fax                ");
           sql.AppendLine("           ,@Mobile             ");
           sql.AppendLine("           ,@email              ");
           sql.AppendLine("           ,@MoneyType          ");
           sql.AppendLine("           ,@Addr               ");
           sql.AppendLine("           ,@BillType           ");
           sql.AppendLine("           ,@PayType            ");
           sql.AppendLine("           ,@CurrencyType       ");
           sql.AppendLine("           ,@Remark             ");
           sql.AppendLine("           ,@UsedStatus         ");
           sql.AppendLine("           ,@Creator            ");
           sql.AppendLine("           ,@CreateDate         ");
           sql.AppendLine("           ,@ModifiedDate       ");
           sql.AppendLine("           ,@ModifiedUserID)    ");

           //定义更新基本信息的命令
           SqlCommand comm = new SqlCommand();
           //设置存储过程名
           comm.CommandText = sql.ToString();
           //设置保存的参数
           SetSaveParameter(comm, model);

           //添加返回参数
           comm.Parameters.Add(SqlHelper.GetOutputParameter("@OtherCorpInfoID", System.Data.SqlDbType.Int));

           //执行登陆操作
           bool isSucc = SqlHelper.ExecuteTransWithCommand(comm);
           //设置ID

           //执行插入并返回插入结果
           return isSucc;

       }

       private static void SetSaveParameter(SqlCommand comm, OtherCorpInfoModel model)
       {
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", model.CompanyCD));//	公司代码                                              
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@BigType", model.BigType));//	往来单位大类（1客户，2供应商，3竞争对手，4银行，5外协加?
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustNo", model.CustNo));//	往来单位编号                                            
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustName", model.CustName));//	往来单位名称                                          
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CorpNam", model.CorpNam));//	往来单位简称                                            
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@PYShort", model.PYShort));//	往来单位拼音代码                                        
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustNote", model.CustNote));//	往来单位简介                                          
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@AreaID", model.AreaID));//	所在区域（对应分类代码表定义，(客户)区域）              
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyType", model.CompanyType));//	单位性质                                    
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@StaffCount", model.StaffCount));//	人员规模                                            
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@SetupDate ", model.SetupDate));//	成立时间                                              
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@ArtiPerson", model.ArtiPerson));//	法人代表                                            
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@SetupMoney", model.SetupMoney));//	注册资本(万元)                                      
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@SetupAddress", model.SetupAddress));//	注册地址                                    
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CapitalScale", model.CapitalScale));//	资产规模(万元)                              
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@SaleroomY", model.SaleroomY));//	年销售额(万元)                                        
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProfitY", model.ProfitY));//	年利润额(万元)                                          
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaxCD ", model.TaxCD));//	税务登记号                                                
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@BusiNumber", model.BusiNumber));//	营业执照号                                          
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@isTax", model.isTax));//	是否为一般纳税人（0否，1是）                              
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@SellArea", model.SellArea));//	经营范围                                              
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CountryID", model.CountryID));//	国家地区（对应分类代码表定义，（人事）国家地区）      
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@Province", model.Province));//	省                                                    
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@City", model.City));//	市（县）                                                  
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@Post", model.Post));//	邮编                                                      
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@ContactName", model.ContactName));//	联系人                                            
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@Tel", model.Tel));//	电话                                                            
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@Fax", model.Fax));//	传真                                                        
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@Mobile", model.Mobile));//	手机                                                    
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@email", model.email));//	邮件                                                      
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@Addr", model.Addr));//	地址                                                      
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillType", model.BillType));//	发票类型（1增值税发票，2普通地税，3普通国税，4收据）  
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@PayType", model.PayType));//	结算方式（对应分类代码表定义，（客户）结算方式）        
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@MoneyType", model.MoneyType));//	支付方式（对应分类代码表定义，（客户）支付方式）      
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CurrencyType", model.CurrencyType));//	结算币种（财务）（对应币种代码表ID）        
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", model.Remark));//	备注                                                    
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", model.UsedStatus));//	启用状态（0停用，1启用）                            
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator", model.Creator));//	建档人ID                                                
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateDate", model.CreateDate));//	建档日期                                            
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedDate", model.ModifiedDate));//	最后更新日期                                
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", model.ModifiedUserID));//	最后更新用户ID（对应操作用户表中的UserID）

       }
       
       /// <summary>
       /// 修改其他往来单位信息
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public static bool UpdateOtherCorpInfo(OtherCorpInfoModel model)
       {
           #region SQL文拼写
           StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE officedba.OtherCorpInfo              ");	
            sql.AppendLine("     SET   BigType = @BigType                  ");        
            sql.AppendLine("      ,CustName = @CustName           ");             
            sql.AppendLine("      ,CorpNam = @CorpNam              ");            
            sql.AppendLine("      ,PYShort = @PYShort              ");            
            sql.AppendLine("      ,CustNote = @CustNote          ");              
            sql.AppendLine("      ,AreaID = @AreaID                        ");    
            sql.AppendLine("      ,CompanyType = @CompanyType      ");            
            sql.AppendLine("      ,MoneyType = @MoneyType                   ");   
            sql.AppendLine("      ,StaffCount = @StaffCount                ");    
            sql.AppendLine("      ,SetupDate = @SetupDate            ");          
            sql.AppendLine("      ,ArtiPerson = @ArtiPerson        ");            
            sql.AppendLine("      ,SetupMoney = @SetupMoney            ");        
            sql.AppendLine("      ,SetupAddress = @SetupAddress   ");             
            sql.AppendLine("      ,CapitalScale = @CapitalScale        ");        
            sql.AppendLine("      ,SaleroomY = @SaleroomY              ");        
            sql.AppendLine("      ,ProfitY = @ProfitY                  ");        
            sql.AppendLine("      ,TaxCD = @TaxCD                  ");            
            sql.AppendLine("      ,BusiNumber = @BusiNumber        ");            
            sql.AppendLine("      ,isTax = @isTax                      ");        
            sql.AppendLine("      ,SellArea = @SellArea          ");              
            sql.AppendLine("      ,CountryID = @CountryID                   ");   
            sql.AppendLine("      ,Province = @Province            ");            
            sql.AppendLine("      ,City = @City                    ");            
            sql.AppendLine("      ,Post = @Post                   ");             
            sql.AppendLine("      ,ContactName = @ContactName      ");            
            sql.AppendLine("      ,Tel = @Tel                      ");            
            sql.AppendLine("      ,Fax = @Fax                      ");            
            sql.AppendLine("      ,Mobile = @Mobile                ");            
            sql.AppendLine("      ,email = @email                  ");            
            sql.AppendLine("      ,Addr = @Addr                  ");              
            sql.AppendLine("      ,BillType = @BillType            ");            
            sql.AppendLine("      ,PayType = @PayType                       ");   
            sql.AppendLine("      ,CurrencyType = @CurrencyType             ");   
            sql.AppendLine("      ,Remark = @Remark              ");              
            sql.AppendLine("      ,UsedStatus = @UsedStatus            ");        
            sql.AppendLine("      ,Creator = @Creator                       ");   
            sql.AppendLine("      ,CreateDate = @CreateDate          ");          
            sql.AppendLine("      ,ModifiedDate = @ModifiedDate      ");          
            sql.AppendLine("      ,ModifiedUserID = @ModifiedUserID");            
            sql.AppendLine("  WHERE CompanyCD = @CompanyCD        ");
            sql.AppendLine("  AND CustNo = @CustNo      ");       
              #endregion

            //定义更新基本信息的命令  
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql.ToString();
            SetSaveParameter(comm, model);//其他参数

            //执行更新并设置更新结果
            bool result = false;
           result = SqlHelper.ExecuteTransWithCommand(comm);

            return result;

       }


       /// <summary>
       ///查询其他往来单位信息
       /// </summary>
       /// <param name="model">查询条件</param>
       /// <returns></returns>
       public static DataTable SearchRectOtherCorpInfo(OtherCorpInfoModel model, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
       {

           StringBuilder searchSql = new StringBuilder();
           searchSql.AppendLine("SELECT a.ID                                                 ");
           searchSql.AppendLine("      ,a.CustNo                                             ");
           searchSql.AppendLine("      ,a.CustName                                           ");
           searchSql.AppendLine("      ,isnull(a.AreaID,'') as AreaID                        ");
           searchSql.AppendLine("      ,isnull(a.CompanyType,'')as  CompanyType              ");
           searchSql.AppendLine("      ,isnull(a.SaleroomY,0)as SaleroomY                                          ");
           searchSql.AppendLine("      ,isnull(a.ProfitY,0)as   ProfitY                                          ");
           searchSql.AppendLine("      ,isnull(a.CapitalScale,0)as   CapitalScale                                          ");
           searchSql.AppendLine("      ,isnull(a.BusiNumber,'')as  BusiNumber                                       ");
           searchSql.AppendLine("      ,isnull(a.isTax,'') as  isTax                                            ");
           searchSql.AppendLine("      ,isnull(a.CountryID,'')as   CountryID                                        ");
           searchSql.AppendLine("      ,isnull(a.BillType,'')as BillType                                           ");
           searchSql.AppendLine("      ,isnull(a.ContactName,'')as ContactName                                           ");
           searchSql.AppendLine("      ,isnull(a.Tel,'')as Tel                                                     ");

           searchSql.AppendLine("      ,isnull(a.PayType,'') as  PayType                                           ");
           searchSql.AppendLine("      ,isnull(a.BigType,'') as  BigType                                           ");
           searchSql.AppendLine("      ,isnull(a.UsedStatus,'') as  UsedStatus                                           ");
           searchSql.AppendLine("      ,isnull(a.MoneyType,'')as   MoneyType                                        ");
           searchSql.AppendLine("      ,isnull(a.CurrencyType,'') as CurrencyType                                       ");
           searchSql.AppendLine("      ,isnull(a.Creator,'')  as Creator                                          ");
           searchSql.AppendLine("      ,isnull(b.EmployeeName,'') as   EmployeeName                                   ");
           searchSql.AppendLine("      ,isnull(c.TypeName,'') as Area                                   ");
           searchSql.AppendLine("      ,isnull(d.TypeName,'') as Pay                                    ");
           searchSql.AppendLine("      ,isnull(e.TypeName,'') as Money                                  ");
           searchSql.AppendLine("      , isnull( CONVERT(CHAR(10), a.CreateDate, 23),'') as CreateDate                ");
           searchSql.AppendLine("  FROM [officedba].[OtherCorpInfo] as a             ");
           searchSql.AppendLine("left join officedba.EmployeeInfo as b on a.Creator=b.ID and a.CompanyCD=b.CompanyCD   ");
           searchSql.AppendLine("left join officedba.CodePublicType as c on a.AreaID=c.ID and a.CompanyCD=c.CompanyCD    ");
           searchSql.AppendLine("left join officedba.CodePublicType as d on a.PayType=d.ID and a.CompanyCD=c.CompanyCD  ");
           searchSql.AppendLine("left join officedba.CodePublicType as e on a.MoneyType=e.ID and a.CompanyCD=e.CompanyCD ");
           searchSql.AppendLine("        where   a.CompanyCD=@CompanyCD                  ");
           //#endregion
          
           //定义查询的命令
           SqlCommand comm = new SqlCommand();
           ////公司代码
           comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
           //编号
           if (!string.IsNullOrEmpty(model.CustNo))
           {
               searchSql.AppendLine("	and a.CustNo LIKE @CustNo ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustNo", "%" + model.CustNo + "%"));
           }
           //名称
           if (!string.IsNullOrEmpty(model.CustName))
           {
               searchSql.AppendLine("	AND a.CustName LIKE @CustName ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustName", "%" + model.CustName + "%"));
           }
           if (!string.IsNullOrEmpty(model.BigType))
           {
               searchSql.AppendLine("	AND a.BigType = @BigType ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@BigType", model.BigType));
           }
           if (!string.IsNullOrEmpty(model.AreaID))
           {
               searchSql.AppendLine("	AND a.AreaID = @AreaID ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@AreaID", model.AreaID));
           }
           if (!string.IsNullOrEmpty(model.UsedStatus))
           {
               searchSql.AppendLine("	AND a.UsedStatus = @UsedStatus ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", model.UsedStatus));
           }
           if (!string.IsNullOrEmpty(model.isTax))
           {
               searchSql.AppendLine("	AND a.isTax = @isTax ");
               comm.Parameters.Add(SqlHelper.GetParameterFromString("@isTax", model.isTax));
           }
           //指定命令的SQL文
           comm.CommandText = searchSql.ToString();
           //执行查询
           return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
       }

       public static OtherCorpInfoModel GetOtherCorpById(int Id)
       {
           StringBuilder sql = new StringBuilder();
           sql.AppendLine("SELECT officedba.OtherCorpInfo.[ID]                               ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[CompanyCD]                        ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[BigType]                          ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[CustNo]                           ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[CustName]                         ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[CorpNam]                          ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[PYShort]                          ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[CustNote]                         ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[AreaID]                           ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[CompanyType]                      ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[MoneyType]                        ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[StaffCount]                       ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[SetupDate]                        ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[ArtiPerson]                       ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[SetupMoney]                       ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[SetupAddress]                     ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[CapitalScale]                     ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[SaleroomY]                        ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[ProfitY]                          ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[TaxCD]                            ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[BusiNumber]                       ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[isTax]                            ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[SellArea]                         ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[CountryID]                        ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[Province]                         ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[City]                             ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[Post]                             ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[ContactName]                      ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[Tel]                              ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[Fax]                              ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[Mobile]                           ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[email]                            ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[Addr]                             ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[BillType]                         ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[PayType]                          ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[CurrencyType]                     ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[Remark]                           ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[UsedStatus]                       ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[Creator]                          ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[CreateDate]                       ");
           sql.AppendLine("      ,officedba.EmployeeInfo.[EmployeeName]                   ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[ModifiedDate]                     ");
           sql.AppendLine("      ,officedba.OtherCorpInfo.[ModifiedUserID]                   ");
           sql.AppendLine("  FROM [officedba].[OtherCorpInfo]");
           sql.AppendLine("left join officedba.EmployeeInfo  on officedba.OtherCorpInfo.Creator=officedba.EmployeeInfo.ID  and officedba.OtherCorpInfo.CompanyCD=officedba.EmployeeInfo.CompanyCD  ");
           sql.AppendLine("  where officedba.OtherCorpInfo.ID=@ID  ");
           //设置参数
           SqlParameter[] param = new SqlParameter[1];
           //人员ID
           param[0] = SqlHelper.GetParameter("@ID", Id);
           //执行查询
           DataTable data = SqlHelper.ExecuteSql(sql.ToString(), param);
           //返回查询的值
           if (data == null || data.Rows.Count < 1)
           {
               //数据不存在时，返回空值
               return null;
           }
           else
           {
               //数据存在时，返回转化后的EmployeeInfoModel
               OtherCorpInfoModel model = ChangeOtherCorpInfoModel(data.Rows[0]);
           
               return model;
           }

       }



       private static OtherCorpInfoModel ChangeOtherCorpInfoModel(DataRow data)
       {
           //定义返回的 EmployeeInfoModel
           OtherCorpInfoModel model = new OtherCorpInfoModel();

           //人员信息存在时，转化为model形式的数据
           if (data != null)
           {
               model.ID = GetSafeData.ValidateDataRow_Int(data, "ID");
               model.CompanyCD = GetSafeData.ValidateDataRow_String(data, "CompanyCD");
               model.BigType = GetSafeData.ValidateDataRow_String(data, "BigType");
               model.CustNo = GetSafeData.ValidateDataRow_String(data, "CustNo");
               model.CustName = GetSafeData.ValidateDataRow_String(data, "CustName");
               model.CorpNam = GetSafeData.ValidateDataRow_String(data, "CorpNam");
               model.PYShort = GetSafeData.ValidateDataRow_String(data, "PYShort");
               model.CustNote = GetSafeData.ValidateDataRow_String(data, "CustNote");
               model.AreaID = GetSafeData.GetStringFromInt(data, "AreaID");
               model.CompanyType = GetSafeData.ValidateDataRow_String(data, "CompanyType");
               model.StaffCount = GetSafeData.GetStringFromInt(data, "StaffCount");
               model.SetupDate = GetSafeData.ValidateDataRow_String(data, "SetupDate");
               model.ArtiPerson = GetSafeData.ValidateDataRow_String(data, "ArtiPerson");
               model.SetupMoney = GetSafeData.GetStringFromDecimal(data, "SetupMoney");
               model.SetupAddress = GetSafeData.ValidateDataRow_String(data, "SetupAddress");
               model.CapitalScale = GetSafeData.GetStringFromDecimal(data, "CapitalScale");
               model.SaleroomY = GetSafeData.GetStringFromDecimal(data, "SaleroomY");
               model.ProfitY = GetSafeData.GetStringFromDecimal(data, "ProfitY");
               model.TaxCD = GetSafeData.ValidateDataRow_String(data, "TaxCD");
               model.BusiNumber = GetSafeData.ValidateDataRow_String(data, "BusiNumber");
               model.isTax = GetSafeData.ValidateDataRow_String(data, "isTax");
               model.SellArea = GetSafeData.ValidateDataRow_String(data, "SellArea");
               model.CountryID = GetSafeData.GetStringFromInt(data, "CountryID");
               model.Province = GetSafeData.ValidateDataRow_String(data, "Province");
               model.City = GetSafeData.ValidateDataRow_String(data, "City");
               model.Post = GetSafeData.ValidateDataRow_String(data, "Post");
               model.ContactName = GetSafeData.ValidateDataRow_String(data, "ContactName");
               model.Tel = GetSafeData.ValidateDataRow_String(data, "Tel");
               model.Fax = GetSafeData.ValidateDataRow_String(data, "Fax");
               model.Mobile = GetSafeData.ValidateDataRow_String(data, "Mobile");
               model.email = GetSafeData.ValidateDataRow_String(data, "email");
               model.Addr = GetSafeData.ValidateDataRow_String(data, "Addr");
               model.BillType = GetSafeData.ValidateDataRow_String(data, "BillType");
               model.PayType = GetSafeData.GetStringFromInt(data, "PayType");
               model.MoneyType = GetSafeData.GetStringFromInt(data, "MoneyType");
               model.CurrencyType = GetSafeData.GetStringFromInt(data, "CurrencyType");
               model.Remark = GetSafeData.ValidateDataRow_String(data, "Remark");
               model.UsedStatus = GetSafeData.ValidateDataRow_String(data, "UsedStatus");
               model.Creator = GetSafeData.GetStringFromInt(data, "Creator");
               model.CreateDate = GetSafeData.GetStringFromDateTime(data, "CreateDate","yyyy-MM-dd");
               model.ModifiedDate = GetSafeData.GetStringFromDateTime(data, "ModifiedDate","yyyy-MM-dd");
               model.ModifiedUserID = GetSafeData.ValidateDataRow_String(data, "ModifiedUserID");
               model.EmployeeName = GetSafeData.ValidateDataRow_String(data, "EmployeeName");

           }

           return model;
       }


       /// <summary>
       /// 删除往来单位信息
       /// </summary>
       /// <param name="TypeFlag"></param>
       /// <param name="CompanyCD"></param>
       /// <returns></returns>
       public static bool DeleteOtherCorpInfo(string ID, string CompanyCD)
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

               Delsql[0] = "delete from  officedba.OtherCorpInfo where ID IN (" + allID + ") and CompanyCD = @CompanyCD";

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


    }
}

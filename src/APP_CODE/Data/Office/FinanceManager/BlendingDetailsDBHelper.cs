/**********************************************
 * 类作用：   勾兑明细表数据库层处理
 * 建立人：   莫申林
 * 建立时间： 2009/06/27
 ***********************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Text;
using XBase.Model.Office.FinanceManager;
using XBase.Common;
using System.Collections;
using XBase.Data.Office.FinanceManager;
using XBase.Data.Office.SellManager;
using XBase.Data.Office.PurchaseManager;

namespace XBase.Data.Office.FinanceManager
{

   public class BlendingDetailsDBHelper
   {

       #region 默认构造函数
       /// <summary>
       /// 默认构造函数
       /// </summary>
       public BlendingDetailsDBHelper()
       {

       }
       #endregion

       #region 添加勾兑明细
       /// <summary>
       /// 添加勾兑明细
       /// </summary>
       /// <param name="MyList"></param>
       /// <returns></returns>
       public bool InSertBlendingDetails(ArrayList MyList,out string ListID)
       {
           try
           {
               ListID = string.Empty;
               int rev = 0;
               for (int i = 0; i < MyList.Count; i++)
               {
                   StringBuilder strSql = new StringBuilder();
                   strSql.Append("insert into Officedba.BlendingDetails(");
                   strSql.Append("CompanyCD,PayOrInComeType,BillingID,SourceDt,SourceID,BillCD,BillingType,InvoiceType,TotalPrice,YAccounts,NAccounts,CreateDate,ContactUnits,ExecutDate,Executor,Status,CurrencyType,CurrencyRate)");
                   strSql.Append(" values (");
                   strSql.Append("@CompanyCD" + i + ",@PayOrInComeType" + i + ",@BillingID" + i + ",@SourceDt" + i + ",@SourceID" + i + ",@BillCD" + i + ",@BillingType" + i + ",@InvoiceType" + i + ",@TotalPrice" + i + ",@YAccounts" + i + ",@NAccounts" + i + ",@CreateDate" + i + ",@ContactUnits" + i + ",@ExecutDate" + i + ",@Executor" + i + ",@Status" + i + ",@CurrencyType" + i + ",@CurrencyRate" + i + " )");
                   strSql.Append("set @IntID" + i + "= @@IDENTITY");

                   SqlParameter[] parms = new SqlParameter[19];
                   parms[0] = SqlHelper.GetParameter("@CompanyCD"+i+"",(MyList[i] as BlendingDetailsModel).CompanyCD);
                   parms[1] = SqlHelper.GetParameter("@PayOrInComeType"+i+"",(MyList[i] as BlendingDetailsModel).PayOrInComeType);
                   parms[2] = SqlHelper.GetParameter("@BillingID"+i+"",(MyList[i] as BlendingDetailsModel).BillingID);
                   parms[3] = SqlHelper.GetParameter("@SourceDt"+i+"",(MyList[i] as BlendingDetailsModel).SourceDt);
                   parms[4] = SqlHelper.GetParameter("@SourceID"+i+"",(MyList[i] as BlendingDetailsModel).SourceID);
                   parms[5] = SqlHelper.GetParameter("@BillCD"+i+"",(MyList[i] as BlendingDetailsModel).BillCD);
                   parms[6] = SqlHelper.GetParameter("@BillingType"+i+"",(MyList[i] as BlendingDetailsModel).BillingType);
                   parms[7] = SqlHelper.GetParameter("@InvoiceType"+i+"",(MyList[i] as BlendingDetailsModel).InvoiceType);
                   parms[8] = SqlHelper.GetParameter("@TotalPrice"+i+"",(MyList[i] as BlendingDetailsModel).TotalPrice);
                   parms[9] = SqlHelper.GetParameter("@YAccounts"+i+"",(MyList[i] as BlendingDetailsModel).YAccounts);
                   parms[10] = SqlHelper.GetParameter("@NAccounts"+i+"",(MyList[i] as BlendingDetailsModel).NAccounts);
                   parms[11] = SqlHelper.GetParameter("@CreateDate"+i+"",(MyList[i] as BlendingDetailsModel).CreateDate);
                   parms[12] = SqlHelper.GetParameter("@ContactUnits"+i+"",(MyList[i] as BlendingDetailsModel).ContactUnits);
                   parms[13] = SqlHelper.GetParameter("@ExecutDate"+i+"",(MyList[i] as BlendingDetailsModel).ExecutDate);
                   parms[14] = SqlHelper.GetParameter("@Executor"+i+"",(MyList[i] as BlendingDetailsModel).Executor);
                   parms[15] = SqlHelper.GetParameter("@Status"+i+"",(MyList[i] as BlendingDetailsModel).Status);
                   parms[16] = SqlHelper.GetParameter("@CurrencyType" + i + "", (MyList[i] as BlendingDetailsModel).CurrencyType);
                   parms[17] = SqlHelper.GetParameter("@CurrencyRate" + i + "", (MyList[i] as BlendingDetailsModel).CurrencyRate);
                   parms[18] = SqlHelper.GetOutputParameter("@IntID"+i+"", SqlDbType.Int);

                   SqlHelper.ExecuteTransSql(strSql.ToString(), parms);
                   rev += SqlHelper.Result.OprateCount;
                   ListID += parms[18].Value+ ",";
               }
               ListID = ListID.TrimEnd(new char[] { ',' });
               return  rev> 0 ? true : false;
           }
           catch (Exception ex)
           {
               throw ex;
           }


       }
       #endregion

       #region 修改勾兑明细表
       /// <summary>
       /// 修改勾兑明细表
       /// </summary>
       /// <param name="MyList"></param>
       /// <returns></returns>
       public bool UpdateBalendingDetails(ArrayList MyList,out string ListID)
       {
           try
           {
               ListID = string.Empty;
               int rev = 0;
               if (MyList.Count > 0)
               {
                   for (int i = 0; i < MyList.Count; i++)
                   {
                       ListID += (MyList[i] as BlendingDetailsModel).ID.ToString() + ",";
                       StringBuilder strSql = new StringBuilder();
                       strSql.Append("update  Officedba.BlendingDetails ");
                       strSql.Append("set YAccounts=@YAccounts" + i + ",NAccounts=@NAccounts" + i + ",CreateDate=@CreateDate" + i + ",ExecutDate=@ExecutDate" + i + ",Executor=@Executor" + i + ",Status=@Status" + i + " ");
                       strSql.Append(" where ID=@ID" + i + "");

                       SqlParameter[] parms = 
                    {
                        
                         new SqlParameter("@YAccounts"+i+"",(MyList[i] as BlendingDetailsModel).YAccounts),
                         new SqlParameter("@NAccounts"+i+"",(MyList[i] as BlendingDetailsModel).NAccounts),
                         new SqlParameter("@CreateDate"+i+"",(MyList[i] as BlendingDetailsModel).CreateDate),
                         new SqlParameter("@ExecutDate"+i+"",(MyList[i] as BlendingDetailsModel).ExecutDate),
                         new SqlParameter("@Executor"+i+"",(MyList[i] as BlendingDetailsModel).Executor),
                         new SqlParameter("@Status"+i+"",(MyList[i] as BlendingDetailsModel).Status),
                         new SqlParameter("@ID"+i+"",(MyList[i] as BlendingDetailsModel).ID),
                    };
                       SqlHelper.ExecuteTransSql(strSql.ToString(), parms);
                       rev += SqlHelper.Result.OprateCount;

                   }
                   ListID = ListID.TrimEnd(new char[] { ',' });
               }
               return rev>0?true:false;
           }
           catch (Exception ex)
           {
               throw ex;

           }
       }
       #endregion

       #region 获取勾兑明细
       /// <summary>
       /// 获取勾兑明细
       /// </summary>
       /// <param name="CompanyCD">公司编码</param>
       /// <param name="PayOrInComeType">来源表类别</param>
       /// <param name="BillingID">来源表ID主键</param>
       /// <returns></returns>
       public DataTable GetBlendingSourse(string CompanyCD, string BillingID,string PayOrInComeType)
       {
           StringBuilder sqlStr = new StringBuilder();
           sqlStr.AppendLine("SELECT a.ID,a.CompanyCD,a.PayOrInComeType,a.BillingID,");
           sqlStr.AppendLine(" a.SourceDt,a.SourceID,a.BillCD,a.BillingType,a.InvoiceType,a.TotalPrice,");
           sqlStr.AppendLine("a.YAccounts,a.NAccounts,CONVERT(VARCHAR(10),a.CreateDate,21) as CreateDate ,a.ContactUnits,a.ExecutDate,");
           sqlStr.AppendLine("a.Executor,a.Status,isnull(a.CurrencyType,0) as CurrencyType,isnull(a.CurrencyRate,1) as Rate ,isnull(b.CurrencyName,'') as CurrencyName  FROM officedba.BlendingDetails a left outer join officedba.CurrencyTypeSetting b on a.CurrencyType=b.ID  ");
           sqlStr.AppendLine(" where a.BillingID=@BillingID ");
           sqlStr.AppendLine(" and a.CompanyCD=@CompanyCD");
           sqlStr.AppendLine(" and a.PayOrInComeType=@PayOrInComeType ");
           SqlParameter[] parms = 
                    {
                         new SqlParameter("@BillingID",BillingID),
                         new SqlParameter("@CompanyCD",CompanyCD),
                         new SqlParameter("@PayOrInComeType",PayOrInComeType),
                    };
           return SqlHelper.ExecuteSql(sqlStr.ToString(),parms);
           
       }
       #endregion

       #region  获取业务单对应的源单的详细信息
       /// <summary>
       /// 获取业务单对应的源单的详细信息
       /// </summary>
       /// <param name="BillingID">业务单主键ID</param>
       /// <returns></returns>
       public DataTable GetSourceDt(int BillingID, string CompanyCD,string PayOrIncomeType)
       {

           try
           {
               DataTable returnDT = new DataTable();
               
               if (IsExist(BillingID,CompanyCD,PayOrIncomeType))
               {
                   returnDT = GetBlendingSourse(CompanyCD, BillingID.ToString(),PayOrIncomeType);
               }
               else
               {
                   string sql = @"select a.SourceID,a.SourceDt,a.BillingType,a.InvoiceType,isnull(a.CurrencyType,0) as CurrencyType,isnull(a.CurrencyRate,1) as Rate ,isnull(b.CurrencyName,'') as CurrencyName from officedba.Billing a left outer join officedba.CurrencyTypeSetting b on a.CurrencyType=b.ID where a.ID=@ID and b.CompanyCD=@CompanyCD and a.CompanyCD=@CompanyCDD ";
                   SqlParameter[] parms = 
                    {
                         new SqlParameter("@ID",BillingID),
                         new SqlParameter("@CompanyCD",CompanyCD),
                         new SqlParameter("@CompanyCDD",CompanyCD)
                    };
                   DataTable dt = SqlHelper.ExecuteSql(sql, parms);
                   DataTable RevDt = new DataTable();
                   
                   string ids = string.Empty;
                   string Fromtable = string.Empty;
                   string BillingType = string.Empty;
                   string InvoiceType = string.Empty;
                   string CurrencyName = string.Empty;
                   string Rate = string.Empty;
                   string CurrencyType = string.Empty;
                   returnDT.Columns.Add("SourceDt");//源单表
                   returnDT.Columns.Add("SourceID");//源单ID
                   returnDT.Columns.Add("BillCD");//源单编码
                   returnDT.Columns.Add("BillingType");//源单类别
                   returnDT.Columns.Add("InvoiceType");//发票类别
                   returnDT.Columns.Add("TotalPrice");//源单总金额
                   returnDT.Columns.Add("ContactUnits");//往来客户
                   returnDT.Columns.Add("CurrencyType");
                   returnDT.Columns.Add("Rate");
                   returnDT.Columns.Add("CurrencyName");

                   if (dt!=null&&dt.Rows.Count > 0)
                   {
                       ids = dt.Rows[0]["SourceID"].ToString();
                       Fromtable = dt.Rows[0]["SourceDt"].ToString().Trim();
                       BillingType = dt.Rows[0]["BillingType"].ToString();
                       InvoiceType = dt.Rows[0]["InvoiceType"].ToString();
                       CurrencyName = dt.Rows[0]["CurrencyName"].ToString();
                       Rate = dt.Rows[0]["Rate"].ToString();
                       CurrencyType = dt.Rows[0]["CurrencyType"].ToString();

                       switch (Fromtable)
                       {
                           case "officedba.SellOrder":
                               RevDt = SellOrderDBHelper.SearchOrderByCondition(ids, CompanyCD);
                               for (int i = 0; i < RevDt.Rows.Count; i++)
                               {
                                   DataRow row = returnDT.NewRow();
                                   row["SourceDt"] = Fromtable;
                                   row["SourceID"] = RevDt.Rows[i]["ID"].ToString();
                                   row["BillCD"] = RevDt.Rows[i]["OrderNo"].ToString();
                                   row["BillingType"] = BillingType;
                                   row["InvoiceType"] = InvoiceType;
                                   row["TotalPrice"] = RevDt.Rows[i]["RealTotal"].ToString();
                                   row["ContactUnits"] = RevDt.Rows[i]["CustName"].ToString();
                                   row["CurrencyName"] = CurrencyName;
                                   row["CurrencyType"] = CurrencyType;
                                   row["Rate"] = Rate;
                                   returnDT.Rows.Add(row);
                               }
                               break;
                           case "officedba.PurchaseOrder":
                               RevDt = PurchaseOrderDBHelper.SearchOrderByCondition(ids, CompanyCD);
                               for (int i = 0; i < RevDt.Rows.Count; i++)
                               {
                                   DataRow row = returnDT.NewRow();
                                   row["SourceDt"] = Fromtable;
                                   row["SourceID"] = RevDt.Rows[i]["ID"].ToString();
                                   row["BillCD"] = RevDt.Rows[i]["OrderNo"].ToString();
                                   row["BillingType"] = BillingType;
                                   row["InvoiceType"] = InvoiceType;
                                   row["TotalPrice"] = RevDt.Rows[i]["RealTotal"].ToString();
                                   row["ContactUnits"] = RevDt.Rows[i]["CustName"].ToString();
                                   row["CurrencyName"] = CurrencyName;
                                   row["CurrencyType"] = CurrencyType;
                                   row["Rate"] = Rate;
                                   returnDT.Rows.Add(row);
                               }
                               break;
                           case "officedba.SellBack":
                               RevDt = BillingDBHelper.SellBackInfo(ids, CompanyCD);
                               for (int i = 0; i < RevDt.Rows.Count; i++)
                               {
                                   DataRow row = returnDT.NewRow();
                                   row["SourceDt"] = Fromtable;
                                   row["SourceID"] = RevDt.Rows[i]["ID"].ToString();
                                   row["BillCD"] = RevDt.Rows[i]["BackNo"].ToString();
                                   row["BillingType"] = BillingType;
                                   row["InvoiceType"] = InvoiceType;
                                   row["TotalPrice"] = RevDt.Rows[i]["RealTotal"].ToString();
                                   row["ContactUnits"] = RevDt.Rows[i]["CustName"].ToString();
                                   row["CurrencyName"] = CurrencyName;
                                   row["CurrencyType"] = CurrencyType;
                                   row["Rate"] = Rate;
                                   returnDT.Rows.Add(row);
                               }
                               break;
                           case "officedba.SellChannelSttl":
                               RevDt = BillingDBHelper.GetSellChannelSttlInfo(ids, CompanyCD);
                               for (int i = 0; i < RevDt.Rows.Count; i++)
                               {
                                   DataRow row = returnDT.NewRow();
                                   row["SourceDt"] = Fromtable;
                                   row["SourceID"] = RevDt.Rows[i]["ID"].ToString();
                                   row["BillCD"] = RevDt.Rows[i]["SttlNo"].ToString();
                                   row["BillingType"] = BillingType;
                                   row["InvoiceType"] = InvoiceType;
                                   row["TotalPrice"] = RevDt.Rows[i]["SttlTotal"].ToString();
                                   row["ContactUnits"] = RevDt.Rows[i]["CustName"].ToString();
                                   row["CurrencyName"] = CurrencyName;
                                   row["CurrencyType"] = CurrencyType;
                                   row["Rate"] = Rate;
                                   returnDT.Rows.Add(row);
                               }
                               break;
                           case "officedba.PurchaseReject":
                               RevDt = BillingDBHelper.GetPurchaseRejectInfo(ids, CompanyCD);
                               for (int i = 0; i < RevDt.Rows.Count; i++)
                               {
                                   DataRow row = returnDT.NewRow();
                                   row["SourceDt"] = Fromtable;
                                   row["SourceID"] = RevDt.Rows[i]["ID"].ToString();
                                   row["BillCD"] = RevDt.Rows[i]["OrderNO"].ToString();
                                   row["BillingType"] = BillingType;
                                   row["InvoiceType"] = InvoiceType;
                                   row["TotalPrice"] = RevDt.Rows[i]["RealTotal"].ToString();
                                   row["ContactUnits"] = RevDt.Rows[i]["CustName"].ToString();
                                   row["CurrencyName"] = CurrencyName;
                                   row["CurrencyType"] = CurrencyType;
                                   row["Rate"] = Rate;
                                   returnDT.Rows.Add(row);
                               }
                               break;
                           case "officedba.PurchaseArrive":
                               RevDt = BillingDBHelper.GetPurchaseIncomeInfo(ids, CompanyCD);
                               for (int i = 0; i < RevDt.Rows.Count; i++)
                               {
                                   DataRow row = returnDT.NewRow();
                                   row["SourceDt"] = Fromtable;
                                   row["SourceID"] = RevDt.Rows[i]["ID"].ToString();
                                   row["BillCD"] = RevDt.Rows[i]["OrderNO"].ToString();
                                   row["BillingType"] = BillingType;
                                   row["InvoiceType"] = InvoiceType;
                                   row["TotalPrice"] = RevDt.Rows[i]["RealTotal"].ToString();
                                   row["ContactUnits"] = RevDt.Rows[i]["CustName"].ToString();
                                   row["CurrencyName"] = CurrencyName;
                                   row["CurrencyType"] = CurrencyType;
                                   row["Rate"] = Rate;
                                   returnDT.Rows.Add(row);
                               }
                               break;
                           case "officedba.SellSend":
                               RevDt = BillingDBHelper.GetSellSendInfo(ids, CompanyCD);
                               for (int i = 0; i < RevDt.Rows.Count; i++)
                               {
                                   DataRow row = returnDT.NewRow();
                                   row["SourceDt"] = Fromtable;
                                   row["SourceID"] = RevDt.Rows[i]["ID"].ToString();
                                   row["BillCD"] = RevDt.Rows[i]["OrderNO"].ToString();
                                   row["BillingType"] = BillingType;
                                   row["InvoiceType"] = InvoiceType;
                                   row["TotalPrice"] = RevDt.Rows[i]["RealTotal"].ToString();
                                   row["ContactUnits"] = RevDt.Rows[i]["CustName"].ToString();
                                   row["CurrencyName"] = CurrencyName;
                                   row["CurrencyType"] = CurrencyType;
                                   row["Rate"] = Rate;
                                   returnDT.Rows.Add(row);
                               }
                               break;

                           default:
                               break;
                       }




                   }
               }
               return returnDT;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       #endregion

       #region 判读勾兑明细表中是否存在记录
       /// <summary>
       /// 判读勾兑明细表中是否存在记录
       /// </summary>
       /// <param name="BillingID">业务单ID</param>
       /// <param name="CompanyCD">公司编码</param>
       /// <returns></returns>
       public bool IsExist(int BillingID, string CompanyCD,string PayOrIncomeType)
       {
           int nev = 0;
           string nevSql = "select count(ID) from officedba.BlendingDetails where BillingID=@BillingID and CompanyCD=@CompanyCD and PayOrIncomeType=@PayOrIncomeType";
           SqlParameter[] parmss = 
                {
                     new SqlParameter("@BillingID",BillingID),
                     new SqlParameter("@CompanyCD",CompanyCD),
                     new SqlParameter("@PayOrIncomeType",PayOrIncomeType),
                };
           object obj = SqlHelper.ExecuteScalar(nevSql, parmss);
           if (obj != null)
           {
               nev = Convert.ToInt32(obj);
           }
           return nev > 0 ? true : false;
       }
       #endregion

       #region 修改时获取勾兑明细记录
       /// <summary>
       /// 修改时获取勾兑明细记录
       /// </summary>
       /// <param name="BillingID">业务单ID</param>
       /// <param name="PayOrIncomeType">收付款单类别</param>
       /// <param name="PayOrInComeID">收付款单ID</param>
       /// <param name="CompanyCD">公司编码</param>
       /// <returns></returns>
       public DataTable GetEditBlendingSource(int BillingID, string PayOrIncomeType,string CompanyCD,int PayOrInComeID)
       {
           try
           {
               DataTable dt = GetBlendingSourse(CompanyCD, BillingID.ToString(), PayOrIncomeType);
               DataTable SourceDt = dt.Clone();
               SourceDt.Clear();
               SourceDt.Columns.Add("ThisAccounts");
               foreach (DataRow dr in dt.Rows)
               {
                   DataRow row = SourceDt.NewRow();
                   row["ID"] = dr["ID"].ToString();
                   row["PayOrInComeType"] = dr["PayOrInComeType"].ToString();
                   row["BillingID"] = dr["BillingID"].ToString();
                   row["SourceDt"] = dr["SourceDt"].ToString();
                   row["SourceID"] = dr["SourceID"].ToString();
                   row["BillCD"] = dr["BillCD"].ToString();
                   row["BillingType"] = dr["BillingType"].ToString();
                   row["InvoiceType"] = dr["InvoiceType"].ToString();
                   row["TotalPrice"] = dr["TotalPrice"].ToString();
                   row["CreateDate"] = dr["CreateDate"].ToString();
                   row["ContactUnits"] = dr["ContactUnits"].ToString();
                   row["ExecutDate"] = dr["ExecutDate"].ToString();
                   row["Executor"] = dr["Executor"].ToString();
                   row["CurrencyName"] = dr["CurrencyName"].ToString();
                   row["Rate"] = dr["Rate"].ToString();
                   row["CurrencyType"] = dr["CurrencyType"].ToString();
                   DataTable steps = StepsDetailsDBHelper.GetStepsDetails(PayOrInComeID, int.Parse(dr["ID"].ToString()), CompanyCD, PayOrIncomeType);
                   if (steps.Rows.Count > 0)
                   {
                       row["YAccounts"] = Convert.ToDecimal(dr["YAccounts"].ToString()) - Convert.ToDecimal(steps.Rows[0]["BlendingAmount"].ToString());
                       row["NAccounts"] = Convert.ToDecimal(dr["NAccounts"].ToString()) + Convert.ToDecimal(steps.Rows[0]["BlendingAmount"].ToString());
                       row["ThisAccounts"] = steps.Rows[0]["BlendingAmount"].ToString();

                   }
                   SourceDt.Rows.Add(row);

               }
               return SourceDt;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       #endregion

        #region 修改收付款单，回滚当前单据勾兑的金额
       /// <summary>
       /// 修改时获取勾兑明细记录
       /// </summary>
       /// <param name="BillingID">业务单ID</param>
       /// <param name="PayOrIncomeType">收付款单类别</param>
       /// <param name="PayOrInComeID">收付款单ID</param>
       /// <param name="CompanyCD">公司编码</param>
       /// <returns></returns>
       public bool UpdateEditBlending(int BillingID, string PayOrIncomeType, string CompanyCD, int PayOrInComeID)
       {
           try
           {
               int rev = 0;
               DataTable dt = GetBlendingSourse(CompanyCD, BillingID.ToString(), PayOrIncomeType);
               foreach (DataRow dr in dt.Rows)
               {
                   
                   decimal YAccounts = 0;
                   decimal NAccounts = 0;
                   DataTable steps = StepsDetailsDBHelper.GetStepsDetails(PayOrInComeID, int.Parse(dr["ID"].ToString()), CompanyCD, PayOrIncomeType);
                   if (steps.Rows.Count > 0)
                   {
                       YAccounts = Convert.ToDecimal(dr["YAccounts"].ToString()) - Convert.ToDecimal(steps.Rows[0]["BlendingAmount"].ToString());
                       NAccounts = Convert.ToDecimal(dr["NAccounts"].ToString()) + Convert.ToDecimal(steps.Rows[0]["BlendingAmount"].ToString());
                       string sql ="Update officedba.BlendingDetails set YAccounts=@YAccounts,NAccounts=@NAccounts where ID=@ID";
                       SqlParameter[] parms = 
                        {
                             new SqlParameter("@YAccounts",YAccounts),
                             new SqlParameter("@NAccounts",NAccounts),
                             new SqlParameter("@ID",dr["ID"].ToString()),
                        };

                       SqlHelper.ExecuteTransSql(sql, parms);
                       rev += SqlHelper.Result.OprateCount;
                   }

               }
               return rev > 0 ? true : false;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
        #endregion


        #region 删除勾兑明细
       /// <summary>
       /// 删除勾兑明细
       /// </summary>
       /// <param name="CompanyCD">公司编码</param>
       /// <param name="PayOrInComeType">收付款类别</param>
       /// <param name="BillingID">业务单ID主键</param>
       /// <returns></returns>
       public bool DeleteBlendingDetails(string CompanyCD, string BillingID, string PayOrInComeType)
       {
           try
           {
               string sql = "delete from officedba.BlendingDetails where BillingID=@BillingID and CompanyCD=@CompanyCD and PayOrIncomeType=@PayOrIncomeType";
               SqlParameter[] parmss = 
                {
                     new SqlParameter("@BillingID",BillingID),
                     new SqlParameter("@CompanyCD",CompanyCD),
                     new SqlParameter("@PayOrIncomeType",PayOrInComeType),
                };
               SqlHelper.ExecuteTransSql(sql, parmss);
               return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch (Exception ex)
           {
               throw ex;
           }
          
       }
        #endregion

       #region  根据凭证主表的来源表及来源表主键__获取对应的勾兑明细信息
       /// <summary>
       /// 根据凭证主表的来源表及来源表主键__获取对应的勾兑明细信息
       /// </summary>
       /// <param name="FormTBName">来源表</param>
       /// <param name="FileValue">来源表主键集</param>
       /// <param name="CompanyCD">公司编码</param>
       /// <returns></returns>
       public DataTable GetBlendingSoureByTB(string FormTBName, string FileValue, string CompanyCD)
       {
           try
           {
               string PayOrIncomeType = string.Empty;
               switch (FormTBName)
               {
                   case "officedba.PayBill":
                       PayOrIncomeType = "1";
                       break;
                   case "officedba.IncomeBill":
                       PayOrIncomeType = "2";
                       break;
                   default:
                       break;
               }
               string sql = string.Format("select a.BillCD,a.BillingType,a.TotalPrice,a.YAccounts,b.BlendingAmount,CONVERT(VARCHAR(10),a.CreateDate,21) as CreateDate from officedba.BlendingDetails a left outer join officedba.StepsDetails b on a.ID=b.BlendingID WHERE a.PayOrInComeType='{0}' and b.PayOrInComeType='{0}' and a.CompanyCD='{1}' and b.CompanyCD='{1}' and  b.SourceID in ( {2} ) order by BillCD desc", PayOrIncomeType, CompanyCD, FileValue);
               DataTable dt=SqlHelper.ExecuteSql(sql);
               if (dt.Rows.Count > 0)
               {
                   DataRow row = dt.NewRow();
                   row["BillCD"] = "合计";
                   row["BillingType"] = "";
                   decimal TotalPrice = 0;
                   decimal BlendingAmount = 0;
                   string BillCD = string.Empty;
                   foreach (DataRow dr in dt.Rows)
                   {
                       string BillCDStr = dr["BillCD"].ToString();
                       if (!BillCD.Contains(BillCDStr))
                       {
                           TotalPrice += Convert.ToDecimal(dr["TotalPrice"].ToString()); 
                       }
                       BlendingAmount += Convert.ToDecimal(dr["BlendingAmount"].ToString());
                       BillCD += BillCDStr;
                   }
                   row["TotalPrice"] = TotalPrice;
                   row["YAccounts"] = BlendingAmount;
                   row["BlendingAmount"] = BlendingAmount;
                   row["CreateDate"] = "";
                   dt.Rows.Add(row);
               }
               return dt;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       #endregion




   }
}

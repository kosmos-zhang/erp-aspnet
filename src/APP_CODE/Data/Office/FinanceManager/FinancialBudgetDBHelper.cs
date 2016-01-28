/**********************************************
 * 类作用：   财务预算管理
 * 建立人：   lysong
 * 建立时间： 2009/09/14
 ***********************************************/
using System;
using XBase.Model.Office.FinanceManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using XBase.Common;
using XBase.Data.Common;

namespace XBase.Data.Office.FinanceManager
{
    /// <summary>
    /// 类名：FinancialBudgetDBHelper
    /// 描述：财务预算管理
    /// 作者：lysong
    /// 创建时间：2009/09/14
    /// </summary>
   public class FinancialBudgetDBHelper
    {
        #region 添加财务预算单信息
        /// <summary>
        /// 添加财务预算单信息
        /// </summary>
        /// <param name="FinacialBudget_Model">预算单主信息</param>
        /// <param name="BudgetInfo">预算单详细信息</param>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool InserFinacialBudgerInfo(FinancialBudgetModel FinacialBudget_Model, string BudgetInfo, string userID, out int RetValID)
        {
            try
            {
                #region 添加财务预算主表信息sql语句
                StringBuilder BudgetSql = new StringBuilder();
                BudgetSql.AppendLine("INSERT INTO officedba.Financialbudgetbill");
                BudgetSql.AppendLine("(CompanyCD");
                BudgetSql.AppendLine(",BudgetCD     ");
                BudgetSql.AppendLine(",Title");
                BudgetSql.AppendLine(",DeptID   ");
                BudgetSql.AppendLine(",StartDate  ");
                BudgetSql.AppendLine(",EndDate    ");
                BudgetSql.AppendLine(",Billstatus    ");
                BudgetSql.AppendLine(",Creator    ");
                BudgetSql.AppendLine(",CreateDate    ");
                BudgetSql.AppendLine(",PayType");
                BudgetSql.AppendLine(",CurrencyType");
                BudgetSql.AppendLine(",CurrencyRate");
                BudgetSql.AppendLine(",Budgetcost");
                BudgetSql.AppendLine(",FinancialBudgetType");
                BudgetSql.AppendLine(",Remark");
                BudgetSql.AppendLine(",ModifiedUserID");
                BudgetSql.AppendLine(",ModifiedDate)");
                BudgetSql.AppendLine(" values ");
                BudgetSql.AppendLine("(@CompanyCD");
                BudgetSql.AppendLine(",@BudgetCD     ");
                BudgetSql.AppendLine(",@Title");
                BudgetSql.AppendLine(",@DeptID   ");
                BudgetSql.AppendLine(",@StartDate  ");
                BudgetSql.AppendLine(",@EndDate    ");
                BudgetSql.AppendLine(",@Billstatus    ");
                BudgetSql.AppendLine(",@Creator    ");
                BudgetSql.AppendLine(",@CreateDate    ");
                BudgetSql.AppendLine(",@PayType");
                BudgetSql.AppendLine(",@CurrencyType");
                BudgetSql.AppendLine(",@CurrencyRate");
                BudgetSql.AppendLine(",@Budgetcost");
                BudgetSql.AppendLine(",@FinancialBudgetType");
                BudgetSql.AppendLine(",@Remark");
                BudgetSql.AppendLine(",@ModifiedUserID");
                BudgetSql.AppendLine(",@ModifiedDate)");
                BudgetSql.AppendLine("set @ID=@@IDENTITY");
                #endregion

                #region 设置参数
                SqlParameter[] paramgas = new SqlParameter[18];
                paramgas[0] = SqlHelper.GetParameter("@CompanyCD", FinacialBudget_Model.CompanyCD);
                paramgas[1] = SqlHelper.GetParameter("@BudgetCD", FinacialBudget_Model.BudgetCD);
                paramgas[2] = SqlHelper.GetParameter("@Title", FinacialBudget_Model.Title);
                paramgas[3] = SqlHelper.GetParameter("@DeptID", FinacialBudget_Model.DeptID);
                paramgas[4] = SqlHelper.GetParameter("@StartDate", FinacialBudget_Model.StartDate);
                paramgas[5] = SqlHelper.GetParameter("@EndDate", FinacialBudget_Model.EndDate);
                paramgas[6] = SqlHelper.GetParameter("@Billstatus", FinacialBudget_Model.Billstatus);
                paramgas[7] = SqlHelper.GetParameter("@Creator", FinacialBudget_Model.Creator);
                paramgas[8] = SqlHelper.GetParameter("@CreateDate", FinacialBudget_Model.CreateDate);
                paramgas[9] = SqlHelper.GetParameter("@PayType", FinacialBudget_Model.PayType);
                paramgas[10] = SqlHelper.GetParameter("@CurrencyType", FinacialBudget_Model.CurrencyType);
                paramgas[11] = SqlHelper.GetParameter("@CurrencyRate", FinacialBudget_Model.CurrencyRate);
                paramgas[12] = SqlHelper.GetParameter("@Budgetcost", FinacialBudget_Model.Budgetcost);
                paramgas[13] = SqlHelper.GetParameter("@Remark", FinacialBudget_Model.Remark);
                paramgas[14] = SqlHelper.GetParameter("@ModifiedUserID", userID);
                paramgas[15] = SqlHelper.GetParameter("@ModifiedDate", System.DateTime.Now);
                paramgas[16] = SqlHelper.GetParameter("@FinancialBudgetType", FinacialBudget_Model.FinancialBudgetType);
                paramgas[17] = SqlHelper.GetOutputParameter("@ID", SqlDbType.Int);
                #endregion
                return InsertAll(BudgetSql.ToString(), BudgetInfo, FinacialBudget_Model.BudgetCD, FinacialBudget_Model.CompanyCD, paramgas,out RetValID);
            }
            catch
            {
                RetValID = 0;
                return false;
            }
        }

        /// <summary>
        /// 添加财务预算单信息
        /// </summary>
        /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool InsertAll(string BudgetSql, string BudgetInfo, string BudgetNo,string CompanyCD,SqlParameter[] paramgas,out int RetValID)
        {
            try
            {
                string[] BudgetInfoArrary = BudgetInfo.Split('|');
                SqlCommand[] comms = new SqlCommand[BudgetInfoArrary.Length]; //申明cmd数组(主表，明细表，每条明细生成一条库存记录)
                SqlCommand BudgetInfoCom = new SqlCommand(BudgetSql.ToString());
                BudgetInfoCom.Parameters.AddRange(paramgas);
                comms[0] = BudgetInfoCom;

                string recorditems = "";
                string[] gasfield = null;

                for (int i = 1; i < BudgetInfoArrary.Length; i++) //循环数组
                {
                    recorditems = BudgetInfoArrary[i].ToString();
                    gasfield = recorditems.Split(',');

                    string DeptID = gasfield[0].ToString();//所属部门
                    string FeeType = gasfield[1].ToString();//费用类型
                    string PlanFee = gasfield[2].ToString();//预算费用
                    string Remark = gasfield[3].ToString();//备注


                    #region 拼写添加入库明细信息sql语句
                    StringBuilder BudgetDetailSql = new StringBuilder();
                    BudgetDetailSql.AppendLine("INSERT INTO officedba.Financialbudgetdetails");
                    BudgetDetailSql.AppendLine("(DudgetNo");
                    BudgetDetailSql.AppendLine(",CostType     ");
                    BudgetDetailSql.AppendLine(",CompanyCD     ");
                    BudgetDetailSql.AppendLine(",Budgetcost");
                    BudgetDetailSql.AppendLine(",DeptID   ");
                    BudgetDetailSql.AppendLine(",Remark)");
                    BudgetDetailSql.AppendLine(" values ");
                    BudgetDetailSql.AppendLine("(@DudgetNo");
                    BudgetDetailSql.AppendLine(",@CostType     ");
                    BudgetDetailSql.AppendLine(",@CompanyCD     ");
                    BudgetDetailSql.AppendLine(",@Budgetcost");
                    BudgetDetailSql.AppendLine(",@DeptID   ");
                    BudgetDetailSql.AppendLine(",@Remark)");
                    #endregion
                    #region 设置参数
                    SqlParameter[] BudgetDetailParams = new SqlParameter[6];
                    BudgetDetailParams[0] = SqlHelper.GetParameter("@DudgetNo", BudgetNo);
                    BudgetDetailParams[1] = SqlHelper.GetParameter("@CostType", Convert.ToInt32(FeeType));
                    BudgetDetailParams[2] = SqlHelper.GetParameter("@Budgetcost", Convert.ToDecimal(PlanFee));
                    BudgetDetailParams[3] = SqlHelper.GetParameter("@DeptID", Convert.ToInt32(DeptID));
                    BudgetDetailParams[4] = SqlHelper.GetParameter("@Remark", Remark);
                    BudgetDetailParams[5] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);

                    SqlCommand BudgetDetDetailComInfo = new SqlCommand(BudgetDetailSql.ToString());
                    BudgetDetDetailComInfo.Parameters.AddRange(BudgetDetailParams);
                    comms[i] = BudgetDetDetailComInfo;
                    #endregion

                }
                //执行
                SqlHelper.ExecuteTransForList(comms);
                RetValID = Convert.ToInt32(paramgas[17].Value.ToString());
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch 
            {
                RetValID = 0;
                return false;
            }
        }
        #endregion
        #region 修改财务预算单信息
       /// <summary>
       /// 修改财务预算单信息
       /// </summary>
       /// <param name="OfficeThingsBuyM">领用单主信息</param>
       /// <param name="OfficeThingsUsedInfos">领用单详细信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool UpdateFinacialBudgerInfo(FinancialBudgetModel FinacialBudget_Model, string BudgetInfo, string userID)
       {
           try
           {
               #region 添加财务预算主表信息sql语句
               StringBuilder BudgetSql = new StringBuilder();
               BudgetSql.AppendLine("UPDATE officedba.Financialbudgetbill");
               BudgetSql.AppendLine("SET Title=@Title");
               BudgetSql.AppendLine(",DeptID=@DeptID   ");
               BudgetSql.AppendLine(",StartDate=@StartDate  ");
               BudgetSql.AppendLine(",EndDate=@EndDate    ");
               BudgetSql.AppendLine(",PayType=@PayType");
               BudgetSql.AppendLine(",CurrencyType=@CurrencyType");
               BudgetSql.AppendLine(",CurrencyRate=@CurrencyRate");
               BudgetSql.AppendLine(",Budgetcost=@Budgetcost");
               BudgetSql.AppendLine(",FinancialBudgetType=@FinancialBudgetType");
               BudgetSql.AppendLine(",Remark=@Remark");
               BudgetSql.AppendLine(",ModifiedUserID=@ModifiedUserID");
               BudgetSql.AppendLine(",ModifiedDate=@ModifiedDate");
               BudgetSql.AppendLine("WHERE CompanyCD=@CompanyCD AND BudgetCD=@BudgetCD");

               #endregion

               #region 设置参数
               SqlParameter[] paramgas = new SqlParameter[14];
               paramgas[0] = SqlHelper.GetParameter("@Title", FinacialBudget_Model.Title);
               paramgas[1] = SqlHelper.GetParameter("@DeptID", FinacialBudget_Model.DeptID);
               paramgas[2] = SqlHelper.GetParameter("@StartDate", FinacialBudget_Model.StartDate);
               paramgas[3] = SqlHelper.GetParameter("@EndDate", FinacialBudget_Model.EndDate);
               paramgas[4] = SqlHelper.GetParameter("@PayType", FinacialBudget_Model.PayType);
               paramgas[5] = SqlHelper.GetParameter("@CurrencyType", FinacialBudget_Model.CurrencyType);
               paramgas[6] = SqlHelper.GetParameter("@CurrencyRate", FinacialBudget_Model.CurrencyRate);
               paramgas[7] = SqlHelper.GetParameter("@Budgetcost", FinacialBudget_Model.Budgetcost);
               paramgas[8] = SqlHelper.GetParameter("@Remark", FinacialBudget_Model.Remark);
               paramgas[9] = SqlHelper.GetParameter("@CompanyCD", FinacialBudget_Model.CompanyCD);
               paramgas[10] = SqlHelper.GetParameter("@BudgetCD", FinacialBudget_Model.BudgetCD);
               paramgas[11] = SqlHelper.GetParameter("@ModifiedUserID",userID);
               paramgas[12] = SqlHelper.GetParameter("@ModifiedDate", System.DateTime.Now);
               paramgas[13] = SqlHelper.GetParameter("@FinancialBudgetType", FinacialBudget_Model.FinancialBudgetType);

               #endregion
               return UpdateAll(BudgetSql.ToString(), BudgetInfo, FinacialBudget_Model.BudgetCD, FinacialBudget_Model.CompanyCD,paramgas);
           }
           catch 
           {
               return false;
           }
       }

       /// <summary>
       /// 修改入库库存和入库单明细信息
       /// </summary>
       /// <returns>更新是否成功 false:失败，true:成功</returns>
       public static bool UpdateAll(string BudgetSql, string BudgetInfo, string BudgetCD, string CompanyCD, SqlParameter[] paramgas)
       {
           try
           {
               string[] BudgetInfoArrary = BudgetInfo.Split('|');
               SqlCommand[] comms = new SqlCommand[BudgetInfoArrary.Length+1]; //申明cmd数组(主表，明细表，每条明细生成一条库存记录)
               SqlCommand BudgetInfoCom = new SqlCommand(BudgetSql.ToString());
               BudgetInfoCom.Parameters.AddRange(paramgas);
               comms[0] = BudgetInfoCom;

               #region 首先删除此入库单的明细
               string BudgetDetailInfoSql = "DELETE FROM officedba.Financialbudgetdetails WHERE DudgetNo='" + BudgetCD + "' AND CompanyCD='" + CompanyCD + "'";
               SqlCommand BudgetDetailInfoCom = new SqlCommand(BudgetDetailInfoSql.ToString());
               comms[1] = BudgetDetailInfoCom;
               #endregion

               string recorditems = "";
               string[] gasfield = null;

               for (int i = 1; i < BudgetInfoArrary.Length; i++) //循环数组
               {
                   recorditems = BudgetInfoArrary[i].ToString();
                   gasfield = recorditems.Split(',');

                   string DeptID = gasfield[0].ToString();//所属部门
                   string FeeType = gasfield[1].ToString();//费用类型
                   string PlanFee = gasfield[2].ToString();//预算费用
                   string Remark = gasfield[3].ToString();//备注


                   #region 拼写添加入库明细信息sql语句
                   StringBuilder BudgetDetailSql = new StringBuilder();
                   BudgetDetailSql.AppendLine("INSERT INTO officedba.Financialbudgetdetails");
                   BudgetDetailSql.AppendLine("(DudgetNo");
                   BudgetDetailSql.AppendLine(",CostType     ");
                   BudgetDetailSql.AppendLine(",CompanyCD     ");
                   BudgetDetailSql.AppendLine(",Budgetcost");
                   BudgetDetailSql.AppendLine(",DeptID   ");
                   BudgetDetailSql.AppendLine(",Remark)");
                   BudgetDetailSql.AppendLine(" values ");
                   BudgetDetailSql.AppendLine("(@DudgetNo");
                   BudgetDetailSql.AppendLine(",@CostType     ");
                   BudgetDetailSql.AppendLine(",@CompanyCD     ");
                   BudgetDetailSql.AppendLine(",@Budgetcost");
                   BudgetDetailSql.AppendLine(",@DeptID   ");
                   BudgetDetailSql.AppendLine(",@Remark)");
                   #endregion
                   #region 设置参数
                   SqlParameter[] BudgetDetailParams = new SqlParameter[6];
                   BudgetDetailParams[0] = SqlHelper.GetParameter("@DudgetNo", BudgetCD);
                   BudgetDetailParams[1] = SqlHelper.GetParameter("@CostType", Convert.ToInt32(FeeType));
                   BudgetDetailParams[2] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                   BudgetDetailParams[3] = SqlHelper.GetParameter("@Budgetcost", Convert.ToDecimal(PlanFee));
                   BudgetDetailParams[4] = SqlHelper.GetParameter("@DeptID", Convert.ToInt32(DeptID));
                   BudgetDetailParams[5] = SqlHelper.GetParameter("@Remark", Remark);

                   SqlCommand BudgetDetDetailComInfo = new SqlCommand(BudgetDetailSql.ToString());
                   BudgetDetDetailComInfo.Parameters.AddRange(BudgetDetailParams);
                   comms[i + 1] = BudgetDetDetailComInfo;
                   #endregion
               }
               //执行
               SqlHelper.ExecuteTransForList(comms);
               return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch 
           {
               return false;
           }
       }
       #endregion
        #region 查询预算单列表
       /// <summary>
       /// 查询预算单列表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetFinancialBudgetInfoList(string RecordNo,string StartDate,string EndDate,string HidDept,string BudgetType,string STotalFee,string ETotalFee,string FlowStatus,string CompanyID, int pageIndex, int pageCount, string ord, ref int TotalCount)
       {
           string sql = "SELECT "
                         + "A.ID,A.CompanyCD,A.BudgetCD,A.Title,A.DeptID,Convert(varchar(20),A.StartDate,23)StartDate,Convert(varchar(20),A.EndDate,23)EndDate,"
                         +"CASE A.Billstatus WHEN '0' THEN '制单' WHEN '1' THEN '结单' END BillStatus,"
                         + "A.Creator,Convert(varchar(20),A.CreateDate,23)CreateDate,A.PayType,A.CurrencyType,A.CurrencyRate,A.Budgetcost,"
                         + "A.Remark,A.ModifiedDate,Convert(varchar(20),A.ConfirmDate,23)ConfirmDate,X.EmployeeName AS Confirmor,B.DeptName,C.EmployeeName,"
                         +"CASE isnull(h.FlowStatus,0) "
                         +"WHEN 0 THEN '' "
                         +"WHEN 1 THEN '待审批' "
                         +"WHEN 2 THEN '审批中' "
                         +"WHEN 3 THEN '审批通过' " 
                         +"WHEN 4 THEN '审批不通过' " 
                         +"WHEN 5 THEN '撤销审批' "
                         +"END FlowStatus "
                         +"FROM officedba.Financialbudgetbill A "
                         +"LEFT OUTER JOIN officedba.DeptInfo B "
                         +"ON A.CompanyCD=B.CompanyCD AND A.DeptID=B.ID "
                         +"LEFT OUTER JOIN officedba.EmployeeInfo C "
                         +"ON A.CompanyCD=C.CompanyCD AND A.Creator=C.ID "
                         + "LEFT OUTER JOIN officedba.EmployeeInfo X "
                         + "ON A.CompanyCD=X.CompanyCD AND A.Confirmor=X.ID "
                         +"LEFT OUTER JOIN "
                         +"(select max(m.id)ID,m.BillID,m.BillNo,m.CompanyCD from officedba.FlowInstance m,officedba.Financialbudgetbill n  "
                         + "where m.BillTypeFlag='" + ConstUtil.BILL_TYPEFLAG_FINANCE + "' AND m.CompanyCD='" + CompanyID + "' AND "
                         + "m.BillTypeCode='" + ConstUtil.TYPE_BUDGET_BILL_CODE + "' and  m.BillID=n.ID group by m.BillID,m.BillNo,m.CompanyCD) g  "
                         +"on A.BudgetCD=g.BillNo "
                         +" LEFT OUTER JOIN officedba.FlowInstance h "
                         +"ON g.ID=h.ID  "
                         + "WHERE A.CompanyCD='" + CompanyID + "'  ";
           if (RecordNo != "")
               sql += " and A.BudgetCD LIKE '%" + RecordNo + "%'";
           if (StartDate != "")
               sql += " and A.StartDate >= '" + StartDate + "'";
           if (EndDate != "")
               sql += " and A.StartDate <= '" + EndDate + "'";
           if (HidDept != "")
               sql += " and A.DeptID = " + HidDept + "";
           if (BudgetType != "" && BudgetType!="0")
               sql += " and A.FinancialBudgetType = '" + BudgetType + "'";
           if (STotalFee != "")
               sql += " and A.Budgetcost >= " + STotalFee + "";
           if (ETotalFee != "")
               sql += " and A.Budgetcost <= " + ETotalFee + "";
           if (FlowStatus != "" && FlowStatus != "0")
               sql += " and h.FlowStatus = '" + FlowStatus + "'";
           if (FlowStatus == "0")
               sql += " and h.FlowStatus IS NULL";
           return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
       }
       #endregion
        #region  根据预算单ID获取预算单信息（查看、编辑）
       /// <summary>
       /// 根据预算单ID获取预算单信息（查看、编辑）
       /// </summary>
       /// <param name="ID">预算单ID</param>
       /// <returns>DataTable</returns>
       public static DataTable GetBudgetListInfoByID(string CompanyCD,string ID)
       {
           string sql = "select A.ID,A.CompanyCD,A.BudgetCD,A.Title,A.DeptID,"
                        + "Convert(varchar(20),A.StartDate,23)StartDate,Convert(varchar(20),A.EndDate,23)EndDate,"
                        +"CASE A.Billstatus WHEN '0' THEN '制单' WHEN '1' THEN '结单' END BillStatus,"
                        + "A.Creator,Convert(varchar(20),A.CreateDate,23)CreateDate,A.PayType,A.CurrencyType,A.CurrencyRate,A.Budgetcost,A.FinancialBudgetType,A.ModifiedUserID,A.ModifiedDate,"
                        + "A.Remark,X.EmployeeName AS Confirmor,B.CostType AS DetailCostType,B.Budgetcost AS DetailBudgetcost,B.DeptID AS DetailDeptID,"
                        + "B.Remark AS DetailRemark,C.DeptName,D.CurrencyName,E.DeptName AS DetailDeptName,F.CodeName AS DetailCostName,K.EmployeeName,"
                        +"CASE isnull(h.FlowStatus,0) WHEN 0 THEN '' "
                        +"WHEN 1 THEN '待审批' "
                        +"WHEN 2 THEN '审批中' "
                        +"WHEN 3 THEN '审批通过' "
                        +"WHEN 4 THEN '审批不通过' "
                        +"WHEN 5 THEN '撤销审批' "
                        +"END FlowStatus "
                        +"FROM "
                        +"officedba.Financialbudgetbill A "
                        +"LEFT OUTER JOIN officedba.Financialbudgetdetails B "
                        +"ON A.CompanyCD=B.CompanyCD AND A.BudgetCD=B.DudgetNo "
                        +"LEFT OUTER JOIN officedba.DeptInfo C "
                        +"ON A.CompanyCD=C.CompanyCD AND A.DeptID=C.ID "
                        +"LEFT OUTER JOIN officedba.CurrencyTypeSetting D "
                        +"ON A.CompanyCD=D.CompanyCD AND A.CurrencyType=D.ID "
                        +"LEFT OUTER JOIN officedba.DeptInfo E "
                        +"ON B.CompanyCD=E.CompanyCD AND B.DeptID=E.ID "
                        +"LEFT OUTER JOIN officedba.CodeFeeType F "
                        +"ON B.CompanyCD=F.CompanyCD AND B.CostType=F.ID "
                        +"LEFT OUTER JOIN officedba.EmployeeInfo K "
                        +"ON A.CompanyCD=k.CompanyCD AND A.Creator=K.ID "
                        + "LEFT OUTER JOIN officedba.EmployeeInfo X "
                        + "ON A.CompanyCD=X.CompanyCD AND A.Confirmor=X.ID "
                        +"LEFT OUTER JOIN "
                        +"(select max(M.id)ID,M.BillID,M.BillNo,M.CompanyCD from officedba.FlowInstance M,officedba.Financialbudgetbill N "
                        + "where M.Billid="+ID+" AND M.BillTypeFlag='" + ConstUtil.BILL_TYPEFLAG_FINANCE + "' AND "
                        + "M.BillTypeCode='" + ConstUtil.TYPE_BUDGET_BILL_CODE + "' and  M.BillID=n.ID group by M.BillID,M.BillNo,M.CompanyCD) G "
                        +"on A.BudgetCD=G.BillNo and A.CompanyCD=G.CompanyCD "
                        +"LEFT OUTER JOIN officedba.FlowInstance H "
                        +"ON G.ID=H.ID and G.CompanyCD=H.CompanyCD "
                        + "WHERE A.CompanyCD='" + CompanyCD + "' AND A.ID=" + ID + "";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
        #region 更新财务预算确认信息
       /// <summary>
       /// 更新财务预算确认信息
       /// </summary>
       /// <param name="CarApplyM">财务预算信息</param>
       /// <returns>添加是否成功 false:失败，true:成功</returns>
       public static bool UpdateFinancialBudgetConfirmInfo(string BillStatus, string Confirmor, string ConfirmDate, string ID, string userID)
       {
           try
           {
               #region 财务预算信息SQL拼写
               StringBuilder sql = new StringBuilder();
               sql.AppendLine("UPDATE officedba.Financialbudgetbill");
               sql.AppendLine("		SET BillStatus=@BillStatus        ");
               sql.AppendLine("		,Confirmor=@Confirmor        ");
               sql.AppendLine("		,ConfirmDate=@ConfirmDate        ");
               sql.AppendLine("		,ModifiedDate=@ModifiedDate        ");
               sql.AppendLine("		,ModifiedUserID=@ModifiedUserID        ");
               sql.AppendLine("WHERE                  ");
               sql.AppendLine("		ID=@ID   ");

               #endregion
               #region 财务预算信息参数设置
               SqlParameter[] param;
               param = new SqlParameter[6];
               param[0] = SqlHelper.GetParameter("@BillStatus", BillStatus);
               param[1] = SqlHelper.GetParameter("@Confirmor", Confirmor);
               param[2] = SqlHelper.GetParameter("@ConfirmDate", ConfirmDate);
               param[3] = SqlHelper.GetParameter("@ModifiedDate", System.DateTime.Now);
               param[4] = SqlHelper.GetParameter("@ModifiedUserID", userID);
               param[5] = SqlHelper.GetParameter("@ID", ID);

               #endregion
               SqlHelper.ExecuteTransSql(sql.ToString(), param);
               return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch 
           {
               return false;
           }
       }
       #endregion

        #region 取消确认信息
       /// <summary>
       /// 取消确认信息
       /// </summary>
       /// <param name="CarApplyM">财务预算信息</param>
       /// <returns>取消是否成功 false:失败，true:成功</returns>
       public static bool UpdateFinancialBudgetCancelConfirmInfo(string BillStatus, string Confirmor, string ConfirmDate, string ID, string userID, string CompanyID)
       {
           try
           {
               #region 财务预算信息SQL拼写
               StringBuilder sql = new StringBuilder();
               sql.AppendLine("UPDATE officedba.Financialbudgetbill");
               sql.AppendLine("		SET BillStatus=@BillStatus        ");
               sql.AppendLine("		,Confirmor=@Confirmor        ");
               sql.AppendLine("		,ConfirmDate=@ConfirmDate        ");
               sql.AppendLine("		,ModifiedDate=@ModifiedDate        ");
               sql.AppendLine("		,ModifiedUserID=@ModifiedUserID        ");
               sql.AppendLine("WHERE                  ");
               sql.AppendLine("		ID=@ID   ");

               #endregion
               #region 财务预算信息参数设置
               SqlParameter[] param;
               param = new SqlParameter[6];
               param[0] = SqlHelper.GetParameter("@BillStatus", BillStatus);
               param[1] = SqlHelper.GetParameter("@Confirmor", Confirmor);
               param[2] = SqlHelper.GetParameter("@ConfirmDate", ConfirmDate);
               param[3] = SqlHelper.GetParameter("@ModifiedDate", System.DateTime.Now);
               param[4] = SqlHelper.GetParameter("@ModifiedUserID", userID);
               param[5] = SqlHelper.GetParameter("@ID", ID);
               #endregion
               TransactionManager tran = new TransactionManager();
               tran.BeginTransaction();
               try
               {
                   FlowDBHelper.OperateCancelConfirm(CompanyID, 9, 6, Convert.ToInt32(ID), userID, tran);//取消确认
                   SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, sql.ToString(), param);
                   tran.Commit();
                   return true;
               }
               catch 
               {
                   tran.Rollback();
                   return false;
               }
           }
           catch 
           {
               return false;
           }
       }
       #endregion

        #region 能否删除预算单信息
       /// <summary>
       /// 能否删除预算单信息
       /// </summary>
       /// <param name="EmployeeID">员工ID</param>
       /// <param name="CompanyID">公司代码</param>
       /// <returns></returns>
       public static bool IfDeleteFinancialBudget(string FinancialBudgetIDS, string BillTypeFlag, string BillTypeCode)
       {
           string[] IDS = null;
           IDS = FinancialBudgetIDS.Split(',');
           bool Flag = true;

           for (int i = 0; i < IDS.Length; i++)
           {
               if (IsExistInfo(IDS[i], BillTypeFlag, BillTypeCode))
               {
                   Flag = false;
                   break;
               }
           }
           return Flag;
       }
       #endregion
        #region 能否删除预算单信息
       /// <summary>
       /// 能否删除预算单信息
       /// </summary>
       /// <param name="EmployeeID">员工ID</param>
       /// <param name="CompanyID">公司代码</param>
       /// <returns></returns>
       public static bool IsExistInfo(string ID, string BillTypeFlag, string BillTypeCode)
       {

           string sql = "SELECT * FROM officedba.FlowInstance WHERE BillID=" + ID + " AND BillTypeFlag='" + BillTypeFlag + "' AND BillTypeCode='" + BillTypeCode + "'";
           return SqlHelper.ExecuteSql(sql).Rows.Count > 0 ? true : false;
       }
       #endregion
        #region 删除财务预算信息
       /// <summary>
       /// 删除财务预算信息
       /// </summary>
       /// <param name="BudgetNos">预算单nos</param>
       /// <returns>删除是否成功 false:失败，true:成功</returns>
       public static bool DeleteFinancialBudgetInfo(string BudgetNos,string CompanyCD)
       {
           string allApplyID = "";
           System.Text.StringBuilder sb = new System.Text.StringBuilder();
           string[] Delsql = new string[2];
           try
           {
               string[] IDS = null;
               IDS = BudgetNos.Split(',');

               for (int i = 0; i < IDS.Length; i++)
               {
                   if (IDS[i] != "")
                   {
                       IDS[i] = "'" + IDS[i] + "'";
                       sb.Append(IDS[i]);
                   }
               }

               allApplyID = sb.ToString().Replace("''", "','");
               Delsql[0] = "DELETE FROM officedba.Financialbudgetbill WHERE BudgetCD IN (" + allApplyID + ") and CompanyCD='" + CompanyCD + "'";
               Delsql[1] = "DELETE FROM officedba.Financialbudgetdetails WHERE DudgetNo IN (" + allApplyID + ") and CompanyCD='" + CompanyCD + "'";
               SqlHelper.ExecuteTransForListWithSQL(Delsql);
               return SqlHelper.Result.OprateCount > 0 ? true : false;
           }
           catch 
           {
               return false;
           }
       }
       #endregion

        #region 查询预算单列表
       /// <summary>
       /// 查询预算单列表
       /// </summary>
       /// <returns>DataTable</returns>
       public static DataTable GetFinancialBudgetInfoListForExp(string RecordNo, string StartDate, string EndDate, string FlowStatus, string CompanyID, string ord)
       {
           string sql = "SELECT "
                         + "A.ID,A.CompanyCD,A.BudgetCD,A.Title,A.DeptID,Convert(varchar(20),A.StartDate,23)StartDate,Convert(varchar(20),A.EndDate,23)EndDate,"
                         + "CASE A.Billstatus WHEN '0' THEN '制单' WHEN '1' THEN '结单' END BillStatus,"
                         + "A.Creator,Convert(varchar(20),A.CreateDate,23)CreateDate,A.PayType,A.CurrencyType,A.CurrencyRate,A.Budgetcost,"
                         + "A.Remark,A.ModifiedDate,Convert(varchar(20),A.ConfirmDate,23)ConfirmDate,X.EmployeeName AS Confirmor,B.DeptName,C.EmployeeName,"
                         + "CASE isnull(h.FlowStatus,0) "
                         + "WHEN 0 THEN '' "
                         + "WHEN 1 THEN '待审批' "
                         + "WHEN 2 THEN '审批中' "
                         + "WHEN 3 THEN '审批通过' "
                         + "WHEN 4 THEN '审批不通过' "
                         + "WHEN 5 THEN '撤销审批' "
                         + "END FlowStatus "
                         + "FROM officedba.Financialbudgetbill A "
                         + "LEFT OUTER JOIN officedba.DeptInfo B "
                         + "ON A.CompanyCD=B.CompanyCD AND A.DeptID=B.ID "
                         + "LEFT OUTER JOIN officedba.EmployeeInfo C "
                         + "ON A.CompanyCD=C.CompanyCD AND A.Creator=C.ID "
                         + "LEFT OUTER JOIN officedba.EmployeeInfo X "
                         + "ON A.CompanyCD=X.CompanyCD AND A.Confirmor=X.ID "
                         + "LEFT OUTER JOIN "
                         + "(select max(m.id)ID,m.BillID,m.BillNo,m.CompanyCD from officedba.FlowInstance m,officedba.Financialbudgetbill n  "
                         + "where m.BillTypeFlag='" + ConstUtil.BILL_TYPEFLAG_FINANCE + "' AND m.CompanyCD='" + CompanyID + "' AND "
                         + "m.BillTypeCode='" + ConstUtil.TYPE_BUDGET_BILL_CODE + "' and  m.BillID=n.ID group by m.BillID,m.BillNo,m.CompanyCD) g  "
                         + "on A.BudgetCD=g.BillNo "
                         + " LEFT OUTER JOIN officedba.FlowInstance h "
                         + "ON g.ID=h.ID  "
                         + "WHERE A.CompanyCD='" + CompanyID + "'  ";
           if (RecordNo != "")
               sql += " and A.BudgetCD LIKE '%" + RecordNo + "%'";
           if (StartDate != "")
               sql += " and A.StartDate >= '" + StartDate + "'";
           if (EndDate != "")
               sql += " and A.StartDate <= '" + EndDate + "'";
           if (FlowStatus != "" && FlowStatus != "0")
               sql += " and h.FlowStatus = '" + FlowStatus + "'";
           if (FlowStatus == "0")
               sql += " and h.FlowStatus IS NULL";
           sql = sql + ord;
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
        #region 预算单单据打印
       /// <summary>
       /// 预算单单据打印
       /// </summary>
       /// <param name="RecordNo">预算单单据号</param>
       /// <returns>DataTable</returns>
       public static DataTable GetFinancialBudgetInfoByRecordNoForPrint(string RecordNo, string CompanyCD)
       {
           string sql = "SELECT A.ID, A.CompanyCD, A.BudgetCD, A.Title, A.DeptID, CONVERT(varchar(20), A.StartDate, 23) AS StartDate, CONVERT(varchar(20), A.EndDate, 23) "
                      + "AS EndDate, CASE A.Billstatus WHEN '0' THEN '制单' WHEN '1' THEN '结单' END AS BillStatus, A.Creator, CONVERT(varchar(20), A.CreateDate, 23) "
                      +"AS CreateDate, CASE A.PayType WHEN '1' THEN '现金' WHEN '2' THEN '银行存款' END PayType, A.CurrencyType, A.CurrencyRate, A.Budgetcost, A.Remark, X.EmployeeName AS Confirmor, C.DeptName, D.CurrencyName, "
                      + "K.EmployeeName,Y.TypeName AS FinancialBudgetType "
                      +"FROM  officedba.Financialbudgetbill AS A LEFT OUTER JOIN "
                      +"officedba.DeptInfo AS C ON A.CompanyCD = C.CompanyCD AND A.DeptID = C.ID LEFT OUTER JOIN "
                      +"officedba.CurrencyTypeSetting AS D ON A.CompanyCD = D.CompanyCD AND A.CurrencyType = D.ID LEFT OUTER JOIN "
                      +"officedba.EmployeeInfo AS K ON A.CompanyCD = K.CompanyCD AND A.Creator = K.ID LEFT OUTER JOIN "
                      +"officedba.EmployeeInfo AS X ON A.CompanyCD = X.CompanyCD AND A.Confirmor = X.ID "
                      + "LEFT OUTER JOIN officedba.CodePublicType AS Y ON A.CompanyCD = Y.CompanyCD AND A.FinancialBudgetType = Y.ID "
                      + "WHERE A.CompanyCD='" + CompanyCD + "' AND A.BudgetCD='" + RecordNo + "'"; 
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
        #region 预算单明细数据打印
       /// <summary>
       /// 预算单明细数据打印
       /// </summary>
       /// <param name="RecordNo">预算单单据号</param>
       /// <returns>DataTable</returns>
       public static DataTable GetFinancialBudgetDetailInfoByRecordNoForPrint(string RecordNo, string CompanyCD)
       {
           string sql = "SELECT B.CostType AS DetailCostType, B.Budgetcost AS DetailBudgetcost, B.DeptID AS DetailDeptID,"
                         +"B.Remark AS DetailRemark,E.DeptName AS DetailDeptName, F.CodeName AS DetailCostName "
                         +"FROM officedba.Financialbudgetdetails AS B LEFT OUTER JOIN "
                         +"officedba.DeptInfo AS E ON B.CompanyCD = E.CompanyCD AND B.DeptID = E.ID LEFT OUTER JOIN "
                         +"officedba.CodeFeeType AS F ON B.CompanyCD = F.CompanyCD AND B.CostType = F.ID "
                         + "WHERE B.CompanyCD='" + CompanyCD + "' AND B.DudgetNo ='" + RecordNo + "'";
           return SqlHelper.ExecuteSql(sql);
       }
       #endregion
    }
}

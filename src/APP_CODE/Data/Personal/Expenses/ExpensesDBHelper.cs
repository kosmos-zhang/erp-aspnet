/****************************************
 *创建人：何小武 
 *创建日期：2009-9-7
 *描述：费用管理
 ***************************************/
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Model.Personal.Expenses;
using XBase.Data.DBHelper;
using XBase.Data.Common;

namespace XBase.Data.Personal.Expenses
{
    public class ExpensesDBHelper
    {
        #region 增、删、改
        #region 添加费用申请单
        /// <summary>
        /// 添加费用申请单
        /// </summary>
        /// <param name="expApplyModel"></param>
        /// <param name="expDetailModelList"></param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public static bool SaveExpensesApply(ExpensesApplyModel expApplyModel, List<ExpDetailsModel> expDetailModelList,out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            strMsg = "";
            //判断单据编号是否存在
            if (NoIsExist(expApplyModel.ExpCode,expApplyModel.CompanyCD))
            {
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    int expID;
                    expID = InsertExpensesApply(expApplyModel, tran);
                    InsertExpensesDetails(expDetailModelList, expID,tran);
                    tran.Commit();
                    isSucc = true;
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
                isSucc = false;
                strMsg = "该编号已被使用，请输入未使用的编号！";
            }
            return isSucc;
        }
        #endregion

        #region 插入费用申请主表信息
        /// <summary>
        /// 插入费用申请主表信息
        /// </summary>
        /// <param name="expApplyModel"></param>
        /// <param name="tran"></param>
        public static int InsertExpensesApply(ExpensesApplyModel expApplyModel, TransactionManager tran)
        { 
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.FeeApply (");
            strSql.Append(" CompanyCD,ExpCode,Title,Applyor,AriseDate,NeedDate,TotalAmount,PayType,CanViewUser,");
            strSql.Append(" Reason,DeptID,TransactorID,Status,Creator,CreateDate,ExpType,CustID,SellChanceNo,IsReimburse,");
            strSql.Append(" ModifiedUserID,ModifiedDate,Confirmor,ConfirmDate,ProjectID,Attachment,EndReimbTime)");
            strSql.Append(" values (@CompanyCD,@ExpCode,@Title,@Applyor,@AriseDate,@NeedDate,@TotalAmount,@PayType,@CanViewUser,");
            strSql.Append(" @Reason,@DeptID,@TransactorID,@Status,@Creator,@CreateDate,");
            strSql.Append(" @ExpType,@CustID,@SellChanceNo,@IsReimburse,@ModifiedUserID,@ModifiedDate,@Confirmor,@ConfirmDate,");
            strSql.Append(" @ProjectID,@Attachment,@EndReimbTime); set @Id=@@IDENTITY");
            #region 参数
            SqlParameter[] param={
                                    new SqlParameter ("@CompanyCD",expApplyModel.CompanyCD ),
                                    new SqlParameter ("@ExpCode",expApplyModel.ExpCode ),
                                    new SqlParameter("@Title",expApplyModel.Title ),
                                    new SqlParameter("@Applyor",expApplyModel.Applyor ),
                                    new SqlParameter("@AriseDate",expApplyModel.AriseDate ),
                                    new SqlParameter("@NeedDate",expApplyModel.NeedDate ),
                                    new SqlParameter("@TotalAmount",expApplyModel.TotalAmount ),
                                    new SqlParameter("@PayType",expApplyModel.PayType ),
                                    new SqlParameter("@Reason",expApplyModel.Reason ),
                                    new SqlParameter("@DeptID",expApplyModel.DeptID ),
                                    new SqlParameter("@TransactorID",expApplyModel.TransactorID ),
                                    new SqlParameter("@Status",expApplyModel.Status ),
                                    new SqlParameter("@Creator",expApplyModel.Creator ),
                                    new SqlParameter("@CreateDate",expApplyModel.CreateDate ),
                                    new SqlParameter("@ExpType",expApplyModel.ExpType ),
                                    new SqlParameter("@CustID",expApplyModel.CustID ),
                                    new SqlParameter("@SellChanceNo",expApplyModel.SellChanceNo ),
                                    new SqlParameter("@IsReimburse",expApplyModel.IsReimburse ),
                                    new SqlParameter("@ModifiedUserID",expApplyModel.ModifiedUserID ),
                                    new SqlParameter("@ModifiedDate",expApplyModel.ModifiedDate ),
                                    new SqlParameter("@Confirmor",DBNull.Value ),
                                    new SqlParameter("@ConfirmDate",DBNull.Value),
                                    new SqlParameter("@Id",SqlDbType.Int,6),
                                    new SqlParameter("@CanViewUser",expApplyModel.CanViewUser),
                                    new SqlParameter("@ProjectID",expApplyModel.ProjectID),
                                    new SqlParameter("@Attachment",expApplyModel.Attachment),
                                    new SqlParameter("@EndReimbTime",expApplyModel.EndReimbTime)
                                  };
            param[22].Direction=ParameterDirection.Output;
            foreach (SqlParameter para in param)
            {
                //if (para.Value == null)
                if (para.Value == null || para.Value.ToString() == "-1")
                {
                    para.Value = DBNull.Value;
                }
            }
            //SqlHelper.ExecuteSql(strSql.ToString(), param);
            SqlHelper.ExecuteTransSql(strSql.ToString(), param);
            int Id =Convert.ToInt32(param[22].Value);
            return Id;
            #endregion
        }
        #endregion

        #region 插入费用申请子表信息--费用明细
        /// <summary>
        /// 插入费用申请子表信息--费用明细
        /// </summary>
        /// <param name="expDetailsModelList"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static void InsertExpensesDetails(List<ExpDetailsModel> expDetailsModelList, int expID,TransactionManager tran)
        {
            foreach (ExpDetailsModel expDetailModel in expDetailsModelList)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into officedba.FeeApplyDetail (");
                strSql.Append("ExpID,SortNo,ExpType,Amount,ExpRemark)values(");
                strSql.Append("@ExpID,@SortNo,@ExpType,@Amount,@ExpRemark)");

                SqlParameter[] param ={
                                      new SqlParameter("@ExpID",expID),
                                      new SqlParameter("@SortNo",expDetailModel.SortNo ),
                                      new SqlParameter("@ExpType",expDetailModel.ExpType ),
                                      new SqlParameter("@Amount",expDetailModel.Amount ),
                                      new SqlParameter("@ExpRemark",expDetailModel.ExpRemark )
                                      };
                foreach (SqlParameter para in param)
                {
                    if (para.Value == null)
                    {
                        para.Value = DBNull.Value;
                    }
                }
                SqlHelper.ExecuteTransSql(strSql.ToString(), param);
            }
        }
        #endregion

        #region 修改费用申请单
        /// <summary>
        /// 修改费用申请单
        /// </summary>
        /// <param name="expApplyModel">费用主表Model</param>
        /// <param name="expDetailModelList">费用明细表Model</param>
        /// <param name="strMsg">消息字符串</param>
        /// <returns></returns>
        public static bool UpdateExpensesApply(ExpensesApplyModel expApplyModel, List<ExpDetailsModel> expDetailModelList, out string strMsg)
        {
            bool isSucc = false;//是否修改成功
            strMsg = "";
            if (IsUpdate(expApplyModel.ExpCode , expApplyModel.CompanyCD))
            {
                string strSql = "delete from officedba.FeeApplyDetail where  ExpID=@ExpID ";
                SqlParameter[] paras = { new SqlParameter("@ExpID", expApplyModel.ID)};
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    UpdateExpApply(expApplyModel, tran);
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), paras);
                    InsertExpensesDetails(expDetailModelList,expApplyModel.ID, tran);
                    tran.Commit();
                    isSucc = true;
                    strMsg = "修改成功！";
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    strMsg = "修改失败，请联系系统管理员！";
                    throw ex;
                }
            }
            else
            {
                isSucc = false;
                strMsg = "非制单状态的单据不可修改！";
            }
            return isSucc;
        }
        #region 修改费用申请单主表信息
        /// <summary>
        /// 修改费用申请单主表信息
        /// </summary>
        /// <param name="expApplyModel">费用主表Model</param>
        /// <param name="tran"></param>
        public static void UpdateExpApply(ExpensesApplyModel expApplyModel, TransactionManager tran)
        { 
            StringBuilder strSql=new StringBuilder ();
            strSql.Append("update officedba.FeeApply set ");
            strSql.Append(" Title=@Title,Applyor=@Applyor,AriseDate=@AriseDate,NeedDate=@NeedDate,TotalAmount=@TotalAmount,");
            strSql.Append(" PayType=@PayType,Reason=@Reason,CanViewUser=@CanViewUser,");
            strSql.Append(" DeptID=@DeptID,TransactorID=@TransactorID,ExpType=@ExpType,CustID=@CustID,");
            strSql.Append(" SellChanceNo=@SellChanceNo,ModifiedUserID=@ModifiedUserID,ModifiedDate=@ModifiedDate,");
            strSql.Append(" ProjectID=@ProjectID,Attachment=@Attachment,EndReimbTime=@EndReimbTime ");
            strSql.Append(" where ID=@ID ");

            SqlParameter [] param={
                                      new SqlParameter("@ID",expApplyModel.ID ),
                                      new SqlParameter("@Title",expApplyModel.Title ),
                                      new SqlParameter("@Applyor",expApplyModel.Applyor),
                                      new SqlParameter("@AriseDate",expApplyModel.AriseDate),
                                      new SqlParameter("@NeedDate",expApplyModel.NeedDate),
                                      new SqlParameter("@TotalAmount",expApplyModel.TotalAmount),
                                      new SqlParameter("@PayType",expApplyModel.PayType),
                                      //new SqlParameter("@CurrencyType",expApplyModel.CurrencyType),
                                      //new SqlParameter("@CurrencyRate",expApplyModel.CurrencyRate),
                                      new SqlParameter("@Reason",expApplyModel.Reason),
                                      new SqlParameter("@DeptID",expApplyModel.DeptID),
                                      new SqlParameter("@TransactorID",expApplyModel.TransactorID),
                                      new SqlParameter("@ExpType",expApplyModel.ExpType),
                                      new SqlParameter("@CustID",expApplyModel.CustID),
                                      new SqlParameter("@SellChanceNo",expApplyModel.SellChanceNo),
                                      new SqlParameter("@ModifiedUserID",expApplyModel.ModifiedUserID),
                                      new SqlParameter("@ModifiedDate",expApplyModel.ModifiedDate),
                                      new SqlParameter("@CanViewUser",expApplyModel.CanViewUser),
                                      new SqlParameter("@ProjectID",expApplyModel.ProjectID),
                                      new SqlParameter("@Attachment",expApplyModel.Attachment),
                                      new SqlParameter("@EndReimbTime",expApplyModel.EndReimbTime)
                                  };
            foreach (SqlParameter para in param)
            {
                if (para.Value == null || para.Value.ToString()=="-1")
                {
                    para.Value = DBNull.Value;
                }
            }
            SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), param);
        }
        #endregion
        #endregion

        #region 删除费用申请单
        /// <summary>
        /// 删除已选择的费用申请单
        /// </summary>
        /// <param name="expID">费用申请单ID</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <param name="strMsg"></param>
        /// <param name="strFieldText"></param>
        /// <returns></returns>
        public static bool DelExpensesApply(string expID, string strCompanyCD, out string strMsg, out string strFieldText)
        {
            bool isSucc = false;
            string allExpApplyID = "";
            strMsg = "";
            strFieldText = "";
            bool bTemp = false;//单据是否可以被删除
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            TransactionManager tran = new TransactionManager();

            string[] strExpApplyID = null;
            strExpApplyID = expID.Split(',');

            for (int i = 0; i < strExpApplyID.Length; i++)
            {
                if (!IsFlow(strExpApplyID[i], strCompanyCD))
                {
                    strFieldText += "单据：" + strExpApplyID[i] + "|";
                    strMsg += "非制单状态单据不允许删除！|";
                    bTemp = true;
                }
                strExpApplyID[i] = "'" + strExpApplyID[i] + "'";
                sb.Append(strExpApplyID[i]);
            }

            allExpApplyID = sb.ToString().Replace("''", "','");
            if (!bTemp)
            {
                tran.BeginTransaction();
                try
                {
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.FeeApply WHERE ID IN ( " + allExpApplyID + " ) and CompanyCD='" + strCompanyCD + "'", null);
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.FeeApplyDetail WHERE ExpID IN ( " + allExpApplyID + " ) ", null);
                    /*删除审批流程相关信息Start*/
                    FlowDBHelper.DelFlowInfo(tran, allExpApplyID, strCompanyCD, "1", "4"); 
                    //SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "delete from officedba.FlowTaskList where FlowInstanceID in (select ID from officedba.FlowInstance where billid in (" + allExpApplyID + ") and billtypeflag='1' and billtypecode='4' and companycd='" + strCompanyCD + "')", null);
                    //SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "delete from officedba.FlowTaskHistory where FlowInstanceID in (select ID from officedba.FlowInstance where billid in (" + allExpApplyID + ") and billtypeflag='1' and billtypecode='4' and companycd='" + strCompanyCD + "')", null);
                    //SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "delete from officedba.FlowInstance where billid in (" + allExpApplyID + ") and billtypeflag='1' and billtypecode='4'  and CompanyCD='" + strCompanyCD + "'", null);
                    /*删除审批流程相关信息End*/
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
            }
            else
            {
                isSucc = false;
            }
            return isSucc;
        }
        #endregion

        #region 单据是否是制单状态
        /// <summary>
        /// 单据是否是制单状态
        /// </summary>
        /// <param name="ExpApplyID">费用申请单ID</param>
        /// <param name="strCompanyCD">公司代码</param>
        /// <returns>返回true时表示都是制单状态</returns>
        private static bool IsFlow(string ExpApplyID, string strCompanyCD)
        {
            bool isSuc = false;
            int iCount = 0;
            string strSql = string.Empty;
            SqlParameter[] paras = { 
                                       new SqlParameter("@ID", ExpApplyID),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += "select count(1) from officedba.FeeApply where ID=@ID and  CompanyCD = @CompanyCD and Status<>1";

            try
            {
                iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, paras));
            }
            catch 
            {
                isSuc = false;
            }

            if (iCount == 0)
            {
                isSuc = true;
            }

            return isSuc;
        }
        #endregion

        #endregion

        #region 确认、取消确认
        #region 确认费用申请单
        /// <summary>
        /// 确认费用申请单
        /// </summary>
        /// <param name="expCode"></param>
        /// <param name="strCompanyCD"></param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public static bool ConfirmExpApply(string expCode, string strCompanyCD, string EmployeeName, int EmployeeID, string UserID, out string strMsg)
        {
            strMsg = "";
            bool isSuc = false;
            StringBuilder strSql = new StringBuilder();

            if (isHandle(expCode, "1", strCompanyCD))
            {
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    strSql.Append(" update officedba.FeeApply set ");
                    strSql.Append(" Confirmor=@Confirmor ,Status=@Status,ConfirmDate=getdate()");
                    strSql.Append(" ,ModifiedUserID=@ModifiedUserID,ModifiedDate=getdate() ");
                    strSql.Append(" where CompanyCD= @CompanyCD and ExpCode=@ExpCode ");

                    SqlParameter[] param ={
                                            new SqlParameter("@Confirmor",EmployeeID),
                                            new SqlParameter("@Status","2"),
                                            new SqlParameter("@CompanyCD",strCompanyCD),
                                            new SqlParameter("@ExpCode",expCode),
                                            new SqlParameter("@ModifiedUserID",UserID)
                                         };

                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), param);
                    tran.Commit();
                    isSuc = true;
                    strMsg = "确认成功！" + "@" + EmployeeName + "@" + System.DateTime.Now;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    strMsg = "确认失败！请联系管理员";
                    isSuc = false;
                    throw ex;
                }
            }
            else
            {//已经被其他人确认
                strMsg = "已经确认的单据不可再次确认！";
                isSuc = false;
            }
            return isSuc;
        }
        #endregion
        #region 取消确认费用申请单
        /// <summary>
        /// 取消确认费用申请单
        /// </summary>
        /// <param name="expCode"></param>
        /// <param name="strCompanyCD"></param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public static bool UnConfirmExpApply(string expCode, string strCompanyCD,string UserID, out string strMsg)
        {
            strMsg = "";
            bool isSuc = false;
            StringBuilder strSql = new StringBuilder();
            int expID = GetExpApplyID(expCode, strCompanyCD);

            //判断单据是否是执行状态，不是执行状态不给取消确认
            if (isHandle(expCode, "2", strCompanyCD))
            {
                strSql.Append(" update officedba.FeeApply set ");
                strSql.Append(" Confirmor=@Confirmor ,Status=@Status,ConfirmDate=@ConfirmDate ");
                strSql.Append(" ,ModifiedUserID=@ModifiedUserID,ModifiedDate=getdate() ");
                strSql.Append(" where CompanyCD= @CompanyCD and ExpCode=@ExpCode ");

                SqlParameter[] param ={
                                        new SqlParameter("@Confirmor",DBNull.Value ),
                                        new SqlParameter("@ConfirmDate",DBNull.Value ),
                                        new SqlParameter("@Status","1"),
                                        new SqlParameter("@CompanyCD",strCompanyCD),
                                        new SqlParameter("@ExpCode",expCode),
                                        new SqlParameter("@ModifiedUserID",UserID)
                                     };

                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                   

                    FlowDBHelper.OperateCancelConfirm(strCompanyCD, 1, 4, expID, UserID, tran);//撤销审批

                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), param);
                    tran.Commit();
                    isSuc = true;
                    strMsg = "取消确认成功！";
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    strMsg = "取消确认失败！请联系管理员";
                    isSuc = false;
                    throw ex;
                }
            }
            else
            {//已经被其他人确认
                strMsg = "该单据已被其他用户取消确认或已报销，不可取消确认！";
                isSuc = false;
            }
            return isSuc;
        }
        #endregion
        #endregion
        #region 报废费用申请单
        /// <summary>
        /// 报废费用申请单
        /// </summary>
        /// <param name="expCode">费用申请单编号</param>
        /// <param name="strCompanyCD">公司编号</param>
        /// <param name="UserID">用户账号</param>
        /// <param name="strMsg">返回操作信息</param>
        /// <returns></returns>
        public static bool ScrapExpenses(string expCode, string strCompanyCD, string UserID,out string strMsg)
        {
            string strSq = string.Empty;
            bool isSuc = false;
            strMsg = "";
            //判断单据是否为确认状态，且未报销的单据
            if (isHandle(expCode, "2", strCompanyCD))
            {
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    strSq = "update  officedba.FeeApply set Status='4' ";
                    strSq += " , ModifiedDate=getdate() ,ModifiedUserID=@UserID ";
                    strSq += " WHERE ExpCode = @ExpCode and CompanyCD=@CompanyCD ";

                    SqlParameter[] param = { 
                                           new SqlParameter("@UserID", UserID),
                                           new SqlParameter("@CompanyCD", strCompanyCD),
                                           new SqlParameter("@ExpCode", expCode)
                                           };

                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSq, param);

                    tran.Commit();
                    isSuc = true;
                    strMsg = "费用申请单作废成功！";
                }
                catch (Exception ex)
                {
                    tran.Rollback();

                    isSuc = false;
                    strMsg = "费用申请单作废失败，请联系系统管理员！";
                    throw ex;
                }
            }
            else
            {
                isSuc = false;
                strMsg = "该单据已被其他用户作废或者已经报销，不可作废！";
            }
            return isSuc;
        }
        #endregion

        #region 根据单据状态判断是否可以执行该操作（报废、确认、取消确认）
        /// <summary>
        /// 根据单据状态判断是否可以执行该操作
        /// 注意：报销状态为“未报销状态”
        /// </summary>
        /// <param name="expCode">费用申请单编号</param>
        /// <param name="billStatus">单据状态</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns>返回true表示能执行该操作</returns>
        public static bool isHandle(string expCode, string billStatus, string strCompanyCD)
        {
            bool isSuc = false;
            int iCount = 0;
            string strSql = string.Empty;

            strSql = "select count(1) from officedba.FeeApply where ExpCode = @ExpCode and CompanyCD=@CompanyCD and Status=@Status and IsReimburse=0 ";
            
            SqlParameter[] param ={
                                 new SqlParameter("@ExpCode", expCode),
                                 new SqlParameter("@CompanyCD", strCompanyCD),
                                 new SqlParameter("@Status", billStatus)
                                 };

            iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, param));
            if (iCount != 0)
            {
                isSuc = true;
            }

            return isSuc;
        }
        #endregion

        #region 根据单据状态判断单据是否可以被修改
        /// <summary>
        /// 根据单据状态判断单据是否可以被修改
        /// </summary>
        /// <param name="SellOutNo"></param>
        /// <returns>返回true时表示可以修改</returns>
        private static bool IsUpdate(string ExpCode, string strCompanyCD)
        {
            bool isSuc = false;
            string strStatus = string.Empty;
            string strSql = string.Empty;
            SqlParameter[] paras = { 
                                       new SqlParameter("@ExpCode", ExpCode),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += "select top 1 Status from officedba.FeeApply where (ExpCode = @ExpCode) AND (CompanyCD = @CompanyCD) and Status=1  ORDER BY ModifiedDate DESC ";
            object obj = SqlHelper.ExecuteScalar(strSql, paras);
            if (obj != null)
            {
                strStatus = obj.ToString();
                switch (strStatus)
                {
                    case "1":
                        isSuc = true;
                        break;
                    default:
                        isSuc = false;
                        break;
                }
            }
            else
            {
                isSuc = true;
            }

            return isSuc;
        }
        #endregion

        #region 判断单据编号是否存在
        /// <summary>
        /// 判断单据编号是否存在
        /// </summary>
        /// <param name="OrderNO">单据编号</param>
        /// <returns>返回true时表示不存在</returns>
        private static bool NoIsExist(string ExpCode, string strCompanyCD)
        {
            bool isSuc = false;
            int iCount = 0;

            string strSql = string.Empty;
            SqlParameter[] paras = { 
                                       new SqlParameter("@ExpCode", ExpCode),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += " select count(1) from officedba.FeeApply ";
            strSql += " WHERE  (ExpCode = @ExpCode) AND (CompanyCD = @CompanyCD) ";
            iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, paras));
            if (iCount == 0)
            {
                isSuc = true;
            }

            return isSuc;
        }
        #endregion

        #region 根据单据编号获取费用申请单ID
        /// <summary>
        /// 根据单据编号获取费用申请单ID
        /// </summary>
        /// <param name="strCode">费用申请单编号</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns></returns>
        public static int GetExpApplyID(string strCode, string strCompanyCD)
        {
            string strSql = string.Empty;
            strSql = "select ID from officedba.FeeApply where ExpCode=@ExpCode and CompanyCD=@CompanyCD ";
            SqlParameter[] param ={
                                 new SqlParameter("@ExpCode",strCode ),
                                 new SqlParameter("@CompanyCD",strCompanyCD )
                                 };
            return (int)SqlHelper.ExecuteScalar(strSql, param); 

        }
        #endregion

        #region 根据查询条件获取费用申请单列表
        /// <summary>
        /// 根据查询条件获取费用申请单列表
        /// 导出列表页调用此方法
        /// </summary>
        /// <param name="expApplyModel">ExpensesApplyModel 实体</param>
        /// <param name="AriseDate1">申请日期</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord">排序</param>
        /// <param name="TotalCount">总记录数</param>
        /// <returns></returns>
        public static DataTable GetExpensesApplyList(ExpensesApplyModel expApplyModel,int empid, DateTime? AriseDate1,string FlowStatus, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select exp.ID,exp.ExpCode,exp.Title,exp.Applyor,e1.EmployeeName as ApplyorName,");
            strSql.Append(" convert(varchar(100), exp.AriseDate, 23) as AriseDate,convert(varchar(100),exp.NeedDate,23) as NeedDate, ");
            strSql.Append(" exp.TotalAmount,exp.DeptID,isnull(d.DeptName,'') as DeptName,exp.TransactorID,e2.EmployeeName as TransactorName,exp.Status ");
            strSql.Append(" ,isnull( case f.FlowStatus when '1' then '待审批' when '2' then '审批中'");
            strSql.Append(" when '3'  then '审批通过' when '4' then '审批不通过' when '5' then '撤消审批' end,'') as FlowStatus ");
            strSql.Append(" ,exp.Confirmor,e3.EmployeeName as ConfirmorName,convert(varchar(100),exp.ConfirmDate,23) as ConfirmDate ");
            //strSql.Append(" ,exp.ProjectID,p,ProjectName ");
            strSql.Append(" ,exp.IsReimburse,case exp.IsReimburse when 1 then '是' when 0 then '否' end as IsReimburseText ");
            strSql.Append(" from officedba.FeeApply exp ");
            strSql.Append(" left join officedba.EmployeeInfo e1 on exp.Applyor=e1.ID ");
            strSql.Append(" left join officedba.EmployeeInfo e2 on exp.TransactorID=e2.ID ");
            strSql.Append(" left join officedba.EmployeeInfo e3 on exp.Confirmor=e3.ID ");
            strSql.Append(" left join officedba.DeptInfo d on exp.DeptID=d.ID ");
            //strSql.Append(" left join officedba.ProjectInfo p on p.ID=exp.ProjectID ");
            strSql.Append(" left join officedba.FlowInstance f on exp.CompanyCD=f.CompanyCD and f.BillTypeFlag=1 ");
            strSql.Append(" and f.BillTypeCode=4 and f.BillNo=exp.ExpCode ");
            strSql.Append(" and f.ID=(select max(ID) from officedba.FlowInstance as f2 where exp.ID=f2.BillID and f2.BillTypeFlag=1 and f2.BillTypeCode=4 )");
            strSql.Append(" where exp.CompanyCD=@CompanyCD ");
            strSql.Append(" and ( charindex('," + empid + ",' , ','+exp.CanViewUser+',')>0 or exp.Creator=" + empid + " OR exp.CanViewUser='' OR exp.CanViewUser is null) ");

            if (expApplyModel.ExpCode != null)
            {
                strSql.Append(" and exp.ExpCode like '%" + expApplyModel.ExpCode + "%' ");
            }
            if (expApplyModel.Title != null)
            {
                strSql.Append(" and exp.Title like '%" + expApplyModel.Title + "%' ");
            }
            if (expApplyModel.TransactorID  != -1)
            {
                strSql.Append(" and exp.TransactorID="+expApplyModel.TransactorID );
            }
            if (expApplyModel.Applyor != -1)
            {
                strSql.Append(" and exp.Applyor="+expApplyModel.Applyor );
            }
            if (expApplyModel.AriseDate != null && AriseDate1 != null)
            {
                strSql.Append(" and (exp.AriseDate >= '"+expApplyModel.AriseDate.ToString().Trim() +"' and exp.AriseDate <= '"+AriseDate1.ToString().Trim() +"' )");
            }
            if (expApplyModel.Status != null)
            {
                strSql.Append(" and exp.Status ="+expApplyModel.Status );
            }
            if (expApplyModel.DeptID != -1)
            {
                strSql.Append(" and exp.DeptID="+expApplyModel.DeptID );
            }
            if (expApplyModel.ProjectID != null)
            {
                strSql.Append(" and exp.ProjectID=" + expApplyModel.ProjectID);
            }
            if (expApplyModel.IsReimburse != null)
            {
                strSql.Append(" and exp.IsReimburse='"+expApplyModel.IsReimburse+"'");
            }
            if (FlowStatus != null)
            {
                if (FlowStatus == "0")
                {
                    strSql.Append(" and f.FlowStatus is null ");
                }
                else
                {
                    strSql.Append(" and f.FlowStatus='" + FlowStatus + "'");
                }
            }

            SqlParameter[] param ={
                               new SqlParameter("@CompanyCD",expApplyModel.CompanyCD)
                               };
            return SqlHelper.CreateSqlByPageExcuteSql(strSql.ToString(), pageIndex, pageCount, ord, param, ref TotalCount);
        }
        #endregion
        #region 根据查询条件，获取所以符合条件记录的金额合计
        /// <summary>
        /// 根据查询条件，获取所以符合条件记录的金额合计
        /// </summary>
        /// <param name="expApplyMOdel">ExpensesApplyModel实体</param>
        /// <param name="empid"></param>
        /// <param name="AriseDate1"></param>
        /// <param name="FlowStatus"></param>
        /// <returns></returns>
        public static DataTable GetExpSumTotal(ExpensesApplyModel expApplyModel, int empid, DateTime? AriseDate1, string FlowStatus)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select sum(exp.TotalAmount) as AllTotalAmount ");
            strSql.Append(" from officedba.FeeApply exp ");
            strSql.Append(" left join officedba.FlowInstance f on exp.CompanyCD=f.CompanyCD and f.BillTypeFlag=1 ");
            strSql.Append(" and f.BillTypeCode=4 and f.BillNo=exp.ExpCode ");
            strSql.Append(" and f.ID=(select max(ID) from officedba.FlowInstance as f2 where exp.ID=f2.BillID and f2.BillTypeFlag=1 and f2.BillTypeCode=4 )");
            strSql.Append(" where exp.CompanyCD=@CompanyCD ");
            strSql.Append(" and ( charindex('," + empid + ",' , ','+exp.CanViewUser+',')>0 or exp.Creator=" + empid + " OR exp.CanViewUser='' OR exp.CanViewUser is null) ");

            if (expApplyModel.ExpCode != null)
            {
                strSql.Append(" and exp.ExpCode like '%" + expApplyModel.ExpCode + "%' ");
            }
            if (expApplyModel.Title != null)
            {
                strSql.Append(" and exp.Title like '%" + expApplyModel.Title + "%' ");
            }
            if (expApplyModel.TransactorID != -1)
            {
                strSql.Append(" and exp.TransactorID=" + expApplyModel.TransactorID);
            }
            if (expApplyModel.Applyor != -1)
            {
                strSql.Append(" and exp.Applyor=" + expApplyModel.Applyor);
            }
            if (expApplyModel.AriseDate != null && AriseDate1 != null)
            {
                strSql.Append(" and (exp.AriseDate >= '" + expApplyModel.AriseDate.ToString().Trim() + "' and exp.AriseDate <= '" + AriseDate1.ToString().Trim() + "' )");
            }
            if (expApplyModel.Status != null)
            {
                strSql.Append(" and exp.Status =" + expApplyModel.Status);
            }
            if (expApplyModel.DeptID != -1)
            {
                strSql.Append(" and exp.DeptID=" + expApplyModel.DeptID);
            }
            if (expApplyModel.ProjectID != null)
            {
                strSql.Append(" and exp.ProjectID=" + expApplyModel.ProjectID);
            }
            if (FlowStatus != null)
            {
                if (FlowStatus == "0")
                {
                    strSql.Append(" and f.FlowStatus is null ");
                }
                else
                {
                    strSql.Append(" and f.FlowStatus='" + FlowStatus + "'");
                }
            }

            SqlParameter[] param ={
                               new SqlParameter("@CompanyCD",expApplyModel.CompanyCD)
                               };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        #region  根据查询条件获取历史费用申请单列表
        /// <summary>
        /// 根据查询条件获取历史费用申请单列表
        /// </summary>
        /// <param name="expApplyModel">ExpensesApplyModel 实体</param>
        /// <param name="AriseDate1">申请日期</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord">排序</param>
        /// <param name="TotalCount">总记录数</param>
        /// <returns></returns>
        public static DataTable GetExpensesApplyHistList(ExpensesApplyModel expApplyModel,int empid, DateTime? AriseDate1, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select exp.ID,exp.ExpCode,exp.Title,exp.Applyor,e1.EmployeeName as ApplyorName,convert(varchar(100), exp.AriseDate, 23) as AriseDate,");
            strSql.Append("convert(varchar(100),exp.NeedDate,23) as NeedDate,exp.TotalAmount,exp.DeptID,isnull(d.DeptName,'') as DeptName, ");
            strSql.Append("exp.TransactorID,e2.EmployeeName as TransactorName,exp.Status ");//,exp.CurrencyType,isnull(c.CurrencyName,'') as CurrencyName,exp.CurrencyRate ");
            strSql.Append(" from officedba.FeeApply exp ");
            strSql.Append(" left join officedba.EmployeeInfo e1 on exp.Applyor=e1.ID ");
            strSql.Append(" left join officedba.EmployeeInfo e2 on exp.TransactorID=e2.ID ");
            strSql.Append(" left join officedba.DeptInfo d on exp.DeptID=d.ID ");
            //strSql.Append(" left join officedba.CurrencyTypeSetting c on exp.CurrencyType=c.ID ");
            strSql.Append(" where exp.CompanyCD=@CompanyCD ");
            strSql.Append(" and ( charindex('," + empid + ",' , ','+exp.CanViewUser+',')>0 or exp.Creator=" + empid + " OR exp.CanViewUser='' OR exp.CanViewUser is null) ");

            if (expApplyModel.ExpCode != null)
            {
                strSql.Append(" and exp.ExpCode like '%" + expApplyModel.ExpCode + "%' ");
            }
            if (expApplyModel.Title != null)
            {
                strSql.Append(" and exp.Title like '%" + expApplyModel.Title + "%' ");
            }
            if (expApplyModel.TransactorID  != -1)
            {
                strSql.Append(" and exp.TransactorID="+expApplyModel.TransactorID );
            }
            if (expApplyModel.Applyor != -1)
            {
                strSql.Append(" and exp.Applyor="+expApplyModel.Applyor );
            }
            if (expApplyModel.AriseDate != null && AriseDate1 != null)
            {
                strSql.Append(" and (exp.AriseDate >= '"+expApplyModel.AriseDate.ToString().Trim() +"' and exp.AriseDate <= '"+AriseDate1.ToString().Trim() +"' )");
            }
            if (expApplyModel.Status != null)
            {
                strSql.Append(" and exp.Status ="+expApplyModel.Status );
            }
            
            SqlParameter[] param ={
                               new SqlParameter("@CompanyCD",expApplyModel.CompanyCD)
                               };
            return SqlHelper.CreateSqlByPageExcuteSql(strSql.ToString(), pageIndex, pageCount, ord, param, ref TotalCount);
        }
        #endregion

        #region 根据费用申请单ID获取该申请单主表信息
        /// <summary>
        /// 根据费用申请单ID获取该申请单主表信息
        /// </summary>
        /// <param name="expID">费用申请单ID</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns></returns>
        public static DataTable GetExpInfoByID(int expID, string strCompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select exp.ID,exp.ExpCode,exp.Title,exp.Applyor,e1.EmployeeName as ApplyorName,convert(varchar(100), exp.AriseDate, 23) as AriseDate,convert(varchar(100),exp.NeedDate,23) as NeedDate, ");
            strSql.Append(" isnull(exp.TotalAmount,0) as TotalAmount,exp.DeptID,d.DeptName as DeptName,exp.TransactorID,isnull(e2.EmployeeName,'') as TransactorName,exp.Status, ");
            strSql.Append(" exp.PayType,");//exp.CurrencyType,isnull(c.CurrencyName,'') as CurrencyName ,exp.CurrencyRate,");
            strSql.Append(" exp.CustID,isnull(cust.CustName,'') as CustName,exp.SellChanceNo,exp.Reason,");
            strSql.Append(" exp.Status,case exp.Status when 1 then '制单' when 2 then '执行' when 4 then '作废' end as StatusText,");
            strSql.Append(" convert(varchar(100), exp.CreateDate, 23) as CreateDate,exp.IsReimburse ,case exp.IsReimburse when 0 then '未报销' when 1 then '已报销' end as IsReimburseText,");
            strSql.Append(" convert(varchar(100),exp.ModifiedDate, 23) as ModifiedDate, exp.ModifiedUserID,");
            strSql.Append(" e3.EmployeeName as CreatorName, e4.EmployeeName as ConfirmorName , ");
            strSql.Append(" convert(varchar(100),exp.ConfirmDate, 23) as ConfirmDate,");
            strSql.Append(" f.FlowStatus as FlowStatus,isnull( case f.FlowStatus when '1' then '待审批' when '2' then '审批中'");
            strSql.Append(" when '3'  then '审批通过' when '4' then '审批不通过' when '5' then '撤消审批' end,'') as FlowStatusText, ");
            strSql.Append(" exp.ExpType,exp.CanViewUser,[dbo].[getEmployeeNameString](exp.CanViewUser) as CanViewUserName ");//暂时没有关联
            strSql.Append(" ,exp.ProjectID,p.ProjectName,exp.Attachment,convert(varchar(10),exp.EndReimbTime,23) as EndReimbTime ");
            strSql.Append(" from officedba.FeeApply exp ");
            strSql.Append(" left join officedba.EmployeeInfo e1 on exp.Applyor=e1.ID ");
            strSql.Append(" left join officedba.EmployeeInfo e2 on exp.TransactorID=e2.ID ");
            strSql.Append(" left join officedba.EmployeeInfo e3 on exp.Creator=e3.ID ");
            strSql.Append(" left join officedba.EmployeeInfo e4 on exp.Confirmor=e4.ID ");
            //strSql.Append(" left join officedba.CurrencyTypeSetting c on exp.CurrencyType=c.ID ");
            strSql.Append(" left join officedba.DeptInfo d on exp.DeptID=d.ID ");
            strSql.Append(" left join officedba.CustInfo cust on exp.CustID=cust.ID ");
            strSql.Append(" left join officedba.ProjectInfo p on p.ID=exp.ProjectID ");
            strSql.Append(" left join officedba.FlowInstance f on exp.CompanyCD=f.CompanyCD and f.BillTypeFlag=1 ");
            strSql.Append(" and f.BillTypeCode=4 and f.BillNo=exp.ExpCode ");
            strSql.Append(" and f.ID=(select max(ID) from officedba.FlowInstance as f2 where exp.ID=f2.BillID and f2.BillTypeFlag=1 and f2.BillTypeCode=4 )");
            strSql.Append(" where exp.ID=@ID and exp.CompanyCD=@CompanyCD ");

            SqlParameter[] param ={
                                 new SqlParameter("@ID",expID ),
                                 new SqlParameter("@CompanyCD",strCompanyCD )
                                 };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        #region 根据费用申请单ID获取子表费用明细信息
        /// <summary>
        /// 根据费用申请单ID获取子表费用明细信息
        /// </summary>
        /// <param name="expID">费用申请单ID</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns></returns>
        public static DataTable GetExpDetailsByExpID(int expID, string strCompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select ed.ID,ed.SortNo,ed.ExpType,isnull(c.CodeName,'') as ExpTypeName,ed.Amount,ed.ExpRemark ");
            strSql.Append(" from officedba.FeeApplyDetail ed ");
            strSql.Append(" left join officedba.CodeFeeType c on c.ID=ed.ExpType ");
            strSql.Append(" where ed.ExpID=@ExpID ");
            strSql.Append(" order By ed.SortNo ");

            SqlParameter[] param ={
                                 new SqlParameter("@ExpID",expID )
                                 };

            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        #region 获取该公司的费用大类
        /// <summary>
        /// 获取该公司的费用大类
        /// </summary>
        /// <param name="strCompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetExpBigType(string strCompanyCD,string flagType, string flagCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select ID,TypeName from officedba.CodePublicType WHERE CompanyCD=@CompanyCD AND TypeFlag=@TypeFlag AND TypeCode=@TypeCode and UsedStatus=1");

            if (flagType == "1")
            {
               strSql.Append( " or (TypeFlag=5 and TypeCode=6 and UsedStatus='1' and CompanyCD=@CompanyCD)");
            }
            if (flagType == "5")
            {
               strSql.Append(" or (TypeFlag=1 and TypeCode=4 and UsedStatus='1' and CompanyCD=@CompanyCD)");
            }

            SqlParameter[] param ={
                                 new SqlParameter("@CompanyCD",strCompanyCD ),
                                 new SqlParameter("@TypeFlag",flagType),
                                 new SqlParameter("@TypeCode",flagCode)
                                 };

            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        #region 根据公司编号获取该公司的在职员工Name、ID
        /// <summary>
        /// 根据公司编号获取该公司的在职员工Name、ID
        /// </summary>
        /// <param name="strCompany"></param>
        /// <returns></returns>
        public static DataTable GetEmployeeName(string strCompany)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,EmployeeName from officedba.EmployeeInfo where CompanyCD=@CompanyCD and Flag=1");
            SqlParameter[] param ={
                                 new SqlParameter("@CompanyCD",strCompany)
                                 };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        #region 费用申请单打印
        /// <summary>
        /// 获取费用申请打印主表信息
        /// </summary>
        /// <param name="expCode">费用申请编号</param>
        /// <param name="strCompanyCD">公司代码</param>
        /// <returns></returns>
        public static DataTable GetRepExpApply(string expCode, string strCompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from officedba.V_FeeApply where ExpCode=@ExpCode and CompanyCD=@CompanyCD ");
            SqlParameter[] param = { 
                                    new SqlParameter("@ExpCode",expCode),
                                    new SqlParameter("@CompanyCD",strCompanyCD)
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        /// <summary>
        /// 获取费用申请打印子表信息
        /// </summary>
        /// <param name="expCode">费用申请编号</param>
        /// <returns></returns>
        public static DataTable GetRepExpApplyDetail(string expCode, string strCompanyCD)
        {
            int expID = GetExpApplyID(expCode, strCompanyCD);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from officedba.V_FeeApplyDetail where ExpID=@ExpID order by SortNo");
            SqlParameter[] param = { 
                                    new SqlParameter("@ExpID",expID)
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        #region 根据员工ID获取员工所在部门
        /// <summary>
        /// 根据员工ID获取员工所在部门
        /// </summary>
        /// <param name="ApplyorID">员工ID</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns></returns>
        public static DataTable GetDeptIDByEmployeeID(int ApplyorID, string strCompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select isnull(a.DeptID,'') as DeptID,isnull(b.DeptName,'') as DeptName ");
            strSql.Append(" from officedba.EmployeeInfo a ");
            strSql.Append(" left join officedba.DeptInfo b on a.DeptID=b.ID ");
            strSql.Append(" where a.ID=@ID and a.CompanyCD=@CompanyCD ");
            SqlParameter[] param = { 
                                        new SqlParameter("@ID",ApplyorID ),
                                        new SqlParameter("@CompanyCD",strCompanyCD)
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        #region  根据查询条件获取已审批的费用申请单列表
        /// <summary>
        /// 根据查询条件获取已审批的费用申请单列表
        /// </summary>
        /// <param name="expApplyModel">ExpensesApplyModel 实体</param>
        /// <param name="AriseDate1">申请日期</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord">排序</param>
        /// <param name="TotalCount">总记录数</param>
        /// <returns></returns>
        public static DataTable GetExpensesApplyAuditList(ExpensesApplyModel expApplyModel,int empid, DateTime? AriseDate1, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select exp.ID,exp.ExpCode,exp.Title,exp.Applyor,e1.EmployeeName as ApplyorName,convert(varchar(100), exp.AriseDate, 23) as AriseDate,");
            strSql.Append("convert(varchar(100),exp.NeedDate,23) as NeedDate,exp.TotalAmount,exp.DeptID,isnull(d.DeptName,'') as DeptName, ");
            strSql.Append("exp.TransactorID,e2.EmployeeName as TransactorName,exp.Status,expd.ExpType,");
            strSql.Append(" (select CodeName from officedba.CodeFeeType where ID=expd.ExpType) as ExpTypeName ");
            strSql.Append(" ,exp.ProjectID,exp.CustID,isnull(p.ProjectName,'') as ProjectName,isnull(c.CustName,'') as CustName ");
            strSql.Append(" from officedba.FeeApply exp ");
            strSql.Append(" left join officedba.EmployeeInfo e1 on exp.Applyor=e1.ID ");
            strSql.Append(" left join officedba.EmployeeInfo e2 on exp.TransactorID=e2.ID ");
            strSql.Append(" left join officedba.DeptInfo d on exp.DeptID=d.ID ");
            strSql.Append(" left join officedba.ProjectInfo p on exp.ProjectID=p.ID ");
            strSql.Append(" left join officedba.CustInfo c on exp.CustID=c.ID ");
            strSql.Append(" left join officedba.FeeApplyDetail expd on exp.ID=expd.ExpID ");
            strSql.Append(" where exp.CompanyCD=@CompanyCD and exp.IsReimburse='0' ");
            strSql.Append(" and ( charindex('," + empid + ",' , ','+exp.CanViewUser+',')>0 or exp.Creator=" + empid + " OR exp.CanViewUser='' OR exp.CanViewUser is null) ");

            if (expApplyModel.ExpCode != null)
            {
                strSql.Append(" and exp.ExpCode like '%" + expApplyModel.ExpCode + "%' ");
            }
            if (expApplyModel.Title != null)
            {
                strSql.Append(" and exp.Title like '%" + expApplyModel.Title + "%' ");
            }
            if (expApplyModel.TransactorID != -1)
            {
                strSql.Append(" and exp.TransactorID=" + expApplyModel.TransactorID);
            }
            if (expApplyModel.Applyor != -1)
            {
                strSql.Append(" and exp.Applyor=" + expApplyModel.Applyor);
            }
            if (expApplyModel.AriseDate != null && AriseDate1 != null)
            {
                strSql.Append(" and (exp.AriseDate >= '" + expApplyModel.AriseDate.ToString().Trim() + "' and exp.AriseDate <= '" + AriseDate1.ToString().Trim() + "' )");
            }
            if (expApplyModel.Status != null)
            {
                strSql.Append(" and exp.Status =" + expApplyModel.Status);
            }

            SqlParameter[] param ={
                               new SqlParameter("@CompanyCD",expApplyModel.CompanyCD)
                               };
            return SqlHelper.CreateSqlByPageExcuteSql(strSql.ToString(), pageIndex, pageCount, ord, param, ref TotalCount);
        }
        #endregion
    }
}

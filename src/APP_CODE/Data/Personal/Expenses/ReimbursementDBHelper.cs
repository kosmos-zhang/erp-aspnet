/****************************************
 *创建人：何小武 
 *创建日期：2009-9-14
 *描述：费用报销
 ***************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Model.Personal.Expenses;
using XBase.Data.DBHelper;
using XBase.Data.Common;
using XBase.Common;

namespace XBase.Data.Personal.Expenses
{
    public class ReimbursementDBHelper
    {
        #region 添加、删除、修改
        #region 保存费用报销单
        /// <summary>
        /// 保存费用报销单
        /// </summary>
        /// <param name="reimbModel"></param>
        /// <param name="reimbDetailList"></param>
        /// <param name="strMsg"></param>
        public static bool SaveReimbursement(ReimbursementModel reimbModel, List<ReimbDetailsModel> reimbDetailList, out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            strMsg = "";
            //判断单据编号是否存在
            if (NoIsExist(reimbModel.ReimbNo, reimbModel.CompanyCD))
            {
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    int reimbID;
                    reimbID = InsertReimbursement(reimbModel, tran);
                    InsertReimbDetails(reimbDetailList, reimbID, tran);
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
        #region 费用报销单主表数据插入
        /// <summary>
        /// 费用报销单主表数据插入
        /// </summary>
        /// <param name="reimbModel">费用报销单主表实体</param>
        /// <param name="tran">事务</param>
        /// <returns>主表插入后返回的ID</returns>
        public static int InsertReimbursement(ReimbursementModel reimbModel, TransactionManager tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.FeeReturn (");
            strSql.Append(" CompanyCD,ReimbNo,Title,FromType,Applyor,ReimbDate,ReimbDeptID,UserReimbID,CanViewUser,");
            strSql.Append(" ExpAllAmount,ReimbAllAmount,RestoreAllAmount,Status,Creator,CreateDate,Remark,");
            strSql.Append(" ModifiedUserID,ModifiedDate,Confirmor,ConfirmDate,ProjectID,SubjectsNo,CustID,ContactsUnitID,FromTBName,ContactsUnitName,Attachment)");
            strSql.Append(" values (@CompanyCD,@ReimbNo,@Title,@FromType,@Applyor,@ReimbDate,@ReimbDeptID,@UserReimbID,@CanViewUser,");
            strSql.Append(" @ExpAllAmount,@ReimbAllAmount,@RestoreAllAmount,@Status,@Creator,@CreateDate,");
            strSql.Append(" @Remark,@ModifiedUserID,@ModifiedDate,@Confirmor,@ConfirmDate,@ProjectID,@SubjectsNo,@CustID,");
            strSql.Append(" @ContactsUnitID,@FromTBName,@ContactsUnitName,@Attachment); set @Id=@@IDENTITY");

            SqlParameter[] param ={
                                    new SqlParameter("@CompanyCD",reimbModel.CompanyCD ),
                                    new SqlParameter("@ReimbNo",reimbModel.ReimbNo ),
                                    new SqlParameter("@Title",reimbModel.Title ),
                                    new SqlParameter("@FromType",reimbModel.FromType),
                                    new SqlParameter("@Applyor",reimbModel.Applyor ),
                                    new SqlParameter("@ReimbDate",reimbModel.ReimbDate ),
                                    new SqlParameter("@ReimbDeptID",reimbModel.ReimbDeptID ),
                                    new SqlParameter("@UserReimbID",reimbModel.UserReimbID ),
                                    new SqlParameter("@ExpAllAmount",reimbModel.ExpAllAmount ),
                                    new SqlParameter("@ReimbAllAmount",reimbModel.ReimbAllAmount ),
                                    new SqlParameter("@RestoreAllAmount",reimbModel.RestoreAllAmount ),
                                    new SqlParameter("@Status",reimbModel.Status ),
                                    new SqlParameter("@Creator",reimbModel.Creator ),
                                    new SqlParameter("@CreateDate",reimbModel.CreateDate ),
                                    new SqlParameter("@Remark",reimbModel.Remark ),
                                    new SqlParameter("@ModifiedUserID",reimbModel.ModifiedUserID ),
                                    new SqlParameter("@ModifiedDate",reimbModel.ModifiedDate ),
                                    new SqlParameter("@Confirmor",DBNull.Value ),
                                    new SqlParameter("@ConfirmDate",DBNull.Value),
                                    new SqlParameter("@Id",SqlDbType.Int,6),
                                    new SqlParameter("@CanViewUser",reimbModel.CanViewUser),
                                    new SqlParameter("@ProjectID",reimbModel.ProjectID),
                                    new SqlParameter("@SubjectsNo",reimbModel.SubjectsNo),
                                    new SqlParameter("@CustID",reimbModel.CustID),
                                    new SqlParameter("@ContactsUnitID",reimbModel.ContactsUnitID),
                                    new SqlParameter("@FromTBName",reimbModel.FromTBName),
                                    new SqlParameter("@ContactsUnitName",reimbModel.ContactsUnitName),
                                    new SqlParameter("@Attachment",reimbModel.Attachment)
                                  };
            param[19].Direction = ParameterDirection.Output;
            foreach (SqlParameter para in param)
            {
                if (para.Value == null || para.Value.ToString() == "-1")
                {
                    para.Value = DBNull.Value;
                }
            }
            SqlHelper.ExecuteTransSql(strSql.ToString(), param);
            int Id = Convert.ToInt32(param[19].Value);
            return Id;
        }
        #endregion
        #region 费用报销明细表数据插入
        /// <summary>
        /// 费用报销明细表数据插入
        /// </summary>
        /// <param name="reimbDetailList">明细实体列表</param>
        /// <param name="reimbID">报销单ID</param>
        /// <param name="tran"></param>
        public static void InsertReimbDetails(List<ReimbDetailsModel> reimbDetailList, int reimbID, TransactionManager tran)
        {
            foreach (ReimbDetailsModel reimbDetailModel in reimbDetailList)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into officedba.FeeReturnDetail (");
                strSql.Append("reimbID,SortNo,ExpID,ExpAmount,ReimbAmount,RestoreAmount,Notes,FeeNameID,SubjectsNo)values(");
                strSql.Append("@ReimbID,@SortNo,@ExpID,@ExpAmount,@ReimbAmount,@RestoreAmount,@Notes,@FeeNameID,@SubjectsNo)");

                SqlParameter[] param ={
                                      new SqlParameter("@ReimbID",reimbID),
                                      new SqlParameter("@SortNo",reimbDetailModel.SortNo ),
                                      new SqlParameter("@ExpID",reimbDetailModel.ExpID ),
                                      new SqlParameter("@ExpAmount",reimbDetailModel.ExpAmount ),
                                      new SqlParameter("@ReimbAmount",reimbDetailModel.ReimbAmount ),
                                      new SqlParameter("@RestoreAmount",reimbDetailModel.RestoreAmount ),
                                      new SqlParameter("@Notes",reimbDetailModel.Notes ),
                                      new SqlParameter("@FeeNameID",reimbDetailModel.FeeNameID),
                                      new SqlParameter("@SubjectsNo",reimbDetailModel.SubjectsNo)
                                      };
                foreach (SqlParameter para in param)
                {
                    if (para.Value == null || para.Value.ToString()=="-1")
                    {
                        para.Value = DBNull.Value;
                    }
                }
                SqlHelper.ExecuteTransSql(strSql.ToString(), param);
            }
        }
        #endregion

        #region 修改费用报销单
        /// <summary>
        /// 修改费用报销单
        /// </summary>
        /// <param name="reimbModel">费用报销主表实体</param>
        /// <param name="reimbDetailModelList">费用报销明细表实体列表</param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public static bool UpdateReimbursement(ReimbursementModel reimbModel, List<ReimbDetailsModel> reimbDetailModelList, out string strMsg)
        {
            bool isSucc = false;//是否修改成功
            strMsg = "";
            if (IsUpdate(reimbModel.ReimbNo, reimbModel.CompanyCD))
            {
                string strSql = "delete from officedba.FeeReturnDetail where  ReimbID=@ReimbID ";
                SqlParameter[] paras = { new SqlParameter("@ReimbID", reimbModel.ID) };
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    UpdateReimbursement(reimbModel, tran);
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), paras);
                    InsertReimbDetails(reimbDetailModelList, reimbModel.ID, tran);
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
        #endregion
        #region 修改费用报销主表信息
        /// <summary>
        /// 修改费用报销主表信息
        /// </summary>
        /// <param name="reimbModel">费用报销主表实体</param>
        /// <param name="tran">事务</param>
        public static void UpdateReimbursement(ReimbursementModel reimbModel,TransactionManager tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.FeeReturn set ");
            strSql.Append(" Title=@Title,Applyor=@Applyor,ReimbDate=@ReimbDate,ReimbDeptID=@ReimbDeptID,UserReimbID=@UserReimbID,");
            strSql.Append(" FromType=@FromType,CanViewUser=@CanViewUser,");
            strSql.Append(" ExpAllAmount=@ExpAllAmount,ReimbAllAmount=@ReimbAllAmount,RestoreAllAmount=@RestoreAllAmount,");
            strSql.Append(" Remark=@Remark,ModifiedUserID=@ModifiedUserID,ModifiedDate=@ModifiedDate,ProjectID=@ProjectID,");
            strSql.Append(" SubjectsNo=@SubjectsNo,CustID=@CustID,ContactsUnitID=@ContactsUnitID,FromTBName=@FromTBName,ContactsUnitName=@ContactsUnitName,Attachment=@Attachment ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] param ={
                                      new SqlParameter("@ID",reimbModel.ID ),
                                      new SqlParameter("@Title",reimbModel.Title ),
                                      new SqlParameter("@FromType",reimbModel.FromType),
                                      new SqlParameter("@Applyor",reimbModel.Applyor),
                                      new SqlParameter("@ReimbDate",reimbModel.ReimbDate),
                                      new SqlParameter("@ReimbDeptID",reimbModel.ReimbDeptID),
                                      new SqlParameter("@UserReimbID",reimbModel.UserReimbID),
                                      new SqlParameter("@ExpAllAmount",reimbModel.ExpAllAmount),
                                      new SqlParameter("@ReimbAllAmount",reimbModel.ReimbAllAmount),
                                      new SqlParameter("@RestoreAllAmount",reimbModel.RestoreAllAmount),
                                      new SqlParameter("@Remark",reimbModel.Remark),
                                      new SqlParameter("@ModifiedUserID",reimbModel.ModifiedUserID),
                                      new SqlParameter("@ModifiedDate",reimbModel.ModifiedDate),
                                      new SqlParameter("@CanViewUser",reimbModel.CanViewUser),
                                      new SqlParameter("@ProjectID",reimbModel.ProjectID),
                                      new SqlParameter("@SubjectsNo",reimbModel.SubjectsNo),
                                      new SqlParameter("@CustID",reimbModel.CustID),
                                      new SqlParameter("@ContactsUnitID",reimbModel.ContactsUnitID),
                                      new SqlParameter("@FromTBName",reimbModel.FromTBName),
                                      new SqlParameter("@ContactsUnitName",reimbModel.ContactsUnitName),
                                      new SqlParameter("@Attachment",reimbModel.Attachment)
                                  };
            foreach (SqlParameter para in param)
            {
                if (para.Value == null || para.Value.ToString() == "-1")
                {
                    para.Value = DBNull.Value;
                }
            }
            SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), param);
        }
        #endregion

        #region 根据ID删除费用报销单
        /// <summary>
        /// 根据ID删除费用报销单
        /// </summary>
        /// <param name="reimbIDs">报销单ID</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <param name="strMsg">返回信息</param>
        /// <param name="strFieldText"></param>
        /// <returns></returns>
        public static bool DelReimbursementByIDs(string reimbIDs, string strCompanyCD, out string strMsg, out string strFieldText)
        {
            bool isSucc = false;
            string allReimbID = "";
            strMsg = "";
            strFieldText = "";
            bool bTemp = false;//单据是否可以被删除
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            TransactionManager tran = new TransactionManager();

            string[] strReimbID = null;
            strReimbID = reimbIDs.Split(',');

            for (int i = 0; i < strReimbID.Length; i++)
            {
                if (!IsFlow(strReimbID[i], strCompanyCD))
                {
                    strFieldText += "单据：" + strReimbID[i] + "|";
                    strMsg += "非制单状态单据不允许删除！|";
                    bTemp = true;
                }
                strReimbID[i] = "'" + strReimbID[i] + "'";
                sb.Append(strReimbID[i]);
            }

            allReimbID = sb.ToString().Replace("''", "','");
            if (!bTemp)
            {
                tran.BeginTransaction();
                try
                {
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.FeeReturn WHERE ID IN ( " + allReimbID + " ) and CompanyCD='" + strCompanyCD + "'", null);
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.FeeReturnDetail WHERE ReimbID IN ( " + allReimbID + " ) ", null);
                    /*删除审批流程相关信息Start*/
                    FlowDBHelper.DelFlowInfo(tran, allReimbID, strCompanyCD, "1", "5"); 
                    //SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "delete from officedba.FlowTaskList where FlowInstanceID in (select ID from officedba.FlowInstance where billid in ("+allReimbID+") and billtypeflag='1' and billtypecode='5' and companycd='"+strCompanyCD+"')", null);
                    //SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "delete from officedba.FlowTaskHistory where FlowInstanceID in (select ID from officedba.FlowInstance where billid in ("+allReimbID+") and billtypeflag='1' and billtypecode='5' and companycd='"+strCompanyCD+"')", null);
                    //SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "delete from officedba.FlowInstance where billid in (" + allReimbID + ") and billtypeflag='1' and billtypecode='5'  and CompanyCD='" + strCompanyCD + "'", null);
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

        #endregion

        #region 确认、取消确认、报废报销单
        #region 确认报销单
        /// <summary>
        /// 确认报销单
        /// </summary>
        /// <param name="reimbNo">报销单ID</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <param name="EmployeeName">当前登录人名称</param>
        /// <param name="EmployeeID">当前登录人ID</param>
        /// <param name="UserID">当前登录人用户ID</param>
        /// <param name="strMsg">返回信息</param>
        /// <returns>返回是否成功标志</returns>
        public static bool ConfirmReimbursement(ReimbursementModel reimbModel,List<ReimbDetailsModel> reimbDetailModelList,string strExpID, string strCompanyCD, string EmployeeName, int EmployeeID, string UserID, out string strMsg)
        {
            strMsg = "";
            bool isSuc = false;
            StringBuilder strSql = new StringBuilder();

            if (isHandle(reimbModel.ReimbNo, "1", strCompanyCD))
            {
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    //判断是否启用自动生成凭证
                    //((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsApply;//自动审核登帐
                    //((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsVoucher;//自动生成凭证
                    if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsVoucher)
                    {
                        if (isExistSubjectNos(reimbModel.SubjectsNo, reimbDetailModelList))
                        {
                            #region 自动生成凭证并确认
                            XBase.Model.Office.FinanceManager.AttestBillModel Model = new XBase.Model.Office.FinanceManager.AttestBillModel();//凭证主表实例
                            ArrayList DetailList = new ArrayList();//凭证明细数组
                            Model.CompanyCD = strCompanyCD;
                            Model.FromName = "";
                            Model.Attachment = 1;//附件数
                            Model.AttestName = "记账凭证";//凭证名称
                            Model.VoucherDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));//凭证日期
                            Model.AttestNo = "记-" + XBase.Data.Office.FinanceManager.VoucherDBHelper.GetMaxAttestNo(strCompanyCD, DateTime.Now.ToString("yyyy-MM-dd"));//凭证号
                            Model.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;//制单人
                            Model.CreateDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));//制单日期
                            Model.FromTbale = "officedba.FeeReturn";//来源表名
                            Model.FromValue = reimbModel.ID.ToString();//来源表主键

                            XBase.Model.Office.FinanceManager.AttestBillDetailsModel DetailModel = new XBase.Model.Office.FinanceManager.AttestBillDetailsModel();//凭证明细表实例
                            DetailModel.Abstract = reimbModel.Remark == null ? "" : reimbModel.Remark;// dt.Rows[0]["Abstract"].ToString();//摘要
                            DetailModel.CurrencyTypeID = Convert.ToInt32(XBase.Data.Office.FinanceManager.CurrTypeSettingDBHelper.GetMasterCurrency(strCompanyCD).Rows[0]["ID"].ToString()); //币种
                            DetailModel.ExchangeRate = 1;//汇率
                            DetailModel.SubjectsCD = reimbModel.SubjectsNo == null ? "" : reimbModel.SubjectsNo;//科目编号
                            DetailModel.OriginalAmount = (decimal)reimbModel.ReimbAllAmount;//原币金额
                            DetailModel.DebitAmount = 0;//借方金额
                            DetailModel.CreditAmount = (decimal)reimbModel.ReimbAllAmount;//贷方金额

                            DetailModel.SubjectsDetails = "";
                            DetailModel.FormTBName = "";
                            DetailModel.FileName = "";
                            string Auciliary = XBase.Data.Office.FinanceManager.VoucherDBHelper.GetSubjectsAuciliaryCD(reimbModel.SubjectsNo, strCompanyCD);

                            if (Auciliary == "供应商" || Auciliary == "客户" || Auciliary == "职员")
                            {
                                DetailModel.SubjectsDetails = reimbModel.UserReimbID.ToString();//辅助核算
                                if (Auciliary == "供应商")
                                {
                                    DetailModel.FormTBName = "officedba.ProviderInfo";
                                    DetailModel.FileName = "CustName";
                                }
                                else if (Auciliary == "客户")
                                {
                                    DetailModel.FormTBName = "officedba.CustInfo";
                                    DetailModel.FileName = "CustName";
                                }
                                else
                                {
                                    DetailModel.FormTBName = "officedba.EmployeeInfo";
                                    DetailModel.FileName = "EmployeeName";
                                }
                            }
                            DetailList.Add(DetailModel);

                            //根据凭证模板构建凭证明细数组
                            foreach (ReimbDetailsModel RDetailM in reimbDetailModelList)
                            {
                                XBase.Model.Office.FinanceManager.AttestBillDetailsModel DetailM = new XBase.Model.Office.FinanceManager.AttestBillDetailsModel();//凭证明细表实例
                                DetailM.Abstract = RDetailM.Notes;//摘要
                                DetailM.CurrencyTypeID = Convert.ToInt32(XBase.Data.Office.FinanceManager.CurrTypeSettingDBHelper.GetMasterCurrency(strCompanyCD).Rows[0]["ID"].ToString());//币种
                                DetailM.ExchangeRate = 1;//汇率
                                DetailM.SubjectsCD = RDetailM.SubjectsNo;//科目编号
                                DetailM.OriginalAmount = RDetailM.ReimbAmount;//原币金额
                                DetailM.DebitAmount = RDetailM.ReimbAmount;//借方金额
                                DetailM.CreditAmount = 0;//贷方金额

                                DetailM.SubjectsDetails = "";
                                DetailM.FormTBName = "";
                                DetailM.FileName = "";

                                DetailList.Add(DetailM);
                            }

                            //自动生成凭证并根据IsApply判断是否登帐 --生成成功
                            string isApply = "0";//自动审核登帐
                            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsApply)
                            {
                                isApply = "1";
                            }
                            int BillID = 0;
                            if (XBase.Data.Office.FinanceManager.VoucherDBHelper.InsertIntoAttestBill(Model, DetailList, out BillID, isApply))
                            {
                                strSql.Append(" update officedba.FeeReturn set ");
                                strSql.Append(" Confirmor=@Confirmor ,Status=@Status,ConfirmDate=getdate()");
                                strSql.Append(" ,ModifiedUserID=@ModifiedUserID,ModifiedDate=getdate() ");
                                strSql.Append(" ,IsAccount='1',Accountor=@Confirmor,AccountDate=getdate(),AttestBillID=@BillID ");
                                strSql.Append(" where CompanyCD= @CompanyCD and ReimbNo=@ReimbNo ");

                                SqlParameter[] param ={
                                            new SqlParameter("@Confirmor",EmployeeID),
                                            new SqlParameter("@Status","2"),
                                            new SqlParameter("@CompanyCD",strCompanyCD),
                                            new SqlParameter("@ReimbNo",reimbModel.ReimbNo),
                                            new SqlParameter("@ModifiedUserID",UserID),
                                            new SqlParameter("@BillID",BillID)
                                         };

                                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), param);

                                string[] expID = strExpID.Split(',');
                                for (int j = 0; j < expID.Length; j++)
                                {
                                    if (expID[j] != "")
                                    {
                                        SqlParameter[] param2 = { 
                                                    new SqlParameter("@ExpID",int.Parse(expID[j]))
                                                    };
                                        SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "update officedba.FeeApply set IsReimburse='1' where ID=@ExpID", param2);
                                    }
                                }

                                tran.Commit();
                                isSuc = true;
                                strMsg = "确认并生产凭证成功！" + "@" + EmployeeName + "@" + System.DateTime.Now + "@" + BillID;

                            }
                            #endregion
                        }
                        else
                        { 
                            strSql.Append(" update officedba.FeeReturn set ");
                            strSql.Append(" Confirmor=@Confirmor ,Status=@Status,ConfirmDate=getdate()");
                            strSql.Append(" ,ModifiedUserID=@ModifiedUserID,ModifiedDate=getdate() ");
                            strSql.Append(" where CompanyCD= @CompanyCD and ReimbNo=@ReimbNo ");

                            SqlParameter[] param ={
                                                new SqlParameter("@Confirmor",EmployeeID),
                                                new SqlParameter("@Status","2"),
                                                new SqlParameter("@CompanyCD",strCompanyCD),
                                                new SqlParameter("@ReimbNo",reimbModel.ReimbNo),
                                                new SqlParameter("@ModifiedUserID",UserID)
                                             };

                            SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), param);

                            string[] expID = strExpID.Split(',');
                            for (int j = 0; j < expID.Length; j++)
                            {
                                if (expID[j] != "")
                                {
                                    SqlParameter[] param2 = { 
                                                        new SqlParameter("@ExpID",int.Parse(expID[j]))
                                                        };
                                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "update officedba.FeeApply set IsReimburse='1' where ID=@ExpID", param2);
                                }
                            }

                            tran.Commit();
                            isSuc = true;
                            strMsg = "警告！确认成功！生成凭证失败：结算科目或者费用科目为空！" + "@" + EmployeeName + "@" + System.DateTime.Now+"@"+0;
                        }
                        

                    }
                    else
                    {
                        strSql.Append(" update officedba.FeeReturn set ");
                        strSql.Append(" Confirmor=@Confirmor ,Status=@Status,ConfirmDate=getdate()");
                        strSql.Append(" ,ModifiedUserID=@ModifiedUserID,ModifiedDate=getdate() ");
                        strSql.Append(" where CompanyCD= @CompanyCD and ReimbNo=@ReimbNo ");

                        SqlParameter[] param ={
                                            new SqlParameter("@Confirmor",EmployeeID),
                                            new SqlParameter("@Status","2"),
                                            new SqlParameter("@CompanyCD",strCompanyCD),
                                            new SqlParameter("@ReimbNo",reimbModel.ReimbNo),
                                            new SqlParameter("@ModifiedUserID",UserID)
                                         };

                        SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), param);

                        string[] expID = strExpID.Split(',');
                        for (int j = 0; j < expID.Length; j++)
                        {
                            if (expID[j] != "")
                            {
                                SqlParameter[] param2 = { 
                                                    new SqlParameter("@ExpID",int.Parse(expID[j]))
                                                    };
                                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "update officedba.FeeApply set IsReimburse='1' where ID=@ExpID", param2);
                            }
                        }

                        tran.Commit();
                        isSuc = true;
                        strMsg = "确认成功！" + "@" + EmployeeName + "@" + System.DateTime.Now+"@"+0;
                    }
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
        #region 取消确认报销单
        /// <summary>
        /// 取消确认报销单
        /// </summary>
        /// <param name="reimbNo">报销单ID</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <param name="UserID">当前登录人用户ID</param>
        /// <param name="strMsg">返回信息</param>
        /// <returns>返回是否成功标志</returns>
        public static bool UnConfirmReimbursement(string reimbNo, string strExpID,string AttestBillID,string strCompanyCD, string UserID, out string strMsg)
        {
            strMsg = "";
            bool isSuc = false;
            StringBuilder strSql = new StringBuilder();
            int reimbID = GetReimbursementID(reimbNo, strCompanyCD);

            //判断单据是否是执行状态，不是执行状态不给取消确认
            if (isHandle(reimbNo, "2", strCompanyCD))
            {
                //凭证ID不为空时，反确认时要对已生成的凭证作相应的操作
                if (AttestBillID != "" && AttestBillID != "0")
                {
                    //判断是否已审核或已登帐，若已审核或已登帐则不给取消确定
                    if (isAttestBilled(AttestBillID))
                    {
                        TransactionManager tran = new TransactionManager();
                        tran.BeginTransaction();
                        try
                        {
                            strSql.Append(" update officedba.FeeReturn set ");
                            strSql.Append(" Confirmor=@Confirmor ,Status=@Status,ConfirmDate=@ConfirmDate ");
                            strSql.Append(" ,ModifiedUserID=@ModifiedUserID,ModifiedDate=getdate() ");
                            strSql.Append(" ,AttestBillID=@AttestBillID,IsAccount=@IsAccount,AccountDate=@AccountDate,Accountor=@Accountor ");
                            strSql.Append(" where CompanyCD= @CompanyCD and ReimbNo=@ReimbNo ");

                            SqlParameter[] param ={
                                                    new SqlParameter("@Confirmor",DBNull.Value ),
                                                    new SqlParameter("@ConfirmDate",DBNull.Value ),
                                                    new SqlParameter("@Status","1"),
                                                    new SqlParameter("@CompanyCD",strCompanyCD),
                                                    new SqlParameter("@ReimbNo",reimbNo),
                                                    new SqlParameter("@ModifiedUserID",UserID),
                                                    new SqlParameter("@IsAccount", "0"),
                                                    new SqlParameter("@AccountDate", DBNull.Value),
                                                    new SqlParameter("@Accountor", DBNull.Value),
                                                    new SqlParameter("@AttestBillID",DBNull.Value)
                                                 };
                            //删除凭证
                            XBase.Data.Office.FinanceManager.VoucherDBHelper.DeleteAttestBillInfo(AttestBillID);

                            FlowDBHelper.OperateCancelConfirm(strCompanyCD, 1, 5, reimbID, UserID, tran);//撤销审批
                            SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), param);
                            string[] expID = strExpID.Split(',');
                            for (int j = 0; j < expID.Length; j++)
                            {
                                if (expID[j] != "")
                                { 
                                    SqlParameter[] param2 = { 
                                                            new SqlParameter("@ExpID",int.Parse(expID[j]))
                                                            };
                                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "update officedba.FeeApply set IsReimburse='0' where ID=@ExpID", param2);
                                }
                            }

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
                    {
                        strMsg = "该单据已审核或者已登帐，不可取消确认！";
                        isSuc = false;  
                    }
                }
                else
                { 
                    TransactionManager tran = new TransactionManager();
                    tran.BeginTransaction();
                    try
                    {
                        strSql.Append(" update officedba.FeeReturn set ");
                        strSql.Append(" Confirmor=@Confirmor ,Status=@Status,ConfirmDate=@ConfirmDate ");
                        strSql.Append(" ,ModifiedUserID=@ModifiedUserID,ModifiedDate=getdate() ");
                        strSql.Append(" where CompanyCD= @CompanyCD and ReimbNo=@ReimbNo ");

                        SqlParameter[] param ={
                                                new SqlParameter("@Confirmor",DBNull.Value ),
                                                new SqlParameter("@ConfirmDate",DBNull.Value ),
                                                new SqlParameter("@Status","1"),
                                                new SqlParameter("@CompanyCD",strCompanyCD),
                                                new SqlParameter("@ReimbNo",reimbNo),
                                                new SqlParameter("@ModifiedUserID",UserID)
                                             };

                        FlowDBHelper.OperateCancelConfirm(strCompanyCD, 1, 5, reimbID, UserID, tran);//撤销审批
                        SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), param);
                        string[] expID = strExpID.Split(',');
                        for (int j = 0; j < expID.Length; j++)
                        {
                            if (expID[j] != "")
                            { 
                                SqlParameter[] param2 = { 
                                                        new SqlParameter("@ExpID",int.Parse(expID[j]))
                                                        };
                                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "update officedba.FeeApply set IsReimburse='0' where ID=@ExpID", param2);
                            }
                        }

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
            }
            else
            {//已经被其他人确认
                strMsg = "该单据已被其他用户取消确认或已报销，不可取消确认！";
                isSuc = false;
            }
            return isSuc;
        }
        #endregion
        #region 报废报销单
        /// <summary>
        /// 报废报销单
        /// </summary>
        /// <param name="reimbNo">报销单编号</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <param name="UserID">当前用户ID</param>
        /// <param name="strMsg">返回信息</param>
        /// <returns>返回是否成功标志</returns>
        public static bool ScrapReimbursement(string reimbNo, string strExpID,string AttestBillID,string strCompanyCD, string UserID, out string strMsg)
        {
            StringBuilder strSql = new StringBuilder();
            bool isSuc = false;
            strMsg = "";
            //判断单据是否为确认状态，且未报销的单据
            if (isHandle(reimbNo, "2", strCompanyCD))
            {
                //凭证ID不为空时，作废时要对已生产的凭证作相应的操作
                if (AttestBillID != "" && AttestBillID != "0")
                {
                    //判断是否已审核或已登记，若已审核或已登记则不给作废
                    if (isAttestBilled(AttestBillID))
                    {
                        TransactionManager tran = new TransactionManager();
                        tran.BeginTransaction();
                        try
                        {
                            strSql.Append("update  officedba.FeeReturn set Status='4' ");
                            strSql.Append(" , ModifiedDate=getdate() ,ModifiedUserID=@UserID ");
                            strSql.Append(" ,AttestBillID=@AttestBillID,IsAccount=@IsAccount,AccountDate=@AccountDate,Accountor=@Accountor ");
                            strSql.Append(" WHERE ReimbNo = @ReimbNo and CompanyCD=@CompanyCD ");

                            SqlParameter[] param = { 
                                                   new SqlParameter("@UserID", UserID),
                                                   new SqlParameter("@CompanyCD", strCompanyCD),
                                                   new SqlParameter("@ReimbNo", reimbNo),
                                                   new SqlParameter("@IsAccount", "0"),
                                                   new SqlParameter("@AccountDate", DBNull.Value),
                                                   new SqlParameter("@Accountor", DBNull.Value),
                                                   new SqlParameter("@AttestBillID", DBNull.Value)
                                                   };

                            //删除凭证
                            XBase.Data.Office.FinanceManager.VoucherDBHelper.DeleteAttestBillInfo(AttestBillID);

                            SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), param);
                            string[] expID = strExpID.Split(',');
                            for (int j = 0; j < expID.Length; j++)
                            {
                                if (expID[j] != "")
                                {
                                    SqlParameter[] param2 = { 
                                                            new SqlParameter("@ExpID",int.Parse(expID[j]))
                                                            };
                                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "update officedba.FeeApply set IsReimburse='0' where ID=@ExpID", param2);
                                }
                            }

                            tran.Commit();
                            isSuc = true;
                            strMsg = "费用报销单作废成功！";
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();

                            isSuc = false;
                            strMsg = "费用报销单作废失败，请联系系统管理员！";
                            throw ex;
                        }
                    }
                    else
                    {
                        isSuc = false;
                        strMsg = "该单据已审核或已登帐，不可作废！";
                    }
                }
                else
                { 
                    TransactionManager tran = new TransactionManager();
                    tran.BeginTransaction();
                    try
                    {
                        strSql.Append("update  officedba.FeeReturn set Status='4' ");
                        strSql.Append(" , ModifiedDate=getdate() ,ModifiedUserID=@UserID ");
                        strSql.Append(" WHERE ReimbNo = @ReimbNo and CompanyCD=@CompanyCD ");

                        SqlParameter[] param = { 
                                               new SqlParameter("@UserID", UserID),
                                               new SqlParameter("@CompanyCD", strCompanyCD),
                                               new SqlParameter("@ReimbNo", reimbNo)
                                               };

                        SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), param);
                        string[] expID = strExpID.Split(',');
                        for (int j = 0; j < expID.Length; j++)
                        {
                            if (expID[j] != "")
                            { 
                                SqlParameter[] param2 = { 
                                                        new SqlParameter("@ExpID",int.Parse(expID[j]))
                                                        };
                                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "update officedba.FeeApply set IsReimburse='0' where ID=@ExpID", param2);
                            }
                        }

                        tran.Commit();
                        isSuc = true;
                        strMsg = "费用报销单作废成功！";
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();

                        isSuc = false;
                        strMsg = "费用报销单作废失败，请联系系统管理员！";
                        throw ex;
                    }                    
                }

            }
            else
            {
                isSuc = false;
                strMsg = "该单据不可作废或已被其他用户作废，不可作废！";
            }
            return isSuc;
        }
        #endregion
        #endregion

        #region 判断结算项目编号和明细中的项目编号是否为空，若有为空的则返回false
        /// <summary>
        /// 判断结算项目编号和明细中的项目编号是否为空，若有为空的则返回false
        /// </summary>
        /// <param name="SubjectsNo"></param>
        /// <param name="reimbDetailModelList"></param>
        /// <returns></returns>
        private static bool isExistSubjectNos(string SubjectsNo, List<ReimbDetailsModel> reimbDetailModelList)
        {
            bool returnValue = true;
            if (SubjectsNo == "" || SubjectsNo==null)
            {
                returnValue = false;
            }
            else
            {
                foreach (ReimbDetailsModel model in reimbDetailModelList)
                {
                    if (model.SubjectsNo == "" || model.SubjectsNo==null)
                    {
                        returnValue = false;
                        break;
                    }
                }
            }
            return returnValue;
        }
        #endregion

        #region 判断是否已审核 或者 登记凭证
        /// <summary>
        /// 判断是否已审核 或者 登记凭证
        /// </summary>
        /// <param name="attestBillID">凭证ID</param>
        /// <returns>false：已审核或登记凭证；true：未审核和登记凭证</returns>
        private static bool isAttestBilled(string attestBillID)
        {
            bool isAttested = false;
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select count(1) from Officedba.AttestBill where ");
            strSql.AppendLine(" status=0 and ID=@BillID ");
            SqlParameter[] param = { 
                                   new SqlParameter("@BillID",attestBillID)
                                   };
            int iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql.ToString(), param));
            if (iCount > 0)
            {
                isAttested = true;
            }
            return isAttested;
        }
        #endregion

        #region 根据单据状态判断是否可以执行该操作（报废、确认、取消确认）
        /// <summary>
        /// 根据单据状态判断是否可以执行该操作
        /// </summary>
        /// <param name="reimbNo">费用报销单编号</param>
        /// <param name="billStatus">单据状态</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns>返回true表示能执行该操作</returns>
        public static bool isHandle(string reimbNo, string billStatus, string strCompanyCD)
        {
            bool isSuc = false;
            int iCount = 0;
            string strSql = string.Empty;

            strSql = "select count(1) from officedba.FeeReturn where ReimbNo = @ReimbNo and CompanyCD=@CompanyCD and Status=@Status ";

            SqlParameter[] param ={
                                 new SqlParameter("@ReimbNo", reimbNo),
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

        #region 判断单据编号是否存在
        /// <summary>
        /// 判断单据编号是否存在
        /// </summary>
        /// <param name="ReimbNo">单据编号</param>
        /// <returns>返回true时表示不存在</returns>
        private static bool NoIsExist(string ReimbNo, string strCompanyCD)
        {
            bool isSuc = false;
            int iCount = 0;

            string strSql = string.Empty;
            SqlParameter[] paras = { 
                                       new SqlParameter("@ReimbNo", ReimbNo),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += " select count(1) from officedba.FeeReturn ";
            strSql += " WHERE  (ReimbNo = @ReimbNo) AND (CompanyCD = @CompanyCD) ";
            iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, paras));
            if (iCount == 0)
            {
                isSuc = true;
            }

            return isSuc;
        }
        #endregion

        #region 单据是否是制单状态
        /// <summary>
        /// 单据是否是制单状态
        /// </summary>
        /// <param name="ReimbID">单据ID</param>
        /// <param name="strCompanyCD">公司代码</param>
        /// <returns>返回true时表示都是制单状态</returns>
        private static bool IsFlow(string ReimbID, string strCompanyCD)
        {
            bool isSuc = false;
            int iCount = 0;
            string strSql = string.Empty;
            SqlParameter[] paras = { 
                                       new SqlParameter("@ID", ReimbID),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += "select count(1) from officedba.FeeReturn where ID=@ID and  CompanyCD = @CompanyCD and Status<>1";

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

        #region 根据报销单编号获取报销单ID
        /// <summary>
        /// 根据报销单编号获取报销单ID
        /// </summary>
        /// <param name="reimbNo">报销单编号</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns>报销单ID</returns>
        public static int GetReimbursementID(string reimbNo, string strCompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID from officedba.FeeReturn ");
            strSql.Append(" where ReimbNo=@ReimbNo and CompanyCD=@CompanyCD ");

            SqlParameter[] param = { 
                                       new SqlParameter("@ReimbNo",reimbNo ),
                                       new SqlParameter("@CompanyCD",strCompanyCD )
                                   };
            return (int)SqlHelper.ExecuteScalar(strSql.ToString(), param);
        }
        #endregion

        #region 根据单据状态判断单据是否可以被修改
        /// <summary>
        /// 根据单据状态判断单据是否可以被修改
        /// </summary>
        /// <param name="ReimbNo"></param>
        /// <returns>返回true时表示可以修改</returns>
        private static bool IsUpdate(string ReimbNo, string strCompanyCD)
        {
            bool isSuc = false;
            string strStatus = string.Empty;
            string strSql = string.Empty;
            SqlParameter[] paras = { 
                                       new SqlParameter("@ReimbNo", ReimbNo),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += "select top 1 Status from officedba.FeeReturn where (ReimbNo = @ReimbNo) AND (CompanyCD = @CompanyCD) and Status=1  ORDER BY ModifiedDate DESC ";
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

        #region 根据查询条件获取费用报销单列表
        /// <summary>
        /// 根据查询条件获取费用报销单列表
        /// 导出列表页调用此方法
        /// </summary>
        /// <param name="reimbModel">费用报销主表实体</param>
        /// <param name="ReimbDate1">报销日期</param>
        /// <param name="FlowStatus">审批状态</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetReimbursementList(ReimbursementModel reimbModel, int empid,DateTime? ReimbDate1, string FlowStatus,decimal? ReimbAmount1, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select reimb.ID,reimb.ReimbNo,reimb.Title,reimb.Applyor,e1.EmployeeName as ApplyorName,");
            strSql.Append(" reimb.UserReimbID,e2.EmployeeName as UserReimbName,");
            strSql.Append(" convert(varchar(100), reimb.ReimbDate, 23) as ReimbDate, ");
            strSql.Append(" reimb.ExpAllAmount,reimb.ReimbAllAmount,reimb.RestoreAllAmount,reimb.Status ");
            strSql.Append(" ,isnull( case f.FlowStatus when '1' then '待审批' when '2' then '审批中'");
            strSql.Append(" when '3'  then '审批通过' when '4' then '审批不通过' when '5' then '撤消审批' end,'') as FlowStatus ");
            strSql.Append(" ,reimb.Confirmor,e3.EmployeeName as ConfirmorName,convert(varchar(100),reimb.ConfirmDate,23) as ConfirmDate ");
            strSql.Append(" ,reimb.ProjectID,p.ProjectName,reimb.CustID,reimb.ContactsUnitID, ");
            strSql.Append(" isnull(c.CustName,'') as CustName,isnull(reimb.ContactsUnitName,'') as ContactsUnitName ");
            strSql.Append(" from officedba.FeeReturn reimb ");
            strSql.Append(" left join officedba.EmployeeInfo e1 on reimb.Applyor=e1.ID ");
            strSql.Append(" left join officedba.EmployeeInfo e2 on reimb.UserReimbID=e2.ID ");
            strSql.Append(" left join officedba.EmployeeInfo e3 on reimb.Confirmor=e3.ID ");
            strSql.Append(" left join officedba.ProjectInfo p on p.ID=reimb.ProjectID ");
            strSql.Append(" left join officedba.CustInfo c on c.ID=reimb.CustID ");
            strSql.Append(" left join officedba.FlowInstance f on reimb.CompanyCD=f.CompanyCD and f.BillTypeFlag=1 ");
            strSql.Append(" and f.BillTypeCode=5 and f.BillNo=reimb.ReimbNo ");
            strSql.Append(" and f.ID=(select max(ID) from officedba.FlowInstance as f2 where reimb.ID=f2.BillID and f2.BillTypeFlag=1 and f2.BillTypeCode=5 )");
            strSql.Append(" where reimb.CompanyCD=@CompanyCD ");
            strSql.Append(" and ( charindex('," + empid + ",' , ','+reimb.CanViewUser+',')>0 or reimb.Creator=" + empid + " OR reimb.CanViewUser='' OR reimb.CanViewUser is null) ");
            if (reimbModel.ReimbNo != null)
            {
                strSql.Append(" and reimb.ReimbNo like '%" + reimbModel.ReimbNo + "%' ");
            }
            if (reimbModel.Title != null)
            {
                strSql.Append(" and reimb.Title like '%" + reimbModel.Title + "%' ");
            }
            if (reimbModel.Applyor != -1)
            {
                strSql.Append(" and reimb.Applyor=" + reimbModel.Applyor);
            }
            if (reimbModel.ReimbDate != null && ReimbDate1 != null)
            {
                strSql.Append(" and (reimb.ReimbDate >= '" + reimbModel.ReimbDate.ToString().Trim() + "' and reimb.ReimbDate <= '" + ReimbDate1.ToString().Trim() + "' )");
            }
            if (reimbModel.Status != null)
            {
                strSql.Append(" and reimb.Status =" + reimbModel.Status);
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
            if (reimbModel.UserReimbID != -1)
            {
                strSql.Append(" and reimb.UserReimbID="+reimbModel.UserReimbID );
            }
            if (reimbModel.ReimbAllAmount != null)
            {
                strSql.Append(" and reimb.ReimbAllAmount>="+reimbModel.ReimbAllAmount);
            }
            if (ReimbAmount1 != null)
            {
                strSql.Append(" and reimb.ReimbAllAmount<="+ReimbAmount1);
            }
            if (reimbModel.ProjectID != null)
            {
                strSql.Append(" and reimb.ProjectID=" + reimbModel.ProjectID);
            }
            if (reimbModel.CustID != null)
            {
                strSql.Append(" and reimb.CustID='" + reimbModel.CustID + "'");
            }
            if (reimbModel.ContactsUnitID != null)
            {
                strSql.Append(" and reimb.ContactsUnitID='" + reimbModel.ContactsUnitID + "'");
            }
            SqlParameter[] param ={
                               new SqlParameter("@CompanyCD",reimbModel.CompanyCD)
                               };
            return SqlHelper.CreateSqlByPageExcuteSql(strSql.ToString(), pageIndex, pageCount, ord, param, ref TotalCount);
        }
        #endregion
        #region 根据检索条件获取报销单据金额合计
        /// <summary>
        /// 根据检索条件获取报销单据金额合计
        /// </summary>
        /// <param name="reimbModel">费用报销主表实体</param>
        /// <param name="empid"></param>
        /// <param name="ReimbDate1"></param>
        /// <param name="FlowStatus"></param>
        /// <param name="ReimbAmount1"></param>
        /// <returns></returns>
        public static DataTable GetReimbSumTotal(ReimbursementModel reimbModel, int empid, DateTime? ReimbDate1, string FlowStatus, decimal? ReimbAmount1)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select sum(reimb.ExpAllAmount) as ExpAllAmountTotal,sum(reimb.ReimbAllAmount) as ReimbAllAmountTotal,sum(reimb.RestoreAllAmount) as RestoreAllAmountTotal ");//reimb.Status ");
            strSql.Append(" from officedba.FeeReturn reimb ");
            strSql.Append(" left join officedba.FlowInstance f on reimb.CompanyCD=f.CompanyCD and f.BillTypeFlag=1 ");
            strSql.Append(" and f.BillTypeCode=5 and f.BillNo=reimb.ReimbNo ");
            strSql.Append(" and f.ID=(select max(ID) from officedba.FlowInstance as f2 where reimb.ID=f2.BillID and f2.BillTypeFlag=1 and f2.BillTypeCode=5 )");
            strSql.Append(" where reimb.CompanyCD=@CompanyCD ");
            strSql.Append(" and ( charindex('," + empid + ",' , ','+reimb.CanViewUser+',')>0 or reimb.Creator=" + empid + " OR reimb.CanViewUser='' OR reimb.CanViewUser is null) ");
            if (reimbModel.ReimbNo != null)
            {
                strSql.Append(" and reimb.ReimbNo like '%" + reimbModel.ReimbNo + "%' ");
            }
            if (reimbModel.Title != null)
            {
                strSql.Append(" and reimb.Title like '%" + reimbModel.Title + "%' ");
            }
            if (reimbModel.Applyor != -1)
            {
                strSql.Append(" and reimb.Applyor=" + reimbModel.Applyor);
            }
            if (reimbModel.ReimbDate != null && ReimbDate1 != null)
            {
                strSql.Append(" and (reimb.ReimbDate >= '" + reimbModel.ReimbDate.ToString().Trim() + "' and reimb.ReimbDate <= '" + ReimbDate1.ToString().Trim() + "' )");
            }
            if (reimbModel.Status != null)
            {
                strSql.Append(" and reimb.Status =" + reimbModel.Status);
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
            if (reimbModel.UserReimbID != -1)
            {
                strSql.Append(" and reimb.UserReimbID=" + reimbModel.UserReimbID);
            }
            if (reimbModel.ReimbAllAmount != null)
            {
                strSql.Append(" and reimb.ReimbAllAmount>=" + reimbModel.ReimbAllAmount);
            }
            if (ReimbAmount1 != null)
            {
                strSql.Append(" and reimb.ReimbAllAmount<=" + ReimbAmount1);
            }
            if (reimbModel.ProjectID != null)
            {
                strSql.Append(" and reimb.ProjectID=" + reimbModel.ProjectID);
            }
            if (reimbModel.CustID != null)
            {
                strSql.Append(" and reimb.CustID='" + reimbModel.CustID + "'");
            }
            if (reimbModel.ContactsUnitID != null)
            {
                strSql.Append(" and reimb.ContactsUnitID='" + reimbModel.ContactsUnitID + "'");
            }
            SqlParameter[] param ={
                               new SqlParameter("@CompanyCD",reimbModel.CompanyCD)
                               };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        #region 根据报销单ID获取报销单详细信息
        /// <summary>
        /// 根据报销单ID获取报销单详细信息
        /// </summary>
        /// <param name="reimbID">报销单ID</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns>报销单详细信息datatable</returns>
        /// [dbo].[getEmployeeNameString](reimb.CanViewUser)此方法为“可编程性--函数--标量值函数”中的方法；括号中的参数为人员ID串以逗号隔开；返回的为对应的人员名称串，同样以逗号隔开。
        public static DataTable GetReimbInfoByID(int reimbID, string strCompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select reimb.ID,reimb.ReimbNo,reimb.Title,reimb.Applyor,e1.EmployeeName as ApplyorName,convert(varchar(100), reimb.ReimbDate, 23) as ReimbDate, ");
            strSql.Append(" reimb.FromType,case reimb.FromType when 0 then '无来源' when 1 then '费用申请' end as FromTypeName, ");
            strSql.Append(" isnull(reimb.ExpAllAmount,0) as ExpAllAmount,isnull(reimb.ReimbAllAmount,0) as ReimbAllAmount,isnull(reimb.RestoreAllAmount,0) as RestoreAllAmount,");
            strSql.Append(" reimb.ReimbDeptID,isnull(c.DeptName,'') as DeptName ,reimb.UserReimbID,isnull(e4.EmployeeName,'') as UserReimbName,");
            strSql.Append(" reimb.Status,case reimb.Status when 1 then '制单' when 2 then '执行' when 4 then '作废' end as StatusText,");
            strSql.Append(" f.FlowStatus as FlowStatus,isnull( case f.FlowStatus when '1' then '待审批' when '2' then '审批中'");
            strSql.Append(" when '3'  then '审批通过' when '4' then '审批不通过' when '5' then '撤消审批' end,'') as FlowStatusText, ");
            strSql.Append(" convert(varchar(100), reimb.CreateDate, 23) as CreateDate,reimb.Remark,");
            strSql.Append(" e2.EmployeeName as CreatorName, e3.EmployeeName as ConfirmorName , ");
            strSql.Append(" convert(varchar(100),reimb.ConfirmDate, 23) as ConfirmDate,");
            strSql.Append(" convert(varchar(100),reimb.ModifiedDate, 23) as ModifiedDate, reimb.ModifiedUserID ");
            strSql.Append(" ,reimb.CanViewUser,[dbo].[getEmployeeNameString](reimb.CanViewUser) as CanViewUserName ");
            strSql.Append(" ,reimb.ProjectID,reimb.AttestBillID,p.ProjectName,reimb.SubjectsNo,d.SubjectsName ");
            strSql.Append(" ,reimb.CustID,isnull(cust.CustName,'') as CustName,reimb.ContactsUnitID,reimb.FromTBName,reimb.ContactsUnitName ");
            strSql.Append(" ,reimb.Attachment ");
            strSql.Append(" from officedba.FeeReturn reimb ");
            strSql.Append(" left join officedba.EmployeeInfo e1 on reimb.Applyor=e1.ID ");
            strSql.Append(" left join officedba.EmployeeInfo e2 on reimb.Creator=e2.ID ");
            strSql.Append(" left join officedba.EmployeeInfo e3 on reimb.Confirmor=e3.ID ");
            strSql.Append(" left join officedba.EmployeeInfo e4 on reimb.UserReimbID=e4.ID ");
            strSql.Append(" left join officedba.DeptInfo c on reimb.ReimbDeptID=c.ID ");
            strSql.Append(" left join officedba.ProjectInfo p on p.ID=reimb.ProjectID ");
            strSql.Append(" left join officedba.CustInfo cust on reimb.CustID=cust.ID ");
            strSql.Append(" left join officedba.AccountSubjects d ON reimb.SubjectsNo = d.SubjectsCD and d.CompanyCD =reimb.CompanyCD ");
            strSql.Append(" left join officedba.FlowInstance f on reimb.CompanyCD=f.CompanyCD and f.BillTypeFlag=1 ");
            strSql.Append(" and f.BillTypeCode=5 and f.BillNo=reimb.ReimbNo ");
            strSql.Append(" and f.ID=(select max(ID) from officedba.FlowInstance as f2 where reimb.ID=f2.BillID and f2.BillTypeFlag=1 and f2.BillTypeCode=5 )");
            strSql.Append(" where reimb.ID=@ID and reimb.CompanyCD=@CompanyCD ");

            SqlParameter[] param ={
                                 new SqlParameter("@ID",reimbID ),
                                 new SqlParameter("@CompanyCD",strCompanyCD )
                                 };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        #region 根据报销单ID获取报销单明细信息
        /// <summary>
        /// 根据报销单ID获取报销单明细信息
        /// </summary>
        /// <param name="reimbID">报销单ID</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns>报销单明细信息datatable</returns>
        public static DataTable GetReimbDetailsByReimbID(int reimbID, string strCompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select rd.ID,rd.SortNo,rd.ExpID,rd.ReimbID,rd.ExpAmount,rd.ReimbAmount,rd.RestoreAmount,");
            strSql.Append("rd.Notes,exp.ExpCode as ExpCode,rd.FeeNameID as ExpType,c.CodeName as ExpTypeName ");
            strSql.Append(" ,rd.SubjectsNo as DSubjectsNo,f.SubjectsName as DSubjectsName ");
            strSql.Append(" from officedba.FeeReturnDetail rd ");
            strSql.Append(" left join officedba.FeeApply exp on rd.ExpID=exp.ID and exp.CompanyCD=@CompanyCD ");
            strSql.Append(" left join officedba.CodeFeeType c on c.ID=rd.FeeNameID ");
            strSql.Append(" left join officedba.AccountSubjects f ON rd.SubjectsNo = f.SubjectsCD and f.CompanyCD=@CompanyCD ");
            strSql.Append(" where ReimbID=@ReimbID ");
            strSql.Append(" order by rd.SortNo ");
            SqlParameter[] param = { 
                                    new SqlParameter("@ReimbID",reimbID),
                                    new SqlParameter("@CompanyCD",strCompanyCD)
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        #region 根据条件获取历史报销单列表
        /// <summary>
        /// 根据条件获取历史报销单列表
        /// </summary>
        /// <param name="reimbModel">报销单主表实体</param>
        /// <param name="ReimbDate1">报销日期</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageCount">每页显示记录数</param>
        /// <param name="ord">排序</param>
        /// <param name="TotalCount">总记录数</param>
        /// <returns>返回历史报销单列表datatable</returns>
        public static DataTable GetHisReimbursementList(ReimbursementModel reimbModel,int empid, DateTime? ReimbDate1, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select reimb.ID,reimb.ReimbNo,reimb.Title,reimb.Applyor,e1.EmployeeName as ApplyorName,");
            strSql.Append(" convert(varchar(100), reimb.ReimbDate, 23) as ReimbDate, ");
            strSql.Append(" reimb.ExpAllAmount,reimb.ReimbAllAmount,reimb.RestoreAllAmount,reimb.Status ");
            strSql.Append(" ,reimb.Confirmor,e3.EmployeeName as ConfirmorName,convert(varchar(100),reimb.ConfirmDate,23) as ConfirmDate ");
            strSql.Append(" from officedba.FeeReturn reimb ");
            strSql.Append(" left join officedba.EmployeeInfo e1 on reimb.Applyor=e1.ID ");
            strSql.Append(" left join officedba.EmployeeInfo e3 on reimb.Confirmor=e3.ID ");
            strSql.Append(" where reimb.CompanyCD=@CompanyCD ");
            strSql.Append(" and ( charindex('," + empid + ",' , ','+reimb.CanViewUser+',')>0 or reimb.Creator=" + empid + " OR reimb.CanViewUser='' OR reimb.CanViewUser is null) ");

            if (reimbModel.ReimbNo != null)
            {
                strSql.Append(" and reimb.ReimbNo like '%" + reimbModel.ReimbNo + "%' ");
            }
            if (reimbModel.Title != null)
            {
                strSql.Append(" and reimb.Title like '%" + reimbModel.Title + "%' ");
            }
            if (reimbModel.Applyor != -1)
            {
                strSql.Append(" and reimb.Applyor=" + reimbModel.Applyor);
            }
            if (reimbModel.ReimbDate != null && ReimbDate1 != null)
            {
                strSql.Append(" and (reimb.ReimbDate >= '" + reimbModel.ReimbDate.ToString().Trim() + "' and reimb.ReimbDate <= '" + ReimbDate1.ToString().Trim() + "' )");
            }
            if (reimbModel.Status != null)
            {
                strSql.Append(" and reimb.Status =" + reimbModel.Status);
            }

            SqlParameter[] param ={
                               new SqlParameter("@CompanyCD",reimbModel.CompanyCD)
                               };
            return SqlHelper.CreateSqlByPageExcuteSql(strSql.ToString(), pageIndex, pageCount, ord, param, ref TotalCount);
        }
        #endregion

        #region 费用申请单打印
        /// <summary>
        /// 获取费用报销单打印主表信息
        /// </summary>
        /// <param name="reimbNo"></param>
        /// <param name="strCompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetRepReimbursement(string reimbNo, string strCompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from officedba.V_FeeReturn where ReimbNo=@ReimbNo and CompanyCD=@CompanyCD ");
            SqlParameter[] param = { 
                                    new SqlParameter("@ReimbNo",reimbNo),
                                    new SqlParameter("@CompanyCD",strCompanyCD)
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }

        /// <summary>
        /// 获取费用报销单打印子表信息
        /// </summary>
        /// <param name="reimbNo"></param>
        /// <param name="strCompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetRepReimbursementDetail(string reimbNo, string strCompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            int ReimbID = GetReimbursementID(reimbNo, strCompanyCD);
            //strSql.Append(" select * from officedba.V_FeeReturnDetail where ReimbID=@ReimbID ");
            strSql.Append(" SELECT a.ID, a.SortNo, a.ExpID, a.ReimbID, a.ExpAmount, a.ReimbAmount, a.RestoreAmount, a.Notes, b.ExpCode, "); 
            strSql.Append(" a.FeeNameID AS ExpType, c.CodeName AS ExpTypeName, a.SubjectsNo AS DSubjectsNo, f.SubjectsName AS DSubjectsName ");
            strSql.Append(" FROM  officedba.FeeReturnDetail AS a ");
            strSql.Append(" LEFT JOIN officedba.FeeApply AS b ON a.ExpID = b.ID and b.CompanyCD = @CompanyCD");
            strSql.Append(" LEFT JOIN officedba.CodeFeeType AS c ON c.ID = a.FeeNameID ");
            strSql.Append(" LEFT JOIN officedba.AccountSubjects AS f ON a.SubjectsNo = f.SubjectsCD AND f.CompanyCD = @CompanyCD ");
            strSql.Append(" where ReimbID=@ReimbID ");
            strSql.Append(" ORDER BY a.SortNo ");
            SqlParameter[] param = { 
                                    new SqlParameter("@ReimbID",ReimbID),
                                    new SqlParameter("@CompanyCD",strCompanyCD)
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        #region 根据费用类别获取科目信息
        /// <summary>
        /// 根据费用类别获取科目信息
        /// </summary>
        /// <param name="expType">类别ID</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns></returns>
        public static DataTable GetSubjectByExpType(int expType, string strCompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select a.SubjectsCD as DSubjectsNo,a.SubjectsName as SubjectsName ");
            strSql.AppendLine(" from officedba.CodeFeeType c ");
            strSql.AppendLine(" left join officedba.AccountSubjects a on a.SubjectsCD=c.FeeSubjectsNo  and c.CompanyCD=a.CompanyCD ");
            strSql.AppendLine(" where c.ID=@ExpType and c.CompanyCD=@CompanyCD ");
            SqlParameter[] param = { 
                                    new SqlParameter("@ExpType",expType),
                                    new SqlParameter("@CompanyCD",strCompanyCD)
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion
    }
}

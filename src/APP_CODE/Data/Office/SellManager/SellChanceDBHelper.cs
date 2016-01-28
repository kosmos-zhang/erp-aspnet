/***********************************************************************
 * 
 * Module Name:XBase.Data.Office.SystemManager.{.cs
 * Current Version: 1.0 
 * Creator: 周军
 * Auditor:2009-01-13
 * End Date:
 * Description: 销售机会数据库层处理
 * Version History:
 ***********************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using XBase.Data.DBHelper;
using System.Data;
using System.Data.Sql;
using XBase.Model.Office.SellManager;
using XBase.Common;

namespace XBase.Data.Office.SellManager
{
    /// <summary>
    /// 销售机会数据层
    /// </summary>
    public class SellChanceDBHelper
    {
        /// <summary>
        /// 添加销售机会及阶段
        /// </summary>
        /// <param name="sellChanceModel">销售机会表实体</param>
        /// <param name="sellChancePushModel">销售阶段表实体</param>
        /// <returns>是否添加成功</returns>
        public static bool InsertSellChance(Hashtable ht,SellChanceModel sellChanceModel, SellChancePushModel sellChancePushModel)
        {
            bool isSucc = false;//是否添加成功

            TransactionManager tran = new TransactionManager();
            tran.BeginTransaction();
            try
            {
                int sellChID = InsertChanece(tran, sellChanceModel);
                //若是设置了手机提醒 则插入以下信息
                if (sellChanceModel.IsMobileNotice == "1")
                {

                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("insert into officedba.NoticeHistory(");
                    strSql.Append("   CompanyCD,SourceFlag,SourceID,PlanNoticeDate  )  ");
                    strSql.Append("      values(@CompanyCD, @SourceFlag,@SourceID,@PlanNoticeDate  ) ");
                    SqlParameter[] param = { 
                                            new SqlParameter("@CompanyCD",sellChanceModel.CompanyCD),
                                            new SqlParameter("@SourceFlag","4"),
                                            new SqlParameter("@SourceID",sellChID),
                                            new SqlParameter("@PlanNoticeDate",sellChanceModel.RemindTime)
                                           };
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), param);//.ExecuteTransWithCommand(commN);
                }

                //拓展属性
                GetExtAttrCmd(sellChanceModel,ht, tran);
                
                InsertPush(tran, sellChancePushModel);
                tran.Commit();
                isSucc = true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                isSucc = false;
                throw ex;
            }


            return isSucc;
        }

        /// <summary>
        /// 为机会表插入插入数据
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="sellChanceModel"></param>
        private static int InsertChanece(TransactionManager tran, SellChanceModel sellChanceModel)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.SellChance(");
            strSql.Append("CompanyCD,CustID,CustType,CustTel,ChanceNo,Title,ChanceType,HapSource,Seller,SellDeptId,FindDate,ProvideMan,Requires,IntendDate,IntendMoney,Remark,IsQuoted,CanViewUser,CanViewUserName,Creator,CreateDate,ModifiedDate,ModifiedUserID,IsMobileNotice,RemindTime,RemindMTel,RemindContent,ReceiverID)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@CustID,@CustType,@CustTel,@ChanceNo,@Title,@ChanceType,@HapSource,@Seller,@SellDeptId,@FindDate,@ProvideMan,@Requires,@IntendDate,@IntendMoney,@Remark,@IsQuoted,@CanViewUser,@CanViewUserName,@Creator,getdate(),getdate(),@ModifiedUserID,@IsMobileNotice,@RemindTime,@RemindMTel,@RemindContent,@ReceiverID)");
            strSql.Append(" ;select @@IDENTITY ");
            #region 
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@CustID", SqlDbType.Int,4),
					new SqlParameter("@CustType", SqlDbType.Int,4),
					new SqlParameter("@CustTel", SqlDbType.VarChar,100),
					new SqlParameter("@ChanceNo", SqlDbType.VarChar,50),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@ChanceType", SqlDbType.Int,4),
					new SqlParameter("@HapSource", SqlDbType.Int,4),
					new SqlParameter("@Seller", SqlDbType.Int,4),
					new SqlParameter("@SellDeptId", SqlDbType.Int,4),
					new SqlParameter("@FindDate", SqlDbType.DateTime),
					new SqlParameter("@ProvideMan", SqlDbType.VarChar,50),
					new SqlParameter("@Requires", SqlDbType.VarChar,800),
					new SqlParameter("@IntendDate", SqlDbType.DateTime),
					new SqlParameter("@IntendMoney", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.VarChar,1024),
					new SqlParameter("@IsQuoted", SqlDbType.Char,1),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,20),
                    new SqlParameter("@CanViewUser", SqlDbType.VarChar,2048),
					new SqlParameter("@CanViewUserName", SqlDbType.VarChar,2048),
					new SqlParameter("@IsMobileNotice", SqlDbType.Char,1),
					new SqlParameter("@RemindTime", SqlDbType.DateTime),
					new SqlParameter("@RemindMTel", SqlDbType.VarChar,15),
					new SqlParameter("@RemindContent", SqlDbType.VarChar,500),
					new SqlParameter("@ReceiverID", SqlDbType.Int,4)  
                                        };
            parameters[0].Value = sellChanceModel.CompanyCD;
            parameters[1].Value = sellChanceModel.CustID;
            parameters[2].Value = sellChanceModel.CustType;
            parameters[3].Value = sellChanceModel.CustTel;
            parameters[4].Value = sellChanceModel.ChanceNo;
            parameters[5].Value = sellChanceModel.Title;
            parameters[6].Value = sellChanceModel.ChanceType;
            parameters[7].Value = sellChanceModel.HapSource;
            parameters[8].Value = sellChanceModel.Seller;
            parameters[9].Value = sellChanceModel.SellDeptId;
            parameters[10].Value = sellChanceModel.FindDate;
            parameters[11].Value = sellChanceModel.ProvideMan;
            parameters[12].Value = sellChanceModel.Requires;
            parameters[13].Value = sellChanceModel.IntendDate;
            parameters[14].Value = sellChanceModel.IntendMoney;
            parameters[15].Value = sellChanceModel.Remark;
            parameters[16].Value = sellChanceModel.IsQuoted;
            parameters[17].Value = sellChanceModel.Creator;
            parameters[18].Value = sellChanceModel.ModifiedUserID;
            parameters[19].Value = sellChanceModel.CanViewUser;
            parameters[20].Value = sellChanceModel.CanViewUserName;
            parameters[21].Value = sellChanceModel.IsMobileNotice;
            parameters[22].Value = sellChanceModel.RemindTime;
            parameters[23].Value = sellChanceModel.RemindMTel;
            parameters[24].Value = sellChanceModel.RemindContent;
            parameters[25].Value = sellChanceModel.ReceiverID;
            foreach (SqlParameter para in parameters)
            {
                if (para.Value == null)
                {
                    para.Value = DBNull.Value;
                }
            }
            #endregion
            int billID = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql.ToString(), parameters));
            return billID;
        }

        /// <summary>
        /// 为推进表更新数据
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="sellChancePushModel"></param>
        private static void InsertPush(TransactionManager tran, SellChancePushModel sellChancePushModel)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.SellChancePush(");
            strSql.Append("CompanyCD,ChanceNo,PushDate,Seller,Phase,State,Feasibility,DelayDate,Remark,ModifiedDate,ModifiedUserID)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@ChanceNo,@PushDate,@Seller,@Phase,@State,@Feasibility,@DelayDate,@Remark,getdate(),@ModifiedUserID)");
            #region 参数
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@ChanceNo", SqlDbType.VarChar,50),
					new SqlParameter("@PushDate", SqlDbType.DateTime),
					new SqlParameter("@Seller", SqlDbType.Int,4),
					new SqlParameter("@Phase", SqlDbType.Char,1),
					new SqlParameter("@State", SqlDbType.Char,1),
					new SqlParameter("@Feasibility", SqlDbType.Int,4),
					new SqlParameter("@DelayDate", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.VarChar,1024),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,20)};
            parameters[0].Value = sellChancePushModel.CompanyCD;
            parameters[1].Value = sellChancePushModel.ChanceNo;
            parameters[2].Value = sellChancePushModel.PushDate;
            parameters[3].Value = sellChancePushModel.Seller;
            parameters[4].Value = sellChancePushModel.Phase;
            parameters[5].Value = sellChancePushModel.State;
            parameters[6].Value = sellChancePushModel.Feasibility;
            parameters[7].Value = sellChancePushModel.DelayDate;
            parameters[8].Value = sellChancePushModel.Remark;
            parameters[9].Value = sellChancePushModel.ModifiedUserID;

            foreach (SqlParameter para in parameters)
            {
                if (para.Value == null)
                {
                    para.Value = DBNull.Value;
                }
            }
            #endregion
            SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), parameters);
        }


        /// <summary>
        /// 为机会表跟新数据
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="sellChanceModel"></param>
        private static void UpdateChanece(TransactionManager tran, SellChanceModel sellChanceModel)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.SellChance set ");
            strSql.Append("CustID=@CustID,");
            strSql.Append("CustType=@CustType,");
            strSql.Append("CustTel=@CustTel,");
            strSql.Append("Title=@Title,");
            strSql.Append("ChanceType=@ChanceType,");
            strSql.Append("HapSource=@HapSource,");
            strSql.Append("Seller=@Seller,");
            strSql.Append("SellDeptId=@SellDeptId,");
            strSql.Append("FindDate=@FindDate,");
            strSql.Append("ProvideMan=@ProvideMan,");
            strSql.Append("Requires=@Requires,");
            strSql.Append("IntendDate=@IntendDate,");
            strSql.Append("IntendMoney=@IntendMoney,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("CanViewUser=@CanViewUser,");
            strSql.Append("CanViewUserName=@CanViewUserName,");
            strSql.Append("ModifiedDate=getdate(),");
            strSql.Append("ModifiedUserID=@ModifiedUserID");
            strSql.Append(",IsMobileNotice=@IsMobileNotice,RemindTime=@RemindTime,");
            strSql.Append("RemindMTel=@RemindMTel,RemindContent=@RemindContent, ");
            strSql.Append("ReceiverID=@ReceiverID ");
            strSql.Append(" where CompanyCD=@CompanyCD and ChanceNo=@ChanceNo ");
            #region
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@CustID", SqlDbType.Int,4),
					new SqlParameter("@CustType", SqlDbType.Int,4),
					new SqlParameter("@CustTel", SqlDbType.VarChar,100),
					new SqlParameter("@ChanceNo", SqlDbType.VarChar,50),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@ChanceType", SqlDbType.Int,4),
					new SqlParameter("@HapSource", SqlDbType.Int,4),
					new SqlParameter("@Seller", SqlDbType.Int,4),
					new SqlParameter("@SellDeptId", SqlDbType.Int,4),
					new SqlParameter("@FindDate", SqlDbType.DateTime),
					new SqlParameter("@ProvideMan", SqlDbType.VarChar,50),
					new SqlParameter("@Requires", SqlDbType.VarChar,800),
					new SqlParameter("@IntendDate", SqlDbType.DateTime),
					new SqlParameter("@IntendMoney", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.VarChar,1024),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,20),
                    new SqlParameter("@CanViewUser", SqlDbType.VarChar,2048),
					new SqlParameter("@CanViewUserName", SqlDbType.VarChar,2048),
					new SqlParameter("@IsMobileNotice", SqlDbType.Char,1),
					new SqlParameter("@RemindTime", SqlDbType.DateTime),
					new SqlParameter("@RemindMTel", SqlDbType.VarChar,15),
					new SqlParameter("@RemindContent", SqlDbType.VarChar,500) ,
					new SqlParameter("@ReceiverID", SqlDbType.Int,4)      };
            parameters[0].Value = sellChanceModel.CompanyCD;
            parameters[1].Value = sellChanceModel.CustID;
            parameters[2].Value = sellChanceModel.CustType;
            parameters[3].Value = sellChanceModel.CustTel;
            parameters[4].Value = sellChanceModel.ChanceNo;
            parameters[5].Value = sellChanceModel.Title;
            parameters[6].Value = sellChanceModel.ChanceType;
            parameters[7].Value = sellChanceModel.HapSource;
            parameters[8].Value = sellChanceModel.Seller;
            parameters[9].Value = sellChanceModel.SellDeptId;
            parameters[10].Value = sellChanceModel.FindDate;
            parameters[11].Value = sellChanceModel.ProvideMan;
            parameters[12].Value = sellChanceModel.Requires;
            parameters[13].Value = sellChanceModel.IntendDate;
            parameters[14].Value = sellChanceModel.IntendMoney;
            parameters[15].Value = sellChanceModel.Remark;
            parameters[16].Value = sellChanceModel.ModifiedUserID;
            parameters[17].Value = sellChanceModel.CanViewUser;
            parameters[18].Value = sellChanceModel.CanViewUserName;
            parameters[19].Value = sellChanceModel.IsMobileNotice;
            parameters[20].Value = sellChanceModel.RemindTime;
            parameters[21].Value = sellChanceModel.RemindMTel;
            parameters[22].Value = sellChanceModel.RemindContent;
            parameters[23].Value = sellChanceModel.ReceiverID;
            foreach (SqlParameter para in parameters)
            {
                if (para.Value == null)
                {
                    para.Value = DBNull.Value;
                }
            }
            #endregion
            SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 修改销售机会及阶段
        /// </summary>
        /// <param name="sellChanceModel">销售机会表实体</param>
        /// <param name="sellChancePushModel">销售阶段表实体</param>
        /// <returns>是否添加成功</returns>
        public static bool UpdateSellChance(Hashtable ht,SellChanceModel sellChanceModel, SellChancePushModel sellChancePushModel)
        {
            bool isSucc = false;//是否添加成功
            string strSql = "delete from officedba.SellChancePush where  ChanceNo=@ChanceNo  and CompanyCD=@CompanyCD and Phase=@Phase";
            SqlParameter[] paras = { 
                                   new SqlParameter("@ChanceNo", sellChancePushModel.ChanceNo),
                                   new SqlParameter("@CompanyCD", sellChancePushModel.CompanyCD),
                                   new SqlParameter("@Phase",sellChancePushModel.Phase)
                                   };
            TransactionManager tran = new TransactionManager();
            tran.BeginTransaction();
            try
            {

                UpdateChanece(tran, sellChanceModel);
                //拓展属性
                GetExtAttrCmd(sellChanceModel, ht, tran);
                //若是设置了手机提醒 则插入以下信息
                if ( sellChanceModel.IsMobileNotice == "1")
                {
                    //若有未发送的记录则更新该未发送的记录，否则则插入新记录
                    if (IsExistedNoSendMsg(sellChanceModel.ID.ToString(), "0",sellChanceModel.CompanyCD))
                    {
                        StringBuilder strSqlMb = new StringBuilder();
                        //strSqlMb.Append("delete  from  officedba.NoticeHistory  where  SourceID=@SourceID and  SourceFlag =@SourceFlag  ");
                       // strSqlMb.Append("insert into officedba.NoticeHistory(");
                        //strSqlMb.Append("   CompanyCD,SourceFlag,SourceID,PlanNoticeDate  )  ");
                        //strSqlMb.Append("      values(@CompanyCD, @SourceFlag,@SourceID,@PlanNoticeDate  ) ");
                        strSqlMb.AppendLine(" update officedba.NoticeHistory set PlanNoticeDate=@PlanNoticeDate ");
                        strSqlMb.AppendLine(" where CompanyCD=@CompanyCD and SourceID=@SourceID and SourceFlag=@SourceFlag and RealNoticeDate is null");
                        SqlParameter[] param = { 
                                                new SqlParameter("@CompanyCD",sellChanceModel.CompanyCD),
                                                new SqlParameter("@SourceFlag","4"),
                                                new SqlParameter("@SourceID",sellChanceModel.ID),
                                                new SqlParameter("@PlanNoticeDate",sellChanceModel.RemindTime)
                                               };
                        SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSqlMb.ToString(), param);
                    }
                    else
                    { 
                        StringBuilder strSqlMb = new StringBuilder();
                        strSqlMb.Append("insert into officedba.NoticeHistory(");
                        strSqlMb.Append("   CompanyCD,SourceFlag,SourceID,PlanNoticeDate  )  ");
                        strSqlMb.Append("      values(@CompanyCD, @SourceFlag,@SourceID,@PlanNoticeDate  ) ");
                        SqlParameter[] param = { 
                                                new SqlParameter("@CompanyCD",sellChanceModel.CompanyCD),
                                                new SqlParameter("@SourceFlag","4"),
                                                new SqlParameter("@SourceID",sellChanceModel.ID),
                                                new SqlParameter("@PlanNoticeDate",sellChanceModel.RemindTime)
                                               };
                        SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSqlMb.ToString(), param);
                    }
                    
                }
                else
                {
                    //若有未发送的记录则删除
                    if (IsExistedNoSendMsg(sellChanceModel.ID.ToString(), "0",sellChanceModel.CompanyCD))
                    { 
                        StringBuilder strSqlMb = new StringBuilder();
                        strSqlMb.Append("delete  from  officedba.NoticeHistory where SourceID=@SourceID and SourceFlag =@SourceFlag and CompanyCD=@CompanyCD ");
                        SqlParameter[] param = { 
                                                new SqlParameter("@CompanyCD",sellChanceModel.CompanyCD),
                                                new SqlParameter("@SourceFlag","4"),
                                                new SqlParameter("@SourceID",sellChanceModel.ID)
                                               };
                        SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSqlMb.ToString(), param);
                    }
                }

                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), paras);
                InsertPush(tran, sellChancePushModel);
                tran.Commit();
                isSucc = true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
            return isSucc;
        }

        #region 判断该单据是否在officedba.NoticeHistory表中是否存在已(未)发送的记录
        /// <summary>
        /// 判断该单据是否在officedba.NoticeHistory表中是否存在已(未)发送的记录
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="isSend">1表示查已发送记录，0表示查未发送记录</param>
        /// <returns>true存在,false不存在</returns>
        private static bool IsExistedNoSendMsg(string billID,string isSend,string CompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            bool existed = false;
            strSql.AppendLine(" select count(1) from officedba.NoticeHistory ");
            strSql.AppendLine(" where CompanyCD=@CompanyCD and SourceFlag=@SourceFlag and SourceID=@BillID ");
            if (isSend == "1")
            {
                strSql.AppendLine(" and RealNoticeDate is not null");
            }
            else
            { 
               strSql.AppendLine(" and RealNoticeDate is null"); 
            }
            
            SqlParameter[] param = { 
                                    new SqlParameter("@BillID",billID),
                                    new SqlParameter("@CompanyCD",CompanyCD),
                                    new SqlParameter("@SourceFlag","4")
                                   };
            int iCount = 0;
            iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql.ToString(), param));
            if (iCount > 0)
            {
                existed = true;
            }
            return existed;
        }
        #endregion

        /// <summary>
        /// 获取销售机会 
        /// 修改记录：2010-02-05 by hexw
        /// 获取的销售机会列表同一单据不同阶段的单据只显示最后一阶段的单据。
        /// </summary>
        /// <param name="ChanceNo"></param>
        /// <param name="Title"></param>
        /// <param name="CustID"></param>
        /// <param name="Seller"></param>
        /// <param name="Phase"></param>
        /// <param name="State"></param>
        /// <param name="ChanceType"></param>
        /// <param name="HapSource"></param>
        /// <param name="FindDate"></param>
        /// <param name="FindDate1"></param>
        /// <returns></returns>
        public static DataTable GetChanceList(string EFIndex, string EFDesc, string ChanceNo, string Title, int? CustID, int? Seller, string Phase, string State, int? ChanceType, int? HapSource, 
            DateTime? FindDate, DateTime? FindDate1, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strCompanyCD = string.Empty;//单位编号
            int eid = 0;
           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                eid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
           
            string strSql = "SELECT s.ID, s.ChanceNo,s.ModifiedDate, s.Title, CONVERT(varchar(100), s.FindDate, 23) AS FindDate, ";
            strSql += "ISNULL(c.CustName, ' ') AS CustName, ISNULL(cpt.TypeName, ' ') ";
            strSql += "AS ChanceTypeName, ISNULL(cpt1.TypeName, ' ') AS HapSourceName, ";
            strSql += "ISNULL(e.EmployeeName, ' ') AS EmployeeName, ";
            strSql += "CASE sp.Phase WHEN '1' THEN '初期沟通' WHEN '2' THEN '立项评估' ";
            strSql += "WHEN '3' THEN '需求分析' WHEN '4' THEN '方案指定' WHEN '5' THEN '招投标/竞争' ";
            strSql += "WHEN '6' THEN '商务谈判' WHEN '7' THEN '合同签约' END AS PhaseName, ";
            strSql += "CASE sp.State WHEN '1' THEN '跟踪' WHEN '2' THEN '成功' WHEN '3' ";
            strSql += "THEN '失败' WHEN '4' THEN '搁置' WHEN '5' THEN '失效' END AS StateName  , ";
            strSql += "isnull(CASE(SELECT count(1) FROM officedba.SellOffer AS sb ";
            strSql += "WHERE  sb.FromType = '1' AND sb.FromBillID = s.ID)  WHEN 0 THEN '不存在' END, '存在') AS FromBillText ";
            strSql += "FROM officedba.SellChance AS s LEFT OUTER JOIN ";

            strSql += "(SELECT scp.ID, scp.CompanyCD, scp.ChanceNo, scp.PushDate, scp.Seller, ";
            strSql += "scp.Phase, scp.State, scp.Feasibility, scp.DelayDate, scp.Remark, ";
            strSql += "scp.ModifiedDate, scp.ModifiedUserID ";

            strSql += " from (SELECT MAX(Phase) AS Phase, ChanceNo,CompanyCD FROM officedba.SellChancePush ";
            strSql += " GROUP BY ChanceNo,CompanyCD) AS scp1 ";
            strSql += " left join ( SELECT ID, CompanyCD, ChanceNo, PushDate, Seller,";
            strSql += " Phase, State, Feasibility, DelayDate, Remark, ModifiedDate, ModifiedUserID ";
            strSql += " from officedba.SellChancePush ) as scp ";
            strSql += " ON scp.ChanceNo  = scp1.ChanceNo and scp.CompanyCD  = scp1.CompanyCD AND scp.Phase = scp1.Phase ";
            strSql += " WHERE scp1.CompanyCD = @CompanyCD ";
            //strSql += "FROM officedba.SellChancePush AS scp LEFT JOIN ";
           // strSql += "(SELECT MAX(Phase) AS Phase, ChanceNo,CompanyCD ";
           // strSql += "FROM officedba.SellChancePush where CompanyCD=@CompanyCD  ";
           // strSql += "GROUP BY ChanceNo,CompanyCD) AS scp1 ON scp.ChanceNo  = scp1.ChanceNo and scp.CompanyCD  = scp1.CompanyCD  AND scp.Phase = scp1.Phase  WHERE scp.CompanyCD = @CompanyCD  ";

            strSql += ") AS sp ON s.ChanceNo = sp.ChanceNo AND s.CompanyCD = sp.CompanyCD LEFT OUTER JOIN ";
            strSql += "officedba.CustInfo AS c ON s.CustID = c.ID AND s.CompanyCD = c.CompanyCD LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e ON s.Seller = e.ID AND s.CompanyCD = e.CompanyCD LEFT OUTER JOIN ";
            strSql += "officedba.CodePublicType AS cpt ON s.ChanceType = cpt.ID AND s.CompanyCD = cpt.CompanyCD LEFT OUTER JOIN ";
            strSql += "officedba.CodePublicType AS cpt1 ON s.HapSource = cpt1.ID AND s.CompanyCD = cpt1.CompanyCD ";

            strSql += " WHERE (s.CompanyCD = @CompanyCD) ";
            strSql += " AND (CHARINDEX('," + eid + ",',','+s.CanViewUser+',')>0  OR s.CanViewUser='' or s.CanViewUser is null OR  s.Creator=" + eid +") " ;

            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            if (ChanceNo != null)
            {
                strSql += " and s.ChanceNo like  '%" + ChanceNo + "%'" ;
            }
            if (Title != null)
            {
                strSql += " and s.Title like '%" + Title + "%'";
            }
            if (CustID != null)
            {
                strSql += " and s.CustID = @CustID ";
                arr.Add(new SqlParameter("@CustID", CustID));
            }
            if (Seller != null)
            {
                strSql += " and s.Seller = @Seller ";
                arr.Add(new SqlParameter("@Seller", Seller));
            }
            if (Phase != null)
            {
                strSql += " and sp.Phase = @Phase ";
                arr.Add(new SqlParameter("@Phase", Phase));
            }
            if (State != null)
            {
                strSql += " and sp.State = @State ";
                arr.Add(new SqlParameter("@State", State));
            }
            if (ChanceType != null)
            {
                strSql += " and s.ChanceType = @ChanceType ";
                arr.Add(new SqlParameter("@ChanceType", ChanceType));
            }
            if (HapSource != null)
            {
                strSql += " and s.HapSource = @HapSource ";
                arr.Add(new SqlParameter("@HapSource", HapSource));
            }
            if (FindDate != null)
            {
                strSql += " and s.FindDate >= @FindDate ";
                arr.Add(new SqlParameter("@FindDate", FindDate));
            }
            if (FindDate1 != null)
            {
                strSql += " and s.FindDate <= @FindDate1 ";
                arr.Add(new SqlParameter("@FindDate1", FindDate1));
            }

            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                strSql += " and s.ExtField" + EFIndex + " LIKE @EFDesc";
                arr.Add(new SqlParameter("@EFDesc", "%" + EFDesc + "%"));
                //comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + EFDesc + "%"));
            }

            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
        }

        /// <summary>
        /// 销售机会控件列表
        /// 修改记录：2010-02-05 by hexw
        /// 1.获取的销售机会列表同一单据不同阶段的单据只显示最后一阶段的单据。
        /// 2.“编号”改为“销售机会编号”
        /// </summary>
        /// <param name="ChanceNo"></param>
        /// <param name="Title"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetChanceList(string ChanceNo, string Title, string CustName, string model, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strCompanyCD = string.Empty;//单位编号
            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            
            string strSql = "SELECT s.ID, s.ChanceNo, s.Title, CONVERT(varchar(100), s.FindDate, 23) AS FindDate, ";
            strSql += "ISNULL(c.CustName, ' ') AS CustName, ISNULL(cpt.TypeName, ' ') ";
            strSql += "AS ChanceTypeName, ISNULL(cpt1.TypeName, ' ') AS HapSourceName, ";
            strSql += "ISNULL(e.EmployeeName, ' ') AS EmployeeName, ";
            strSql += "CASE sp.Phase WHEN '1' THEN '初期沟通' WHEN '2' THEN '立项评估' ";
            strSql += "WHEN '3' THEN '需求分析' WHEN '4' THEN '方案指定' WHEN '5' THEN '招投标/竞争' ";
            strSql += "WHEN '6' THEN '商务谈判' WHEN '7' THEN '合同签约' END AS PhaseName, ";
            strSql += "CASE sp.State WHEN '1' THEN '跟踪' WHEN '2' THEN '成功' WHEN '3' ";
            strSql += "THEN '失败' WHEN '4' THEN '搁置' WHEN '5' THEN '失效' END AS StateName  , ";
            strSql += "isnull(CASE(SELECT count(1) FROM officedba.SellOffer AS sb ";
            strSql += "WHERE  sb.FromType = '1' AND sb.FromBillID = s.ID)  WHEN 0 THEN '不存在' END, '存在') AS FromBillText ";
            strSql += "FROM officedba.SellChance AS s LEFT OUTER JOIN ";
            strSql += "(SELECT scp.ID, scp.CompanyCD, scp.ChanceNo, scp.PushDate, scp.Seller, ";
            strSql += "scp.Phase, scp.State, scp.Feasibility, scp.DelayDate, scp.Remark, ";
            strSql += "scp.ModifiedDate, scp.ModifiedUserID ";

            strSql += " from (SELECT MAX(Phase) AS Phase, ChanceNo,CompanyCD FROM officedba.SellChancePush ";
            strSql += " GROUP BY ChanceNo,CompanyCD) AS scp1 ";
            strSql += " left join ( SELECT ID, CompanyCD, ChanceNo, PushDate, Seller,";
            strSql += " Phase, State, Feasibility, DelayDate, Remark, ModifiedDate, ModifiedUserID ";
            strSql += " from officedba.SellChancePush ) as scp ";
            strSql += " ON scp.ChanceNo  = scp1.ChanceNo and scp.CompanyCD  = scp1.CompanyCD AND scp.Phase = scp1.Phase ";
            strSql += " WHERE scp1.CompanyCD = @CompanyCD ";
            //strSql += "FROM officedba.SellChancePush AS scp LEFT JOIN ";
            //strSql += "(SELECT MAX(Phase) AS Phase, ChanceNo,CompanyCD ";
            //strSql += "FROM officedba.SellChancePush where CompanyCD = @CompanyCD ";
            //strSql += "GROUP BY ChanceNo,CompanyCD) AS scp1 ON scp.ChanceNo = scp1.ChanceNo and scp.CompanyCD  = scp1.CompanyCD  AND scp.Phase = scp1.Phase ";
            
            strSql += ") AS sp ON s.ChanceNo = sp.ChanceNo AND s.CompanyCD = sp.CompanyCD LEFT OUTER JOIN ";
            strSql += "officedba.CustInfo AS c ON s.CustID = c.ID AND s.CompanyCD = c.CompanyCD LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e ON s.Seller = e.ID AND s.CompanyCD = e.CompanyCD LEFT OUTER JOIN ";
            strSql += "officedba.CodePublicType AS cpt ON s.ChanceType = cpt.ID AND s.CompanyCD = cpt.CompanyCD LEFT OUTER JOIN ";
            strSql += "officedba.CodePublicType AS cpt1 ON s.HapSource = cpt1.ID AND s.CompanyCD = cpt1.CompanyCD ";

            strSql += " WHERE (s.CompanyCD = @CompanyCD) ";

            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            if (ChanceNo != null)
            {
                strSql += " and s.ChanceNo like  '%" + ChanceNo + "%'";
            }
            if (Title != null)
            {
                strSql += " and s.Title like '%" + Title + "%'";
                
            }
            if (CustName != null)
            {
                strSql += " and c.CustName like '%" + CustName + "%'";

            }
            if (model != "all")
            {
                strSql += " and s.IsQuoted = 0";
            }
            
            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
        }

        /// <summary>
        /// 删除销售机会
        /// </summary>
        /// <param name="orderNos"></param>
        /// <returns></returns>
        public static bool DelOrder(string orderNos)
        {
            string strCompanyCD = string.Empty;//单位编号
            bool isSucc = false;
            string allOfferID = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            TransactionManager tran = new TransactionManager();
            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            string[] offerIDS = null;
            offerIDS = orderNos.Split(',');

            for (int i = 0; i < offerIDS.Length; i++)
            {
                offerIDS[i] = "'" + offerIDS[i] + "'";
                sb.Append(offerIDS[i]);
            }

            allOfferID = sb.ToString().Replace("''", "','");
            tran.BeginTransaction();
            try
            {
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.SellChance WHERE ChanceNo IN ( " + allOfferID + " ) and CompanyCD='" + strCompanyCD + "'", null);
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.SellChancePush WHERE ChanceNo IN ( " + allOfferID + " ) and CompanyCD='" + strCompanyCD + "'", null);
                tran.Commit();
                isSucc = true;

            }
            catch (Exception ex)
            {
                tran.Rollback();
                isSucc = false;
                throw ex;
            }
            return isSucc;
        }

        /// <summary>
        /// 获取机会推进历史
        /// </summary>
        /// <param name="chanceNo"></param>
        /// <returns></returns>
        public static DataTable GetPush(string chanceNo)
        {

            string strCompanyCD = string.Empty;//单位编号
           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            
            string strSql = " SELECT  s.Phase, CASE s.Phase WHEN '1' THEN '初期沟通' WHEN '2' THEN '立项评估' WHEN '3' THEN '需求分析' WHEN '4' THEN '方案指定' WHEN '5' THEN '招投标/竞争' ";
            strSql += " WHEN '6' THEN '商务谈判' WHEN '7' THEN '合同签约' END AS PhaseName, s.State,s.Seller,isnull(s.Feasibility,0) as Feasibility ,s.DelayDate, ";
            strSql += " CASE s.State WHEN '1' THEN '跟踪' WHEN '2' THEN '成功' WHEN '3' THEN '失败' WHEN '4' THEN '搁置' WHEN '5' THEN '失效' END AS StateName,  ";
            strSql += " s.Remark, ISNULL(e.EmployeeName, '') AS EmployeeName, ISNULL(c.TypeName, '') AS TypeName, CONVERT(varchar(100), s.PushDate, 23)  ";
            strSql += " AS PushDate FROM officedba.SellChancePush AS s LEFT OUTER JOIN ";
            strSql += " officedba.EmployeeInfo AS e ON s.Seller = e.ID AND s.CompanyCD = e.CompanyCD LEFT OUTER JOIN ";
            strSql += " officedba.CodePublicType AS c ON s.Feasibility = c.ID AND s.CompanyCD = c.CompanyCD";
            strSql += " WHERE (s.ChanceNo = @ChanceNo ) AND (s.CompanyCD = @CompanyCD) ORDER BY s.Phase asc ";
            SqlParameter[] paras = new SqlParameter[2];
            ArrayList arr = new ArrayList();
            paras[0] = new SqlParameter("@ChanceNo", chanceNo);
            arr.Add(paras[0]);
            paras[1] = new SqlParameter("@CompanyCD", strCompanyCD);
            arr.Add(paras[1]);
            return SqlHelper.ExecuteSql(strSql, arr);
        }

        #region 获取销售机会详细信息
        /// <summary>
        /// 获取销售机会详细信息
        /// </summary>
        /// <param name="chanceNo"></param>
        /// <returns></returns>
        public static DataTable GetOrderInfo(int orderID)
        {

            string strCompanyCD = string.Empty;//单位编号
            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            string strSql = "SELECT sc.CustID, sc.CustType, sc.CustTel, sc.ChanceNo, sc.HapSource, ";
            strSql += "sc.ChanceType, sc.Title, sc.Seller, sc.SellDeptId,sc.CanViewUser,sc.CanViewUserName, CONVERT(varchar(100), ";
            strSql += "sc.FindDate, 23) AS FindDate, sc.ProvideMan, CASE sc.IsQuoted WHEN '0' ";
            strSql += "THEN '否' WHEN '1' THEN '是' END AS IsQuotedText, ";
            strSql += "CONVERT(varchar(100), sc.IntendDate, 23) AS IntendDate, sc.IntendMoney, ";
            strSql += "sc.Remark, sc.Requires, CONVERT(varchar(100), sc.CreateDate, 23) ";
            strSql += "AS CreateDate, CONVERT(varchar(100), sc.ModifiedDate, 23) AS ModifiedDate, ";
            strSql += "sc.ModifiedUserID, cpt.TypeName, d.DeptName, c.CustName, ";
            strSql += "e.EmployeeName, e1.EmployeeName AS CreatorName, ";
            strSql += " sc.ExtField1,sc.ExtField2,sc.ExtField3,sc.ExtField4,sc.ExtField5,sc.ExtField6,sc.ExtField7,sc.ExtField8,sc.ExtField9,sc.ExtField10 ";
            strSql += " ,sc.IsMobileNotice,sc.RemindTime,convert(varchar(10),sc.RemindTime,23) as ReimndTimeOnly,sc.RemindMTel,sc.RemindContent,sc.ReceiverID,e2.EmployeeName as ReceiverName ";
            strSql += " FROM officedba.SellChance AS sc LEFT OUTER JOIN ";
            strSql += "officedba.CustInfo AS c ON sc.CustID = c.ID AND sc.CompanyCD = c.CompanyCD LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e ON sc.Seller = e.ID AND sc.CompanyCD = e.CompanyCD LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e1 ON sc.Creator = e1.ID AND sc.CompanyCD = e1.CompanyCD LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e2 ON sc.ReceiverID = e2.ID AND sc.CompanyCD = e2.CompanyCD LEFT OUTER JOIN ";
            strSql += "officedba.DeptInfo AS d ON sc.SellDeptId = d.ID AND sc.CompanyCD = d.CompanyCD LEFT OUTER JOIN ";
            strSql += "officedba.CodePublicType AS cpt ON sc.CustType = cpt.ID AND sc.CompanyCD = cpt.CompanyCD ";

            strSql += " WHERE (sc.ID = @ID ) AND (sc.CompanyCD = @CompanyCD)";
            SqlParameter[] paras = new SqlParameter[2];
            ArrayList arr = new ArrayList();
            paras[0] = new SqlParameter("@ID", orderID);
            arr.Add(paras[0]);
            paras[1] = new SqlParameter("@CompanyCD", strCompanyCD);
            arr.Add(paras[1]);
            return SqlHelper.ExecuteSql(strSql, arr);
        }
        #endregion

        #region 根据单据编号获取单据ID
        /// <summary>
        /// 根据单据编号获取单据ID
        /// </summary>
        /// <param name="billNo"></param>
        /// <param name="strCompanyCD"></param>
        /// <returns>对应单据ID</returns>
        public static int GetBillIDByBillNo(string billNo,string strCompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select ID from officedba.SellChance where ChanceNo=@BillNo and CompanyCD=@CompanyCD");
            SqlParameter[] param = { 
                                    new SqlParameter("@BillNo",billNo),
                                    new SqlParameter("@CompanyCD",strCompanyCD)
                                   };
            int returnID = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql.ToString(), param));
            return returnID;
        }
        #endregion

        #region 报表相关

        #region 按发现时间月份统计
        /// <summary>
        /// 按发现时间月份统计
        /// </summary>
        /// <param name="FindDate">开始日期</param>
        /// <param name="FindDate1">结束日期</param>
        /// <param name="total">统计方式</param>
        /// <returns></returns>
        public static DataTable GetChance(DateTime FindDate, DateTime FindDate1, string total, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strCompanyCD = string.Empty;//单位编号
           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            string strSql = string.Empty;
            strSql = "SELECT COUNT(1) AS chanceCount, LEFT(CONVERT(varchar(10), s.FindDate, 20), 7) AS FindDate ";
            if (total == "SellDeptId")
            {
                strSql += " , d.DeptName AS title FROM officedba.SellChance as s LEFT OUTER JOIN ";
                strSql += " officedba.DeptInfo AS d ON s.SellDeptId = d.ID  ";
                strSql += " where s.CompanyCD = @CompanyCD and LEFT(CONVERT(varchar(10), s.FindDate, 20), 7) >= @FindDate  and LEFT(CONVERT(varchar(10), s.FindDate, 20), 7) <= @FindDate1 ";
                strSql += " GROUP BY d.DeptName, LEFT(CONVERT(varchar(10), s.FindDate, 20), 7)";
            }
            else
            {
                strSql += " , e.EmployeeName AS title FROM officedba.SellChance as s LEFT OUTER JOIN ";
                strSql += " officedba.EmployeeInfo AS e ON s.Seller = e.ID  ";
                strSql += " where s.CompanyCD = @CompanyCD and LEFT(CONVERT(varchar(10), s.FindDate, 20), 7) >= @FindDate  and LEFT(CONVERT(varchar(10), s.FindDate, 20), 7) <= @FindDate1 ";
                strSql += " GROUP BY e.EmployeeName, LEFT(CONVERT(varchar(10), s.FindDate, 20), 7),s.Seller";
            }
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@FindDate", FindDate.ToString("yyyy-MM")));
            arr.Add(new SqlParameter("@FindDate1", FindDate1.ToString("yyyy-MM")));
            DataTable dt = new DataTable();
            dt = SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
            
            return dt;
        }

        /// <summary>
        /// 按发现时间月份统计
        /// </summary>
        /// <param name="FindDate">开始日期</param>
        /// <param name="FindDate1">结束日期</param>
        /// <param name="total">统计方式</param>
        /// <returns></returns>
        public static DataTable GetChance(DateTime FindDate, DateTime FindDate1, string total)
        {
            string strCompanyCD = string.Empty;//单位编号
           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            
            string strSql = string.Empty;
            strSql = "SELECT COUNT(1) AS chanceCount, LEFT(CONVERT(varchar(10), s.FindDate, 20), 7) AS FindDate ";
            if (total == "SellDeptId")
            {
                strSql += " , d.DeptName AS title FROM officedba.SellChance as s LEFT OUTER JOIN ";
                strSql += " officedba.DeptInfo AS d ON s.SellDeptId = d.ID  ";
                strSql += " where s.CompanyCD = @CompanyCD and LEFT(CONVERT(varchar(10), s.FindDate, 20), 7) >= @FindDate  and LEFT(CONVERT(varchar(10), s.FindDate, 20), 7) <= @FindDate1 ";
                strSql += " GROUP BY d.DeptName, LEFT(CONVERT(varchar(10), s.FindDate, 20), 7)";
            }
            else
            {
                strSql += " , e.EmployeeName AS title FROM officedba.SellChance as s LEFT OUTER JOIN ";
                strSql += " officedba.EmployeeInfo AS e ON s.Seller = e.ID  ";
                strSql += " where s.CompanyCD = @CompanyCD and LEFT(CONVERT(varchar(10), s.FindDate, 20), 7) >= @FindDate  and LEFT(CONVERT(varchar(10), s.FindDate, 20), 7) <= @FindDate1 ";
                strSql += " GROUP BY e.EmployeeName, LEFT(CONVERT(varchar(10), s.FindDate, 20), 7),s.Seller  ";
            }
          
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@FindDate", FindDate.ToString("yyyy-MM")));
            arr.Add(new SqlParameter("@FindDate1", FindDate1.ToString("yyyy-MM")));
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteSql(strSql.ToString(), arr);
           
            return dt;
        }

        #endregion


        #region 按业务员统计

        /// <summary>
        /// 按业务员统计
        /// </summary>
        /// <param name="FindDate">开始日期</param>
        /// <param name="FindDate1">结束日期</param>
        /// <param name="SellerID">业务员编号</param>
        /// <returns></returns>
        public static DataTable GetChanceBySeller(DateTime FindDate, DateTime FindDate1, int SellerID, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strCompanyCD = string.Empty;//单位编号
            FindDate1 = FindDate1.AddDays(1);
            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            string strSql = string.Empty;

            strSql = "SELECT COUNT(1) AS chanceCount, e.EmployeeName               ";
            strSql += "FROM officedba.SellChance AS s LEFT OUTER JOIN               ";
            strSql += "officedba.EmployeeInfo AS e ON s.Seller = e.ID               ";
            strSql += "WHERE (s.Seller = @Seller) AND (s.CompanyCD = @CompanyCD)             ";
            strSql += "and s.FindDate >= @FindDate  and s.FindDate <= @FindDate1    ";
            strSql += "GROUP BY e.EmployeeName  ,s.Seller                                    ";


            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@FindDate", FindDate));
            arr.Add(new SqlParameter("@FindDate1", FindDate1));
            arr.Add(new SqlParameter("@Seller", SellerID));
            DataTable dt = new DataTable();
            dt = SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
            
            return dt;
        }


        /// <summary>
        /// 按业务员统计
        /// </summary>
        /// <param name="FindDate">开始日期</param>
        /// <param name="FindDate1">结束日期</param>
        /// <param name="SellerID">业务员编号</param>
        /// <returns></returns>
        public static DataTable GetChanceBySeller(DateTime FindDate, DateTime FindDate1, int SellerID)
        {
            string strCompanyCD = string.Empty;//单位编号
            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            
            string strSql = string.Empty;
            FindDate1 = FindDate1.AddDays(1);
            strSql = "SELECT COUNT(1) AS chanceCount, e.EmployeeName               ";
            strSql += "FROM officedba.SellChance AS s LEFT OUTER JOIN               ";
            strSql += "officedba.EmployeeInfo AS e ON s.Seller = e.ID               ";
            strSql += "WHERE (s.Seller = @Seller) AND (s.CompanyCD = @CompanyCD)             ";
            strSql += "and s.FindDate >= @FindDate  and s.FindDate <= @FindDate1    ";
            strSql += "GROUP BY e.EmployeeName  ,s.Seller                                    ";   


            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@FindDate", FindDate));
            arr.Add(new SqlParameter("@FindDate1", FindDate1));
            arr.Add(new SqlParameter("@Seller", SellerID));
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteSql(strSql, arr);
            
            return dt;
        }

        #endregion

        #region 按业务员与阶段统计

        /// <summary>
        /// 按业务员与阶段统计
        /// </summary>
        /// <param name="FindDate">开始日期</param>
        /// <param name="FindDate1">结束日期</param>
        /// <param name="SellerID">业务员编号</param>
        /// <param name="Phase">阶段</param>
        /// <returns></returns>
        public static DataTable GetChanceBySellerAndPhase(DateTime FindDate, DateTime FindDate1, int? SellerID,
            string Phase, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strCompanyCD = string.Empty;//单位编号
            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            FindDate1 = FindDate1.AddDays(1);
            string strSql = string.Empty;

            strSql = "SELECT  ISNULL(e.EmployeeName, ' ') AS EmployeeName, COUNT(1) AS chanceCount,                         ";
            strSql += "CASE sp.Phase WHEN '1' THEN '初期沟通' WHEN '2' THEN '立项评估'                                      ";
            strSql += "WHEN '3' THEN '需求分析' WHEN '4' THEN '方案指定' WHEN '5' THEN '招投标/竞争'                        ";
            strSql += "WHEN '6' THEN '商务谈判' WHEN '7' THEN '合同签约' END AS PhaseName                                   ";
            strSql += "FROM officedba.SellChance AS s LEFT OUTER JOIN                                                       ";
            strSql += "(SELECT  scp.CompanyCD, scp.ChanceNo,scp.Phase                                                       ";
            strSql += "FROM officedba.SellChancePush AS scp left JOIN                                                      ";
            strSql += "(SELECT MAX(Phase) AS Phase, ChanceNo,CompanyCD                                                                ";
            strSql += "FROM officedba.SellChancePush    where  CompanyCD=@CompanyCD                                                                      ";
            strSql += "GROUP BY ChanceNo,CompanyCD) AS scp1 ON scp.ChanceNo = scp1.ChanceNo and scp.CompanyCD  = scp1.CompanyCD  AND scp.Phase = scp1.Phase) AS sp ON      ";
            strSql += "s.ChanceNo = sp.ChanceNo AND s.CompanyCD = sp.CompanyCD LEFT OUTER JOIN                              ";
            strSql += "officedba.EmployeeInfo AS e ON s.Seller = e.ID AND s.CompanyCD = e.CompanyCD                         ";
            strSql += "WHERE (s.CompanyCD = @CompanyCD)                         ";
            ArrayList arr = new ArrayList();
            if (SellerID != null)
            {
                strSql += " AND (s.Seller = @Seller) ";
                arr.Add(new SqlParameter("@Seller", SellerID));
            }
            if (Phase != null)
            {
                strSql += " AND (sp.Phase=@Phase ) ";
                arr.Add(new SqlParameter("@Phase", Phase));
            }
            strSql += "and s.FindDate >= @FindDate  and s.FindDate <= @FindDate1    ";
            strSql += "GROUP BY e.EmployeeName,sp.Phase ,s.Seller                                                                    "; 
  
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@FindDate", FindDate));
            arr.Add(new SqlParameter("@FindDate1", FindDate1));
           
            DataTable dt = new DataTable();
            dt = SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
           
            return dt;
        }

        /// <summary>
        /// 按业务员与阶段统计
        /// </summary>
        /// <param name="FindDate">开始日期</param>
        /// <param name="FindDate1">结束日期</param>
        /// <param name="SellerID">业务员编号</param>
        /// <param name="Phase">阶段</param>
        /// <returns></returns>
        public static DataTable GetChanceBySellerAndPhase(DateTime FindDate, DateTime FindDate1, int? SellerID, string Phase)
        {
            string strCompanyCD = string.Empty;//单位编号
           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            
            FindDate1 = FindDate1.AddDays(1);
            string strSql = string.Empty;

            strSql = "SELECT  ISNULL(e.EmployeeName, ' ') AS EmployeeName, COUNT(1) AS chanceCount,                         ";
            strSql += "CASE sp.Phase WHEN '1' THEN '初期沟通' WHEN '2' THEN '立项评估'                                      ";
            strSql += "WHEN '3' THEN '需求分析' WHEN '4' THEN '方案指定' WHEN '5' THEN '招投标/竞争'                        ";
            strSql += "WHEN '6' THEN '商务谈判' WHEN '7' THEN '合同签约' END AS PhaseName                                   ";
            strSql += "FROM officedba.SellChance AS s LEFT OUTER JOIN                                                       ";
            strSql += "(SELECT  scp.CompanyCD, scp.ChanceNo,scp.Phase                                                       ";
            strSql += "FROM officedba.SellChancePush AS scp LEFT JOIN                                                      ";
            strSql += "(SELECT MAX(Phase) AS Phase, ChanceNo ,CompanyCD                                                               ";
            strSql += "FROM officedba.SellChancePush      where  CompanyCD=@CompanyCD                                                                    ";
            strSql += "GROUP BY ChanceNo,CompanyCD) AS scp1 ON scp.ChanceNo = scp1.ChanceNo and scp.CompanyCD  = scp1.CompanyCD  AND scp.Phase = scp1.Phase) AS sp ON      ";
            strSql += "s.ChanceNo = sp.ChanceNo AND s.CompanyCD = sp.CompanyCD LEFT OUTER JOIN                              ";
            strSql += "officedba.EmployeeInfo AS e ON s.Seller = e.ID AND s.CompanyCD = e.CompanyCD                         ";
            strSql += "WHERE (s.CompanyCD = @CompanyCD)                         ";
            ArrayList arr = new ArrayList();
            if (SellerID != null)
            {
                strSql += " AND (s.Seller = @Seller) ";
                arr.Add(new SqlParameter("@Seller", SellerID));
            }
            if (Phase != null)
            {
                strSql += " AND (sp.Phase=@Phase ) ";
                arr.Add(new SqlParameter("@Phase", Phase));
            }
            strSql += "and s.FindDate >= @FindDate  and s.FindDate <= @FindDate1    ";
            strSql += "GROUP BY e.EmployeeName,sp.Phase ,s.Seller                                                                    ";

            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@FindDate", FindDate));
            arr.Add(new SqlParameter("@FindDate1", FindDate1));

            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteSql(strSql, arr);
           
            return dt;
        }

        #endregion

        #region 按业务员与状态统计

        /// <summary>
        /// 按业务员与状态统计
        /// </summary>
        /// <param name="FindDate">开始日期</param>
        /// <param name="FindDate1">结束日期</param>
        /// <param name="SellerID">业务员编号</param>
        /// <param name="State">状态</param>
        /// <returns></returns>
        public static DataTable GetChanceOfState(DateTime FindDate, DateTime FindDate1, int? SellerID, string State,
            int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strCompanyCD = string.Empty;//单位编号
           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            FindDate1 = FindDate1.AddDays(1);
            string strSql = string.Empty;

            strSql = "SELECT  ISNULL(e.EmployeeName, ' ') AS EmployeeName, COUNT(1) AS chanceCount,                           ";
            strSql += "CASE sp.State WHEN '1' THEN '跟踪' WHEN '2' THEN '成功' WHEN '3'                                        ";
            strSql += "THEN '失败' WHEN '4' THEN '搁置' WHEN '4' THEN '失效' END AS PhaseName                                  ";
            strSql += "FROM officedba.SellChance AS s LEFT OUTER JOIN                                                          ";
            strSql += "(SELECT  scp.CompanyCD, scp.ChanceNo,scp.State                                                          ";
            strSql += "FROM officedba.SellChancePush AS scp INNER JOIN                                                         ";
            strSql += "(SELECT MAX(Phase) AS Phase, ChanceNo ,CompanyCD                                                                  ";
            strSql += "FROM officedba.SellChancePush   where  CompanyCD=@CompanyCD                                                                          ";
            strSql += "GROUP BY ChanceNo,CompanyCD) AS scp1 ON scp.ChanceNo = scp1.ChanceNo  and scp.CompanyCD  = scp1.CompanyCD AND scp.Phase = scp1.Phase) AS sp ON         ";
            strSql += "s.ChanceNo = sp.ChanceNo AND s.CompanyCD = sp.CompanyCD LEFT OUTER JOIN                                 ";
            strSql += "officedba.EmployeeInfo AS e ON s.Seller = e.ID AND s.CompanyCD = e.CompanyCD                            ";
            strSql += "WHERE (s.CompanyCD = @CompanyCD)                         ";
            ArrayList arr = new ArrayList();
            if (SellerID != null)
            {
                strSql += " AND (s.Seller = @Seller) ";
                arr.Add(new SqlParameter("@Seller", SellerID));
            }
            if (State != null)
            {
                strSql += " AND (sp.State=@State ) ";
                arr.Add(new SqlParameter("@State", State));
            }
            strSql += "and s.FindDate >= @FindDate  and s.FindDate <= @FindDate1    ";
            strSql += "GROUP BY e.EmployeeName,sp.State,s.Seller                                                                     ";

            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@FindDate", FindDate));
            arr.Add(new SqlParameter("@FindDate1", FindDate1));

            DataTable dt = new DataTable();
            dt = SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
           
            return dt;
        }

        /// <summary>
        /// 按业务员与状态统计
        /// </summary>
        /// <param name="FindDate">开始日期</param>
        /// <param name="FindDate1">结束日期</param>
        /// <param name="SellerID">业务员编号</param>
        /// <param name="State">状态</param>
        /// <returns></returns>
        public static DataTable GetChanceOfState(DateTime FindDate, DateTime FindDate1, int? SellerID, string State)
        {
            string strCompanyCD = string.Empty;//单位编号
           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            FindDate1 = FindDate1.AddDays(1);
            string strSql = string.Empty;

            strSql = "SELECT  ISNULL(e.EmployeeName, ' ') AS EmployeeName, COUNT(1) AS chanceCount,                           ";
            strSql += "CASE sp.State WHEN '1' THEN '跟踪' WHEN '2' THEN '成功' WHEN '3'                                        ";
            strSql += "THEN '失败' WHEN '4' THEN '搁置' WHEN '4' THEN '失效' END AS PhaseName                                  ";
            strSql += "FROM officedba.SellChance AS s LEFT OUTER JOIN                                                          ";
            strSql += "(SELECT  scp.CompanyCD, scp.ChanceNo,scp.State                                                          ";
            strSql += "FROM officedba.SellChancePush AS scp INNER JOIN                                                         ";
            strSql += "(SELECT MAX(Phase) AS Phase, ChanceNo ,CompanyCD                                                                  ";
            strSql += "FROM officedba.SellChancePush     where  CompanyCD=@CompanyCD                                                                          ";
            strSql += "GROUP BY ChanceNo,CompanyCD) AS scp1 ON scp.ChanceNo = scp1.ChanceNo  and scp.CompanyCD  = scp1.CompanyCD  AND scp.Phase = scp1.Phase) AS sp ON         ";
            strSql += "s.ChanceNo = sp.ChanceNo AND s.CompanyCD = sp.CompanyCD LEFT OUTER JOIN                                 ";
            strSql += "officedba.EmployeeInfo AS e ON s.Seller = e.ID AND s.CompanyCD = e.CompanyCD                            ";
            strSql += "WHERE (s.CompanyCD = @CompanyCD)                         ";
            ArrayList arr = new ArrayList();
            if (SellerID != null)
            {
                strSql += " AND (s.Seller = @Seller) ";
                arr.Add(new SqlParameter("@Seller", SellerID));
            }
            if (State != null)
            {
                strSql += " AND (sp.State=@State ) ";
                arr.Add(new SqlParameter("@State", State));
            }
            strSql += "and s.FindDate >= @FindDate  and s.FindDate <= @FindDate1    ";
            strSql += "GROUP BY e.EmployeeName,sp.State,s.Seller                                                                     ";

            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@FindDate", FindDate));
            arr.Add(new SqlParameter("@FindDate1", FindDate1));

            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteSql(strSql, arr);


            return dt;
        }

        #endregion

        #region 按来源统计

        /// <summary>
        /// 按来源统计
        /// </summary>
        /// <param name="FindDate">开始日期</param>
        /// <param name="FindDate1">结束日期</param>
        /// <param name="HapSource">机会来源</param>
        /// <returns></returns>
        public static DataTable GetChanceByHapSource(DateTime FindDate, DateTime FindDate1, int? HapSource,
            int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strCompanyCD = string.Empty;//单位编号
           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            
            FindDate1 = FindDate1.AddDays(1);
            string strSql = string.Empty;

            strSql += "SELECT COUNT(1) AS chanceCount, ISNULL(cpt1.TypeName, '无来源') AS EmployeeName   ";
            strSql += "FROM officedba.SellChance AS s LEFT OUTER JOIN                              ";
            strSql += "officedba.CodePublicType AS cpt1 ON s.HapSource = cpt1.ID                   ";

            ArrayList arr = new ArrayList();
            strSql += "WHERE   (s.CompanyCD = @CompanyCD)             ";
            if (HapSource != null)
            {
                if (HapSource != 0)
                {
                    strSql += " AND (s.HapSource = @HapSource)";
                    arr.Add(new SqlParameter("@HapSource", HapSource));
                }
                else
                {
                    strSql += " AND (s.HapSource is null ) ";
                }
            }
            strSql += "and s.FindDate >= @FindDate  and s.FindDate <= @FindDate1    ";
            strSql += "GROUP BY cpt1.TypeName ,s.HapSource                                        ";

            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@FindDate", FindDate));
            arr.Add(new SqlParameter("@FindDate1", FindDate1));
          
            DataTable dt = new DataTable();
            dt = SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
           
            return dt;
        }

        /// <summary>
        /// 按来源统计
        /// </summary>
        /// <param name="FindDate">开始日期</param>
        /// <param name="FindDate1">结束日期</param>
        /// <param name="HapSource">机会来源</param>
        /// <returns></returns>
        public static DataTable GetChanceByHapSource(DateTime FindDate, DateTime FindDate1, int? HapSource)
        {
            string strCompanyCD = string.Empty;//单位编号
           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            FindDate1 = FindDate1.AddDays(1);
            string strSql = string.Empty;

            strSql += "SELECT COUNT(1) AS chanceCount, ISNULL(cpt1.TypeName, '无来源') AS EmployeeName   ";
            strSql += "FROM officedba.SellChance AS s LEFT OUTER JOIN                              ";
            strSql += "officedba.CodePublicType AS cpt1 ON s.HapSource = cpt1.ID                   ";

            ArrayList arr = new ArrayList();
            strSql += "WHERE   (s.CompanyCD = @CompanyCD)             ";
            if (HapSource != null)
            {
                if (HapSource != 0)
                {
                    strSql += " AND (s.HapSource = @HapSource)";
                    arr.Add(new SqlParameter("@HapSource", HapSource));
                }
                else
                {
                    strSql += " AND (s.HapSource is null ) ";
                }
            }
            strSql += "and s.FindDate >= @FindDate  and s.FindDate <= @FindDate1    ";
            strSql += "GROUP BY cpt1.TypeName ,s.HapSource                                        ";

            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@FindDate", FindDate));
            arr.Add(new SqlParameter("@FindDate1", FindDate1));

            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteSql(strSql, arr);

            return dt;
        }

        #endregion


        #region 按可能性与状态统计

        /// <summary>
        /// 按可能性与状态统计
        /// </summary>
        /// <param name="FindDate">开始日期</param>
        /// <param name="FindDate1">结束日期</param>
        /// <param name="Feasibility">机会可能性</param>
        /// <param name="State">状态</param>
        /// <returns></returns>
        public static DataTable GetChanceOfStateAndFeasibility(DateTime FindDate, DateTime FindDate1, int? Feasibility, string State,
            int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strCompanyCD = string.Empty;//单位编号
           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            FindDate1 = FindDate1.AddDays(1);
            string strSql = string.Empty;

            strSql = "SELECT COUNT(1) AS chanceCount,CASE sp.State WHEN '1' THEN '跟踪' WHEN '2'                           ";
            strSql += "THEN '成功' WHEN '3' THEN '失败' WHEN '4' THEN '搁置' WHEN '4' THEN '失效' END AS StateName,         ";
            strSql += "ISNULL(cpt.TypeName, '未知 ') AS FeasibilityName                                                     ";
            strSql += "FROM officedba.SellChance AS s LEFT OUTER JOIN                                                       ";
            strSql += "(SELECT scp.CompanyCD, scp.ChanceNo, scp.Feasibility, scp.State                                      ";
            strSql += "FROM officedba.SellChancePush AS scp LEFT JOIN                                                      ";
            strSql += "(SELECT MAX(Phase) AS Phase, ChanceNo  ,CompanyCD                                                              ";
            strSql += "FROM officedba.SellChancePush    where  CompanyCD=@CompanyCD                                                                      ";
            strSql += "GROUP BY ChanceNo,CompanyCD) AS scp1 ON scp.ChanceNo = scp1.ChanceNo and scp.CompanyCD  = scp1.CompanyCD  AND scp.Phase = scp1.Phase) AS sp ON      ";
            strSql += "s.ChanceNo = sp.ChanceNo AND s.CompanyCD = sp.CompanyCD LEFT OUTER JOIN                              ";
            strSql += "officedba.CodePublicType AS cpt ON sp.Feasibility = cpt.ID AND s.CompanyCD = cpt.CompanyCD           ";
                                                                           

            strSql += "WHERE (s.CompanyCD = @CompanyCD)                         ";
            ArrayList arr = new ArrayList();
            if (Feasibility != null)
            {
                if (Feasibility != 0)
                {
                    strSql += " AND (sp.Feasibility = @Feasibility) ";
                    arr.Add(new SqlParameter("@Feasibility", Feasibility));
                }
                else
                {
                    strSql += " AND (sp.Feasibility is null ) ";
                }
               
            }
            if (State != null)
            {
                strSql += " AND (sp.State=@State ) ";
                arr.Add(new SqlParameter("@State", State));
            }
            strSql += "and s.FindDate >= @FindDate  and s.FindDate <= @FindDate1    ";
            strSql += "GROUP BY sp.State, cpt.TypeName ,sp.Feasibility                                                                        ";

            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@FindDate", FindDate));
            arr.Add(new SqlParameter("@FindDate1", FindDate1));

            DataTable dt = new DataTable();
            dt = SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
            
            return dt;
        }

        /// <summary>
        /// 按可能性与状态统计
        /// </summary>
        /// <param name="FindDate">开始日期</param>
        /// <param name="FindDate1">结束日期</param>
        /// <param name="Feasibility">机会可能性</param>
        /// <param name="State">状态</param>
        /// <returns></returns>
        public static DataTable GetChanceOfStateAndFeasibility(DateTime FindDate, DateTime FindDate1, int? Feasibility, string State)
        {
            string strCompanyCD = string.Empty;//单位编号
            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            
            FindDate1 = FindDate1.AddDays(1);
            string strSql = string.Empty;

            strSql = "SELECT COUNT(1) AS chanceCount,CASE sp.State WHEN '1' THEN '跟踪' WHEN '2'                           ";
            strSql += "THEN '成功' WHEN '3' THEN '失败' WHEN '4' THEN '搁置' WHEN '4' THEN '失效' END AS StateName,         ";
            strSql += "ISNULL(cpt.TypeName, '未知 ') AS FeasibilityName                                                     ";
            strSql += "FROM officedba.SellChance AS s LEFT OUTER JOIN                                                       ";
            strSql += "(SELECT scp.CompanyCD, scp.ChanceNo, scp.Feasibility, scp.State                                      ";
            strSql += "FROM officedba.SellChancePush AS scp LEFT JOIN                                                      ";
            strSql += "(SELECT MAX(Phase) AS Phase, ChanceNo ,CompanyCD                                                               ";
            strSql += "FROM officedba.SellChancePush    where  CompanyCD=@CompanyCD                                                                   ";
            strSql += "GROUP BY ChanceNo,CompanyCD) AS scp1 ON scp.ChanceNo = scp1.ChanceNo and scp.CompanyCD  = scp1.CompanyCD  AND scp.Phase = scp1.Phase) AS sp ON      ";
            strSql += "s.ChanceNo = sp.ChanceNo AND s.CompanyCD = sp.CompanyCD LEFT OUTER JOIN                              ";
            strSql += "officedba.CodePublicType AS cpt ON sp.Feasibility = cpt.ID AND s.CompanyCD = cpt.CompanyCD           ";


            strSql += "WHERE (s.CompanyCD = @CompanyCD)                         ";
            ArrayList arr = new ArrayList();
            if (Feasibility != null)
            {
                if (Feasibility != 0)
                {
                    strSql += " AND (sp.Feasibility = @Feasibility) ";
                    arr.Add(new SqlParameter("@Feasibility", Feasibility));
                }
                else
                {
                    strSql += " AND (sp.Feasibility is null ) ";
                }

            }
            if (State != null)
            {
                strSql += " AND (sp.State=@State ) ";
                arr.Add(new SqlParameter("@State", State));
            }
            strSql += "and s.FindDate >= @FindDate  and s.FindDate <= @FindDate1    ";
            strSql += "GROUP BY sp.State, cpt.TypeName ,sp.Feasibility                                                                        ";

            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@FindDate", FindDate));
            arr.Add(new SqlParameter("@FindDate1", FindDate1));

            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteSql(strSql, arr);
           
            return dt;
        }

        #endregion

        #region 按阶段与可能性统计

        /// <summary>
        /// 按阶段与可能性统计
        /// </summary>
        /// <param name="FindDate">开始日期</param>
        /// <param name="FindDate1">结束日期</param>
        /// <param name="Feasibility">阶段</param>
        /// <param name="Phase">阶段</param>
        /// <returns></returns>
        public static DataTable GetChanceOfPhaseAndFeasibility(DateTime FindDate, DateTime FindDate1, int? Feasibility, string Phase,
             int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strCompanyCD = string.Empty;//单位编号
            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            FindDate1 = FindDate1.AddDays(1);
            string strSql = string.Empty;

            strSql += "SELECT COUNT(1) AS chanceCount,CASE sp.Phase WHEN '1' THEN '初期沟通' WHEN '2'                      ";
            strSql += "THEN '立项评估' WHEN '3' THEN '需求分析' WHEN '4' THEN '方案指定' WHEN '5' THEN '招投标/竞争'       ";
            strSql += "WHEN '6' THEN '商务谈判' WHEN '7' THEN '合同签约' END AS StateName,                                 ";
            strSql += "ISNULL(cpt.TypeName, '未知 ') AS FeasibilityName                                                    ";
            strSql += "FROM officedba.SellChance AS s LEFT OUTER JOIN                                                      ";
            strSql += "(SELECT scp.CompanyCD, scp.ChanceNo, scp.Feasibility, scp.Phase                                     ";
            strSql += "FROM officedba.SellChancePush AS scp left JOIN                                                     ";
            strSql += "(SELECT MAX(Phase) AS Phase, ChanceNo ,CompanyCD                                                              ";
            strSql += "FROM officedba.SellChancePush     where  CompanyCD=@CompanyCD                                                                    ";
            strSql += "GROUP BY ChanceNo,CompanyCD) AS scp1 ON scp.ChanceNo = scp1.ChanceNo and scp.CompanyCD  = scp1.CompanyCD  AND scp.Phase = scp1.Phase) AS sp ON     ";
            strSql += "s.ChanceNo = sp.ChanceNo AND s.CompanyCD = sp.CompanyCD LEFT OUTER JOIN                             ";
            strSql += "officedba.CodePublicType AS cpt ON sp.Feasibility = cpt.ID AND s.CompanyCD = cpt.CompanyCD          ";
           


            strSql += "WHERE (s.CompanyCD = @CompanyCD)                         ";
            ArrayList arr = new ArrayList();
            if (Feasibility != null)
            {
                if (Feasibility != 0)
                {
                    strSql += " AND (sp.Feasibility = @Feasibility) ";
                    arr.Add(new SqlParameter("@Feasibility", Feasibility));
                }
                else
                {
                    strSql += " AND (sp.Feasibility is null ) ";
                }

            }
            if (Phase != null)
            {
                strSql += " AND (sp.Phase=@Phase ) ";
                arr.Add(new SqlParameter("@Phase", Phase));
            }
            strSql += "and s.FindDate >= @FindDate  and s.FindDate <= @FindDate1    ";
            strSql += "GROUP BY sp.Phase, cpt.TypeName                                                                     "; 




            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@FindDate", FindDate));
            arr.Add(new SqlParameter("@FindDate1", FindDate1));

            DataTable dt = new DataTable();
            dt = SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
            
            return dt;
        }

        /// <summary>
        /// 按阶段与可能性统计
        /// </summary>
        /// <param name="FindDate">开始日期</param>
        /// <param name="FindDate1">结束日期</param>
        /// <param name="Feasibility">阶段</param>
        /// <param name="Phase">阶段</param>
        /// <returns></returns>
        public static DataTable GetChanceOfPhaseAndFeasibility(DateTime FindDate, DateTime FindDate1, int? Feasibility, string Phase)
        {
            string strCompanyCD = string.Empty;//单位编号
           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            
            FindDate1 = FindDate1.AddDays(1);
            string strSql = string.Empty;

            strSql += "SELECT COUNT(1) AS chanceCount,CASE sp.Phase WHEN '1' THEN '初期沟通' WHEN '2'                      ";
            strSql += "THEN '立项评估' WHEN '3' THEN '需求分析' WHEN '4' THEN '方案指定' WHEN '5' THEN '招投标/竞争'       ";
            strSql += "WHEN '6' THEN '商务谈判' WHEN '7' THEN '合同签约' END AS StateName,                                 ";
            strSql += "ISNULL(cpt.TypeName, '未知 ') AS FeasibilityName                                                    ";
            strSql += "FROM officedba.SellChance AS s LEFT OUTER JOIN                                                      ";
            strSql += "(SELECT scp.CompanyCD, scp.ChanceNo, scp.Feasibility, scp.Phase                                     ";
            strSql += "FROM officedba.SellChancePush AS scp left JOIN                                                     ";
            strSql += "(SELECT MAX(Phase) AS Phase, ChanceNo ,CompanyCD                                                              ";
            strSql += "FROM officedba.SellChancePush        where  CompanyCD=@CompanyCD                                                                 ";
            strSql += "GROUP BY ChanceNo,CompanyCD) AS scp1 ON scp.ChanceNo = scp1.ChanceNo and scp.CompanyCD  = scp1.CompanyCD  AND scp.Phase = scp1.Phase) AS sp ON     ";
            strSql += "s.ChanceNo = sp.ChanceNo AND s.CompanyCD = sp.CompanyCD LEFT OUTER JOIN                             ";
            strSql += "officedba.CodePublicType AS cpt ON sp.Feasibility = cpt.ID AND s.CompanyCD = cpt.CompanyCD          ";



            strSql += "WHERE (s.CompanyCD = @CompanyCD)                         ";
            ArrayList arr = new ArrayList();
            if (Feasibility != null)
            {
                if (Feasibility != 0)
                {
                    strSql += " AND (sp.Feasibility = @Feasibility) ";
                    arr.Add(new SqlParameter("@Feasibility", Feasibility));
                }
                else
                {
                    strSql += " AND (sp.Feasibility is null ) ";
                }

            }
            if (Phase != null)
            {
                strSql += " AND (sp.Phase=@Phase ) ";
                arr.Add(new SqlParameter("@Phase", Phase));
            }
            strSql += "and s.FindDate >= @FindDate  and s.FindDate <= @FindDate1    ";
            strSql += "GROUP BY sp.Phase, cpt.TypeName                                                                     ";




            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@FindDate", FindDate));
            arr.Add(new SqlParameter("@FindDate1", FindDate1));

            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteSql(strSql, arr);
           
            return dt;
        }

        #endregion

        #region 按预计签单月份统计

        /// <summary>
        /// 按预计签单月份统计
        /// </summary>
        /// <param name="IntendDate">开始日期</param>
        /// <param name="IntendDate1">结束日期</param>
        /// <returns></returns>
        public static DataTable GetChanceOfIntendDate(DateTime IntendDate, DateTime IntendDate1, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strCompanyCD = string.Empty;//单位编号
          
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            string strSql = string.Empty;
            strSql += "SELECT COUNT(1) AS chanceCount, LEFT(CONVERT(varchar(10), IntendDate, 20), 7) AS IntendDate ";
            strSql += "FROM officedba.SellChance AS s                                                              ";
            strSql += "WHERE (CompanyCD = @CompanyCD and LEFT(CONVERT(varchar(10), s.IntendDate, 20), 7) >= @IntendDate  and LEFT(CONVERT(varchar(10), s.IntendDate, 20), 7) <= @IntendDate1  ) ";
            strSql += "GROUP BY LEFT(CONVERT(varchar(10), s.IntendDate, 20), 7)                                      ";   

          
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@IntendDate", IntendDate.ToString("yyyy-MM")));
            arr.Add(new SqlParameter("@IntendDate1", IntendDate1.ToString("yyyy-MM")));
            DataTable dt = new DataTable();
            dt = SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
            
            return dt;
        }

        /// <summary>
        /// 按预计签单月份统计
        /// </summary>
        /// <param name="IntendDate">开始日期</param>
        /// <param name="IntendDate1">结束日期</param>
        /// <returns></returns>
        public static DataTable GetChanceOfIntendDate(DateTime IntendDate, DateTime IntendDate1)
        {
            string strCompanyCD = string.Empty;//单位编号
           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            
            string strSql = string.Empty;
            strSql += "SELECT COUNT(1) AS chanceCount, LEFT(CONVERT(varchar(10), IntendDate, 20), 7) AS IntendDate ";
            strSql += "FROM officedba.SellChance AS s                                                              ";
            strSql += "WHERE (CompanyCD = @CompanyCD and LEFT(CONVERT(varchar(10), s.IntendDate, 20), 7) >= @IntendDate  and LEFT(CONVERT(varchar(10), s.IntendDate, 20), 7) <= @IntendDate1  ) ";
            strSql += "GROUP BY LEFT(CONVERT(varchar(10), s.IntendDate, 20), 7)                                      ";


            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@IntendDate", IntendDate.ToString("yyyy-MM")));
            arr.Add(new SqlParameter("@IntendDate1", IntendDate1.ToString("yyyy-MM")));
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteSql(strSql, arr);
         
            return dt;
        }

        #endregion


        #region 按创建时间统计

        /// <summary>
        /// 按创建时间统计
        /// </summary>
        /// <param name="CreateDate">开始日期</param>
        /// <param name="CreateDate1">结束日期</param>
        /// <returns></returns>
        public static DataTable GetChanceOfCreateDate(DateTime CreateDate, DateTime CreateDate1, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strCompanyCD = string.Empty;//单位编号
            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            
            string strSql = string.Empty;
            strSql += "SELECT COUNT(1) AS chanceCount, LEFT(CONVERT(varchar(10), CreateDate, 20), 7) AS IntendDate ";
            strSql += "FROM officedba.SellChance AS s                                                              ";
            strSql += "WHERE (CompanyCD = @CompanyCD and LEFT(CONVERT(varchar(10), CreateDate, 20), 7) >= @CreateDate  and LEFT(CONVERT(varchar(10), CreateDate, 20), 7) <= @CreateDate1  ) ";
            strSql += "GROUP BY LEFT(CONVERT(varchar(10), CreateDate, 20), 7)                                      ";


            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@CreateDate", CreateDate.ToString("yyyy-MM")));
            arr.Add(new SqlParameter("@CreateDate1", CreateDate1.ToString("yyyy-MM")));
            DataTable dt = new DataTable();
            dt = SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
            
            return dt;
        }

        /// <summary>
        /// 按创建时间统计
        /// </summary>
        /// <param name="CreateDate">开始日期</param>
        /// <param name="CreateDate1">结束日期</param>
        /// <returns></returns>
        public static DataTable GetChanceOfCreateDate(DateTime CreateDate, DateTime CreateDate1)
        {
            string strCompanyCD = string.Empty;//单位编号
           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            string strSql = string.Empty;
            strSql += "SELECT COUNT(1) AS chanceCount, LEFT(CONVERT(varchar(10), CreateDate, 20), 7) AS IntendDate ";
            strSql += "FROM officedba.SellChance AS s                                                              ";
            strSql += "WHERE (CompanyCD = @CompanyCD and LEFT(CONVERT(varchar(10), CreateDate, 20), 7) >= @CreateDate  and LEFT(CONVERT(varchar(10), CreateDate, 20), 7) <= @CreateDate1  ) ";
            strSql += "GROUP BY LEFT(CONVERT(varchar(10), CreateDate, 20), 7)                                      ";

            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@CreateDate", CreateDate.ToString("yyyy-MM")));
            arr.Add(new SqlParameter("@CreateDate1", CreateDate1.ToString("yyyy-MM")));
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteSql(strSql, arr);
           
            return dt;
        }
        #endregion


        #region 按业务员与创建时间统计

        /// <summary>
        /// 按业务员与创建时间统计
        /// </summary>
        /// <param name="CreateDate">开始日期</param>
        /// <param name="CreateDate1">结束日期</param>
        /// <param name="SellerID">业务员编号</param>
        /// <returns></returns>
        public static DataTable GetChanceOfCreateDateAndSeller(DateTime CreateDate, DateTime CreateDate1, int? SellerID,
            int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strCompanyCD = string.Empty;//单位编号
           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            string strSql = string.Empty;

            strSql = "SELECT COUNT(1) AS chanceCount, e.EmployeeName , LEFT(CONVERT(varchar(10), s.CreateDate, 20), 7)  as     CreateDate         ";
            strSql += "FROM officedba.SellChance AS s LEFT OUTER JOIN               ";
            strSql += "officedba.EmployeeInfo AS e ON s.Seller = e.ID               ";
            strSql += "WHERE  (s.CompanyCD = @CompanyCD)             ";
            ArrayList arr = new ArrayList();
            if (SellerID != null)
            {
                strSql += " AND (s.Seller = @Seller)  ";
                arr.Add(new SqlParameter("@Seller", SellerID));
            }
            strSql += "and LEFT(CONVERT(varchar(10), s.CreateDate, 20), 7)>= @CreateDate  and LEFT(CONVERT(varchar(10), s.CreateDate, 20), 7) <= @CreateDate1    ";
            strSql += "GROUP BY e.EmployeeName,s.Seller ,LEFT(CONVERT(varchar(10), s.CreateDate, 20), 7)                                      ";



            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@CreateDate", CreateDate.ToString("yyyy-MM")));
            arr.Add(new SqlParameter("@CreateDate1", CreateDate1.ToString("yyyy-MM")));

            DataTable dt = new DataTable();
            dt = SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
            
            return dt;
        }

        /// <summary>
        /// 按业务员与创建时间统计
        /// </summary>
        /// <param name="CreateDate">开始日期</param>
        /// <param name="CreateDate1">结束日期</param>
        /// <param name="SellerID">业务员编号</param>
        /// <returns></returns>
        public static DataTable GetChanceOfCreateDateAndSeller(DateTime CreateDate, DateTime CreateDate1, int? SellerID)
        {
            string strCompanyCD = string.Empty;//单位编号
           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            
            string strSql = string.Empty;

            strSql = "SELECT COUNT(1) AS chanceCount, e.EmployeeName , LEFT(CONVERT(varchar(10), s.CreateDate, 20), 7)  as     CreateDate         ";
            strSql += "FROM officedba.SellChance AS s LEFT OUTER JOIN               ";
            strSql += "officedba.EmployeeInfo AS e ON s.Seller = e.ID               ";
            strSql += "WHERE  (s.CompanyCD = @CompanyCD)             ";
            ArrayList arr = new ArrayList();
            if (SellerID != null)
            {
                strSql += " AND (s.Seller = @Seller)  ";
                arr.Add(new SqlParameter("@Seller", SellerID));
            }
            strSql += "and LEFT(CONVERT(varchar(10), s.CreateDate, 20), 7)>= @CreateDate  and LEFT(CONVERT(varchar(10), s.CreateDate, 20), 7) <= @CreateDate1    ";
            strSql += "GROUP BY e.EmployeeName,s.Seller ,LEFT(CONVERT(varchar(10), s.CreateDate, 20), 7)                                      ";



            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            arr.Add(new SqlParameter("@CreateDate", CreateDate.ToString("yyyy-MM")));
            arr.Add(new SqlParameter("@CreateDate1", CreateDate1.ToString("yyyy-MM")));

            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteSql(strSql, arr);
            
            return dt;
        }

        #endregion
        #endregion

        #region 打印
        /// <summary>
        /// 打印主表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrder(string OrderNo)
        {
            string strCompanyCD = string.Empty;//单位编号
           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            
            string strSql = "select * from  officedba.sellmodule_report_SellChance WHERE (ChanceNo = @ChanceNo) AND (CompanyCD = @CompanyCD)";
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("@ChanceNo",OrderNo)
                                   };

            DataTable dt = SqlHelper.ExecuteSql(strSql, param);
            return dt;
        }

        /// <summary>
        /// 打印子表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrderDetail(string OrderNo)
        {
            string strCompanyCD = string.Empty;//单位编号
           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            string strSql = "select * from  officedba.sellmodule_report_SellChancePush WHERE (ChanceNo = @ChanceNo) AND (CompanyCD = @CompanyCD) order by Phase asc";
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("@ChanceNo",OrderNo)
                                   };

            DataTable dt = SqlHelper.ExecuteSql(strSql, param);
            return dt;
        }
        #endregion

        #region zxb报表
        /// <summary>
        /// 绑定销售机会到下拉框
        /// zxb 2009-10-20
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="ddl"></param>
        public static void GetChanceType(string companycd, System.Web.UI.WebControls.DropDownList ddl)
        {
            SqlParameter[] paramter = { new SqlParameter("@companycd", companycd) };
            string sql = "select * from officedba.CodePublicType where CompanyCD=@companyCD and typeflag=6 and typeCode=2 and UsedStatus=1";
            DataTable dt = SqlHelper.ExecuteSql(sql,paramter);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ddl.DataSource = ds;
            ddl.DataTextField = "TypeName";
            ddl.DataValueField = "ID";
            ddl.DataBind();
            ddl.DataSource = null;
            ddl.Items.Insert(0, new System.Web.UI.WebControls.ListItem("请选择", "0"));
        }

        /// <summary>
        /// 销售机会部门数量分布
        /// zxb by 2009-10-23
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="ChanceType"></param>
        /// <param name="Phase"></param>
        /// <param name="State"></param>
        /// <returns></returns>
        public static DataTable GetChanceByDept(string companycd, int ChanceType, string Phase, string State,string begindate,string enddate)
        {
            string jingdu = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            SqlParameter[] param = 
            {
                new SqlParameter("@CompanyCD",companycd),
                new SqlParameter("@ChanceType",ChanceType),
                new SqlParameter("@Phase",Phase),
                new SqlParameter("@State",State),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                  new SqlParameter("@jingdu",jingdu)
            };
            DataTable dt = SqlHelper.ExecuteStoredProcedure("[officedba].[GetChanceByDept]", param);
            return dt;
        }

        /// <summary>
        /// 销售机会部门数量分布明细
        /// zxb by 2009-10-23
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="ChanceType"></param>
        /// <param name="Phase"></param>
        /// <param name="State"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <param name="deptName"></param>
        /// <param name="order"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static DataTable GetChanceByDeptDetials(string companycd, int ChanceType, string Phase, string State, string begindate, string enddate,string getvalue,string order,string flag,int pageindex,int pagesize, ref int recordCount)
        {
              string jingdu = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            SqlParameter[] param = 
            {
                new SqlParameter("@CompanyCD",companycd),
                new SqlParameter("@ChanceType",ChanceType),
                new SqlParameter("@Phase",Phase),
                new SqlParameter("@State",State),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@getvalue",getvalue),//传部门/人员的编号
                new SqlParameter("@order",order),
                new SqlParameter("@flag",flag),
                new SqlParameter("@pageindex",pageindex),
                new SqlParameter("@pagesize",pagesize),
                new SqlParameter ("@jingdu", jingdu)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetChanceDetails]", param);
            recordCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 部门
        /// zxb by 2009-10-24
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="ddl"></param>
        public static void GetDeptByCompanyCD(string companycd, System.Web.UI.WebControls.DropDownList ddl)
        {
            SqlParameter[] paramter = { new SqlParameter("@companycd", companycd) };
            string sql = "select * from officedba.DeptInfo where CompanyCD=@companyCD";
            DataTable dt = SqlHelper.ExecuteSql(sql, paramter);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ddl.DataSource = ds;
            ddl.DataTextField = "DeptName";
            ddl.DataValueField = "ID";
            ddl.DataBind();
            ddl.DataSource = null;
            ddl.Items.Insert(0, new System.Web.UI.WebControls.ListItem("请选择部门", "0"));
        }

        /// <summary>
        /// 销售机会部门数量分布
        /// zxb by 2009-10-23
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="ChanceType"></param>
        /// <param name="Phase"></param>
        /// <param name="State"></param>
        /// <returns></returns>
        public static DataTable GetChanceByDept(string companycd, int ChanceType, string Phase, string State, string begindate, string enddate, string deptcode)
        {

            string jingdu = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;

            SqlParameter[] param = 
            {
                new SqlParameter("@CompanyCD",companycd),
                new SqlParameter("@ChanceType",ChanceType),
                new SqlParameter("@Phase",Phase),
                new SqlParameter("@State",State),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@deptcode",deptcode),
                new SqlParameter ("@jingdu", jingdu)
            };
            DataTable dt = SqlHelper.ExecuteStoredProcedure("[officedba].[GetChanceByDept_person]", param);
            return dt;
        }

        /// <summary>
        /// 销售机会状态分布
        /// zxb by 2009-10-27
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="deptcode"></param>
        /// <param name="seller"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public static DataTable GetChanceState(string companycd, int deptcode, int seller, string begindate, string enddate)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@CompanyCD",companycd),
                new SqlParameter("@deptcode",deptcode),
                new SqlParameter("@seller",seller),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate)
            };
            DataTable dt = SqlHelper.ExecuteStoredProcedure("[officedba].[GetChanceState]", param);
            return dt;
        }

        /// <summary>
        /// 根据销售机会状态获取明细
        /// zxb by 2009-10-27
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="State"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <param name="order"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static DataTable GetChanceByStateDetials(string companycd, int State, string begindate, string enddate, string order,int pageindex,int pagesize, ref int recordCount)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@CompanyCD",companycd),
                new SqlParameter("@State",State),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@order",order),
                new SqlParameter("@pageindex",pageindex),
                new SqlParameter("@pagesize",pagesize)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetChanceDetailsByState]", param);
            recordCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 销售机会阶段分布
        /// zxb by 2009-10-27
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="deptcode"></param>
        /// <param name="seller"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public static DataTable GetChancePhase(string companycd, int deptcode, int seller, string begindate, string enddate)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@CompanyCD",companycd),
                new SqlParameter("@deptcode",deptcode),
                new SqlParameter("@seller",seller),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate)
            };
            DataTable dt = SqlHelper.ExecuteStoredProcedure("[officedba].[GetChancePhase]", param);
            return dt;
        }

        /// <summary>
        /// 根据销售机会阶段获取明细
        /// zxb by 2009-10-27
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="State"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <param name="order"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static DataTable GetChanceByPhaseDetials(string companycd, int Phase, string begindate, string enddate, string order,int pageindex,int pagesize, ref int recordCount)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@CompanyCD",companycd),
                new SqlParameter("@Phase",Phase),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@order",order),
                new SqlParameter("@pageindex",pageindex),
                new SqlParameter("@pagesize",pagesize)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetChanceDetailsByPhase]", param);
            recordCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 销售机会时间走势分析
        /// zxb by 2009-10-27
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="deptcode"></param>
        /// <param name="Phase"></param>
        /// <param name="State"></param>
        /// <param name="deptcode"></param>
        /// <param name="seller"></param>
        /// <param name="timetype"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public static DataTable GetChanceSetUp(string companycd, int chanceType, string Phase, string State, int deptcode, int seller, int timetype, string begindate, string enddate)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@CompanyCD",companycd),
                new SqlParameter("@ChanceType",chanceType),
                new SqlParameter("@Phase",Phase),
                new SqlParameter("@State",State),
                new SqlParameter("@deptcode",deptcode),
                new SqlParameter("@seller",seller),
                new SqlParameter("@timetype",timetype),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate)
            };
            DataTable dt = SqlHelper.ExecuteStoredProcedure("[officedba].[GetChanceSetup]", param);
            return dt;
        }

        /// <summary>
        /// 时间走势明细
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="timeType"></param>
        /// <param name="timestr"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <param name="order"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static DataTable GetChanceBySetUpDetials(string companycd, string timeType,string timestr, string begindate, string enddate, string order,int pageindex,int pagesize, ref int recordCount)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@CompanyCD",companycd),
                new SqlParameter("@timeType",timeType),
                new SqlParameter("@timestr",timestr),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate),
                new SqlParameter("@order",order),
                new SqlParameter("@pageindex",pageindex),
                new SqlParameter("@pagesize",pagesize)
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetChanceDetailsBySetup]", param);
            recordCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 实际到帐部门比较
        /// zxb by 2009-10-29
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public static DataTable GetActiveMoneyByDept(string companycd,string begindate, string enddate)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@CompanyCD",companycd),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate)
            };
            DataTable dt = SqlHelper.ExecuteStoredProcedure("[officedba].[ActiveMoneyCharge]", param);
            return dt;
        }

        /// <summary>
        /// 实际到帐人员比较
        /// zxb by 2009-10-29
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="begindate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public static DataTable GetActiveMoneyByPerson(string companycd, string begindate, string enddate)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@CompanyCD",companycd),
                new SqlParameter("@begindate",begindate),
                new SqlParameter("@enddate",enddate)
            };
            DataTable dt = SqlHelper.ExecuteStoredProcedure("[officedba].[ActiveMoneyCharge_person]", param);
            return dt;
        }
        #endregion

        #region 扩展属性保存操作
        /// <summary>
        /// 扩展属性保存操作
        /// </summary>
        /// <returns></returns>
        private static void GetExtAttrCmd(SellChanceModel model, Hashtable htExtAttr, TransactionManager tran)
        {
            try
            {
                string strSql = string.Empty;
                strSql = "UPDATE officedba.SellChance set ";

                SqlParameter[] parameters = new SqlParameter[htExtAttr.Count + 2];
                int i = 0;

                foreach (DictionaryEntry de in htExtAttr)// de为一个Hashtable实例
                {
                    strSql += de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",";
                    parameters[i] = SqlHelper.GetParameter("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim());
                    i++;
                }
                int iLength = strSql.Length - 1;
                strSql = strSql.Substring(0, iLength);
                strSql += " where CompanyCD = @CompanyCD  AND ChanceNo = @ChanceNo";
                parameters[i] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
                parameters[i + 1] = SqlHelper.GetParameter("@ChanceNo", model.ChanceNo);
                //cmd.Parameters.Add("@CompanyCD", model.CompanyCD);
                //cmd.Parameters.Add("@PlanNo", model.PlanNo);
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), parameters);
                //cmd.CommandText = strSql;
            }
            catch (Exception ex)
            {
                string ss = ex.Message;
            }
        }
        #endregion
    }
}

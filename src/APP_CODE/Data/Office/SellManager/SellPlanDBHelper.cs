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
using XBase.Data.Common;

namespace XBase.Data.Office.SellManager
{
    public class SellPlanDBHelper
    {
        #region 添加、修改、删除相关操作

        /// <summary>
        /// 保存销售计划
        /// </summary>
        /// <returns></returns>
        public static bool Save(Hashtable ht,SellPlanModel sellPlanModel, SellPlanDetailModel sellPlanDetailModel, out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            strMsg = "";
            //判断计划编号是否存在
            if (NoIsExist(sellPlanModel.PlanNo))
            {
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    InsertOrder(sellPlanModel, tran);

                    #region 拓展属性                  
                    GetExtAttrCmd(sellPlanModel, ht, tran);
                    #endregion

                    if (sellPlanDetailModel.PlanNo != null)
                    {
                        InsertOrderDetail(sellPlanDetailModel, tran);
                    }
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
                strMsg = "该编号已被使用，请输入未被使用的编号！";
            }
            return isSucc;
        }

        /// <summary>
        /// 更新销售计划
        /// </summary>
        /// <returns></returns>
        public static bool Update(Hashtable ht,SellPlanModel sellPlanModel, SellPlanDetailModel sellPlanDetailModel, string strDetailAction, out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            strMsg = "";
            if (IsUpdate(sellPlanModel.PlanNo))
            {

                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {

                    UpdateOrder(sellPlanModel, tran);

                    #region 拓展属性
                    GetExtAttrCmd(sellPlanModel, ht, tran);
                    #endregion

                    //明细操作的类型
                    switch (strDetailAction)
                    {
                        case "1"://无操作
                            break;
                        case "2"://添加新明细
                            InsertOrderDetail(sellPlanDetailModel, tran);
                            break;
                        case "3"://更新明细
                            UpdateOrderDetail(sellPlanDetailModel, tran);
                            break;
                        case "4"://删除明细
                            DelOrderDetail(sellPlanDetailModel, tran);
                            break;
                        default:
                            break;
                    }

                    tran.Commit();
                    strMsg = "保存成功！";
                    isSucc = true;
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
                strMsg = "非制单状态的未提交审批、审批未通过或撤销审批计划不可修改！";
            }
            return isSucc;
        }

        /// <summary>
        /// 跟新主表数据
        /// </summary>
        /// <param name="sellBackModel"></param>
        /// <param name="tran"></param>
        private static void UpdateOrder(SellPlanModel model, TransactionManager tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.SellPlan set ");
            strSql.Append("Title=@Title,");
            strSql.Append("PlanType=@PlanType,");
            strSql.Append("PlanYear=@PlanYear,");
            strSql.Append("PlanTime=@PlanTime,");
            strSql.Append("PlanTotal=@PlanTotal,");
            strSql.Append("StartDate=@StartDate,");
            strSql.Append("EndDate=@EndDate,");
            strSql.Append("MinPlanTotal=@MinPlanTotal,");
            strSql.Append("Hortation=@Hortation,");
            strSql.Append("CanViewUser=@CanViewUser,");
            strSql.Append("CanViewUserName=@CanViewUserName,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("ModifiedDate=@ModifiedDate,");
            strSql.Append("ModifiedUserID=@ModifiedUserID");
            strSql.Append(" where ID=@ID ");

            #region 参数
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@PlanType", SqlDbType.Char,1),
					new SqlParameter("@PlanYear", SqlDbType.VarChar,10),
					new SqlParameter("@PlanTime", SqlDbType.VarChar,2),
					new SqlParameter("@PlanTotal", SqlDbType.Decimal,13),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@MinPlanTotal", SqlDbType.Decimal,13),
					new SqlParameter("@Hortation", SqlDbType.VarChar,1024),
					new SqlParameter("@CanViewUser", SqlDbType.VarChar,2048),
					new SqlParameter("@CanViewUserName", SqlDbType.VarChar,2048),
					new SqlParameter("@Remark", SqlDbType.VarChar,1024),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.PlanType;
            parameters[3].Value = model.PlanYear;
            parameters[4].Value = model.PlanTime;
            parameters[5].Value = model.PlanTotal;
            parameters[6].Value = model.StartDate;
            parameters[7].Value = model.EndDate;
            parameters[8].Value = model.MinPlanTotal;
            parameters[9].Value = model.Hortation;
            parameters[10].Value = model.CanViewUser;
            parameters[11].Value = model.CanViewUserName;
            parameters[12].Value = model.Remark;
            parameters[13].Value = model.ModifiedDate;
            parameters[14].Value = model.ModifiedUserID;


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
        /// 跟新子表数据
        /// </summary>
        /// <param name="sellBackModel"></param>
        /// <param name="tran"></param>
        private static void UpdateOrderDetail(SellPlanDetailModel model, TransactionManager tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.SellPlanDetail set ");
            strSql.Append("DetailType=@DetailType,");
            strSql.Append("DetailID=@DetailID,");
            strSql.Append("DetailTotal=@DetailTotal,");
            strSql.Append("MinDetailotal=@MinDetailotal");
            strSql.Append(" where ID=@ID ");

            #region 参数
            SqlParameter[] parameters = {
					new SqlParameter("@DetailType", SqlDbType.Char,1),
					new SqlParameter("@DetailID", SqlDbType.Int,4),
					new SqlParameter("@DetailTotal", SqlDbType.Decimal,13),
					new SqlParameter("@MinDetailotal", SqlDbType.Decimal,13),
                                        new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.DetailType;
            parameters[1].Value = model.DetailID;
            parameters[2].Value = model.DetailTotal;
            parameters[3].Value = model.MinDetailotal;
            parameters[4].Value = model.ID;

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
        /// 总结计划
        /// </summary>
        /// <param name="OrderNO">计划编号</param>
        /// <param name="strMsg">操作返回的信息</param>
        /// <returns></returns>
        public static bool SummarizeOrder(SellPlanDetailModel model, out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            strMsg = "";
            //判断计划是否为执行状态，非执行状态不能总结
            if (isHandle(model.PlanNo, "2"))
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update officedba.SellPlanDetail set ");
                strSql.Append("SummarizeDate=@SummarizeDate,");
                strSql.Append("SummarizeNote=@SummarizeNote,");
                strSql.Append("Summarizer=@Summarizer,");
                strSql.Append("AimRealResult=@AimRealResult,");
                strSql.Append("AddOrCut=@AddOrCut,");
                strSql.Append("Difference=@Difference,");
                strSql.Append("CompletePercent=@CompletePercent,");
                strSql.Append("IsSummarize=@IsSummarize");
                strSql.Append(" where ID=@ID ");
                #region 参数
                SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					
					new SqlParameter("@SummarizeDate", SqlDbType.DateTime),
					new SqlParameter("@SummarizeNote", SqlDbType.VarChar,1024),
					new SqlParameter("@Summarizer", SqlDbType.Int,4),
					new SqlParameter("@AimRealResult", SqlDbType.VarChar,1024),
					new SqlParameter("@AddOrCut", SqlDbType.Char,1),
					new SqlParameter("@Difference", SqlDbType.VarChar,100),
					new SqlParameter("@CompletePercent", SqlDbType.Decimal,5),
					new SqlParameter("@IsSummarize", SqlDbType.Char,1)};
                parameters[0].Value = model.ID;
              
                parameters[1].Value = model.SummarizeDate;
                parameters[2].Value = model.SummarizeNote;
                parameters[3].Value = model.Summarizer;
                parameters[4].Value = model.AimRealResult;
                parameters[5].Value = model.AddOrCut;
                parameters[6].Value = model.Difference;
                parameters[7].Value = model.CompletePercent;
                parameters[8].Value = model.IsSummarize;

                foreach (SqlParameter para in parameters)
                {
                    if (para.Value == null)
                    {
                        para.Value = DBNull.Value;
                    }
                }
                #endregion
                try
                {

                    SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);

                    strMsg = "总结成功！";
                    isSucc = true;
                }
                catch (Exception ex)
                {

                    strMsg = "总结失败，请联系系统管理员！";
                    throw ex;
                }
            }
            else
            {
                isSucc = false;
                strMsg = "只有执行状态的计划才可以进行总结！";
            }
            return isSucc;
        }


        /// <summary>
        /// 删除子表数据
        /// </summary>
        /// <param name="sellBackModel"></param>
        /// <param name="tran"></param>
        private static void DelOrderDetail(SellPlanDetailModel model, TransactionManager tran)
        {
            string strSql = string.Empty;

            strSql += " DECLARE @Id Int                                                                  ";
            strSql += " SET @Id = " + model.ID.ToString() + "; WITH RootNodeCTE(Id, ParentId) AS                    ";
            strSql += " (SELECT     Id, ParentId                                                         ";
            strSql += " FROM           officedba.SellPlanDetail                                          ";
            strSql += " WHERE      ParentId IN (@Id)                                                     ";
            strSql += " UNION ALL                                                                        ";
            strSql += " SELECT      officedba.SellPlanDetail.Id,  officedba.SellPlanDetail.ParentId      ";
            strSql += " FROM         RootNodeCTE INNER JOIN                                              ";
            strSql += " officedba.SellPlanDetail ON RootNodeCTE.Id =  officedba.SellPlanDetail.ParentId  ";
            strSql += " )                                                                                ";
            strSql += " SELECT     *                                                                     ";
            strSql += " FROM         RootNodeCTE                                                         ";

            DataTable dt = SqlHelper.ExecuteSql(strSql);//当前删除节点的所有子节点
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.SellPlanDetail WHERE ID=" + dt.Rows[i]["ID"].ToString(), null);
                }
            }

            strSql = "DELETE FROM officedba.SellPlanDetail WHERE ID= " + model.ID.ToString();
            SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), null);
        }

        /// <summary>
        /// 获取当前计划的id
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public static int GetOrderID(string orderNo)
        {
            int OrderID = 0;
            string sql = string.Empty;
            string strCompanyCD = string.Empty;//单位编号


            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;


            sql = " select ID from officedba.SellPlan where CompanyCD=@CompanyCD and PlanNo=@PlanNo ";
            SqlParameter[] paras = { new SqlParameter("@CompanyCD", strCompanyCD), new SqlParameter("@PlanNo", orderNo) };
            OrderID = (int)SqlHelper.ExecuteScalar(sql, paras);
            return OrderID;
        }

        /// <summary>
        /// 为主表插入数据
        /// </summary>
        /// <param name="sellPlanModel"></param>
        /// <param name="tran"></param>
        private static void InsertOrder(SellPlanModel model, TransactionManager tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.SellPlan(");
            strSql.Append("CompanyCD,PlanNo,Title,PlanType,PlanYear,PlanTime,PlanTotal,StartDate,EndDate,MinPlanTotal,Hortation,CanViewUser,CanViewUserName,Remark,BillStatus,Creator,CreateDate,ModifiedDate,ModifiedUserID)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@PlanNo,@Title,@PlanType,@PlanYear,@PlanTime,@PlanTotal,@StartDate,@EndDate,@MinPlanTotal,@Hortation,@CanViewUser,@CanViewUserName,@Remark,@BillStatus,@Creator,@CreateDate,@ModifiedDate,@ModifiedUserID)");

            #region 参数
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@PlanNo", SqlDbType.VarChar,50),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@PlanType", SqlDbType.Char,1),
					new SqlParameter("@PlanYear", SqlDbType.VarChar,10),
					new SqlParameter("@PlanTime", SqlDbType.VarChar,2),
					new SqlParameter("@PlanTotal", SqlDbType.Decimal,13),
					new SqlParameter("@StartDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@MinPlanTotal", SqlDbType.Decimal,13),
					new SqlParameter("@Hortation", SqlDbType.VarChar,1024),
					new SqlParameter("@CanViewUser", SqlDbType.VarChar,2048),
					new SqlParameter("@CanViewUserName", SqlDbType.VarChar,2048),
					new SqlParameter("@Remark", SqlDbType.VarChar,1024),
					new SqlParameter("@BillStatus", SqlDbType.Char,1),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50)};
            parameters[0].Value = model.CompanyCD;
            parameters[1].Value = model.PlanNo;
            parameters[2].Value = model.Title;
            parameters[3].Value = model.PlanType;
            parameters[4].Value = model.PlanYear;
            parameters[5].Value = model.PlanTime;
            parameters[6].Value = model.PlanTotal;
            parameters[7].Value = model.StartDate;
            parameters[8].Value = model.EndDate;
            parameters[9].Value = model.MinPlanTotal;
            parameters[10].Value = model.Hortation;
            parameters[11].Value = model.CanViewUser;
            parameters[12].Value = model.CanViewUserName;
            parameters[13].Value = model.Remark;
            parameters[14].Value = "1";
            parameters[15].Value = model.Creator;
            parameters[16].Value = model.CreateDate;
            parameters[17].Value = model.ModifiedDate;
            parameters[18].Value = model.ModifiedUserID;
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
        /// 为明细表插入数据
        /// </summary>
        /// <param name="sellSendDetailModellList"></param>
        /// <param name="tran"></param>
        private static void InsertOrderDetail(SellPlanDetailModel model, TransactionManager tran)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.SellPlanDetail(");
            strSql.Append("CompanyCD,PlanNo,DetailType,ParentID,DetailID,DetailTotal,MinDetailotal,IsSummarize)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@PlanNo,@DetailType,@ParentID,@DetailID,@DetailTotal,@MinDetailotal,'0')");
            #region 参数
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@PlanNo", SqlDbType.VarChar,50),
					new SqlParameter("@DetailType", SqlDbType.Char,1),
					new SqlParameter("@ParentID", SqlDbType.Int,4),
					new SqlParameter("@DetailID", SqlDbType.Int,4),
					new SqlParameter("@DetailTotal", SqlDbType.Decimal,13),
					new SqlParameter("@MinDetailotal", SqlDbType.Decimal,13)};
            parameters[0].Value = model.CompanyCD;
            parameters[1].Value = model.PlanNo;
            parameters[2].Value = model.DetailType;
            parameters[3].Value = model.ParentID;
            parameters[4].Value = model.DetailID;
            parameters[5].Value = model.DetailTotal;
            parameters[6].Value = model.MinDetailotal;
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
        /// 删除销售计划
        /// </summary>
        /// <param name="orderNos"></param>
        /// <returns></returns>
        public static bool DelOrder(string orderNos, out string strMsg, out string strFieldText)
        {
            string strCompanyCD = string.Empty;//单位编号
            bool isSucc = false;
            string allOrderNo = "";
            strMsg = "";
            strFieldText = "";
            bool bTemp = false;//计划是否可以被删除
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            TransactionManager tran = new TransactionManager();

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string[] orderNoS = null;
            orderNoS = orderNos.Split(',');

            for (int i = 0; i < orderNoS.Length; i++)
            {
                if (!IsFlow(orderNoS[i]))
                {
                    strFieldText += "计划：" + orderNoS[i] + "|";
                    strMsg += "已提交审批或已确认后的计划不允许删除！|";
                    bTemp = true;
                }
                else if (!isHandle(orderNoS[i], "1"))
                {
                    strFieldText += "计划：" + orderNoS[i] + "|";
                    strMsg += "已提交审批或已确认后的计划不允许删除！|";
                    bTemp = true;
                }
                orderNoS[i] = "'" + orderNoS[i] + "'";
                sb.Append(orderNoS[i]);
            }

            allOrderNo = sb.ToString().Replace("''", "','");
            if (!bTemp)
            {
                tran.BeginTransaction();
                try
                {
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.SellPlan WHERE PlanNo IN ( " + allOrderNo + " ) and CompanyCD='" + strCompanyCD + "'", null);
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.SellPlanDetail WHERE PlanNo IN ( " + allOrderNo + " ) and CompanyCD='" + strCompanyCD + "'", null);

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

        #region 获取计划信息相关操作
        /// <summary>
        /// 获取计划列表 
        /// </summary>
        /// <param name="sellContractModel">sellContractModel表实体</param>
        /// <param name="FlowStatus">审批状态</param>
        /// <returns></returns>
        public static DataTable GetOrderList(string EFIndex,string EFDesc,SellPlanModel sellPlanModel, int? FlowStatus, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            StringBuilder strSql = new StringBuilder();

            int eid = 0;

          
            eid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
            strSql.Append("select * from (  SELECT so.ID,so.PlanNo, so.Title, so.PlanTotal,case so.PlanType ");
            strSql.Append(" when '1' then '月'  when '3' then '年'  when '2' then '季'       ");
            strSql.Append(" when '4' then '日'  when '5' then '周'  when '6' then '半年'       ");
            strSql.Append(" when '7' then '其他'   end as PlanTypeText,      ");
            strSql.Append(" so.MinPlanTotal,  so.ModifiedDate,                                                               ");
            strSql.Append(" (CASE so.PlanType WHEN '1' THEN PlanYear + '年  ' + so.PlanTime + '月' WHEN '2'  ");
            strSql.Append(" THEN PlanYear + '年  ' + (CASE so.PlanTime WHEN '1' THEN '第一季度' WHEN '2' THEN ");
            strSql.Append(" '第二季度' WHEN '3' THEN '第三季度' WHEN '4' THEN '第四季度' END) WHEN '3' THEN PlanYear + '年  '   ");
            strSql.Append("WHEN '4'  THEN PlanYear WHEN '5'  THEN PlanYear +'年  ' +'第'+PlanTime+'周'  WHEN '6'  ");
            strSql.Append(" THEN PlanYear + '年  ' + (CASE so.PlanTime WHEN '1' THEN '上半年' WHEN '2' THEN  ");
            strSql.Append(" '下半年' end) WHEN '7' THEN PlanYear + '年  ' END) AS PlanDate,  ");
            strSql.Append(" (SELECT TOP 1 FlowStatus                                                                         ");
            strSql.Append(" FROM officedba.FlowInstance                                                                      ");
            strSql.Append(" WHERE BillID = so.ID AND BillTypeFlag = 5 AND BillTypeCode = 8                                   ");
            strSql.Append(" ORDER BY ModifiedDate DESC) AS FlowStatus,                                                       ");
            strSql.Append(" CASE so.BillStatus WHEN 1 THEN '制单' WHEN 2 THEN '执行' WHEN 4                                  ");
            strSql.Append(" THEN '手工结单' WHEN 5 THEN '自动结单'  END AS BillStatusText,                                   ");
            strSql.Append(" CASE WHEN (SELECT TOP 1 FlowStatus                                                               ");
            strSql.Append(" FROM officedba.FlowInstance                                                                      ");
            strSql.Append(" WHERE BillID = so.ID AND BillTypeFlag = 5 AND BillTypeCode = 8                                   ");
            strSql.Append(" ORDER BY ModifiedDate DESC) IS NULL THEN '' WHEN                                                 ");
            strSql.Append(" (SELECT TOP 1 FlowStatus                                                                         ");
            strSql.Append(" FROM officedba.FlowInstance                                                                      ");
            strSql.Append(" WHERE BillID = so.ID AND BillTypeFlag = 5 AND BillTypeCode = 8                                   ");
            strSql.Append(" ORDER BY ModifiedDate DESC) = 1 THEN '待审批' WHEN                                               ");
            strSql.Append(" (SELECT TOP 1 FlowStatus                                                                         ");
            strSql.Append(" FROM officedba.FlowInstance                                                                      ");
            strSql.Append(" WHERE BillID = so.ID AND BillTypeFlag = 5 AND BillTypeCode = 8                                   ");
            strSql.Append(" ORDER BY ModifiedDate DESC) = 2 THEN '审批中' WHEN                                               ");
            strSql.Append(" (SELECT TOP 1 FlowStatus                                                                         ");
            strSql.Append(" FROM officedba.FlowInstance                                                                      ");
            strSql.Append(" WHERE BillID = so.ID AND BillTypeFlag = 5 AND BillTypeCode = 8                                   ");
            strSql.Append(" ORDER BY ModifiedDate DESC) = 3 THEN '审批通过' WHEN                                             ");
            strSql.Append(" (SELECT TOP 1 FlowStatus                                                                         ");
            strSql.Append(" FROM officedba.FlowInstance                                                                      ");
            strSql.Append(" WHERE BillID = so.ID AND BillTypeFlag = 5 AND BillTypeCode = 8                                   ");
            strSql.Append(" ORDER BY ModifiedDate DESC) = 4 THEN '审批不通过' WHEN                                           ");
            strSql.Append(" (SELECT TOP 1 FlowStatus                                                                         ");
            strSql.Append(" FROM officedba.FlowInstance                                                                      ");
            strSql.Append(" WHERE BillID = so.ID AND BillTypeFlag = 5 AND BillTypeCode = 8                                   ");
            strSql.Append(" ORDER BY ModifiedDate DESC) = 5 THEN '撤销审批' END AS FlowInstanceText                          ");
            strSql.Append(" FROM officedba.SellPlan as so where 1=1     and so.CompanyCD=@CompanyCD                          ");
            strSql.Append( " AND (CHARINDEX('," + eid + ",',','+so.CanViewUser+',')>0  OR so.CanViewUser='' or so.CanViewUser is null OR  so.Creator=" + eid + ") ");
            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            if (sellPlanModel.BillStatus != null)
            {
                strSql.Append(" and so.BillStatus= @BillStatus");

                arr.Add(new SqlParameter("@BillStatus", sellPlanModel.BillStatus));
            }

            if (sellPlanModel.PlanType != null)
            {
                strSql.Append(" and so.PlanType=@PlanType");
                arr.Add(new SqlParameter("@PlanType", sellPlanModel.PlanType));
            }

            if (sellPlanModel.PlanNo != null)
            {
                strSql.Append(" and so.PlanNo like @PlanNo");
                arr.Add(new SqlParameter("@PlanNo", "%" + sellPlanModel.PlanNo + "%"));
            }

            if (sellPlanModel.Title != null)
            {
                strSql.Append(" and so.Title like @Title");
                arr.Add(new SqlParameter("@Title", "%" + sellPlanModel.Title + "%"));
            }

            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                strSql.AppendLine(" and so.ExtField" + EFIndex + " LIKE @EFDesc");
                arr.Add(new SqlParameter("@EFDesc", "%" + EFDesc + "%"));
                //comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + EFDesc + "%"));
            }

            strSql.Append(" ) as f  where 1=1 ");
            if (FlowStatus != null)
            {
                if (FlowStatus != 0)
                {
                    strSql.Append(" and f.FlowStatus=@FlowStatus");
                    arr.Add(new SqlParameter("@FlowStatus", FlowStatus));
                }
                else
                {
                    strSql.Append(" and f.FlowStatus is null ");
                }

            }

            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
        }

        /// <summary>
        /// 获取计划明细信息
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public static DataTable GetOrderDetail(string orderNo)
        {
            string strCompanyCD = string.Empty;//单位编号
            string strSql = string.Empty;

            strSql += " SELECT s.ID, s.DetailType, CASE s.DetailType WHEN '1' THEN '部门' WHEN '2' THEN '员工' WHEN '3'   ";
            strSql += " THEN '物品' END AS DetailTypeName, s.ParentID, s.DetailID, s.DetailTotal, s.MinDetailotal,        ";
            strSql += " s.IsSummarize,CONVERT(varchar(100), s.SummarizeDate, 23) AS SummarizeDate ,s.SummarizeNote ,       ";
            strSql += " s.AimRealResult,s.AddOrCut,[Difference] ,s.CompletePercent,e1.EmployeeName as SummarizerName ,       ";
            strSql += " case s.AddOrCut when '0' then '完成情况：低于计划值'  when '1' then '完成情况：等于计划值'  when '2' then '完成情况：大于计划值' end as AddOrCutText,      ";
            strSql += " case s.AddOrCut when '0' then '低于计划值'  when '1' then '等于计划值'  when '2' then '大于计划值' end as AddOrCutText2,      ";
          
            strSql += " CASE s.DetailType WHEN '1' THEN d .DeptName WHEN '2' THEN e.EmployeeName WHEN '3' THEN            ";
            strSql += " p.ProductName END AS DetailName FROM officedba.SellPlanDetail AS s LEFT OUTER JOIN                ";
            strSql += " officedba.DeptInfo AS d ON s.DetailID = d.ID AND s.DetailType = '1' LEFT OUTER JOIN               ";
            strSql += " officedba.EmployeeInfo AS e ON s.DetailID = e.ID AND s.DetailType = '2' LEFT OUTER JOIN           ";
            strSql += " officedba.EmployeeInfo AS e1 ON s.Summarizer = e1.ID  LEFT OUTER JOIN           ";
            strSql += " officedba.ProductInfo AS p ON s.DetailID = p.ID AND s.DetailType = '3'                            ";
            strSql += " where s.PlanNo=@PlanNo and s.CompanyCD=@CompanyCD           ORDER BY ID asc                       ";

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            SqlParameter[] p = { new SqlParameter("@PlanNo", orderNo), new SqlParameter("@CompanyCD", strCompanyCD) };
            return SqlHelper.ExecuteSql(strSql, p);
        }

        /// <summary>
        /// 获取计划主表信息
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public static DataTable GetOrderInfo(int orderID)
        {
            string strSql = string.Empty;
            strSql += " SELECT s.ID, s.PlanNo, s.Title, s.PlanType, s.PlanYear, s.PlanTime, s.PlanTotal, s.MinPlanTotal,CONVERT(varchar(100), s.StartDate, 23) AS StartDate,CONVERT(varchar(100), s.EndDate, 23) AS EndDate ,s.Hortation,s.CanViewUser, ";
            strSql += " s.Remark, s.BillStatus, CASE s.BillStatus WHEN '1' THEN '制单' WHEN '2' THEN '执行' WHEN '4'     ";
            strSql += " THEN '手工结单' END AS BillStatusText, s.Creator,CONVERT(varchar(100), s.CreateDate, 23) AS CreateDate , s.Confirmor, CONVERT(varchar(100), s.ConfirmDate, 23) AS ConfirmDate,s.CanViewUserName,      ";
            strSql += " s.Closer,CONVERT(varchar(100), s.CloseDate, 23) AS CloseDate ,CONVERT(varchar(100), s.ModifiedDate, 23) AS ModifiedDate, s.ModifiedUserID, e.EmployeeName AS CreatorName,          ";
            strSql += " e1.EmployeeName AS ConfirmorName, e2.EmployeeName AS CloserName,                                  ";
            strSql += " s.ExtField1,s.ExtField2,s.ExtField3,s.ExtField4,s.ExtField5,s.ExtField6,s.ExtField7,s.ExtField8,s.ExtField9,s.ExtField10    ";
            strSql += " FROM officedba.SellPlan AS s LEFT OUTER JOIN                                                     ";
            strSql += " officedba.EmployeeInfo AS e ON s.Creator = e.ID LEFT OUTER JOIN                                  ";
            strSql += " officedba.EmployeeInfo AS e1 ON s.Confirmor = e1.ID LEFT OUTER JOIN                              ";
            strSql += " officedba.EmployeeInfo AS e2 ON s.Closer = e2.ID                                                 ";
            strSql += " where s.ID=@ID";
            SqlParameter[] p ={
            new  SqlParameter("@ID",orderID)
            };

            return SqlHelper.ExecuteSql(strSql, p);
        }

        #endregion

        #region 确认、结单、取消确认、取消结单操作

        /// <summary>
        /// 确认计划
        /// </summary>
        /// <param name="OrderNO">计划编号</param>
        /// <param name="strMsg">操作返回的信息</param>
        /// <returns></returns>
        public static bool ConfirmOrder(string OrderNO, out string strMsg, out string strFieldText)
        {
            string strSq = string.Empty;
            bool isSuc = false;
            strMsg = "";
            strFieldText = "";
            //判断计划是够为制单状态，非制单状态不能确认
            if (isHandle(OrderNO, "1"))
            {

                try
                {
                    SqlParameter[] paras = new SqlParameter[4];
                    strSq = "update  officedba.SellPlan set BillStatus='2'  ";

                    strSq += " , Confirmor=@EmployeeID, ConfirmDate=getdate(), ModifiedDate=getdate() ,ModifiedUserID=@UserID ";
                    paras[0] = new SqlParameter("@EmployeeID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID);
                    paras[1] = new SqlParameter("@UserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID);
                    paras[2] = new SqlParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                    paras[3] = new SqlParameter("@PlanNo", OrderNO);
                    strSq += " WHERE PlanNo = @PlanNo and CompanyCD=@CompanyCD";
                    SqlHelper.ExecuteTransSql(strSq, paras);

                    isSuc = true;
                    strMsg = "确认成功！";
                }
                catch (Exception ex)
                {
                    isSuc = false;
                    strMsg = "确认失败，请联系系统管理员！";
                    throw ex;
                }

            }
            else
            {
                isSuc = false;
                strMsg = "该计划已被其他用户确认，不可再次确认！";
            }
            return isSuc;
        }

        /// <summary>
        /// 结单
        /// </summary>
        /// <param name="OrderNO">计划编号</param>
        /// <param name="strMsg">操作返回的信息</param>
        /// <returns></returns>
        public static bool CloseOrder(string OrderNO, out string strMsg)
        {
            string strSq = string.Empty;
            bool isSuc = false;
            strMsg = "";
            //判断计划是否为执行状态，非执行状态不能结单
            if (isHandle(OrderNO, "2"))
            {

                try
                {
                    SqlParameter[] paras = new SqlParameter[4];
                    strSq = "update  officedba.SellPlan set BillStatus='4'  ";

                    strSq += " , Closer=@EmployeeID, CloseDate=getdate(), ModifiedDate=getdate() ,ModifiedUserID=@UserID ";

                    paras[0] = new SqlParameter("@EmployeeID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID);
                    paras[1] = new SqlParameter("@UserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID);
                    paras[2] = new SqlParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                    paras[3] = new SqlParameter("@PlanNo", OrderNO);

                    strSq += " WHERE PlanNo = @PlanNo and CompanyCD=@CompanyCD";

                    SqlHelper.ExecuteTransSql(strSq, paras);
                    isSuc = true;
                    strMsg = "结单成功！";
                }
                catch (Exception ex)
                {


                    isSuc = false;
                    strMsg = "结单失败，请联系系统管理员！";
                    throw ex;
                }
            }
            else
            {
                isSuc = false;
                strMsg = "该计划已被其他用户结单，不可再次结单！";
            }
            return isSuc;
        }

        /// <summary>
        /// 取消结单
        /// </summary>
        /// <param name="OrderNO">计划编号</param>
        /// <param name="strMsg">操作返回的信息</param>
        /// <returns></returns>
        public static bool UnCloseOrder(string OrderNO, out string strMsg)
        {
            string strSq = string.Empty;
            bool isSuc = false;
            strMsg = "";
            //判断计划是否为手工结单状态，非手工结单状态不能结单
            if (isHandle(OrderNO, "4"))
            {

                try
                {
                    SqlParameter[] paras = new SqlParameter[5];
                    strSq = "update  officedba.SellPlan set BillStatus='2'  ";

                    strSq += " , Closer=@EmployeeID, CloseDate=@CloseDate, ModifiedDate=getdate() ,ModifiedUserID=@UserID ";

                    paras[0] = new SqlParameter("@EmployeeID", DBNull.Value);
                    paras[1] = new SqlParameter("@UserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID);
                    paras[2] = new SqlParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                    paras[3] = new SqlParameter("@PlanNo", OrderNO);
                    paras[4] = new SqlParameter("@CloseDate", DBNull.Value);

                    strSq += " WHERE PlanNo = @PlanNo and CompanyCD=@CompanyCD";

                    SqlHelper.ExecuteTransSql(strSq, paras);

                    isSuc = true;
                    strMsg = "取消结单成功！";
                }
                catch (Exception ex)
                {

                    isSuc = false;
                    strMsg = "取消结单失败，请联系系统管理员！";
                    throw ex;
                }

            }
            else
            {
                isSuc = false;
                strMsg = "该计划已被其他用户取消结单，不可再次取消结单！";
            }
            return isSuc;
        }

        /// <summary>
        /// 取消确认计划
        /// </summary>
        /// <param name="OrderNO">计划编号</param>
        /// <param name="strMsg">操作返回的信息</param>
        /// <returns></returns>
        public static bool UnConfirmOrder(string OrderNO, out string strMsg)
        {
            string strSq = string.Empty;
            bool isSuc = false;
            strMsg = "";
            int OrderId = GetOrderID(OrderNO);


            //判断计划是否为执行状态，非执行状态不能取消确认
            if (isHandle(OrderNO, "2"))
            {
                //判断计划明细是否总结
                if (IsSummarize(OrderNO))
                {

                    int iEmployeeID = 0;//员工id
                    string strUserID = string.Empty;//用户id
                    string strCompanyCD = string.Empty;//单位编码
                    SqlParameter[] paras = new SqlParameter[5];

                    iEmployeeID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
                    strUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                    strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

                    strSq = "update  officedba.SellPlan set BillStatus='1'   ";
                    strSq += " , Confirmor=@Confirmor, ConfirmDate=@ConfirmDate, ModifiedDate=getdate() ,ModifiedUserID=@UserID ";
                    strSq += " WHERE PlanNo = @PlanNo and CompanyCD=@CompanyCD";

                    paras[0] = new SqlParameter("@Confirmor", DBNull.Value);
                    paras[1] = new SqlParameter("@UserID", strUserID);
                    paras[2] = new SqlParameter("@CompanyCD", strCompanyCD);
                    paras[3] = new SqlParameter("@PlanNo", OrderNO);
                    paras[4] = new SqlParameter("@ConfirmDate", DBNull.Value);

                    TransactionManager tran = new TransactionManager();
                    tran.BeginTransaction();
                    try
                    {

                        FlowDBHelper.OperateCancelConfirm(strCompanyCD, 5, 8, OrderId, strUserID, tran);//撤销审批
                        SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSq, paras);

                        tran.Commit();
                        isSuc = true;
                        strMsg = "取消确认成功！";
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();

                        isSuc = false;
                        strMsg = "取消确认失败，请联系系统管理员！";
                        throw ex;
                    }
                }
                else
                {
                    isSuc = false;
                    strMsg = "该计划已有明细总结，不可取消确认！";
                }

            }
            else
            {
                isSuc = false;
                strMsg = "该计划已被其他用户取消确认，不可再次取消确认！";
            }
            return isSuc;
        }

        #endregion

        #region 确认相关操作是否可以进行

        /// <summary>
        /// 根据计划状态判断该计划是否可以执行该操作
        /// </summary>
        /// <param name="OrderNO">计划编号</param>
        /// <param name="OrderStatus">计划状态</param>
        /// <returns>返回true时表示可以执行操作</returns>
        private static bool isHandle(string OrderNO, string OrderStatus)
        {
            bool isSuc = false;
            string strCompanyCD = string.Empty;//单位编号
            int iCount = 0;

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string strSql = string.Empty;

            strSql = "select count(1) from officedba.SellPlan where PlanNo = @PlanNo and CompanyCD=@CompanyCD and BillStatus=@BillStatus ";
            ArrayList arr = new ArrayList();
            SqlParameter[] paras = new SqlParameter[3];
            paras[0] = new SqlParameter("@PlanNo", OrderNO);
            paras[1] = new SqlParameter("@CompanyCD", strCompanyCD);
            paras[2] = new SqlParameter("@BillStatus", OrderStatus);

            iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, paras));
            if (iCount != 0)
            {
                isSuc = true;
            }

            return isSuc;

        }

        /// <summary>
        /// 获取计划明细是否以总结，总结后不可取消确认
        /// </summary>
        /// <param name="OrderNO">计划编号</param>
        /// <returns>返回true时表示可以取消确认</returns>
        private static bool IsSummarize(string OrderNO)
        {
            bool isSuc = false;
            string strCompanyCD = string.Empty;//单位编号
            int iCount = 0;

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string strSql = string.Empty;

            strSql = "select isnull(count(1),0) from officedba.SellPlanDetail where PlanNo = @PlanNo and CompanyCD=@CompanyCD and IsSummarize='1' ";
            ArrayList arr = new ArrayList();
            SqlParameter[] paras = new SqlParameter[2];
            paras[0] = new SqlParameter("@PlanNo", OrderNO);
            paras[1] = new SqlParameter("@CompanyCD", strCompanyCD);

            iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, paras));
            if (iCount == 0)
            {
                isSuc = true;
            }

            return isSuc;

        }


        /// <summary>
        /// 判断计划编号是否存在
        /// </summary>
        /// <param name="OrderNO">计划编号</param>
        /// <returns>返回true时表示不存在</returns>
        private static bool NoIsExist(string OrderNO)
        {
            bool isSuc = false;
            string strCompanyCD = string.Empty;//单位编号
            int iCount = 0;

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string strSql = string.Empty;
            SqlParameter[] paras = { 
                                       new SqlParameter("@PlanNo", OrderNO),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += " select count(1) from officedba.SellPlan ";
            strSql += " WHERE  (PlanNo = @PlanNo) AND (CompanyCD = @CompanyCD) ";
            iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, paras));
            if (iCount == 0)
            {
                isSuc = true;
            }

            return isSuc;
        }

        /// <summary>
        /// 计划是否提交审批
        /// </summary>
        /// <param name="OrderNO"></param>
        /// <returns>返回true时表示未提交</returns>
        private static bool IsFlow(string OrderNO)
        {
            bool isSuc = false;
            string strCompanyCD = string.Empty;//单位编号
            int iCount = 0;

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string strSql = string.Empty;
            SqlParameter[] paras = { 
                                       new SqlParameter("@PlanNo", OrderNO),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += " SELECT COUNT(1) FROM officedba.FlowInstance AS f LEFT OUTER JOIN officedba.SellPlan AS s ON f.BillID = s.ID AND f.BillTypeFlag = 5 AND f.BillTypeCode = 8 and f.CompanyCD=s.CompanyCD";
            strSql += "  WHERE (s.PlanNo = @PlanNo) AND (s.CompanyCD = @CompanyCD)  ";
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

        /// <summary>
        /// 根据计划状态判断计划是否可以被修改
        /// </summary>
        /// <param name="OrderNO"></param>
        /// <returns>返回true时表示可以修改</returns>
        private static bool IsUpdate(string OrderNO)
        {
            bool isSuc = false;
            string strCompanyCD = string.Empty;//单位编号
            string strStatus = string.Empty;

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string strSql = string.Empty;
            SqlParameter[] paras = { 
                                       new SqlParameter("@PlanNo", OrderNO),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += " select TOP  1  FlowStatus  from officedba.FlowInstance ";
            strSql += " WHERE (BillID = (SELECT ID FROM officedba.SellPlan WHERE (PlanNo = @PlanNo) AND (CompanyCD = @CompanyCD))) AND BillTypeFlag = 5 AND BillTypeCode = 8  ";
            strSql += " ORDER BY ModifiedDate DESC ";
            object obj = SqlHelper.ExecuteScalar(strSql, paras);
            if (obj != null)
            {
                strStatus = obj.ToString();
                switch (strStatus)
                {
                    case "4":
                        isSuc = true;
                        break;
                    case "5":
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

        /// <summary>
        /// 打印主表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrder(string OrderNo)
        {
            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string strSql = string.Empty;
            strSql += " SELECT s.ID, s.PlanNo, s.Title, s.PlanTotal, s.MinPlanTotal,case s.PlanType  ";
            strSql += " when '1' then '月'  when '3' then '年'  when '2' then '季'       ";
            strSql += " when '4' then '日'  when '5' then '周'  when '6' then '半年'       ";
            strSql += " when '7' then '其他'   end as PlanType, s.CanViewUserName,      ";

            strSql += " s.MinPlanTotal,  CONVERT(varchar(100), s.StartDate, 23) AS StartDate,CONVERT(varchar(100), s.EndDate, 23) AS EndDate ,s.Hortation,s.CanViewUser, ";
            strSql += " (CASE s.PlanType WHEN '1' THEN PlanYear + '年  ' + s.PlanTime + '月' WHEN '2'  ";
            strSql += " THEN PlanYear + '年  ' + (CASE s.PlanTime WHEN '1' THEN '第一季度' WHEN '2' THEN ";
            strSql += " '第二季度' WHEN '3' THEN '第三季度' WHEN '4' THEN '第四季度' END) WHEN '3' THEN PlanYear + '年  '   ";
            strSql += " WHEN '4'  THEN PlanYear WHEN '5'  THEN PlanYear +'年  ' +'第'+PlanTime+'周'  WHEN '6'  ";
            strSql += " THEN PlanYear + '年  ' + (CASE s.PlanTime WHEN '1' THEN '上半年' WHEN '2' THEN  ";
            strSql += " '下半年' end) WHEN '7' THEN PlanYear + '年  ' END) AS PlanTime,  ";
            strSql += " s.Remark,  CASE s.BillStatus WHEN '1' THEN '制单' WHEN '2' THEN '执行' WHEN '4'     ";
            strSql += " THEN '手工结单' END AS BillStatus,  Convert(varchar(20),s.CreateDate,23)CreateDate,  Convert(varchar(20),s.ConfirmDate,23)ConfirmDate,      ";
            strSql += " Convert(varchar(20),s.CloseDate,23)CloseDate, Convert(varchar(20),s.ModifiedDate,23)ModifiedDate, s.ModifiedUserID, e.EmployeeName AS Creator,          ";
            strSql += " e1.EmployeeName AS Confirmor, e2.EmployeeName AS Closer,                                  ";
            strSql += " s.ExtField1,s.ExtField2,s.ExtField3,s.ExtField4,s.ExtField5,s.ExtField6,s.ExtField7,s.ExtField8,s.ExtField9,s.ExtField10 ";
            strSql += " FROM officedba.SellPlan AS s LEFT OUTER JOIN                                                     ";
            strSql += " officedba.EmployeeInfo AS e ON s.Creator = e.ID LEFT OUTER JOIN                                  ";
            strSql += " officedba.EmployeeInfo AS e1 ON s.Confirmor = e1.ID LEFT OUTER JOIN                              ";
            strSql += " officedba.EmployeeInfo AS e2 ON s.Closer = e2.ID                                                 ";
            strSql += " where s.PlanNo=@PlanNo and s.CompanyCD=@CompanyCD  ";

            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("@PlanNo",OrderNo)
                                   };

            DataTable dt = SqlHelper.ExecuteSql(strSql, param);
            return dt;
        }

        #region 扩展属性保存操作
        /// <summary>
        /// 扩展属性保存操作
        /// </summary>
        /// <returns></returns>
        private static void GetExtAttrCmd(SellPlanModel model, Hashtable htExtAttr, TransactionManager tran)
        {
            try
            {
                string strSql = string.Empty;
                strSql = "UPDATE officedba.SellPlan set ";

                SqlParameter[] parameters = new SqlParameter[htExtAttr.Count+2];
                int i = 0;
                
                foreach (DictionaryEntry de in htExtAttr)// de为一个Hashtable实例
                {
                    strSql += de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",";                   
                    parameters[i] = SqlHelper.GetParameter("@" + de.Key.ToString().Trim(),de.Value.ToString().Trim());
                    i++;                   
                }
                int iLength = strSql.Length - 1;
                strSql = strSql.Substring(0, iLength);
                strSql += " where CompanyCD = @CompanyCD  AND PlanNo = @PlanNo";
                parameters[i] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
                parameters[i + 1] = SqlHelper.GetParameter("@PlanNo", model.PlanNo);
                //cmd.Parameters.AddWithValue("@CompanyCD", model.CompanyCD);
                //cmd.Parameters.AddWithValue("@PlanNo", model.PlanNo);
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

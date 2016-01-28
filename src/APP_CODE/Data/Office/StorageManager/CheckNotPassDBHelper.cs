using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using XBase.Common;
using XBase.Data.DBHelper;
using XBase.Model.Office.StorageManager;
namespace XBase.Data.Office.StorageManager
{
    public class CheckNotPassDBHelper
    {
        /// <summary>
        /// 获取不合格原因
        /// </summary>
        /// <returns></returns>
        public static DataTable GetReason(string CompanyCD)
        {
            string sql = "select CodeName,ID from officedba.CodeReasonType where UsedStatus='1' and Flag=6 and CompanyCD='" + CompanyCD + "'";
            return SqlHelper.ExecuteSql(sql);
        }

        public static DataTable GetReportInfo(string CompanyCD, string method, string NoPassStr)
        {
            SqlCommand comm = new SqlCommand();
            string[] testStr = NoPassStr.Split('?');
            string BillNo = testStr[0];
            string BillTitle = testStr[1];
            string sql = "";
            sql += " select isnull(q.ProductID,'') as ProductID,q.ModifiedDate,isnull(q.NoPass,'0') as NoPass,q.ID,isnull(q.Title,'') as Title,isnull(p.Specification,'') as Specification,         ";
            sql += "        isnull(q.ReportNo,'') as ReportNo,isnull(p.ProductName,'') as ProductName,isnull(p.ProdNo,'') as ProdNo,isnull(p.UnitID,'') as UnitID,               ";
            sql += " isnull((select CodeName from officedba.CodeUnitType where officedba.CodeUnitType.ID=p.UnitID),'') as UnitName,p.Specification, ";
            sql += " isnull(e.EmployeeName,'') as EmployeeName,isnull(d.DeptName,'') as DeptName ";
            sql += "  ,isnull((select sum(n.NotPassNum)from officedba.CheckNotPass as m left join officedba.CheckNotPassDetail as n on m.ProcessNo=n.ProcessNo and m.ReportID=q.ID where m.BillStatus='2' and m.CompanyCD=@CompanyCD ),0)  as  NotPassNum";
            sql += " from  officedba.QualityCheckReport as q left join officedba.ProductInfo as p ";
            sql += "       on q.ProductID=p.ID  left join officedba.EmployeeInfo as e on q.Checker=e.ID ";
            sql += "       left join officedba.DeptInfo as d on q.CheckDeptId=d.ID ";
            sql += " where   q.CompanyCD=@CompanyCD  and q.BillStatus='2' and p.CompanyCD=@CompanyCD";
            if (BillNo != "")
            {
                sql += " and q.ReportNo like @ReportNo";
                comm.Parameters.Add(SqlHelper.GetParameter("@ReportNo", "%" + BillNo + "%"));
            }
            if (BillTitle != "")
            {
                sql += " and q.Title like @Title";
                comm.Parameters.Add(SqlHelper.GetParameter("@Title", "%" + BillTitle + "%"));
            }
            if (method != "1")
            {
                sql += "  and q.NoPass!=0 and q.NoPass>isnull((select sum(n.NotPassNum) from officedba.CheckNotPass as m  left join officedba.CheckNotPassDetail as n on m.ProcessNo=n.ProcessNo where m.ReportID=q.ID  and m.BillStatus='2'),0)";
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            comm.CommandText = sql;
            return SqlHelper.ExecuteSearch(comm);

        }

        /// <summary>
        /// 添加不合格品处置单 
        /// </summary>
        /// <param name="model">不合格品处置单 </param>
        /// <param name="detail">明细</param>
        /// <param name="htExtAttr">扩展属性</param>
        /// <returns></returns>
        public static bool AddNoPass(CheckNotPassModel model, List<CheckNotPassDetailModel> detail, Hashtable htExtAttr)
        {
            ArrayList sqllist = new ArrayList();
            string sql = "";
            #region 主表---Start
            sql += "INSERT INTO officedba.CheckNotPass  ";
            sql += "           ([CompanyCD]                         ";
            sql += "           ,[ProcessNo]                         ";
            sql += "           ,[FromType]                          ";
            sql += "           ,[ReportID]                          ";
            sql += "           ,[Executor]                          ";
            sql += "           ,[ProcessDate]                       ";
            if (!string.IsNullOrEmpty(model.Remark))
            {
                sql += "           ,[Remark]                        ";
            }
            sql += "           ,[Title]                             ";
            sql += "           ,[CreateDate]                        ";
            sql += "           ,[BillStatus]                        ";
            sql += "           ,[ModifiedDate]                      ";
            sql += "           ,[ModifiedUserID]                    ";

            if (!string.IsNullOrEmpty(model.Attachment))
            {
                sql += "           ,[Attachment]                       ";
            }
            sql += "           ,[Creator])                           ";
            sql += "     VALUES                                     ";
            sql += "           (@CompanyCD                          ";
            sql += "           ,@ProcessNo                          ";
            sql += "           ,@FromType                           ";
            sql += "           ,@ReportID                           ";
            sql += "           ,@Executor                           ";
            sql += "           ,@ProcessDate                        ";
            if (!string.IsNullOrEmpty(model.Remark))
            {
                sql += "           ,@Remark                         ";
            }
            sql += "           ,@Title                              ";
            sql += "           ,@CreateDate                         ";
            sql += "           ,@BillStatus                         ";
            sql += "           ,@ModifiedDate                       ";
            sql += "           ,@ModifiedUserID                     ";

            if (!string.IsNullOrEmpty(model.Attachment))
            {
                sql += "           ,@Attachment                        ";
            }
            sql += "           ,@Creator)                            ";
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql;
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProcessNo", model.ProcessNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
            comm.Parameters.Add(SqlHelper.GetParameter("@ReportID", model.ReportID));
            comm.Parameters.Add(SqlHelper.GetParameter("@Executor", model.Executor));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProcessDate", model.ProcessDate));
            if (!string.IsNullOrEmpty(model.Remark))
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@Title", model.Title));
            comm.Parameters.Add(SqlHelper.GetParameter("@CreateDate", model.CreateDate));
            comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", model.BillStatus));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", model.ModifiedDate));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
            comm.Parameters.Add(SqlHelper.GetParameter("@Creator", model.Creator));
            if (!string.IsNullOrEmpty(model.Attachment))
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@Attachment", model.Attachment));
            }

            sqllist.Add(comm);

            // 更新扩展属性
            SqlCommand commExtAttr = UpdateExtAttr(model, htExtAttr);
            if (commExtAttr != null)
            {
                sqllist.Add(commExtAttr);
            }
            #endregion

            #region 明细---Start
            CheckNotPassDetailModel detailmodel = new CheckNotPassDetailModel();
            if (!string.IsNullOrEmpty(model.ProcessNo))
            {
                for (int i = 0; i < detail.Count; i++)
                {
                    string detailsql = "";
                    detailsql += "";
                    detailsql += " INSERT INTO [officedba].[CheckNotPassDetail] ";
                    detailsql += "            ([CompanyCD]                              ";
                    detailsql += "            ,[ProcessNo]                              ";
                    detailsql += "            ,[SortNo]                                 ";
                    detailsql += "            ,[ProductID]                              ";
                    detailsql += "            ,[UnitID]                                 ";
                    detailsql += "            ,[ReasonID]                               ";
                    detailsql += "            ,[NotPassNum]                             ";
                    detailsql += "            ,[ProcessWay]                             ";
                    detailsql += "            ,[Rate]                                   ";
                    detailsql += "            ,[ModifiedDate]                           ";
                    if (!string.IsNullOrEmpty(detail[i].Remark))
                    {
                        detailsql += "          ,[Remark]                               ";
                    }
                    detailsql += "            ,[ModifiedUserID])                        ";
                    detailsql += "      VALUES                                          ";
                    detailsql += "            (@CompanyCD                               ";
                    detailsql += "            ,@ProcessNo                               ";
                    detailsql += "            ,@SortNo                                  ";
                    detailsql += "            ,@ProductID                               ";
                    detailsql += "            ,@UnitID                                  ";
                    detailsql += "            ,@ReasonID                                ";
                    detailsql += "            ,@NotPassNum                              ";
                    detailsql += "            ,@ProcessWay                              ";
                    detailsql += "            ,@Rate                                    ";
                    detailsql += "            ,@ModifiedDate                            ";
                    if (!string.IsNullOrEmpty(detail[i].Remark))
                    {
                        detailsql += "        ,@Remark                                  ";
                    }
                    detailsql += "            ,@ModifiedUserID)                         ";
                    SqlCommand detailcomm = new SqlCommand();
                    detailcomm.CommandText = detailsql;
                    detailcomm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                    detailcomm.Parameters.Add(SqlHelper.GetParameter("@ProcessNo", model.ProcessNo));
                    detailcomm.Parameters.Add(SqlHelper.GetParameter("@SortNo", detail[i].SortNo));

                    detailcomm.Parameters.Add(SqlHelper.GetParameter("@ProductID", detail[i].ProductID));
                    detailcomm.Parameters.Add(SqlHelper.GetParameter("@UnitID", detail[i].UnitID));
                    detailcomm.Parameters.Add(SqlHelper.GetParameter("@ReasonID", detail[i].ReasonID));

                    detailcomm.Parameters.Add(SqlHelper.GetParameter("@NotPassNum", detail[i].NotPassNum));
                    detailcomm.Parameters.Add(SqlHelper.GetParameter("@ProcessWay", detail[i].ProcessWay));
                    detailcomm.Parameters.Add(SqlHelper.GetParameter("@Rate", detail[i].Rate));

                    detailcomm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", DateTime.Now));
                    if (!string.IsNullOrEmpty(detail[i].Remark))
                    {
                        detailcomm.Parameters.Add(SqlHelper.GetParameter("@Remark", detail[i].Remark));
                    }
                    detailcomm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
                    sqllist.Add(detailcomm);

                }
            }
            #endregion
            if (SqlHelper.ExecuteTransWithArrayList(sqllist))
            {
                return true;
            }
            else
            {
                return false;
            }


        }


        /// <summary>
        /// 扩展属性更新命令
        /// </summary>
        /// <param name="model">不合格品处置单</param>
        /// <param name="htExtAttr">扩展属性</param>
        /// <returns></returns>
        public static SqlCommand UpdateExtAttr(CheckNotPassModel model, Hashtable htExtAttr)
        {
            SqlCommand sqlcomm = new SqlCommand();
            if (htExtAttr == null || htExtAttr.Count < 1)
            {// 没有属性需要修改
                return null;
            }

            StringBuilder sb = new StringBuilder(" UPDATE officedba.CheckNotPass SET ");
            foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
            {
                sb.AppendFormat(" {0}=@{0},", de.Key.ToString());
                sqlcomm.Parameters.Add(SqlHelper.GetParameter(String.Format("@{0}", de.Key.ToString()), de.Value));
            }
            string strSql = sb.ToString();
            strSql = strSql.TrimEnd(',');
            strSql += " WHERE CompanyCD = @CompanyCD  AND ProcessNo = @ProcessNo ";
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@ProcessNo", model.ProcessNo));
            sqlcomm.CommandText = strSql;

            return sqlcomm;
        }

        /// <summary>
        /// 检索
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetAllNoPass(CheckNotPassModel model, string ProductID, string BetinTime, string EndTime, string FlowStatus, string EFIndex, string EFDesc, ref int TotalCount)
        {
            string sql = "";
            int BillTypeFlag = int.Parse(ConstUtil.CODING_RULE_StorageQuality_NO);
            int BillTypeCode = int.Parse(ConstUtil.CODING_RULE_StorageNOPass_NO);
            sql += "select * from (";
            sql += " SELECT c.[ID],c.ModifiedDate ";
            sql += "       ,c.[ProcessNo] ";
            sql += "       ,c.[FromType] ";
            sql += "       ,(case c.FromType when '1'then '质检报告单' else '' end) as FromTypeName ";
            sql += "       ,c.[ReportID],isnull(q.ReportNo,'') as ReportNo ";
            sql += "       ,CASE f.FlowStatus WHEN '1' THEN '待审批' WHEN '2' THEN '审批中' WHEN '3' THEN '审批通过' WHEN '4' THEN '审批不通过' when '5' then '撤消审批' ELSE '' END AS FlowStatus";
            sql += "       ,CASE c.BillStatus WHEN '1' THEN '制单' WHEN '2' THEN '执行' WHEN '3' THEN '变更' WHEN '4' THEN '手工结单' WHEN '5' THEN '自动结单' ELSE '' END AS BillStatus";
            sql += "       ,isnull(e.[EmployeeName],'') as EmployeeName ";
            sql += "       ,isnull(substring(CONVERT(varchar,c.ProcessDate,120),0,11),'')   as    ProcessDate ";
            sql += "       ,isnull(c.[BillStatus],'0')  as BillStatusID ";
            sql += "       ,isnull(c.[Title],'') as Title ";
            sql += "       ,isnull(p.ProductName,'') as ProductName ";
            sql += "       ,isnull(p.ProdNo,'') as ProNo ,isnull(f.FlowStatus,'0') as FlowStatusID";
            sql += "   FROM [officedba].[CheckNotPass] as c left join officedba.QualityCheckReport as q     ";
            sql += " on c.ReportID=q.ID left join officedba.ProductInfo as p on q.ProductID=p.ID                    ";
            sql += "  left join officedba.EmployeeInfo as e on c.Executor=e.ID                                      ";
            sql += "  left join    officedba.FlowInstance AS f ON c.CompanyCD = f.CompanyCD AND f.BillTypeFlag = " + BillTypeFlag + " AND f.BillTypeCode = " + BillTypeCode + " AND  c.ID = f.BillID    ";
            sql += "   and f.ID=(select max(ID) from officedba.FlowInstance where c.CompanyCD = officedba.FlowInstance.CompanyCD AND officedba.FlowInstance.BillTypeFlag = " + BillTypeFlag + " AND officedba.FlowInstance.BillTypeCode = " + BillTypeCode + " AND  c.ID = officedba.FlowInstance.BillID) ";
            sql += " where c.CompanyCD=@CompanyCD  ";
            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            if (ProductID != "0" && ProductID.ToString().Trim() != "")
            {
                sql += "  and  p.ID=@ProductID                                                                     ";
                comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", ProductID));
            }
            if (!string.IsNullOrEmpty(model.Title))
            {
                sql += "   and c.Title like @Title                                                                  ";
                comm.Parameters.Add(SqlHelper.GetParameter("@Title", "%" + model.Title + "%"));

            }
            if (!string.IsNullOrEmpty(model.ProcessNo))
            {
                sql += "   and c.ProcessNo like @ProcessNo                                                                ";
                comm.Parameters.Add(SqlHelper.GetParameter("@ProcessNo", "%" + model.ProcessNo + "%"));
            }
            if (model.ReportID != 0)
            {
                sql += "  and c.ReportID=@ReportID";
                comm.Parameters.Add(SqlHelper.GetParameter("@ReportID", model.ReportID));
            }
            if (model.BillStatus != "00")
            {
                sql += "  and c.BillStatus=@BillStatus";
                comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", model.BillStatus));
            }
            if (!string.IsNullOrEmpty(BetinTime))
            {
                sql += "    and c.ProcessDate>=@BeginTime";
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginTime", BetinTime));

            }
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                sql += " and c.ExtField" + EFIndex + " LIKE @EFDesc ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + EFDesc + "%"));
            }

            if (!string.IsNullOrEmpty(EndTime))
            {

                sql += "         and c.ProcessDate<=@EndTime";
                comm.Parameters.Add(SqlHelper.GetParameter("@EndTime", EndTime));
            }
            if (FlowStatus != "00" && FlowStatus != "6")
            {
                sql += " and f.FlowStatus=@FlowStatus";
                comm.Parameters.Add(SqlHelper.GetParameter("@FlowStatus", FlowStatus));

            }
            sql += " ) as Info";
            if (FlowStatus == "6")
            {
                sql += " where  FlowStatusID=@FlowStatus1";
                comm.Parameters.Add(SqlHelper.GetParameter("@FlowStatus1", "0"));
            }
            if (model.Creator == -100)
            {
                sql += " order by " + model.Attachment;
            }

            comm.CommandText = sql;
            DataTable dt = new DataTable();
            if (model.Creator == -100)
            {
                dt = SqlHelper.ExecuteSearch(comm);
            }
            else
            {
                dt = SqlHelper.PagerWithCommand(comm, model.Creator, model.Confirmor, model.Attachment, ref TotalCount);
            }
            return dt;

        }

        public static bool DelNoPass(string ID, string CompanyCD)
        {
            ArrayList listADD = new ArrayList();
            string[] arrID = ID.Split(',');
            if (arrID.Length > 0)
            {
                for (int i = 1; i < arrID.Length; i++)
                {
                    StringBuilder sqlDet = new StringBuilder();
                    StringBuilder sqlBom = new StringBuilder();
                    sqlDet.AppendLine("delete from officedba.CheckNotPassDetail where CompanyCD=@CompanyCD and ProcessNo=(select top 1 ProcessNo from officedba.CheckNotPass where CompanyCD=@CompanyCD and ID=@ID)");
                    sqlBom.AppendLine("delete from officedba.CheckNotPass where CompanyCD=@CompanyCD and ID=@ID");

                    SqlCommand commDet = new SqlCommand();
                    commDet.CommandText = sqlDet.ToString();
                    commDet.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                    commDet.Parameters.Add(SqlHelper.GetParameter("@ID", arrID[i].ToString()));
                    listADD.Add(commDet);

                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = sqlBom.ToString();
                    comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                    comm.Parameters.Add(SqlHelper.GetParameter("@ID", arrID[i].ToString()));
                    listADD.Add(comm);
                }
            }
            return SqlHelper.ExecuteTransWithArrayList(listADD);
        }
        public static DataTable GetNoPassInfo(CheckNotPassModel model)
        {
            int BillTypeFlag = int.Parse(ConstUtil.CODING_RULE_StorageQuality_NO);
            int BillTypeCode = int.Parse(ConstUtil.CODING_RULE_StorageNOPass_NO);
            string sql = "";
            sql += " SELECT isnull(c.[ProcessNo],'') as ProcessNo                                                                                                                                            ";
            sql += "       ,c.[FromType],c.ID                                                                                                                                                                     ";
            sql += "       ,c.[ReportID]                                                                                                                                                                       ";
            sql += "       ,c.[Executor]                                                                                                                                                                       ";
            sql += "       ,Replace(c.[Attachment],'\\',',') as Attachment ";
            sql += "       ,isnull(CONVERT(varchar,c.ProcessDate,120),'') as [ProcessDate]                                                                                                                                                                    ";
            sql += "       ,isnull(c.[Remark],'') as Remark                                                                                                                                                    ";
            sql += "       ,isnull(CONVERT(varchar,c.CreateDate,120),'') as [CreateDate]                                                                                                                                                                     ";
            sql += "       ,isnull(f.FlowStatus,'0') as FlowStatus";
            sql += "       ,c.[BillStatus]                                                                                                                                                                     ";
            sql += "       ,c.[Confirmor]                                                                                                                                                                      ";
            sql += "       ,isnull(CONVERT(varchar,c.ConfirmorDate,120),'') as [ConfirmorDate]                                                                                                                                                                  ";
            sql += "       ,c.[Closer]";
            sql += "       ,q.ProductID ";
            sql += "       ,isnull(CONVERT(varchar,c.CloserDate,120),'') as [CloserDate]                                                                                                                                                                     ";
            sql += "       ,isnull(CONVERT(varchar,c.ModifiedDate,120),'') as [ModifiedDate]                                                                                                                                                                   ";
            sql += "       ,(c.ModifiedUserID) as ModifiedUserIDName";
            sql += "       ,c.[ModifiedUserID]                                                                                                                                                                 ";
            sql += "       ,(select EmployeeName from officedba.EmployeeInfo where officedba.EmployeeInfo.ID=c.Closer) as CloserName";
            sql += "       ,(select EmployeeName from officedba.EmployeeInfo where officedba.EmployeeInfo.ID=c.Confirmor) as ConfirmorName";
            sql += "       ,(select EmployeeName from officedba.EmployeeInfo where officedba.EmployeeInfo.ID=c.Creator) as CreatorName";
            sql += "       ,isnull(c.[Creator],0) as Creator                                                                                                                                                   ";
            sql += "       ,isnull(c.[Title],'')as Title                                                                                                                                                     ";
            sql += "       ,isnull(q.ReportNo,'') as ReportNo                                                                                                                                                ";
            sql += "       ,isnull(e.EmployeeName,'') as EmployeeName                                                                                                                                        ";
            sql += "       ,isnull((select ProductName from officedba.ProductInfo as p1 where p1.ID=q.ProductID),'')as ProductName                                                                           ";
            sql += "       ,isnull((select ProdNo from officedba.ProductInfo as p1 where p1.ID=q.ProductID),'')as ProNo                                                                                      ";
            sql += "       ,isnull((select Specification from officedba.ProductInfo as p1 where p1.ID=q.ProductID),'')as Specification                                                                         ";
            sql += "       ,isnull((select CodeName from officedba.CodeUnitType where officedba.CodeUnitType.ID=(select UnitID from officedba.ProductInfo as p1 where p1.ID=q.ProductID)),'')as UnitName     ";
            sql += "       ,isnull((select UnitID from officedba.ProductInfo as p1 where p1.ID=q.ProductID),0) as UnitID";
            sql += "       ,convert(numeric(12,2),isnull(q.NoPass,0))  as NoPass                                                                                                                                                          ";
            sql += "       ,c.ExtField1,c.ExtField2,c.ExtField3,c.ExtField4,c.ExtField5,c.ExtField6,c.ExtField7,c.ExtField8,c.ExtField9,c.ExtField10 ";
            sql += "   FROM [officedba].[CheckNotPass] as c left join officedba.QualityCheckReport as q on c.ReportID=q.ID                                                                             ";
            sql += "       left join officedba.EmployeeInfo as e on c.Executor=e.ID                                                                             ";
            sql += "       left join    officedba.FlowInstance AS f ON c.CompanyCD = f.CompanyCD AND f.BillTypeFlag = " + BillTypeFlag + " AND f.BillTypeCode = " + BillTypeCode + " AND  c.ID = f.BillID    ";
            sql += "    where c.CompanyCD=@CompanyCD and q.CompanyCD=@CompanyCD and c.ID=@ID      ";

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", model.ID.ToString()));


            //指定命令的SQL文
            comm.CommandText = sql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        public static DataTable GetNoPassDetail(CheckNotPassModel model)
        {
            string sql = "";
            sql += " select * from (SELECT [ProcessNo],ID                                     ";
            sql += "       ,[SortNo]                                                       ";
            sql += "       ,[ProductID]                                                    ";
            sql += "       ,[UnitID]                                                       ";
            sql += "       ,[ReasonID]                                                     ";
            sql += "       ,convert(numeric(12,2),isnull(NotPassNum,0)) NotPassNum         ";
            sql += "       ,[ProcessWay]                                                   ";
            sql += "       ,convert(numeric(12,2),isnull(Rate,0))  as Rate                                       ";
            sql += "       ,isnull([Remark],'') as Remark                                  ";
            sql += "   FROM [officedba].[CheckNotPassDetail]                       ";
            sql += " where [officedba].[CheckNotPassDetail].ProcessNo=(select top 1 ProcessNo from [officedba].[CheckNotPass] where CompanyCD=@CompanyCD and ID=@ID)) as Info   ";
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", model.ID.ToString()));


            //指定命令的SQL文
            comm.CommandText = sql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }

        /// <summary>
        /// 更新不合格品处置单 
        /// </summary>
        /// <param name="model">不合格品处置单 </param>
        /// <param name="detailmodel">明细</param>
        /// <param name="SortID"></param>
        /// <param name="htExtAttr">扩展属性</param>
        /// <returns></returns>
        public static bool UpdateNoPassInfo(CheckNotPassModel model, List<CheckNotPassDetailModel> detailmodel, string[] SortID, Hashtable htExtAttr)
        {
            string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//[待修改]
            ArrayList sqllist = new ArrayList();
            //string loginUserID = "admin123";
            if (string.IsNullOrEmpty(model.ProcessNo))
            {
                return false;
            }
            #region 基本信息
            string sql = "";
            sql += " UPDATE [officedba].[CheckNotPass]    ";
            sql += "        SET     [FromType] = @FromType  ";
            sql += "       ,[ReportID] = @ReportID                ";
            sql += "       ,[Executor] = @Executor                ";
            sql += "       ,[ProcessDate] = @ProcessDate          ";
            sql += "       ,[BillStatus] = @BillStatus            ";
            sql += "       ,[ModifiedDate] = @ModifiedDate        ";
            sql += "       ,[ModifiedUserID] = @ModifiedUserID    ";


            sql += "       ,[Attachment] = @Attachment            ";


            sql += "       ,[Title] = @Title                      ";

            sql += "       ,[Remark] = @Remark                    ";

            sql += "  WHERE ID=@ID                                ";
            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
            comm.Parameters.Add(SqlHelper.GetParameter("@ReportID", model.ReportID));
            comm.Parameters.Add(SqlHelper.GetParameter("@Executor", model.Executor));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProcessDate", model.ProcessDate));
            comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", model.BillStatus));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", DateTime.Now));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", loginUserID));

            comm.Parameters.Add(SqlHelper.GetParameter("@Attachment", model.Attachment));


            comm.Parameters.Add(SqlHelper.GetParameter("@Title", model.Title));

            comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));

            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.CommandText = sql;
            sqllist.Add(comm);

            // 更新扩展属性
            SqlCommand commExtAttr = UpdateExtAttr(model, htExtAttr);
            if (commExtAttr != null)
            {
                sqllist.Add(commExtAttr);
            }
            #endregion

            #region 明细
            if (SortID.Length > 0)
            {
                string delsql = "DELETE FROM [officedba].[CheckNotPassDetail] where CompanyCD=@CompanyCD and ProcessNo=@ProcessNo ";
                SqlCommand delcomm = new SqlCommand();
                delcomm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                delcomm.Parameters.Add(SqlHelper.GetParameter("@ProcessNo", model.ProcessNo));
                delcomm.CommandText = delsql;
                sqllist.Add(delcomm);
                if (SortID[0] != null && SortID[0] != "")
                {
                    for (int i = 0; i < detailmodel.Count; i++)
                    {
                        string detailsql = "";
                        detailsql += "";
                        detailsql += " INSERT INTO [officedba].[CheckNotPassDetail] ";
                        detailsql += "            ([CompanyCD]                              ";
                        detailsql += "            ,[ProcessNo]                              ";
                        detailsql += "            ,[SortNo]                                 ";
                        detailsql += "            ,[ProductID]                              ";
                        detailsql += "            ,[UnitID]                                 ";
                        detailsql += "            ,[ReasonID]                               ";
                        detailsql += "            ,[NotPassNum]                             ";
                        detailsql += "            ,[ProcessWay]                             ";
                        detailsql += "            ,[Rate]                                   ";
                        detailsql += "            ,[ModifiedDate]                           ";
                        if (!string.IsNullOrEmpty(detailmodel[i].Remark))
                        {
                            detailsql += "          ,[Remark]                               ";
                        }
                        detailsql += "            ,[ModifiedUserID])                        ";
                        detailsql += "      VALUES                                          ";
                        detailsql += "            (@CompanyCD                               ";
                        detailsql += "            ,@ProcessNo                               ";
                        detailsql += "            ,@SortNo                                  ";
                        detailsql += "            ,@ProductID                               ";
                        detailsql += "            ,@UnitID                                  ";
                        detailsql += "            ,@ReasonID                                ";
                        detailsql += "            ,@NotPassNum                              ";
                        detailsql += "            ,@ProcessWay                              ";
                        detailsql += "            ,@Rate                                    ";
                        detailsql += "            ,@ModifiedDate                            ";
                        if (!string.IsNullOrEmpty(detailmodel[i].Remark))
                        {
                            detailsql += "        ,@Remark                                  ";
                        }
                        detailsql += "            ,@ModifiedUserID)                         ";
                        SqlCommand detailcomm = new SqlCommand();
                        detailcomm.CommandText = detailsql;
                        detailcomm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                        detailcomm.Parameters.Add(SqlHelper.GetParameter("@ProcessNo", model.ProcessNo));
                        detailcomm.Parameters.Add(SqlHelper.GetParameter("@SortNo", detailmodel[i].SortNo));

                        detailcomm.Parameters.Add(SqlHelper.GetParameter("@ProductID", detailmodel[i].ProductID));
                        detailcomm.Parameters.Add(SqlHelper.GetParameter("@UnitID", detailmodel[i].UnitID));
                        detailcomm.Parameters.Add(SqlHelper.GetParameter("@ReasonID", detailmodel[i].ReasonID));

                        detailcomm.Parameters.Add(SqlHelper.GetParameter("@NotPassNum", detailmodel[i].NotPassNum));
                        detailcomm.Parameters.Add(SqlHelper.GetParameter("@ProcessWay", detailmodel[i].ProcessWay));
                        detailcomm.Parameters.Add(SqlHelper.GetParameter("@Rate", detailmodel[i].Rate));

                        detailcomm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", DateTime.Now));
                        if (!string.IsNullOrEmpty(detailmodel[i].Remark))
                        {
                            detailcomm.Parameters.Add(SqlHelper.GetParameter("@Remark", detailmodel[i].Remark));
                        }
                        detailcomm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
                        sqllist.Add(detailcomm);
                    }
                }
            }
            #endregion


            if (SqlHelper.ExecuteTransWithArrayList(sqllist))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ConfirmBill(CheckNotPassModel model, string CheckNum)
        {
            ArrayList sqllist = new ArrayList();
            bool returnvalue = false;
            SqlCommand thecomm = new SqlCommand();
            string thesql = "select BillStatus,ReportID from officedba.CheckNotPass where ID=@ID";
            thecomm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            thecomm.CommandText = thesql;
            string BillStatus = "0";
            string ReportID = "0";
            string FromDetailID = "0";
            string FromType = "00";
            DataTable thedt = SqlHelper.ExecuteSearch(thecomm);
            if (thedt.Rows.Count > 0)
            {
                BillStatus = thedt.Rows[0]["BillStatus"].ToString();
                ReportID = thedt.Rows[0]["ReportID"].ToString();
            }
            #region 根据报告单ID获取 源单信息
            SqlCommand reportComm = new SqlCommand();
            string reportsql = "select FromType,FromDetailID from officedba.QualityCheckReport where ID=@ID";
            reportComm.Parameters.Add(SqlHelper.GetParameter("@ID", ReportID));
            reportComm.CommandText = reportsql;
            DataTable reportDt = SqlHelper.ExecuteSearch(reportComm);
            if (reportDt.Rows.Count > 0)
            {
                FromDetailID = reportDt.Rows[0]["FromDetailID"].ToString();
                FromType = reportDt.Rows[0]["FromType"].ToString();
            }
            #endregion
            if (BillStatus == "1")
            {
                StringBuilder sql = new StringBuilder();
                SqlCommand comm = new SqlCommand();
                sql.AppendLine(" UPDATE officedba.CheckNotPass SET");
                sql.AppendLine(" Confirmor          = @Confirmor,");
                sql.AppendLine(" ConfirmorDate      = @ConfirmorDate,");
                sql.AppendLine(" BillStatus              = 2,");
                sql.AppendLine(" ModifiedUserID      = @ModifiedUserID,");
                sql.AppendLine(" ModifiedDate                = @ModifiedDate ");
                sql.AppendLine("  Where  CompanyCD=@CompanyCD and ID=@ID");
                comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                comm.Parameters.Add(SqlHelper.GetParameter("@Confirmor", model.Confirmor));
                comm.Parameters.Add(SqlHelper.GetParameter("@ConfirmorDate", model.ConfirmorDate));
                comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
                comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", model.ModifiedDate));
                comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD.Trim()));
                comm.CommandText = sql.ToString();
                sqllist.Add(comm);


                //#region 回写需要 -回写质检报告单源单类型为采购到货的数据  ---不需要
                //if (FromType == "4")
                //{
                //    SqlCommand purcomm = new SqlCommand();
                //    string pursql = "update officedba.PurchaseArriveDetail set RejectCount=isnull(RejectCount,0)+@RejectCount where ID=@ID";
                //    purcomm.Parameters.Add(SqlHelper.GetParameter("@ID", FromDetailID));
                //    purcomm.Parameters.Add(SqlHelper.GetParameter("@RejectCount", CheckNum));
                //    purcomm.CommandText = pursql;
                //    sqllist.Add(purcomm);
                //}
                //#endregion
            }
            else
            {
                returnvalue = false;
            }
            if (BillStatus == "1")
            {

                if (SqlHelper.ExecuteTransWithArrayList(sqllist))
                {
                    returnvalue = true;
                }
                else
                {
                    returnvalue = false;
                }
            }
            return returnvalue;

        }
        public static bool UnConfirmBill(CheckNotPassModel model, string CheckNum)
        {
            int BillTypeFlag = int.Parse(ConstUtil.BILL_TYPECODE_STORAGE_QUALITY);
            int BillTypeCode = int.Parse(ConstUtil.BILL_TYPECODE_STORAGE_NOPASS);
            ArrayList sqllist = new ArrayList();
            bool returnvalue = false;
            SqlCommand thecomm = new SqlCommand();
            string thesql = "select  BillStatus,ReportID from officedba.CheckNotPass where ID=@ID";
            thecomm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            thecomm.CommandText = thesql;
            string BillStatus = "0";
            DataTable thedt = SqlHelper.ExecuteSearch(thecomm);
            if (thedt.Rows.Count > 0)
            {
                BillStatus = thedt.Rows[0]["BillStatus"].ToString();
            }
            //#region 根据报告单ID获取 源单信息
            //SqlCommand reportComm = new SqlCommand();
            //string reportsql = "select FromType,FromDetailID from officedba.QualityCheckReport where ID=@ID";
            //reportComm.Parameters.Add(SqlHelper.GetParameter("@ID", ReportID));
            //reportComm.CommandText = reportsql;
            //DataTable reportDt = SqlHelper.ExecuteSearch(reportComm);
            //if (reportDt.Rows.Count > 0)
            //{
            //    FromDetailID = reportDt.Rows[0]["FromDetailID"].ToString();
            //    FromType = reportDt.Rows[0]["FromType"].ToString();
            //}
            //#endregion
            if (BillStatus == "2")
            {
                SqlCommand comm = new SqlCommand();
                string sql = "Update officedba.CheckNotPass set BillStatus='1',ModifiedUserID=@ModifiedUserID,ModifiedDate=@ModifiedDate where ID=@ID";
                comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
                comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", model.ModifiedDate));
                comm.CommandText = sql;
                sqllist.Add(comm);
                //#region 回写需要 -回写质检报告单源单类型为采购到货的数据  --不需要
                //if (FromType == "4")
                //{
                //    SqlCommand purcomm = new SqlCommand();
                //    string pursql = "update officedba.PurchaseArriveDetail set RejectCount=isnull(RejectCount,0)-@RejectCount where ID=@ID";
                //    purcomm.Parameters.Add(SqlHelper.GetParameter("@ID", FromDetailID));
                //    purcomm.Parameters.Add(SqlHelper.GetParameter("@RejectCount", CheckNum));
                //    purcomm.CommandText = pursql;
                //    sqllist.Add(purcomm);
                //}
                //#endregion
                #region 撤消审批流程
                DataTable dtFlowInstance = Common.FlowDBHelper.GetFlowInstanceInfo(model.CompanyCD, BillTypeFlag, BillTypeCode, model.ID);
                if (dtFlowInstance.Rows.Count > 0)
                {
                    //提交审批了的单据
                    string FlowInstanceID = dtFlowInstance.Rows[0]["FlowInstanceID"].ToString();
                    string FlowStatus = dtFlowInstance.Rows[0]["FlowStatus"].ToString();
                    string FlowNo = dtFlowInstance.Rows[0]["FlowNo"].ToString();

                    #region 往流程任务历史记录表
                    StringBuilder sqlHis = new StringBuilder();
                    sqlHis.AppendLine("Insert into officedba.FlowTaskHistory(CompanyCD,FlowInstanceID,FlowNo,BillTypeID,BillID,StepNo,State,operateUserId,operateDate)");
                    sqlHis.AppendLine("Values(@CompanyCD,@tempFlowInstanceID,@tempFlowNo,@BillTypeFlag,@BillID,0,2,@ModifiedUserID,getdate())");

                    SqlCommand commHis = new SqlCommand();
                    commHis.CommandText = sqlHis.ToString();
                    commHis.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                    commHis.Parameters.Add(SqlHelper.GetParameter("@tempFlowInstanceID", FlowInstanceID));
                    commHis.Parameters.Add(SqlHelper.GetParameter("@tempFlowNo", FlowNo));
                    commHis.Parameters.Add(SqlHelper.GetParameter("@BillTypeFlag", BillTypeFlag));
                    commHis.Parameters.Add(SqlHelper.GetParameter("@BillID", model.ID));
                    commHis.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
                    sqllist.Add(commHis);
                    #endregion

                    #region 更新流程任务处理表
                    StringBuilder sqlTask = new StringBuilder();
                    sqlTask.AppendLine("Update officedba.FlowTaskList Set StepNo=0,ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID");
                    sqlTask.AppendLine("Where CompanyCD=@CompanyCD and FlowInstanceID=@tempFlowInstanceID");


                    SqlCommand commTask = new SqlCommand();
                    commTask.CommandText = sqlTask.ToString();
                    commTask.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                    commTask.Parameters.Add(SqlHelper.GetParameter("@tempFlowInstanceID", FlowInstanceID));
                    commTask.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
                    sqllist.Add(commTask);
                    #endregion

                    #region 更新流程实例表
                    StringBuilder sqlIns = new StringBuilder();
                    sqlIns.AppendLine("Update officedba.FlowInstance Set FlowStatus=5,ModifiedDate=getdate(),ModifiedUserID=@ModifiedUserID ");
                    sqlIns.AppendLine("Where CompanyCD=@CompanyCD ");
                    sqlIns.AppendLine("and FlowNo=@tempFlowNo ");
                    sqlIns.AppendLine("and BillTypeFlag=@BillTypeFlag ");
                    sqlIns.AppendLine("and BillTypeCode=@BillTypeCode ");
                    sqlIns.AppendLine("and BillID=@BillID");


                    SqlCommand commIns = new SqlCommand();
                    commIns.CommandText = sqlIns.ToString();
                    commIns.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                    commIns.Parameters.Add(SqlHelper.GetParameter("@tempFlowNo", FlowNo));
                    commIns.Parameters.Add(SqlHelper.GetParameter("@BillTypeFlag", BillTypeFlag));
                    commIns.Parameters.Add(SqlHelper.GetParameter("@BillTypeCode", BillTypeCode));
                    commIns.Parameters.Add(SqlHelper.GetParameter("@BillID", model.ID));
                    commIns.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
                    sqllist.Add(commIns);
                    #endregion

                }
                #endregion
            }
            else
            {
                returnvalue = false;
            }
            if (BillStatus == "2")
            {

                if (SqlHelper.ExecuteTransWithArrayList(sqllist))
                {
                    returnvalue = true;
                }
                else
                {
                    returnvalue = false;
                }
            }
            return returnvalue;
        }

        public static bool CloseBill(CheckNotPassModel model, string method)
        {
            ArrayList listsql = new ArrayList();
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.CheckNotPass SET");
            sql.AppendLine(" BillStatus              =@BillStatus,");
            sql.AppendLine(" ModifiedUserID      = @ModifiedUserID,");
            if (method == "0")
            {
                sql.AppendLine(" Closer  = @Closer,");
                sql.AppendLine(" CloserDate = @CloserDate, ");
            }
            sql.AppendLine(" ModifiedDate    = @ModifiedDate");
            sql.AppendLine("  Where  CompanyCD=@CompanyCD and ID=@ID");

            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", model.ModifiedDate));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            string BillStatus = "2";
            if (method == "0")
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@Closer", model.Closer));
                comm.Parameters.Add(SqlHelper.GetParameter("@CloserDate", model.CloserDate));
                BillStatus = "4";
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", BillStatus));
            comm.CommandText = sql.ToString();
            listsql.Add(comm);
            if (SqlHelper.ExecuteTransWithArrayList(listsql))
            { return true; }
            else
            { return false; }
        }
        public static string IsFlow(int ID)
        {
            string Rows = "0";
            string returnValue = "0";
            int BillTypeFlag = int.Parse(ConstUtil.BILL_TYPECODE_STORAGE_QUALITY);
            int BillTypeCode = int.Parse(ConstUtil.BILL_TYPECODE_STORAGE_NOPASS);
            string sql = "";
            sql += "select count(e.ID) as Rows from officedba.CheckNotPass as a left join officedba.FlowInstance AS e ON a.CompanyCD = e.CompanyCD AND e.BillTypeFlag =" + BillTypeFlag + " AND e.BillTypeCode = " + BillTypeCode + " AND a.ID = e.BillID ";
            sql += " where a.ID=" + ID + " and e.FlowStatus!=4 and e.FlowStatus!=5";
            DataTable dt = SqlHelper.ExecuteSql(sql);
            if (dt.Rows.Count > 0)
            {
                Rows = dt.Rows[0]["Rows"].ToString();
            }
            if (int.Parse(Rows) > 0)
            {
                returnValue = Rows;
            }
            return returnValue;
        }
        public static string GetNoExecutNum(CheckNotPassModel model)
        {
            SqlCommand comm = new SqlCommand();
            string sql = "";
            sql += " select isnull(a.NoPass,0) as NoPass, ";
            sql += " isnull((select isnull(sum(NotPassNum),0) from officedba.CheckNotPassDetail as m left join officedba.CheckNotPass as n on m.ProcessNo=n.ProcessNo where n.BillStatus='2' and m.CompanyCD=@CompanyCD and n.ReportID=@ReportID),0) as NotPassNum";
            sql += " from   officedba.QualityCheckReport as a  ";
            sql += " where  a.CompanyCD=@CompanyCD  and a.ID=@ID  ";
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ReportID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ReportID", model.ReportID));
            comm.CommandText = sql;
            DataTable dt = SqlHelper.ExecuteSearch(comm);
            string NoPass = "0";
            string NotPassNum = "0";
            if (dt.Rows.Count > 0)
            {
                NoPass = dt.Rows[0]["NoPass"].ToString();
                NotPassNum = dt.Rows[0]["NotPassNum"].ToString();

            }
            string value = "0";
            if (NoPass != "" && NotPassNum != "")
            {
                value = Convert.ToString(Convert.ToDecimal(NoPass) - Convert.ToDecimal(NotPassNum));
            }
            return value;

        }


        //----------------------------------------------页面打印需要
        public static DataTable GetNoPassInfo(int ID)
        {
            int BillTypeFlag = int.Parse(ConstUtil.CODING_RULE_StorageQuality_NO);
            int BillTypeCode = int.Parse(ConstUtil.CODING_RULE_StorageNOPass_NO);
            string sql = @" SELECT pi1.ProdNo,pi1.ProductName,pi1.Specification,cut.CodeName
,CASE cnp.BillStatus WHEN '1' THEN '制单' WHEN '2' THEN '执行' WHEN '3' THEN '变更' WHEN '4' THEN '手工结单' WHEN '5' THEN '自动结单' ELSE '' END AS BillStatus
,CASE cnp.FromType WHEN '0' THEN '无来源' WHEN '1' THEN '质检申请单' WHEN '2' THEN '质检报告单' WHEN '3' THEN '生产任务单' WHEN '4' THEN '采购到货单' ELSE '' END AS FromType
,ei2.EmployeeName AS ExecutorName, ei3.EmployeeName AS CreatorName,ei4.EmployeeName AS ConfirmorName,ei5.EmployeeName AS CloserName
,isnull(substring(CONVERT(varchar,cnp.ProcessDate,120),0,11),'') as ProcessDate
,isnull(substring(CONVERT(varchar,cnp.CreateDate,120),0,11),'') as CreateDate
,isnull(substring(CONVERT(varchar,cnp.ConfirmorDate,120),0,11),'') as ConfirmDate
,isnull(substring(CONVERT(varchar,cnp.CloserDate,120),0,11),'') as CloseDate
,isnull(substring(CONVERT(varchar,cnp.ModifiedDate,120),0,11),'') as ModifiedDate
,ISNULL(qcr.NoPass,0) AS NoPass,qcr.ReportNO AS ReportNO,cnp.ProcessNo
,cnp.Title,cnp.ModifiedUserID,cnp.Remark,cnp.ExtField1,cnp.ExtField2,cnp.ExtField3,cnp.ExtField4
,cnp.ExtField5,cnp.ExtField6,cnp.ExtField7,cnp.ExtField8,cnp.ExtField9,cnp.ExtField10
FROM officedba.CheckNotPass cnp
LEFT JOIN officedba.QualityCheckReport qcr ON cnp.CompanyCD=qcr.CompanyCD AND cnp.ReportID=qcr.ID
LEFT JOIN officedba.ProductInfo pi1 ON cnp.CompanyCD=pi1.CompanyCD AND qcr.ProductID=pi1.ID
LEFT JOIN officedba.CodeUnitType cut ON cnp.CompanyCD=cut.CompanyCD AND pi1.UnitID=cut.ID
LEFT JOIN officedba.EmployeeInfo ei2 ON cnp.CompanyCD=ei2.CompanyCD AND cnp.Executor=ei2.ID
LEFT JOIN officedba.EmployeeInfo ei3 ON cnp.CompanyCD=ei3.CompanyCD AND cnp.Creator=ei3.ID
LEFT JOIN officedba.EmployeeInfo ei4 ON cnp.CompanyCD=ei4.CompanyCD AND cnp.Confirmor=ei4.ID
LEFT JOIN officedba.EmployeeInfo ei5 ON cnp.CompanyCD=ei5.CompanyCD AND cnp.Closer=ei5.ID
WHERE cnp.ID=@ID ";


            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID.ToString()));


            //指定命令的SQL文
            comm.CommandText = sql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }

        public static DataTable GetNoPassDetail(int ID)
        {
            string sql = "";
            sql += " select * from (SELECT      ";
            sql += "       isnull(b.CodeName,'') as [ReasonID]                                                     ";
            sql += "       ,convert(numeric(12,2),isnull(a.NotPassNum,0)) NotPassNum         ";
            sql += "       ,case a.[ProcessWay] when '1' then '拒收' when '2' then '报费' when '3' then '降级' when '4' then '销毁' else '' end as ProcessWay   ";
            sql += "       ,convert(numeric(12,2),isnull(a.Rate,0))  as Rate               ";
            sql += "       ,isnull(a.[Remark],'') as Remark                                ";
            sql += "   FROM [officedba].[CheckNotPassDetail] as a                  ";
            sql += "       left join officedba.CodeReasonType as b on a.ReasonID=b.ID ";
            sql += " where a.ProcessNo=(select top 1 ProcessNo from [officedba].[CheckNotPass] where ID=@ID)) as Info   ";
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID.ToString()));
            //指定命令的SQL文
            comm.CommandText = sql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }

    }
}

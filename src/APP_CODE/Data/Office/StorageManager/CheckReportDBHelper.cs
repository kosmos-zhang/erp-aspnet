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
    public class CheckReportDBHelper
    {
        public static string myReportNo = "00", myFromType = "00";
        public static string FromReportID = "0";//源单ID
        public static string FromDetailID = "0";//源单明细ID
        /// <summary>
        /// 获取质检报告的对应质检申请单
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCheckApplay(string CompanyCD, string method, string ReportStr)
        {
            SqlCommand comm = new SqlCommand();
            string[] testStr = ReportStr.Split('?');
            string BillNo = testStr[0];
            string BillTitle = testStr[1];

            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" SELECT distinct q1.[ID],q1.ModifiedDate ");
            sql.AppendLine("       ,isnull(q1.[Title],'') as Title ");
            sql.AppendLine("       ,q1.[CompanyCD] ");
            sql.AppendLine("       ,q1.[ApplyNo] ");
            sql.AppendLine("       ,isnull(q1.[FromType],'') as FromType ");
            sql.AppendLine("       ,isnull(q1.[CustID],0) as CustID ");
            sql.AppendLine("       ,isnull(q1.[CustName],'') as CustName ");
            sql.AppendLine("       ,isnull(q1.[CustBigType],0) as CustBigTypeID ");
            sql.AppendLine("       ,(case q1.[CustBigType] when '1' then '客户' when '2' then '供应商' when '3' then '竞争对手' when '4' then '银行' when '5' then '外协加工厂' when '6' then '运输商' when '7' then '其他' else '' end) as CustBigTypeName   ");
            sql.AppendLine("       ,isnull(q1.[CheckType],'') as CheckType ");
            sql.AppendLine("       ,(case q1.CheckType when '1' then '进货检验' when '2' then '过程检验' when '3' then '最终检验' else '' end ) as CheckTypeName ");
            sql.AppendLine("       ,(case q1.CheckMode when '1' then '全检' when '2' then '抽检' when '3' then '临检' else '' end ) as CheckModeName ");
            sql.AppendLine("       ,isnull(q1.[CheckMode],'') as CheckMode ");
            sql.AppendLine("       ,isnull(q1.[Checker],0)  as ApplyUserID ");
            sql.AppendLine("       ,isnull(q1.[CheckDeptId],0) as CheckDeptId ");
            sql.AppendLine(" 	   ,isnull((select officedba.DeptInfo.DeptName from officedba.DeptInfo where officedba.DeptInfo.ID=q1.CheckDeptId),'') as CheckDeptName ");
            sql.AppendLine("       ,isnull(q1.[CheckDate],'') as CheckDate ");
            sql.AppendLine("       ,isnull(q1.[Principal],0) as PrincipalID ");
            sql.AppendLine("       ,isnull((select officedba.DeptInfo.DeptName from officedba.DeptInfo where officedba.DeptInfo.ID=q1.DeptID ),'') as DeptName ");
            sql.AppendLine("       ,isnull(q1.[DeptID],0) as DeptID ");
            sql.AppendLine("       ,isnull(q1.[CountTotal],0) as CountTotal ");
            sql.AppendLine("       ,isnull((select officedba.EmployeeInfo.EmployeeName from officedba.EmployeeInfo where officedba.EmployeeInfo.ID=q1.Principal),'') as PrincipalName ");
            sql.AppendLine("       ,isnull((select officedba.EmployeeInfo.EmployeeName from officedba.EmployeeInfo where officedba.EmployeeInfo.ID=q1.Checker),'') as ApplyUserName ");
            sql.AppendLine("   FROM [officedba].[QualityCheckApplay] as q1 left join officedba.EmployeeInfo as e on q1.Principal=e.ID ");
            sql.AppendLine("   left join officedba.ProviderInfo as p on q1.CustID=p.ID ");
            sql.AppendLine("   left join officedba.QualityCheckApplyDetail as qd on q1.ApplyNo=qd.ApplyNo");
            sql.AppendLine("   where q1.CompanyCD=@CompanyCD and   q1.BillStatus='2' and qd.CompanyCD=@CompanyCD ");
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            if (BillNo != "")
            {
                sql.AppendLine(" and q1.ApplyNo like @ApplyNo ");
                comm.Parameters.Add(SqlHelper.GetParameter("@ApplyNo", "%" + BillNo + "%"));
            }
            if (BillTitle != "")
            {
                sql.AppendLine(" and q1.Title like @Title");
                comm.Parameters.Add(SqlHelper.GetParameter("@Title", "%" + BillTitle + "%"));
            }
            if (method != "2")
            {
                sql.AppendLine("    and  isnull(qd.ProductCount,0) >isnull(qd.RealCheckedCount,0) and qd.ProductCount is not null");
            }
            sql.AppendLine(" order by q1.ModifiedDate desc ");
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }
        /// <summary>
        /// 根据编号获取相应的明细
        /// </summary>
        /// <param name="ApplyNo"></param>
        /// <returns></returns>
        public static DataTable GetDetailBy(string ApplyNo, string CompanyCD, string ReportStr)
        {
            string[] testStr = ReportStr.Split('?');
            string ProductNo = testStr[0];
            string ProductName = testStr[1];
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("  SELECT q.[ID] ");
            sql.AppendLine("        ,q.FromLineNo");
            sql.AppendLine("        ,q.[UnitID] ");
            sql.AppendLine("        ,q.[ProductCount] ");
            sql.AppendLine(" 	    ,isnull(p.ProductName,'') as ProductName ");
            sql.AppendLine("        ,isnull(p.Specification,'') as Specification");
            sql.AppendLine("        ,q.ProductID,q1.ModifiedDate ");
            sql.AppendLine("        ,isnull(c.CodeName,'') as CodeName ");
            sql.AppendLine("        ,case q1.CheckMode when '1' then '全检' when '2' then '抽检' when '3' then '临检' else '' end as CheckMode,q1.CheckMode as CheckModeID");
            sql.AppendLine("        ,isnull(p.ProdNo,'') as ProNo ");
            sql.AppendLine(" ,isnull(q.RealCheckedCount,0) as CheckedCount");
            sql.AppendLine("   FROM [officedba].[QualityCheckApplyDetail] as q left join officedba.ProductInfo as p on q.ProductID=p.ID ");
            sql.AppendLine("   left join officedba.QualityCheckApplay as q1 on q.ApplyNo=q1.ApplyNo ");
            sql.AppendLine("   left join officedba.CodeUnitType as c on q.UnitID=c.ID  where q.ApplyNo=@ApplyNo  and q.CompanyCD=@CompanyCD  and q1.CompanyCD=@CompanyCD ");
            if (ProductNo != "")
            {
                sql.AppendLine(" and p.ProdNo like @ProdNo");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProdNo", "%" + ProductNo + "%"));
            }
            if (ProductName != "")
            {
                sql.AppendLine(" and p.ProductName=@ProductName");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProductName", ProductName));
            }
            //sql += " order by  q1.ModifiedDate desc ";
            comm.Parameters.Add(SqlHelper.GetParameter("@ApplyNo", ApplyNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);

        }
        /// <summary>
        /// 获取质检报告 生产需要
        /// </summary>
        /// <returns></returns>
        public static DataTable GetMan(string CompanyCD, string method, string ReportStr)
        {
            SqlCommand comm = new SqlCommand();
            string[] testStr = ReportStr.Split('?');
            string BillNo = testStr[0];
            string BillTitle = testStr[1];
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("  SELECT distinct m.[ID],m.ModifiedDate          ");
            sql.AppendLine("       ,m.[TaskNo]                                        ");
            sql.AppendLine("       ,isnull(m.[Subject],'') as Subject ");
            sql.AppendLine("       ,isnull(m.[Principal],0) as PrincipalID           ");
            sql.AppendLine("       ,isnull((select officedba.EmployeeInfo.EmployeeName from officedba.EmployeeInfo where  officedba.EmployeeInfo.ID=m.Principal),'') as PrincipalName    ");
            sql.AppendLine("       ,isnull(m.[DeptID],0) as DeptID                                                                                                                       ");
            sql.AppendLine("       ,isnull((select officedba.DeptInfo.DeptName from officedba.DeptInfo where officedba.DeptInfo.ID=m.DeptID),'') as DeptName                              ");
            sql.AppendLine("   FROM [officedba].[ManufactureTask] as m  ");
            sql.AppendLine("   left join officedba.ManufactureTaskDetail as md on m.TaskNo=md.TaskNo ");
            sql.AppendLine("  where   m.CompanyCD=@CompanyCD  and m.BillStatus='2'  and md.CompanyCD=@CompanyCD ");
            if (BillNo != "")
            {
                sql.AppendLine(" and m.TaskNo like @TaskNo");
                comm.Parameters.Add(SqlHelper.GetParameter("@TaskNo", "%" + BillNo + "%"));
            }
            if (BillTitle != "")
            {
                sql.AppendLine(" and m.Subject like @Title");
                comm.Parameters.Add(SqlHelper.GetParameter("@Title", "%" + BillTitle + "%"));
            }
            if (method != "2")
            {
                sql.AppendLine("   and  md.ID in (select md2.ID from officedba.ManufactureTaskDetail as md2 left join officedba.ManufactureTask as m2 on m2.TaskNo=md2.TaskNo where m2.CompanyCD=@CompanyCD and isnull(md2.ProductedCount,0)>isnull(md2.ApplyCheckCount,0) and md2.ProductedCount is not null) ");
            }
            sql.AppendLine(" order by m.ModifiedDate desc ");
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }

        public static DataTable GetManDetail(string TaskNo, string CompanyCD, string ReportStr)
        {
            SqlCommand comm = new SqlCommand();
            string[] testStr = ReportStr.Split('?');
            string ProductNo = testStr[0];
            string ProductName = testStr[1];
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" SELECT distinct m.[ID],m.ModifiedDate ");
            sql.AppendLine("        ,isnull(m.FromLineNo,'') as  FromLineNo ");
            sql.AppendLine("       ,isnull(m.[ProductID],'') as  ProductID ");
            sql.AppendLine("       ,isnull(p.ProductName,'') as  ProductName ");
            sql.AppendLine("       ,isnull(p.UnitID,'') as UnitID ");
            sql.AppendLine("       ,isnull(m.ProductedCount,0) as ProductedCount ");
            sql.AppendLine("       ,isnull(c.CodeName,'') as CodeName ");
            sql.AppendLine("       ,isnull(p.ProdNo,'') as ProNo ");
            sql.AppendLine("       ,isnull(m.CheckedCount,0) as CheckedCount");
            sql.AppendLine("       ,isnull(p.Specification,'') as Specification");
            sql.AppendLine("       ,isnull(m.ProductedCount,0)-isnull(m.ApplyCheckCount,0) as CheckCount");//报检数量
            sql.AppendLine("       ,isnull(m.ApplyCheckCount,0) as ApplyCheckCount");//已报检数量
            sql.AppendLine("   FROM [officedba].[ManufactureTaskDetail] as m left join officedba.ProductInfo as p on m.ProductID=p.ID  ");
            sql.AppendLine("   left join officedba.CodeUnitType as c on p.UnitID=c.ID where m.TaskNo=@TaskNo   ");
            sql.AppendLine("  and  m.CompanyCD=@CompanyCD  and isnull(m.ProductedCount,0)>isnull(m.ApplyCheckCount,0) and m.ProductedCount is not null");
            if (ProductNo != "")
            {
                sql.AppendLine(" and p.ProdNo like @ProdNo");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProdNo", "%" + ProductNo + "%"));
            }
            if (ProductName != "")
            {
                sql.AppendLine(" and p.ProductName  like @ProductName");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProductName", "%" + ProductName + "%"));
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@TaskNo", TaskNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);

        }
        /// <summary>
        /// 获取源单类型为质检报告的相关基本信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetReportInfo(string CompanyCD, string method, string ReportStr)
        {
            SqlCommand comm = new SqlCommand();
            string[] testStr = ReportStr.Split('?');
            string BillNo = testStr[0];
            string BillTitle = testStr[1];
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" SELECT distinct q.[ID] ");
            sql.AppendLine("        ,isnull(q.CheckStandard,'') as CheckStandard ");
            sql.AppendLine("        ,isnull(p.Specification,'') as Specification");
            sql.AppendLine("        ,q.ReportNo ");
            sql.AppendLine("        ,isnull(q.CheckType,0) as CheckType ");
            sql.AppendLine("        ,isnull((case q.CheckType when '1' then '进货检验' when '2' then '过程检验' when '3' then '最终检验' end),'') as CheckTypeName   ");
            sql.AppendLine("        ,isnull(q.Title,'') as Title ");
            sql.AppendLine("        ,isnull(q.CheckMode,0) as CheckMode ");
            sql.AppendLine("        ,isnull((case q.CheckMode when '1' then '全检' when '2' then '抽检' when '3' then '临检' end ),'')as CheckModeName               ");
            sql.AppendLine("        ,isnull(q.ApplyUserID,0)  as CheckerID ");
            sql.AppendLine(" 		,isnull((select e.EmployeeName from officedba.EmployeeInfo as e where e.ID=q.ApplyUserID),'') as CheckerName ");
            sql.AppendLine("        ,isnull((select d.DeptName from officedba.DeptInfo as d where q.ApplyDeptID=d.ID),'') as DeptName ");
            sql.AppendLine("        ,isnull(q.ApplyDeptID,0) as DeptID ");
            sql.AppendLine("        ,isnull(q.ProductID,0) as ProductID ");
            sql.AppendLine("        ,isnull(p.ProductName,'') as ProductName ");
            sql.AppendLine(" 	    ,isnull(p.ProdNo,'') as ProNo ");
            sql.AppendLine("        ,isnull(q.CheckNum,0) as CheckNum");
            sql.AppendLine("        ,isnull(p.UnitID,0) as UnitID ");
            sql.AppendLine("        ,isnull(q.FromLineNo,'0') as FromLineNo                                                                                         ");
            sql.AppendLine("        ,isnull(q.CheckContent,'') as CheckContent");
            sql.AppendLine("        ,isnull(c.CodeName,'') as CodeName ");
            sql.AppendLine("        ,isnull((select sum(CheckNum) from officedba.QualityCheckReport as m where m.ReportID=q.ID and m.BillStatus='2' and m.FromType='2'),0) as CheckedCount");
            sql.AppendLine("   FROM [officedba].[QualityCheckReport] as q left join officedba.ProductInfo as p on q.ProductID=p.ID                               ");
            sql.AppendLine("   left join officedba.CodeUnitType as c on p.UnitID=c.ID                                                                                    ");
            sql.AppendLine("   where q.CompanyCD=@CompanyCD and  q.BillStatus='2' ");
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            if (BillNo != "")
            {
                sql.AppendLine(" and q.ReportNo like @ReportNo");
                comm.Parameters.Add(SqlHelper.GetParameter("@ReportNo", "%" + BillNo + "%"));
            }
            if (BillTitle != "")
            {
                sql.AppendLine(" and q.Title like @Title");
                comm.Parameters.Add(SqlHelper.GetParameter("@Title", "%" + BillTitle + "%"));
            }
            if (method != "2")
            {
                sql.AppendLine("   and q.isRecheck=1 and isnull(q.CheckNum,0)>isnull((select sum(m1.CheckNum) from officedba.QualityCheckReport as m1 where m1.ReportID=q.ID and m1.BillStatus='2' and m1.FromType='2'),0) and q.CheckNum is not null");
            }
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);


        }

        /// <summary>
        /// 新增汇报单
        /// </summary>
        /// <param name="model">汇报单</param>
        /// <param name="detailList">明细</param>
        /// <param name="htExtAttr">扩展属性</param>
        /// <returns></returns>
        public static bool AddReport(StorageQualityCheckReportModel model, List<StorageQualityCheckReportDetailModel> detailList, Hashtable htExtAttr)
        {
            ArrayList sqlList = new ArrayList();
            #region 主表
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" INSERT INTO [officedba].[QualityCheckReport]                      ");
            sql.AppendLine("            ([CompanyCD]                                                   ");
            sql.AppendLine("            ,[ReportNo]                                                    ");
            sql.AppendLine("            ,[Title]                                                       ");
            sql.AppendLine("            ,[FromType]                                                    ");
            if (model.FromType != "0")
            {
                sql.AppendLine("            ,[ReportID]                                                ");
            }
            sql.AppendLine("            ,[FromLineNo]                                                  ");
            sql.AppendLine("            ,[CheckType]                                                   ");
            sql.AppendLine("            ,[CheckMode]                                                   ");
            sql.AppendLine("            ,[ProductID]                                                   ");
            sql.AppendLine("            ,[ApplyUserID]                                                 ");
            sql.AppendLine("            ,[ApplyDeptID]                                                 ");
            sql.AppendLine("            ,[Checker]                                                     ");
            sql.AppendLine("            ,[CheckDeptId]                                                 ");
            sql.AppendLine("            ,[CheckDate]                                                   ");
            if (model.Dept != 0)
            {
                sql.AppendLine(" ,DeptID");
            }
            if (model.Principal != 0)
            {
                sql.AppendLine(" ,Principal");
            }
            sql.AppendLine("             ,FromDetailID");
            if (!string.IsNullOrEmpty(model.CheckContent))
            {
                sql.AppendLine("            ,[CheckContent]                                                ");
            }
            if (!string.IsNullOrEmpty(model.CheckStandard))
            {
                sql.AppendLine("            ,[CheckStandard]                                               ");
            }
            sql.AppendLine("            ,[CheckNum]                                                    ");
            if (model.SampleNum != -99999)
            {
                sql.AppendLine("            ,[SampleNum]                                                   ");
            }
            //if (model.SampleBadNum != -99999)
            //{
            //    sql += "            ,[SampleBadNum]                                                ";
            //}
            //if (model.SamplePassNum != -99999)
            //{
            //    sql += "            ,[SamplePassNum]                                               ";
            //}
            sql.AppendLine("            ,[PassNum]                                                     ");
            sql.AppendLine("            ,[PassPercent]                                                 ");
            sql.AppendLine("            ,[NoPass]                                                      ");
            if (!string.IsNullOrEmpty(model.CheckResult))
            {
                sql.AppendLine("            ,[CheckResult]                                                 ");
            }
            sql.AppendLine("            ,[isPass]                                                      ");
            sql.AppendLine("            ,[isRecheck]                                                   ");
            if (!string.IsNullOrEmpty(model.Remark))
            {
                sql.AppendLine("            ,[Remark]                                                      ");
            }
            if (!string.IsNullOrEmpty(model.Attachment))
            {
                sql.AppendLine("            ,[Attachment]                                                  ");
            }
            sql.AppendLine("            ,[Creator]                                                     ");
            sql.AppendLine("            ,[CreateDate]                                                  ");
            sql.AppendLine("            ,[BillStatus]                                                  ");
            sql.AppendLine("            ,[ModifiedDate]                                                ");
            sql.AppendLine("            ,[ModifiedUserID]                                              ");
            if (!string.IsNullOrEmpty(model.OtherCorpName))
            {
                sql.AppendLine("            ,OtherCorpName                                                 ");
            }
            sql.AppendLine("            ,CorpBigType                                                   ");
            if (model.OtherCorpID != 0)
            {
                sql.AppendLine("            ,OtherCorpID                                                  ");
            }
            sql.AppendLine("            ,FromReportNo)                                                 ");
            sql.AppendLine("     VALUES                                                                ");
            sql.AppendLine("            (@CompanyCD                                                    ");
            sql.AppendLine("            ,@ReportNo                                                     ");
            sql.AppendLine("            ,@Title                                                        ");
            sql.AppendLine("            ,@FromType                                                     ");
            if (model.FromType != "0")
            {
                sql.AppendLine("            ,@ReportID                                                 ");
            }
            sql.AppendLine("            ,@FromLineNo                                                   ");
            sql.AppendLine("            ,@CheckType                                                    ");
            sql.AppendLine("            ,@CheckMode                                                    ");
            sql.AppendLine("            ,@ProductID                                                    ");
            sql.AppendLine("            ,@ApplyUserID                                                  ");
            sql.AppendLine("            ,@ApplyDeptID                                                  ");
            sql.AppendLine("            ,@Checker                                                      ");
            sql.AppendLine("            ,@CheckDeptId                                                   ");
            sql.AppendLine("            ,@CheckDate                                                    ");
            if (model.Dept != 0)
            {
                sql.AppendLine(" ,@DeptID");
            }
            if (model.Principal != 0)
            {
                sql.AppendLine(" ,@Principal");
            }
            sql.AppendLine("            ,@FromDetailID");

            if (!string.IsNullOrEmpty(model.CheckContent))
            {
                sql.AppendLine("            ,@CheckContent                                                 ");
            }
            if (!string.IsNullOrEmpty(model.CheckStandard))
            {
                sql.AppendLine("            ,@CheckStandard                                                ");
            }
            sql.AppendLine("            ,@CheckNum                                                     ");
            if (model.SampleNum != -99999)
            {
                sql.AppendLine("            ,@SampleNum                                                    ");
            }
            sql.AppendLine("            ,@PassNum                                                      ");
            sql.AppendLine("            ,@PassPercent                                                  ");
            sql.AppendLine("            ,@NoPass                                                       ");
            if (!string.IsNullOrEmpty(model.CheckResult))
            {
                sql.AppendLine("            ,@CheckResult                                                  ");
            }
            sql.AppendLine("            ,@isPass                                                       ");
            sql.AppendLine("            ,@isRecheck                                                    ");
            if (!string.IsNullOrEmpty(model.Remark))
            {
                sql.AppendLine("            ,@Remark                                                       ");
            }
            if (!string.IsNullOrEmpty(model.Attachment))
            {
                sql.AppendLine("            ,@Attachment                                                   ");
            }
            sql.AppendLine("            ,@Creator                                                      ");
            sql.AppendLine("            ,@CreateDate                                                   ");
            sql.AppendLine("            ,@BillStatus                                                   ");
            sql.AppendLine("            ,@ModifiedDate                                                 ");
            sql.AppendLine("            ,@ModifiedUserID                                               ");

            if (!string.IsNullOrEmpty(model.OtherCorpName))
            {
                sql.AppendLine("      ,@OtherCorpName           ");
            }
            sql.AppendLine("       ,@CorpBigType              ");
            if (model.OtherCorpID != 0)
            {
                sql.AppendLine("       ,@OtherCorpID               ");
            }
            sql.AppendLine("       ,@FromReportNo)             ");
            sql.AppendLine("set @ID=@@IDENTITY                                                         ");
            SqlParameter[] parma = new SqlParameter[1];
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql.ToString();
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@ReportNo", model.ReportNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@Title", model.Title));
            comm.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
            if (model.FromType != "0")
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@ReportID", model.ReportID));
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@FromLineNo", model.FromLineNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@CheckType", model.CheckType));
            comm.Parameters.Add(SqlHelper.GetParameter("@CheckMode", model.CheckMode));
            if (model.Dept != 0)
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.Dept));
            }
            if (model.Principal != 0)
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@Principal", model.Principal));
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", model.ProductID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ApplyUserID", model.ApplyUserID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ApplyDeptID", model.ApplyDeptID));
            comm.Parameters.Add(SqlHelper.GetParameter("@Checker", model.Checker));

            comm.Parameters.Add(SqlHelper.GetParameter("@CheckDeptId", model.CheckDeptId));
            comm.Parameters.Add(SqlHelper.GetParameter("@CheckDate", model.CheckDate));
            comm.Parameters.Add(SqlHelper.GetParameter("@FromDetailID", model.FromDetailID));
            if (!string.IsNullOrEmpty(model.CheckContent))
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@CheckContent", model.CheckContent));
            }
            if (!string.IsNullOrEmpty(model.CheckStandard))
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@CheckStandard", model.CheckStandard));
            }

            comm.Parameters.Add(SqlHelper.GetParameter("@CheckNum", model.CheckNum));
            if (model.SampleNum != -99999)
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@SampleNum", model.SampleNum));
            }
            //if (model.SampleBadNum != -99999)
            //{
            //    comm.Parameters.Add(SqlHelper.GetParameter("@SampleBadNum", model.SampleBadNum));
            //}
            //if (model.SamplePassNum != -99999)
            //{
            //    comm.Parameters.Add(SqlHelper.GetParameter("@SamplePassNum", model.SamplePassNum));
            //}
            comm.Parameters.Add(SqlHelper.GetParameter("@PassNum", model.PassNum));
            comm.Parameters.Add(SqlHelper.GetParameter("@PassPercent", model.PassPercent));
            comm.Parameters.Add(SqlHelper.GetParameter("@NoPass", model.NoPass));
            comm.Parameters.Add(SqlHelper.GetParameter("@CheckResult", model.CheckResult));
            comm.Parameters.Add(SqlHelper.GetParameter("@isPass", model.isPass));
            comm.Parameters.Add(SqlHelper.GetParameter("@isRecheck", model.isRecheck));
            if (!string.IsNullOrEmpty(model.Remark))
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
            }
            if (!string.IsNullOrEmpty(model.Attachment))
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@Attachment", model.Attachment));
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@Creator", model.Creator));
            comm.Parameters.Add(SqlHelper.GetParameter("@CreateDate", model.CreateDate));
            comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", model.BillStatus));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", model.ModifiedDate));

            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
            if (!string.IsNullOrEmpty(model.OtherCorpName))
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@OtherCorpName", model.OtherCorpName));
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@CorpBigType", model.CorpBigType));
            comm.Parameters.Add(SqlHelper.GetParameter("@FromReportNo", model.FromReportNo));
            if (model.OtherCorpID != 0)
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@OtherCorpID", model.OtherCorpID));
            }
            comm.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));
            sqlList.Add(comm);
            // 更新扩展属性
            SqlCommand commExtAttr = UpdateExtAttr(model, htExtAttr);
            if (commExtAttr != null)
            {
                sqlList.Add(commExtAttr);
            }
            #endregion

            #region 明细Start---------
            StorageQualityCheckReportDetailModel detailmodel = new StorageQualityCheckReportDetailModel();

            if (!string.IsNullOrEmpty(model.ReportNo))
            {
                for (int i = 0; i < detailList.Count; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("INSERT INTO [officedba].[CheckReportDetail]");
                    sb.AppendLine("([ReportNo],[SortNo],[CheckItem] ");
                    if (!string.IsNullOrEmpty(detailList[i].CheckValue))
                    {
                        sb.AppendLine(" ,[CheckValue]");
                    }
                    sb.AppendLine(" ,[CheckNum]");
                    if (!string.IsNullOrEmpty(detailList[i].StandardValue))
                    {
                        sb.AppendLine(",[StandardValue]");
                    }
                    sb.AppendLine(",[isPass],[Checker],[CheckDeptID],[ModifiedDate],[ModifiedUserID]");
                    if (!string.IsNullOrEmpty(detailList[i].CheckResult))
                    {
                        sb.AppendLine(",[CheckResult]");
                    }
                    if (!string.IsNullOrEmpty(detailList[i].CheckStandard))
                    {
                        sb.AppendLine(",[CheckStandard]");
                    }
                    if (!string.IsNullOrEmpty(detailList[i].LowerLimit))
                    {
                        sb.AppendLine(",[LowerLimit]");
                    }
                    if (!string.IsNullOrEmpty(detailList[i].NormUpLimit))
                    {
                        sb.AppendLine(",[NormUpLimit]");
                    }
                    if (!string.IsNullOrEmpty(detailList[i].Remark))
                    {
                        sb.AppendLine(",[Remark]");
                    }
                    if (detailList[i].PassNum != -1)
                    {

                        sb.AppendLine(",[PassNum]");
                    }
                    if (detailList[i].NotPassNum != -1)
                    {
                        sb.AppendLine(",[NotPassNum]");
                    }
                    //if (detailList[i].BadNum != -1)
                    //{
                    //    sb.AppendLine(",[BadNum]");
                    //}
                    sb.AppendLine(" ,[CompanyCD])");
                    sb.AppendLine("  values(");
                    sb.AppendLine(" @ReportNo,@SortNo,@CheckItem");
                    if (!string.IsNullOrEmpty(detailList[i].CheckValue))
                    {
                        sb.AppendLine(",@CheckValue");
                    }
                    sb.AppendLine(",@CheckNum");
                    if (!string.IsNullOrEmpty(detailList[i].StandardValue))
                    {
                        sb.AppendLine(",@StandardValue");
                    }
                    sb.AppendLine(",@isPass,@Checker,@CheckDeptID,@ModifiedDate,@ModifiedUserID");
                    if (!string.IsNullOrEmpty(detailList[i].CheckResult))
                    {
                        sb.AppendLine("         ,@CheckResult");
                    }
                    if (!string.IsNullOrEmpty(detailList[i].CheckStandard))
                    {
                        sb.AppendLine("            ,@CheckStandard");
                    }
                    if (!string.IsNullOrEmpty(detailList[i].LowerLimit))
                    {
                        sb.AppendLine("           ,@LowerLimit");
                    }
                    if (!string.IsNullOrEmpty(detailList[i].NormUpLimit))
                    {
                        sb.AppendLine("            ,@NormUpLimit");
                    }
                    if (!string.IsNullOrEmpty(detailList[i].Remark))
                    {
                        sb.AppendLine("       , @Remark");
                    }
                    if (detailList[i].PassNum != -1)
                    {
                        sb.AppendLine("               ,@PassNum");
                    }
                    if (detailList[i].NotPassNum != -1)
                    {
                        sb.AppendLine("           ,@NotPassNum");
                    }
                    //if (detailList[i].BadNum != -1)
                    //{
                    //    sb.AppendLine("                ,@BadNum");
                    //}
                    sb.AppendLine("  ,@CompanyCD)");
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.CommandText = sb.ToString();
                    sqlcomm.Parameters.Add(SqlHelper.GetParameter("@ReportNo", model.ReportNo));
                    sqlcomm.Parameters.Add(SqlHelper.GetParameter("@SortNo", detailList[i].SortNo));
                    sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CheckItem", detailList[i].CheckItem));
                    if (!string.IsNullOrEmpty(detailList[i].CheckValue))
                    {
                        sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CheckValue", detailList[i].CheckValue));
                    }
                    sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CheckNum", detailList[i].CheckNum));
                    if (!string.IsNullOrEmpty(detailList[i].StandardValue))
                    {
                        sqlcomm.Parameters.Add(SqlHelper.GetParameter("@StandardValue", detailList[i].StandardValue));
                    }
                    sqlcomm.Parameters.Add(SqlHelper.GetParameter("@isPass", detailList[i].isPass));
                    sqlcomm.Parameters.Add(SqlHelper.GetParameter("@Checker", detailList[i].Checker));
                    sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CheckDeptID", detailList[i].CheckDeptID));
                    sqlcomm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", model.ModifiedDate));
                    sqlcomm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
                    if (!string.IsNullOrEmpty(detailList[i].CheckResult))
                    {
                        sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CheckResult", detailList[i].CheckResult));
                    }
                    if (!string.IsNullOrEmpty(detailList[i].CheckStandard))
                    {
                        sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CheckStandard", detailList[i].CheckStandard));
                    }
                    if (!string.IsNullOrEmpty(detailList[i].LowerLimit))
                    {
                        sqlcomm.Parameters.Add(SqlHelper.GetParameter("@LowerLimit", detailList[i].LowerLimit));
                    }
                    if (!string.IsNullOrEmpty(detailList[i].NormUpLimit))
                    {
                        sqlcomm.Parameters.Add(SqlHelper.GetParameter("@NormUpLimit", detailList[i].NormUpLimit));
                    }
                    if (!string.IsNullOrEmpty(detailList[i].Remark))
                    {
                        sqlcomm.Parameters.Add(SqlHelper.GetParameter("@Remark", detailList[i].Remark));
                    }
                    if (detailList[i].PassNum != -1)
                    {
                        sqlcomm.Parameters.Add(SqlHelper.GetParameter("@PassNum", detailList[i].PassNum));
                    }
                    if (detailList[i].NotPassNum != -1)
                    {
                        sqlcomm.Parameters.Add(SqlHelper.GetParameter("@NotPassNum", detailList[i].NotPassNum));
                    }
                    //if (detailList[i].BadNum != -1)
                    //{
                    //    sqlcomm.Parameters.Add(SqlHelper.GetParameter("@BadNum", detailList[i].BadNum));
                    //}
                    sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                    sqlList.Add(sqlcomm);

                }

            }
            #endregion
            if (SqlHelper.ExecuteTransWithArrayList(sqlList))
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
        /// <param name="model">质检汇报单</param>
        /// <param name="htExtAttr">扩展属性</param>
        /// <returns></returns>
        public static SqlCommand UpdateExtAttr(StorageQualityCheckReportModel model, Hashtable htExtAttr)
        {
            SqlCommand sqlcomm = new SqlCommand();
            if (htExtAttr == null || htExtAttr.Count < 1)
            {// 没有属性需要修改
                return null;
            }

            StringBuilder sb = new StringBuilder(" UPDATE officedba.QualityCheckReport SET ");
            foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
            {
                sb.AppendFormat(" {0}=@{0},", de.Key.ToString());
                sqlcomm.Parameters.Add(SqlHelper.GetParameter(String.Format("@{0}", de.Key.ToString()), de.Value));
            }
            string strSql = sb.ToString();
            strSql = strSql.TrimEnd(',');
            strSql += " WHERE CompanyCD = @CompanyCD  AND ReportNo = @ReportNo ";
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@ReportNo", model.ReportNo));
            sqlcomm.CommandText = strSql;

            return sqlcomm;
        }


        /// <summary>
        /// 检索需要
        /// </summary>
        /// <param name="model"></param>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="FlowStatus"></param>
        /// <returns></returns>
        public static DataTable GetAllReport(StorageQualityCheckReportModel model, string BeginTime, string EndTime, string FlowStatus, string EFIndex, string EFDesc, ref int TotalCount)
        {
            int BillTypeFlag = int.Parse(ConstUtil.BILL_TYPECODE_STORAGE_QUALITY);
            int BillTypeCode = int.Parse(ConstUtil.BILL_TYPECODE_STORAGE_REPORT);
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select * from ( ");
            sql.AppendLine(" SELECT     q.ReportNo,q.ModifiedDate,q.Title,isnull(q.OtherCorpName,'') as OtherCorpName,isnull(d.DeptName,'') as DeptName,q.BillStatus as BillStatusID,  ");
            sql.AppendLine("                       CASE CorpBigType WHEN '1' THEN '客户' WHEN '2' THEN '供应商' WHEN '3' THEN '竞争对手' WHEN '4' THEN '银行' WHEN '5' THEN '外协加工厂' WHEN '6' THEN '运输商' WHEN '7' THEN '其他' ELSE '' END AS BigTypeName,    ");
            sql.AppendLine("                       CASE FromType WHEN '0' THEN '无来源' WHEN '1' THEN '质检申请单' WHEN '2' THEN '质检报告单' WHEN '3' THEN '生产任务单' WHEN '4' THEN '采购到货单' ELSE '' END AS FromTypeName,                                ");
            sql.AppendLine("                       CASE CheckMode WHEN '1' THEN '全检' WHEN '2' THEN '抽检' WHEN '3' THEN '临检' ELSE '空' END AS CheckModeName, ");
            sql.AppendLine("                       CASE CheckType WHEN '1' THEN '进货检验' WHEN '2' THEN '过程检验' WHEN '3' THEN '最终检验' ELSE '' END AS CheckTypeName,isnull(e.EmployeeName,'') as EmployeeName, ");
            sql.AppendLine("                       CASE f.FlowStatus WHEN '1' THEN '待审批' WHEN '2' THEN '审批中' WHEN '3' THEN '审批通过' WHEN '4' THEN '审批不通过' when '5' then '撤消审批' ELSE '' END AS FlowStatus,isnull(f.FlowStatus,'0') as FlowStatusID, ");
            sql.AppendLine("                       CASE q.BillStatus WHEN '1' THEN '制单' WHEN '2' THEN '执行' WHEN '3' THEN '变更' WHEN '4' THEN '手工结单' WHEN '5' THEN '自动结单' ELSE '' END AS BillStatus, ");
            sql.AppendLine("                       isnull(substring(CONVERT(varchar,q.CheckDate,120),0,11),'') as CheckDate, q.ID ");
            sql.AppendLine(" FROM         officedba.QualityCheckReport AS q LEFT  JOIN ");
            sql.AppendLine("                       officedba.FlowInstance AS f ON q.CompanyCD = f.CompanyCD AND f.BillTypeFlag = " + BillTypeFlag + " AND f.BillTypeCode =" + BillTypeCode + " AND ");
            sql.AppendLine("                       q.ID = f.BillID  ");
            sql.AppendLine("   and f.ID=(select max(ID) from officedba.FlowInstance where q.CompanyCD = officedba.FlowInstance.CompanyCD AND officedba.FlowInstance.BillTypeFlag = " + BillTypeFlag + " AND officedba.FlowInstance.BillTypeCode = " + BillTypeCode + " AND  q.ID = officedba.FlowInstance.BillID) ");
            sql.AppendLine("              LEFT JOIN     officedba.EmployeeInfo AS e ON q.ApplyUserID = e.ID LEFT  JOIN ");
            sql.AppendLine("                       officedba.DeptInfo AS d ON q.ApplyDeptID = d.ID ");
            sql.AppendLine(" where q.CompanyCD=@CompanyCD  ");
            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            if (!string.IsNullOrEmpty(model.ReportNo))
            {
                sql.AppendLine(" and q.ReportNo like @ReportNo");
                comm.Parameters.Add(SqlHelper.GetParameter("@ReportNo", "%" + model.ReportNo + "%"));
            }
            if (!string.IsNullOrEmpty(model.Title))
            {
                sql.AppendLine(" and q.Title like @Title");
                comm.Parameters.Add(SqlHelper.GetParameter("@Title", "%" + model.Title + "%"));
            }
            if (model.Checker > 0)
            {
                sql.AppendLine(" and q.ApplyUserID=@Checker");
                comm.Parameters.Add(SqlHelper.GetParameter("@Checker", model.Checker));

            }
            if (!string.IsNullOrEmpty(model.FromReportNo))
            {
                sql.AppendLine(" and q.FromReportNo like @FromReportNo");
                comm.Parameters.Add(SqlHelper.GetParameter("@FromReportNo", "%" + model.FromReportNo + "%"));
            }
            if (model.ApplyDeptID > 0)
            {
                sql.AppendLine(" and q.ApplyDeptID=@ApplyDeptID");
                comm.Parameters.Add(SqlHelper.GetParameter("@ApplyDeptID", model.ApplyDeptID));
            }
            if (!string.IsNullOrEmpty(BeginTime))
            {
                sql.AppendLine(" and q.CheckDate>=@BeginTime");
                comm.Parameters.Add(SqlHelper.GetParameter("@BeginTime", BeginTime));
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                sql.AppendLine(" and q.CheckDate<=@EndTime");
                comm.Parameters.Add(SqlHelper.GetParameter("@EndTime", EndTime));
            }

            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                sql.AppendLine(" and q.ExtField" + EFIndex + " LIKE @EFDesc ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@EFDesc", "%" + EFDesc + "%"));
            }

            if (model.FromType != "00")
            {
                sql.AppendLine(" and q.FromType=@FromType");
                comm.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
            }
            if (model.CheckType != "00")
            {
                sql.AppendLine(" and q.CheckType=@CheckType");
                comm.Parameters.Add(SqlHelper.GetParameter("@CheckType", model.CheckType));
            }
            if (model.BillStatus != "00")
            {
                sql.AppendLine(" and q.BillStatus=@BillStatus");
                comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", model.BillStatus));
            }
            if (model.CheckMode != "00")
            {
                sql.AppendLine(" and q.CheckMode=@CheckMode");
                comm.Parameters.Add(SqlHelper.GetParameter("@CheckMode", model.CheckMode));
            }
            if (model.ApplyUserID > 0)
            {

                sql.AppendLine(" and q.ApplyUserID=@ApplyUserID");
                comm.Parameters.Add(SqlHelper.GetParameter("@ApplyUserID", model.ApplyUserID));
            }
            if (FlowStatus != "00" && FlowStatus != "6")
            {
                sql.AppendLine(" and f.FlowStatus=@FlowStatus");
                comm.Parameters.Add(SqlHelper.GetParameter("@FlowStatus", FlowStatus));
            }
            sql.AppendLine(" ) as Info ");
            if (FlowStatus == "6")
            {
                sql.AppendLine(" where  FlowStatusID=@FlowStatus1");
                comm.Parameters.Add(SqlHelper.GetParameter("@FlowStatus1", "0"));
            }
            if (model.Creator == -100)
            {
                sql.AppendLine(" order by " + model.Attachment);
            }
            comm.CommandText = sql.ToString();
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
        /// <summary>
        /// 删除报告
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DelReport(string ID, string CompanyCD)
        {
            ArrayList listADD = new ArrayList();
            string[] arrID = ID.Split(',');
            if (arrID.Length > 0)
            {
                for (int i = 1; i < arrID.Length; i++)
                {
                    StringBuilder sqlDet = new StringBuilder();
                    StringBuilder sqlBom = new StringBuilder();
                    sqlDet.AppendLine("delete from officedba.CheckReportDetail where CompanyCD=@CompanyCD and ReportNo=(select top 1 ReportNo from officedba.QualityCheckReport where CompanyCD=@CompanyCD and ID=@ID)");
                    sqlBom.AppendLine("delete from officedba.QualityCheckReport where CompanyCD=@CompanyCD and ID=@ID");

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


        public static DataTable GetReportInfo(StorageQualityCheckReportModel model)
        {
            #region 查询语句
            //查询SQL拼写
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" SELECT     q.ID,q.ReportNo, q.Title, isnull(q.OtherCorpName,'') as OtherCorpName,isnull(q.OtherCorpID,0) as OtherCorpID,isnull(q.ReportName,'') as ReportName,isnull(CONVERT(varchar,q.CreateDate,120),'') as CreateDate,isnull(q.FromReportNo,'') as FromReportNo,isnull(q.CorpBigType,'0') as CorpBigType,                                                                        ");
            sql.AppendLine("       Replace(q.[Attachment],'\\',',') as Attachment,isnull(q.FromDetailID,0) as FromDetailID,  ");
            sql.AppendLine(" CASE CorpBigType WHEN '1' THEN '客户' WHEN '2' THEN '供应商' WHEN '3' THEN '竞争对手' WHEN '4' THEN '银行' WHEN '5' THEN '外协加工厂'                                                                           ");
            sql.AppendLine(" WHEN   '6' THEN '运输商' WHEN '7' THEN '其他' END AS BigTypeName,       q.FromType,    q.CheckMode,  q.CheckType,isnull(CONVERT(varchar,q.CheckDate,120),'') as CheckDate,                                                                 ");
            sql.AppendLine(" (select EmployeeName from officedba.EmployeeInfo where officedba.EmployeeInfo.ID=q.Checker) as  EmployeeName ,                                                    ");
            sql.AppendLine(" CASE q.BillStatus WHEN '1' THEN '制单' WHEN '2' THEN '执行' WHEN '3' THEN '变更' WHEN '4' THEN '手工结单' WHEN '5' THEN '自动结单' END AS BillStatus,                                                           ");
            sql.AppendLine(" (select DeptName from officedba.DeptInfo where officedba.DeptInfo.ID=q.ApplyDeptID) as ApplyDeptIDName,                                                                                                         ");
            sql.AppendLine(" (select EmployeeName from officedba.EmployeeInfo where officedba.EmployeeInfo.ID=q.ApplyUserID) as ApplyUserIDName,                                                                                             ");
            sql.AppendLine(" q.FromType, q.ReportID,isnull(q.FromLineNo,'0') as FromLineNo, q.ProductID, isnull(q.ApplyUserID,0) as ApplyUserID,isnull(q.ApplyDeptID,0) as ApplyDeptID,isnull(q.Checker,0) as Checker,isnull(q.CheckDeptId,0) as CheckDeptId,                                                               ");
            sql.AppendLine(" isnull(q.CheckContent,'') as CheckContent,isnull(q.CheckStandard,'') as CheckStandard,convert(numeric(12,2),isnull(q.CheckNum,0)) as CheckNum,convert(numeric(12,2),isnull(q.SampleNum,0)) as SampleNum,convert(numeric(12,2),isnull(q.PassNum,0)) as PassNum,                                                                                                           ");
            sql.AppendLine(" (select EmployeeName from officedba.EmployeeInfo where officedba.EmployeeInfo.ID=q.Creator) as CreatorName,   (select EmployeeName from officedba.EmployeeInfo where officedba.EmployeeInfo.ID=q.Closer) as CloserName,              ");
            sql.AppendLine(" (q.ModifiedUserID) as ModifiedUserIDName,           ");
            sql.AppendLine(" (select ProductName from officedba.ProductInfo where officedba.ProductInfo.ID=q.ProductID) as ProductName,");
            sql.AppendLine(" (select ProdNo from officedba.ProductInfo where officedba.ProductInfo.ID=q.ProductID) as ProdNo,");
            sql.AppendLine(" (select UnitID from officedba.ProductInfo where officedba.ProductInfo.ID=q.ProductID) as UnitID,");
            sql.AppendLine(" (select CodeName from officedba.CodeUnitType where officedba.CodeUnitType.ID=(select UnitID from officedba.ProductInfo where officedba.ProductInfo.ID=q.ProductID)) as CodeName, ");
            sql.AppendLine(" (select DeptName from officedba.DeptInfo where officedba.DeptInfo.ID=q.CheckDeptId) as DeptName,");
            sql.AppendLine(" (select EmployeeName from officedba.EmployeeInfo where officedba.EmployeeInfo.ID=q.Confirmor) as ConfirmorName,convert(numeric(12,2),isnull(q.PassPercent,0)) as PassPercent,convert(numeric(12,2),isnull(q.NoPass,0)) as NoPass, q.CheckResult, q.isPass, q.isRecheck,                                 ");
            sql.AppendLine(" isnull(q.Remark,'') as Remark,q.Creator,q.Confirmor,isnull(CONVERT(varchar,q.ConfirmDate,120),'') as ConfirmDate,q.Closer,isnull(CONVERT(varchar,q.CloseDate,120),'') as CloseDate,isnull(CONVERT(varchar,q.ModifiedDate,120),'') as ModifiedDate,q.ModifiedUserID,q.BillStatus as BillStatusID,isnull(q.Principal,0)as Principal,isnull(q.DeptID,0) as TheDeptID, ");
            sql.AppendLine(" (select Specification from officedba.ProductInfo where officedba.ProductInfo.ID=q.ProductID) as Specification, ");
            sql.AppendLine(" (select DeptName from officedba.DeptInfo where officedba.DeptInfo.ID=q.DeptID) as TheDeptName,");
            sql.AppendLine(" (select EmployeeName from officedba.EmployeeInfo where officedba.EmployeeInfo.ID=q.Principal) as PrincipalName, ");
            sql.AppendLine(" q.ExtField1,q.ExtField2,q.ExtField3,q.ExtField4,q.ExtField5,q.ExtField6,q.ExtField7,q.ExtField8,q.ExtField9,q.ExtField10 ");
            sql.AppendLine(" FROM         officedba.QualityCheckReport AS q                                                                             ");
            sql.AppendLine("  where q.CompanyCD=@CompanyCD and q.ID=@ID ");

            #endregion

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
        /// 获取质检报告明细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetReportDetailInfo(StorageQualityCheckReportModel model)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select * from (");
            sql.AppendLine("SELECT     c.ID as DetailID,c.CheckStandard, e.EmployeeName, d.DeptName, c.SortNo, c.CheckItem, isnull(c.CheckValue,'') as CheckValue, c.CheckNum, c.PassNum, c.NotPassNum,");
            sql.AppendLine("                      isnull(c.StandardValue,'') as StandardValue, c.NormUpLimit, c.LowerLimit, isnull(c.CheckResult,'') as CheckResult, c.isPass, c.Checker, c.CheckDeptID, c.Remark, c.ModifiedDate, c.ModifiedUserID  ");
            sql.AppendLine("FROM                  officedba.CheckReportDetail AS c LEFT  JOIN                                                                                                      ");
            sql.AppendLine("                      officedba.EmployeeInfo AS e ON c.Checker = e.ID left JOIN                                                                                   ");
            sql.AppendLine("                      officedba.DeptInfo AS d ON c.CheckDeptID = d.ID                                                                                              ");
            sql.AppendLine(" where c.ReportNo=(select top 1 ReportNo from officedba.QualityCheckReport where CompanyCD=@CompanyCD and ID=@ID) and c.CompanyCD=@CompanyCD");
            sql.AppendLine(") as info ");
            //sql += " where CompanyCD=@CompanyCD";

            #endregion

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
        /// 更新汇报单
        /// </summary>
        /// <param name="model">汇报单</param>
        /// <param name="detailList">明细</param>
        /// <param name="SortID"></param>
        /// <param name="htExtAttr">扩展属性</param>
        /// <returns></returns>
        public static bool UpdateReport(StorageQualityCheckReportModel model, List<StorageQualityCheckReportDetailModel> detailList, string[] SortID, Hashtable htExtAttr)
        {
            string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//[待修改]
            ArrayList sqllist = new ArrayList();
            //string loginUserID = "admin123";
            if (string.IsNullOrEmpty(model.ReportNo))
            {
                return false;
            }
            #region  基本信息
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE [officedba].[QualityCheckReport]  ");
            sql.AppendLine("    SET [CompanyCD] = @CompanyCD                  ");
            sql.AppendLine("       ,[ReportNo] =@ReportNo                     ");
            sql.AppendLine("       ,[Title] = @Title                          ");
            sql.AppendLine("       ,[FromType] = @FromType                    ");
            sql.AppendLine("       ,[ReportID] = @ReportID                    ");
            sql.AppendLine("       ,[FromLineNo] = @FromLineNo                ");
            sql.AppendLine("       ,[CheckType] = @CheckType                  ");
            sql.AppendLine("       ,[CheckMode] = @CheckMode                  ");
            sql.AppendLine("       ,[ProductID] = @ProductID                  ");
            sql.AppendLine("       ,[ApplyUserID] =@ApplyUserID               ");
            sql.AppendLine("       ,[ApplyDeptID] =@ApplyDeptID               ");
            sql.AppendLine("       ,[Checker] =@Checker                       ");
            sql.AppendLine("       ,[CheckDeptId] = @CheckDeptId              ");
            sql.AppendLine("       ,[CheckDate] =@CheckDate                   ");

            sql.AppendLine("       ,[CheckContent] = @CheckContent            ");

            sql.AppendLine("       ,[CheckNum] =@CheckNum                     ");
            if (model.SampleNum != -99999)
            {
                sql.AppendLine("       ,[SampleNum] =@SampleNum                   ");
            }
            sql.AppendLine("       ,[PassNum] =@PassNum                       ");
            sql.AppendLine("       ,[PassPercent] =@PassPercent               ");
            sql.AppendLine("       ,[NoPass] =@NoPass                         ");
            sql.AppendLine("       ,[isPass] =@isPass                         ");
            sql.AppendLine("       ,[isRecheck] =@isRecheck                   ");
            sql.AppendLine("       ,FromDetailID=@FromDetailID");

            sql.AppendLine(" ,DeptID=@DeptID");


            sql.AppendLine(" ,Principal=@Principal");

            sql.AppendLine("       ,[BillStatus] = @BillStatus                ");
            sql.AppendLine("       ,[ModifiedDate] =@ModifiedDate             ");
            sql.AppendLine("       ,[ModifiedUserID] = @ModifiedUserID        ");

            sql.AppendLine("       ,[OtherCorpName] =@OtherCorpName           ");

            sql.AppendLine("       ,[CorpBigType] = @CorpBigType              ");
            sql.AppendLine("       ,[FromReportNo] =@FromReportNo             ");
            sql.AppendLine("       ,[OtherCorpID] =@OtherCorpID               ");

            sql.AppendLine("       ,[Remark] = @Remark                        ");



            sql.AppendLine("       ,[Attachment] =@Attachment                 ");


            sql.AppendLine("       ,[CheckStandard] =@CheckStandard           ");

            sql.AppendLine("       ,[CheckResult] = @CheckResult              ");


            sql.AppendLine(" where ID=@ID and CompanyCD=@CompanyCD ");

            SqlCommand comm = new SqlCommand();

            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@ReportNo", model.ReportNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@Title", model.Title));
            comm.Parameters.Add(SqlHelper.GetParameter("@FromType", model.FromType));
            comm.Parameters.Add(SqlHelper.GetParameter("@ReportID", model.ReportID));
            comm.Parameters.Add(SqlHelper.GetParameter("@FromLineNo", model.FromLineNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@CheckType", model.CheckType));
            comm.Parameters.Add(SqlHelper.GetParameter("@CheckMode", model.CheckMode));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", model.ProductID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ApplyUserID", model.ApplyUserID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ApplyDeptID", model.ApplyDeptID));
            comm.Parameters.Add(SqlHelper.GetParameter("@Checker", model.Checker));
            comm.Parameters.Add(SqlHelper.GetParameter("@CheckDeptId", model.CheckDeptId));
            comm.Parameters.Add(SqlHelper.GetParameter("@CheckDate", model.CheckDate));

            comm.Parameters.Add(SqlHelper.GetParameter("@CheckContent", model.CheckContent));

            comm.Parameters.Add(SqlHelper.GetParameter("@CheckNum", model.CheckNum));
            if (model.SampleNum != -99999)
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@SampleNum", model.SampleNum));//
            }
            //comm.Parameters.Add(SqlHelper.GetParameter("@SampleBadNum", model.SampleBadNum));
            //comm.Parameters.Add(SqlHelper.GetParameter("@SamplePassNum", model.SamplePassNum));
            comm.Parameters.Add(SqlHelper.GetParameter("@PassNum", model.PassNum));
            comm.Parameters.Add(SqlHelper.GetParameter("@PassPercent", model.PassPercent));
            comm.Parameters.Add(SqlHelper.GetParameter("@FromDetailID", model.FromDetailID));
            comm.Parameters.Add(SqlHelper.GetParameter("@NoPass", model.NoPass));
            comm.Parameters.Add(SqlHelper.GetParameter("@isPass", model.isPass));
            comm.Parameters.Add(SqlHelper.GetParameter("@isRecheck", model.isRecheck));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", model.ModifiedDate));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
            //comm.Parameters.Add(SqlHelper.GetParameter("@OtherCorpNo", model.OtherCorpNo));

            comm.Parameters.Add(SqlHelper.GetParameter("@OtherCorpName", model.OtherCorpName));

            comm.Parameters.Add(SqlHelper.GetParameter("@CorpBigType", model.CorpBigType));
            comm.Parameters.Add(SqlHelper.GetParameter("@FromReportNo", model.FromReportNo));
            //  comm.Parameters.Add(SqlHelper.GetParameter("@ReportName", model.ReportName));
            comm.Parameters.Add(SqlHelper.GetParameter("@OtherCorpID", model.OtherCorpID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));

            comm.Parameters.Add(SqlHelper.GetParameter("@DeptID", model.Dept));


            comm.Parameters.Add(SqlHelper.GetParameter("@Principal", model.Principal));

            if (model.BillStatus == "2")
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", "3"));
            }
            else
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", model.BillStatus));
            }

            comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));



            comm.Parameters.Add(SqlHelper.GetParameter("@Attachment", model.Attachment));


            comm.Parameters.Add(SqlHelper.GetParameter("@CheckStandard", model.CheckStandard));


            comm.Parameters.Add(SqlHelper.GetParameter("@CheckResult", model.CheckResult));

            comm.CommandText = sql.ToString();
            sqllist.Add(comm);

            // 更新扩展属性
            SqlCommand commExtAttr = UpdateExtAttr(model, htExtAttr);
            if (commExtAttr != null)
            {
                sqllist.Add(commExtAttr);
            }
            #endregion

            #region  明细
            if (SortID.Length > 0)
            {
                string delsql = "delete officedba.CheckReportDetail where CompanyCD=@CompanyCD and ReportNo=@ReportNo ";
                SqlCommand delcomm = new SqlCommand();
                delcomm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                delcomm.Parameters.Add(SqlHelper.GetParameter("@ReportNo", model.ReportNo));
                delcomm.CommandText = delsql;
                sqllist.Add(delcomm);
                if (SortID[0] != null && SortID[0] != "")
                {
                    for (int i = 0; i < detailList.Count; i++)
                    {
                        StringBuilder sbsql = new StringBuilder();
                        sbsql.AppendLine("INSERT INTO [officedba].[CheckReportDetail]");
                        sbsql.AppendLine("([ReportNo],[SortNo],[CheckItem]");
                        if (!string.IsNullOrEmpty(detailList[i].CheckValue))
                        {
                            sbsql.AppendLine(",[CheckValue]");
                        }
                        sbsql.AppendLine(",[CheckNum]");
                        if (!string.IsNullOrEmpty(detailList[i].StandardValue))
                        {
                            sbsql.AppendLine(",[StandardValue]");
                        }
                        sbsql.AppendLine(",[isPass],[Checker],[CheckDeptID],[ModifiedDate],[ModifiedUserID]");
                        if (!string.IsNullOrEmpty(detailList[i].NormUpLimit))
                        {
                            sbsql.AppendLine(",[NormUpLimit]");
                        }
                        if (!string.IsNullOrEmpty(detailList[i].LowerLimit))
                        {
                            sbsql.AppendLine(",[LowerLimit]");
                        }
                        if (!string.IsNullOrEmpty(detailList[i].CheckStandard))
                        {
                            sbsql.AppendLine(",[CheckStandard]");
                        }
                        if (!string.IsNullOrEmpty(detailList[i].CheckResult))
                        {
                            sbsql.AppendLine(",[CheckResult]");
                        }
                        if (!string.IsNullOrEmpty(detailList[i].Remark))
                        {
                            sbsql.AppendLine(",[Remark]");
                        }
                        if (detailList[i].PassNum != -1)
                        {
                            sbsql.AppendLine(",[PassNum]");
                        }
                        if (detailList[i].NotPassNum != -1)
                        {
                            sbsql.AppendLine(",[NotPassNum]");
                        }
                        //if (detailList[i].BadNum != -1)
                        //{
                        //    sbsql.AppendLine(",[BadNum]");
                        //}
                        sbsql.AppendLine(" ,[CompanyCD])");
                        sbsql.AppendLine("  values(");
                        sbsql.AppendLine(" @ReportNo,@SortNo,@CheckItem");
                        if (!string.IsNullOrEmpty(detailList[i].CheckValue))
                        {
                            sbsql.AppendLine(",@CheckValue");
                        }
                        sbsql.AppendLine(",@CheckNum");
                        if (!string.IsNullOrEmpty(detailList[i].StandardValue))
                        {
                            sbsql.AppendLine(",@StandardValue");
                        }
                        sbsql.AppendLine(",@isPass,@Checker,@CheckDeptID,@ModifiedDate,@ModifiedUserID");
                        if (!string.IsNullOrEmpty(detailList[i].NormUpLimit))
                        {
                            sbsql.AppendLine(",@NormUpLimit");
                        }
                        if (!string.IsNullOrEmpty(detailList[i].LowerLimit))
                        {
                            sbsql.AppendLine(",@LowerLimit");
                        }
                        if (!string.IsNullOrEmpty(detailList[i].CheckStandard))
                        {
                            sbsql.AppendLine(",@CheckStandard");
                        }
                        if (!string.IsNullOrEmpty(detailList[i].CheckResult))
                        {
                            sbsql.AppendLine(",@CheckResult");
                        }
                        if (!string.IsNullOrEmpty(detailList[i].Remark))
                        {
                            sbsql.AppendLine("       , @Remark");
                        }
                        if (detailList[i].PassNum != -1)
                        {
                            sbsql.AppendLine("               ,@PassNum");
                        }
                        if (detailList[i].NotPassNum != -1)
                        {
                            sbsql.AppendLine("           ,@NotPassNum");
                        }
                        //if (detailList[i].BadNum != -1)
                        //{
                        //    sbsql.AppendLine("                ,@BadNum");
                        //}
                        sbsql.AppendLine("  ,@CompanyCD)");
                        SqlCommand sqlcomm = new SqlCommand();

                        sqlcomm.Parameters.Add(SqlHelper.GetParameter("@ReportNo", model.ReportNo));
                        sqlcomm.Parameters.Add(SqlHelper.GetParameter("@SortNo", detailList[i].SortNo));
                        sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CheckItem", detailList[i].CheckItem));
                        if (!string.IsNullOrEmpty(detailList[i].CheckStandard))
                        {
                            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CheckStandard", detailList[i].CheckStandard));
                        }
                        if (!string.IsNullOrEmpty(detailList[i].CheckValue))
                        {
                            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CheckValue", detailList[i].CheckValue));
                        }
                        sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CheckNum", detailList[i].CheckNum));
                        if (!string.IsNullOrEmpty(detailList[i].StandardValue))
                        {
                            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@StandardValue", detailList[i].StandardValue));
                        }
                        if (!string.IsNullOrEmpty(detailList[i].NormUpLimit))
                        {
                            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@NormUpLimit", detailList[i].NormUpLimit));
                        }
                        if (!string.IsNullOrEmpty(detailList[i].LowerLimit))
                        {
                            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@LowerLimit", detailList[i].LowerLimit));
                        }
                        if (!string.IsNullOrEmpty(detailList[i].CheckResult))
                        {
                            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CheckResult", detailList[i].CheckResult));
                        }
                        sqlcomm.Parameters.Add(SqlHelper.GetParameter("@isPass", detailList[i].isPass));
                        sqlcomm.Parameters.Add(SqlHelper.GetParameter("@Checker", detailList[i].Checker));
                        sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CheckDeptID", detailList[i].CheckDeptID));
                        sqlcomm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", model.ModifiedDate));
                        sqlcomm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
                        if (!string.IsNullOrEmpty(detailList[i].Remark))
                        {
                            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@Remark", detailList[i].Remark));
                        }
                        if (detailList[i].PassNum != -1)
                        {
                            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@PassNum", detailList[i].PassNum));
                        }
                        if (detailList[i].NotPassNum != -1)
                        {
                            sqlcomm.Parameters.Add(SqlHelper.GetParameter("@NotPassNum", detailList[i].NotPassNum));
                        }
                        //if (detailList[i].BadNum != -1)
                        //{
                        //    sqlcomm.Parameters.Add(SqlHelper.GetParameter("@BadNum", detailList[i].BadNum));
                        //}
                        sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                        sqlcomm.CommandText = sbsql.ToString();
                        sqllist.Add(sqlcomm);
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
        /// <summary>
        /// 获取外协加工厂-往来单位
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetFroeignCorpInfo(string CompanyCD)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ID,CustName from officedba.OtherCorpInfo");
            sql.AppendLine("where CompanyCD=@CompanyCD and BigType=5 and UsedStatus=1");
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            return SqlHelper.ExecuteSql(sql.ToString(), parms);
        }

        public static DataTable GetOtherCorpInfo(string CompanyCD)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ID,CustName from officedba.OtherCorpInfo");
            sql.AppendLine("where CompanyCD=@CompanyCD and BigType=7 and UsedStatus=1");
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            return SqlHelper.ExecuteSql(sql.ToString(), parms);
        }

        /// <summary>
        /// 回写生产任务单的质检相关信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool UpdateManufa(StorageQualityCheckReportModel model)
        {
            ArrayList sqllist = new ArrayList();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update officedba.ManufactureTaskDetail set CheckedCount=isnull(CheckedCount,0)+@CheckedCount,PassCount=isnull(PassCount,0)+@PassCount,NotPassCount=isnull(NotPassCount,0)+@NotPassCount");
            if (model.FromType == "3")
            {
                sql.AppendLine("  ,ApplyCheckCount=isnull(ApplyCheckCount,0)+@ApplyCheckCount");
            }
            sql.AppendLine(" where ID=@ID");
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql.ToString();
            comm.Parameters.Add(SqlHelper.GetParameter("@CheckedCount", model.CheckNum));
            comm.Parameters.Add(SqlHelper.GetParameter("@PassCount", model.PassNum));
            comm.Parameters.Add(SqlHelper.GetParameter("@NotPassCount", model.NoPass));
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.FromDetailID));
            if (model.FromType == "3")
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@ApplyCheckCount", model.CheckNum));
            }
            sqllist.Add(comm);
            if (SqlHelper.ExecuteTransWithArrayList(sqllist))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool UnUpdateManufa(StorageQualityCheckReportModel model)
        {
            ArrayList sqllist = new ArrayList();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update officedba.ManufactureTaskDetail set CheckedCount=isnull(CheckedCount,0)-@CheckedCount,PassCount=isnull(PassCount,0)-@PassCount,NotPassCount=isnull(NotPassCount,0)-@NotPassCount ");
            if (model.FromType == "3")
            {
                sql.AppendLine(" ,ApplyCheckCount=isnull(ApplyCheckCount,0)-@ApplyCheckCount");
            }
            sql.AppendLine("  where ID=@ID");
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql.ToString();
            comm.Parameters.Add(SqlHelper.GetParameter("@CheckedCount", model.CheckNum));
            if (model.FromType == "3")
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@ApplyCheckCount", model.CheckNum));
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@PassCount", model.PassNum));
            comm.Parameters.Add(SqlHelper.GetParameter("@NotPassCount", model.NoPass));
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.FromDetailID));
            sqllist.Add(comm);
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
        /// 回写质检报告
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool UpdateReport(StorageQualityCheckReportModel model)
        {
            bool returnvalue = false;
            try
            {
                GetFromType(model.FromReportNo, model.CompanyCD);
                if (myFromType == "2")
                {
                    GetFromType(myReportNo, model.CompanyCD);
                }
                if (myFromType != "2" && myFromType != "00")
                {
                    if (int.Parse(FromDetailID) > 0)
                    {
                        model.FromDetailID = int.Parse(FromDetailID);
                    }
                    if (int.Parse(FromReportID) > 0)
                    {
                        model.ReportID = int.Parse(FromReportID);
                    }
                    switch (myFromType)
                    {
                        case "1": //申请单
                            if (UpdateApply(model))
                            {
                                returnvalue = true;
                            }
                            else
                            { returnvalue = false; }
                            break;
                        case "3"://生产
                            if (UpdateManufa(model))
                            {
                                returnvalue = true;
                            }
                            else
                            { returnvalue = false; }
                            break;
                        case "4"://采购
                            if (UpdatePur(model))
                            {
                                returnvalue = true;
                            }
                            else
                            { returnvalue = false; }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch
            { }
            myReportNo = "";
            myFromType = "";
            return returnvalue;
        }
        public static void GetFromType(string FromReportNo, string CompanyCD)
        {
            string detailsql = "select FromDetailID,ReportID,ReportNo,FromType from officedba.QualityCheckReport where ReportNo=@FromReportNo and CompanyCD=@CompanyCD";
            SqlCommand testcomm = new SqlCommand();
            testcomm.Parameters.Add(SqlHelper.GetParameter("@FromReportNo", FromReportNo));
            testcomm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            testcomm.CommandText = detailsql;
            DataTable dt = SqlHelper.ExecuteSearch(testcomm);
            if (dt.Rows.Count > 0)
            {
                myReportNo = dt.Rows[0]["ReportNo"].ToString();
                myFromType = dt.Rows[0]["FromType"].ToString();
                FromDetailID = dt.Rows[0]["FromDetailID"].ToString();
                FromReportID = dt.Rows[0]["ReportID"].ToString();
                if (myFromType == "2")
                {
                    FromReportNo = dt.Rows[0]["ReportNo"].ToString();
                    GetFromType(FromReportNo, CompanyCD);
                }
            }
        }
        /// <summary>
        /// 回写质检申请单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool UpdateApply(StorageQualityCheckReportModel model)
        {
            ArrayList sqllist = new ArrayList();
            string sql = "";
            sql = "update officedba.QualityCheckApplyDetail set RealCheckedCount=isnull(RealCheckedCount,0)+@CheckedCount where  ID=@ID";
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql;
            comm.Parameters.Add(SqlHelper.GetParameter("@CheckedCount", model.CheckNum));
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.FromDetailID));
            sqllist.Add(comm);
            #region 回写质检申请单源单类型的相应数据
            SqlCommand SelComm = new SqlCommand();
            string selsql = "select FromType from officedba.QualityCheckApplay where ID=@ID";
            SelComm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ReportID));
            SelComm.CommandText = selsql;
            string Fromtype = "00";
            DataTable seldt = SqlHelper.ExecuteSearch(SelComm);
            if (seldt.Rows.Count > 0)
            {
                Fromtype = seldt.Rows[0]["FromType"].ToString();
            }

            if (Fromtype == "1") //质检申请单的源单类型为 采购时
            {
                #region 根据源单明细ID 获取采购或生产明细ID
                SqlCommand GetDetailComm = new SqlCommand();
                string GetDetailSql = "select FromBillID from officedba.QualityCheckApplyDetail where ID=@ID";
                GetDetailComm.Parameters.Add(SqlHelper.GetParameter("@ID", model.FromDetailID));
                GetDetailComm.CommandText = GetDetailSql;
                DataTable GetDetailDt = SqlHelper.ExecuteSearch(GetDetailComm);
                string FromBillID = "0";
                if (GetDetailDt.Rows.Count > 0)
                {
                    FromBillID = GetDetailDt.Rows[0]["FromBillID"].ToString();
                }
                #endregion
                if (int.Parse(FromBillID) > 0)
                {
                    SqlCommand purcomm = new SqlCommand();
                    string pursql = " update  officedba.PurchaseArriveDetail set CheckedCount=isnull(CheckedCount,0)+@CheckedCount,PassCount=isnull(PassCount,0)+@PassCount,NotPassCount=isnull(NotPassCount,0)+@NotPassCount";
                    pursql += " where ID=@ID";
                    purcomm.Parameters.Add(SqlHelper.GetParameter("@ID", FromBillID));
                    purcomm.Parameters.Add(SqlHelper.GetParameter("@CheckedCount", model.CheckNum));
                    purcomm.Parameters.Add(SqlHelper.GetParameter("@PassCount", model.PassNum));
                    purcomm.Parameters.Add(SqlHelper.GetParameter("@NotPassCount", model.NoPass));
                    purcomm.CommandText = pursql;
                    sqllist.Add(purcomm);
                }
            }
            if (Fromtype == "2") //质检申请单的源单类型为 生产
            {
                #region 根据源单明细ID 获取采购或生产明细ID
                SqlCommand GetDetailComm = new SqlCommand();
                string GetDetailSql = "select FromBillID from officedba.QualityCheckApplyDetail where ID=@ID";
                GetDetailComm.Parameters.Add(SqlHelper.GetParameter("@ID", model.FromDetailID));
                GetDetailComm.CommandText = GetDetailSql;
                DataTable GetDetailDt = SqlHelper.ExecuteSearch(GetDetailComm);
                string FromBillID = "0";
                if (GetDetailDt.Rows.Count > 0)
                {
                    FromBillID = GetDetailDt.Rows[0]["FromBillID"].ToString();
                }
                #endregion
                if (int.Parse(FromBillID) > 0)
                {
                    SqlCommand mancomm = new SqlCommand();
                    string mansql = " update  officedba.ManufactureTaskDetail set CheckedCount=isnull(CheckedCount,0)+@CheckedCount,PassCount=isnull(PassCount,0)+@PassCount,NotPassCount=isnull(NotPassCount,0)+@NotPassCount";
                    mansql += " where ID=@ID";
                    mancomm.Parameters.Add(SqlHelper.GetParameter("@ID", FromBillID));
                    mancomm.Parameters.Add(SqlHelper.GetParameter("@CheckedCount", model.CheckNum));
                    mancomm.Parameters.Add(SqlHelper.GetParameter("@PassCount", model.PassNum));
                    mancomm.Parameters.Add(SqlHelper.GetParameter("@NotPassCount", model.NoPass));
                    mancomm.CommandText = mansql;
                    sqllist.Add(mancomm);
                }
            }
            //if (Fromtype == "0")//无来源
            //{
            //    SqlCommand noncomm = new SqlCommand();
            //    string nonsql = "update officedba.QualityCheckApplyDetail set RealCheckedCount=isnull(RealCheckedCount,0)+@RealCheckedCount where ID=@ID";
            //    noncomm.Parameters.Add(SqlHelper.GetParameter("@RealCheckedCount", model.CheckNum));
            //    noncomm.Parameters.Add(SqlHelper.GetParameter("@ID", model.FromDetailID));
            //    noncomm.CommandText = nonsql;
            //    sqllist.Add(noncomm);
            //}
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
        //public static bool UnUpdateApply(StorageQualityCheckReportModel model)
        //{
        //    ArrayList sqllist = new ArrayList();
        //    SqlCommand quacomm=new SqlCommand

        //    string sql = "";
        //    sql = "update officedba.QualityCheckApplyDetail set CheckedCount=isnull(CheckedCount,0)-@CheckedCount,PassCount=isnull(PassCount,0)-@PassCount,NotPassCount=isnull(NotPassCount,0)-@NotPassCount where  ID=@ID";
        //    SqlCommand comm = new SqlCommand();
        //    comm.CommandText = sql;
        //    comm.Parameters.Add(SqlHelper.GetParameter("@CheckedCount", model.CheckNum));
        //    comm.Parameters.Add(SqlHelper.GetParameter("@PassCount", model.PassNum));
        //    comm.Parameters.Add(SqlHelper.GetParameter("@NotPassCount", model.NoPass));
        //    comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.FromDetailID));
        //    sqllist.Add(comm);
        //    if (SqlHelper.ExecuteTransWithArrayList(sqllist))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        public static bool UnUpdateApply(StorageQualityCheckReportModel model)
        {
            ArrayList sqllist = new ArrayList();
            string sql = "";
            sql = "update officedba.QualityCheckApplyDetail set RealCheckedCount=isnull(RealCheckedCount,0)-@ProductCount where  ID=@ID";
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql;
            comm.Parameters.Add(SqlHelper.GetParameter("@ProductCount", model.CheckNum));
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.FromDetailID));
            sqllist.Add(comm);
            #region 回写质检申请单源单类型的相应数据
            SqlCommand SelComm = new SqlCommand();
            string selsql = "select FromType from officedba.QualityCheckApplay where ID=@ID";
            SelComm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ReportID));
            SelComm.CommandText = selsql;
            string Fromtype = "00";
            DataTable seldt = SqlHelper.ExecuteSearch(SelComm);
            if (seldt.Rows.Count > 0)
            {
                Fromtype = seldt.Rows[0]["FromType"].ToString();
            }

            if (Fromtype == "1") //质检申请单的源单类型为 采购时
            {
                #region 根据源单明细ID 获取采购或生产明细ID
                SqlCommand GetDetailComm = new SqlCommand();
                string GetDetailSql = "select FromBillID from officedba.QualityCheckApplyDetail where ID=@ID";
                GetDetailComm.Parameters.Add(SqlHelper.GetParameter("@ID", model.FromDetailID));
                GetDetailComm.CommandText = GetDetailSql;
                DataTable GetDetailDt = SqlHelper.ExecuteSearch(GetDetailComm);
                string FromBillID = "0";
                if (GetDetailDt.Rows.Count > 0)
                {
                    FromBillID = GetDetailDt.Rows[0]["FromBillID"].ToString();
                }
                #endregion
                if (int.Parse(FromBillID) > 0)
                {
                    SqlCommand purcomm = new SqlCommand();
                    string pursql = " update  officedba.PurchaseArriveDetail set CheckedCount=isnull(CheckedCount,0)-@CheckedCount,PassCount=isnull(PassCount,0)-@PassCount,NotPassCount=isnull(NotPassCount,0)-@NotPassCount";
                    pursql += " where ID=@ID";
                    purcomm.Parameters.Add(SqlHelper.GetParameter("@ID", FromBillID));
                    purcomm.Parameters.Add(SqlHelper.GetParameter("@CheckedCount", model.CheckNum));
                    purcomm.Parameters.Add(SqlHelper.GetParameter("@PassCount", model.PassNum));
                    purcomm.Parameters.Add(SqlHelper.GetParameter("@NotPassCount", model.NoPass));
                    purcomm.CommandText = pursql;
                    sqllist.Add(purcomm);
                }
            }
            if (Fromtype == "2") //质检申请单的源单类型为 生产
            {
                #region 根据源单明细ID 获取采购或生产明细ID
                SqlCommand GetDetailComm = new SqlCommand();
                string GetDetailSql = "select FromBillID from officedba.QualityCheckApplyDetail where ID=@ID";
                GetDetailComm.Parameters.Add(SqlHelper.GetParameter("@ID", model.FromDetailID));
                GetDetailComm.CommandText = GetDetailSql;
                DataTable GetDetailDt = SqlHelper.ExecuteSearch(GetDetailComm);
                string FromBillID = "0";
                if (GetDetailDt.Rows.Count > 0)
                {
                    FromBillID = GetDetailDt.Rows[0]["FromBillID"].ToString();
                }
                #endregion
                if (int.Parse(FromBillID) > 0)
                {
                    SqlCommand mancomm = new SqlCommand();
                    string mansql = " update  officedba.ManufactureTaskDetail set CheckedCount=isnull(CheckedCount,0)-@CheckedCount,PassCount=isnull(PassCount,0)-@PassCount,NotPassCount=isnull(NotPassCount,0)-@NotPassCount";
                    mansql += " where ID=@ID";
                    mancomm.Parameters.Add(SqlHelper.GetParameter("@ID", FromBillID));
                    mancomm.Parameters.Add(SqlHelper.GetParameter("@CheckedCount", model.CheckNum));
                    mancomm.Parameters.Add(SqlHelper.GetParameter("@PassCount", model.PassNum));
                    mancomm.Parameters.Add(SqlHelper.GetParameter("@NotPassCount", model.NoPass));
                    mancomm.CommandText = mansql;
                    sqllist.Add(mancomm);
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
        /// 回写采购到货
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool UpdatePur(StorageQualityCheckReportModel model)
        {
            ArrayList list = new ArrayList();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update officedba.PurchaseArriveDetail set CheckedCount=isnull(CheckedCount,0)+@CheckedCount,PassCount=isnull(PassCount,0)+@PassCount,NotPassCount=isnull(NotPassCount,0)+@NotPassCount");
            if (model.FromType == "4")
            {
                sql.AppendLine("      ,ApplyCheckCount=isnull(ApplyCheckCount,0)+@ApplyCheckCount ");
            }
            sql.AppendLine(" where ID=@ID");
            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameter("@CheckedCount", model.CheckNum));
            comm.Parameters.Add(SqlHelper.GetParameter("@PassCount", model.PassNum));
            comm.Parameters.Add(SqlHelper.GetParameter("@NotPassCount", model.NoPass));
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.FromDetailID));
            if (model.FromType == "4")
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@ApplyCheckCount", model.CheckNum));
            }
            comm.CommandText = sql.ToString();
            list.Add(comm);
            if (SqlHelper.ExecuteTransWithArrayList(list))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool UnUpdatePur(StorageQualityCheckReportModel model)
        {
            ArrayList list = new ArrayList();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update officedba.PurchaseArriveDetail set CheckedCount=isnull(CheckedCount,0)-@CheckedCount,PassCount=isnull(PassCount,0)-@PassCount,NotPassCount=isnull(NotPassCount,0)-@NotPassCount");
            if (model.FromType == "4")
            {
                sql.AppendLine(" ,ApplyCheckCount=isnull(ApplyCheckCount,0)-@ApplyCheckCount");
            }
            sql.AppendLine(" where ID=@ID");
            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameter("@CheckedCount", model.CheckNum));
            comm.Parameters.Add(SqlHelper.GetParameter("@PassCount", model.PassNum));
            comm.Parameters.Add(SqlHelper.GetParameter("@NotPassCount", model.NoPass));
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.FromDetailID));
            if (model.FromType == "4")
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@ApplyCheckCount", model.CheckNum));
            }
            comm.CommandText = sql.ToString();
            list.Add(comm);
            if (SqlHelper.ExecuteTransWithArrayList(list))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string GetCheckConfirm(StorageQualityCheckReportModel model)
        {
            string returnvalue = "0";
            #region  多次引用同个单据的时候要多其报检数量 进行校验
            switch (model.FromType)
            {
                case "1": //质检申请单
                    SqlCommand qualitycomm = new SqlCommand();
                    string qualitysql = "select FromType,FromBillID from officedba.QualityCheckApplyDetail where ID=@ID";
                    qualitycomm.Parameters.Add(SqlHelper.GetParameter("@ID", model.FromDetailID));
                    qualitycomm.CommandText = qualitysql;
                    DataTable qualitydt = SqlHelper.ExecuteSearch(qualitycomm);
                    string qualityFromType = "00";
                    string FromBillID = "00";
                    if (qualitydt.Rows.Count > 0)
                    {
                        qualityFromType = qualitydt.Rows[0]["FromType"].ToString();
                        FromBillID = qualitydt.Rows[0]["FromBillID"].ToString();
                    }
                    if (qualityFromType == "1")//采购
                    {
                        string ProductCount = "00";
                        string CheckedCount1 = "00";
                        SqlCommand FromPurComm = new SqlCommand();
                        string FormPurSql = "select ProductCount as ProductCount,CheckedCount as CheckedCount1,(isnull(ProductCount,0)-isnull(CheckedCount,0)) as CheckedCount from officedba.PurchaseArriveDetail where ID=@ID";
                        FromPurComm.Parameters.Add(SqlHelper.GetParameter("@ID", FromBillID));
                        FromPurComm.CommandText = FormPurSql;
                        DataTable FromPurDt = SqlHelper.ExecuteSearch(FromPurComm);
                        if (FromPurDt.Rows.Count > 0)
                        {
                            ProductCount = FromPurDt.Rows[0]["ProductCount"].ToString();
                            CheckedCount1 = FromPurDt.Rows[0]["CheckedCount1"].ToString();
                            returnvalue = FromPurDt.Rows[0]["CheckedCount"].ToString();
                            if (ProductCount != "" && CheckedCount1 != "")
                            {
                                if (Convert.ToDecimal(ProductCount) == Convert.ToDecimal(CheckedCount1))
                                {
                                    returnvalue = "none";  //生产数量=已检数量
                                }
                            }
                        }
                    }
                    if (qualityFromType == "2")//生产
                    {
                        string ProductedCount = "00";
                        string CheckedCount = "00";
                        SqlCommand FromManComm = new SqlCommand();
                        string FormManSql = "select ProductedCount as ProductedCount,CheckedCount as CheckedCount1,(isnull(ProductedCount,0)-isnull(CheckedCount,0)) as CheckedCount from officedba.ManufactureTaskDetail where ID=@ID";
                        FromManComm.Parameters.Add(SqlHelper.GetParameter("@ID", FromBillID));
                        FromManComm.CommandText = FormManSql;
                        DataTable FromManDt = SqlHelper.ExecuteSearch(FromManComm);
                        if (FromManDt.Rows.Count > 0)
                        {
                            ProductedCount = FromManDt.Rows[0]["ProductedCount"].ToString();
                            CheckedCount = FromManDt.Rows[0]["CheckedCount1"].ToString();
                            returnvalue = FromManDt.Rows[0]["CheckedCount"].ToString();
                            if (CheckedCount != "" && ProductedCount != "")
                            {
                                if (Convert.ToDecimal(ProductedCount) == Convert.ToDecimal(CheckedCount))
                                {
                                    returnvalue = "none";
                                }
                            }

                        }
                    }
                    if (qualityFromType == "0")
                    {
                        string RealCheckedCount = "00";
                        string ProductCount = "00";
                        SqlCommand FromNonComm = new SqlCommand();
                        string FormNonSql = "select ProductCount as ProductCount,RealCheckedCount as RealCheckedCount,(isnull(ProductCount,0)-isnull(RealCheckedCount,0)) as CheckedCount from officedba.QualityCheckApplyDetail where ID=@ID";
                        FromNonComm.Parameters.Add(SqlHelper.GetParameter("@ID", model.FromDetailID));
                        FromNonComm.CommandText = FormNonSql;
                        DataTable FromNonDt = SqlHelper.ExecuteSearch(FromNonComm);
                        if (FromNonDt.Rows.Count > 0)
                        {
                            ProductCount = FromNonDt.Rows[0]["ProductCount"].ToString();
                            RealCheckedCount = FromNonDt.Rows[0]["RealCheckedCount"].ToString();
                            returnvalue = FromNonDt.Rows[0]["CheckedCount"].ToString();
                            if (RealCheckedCount != "" && ProductCount != "")
                            {
                                if (Convert.ToDecimal(ProductCount) == Convert.ToDecimal(RealCheckedCount))
                                {
                                    returnvalue = "none";
                                }
                            }
                        }
                    }
                    break;
                case "2": //质检报告单
                    GetFromType(model.FromReportNo, model.CompanyCD);
                    if (myFromType == "2")
                    {
                        GetFromType(myReportNo, model.CompanyCD);
                    }
                    if (myFromType != "2" && myFromType != "00")
                    {
                        if (int.Parse(FromDetailID) > 0)
                        {
                            model.FromDetailID = int.Parse(FromDetailID);
                        }
                        if (int.Parse(FromReportID) > 0)
                        {
                            model.ReportID = int.Parse(FromReportID);
                        }
                        if (myFromType == "1") //质检申请单
                        {
                            returnvalue = GetCheckedCount(model);
                        }
                        if (myFromType == "3") //生产任务单
                        {
                            string ProductedCount = "00";
                            string CheckedCount = "00";
                            SqlCommand FromManComm1 = new SqlCommand();
                            string FormManSql1 = "select  ProductedCount,CheckedCount as CheckedCount1,(isnull(ProductedCount,0)-isnull(CheckedCount,0)) as CheckedCount from officedba.ManufactureTaskDetail where ID=@ID";
                            FromManComm1.Parameters.Add(SqlHelper.GetParameter("@ID", model.FromDetailID));
                            FromManComm1.CommandText = FormManSql1;
                            DataTable FromManDt1 = SqlHelper.ExecuteSearch(FromManComm1);
                            if (FromManDt1.Rows.Count > 0)
                            {
                                ProductedCount = FromManDt1.Rows[0]["ProductedCount"].ToString();
                                CheckedCount = FromManDt1.Rows[0]["CheckedCount1"].ToString();
                                returnvalue = FromManDt1.Rows[0]["CheckedCount"].ToString();
                                if (ProductedCount != "" && CheckedCount != "")
                                {
                                    if (Convert.ToDecimal(ProductedCount) == Convert.ToDecimal(CheckedCount))
                                    {
                                        returnvalue = "none";
                                    }
                                }
                            }
                        }
                        if (myFromType == "4") //采购
                        {
                            string ProductCount = "00";
                            string CheckedCount1 = "0";
                            SqlCommand FromPurComm1 = new SqlCommand();
                            string FormPurSql1 = "select  ProductCount,CheckedCount as CheckedCount1,(isnull(ProductCount,0)-isnull(CheckedCount,0)) as CheckedCount from officedba.PurchaseArriveDetail where ID=@ID";
                            FromPurComm1.Parameters.Add(SqlHelper.GetParameter("@ID", model.FromDetailID));
                            FromPurComm1.CommandText = FormPurSql1;
                            DataTable FromPurDt1 = SqlHelper.ExecuteSearch(FromPurComm1);
                            if (FromPurDt1.Rows.Count > 0)
                            {
                                ProductCount = FromPurDt1.Rows[0]["ProductCount"].ToString();
                                CheckedCount1 = FromPurDt1.Rows[0]["CheckedCount1"].ToString();
                                returnvalue = FromPurDt1.Rows[0]["CheckedCount"].ToString();
                                if (ProductCount != "" && CheckedCount1 != "")
                                {
                                    if (Convert.ToDecimal(ProductCount) == Convert.ToDecimal(CheckedCount1))
                                    {
                                        returnvalue = "none";  //生产数量=已检数量
                                    }
                                }
                            }
                        }
                    }
                    break;
                case "4": //采购
                    string ProductCount4 = "00";
                    string CheckedCount14 = "00";
                    SqlCommand FromPurComm4 = new SqlCommand();
                    string FormPurSql4 = "select ProductCount as ProductCount,CheckedCount as CheckedCount1,(isnull(ProductCount,0)-isnull(CheckedCount,0)) as CheckedCount from officedba.PurchaseArriveDetail where ID=@ID";
                    FromPurComm4.Parameters.Add(SqlHelper.GetParameter("@ID", model.FromDetailID));
                    FromPurComm4.CommandText = FormPurSql4;
                    DataTable FromPurDt4 = SqlHelper.ExecuteSearch(FromPurComm4);
                    if (FromPurDt4.Rows.Count > 0)
                    {
                        ProductCount4 = FromPurDt4.Rows[0]["ProductCount"].ToString();
                        CheckedCount14 = FromPurDt4.Rows[0]["CheckedCount1"].ToString();
                        returnvalue = FromPurDt4.Rows[0]["CheckedCount"].ToString();
                        if (ProductCount4 != "" && CheckedCount14 != "")
                        {
                            if (Convert.ToDecimal(ProductCount4) == Convert.ToDecimal(CheckedCount14))
                            {
                                returnvalue = "none";  //生产数量=已检数量
                            }
                        }
                    }
                    break;
                case "3":
                    string ProductedCount3 = "00";
                    string CheckedCount3 = "00";
                    SqlCommand FromManComm3 = new SqlCommand();
                    string FormManSql3 = "select ProductedCount as ProductedCount,CheckedCount as CheckedCount1,(isnull(ProductedCount,0)-isnull(CheckedCount,0)) as CheckedCount from officedba.ManufactureTaskDetail where ID=@ID";
                    FromManComm3.Parameters.Add(SqlHelper.GetParameter("@ID", model.FromDetailID));
                    FromManComm3.CommandText = FormManSql3;
                    DataTable FromManDt3 = SqlHelper.ExecuteSearch(FromManComm3);
                    if (FromManDt3.Rows.Count > 0)
                    {
                        ProductedCount3 = FromManDt3.Rows[0]["ProductedCount"].ToString();
                        CheckedCount3 = FromManDt3.Rows[0]["CheckedCount1"].ToString();
                        returnvalue = FromManDt3.Rows[0]["CheckedCount"].ToString();
                        if (CheckedCount3 != "" && ProductedCount3 != "")
                        {
                            if (Convert.ToDecimal(ProductedCount3) == Convert.ToDecimal(CheckedCount3))
                            {
                                returnvalue = "none";
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
            #endregion
            return returnvalue;
        }
        /// <summary>
        /// 源单类型为质检申请单 获取已检数量
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string GetCheckedCount(StorageQualityCheckReportModel model)
        {
            string returnvalue = "0";
            SqlCommand checkcomm = new SqlCommand();
            string FromType = "0";
            string FromBillID = "0";
            string checksql = "select FromType,FromBillID from officedba.QualityCheckApplyDetail where ID=@ID";
            checkcomm.Parameters.Add(SqlHelper.GetParameter("@ID", model.FromDetailID));
            checkcomm.CommandText = checksql;
            DataTable dt = SqlHelper.ExecuteSearch(checkcomm);
            if (dt.Rows.Count > 0)
            {
                FromType = dt.Rows[0]["FromType"].ToString();
                FromBillID = dt.Rows[0]["FromBillID"].ToString();
            }
            if (FromType == "0")
            {
                string RealCheckedCount = "00";
                string ProductCount = "00";
                SqlCommand FromNonComm = new SqlCommand();
                string FormNonSql = "select ProductCount as ProductCount,RealCheckedCount as RealCheckedCount,(isnull(ProductCount,0)-isnull(RealCheckedCount,0)) as CheckedCount from officedba.QualityCheckApplyDetail where ID=@ID";
                FromNonComm.Parameters.Add(SqlHelper.GetParameter("@ID", model.FromDetailID));
                FromNonComm.CommandText = FormNonSql;
                DataTable FromNonDt = SqlHelper.ExecuteSearch(FromNonComm);
                if (FromNonDt.Rows.Count > 0)
                {
                    ProductCount = FromNonDt.Rows[0]["ProductCount"].ToString();
                    RealCheckedCount = FromNonDt.Rows[0]["RealCheckedCount"].ToString();
                    returnvalue = FromNonDt.Rows[0]["CheckedCount"].ToString();
                    if (RealCheckedCount != "" && ProductCount != "")
                    {
                        if (Convert.ToDecimal(ProductCount) == Convert.ToDecimal(RealCheckedCount))
                        {
                            returnvalue = "none";
                        }
                    }
                }
            }
            if (FromType == "1")//源单为采购时候
            {
                string RealCheckedCount = "00";
                string ProductCount = "00";
                SqlCommand purcomm = new SqlCommand();
                string pursql = "select ProductCount as ProductCount,CheckedCount as CheckedCount1,(isnull(ProductCount,0)-isnull(CheckedCount,0)) as CheckedCount from officedba.PurchaseArriveDetail where ID=@ID";
                purcomm.Parameters.Add(SqlHelper.GetParameter("@ID", FromBillID));
                purcomm.CommandText = pursql;
                DataTable purdt = SqlHelper.ExecuteSearch(purcomm);
                if (purdt.Rows.Count > 0)
                {
                    ProductCount = purdt.Rows[0]["ProductCount"].ToString();
                    RealCheckedCount = purdt.Rows[0]["CheckedCount1"].ToString();
                    returnvalue = purdt.Rows[0]["CheckedCount"].ToString();
                    if (RealCheckedCount != "" && ProductCount != "")
                    {
                        if (Convert.ToDecimal(ProductCount) == Convert.ToDecimal(RealCheckedCount))
                        {
                            returnvalue = "none";
                        }
                    }
                }
            }
            if (FromType == "2")//源单为生产
            {
                string RealCheckedCount = "00";
                string ProductCount = "00";
                SqlCommand mancomm = new SqlCommand();
                string mansql = "select ProductedCount as ProductedCount,CheckedCount as CheckedCount1,(isnull(ProductedCount,0)-isnull(CheckedCount,0)) as CheckedCount from officedba.ManufactureTaskDetail where ID=@ID";
                mancomm.Parameters.Add(SqlHelper.GetParameter("@ID", FromBillID));
                mancomm.CommandText = mansql;
                DataTable mandt = SqlHelper.ExecuteSearch(mancomm);
                if (mandt.Rows.Count > 0)
                {
                    ProductCount = mandt.Rows[0]["ProductedCount"].ToString();
                    RealCheckedCount = mandt.Rows[0]["CheckedCount1"].ToString();
                    returnvalue = mandt.Rows[0]["CheckedCount"].ToString();
                    if (RealCheckedCount != "" && ProductCount != "")
                    {
                        if (Convert.ToDecimal(ProductCount) == Convert.ToDecimal(RealCheckedCount))
                        {
                            returnvalue = "none";
                        }
                    }
                }
            }
            return returnvalue;
        }

        /// <summary>
        /// 报告确认
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool ConfirmBill(StorageQualityCheckReportModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.QualityCheckReport SET");
            sql.AppendLine(" Confirmor          = @Confirmor,");
            sql.AppendLine(" ConfirmDate      = @ConfirmDate,");
            sql.AppendLine(" BillStatus              = 2,");
            sql.AppendLine(" ModifiedUserID      = @ModifiedUserID,");
            sql.AppendLine(" ModifiedDate                = @ModifiedDate ");
            sql.AppendLine("  Where  CompanyCD=@CompanyCD and ID=@ID");

            SqlParameter[] param = new SqlParameter[6];
            param[0] = SqlHelper.GetParameter("@ID", model.ID);
            param[1] = SqlHelper.GetParameter("@Confirmor", model.Confirmor);
            param[2] = SqlHelper.GetParameter("@ConfirmDate", model.ConfirmDate);
            param[3] = SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID);
            param[4] = SqlHelper.GetParameter("@ModifiedDate", model.ModifiedDate);
            param[5] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD.Trim());

            SqlHelper.ExecuteTransSql(sql.ToString(), param);
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }

        public static bool UnConfirm(StorageQualityCheckReportModel model)
        {
            ArrayList sqllist = new ArrayList();
            bool returnvalue = false;
            int BillTypeFlag = int.Parse(ConstUtil.BILL_TYPECODE_STORAGE_QUALITY);
            int BillTypeCode = int.Parse(ConstUtil.BILL_TYPECODE_STORAGE_REPORT);
            SqlCommand thecomm = new SqlCommand();
            string thesql = "select BillStatus from officedba.QualityCheckReport where ID=@ID";
            thecomm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            thecomm.CommandText = thesql;
            string BillStatus = "0";
            DataTable theDt = SqlHelper.ExecuteSearch(thecomm);
            if (theDt.Rows.Count > 0)
            {
                BillStatus = theDt.Rows[0]["BillStatus"].ToString();
            }
            if (BillStatus == "2")
            {
                SqlCommand comm = new SqlCommand();
                string sql = " update officedba.QualityCheckReport set BillStatus='1',ModifiedUserID=@ModifiedUserID,ModifiedDate=@ModifiedDate where ID=@ID";
                comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
                comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", model.ModifiedDate));
                comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                comm.CommandText = sql;
                sqllist.Add(comm);
                if (model.isRecheck == "0")
                {
                    switch (model.FromType)
                    {
                        case "1"://质检申请
                            UnUpdateApply(model);
                            break;
                        case "2"://质检报告
                            UnUpdateReport(model);
                            break;
                        case "3"://生产任务
                            UnUpdateManufa(model);
                            break;
                        case "4"://采购到货
                            UnUpdatePur(model);
                            break;
                        default:
                            break;
                    }
                }
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

        /// <summary>
        /// 结单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool CloseBill(StorageQualityCheckReportModel model, string method)
        {
            ArrayList listsql = new ArrayList();
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" UPDATE officedba.QualityCheckReport SET");
            sql.AppendLine(" BillStatus              = @BillStatus,");
            sql.AppendLine(" ModifiedUserID      = @ModifiedUserID,");
            if (method == "0")
            {
                sql.AppendLine(" Closer      = @Closer,");
                sql.AppendLine(" CloseDate                = @CloseDate, ");
            }
            sql.AppendLine(" ModifiedDate                = @ModifiedDate ");
            sql.AppendLine("  Where  CompanyCD=@CompanyCD and ID=@ID");


            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", model.ModifiedDate));
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            string BillStatus = "2";
            if (method == "0")
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@Closer", model.Closer));
                comm.Parameters.Add(SqlHelper.GetParameter("@CloseDate", model.CloseDate));
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

        /// <summary>
        /// 修改质检报告单时判断是否被引用
        /// </summary>
        /// <param name="FromReportNo"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static string IsTransfer(int ReportID, string CompanyCD)
        {
            string Result = "0";
            int Rows = 0;
            string sql = "select Count(ID) as Rows from officedba.CheckNotPass  where ReportID=" + ReportID + "  and CompanyCD='" + CompanyCD + "'";
            DataTable dt = SqlHelper.ExecuteSql(sql);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Rows"].ToString() != "")
                {
                    Rows = int.Parse(dt.Rows[0]["Rows"].ToString());
                }
            }
            int Rows1 = 0;
            SqlCommand comm = new SqlCommand();
            string reportsql = "select count(ID) as Rows from officedba.QualityCheckReport where ReportID=" + ReportID + " and CompanyCD='" + CompanyCD + "'";
            DataTable reportdt = SqlHelper.ExecuteSql(reportsql);
            if (reportdt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Rows"].ToString() != "")
                {
                    Rows1 = int.Parse(reportdt.Rows[0]["Rows"].ToString());
                }
            }
            Rows += Rows1;
            if (Rows > 0)
            {
                Result = "1";
            }
            return Result;

        }
        /// <summary>
        /// 判断制单状态的单据是否提交审批
        /// </summary>
        /// <returns></returns>
        public static string IsFlow(int ID)
        {
            string Rows = "0";
            string returnValue = "0";
            int BillTypeFlag = int.Parse(ConstUtil.BILL_TYPECODE_STORAGE_QUALITY);
            int BillTypeCode = int.Parse(ConstUtil.BILL_TYPECODE_STORAGE_REPORT);
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select count(e.ID) as Rows from officedba.QualityCheckReport as a left join officedba.FlowInstance AS e ON a.CompanyCD = e.CompanyCD AND e.BillTypeFlag =" + BillTypeFlag + " AND e.BillTypeCode = " + BillTypeCode + " AND a.ID = e.BillID ");
            sql.AppendLine(" where a.ID=" + ID + " and e.FlowStatus!=4 and e.FlowStatus!=5");
            DataTable dt = SqlHelper.ExecuteSql(sql.ToString());
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

        public static DataTable GetReportPur(string CompanyCD, string method, string ReprotStr)
        {
            SqlCommand comm = new SqlCommand();
            string[] testStr = ReprotStr.Split('?');
            string BillNo = testStr[0];
            string BillTitle = testStr[1];
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" select distinct a.ID,a.ModifiedDate,a.ArriveNo,isnull(a.Title,'') as Title,isnull(a.ProviderID,0) as CustID,isnull(b.CustName,'') as CustName,                                                                                                                                       ");
            sql.AppendLine(" 	   (case b.BigType when '1' then '客户' when '2' then '供应商' when '3' then '竞争对手' when '4' then '银行' when '5' then '外协加工厂' when '6' then '运输商' when '7' then '其他' else '' end ) as CustBigTypeName,isnull(b.BigType,0) as CustBigTypeID,  ");
            sql.AppendLine("        isnull(b.CustNo,'') as CustNo,isnull(c.EmployeeName,'') as EmployeeName,isnull(d.DeptName,'') as DeptName,isnull(a.ArriveDate,'') as ArriveDate                                                                                                       ");
            sql.AppendLine(" from officedba.PurchaseArrive as a left join officedba.ProviderInfo as b on a.ProviderID=b.ID left join officedba.EmployeeInfo as c on a.Purchaser=c.ID                                                                                                      ");
            sql.AppendLine("      left join officedba.DeptInfo as d on a.DeptID=d.ID                                                                                                                                                                                                      ");
            sql.AppendLine("      left join officedba.PurchaseArriveDetail as pd on a.ArriveNo=pd.ArriveNo ");
            sql.AppendLine(" where a.CompanyCD=@CompanyCD  ");
            sql.AppendLine("   and a.BillStatus='2'");
            if (BillNo != "")
            {
                sql.AppendLine(" and a.ArriveNo like @ArriveNo");
                comm.Parameters.Add(SqlHelper.GetParameter("@ArriveNo", "%" + BillNo + "%"));
            }
            if (BillTitle != "")
            {
                sql.AppendLine(" and a.Title like @Title");
                comm.Parameters.Add(SqlHelper.GetParameter("@Title", "%" + BillTitle + "%"));
            }
            if (method != "0")
            {
                sql.AppendLine(" and pd.ID in ( select pd1.ID from officedba.PurchaseArriveDetail as pd1 left join officedba.PurchaseArrive as p1 on pd1.ArriveNo=a.ArriveNo  where p1.CompanyCD=@CompanyCD  and isnull(pd1.ProductCount,0)>isnull(pd1.ApplyCheckCount,0) and pd1.ProductCount is not null)");
            }
            sql.AppendLine(" order by a.ModifiedDate desc ");
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            comm.CommandText = sql.ToString();
            DataTable dt = SqlHelper.ExecuteSearch(comm);
            return dt;
        }
        public static DataTable GetReportPurDetail(string ArriveNo, string CompanyCD, string ReportStr)
        {
            SqlCommand comm = new SqlCommand();
            string[] testStr = ReportStr.Split('?');
            string ProNO = testStr[0];
            string ProductName = testStr[1];
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select distinct a.ID,isnull(a.ProductName,'') as ProductName,isnull(p.Specification,'') as Specification,isnull(d.CodeName,'') as CodeName,isnull(a.ProductNo,'') as ProNo,isnull(a.ProductCount,0) as ProductCount");
            sql.AppendLine("       ,isnull(a.ApplyCheckCount,0) as ApplyCheckCount,isnull(a.CheckedCount,0) as CheckedCount,isnull(a.ProductID,0) as ProductID     ");
            sql.AppendLine("from officedba.PurchaseArriveDetail as a left join officedba.PurchaseArrive as b on a.ArriveNo=b.ArriveNo                              ");
            sql.AppendLine("     left join officedba.ProductInfo as p on p.ID=a.ProductID ");
            sql.AppendLine("     left join officedba.CodeUnitType as d on p.UnitID=d.ID                                                                            ");
            sql.AppendLine(" where a.ArriveNo=@ArriveNo and a.CompanyCD=@CompanyCD  ");
            sql.AppendLine(" and isnull(a.ProductCount,0)>isnull(a.ApplyCheckCount,0) and a.ProductCount is not null");
            if (ProNO != "")
            {
                sql.AppendLine(" and a.ProductNo like @ProductNo");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProductNo", "%" + ProNO + "%"));
            }
            if (ProductName != "")
            {
                sql.AppendLine(" and a.ProductName like @ProductName");
                comm.Parameters.Add(SqlHelper.GetParameter("@ProductName", "%" + ProductName + "%"));
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@ArriveNo", ArriveNo));
            comm.CommandText = sql.ToString();
            DataTable dt = SqlHelper.ExecuteSearch(comm);
            return dt;
        }
        public static bool UnUpdateReport(StorageQualityCheckReportModel model)
        {
            bool returnvalue = false;
            try
            {

                GetFromType(model.FromReportNo, model.CompanyCD);
                if (myFromType == "2")
                {
                    GetFromType(myReportNo, model.CompanyCD);
                }
                if (myFromType != "2" && myFromType != "00")
                {
                    if (int.Parse(FromDetailID) > 0)
                    {
                        model.FromDetailID = int.Parse(FromDetailID);
                    }
                    if (int.Parse(FromReportID) > 0)
                    {
                        model.ReportID = int.Parse(FromReportID);
                    }
                    switch (myFromType)
                    {
                        case "1"://质检申请单
                            if (UnUpdateApply(model))
                            {
                                returnvalue = true;
                            }
                            break;
                        case "3"://生产任务单
                            if (UnUpdateManufa(model))
                            {
                                returnvalue = true;
                            }
                            break;
                        case "4"://采购
                            if (UnUpdatePur(model))
                            {
                                returnvalue = true;
                            }
                            break;
                        case "0"://无来源
                            ArrayList sqllist = new ArrayList();
                            SqlCommand noncomm = new SqlCommand();
                            string nonsql = "update officedba.QualityCheckApplyDetail set RealCheckedCount=isnull(RealCheckedCount,0)-@RealCheckedCount where ID=@ID";
                            noncomm.Parameters.Add(SqlHelper.GetParameter("@RealCheckedCount", model.CheckNum));
                            noncomm.Parameters.Add(SqlHelper.GetParameter("@ID", model.FromDetailID));
                            noncomm.CommandText = nonsql;
                            sqllist.Add(noncomm);
                            if (SqlHelper.ExecuteTransWithArrayList(sqllist))
                            {
                                returnvalue = true;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch
            { }
            myReportNo = "";
            myFromType = "";
            return returnvalue;
        }


        public static string GetCheckSave(StorageQualityCheckReportModel model)
        {
            string returnvalue = "00";
            #region  多次引用同个单据的时候要多其报检数量 进行校验

            #region   质检申请单不需要进行该操作
            //if (model.FromType == "1")//质检申请单时候
            //{
            //    // returnvalue = GetCheckedCount(model);
            //    SqlCommand qualitycomm = new SqlCommand();
            //    string qualitysql = "select FromType,FromBillID from officedba.QualityCheckApplyDetail where ID=@ID";
            //    qualitycomm.Parameters.Add(SqlHelper.GetParameter("@ID", model.FromDetailID));
            //    qualitycomm.CommandText = qualitysql;
            //    DataTable qualitydt = SqlHelper.ExecuteSearch(qualitycomm);
            //    string qualityFromType = "00";
            //    string FromBillID = "00";
            //    if (qualitydt.Rows.Count > 0)
            //    {
            //        qualityFromType = qualitydt.Rows[0]["FromType"].ToString();
            //        FromBillID = qualitydt.Rows[0]["FromBillID"].ToString();
            //    }
            //    if (qualityFromType == "1")//采购
            //    {
            //        string ProductCount = "00";
            //        string ApplyCheckCount1 = "00";
            //        SqlCommand FromPurComm = new SqlCommand();
            //        string FormPurSql = "select ProductCount as ProductCount,ApplyCheckCount as ApplyCheckCount1,(isnull(ProductCount,0)-isnull(ApplyCheckCount,0)) as ApplyCheckCount from officedba.PurchaseArriveDetail where ID=@ID";
            //        FromPurComm.Parameters.Add(SqlHelper.GetParameter("@ID", FromBillID));
            //        FromPurComm.CommandText = FormPurSql;
            //        DataTable FromPurDt = SqlHelper.ExecuteSearch(FromPurComm);
            //        if (FromPurDt.Rows.Count > 0)
            //        {
            //            ProductCount = FromPurDt.Rows[0]["ProductCount"].ToString();//到货数量
            //            ApplyCheckCount1 = FromPurDt.Rows[0]["ApplyCheckCount1"].ToString();//已报检数量
            //            returnvalue = FromPurDt.Rows[0]["ApplyCheckCount"].ToString();//未报检数量
            //            if (ProductCount != "" && ApplyCheckCount1 != "")
            //            {
            //                if (Convert.ToDecimal(ProductCount) == Convert.ToDecimal(ApplyCheckCount1))
            //                {
            //                    returnvalue = "none";  //生产数量=已检数量
            //                }
            //            }
            //        }
            //    }
            //    if (qualityFromType == "2")//生产
            //    {
            //        string ProductedCount = "00";
            //        string CheckedCount = "00";
            //        SqlCommand FromManComm = new SqlCommand();
            //        string FormManSql = "select ProductedCount as ProductedCount,ApplyCheckCount as CheckedCount1,(isnull(ProductedCount,0)-isnull(ApplyCheckCount,0)) as CheckedCount from officedba.ManufactureTaskDetail where ID=@ID";
            //        FromManComm.Parameters.Add(SqlHelper.GetParameter("@ID", FromBillID));
            //        FromManComm.CommandText = FormManSql;
            //        DataTable FromManDt = SqlHelper.ExecuteSearch(FromManComm);
            //        if (FromManDt.Rows.Count > 0)
            //        {
            //            ProductedCount = FromManDt.Rows[0]["ProductedCount"].ToString();
            //            CheckedCount = FromManDt.Rows[0]["CheckedCount1"].ToString();
            //            returnvalue = FromManDt.Rows[0]["CheckedCount"].ToString();
            //            if (CheckedCount != "" && ProductedCount != "")
            //            {
            //                if (Convert.ToDecimal(ProductedCount) == Convert.ToDecimal(CheckedCount))
            //                {
            //                    returnvalue = "none";
            //                }
            //            }

            //        }
            //    }
            //    if (qualityFromType == "0")//无来源
            //    {
            //        string RealCheckedCount = "00";
            //        string ProductCount = "00";
            //        SqlCommand FromNonComm = new SqlCommand();
            //        string FormNonSql = "select ProductCount from officedba.QualityCheckApplyDetail where ID=@ID";
            //        FromNonComm.Parameters.Add(SqlHelper.GetParameter("@ID", FromBillID));
            //        FromNonComm.CommandText = FormNonSql;
            //        DataTable FromNonDt = SqlHelper.ExecuteSearch(FromNonComm);
            //        if (FromNonDt.Rows.Count > 0)
            //        {
            //            returnvalue = FromNonDt.Rows[0]["ProductCount"].ToString();
            //        }
            //    }
            //    //SqlCommand quacomm = new SqlCommand();
            //    //string quasql = "select isnull(ProductCount,0) as ProductCount,isnull(RealCheckedCount,0) as RealCheckedCount from officedba.QualityCheckApplyDetail where ID=@ID";
            //    //quacomm.Parameters.Add(SqlHelper.GetParameter("@ID", model.FromDetailID));
            //    //quacomm.CommandText = quasql;
            //    //DataTable quadt = SqlHelper.ExecuteSearch(quacomm);
            //    //string ProductCount = "0.00";
            //    //string RealCheckedCount = "0.00";
            //    //if (quadt.Rows.Count > 0)
            //    //{
            //    //    RealCheckedCount = quadt.Rows[0]["RealCheckedCount"].ToString();
            //    //    ProductCount = quadt.Rows[0]["ProductCount"].ToString();
            //    //}
            //    ////if(ProductCount!="" && RealCheckedCount!="")
            //    ////{
            //    ////    if (Convert.ToDecimal(ProductCount) == Convert.ToDecimal(RealCheckedCount))
            //    ////    {
            //    ////        returnvalue = "none";
            //    ////    }
            //    ////}
            //    //returnvalue = Convert.ToString(Convert.ToDecimal(ProductCount) - Convert.ToDecimal(RealCheckedCount));
            //}
            //#endregion

            //#region 无来源
            //if (model.FromType == "0")
            //{
            //    string RealCheckedCount = "00";
            //    string ProductCount = "00";
            //    SqlCommand FromNonComm = new SqlCommand();
            //    string FormNonSql = "select ProductCount from officedba.QualityCheckApplyDetail where ID=@ID";
            //    FromNonComm.Parameters.Add(SqlHelper.GetParameter("@ID", model.FromDetailID));
            //    FromNonComm.CommandText = FormNonSql;
            //    DataTable FromNonDt = SqlHelper.ExecuteSearch(FromNonComm);
            //    if (FromNonDt.Rows.Count > 0)
            //    {
            //        returnvalue = FromNonDt.Rows[0]["ProductCount"].ToString();
            //    }
            //}
            //#endregion

            //#region 质检报告单
            //if (model.FromType == "2")
            //{
            //    GetFromType(model.FromReportNo, model.CompanyCD);
            //    if (myFromType == "2")
            //    {
            //        GetFromType(myReportNo, model.CompanyCD);
            //    }
            //    if (myFromType != "2" && myFromType != "00")
            //    {
            //        if (int.Parse(FromDetailID) > 0)
            //        {
            //            model.FromDetailID = int.Parse(FromDetailID);
            //        }
            //        if (int.Parse(FromReportID) > 0)
            //        {
            //            model.ReportID = int.Parse(FromReportID);
            //        }
            //        if (myFromType == "1") //质检申请单
            //        {
            //            returnvalue = GetSaveCheckCount(model);
            //        }
            //        if (myFromType == "3") //生产任务单
            //        {
            //            string ProductedCount = "00";
            //            string CheckedCount = "00";
            //            SqlCommand FromManComm1 = new SqlCommand();
            //            string FormManSql1 = "select  ProductedCount,ApplyCheckCount as CheckedCount1,(isnull(ProductedCount,0)-isnull(ApplyCheckCount,0)) as CheckedCount from officedba.ManufactureTaskDetail where ID=@ID";
            //            FromManComm1.Parameters.Add(SqlHelper.GetParameter("@ID", model.FromDetailID));
            //            FromManComm1.CommandText = FormManSql1;
            //            DataTable FromManDt1 = SqlHelper.ExecuteSearch(FromManComm1);
            //            if (FromManDt1.Rows.Count > 0)
            //            {
            //                ProductedCount = FromManDt1.Rows[0]["ProductedCount"].ToString();
            //                CheckedCount = FromManDt1.Rows[0]["CheckedCount1"].ToString();
            //                returnvalue = FromManDt1.Rows[0]["CheckedCount"].ToString();
            //                if (ProductedCount != "" && CheckedCount != "")
            //                {
            //                    if (Convert.ToDecimal(ProductedCount) == Convert.ToDecimal(CheckedCount))
            //                    {
            //                        returnvalue = "none";
            //                    }
            //                }
            //            }
            //        }
            //        if (myFromType == "4") //采购
            //        {
            //            string ProductCount = "00";
            //            string CheckedCount1 = "0";
            //            SqlCommand FromPurComm1 = new SqlCommand();
            //            string FormPurSql1 = "select  ProductCount,ApplyCheckCount as CheckedCount1,(isnull(ProductCount,0)-isnull(ApplyCheckCount,0)) as CheckedCount from officedba.PurchaseArriveDetail where ID=@ID";
            //            FromPurComm1.Parameters.Add(SqlHelper.GetParameter("@ID", model.FromDetailID));
            //            FromPurComm1.CommandText = FormPurSql1;
            //            DataTable FromPurDt1 = SqlHelper.ExecuteSearch(FromPurComm1);
            //            if (FromPurDt1.Rows.Count > 0)
            //            {
            //                ProductCount = FromPurDt1.Rows[0]["ProductCount"].ToString();
            //                CheckedCount1 = FromPurDt1.Rows[0]["CheckedCount1"].ToString();
            //                returnvalue = FromPurDt1.Rows[0]["CheckedCount"].ToString();
            //                if (ProductCount != "" && CheckedCount1 != "")
            //                {
            //                    if (Convert.ToDecimal(ProductCount) == Convert.ToDecimal(CheckedCount1))
            //                    {
            //                        returnvalue = "none";
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            #endregion

            #region 采购
            if (model.FromType == "4")
            {
                string ProductCount = "00";
                string CheckedCount1 = "00";
                SqlCommand FromPurComm = new SqlCommand();
                string FormPurSql = "select ProductCount as ProductCount,ApplyCheckCount as CheckedCount1,(isnull(ProductCount,0)-isnull(ApplyCheckCount,0)) as CheckedCount from officedba.PurchaseArriveDetail where ID=@ID";
                FromPurComm.Parameters.Add(SqlHelper.GetParameter("@ID", model.FromDetailID));
                FromPurComm.CommandText = FormPurSql;
                DataTable FromPurDt = SqlHelper.ExecuteSearch(FromPurComm);
                if (FromPurDt.Rows.Count > 0)
                {
                    ProductCount = FromPurDt.Rows[0]["ProductCount"].ToString();
                    CheckedCount1 = FromPurDt.Rows[0]["CheckedCount1"].ToString();
                    returnvalue = FromPurDt.Rows[0]["CheckedCount"].ToString();
                    if (ProductCount != "" && CheckedCount1 != "")
                    {
                        if (Convert.ToDecimal(ProductCount) == Convert.ToDecimal(CheckedCount1))
                        {
                            returnvalue = "none";  //生产数量=已检数量
                        }
                    }
                }
            }
            #endregion

            #region 生产
            if (model.FromType == "3")
            {
                string ProductedCount = "00";
                string CheckedCount = "00";
                SqlCommand FromManComm = new SqlCommand();
                string FormManSql = "select ProductedCount as ProductedCount,ApplyCheckCount as CheckedCount1,(isnull(ProductedCount,0)-isnull(ApplyCheckCount,0)) as CheckedCount from officedba.ManufactureTaskDetail where ID=@ID";
                FromManComm.Parameters.Add(SqlHelper.GetParameter("@ID", model.FromDetailID));
                FromManComm.CommandText = FormManSql;
                DataTable FromManDt = SqlHelper.ExecuteSearch(FromManComm);
                if (FromManDt.Rows.Count > 0)
                {
                    ProductedCount = FromManDt.Rows[0]["ProductedCount"].ToString();
                    CheckedCount = FromManDt.Rows[0]["CheckedCount1"].ToString();
                    returnvalue = FromManDt.Rows[0]["CheckedCount"].ToString();
                    if (CheckedCount != "" && ProductedCount != "")
                    {
                        if (Convert.ToDecimal(ProductedCount) == Convert.ToDecimal(CheckedCount))
                        {
                            returnvalue = "none";
                        }
                    }

                }
            }
            #endregion

            #endregion
            return returnvalue;
        }

        public static string GetSaveCheckCount(StorageQualityCheckReportModel model)
        {
            string returnvalue = "0";
            SqlCommand checkcomm = new SqlCommand();
            string FromType = "0";
            string FromBillID = "0";
            string checksql = "select FromType,FromBillID from officedba.QualityCheckApplyDetail where ID=@ID";
            checkcomm.Parameters.Add(SqlHelper.GetParameter("@ID", model.FromDetailID));
            checkcomm.CommandText = checksql;
            DataTable dt = SqlHelper.ExecuteSearch(checkcomm);
            if (dt.Rows.Count > 0)
            {
                FromType = dt.Rows[0]["FromType"].ToString();
                FromBillID = dt.Rows[0]["FromBillID"].ToString();
            }
            switch (FromType)
            {
                case "0":
                    SqlCommand FromNonComm = new SqlCommand();
                    string FormNonSql = "select ProductCount as ProductCount from officedba.QualityCheckApplyDetail where ID=@ID";
                    FromNonComm.Parameters.Add(SqlHelper.GetParameter("@ID", model.FromDetailID));
                    FromNonComm.CommandText = FormNonSql;
                    DataTable FromNonDt = SqlHelper.ExecuteSearch(FromNonComm);
                    if (FromNonDt.Rows.Count > 0)
                    {
                        returnvalue = FromNonDt.Rows[0]["ProductCount"].ToString();
                    }
                    break;
                case "1"://源单为采购时候
                    string RealCheckedCount1 = "00";
                    string ProductCount1 = "00";
                    SqlCommand purcomm = new SqlCommand();
                    string pursql = "select ProductCount as ProductCount,ApplyCheckCount as CheckedCount1,(isnull(ProductCount,0)-isnull(ApplyCheckCount,0)) as CheckedCount from officedba.PurchaseArriveDetail where ID=@ID";
                    purcomm.Parameters.Add(SqlHelper.GetParameter("@ID", FromBillID));
                    purcomm.CommandText = pursql;
                    DataTable purdt = SqlHelper.ExecuteSearch(purcomm);
                    if (purdt.Rows.Count > 0)
                    {
                        ProductCount1 = purdt.Rows[0]["ProductCount"].ToString();// 到货数量
                        RealCheckedCount1 = purdt.Rows[0]["CheckedCount1"].ToString();//已检数量
                        returnvalue = purdt.Rows[0]["CheckedCount"].ToString();//未检数量
                        if (RealCheckedCount1 != "" && ProductCount1 != "")
                        {
                            if (Convert.ToDecimal(ProductCount1) == Convert.ToDecimal(RealCheckedCount1))
                            {
                                returnvalue = "none";
                            }
                        }
                    }
                    break;
                case "2"://源单为生产
                    string RealCheckedCount2 = "00";
                    string ProductCount2 = "00";
                    SqlCommand mancomm = new SqlCommand();
                    string mansql = "select ProductedCount as ProductedCount,ApplyCheckCount as CheckedCount1,(isnull(ProductedCount,0)-isnull(ApplyCheckCount,0)) as CheckedCount from officedba.ManufactureTaskDetail where ID=@ID";
                    mancomm.Parameters.Add(SqlHelper.GetParameter("@ID", FromBillID));
                    mancomm.CommandText = mansql;
                    DataTable mandt = SqlHelper.ExecuteSearch(mancomm);
                    if (mandt.Rows.Count > 0)
                    {
                        ProductCount2 = mandt.Rows[0]["ProductedCount"].ToString();
                        RealCheckedCount2 = mandt.Rows[0]["CheckedCount1"].ToString();
                        returnvalue = mandt.Rows[0]["CheckedCount"].ToString();
                        if (RealCheckedCount2 != "" && ProductCount2 != "")
                        {
                            if (Convert.ToDecimal(ProductCount2) == Convert.ToDecimal(RealCheckedCount2))
                            {
                                returnvalue = "none";
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
            return returnvalue;
        }
        //---------------------------------------------------------------------------------------------------------报告单打印需要
        public static DataTable GetReportInfo(int ID)
        {
            #region 查询语句
            //查询SQL拼写
            string sqlStr = @" SELECT pi1.ProdNo,pi1.ProductName,pi1.Specification,cut.CodeName,ei.EmployeeName AS ApplyUserName,di.DeptName AS ApplyDeptName
,ei2.EmployeeName AS CheckerName,di2.DeptName AS CheckDeptName,ei3.EmployeeName AS CreatorName,ei4.EmployeeName AS ConfirmorName
,ei5.EmployeeName AS CloserName,ei6.EmployeeName AS PrincipalName,di3.DeptName AS DeptName
,CASE qcr.BillStatus WHEN '1' THEN '制单' WHEN '2' THEN '执行' WHEN '3' THEN '变更' WHEN '4' THEN '手工结单' WHEN '5' THEN '自动结单' ELSE '' END AS BillStatus
,CASE qcr.CorpBigType WHEN '1' THEN '客户' WHEN '2' THEN '供应商' WHEN '3' THEN '竞争对手' WHEN '4' THEN '银行' WHEN '5' THEN '外协加工厂' WHEN '6' THEN '运输商' WHEN '7' THEN '其他' ELSE '' END AS CorpBigType
,CASE qcr.CheckMode WHEN '1' THEN '全检' WHEN '2' THEN '抽检' WHEN '3' THEN '临检' ELSE '空' END AS CheckMode
,CASE qcr.CheckType WHEN '1' THEN '进货检验' WHEN '2' THEN '过程检验' WHEN '3' THEN '最终检验' ELSE '' END AS CheckType
,CASE qcr.FromType WHEN '0' THEN '无来源' WHEN '1' THEN '质检申请单' WHEN '2' THEN '质检报告单' WHEN '3' THEN '生产任务单' WHEN '4' THEN '采购到货单' ELSE '' END AS FromType
,CASE qcr.IsPass WHEN '0' THEN '不合格' WHEN '1' THEN '合格'  ELSE '' END AS IsPass
,CASE qcr.isRecheck WHEN '0' THEN '否' WHEN '1' THEN '是' ELSE '' END AS isRecheck
,isnull(substring(CONVERT(varchar,qcr.CreateDate,120),0,11),'') as CreateDate
,isnull(substring(CONVERT(varchar,qcr.CheckDate,120),0,11),'') as CheckDate
,isnull(substring(CONVERT(varchar,qcr.ConfirmDate,120),0,11),'') as ConfirmDate
,isnull(substring(CONVERT(varchar,qcr.CloseDate,120),0,11),'') as CloseDate
,isnull(substring(CONVERT(varchar,qcr.ModifiedDate,120),0,11),'') as ModifiedDate
,qcr.ReportNo,qcr.Title,qcr.ExtField1,qcr.ExtField2,qcr.ExtField3,qcr.ExtField4
,qcr.ExtField5,qcr.ExtField6,qcr.ExtField7,qcr.ExtField8,qcr.ExtField9,qcr.ExtField10
,qcr.FromReportNo,qcr.OtherCorpName,qcr.CheckContent,qcr.CheckNum,qcr.SampleNum
,qcr.CheckMode,qcr.PassNum,qcr.NoPass,qcr.PassPercent,qcr.CheckStandard,qcr.CheckResult,qcr.Remark
,qcr.ModifiedUserID
FROM officedba.QualityCheckReport qcr
LEFT JOIN officedba.ProductInfo pi1 ON qcr.CompanyCD=pi1.CompanyCD AND qcr.ProductID=pi1.ID
LEFT JOIN officedba.CodeUnitType cut ON pi1.CompanyCD=cut.CompanyCD AND pi1.UnitID=cut.ID
LEFT JOIN officedba.EmployeeInfo ei ON qcr.CompanyCD=ei.CompanyCD AND qcr.ApplyUserID=ei.ID
LEFT JOIN officedba.DeptInfo di ON qcr.CompanyCD=di.CompanyCD AND qcr.ApplyDeptID=di.ID
LEFT JOIN officedba.EmployeeInfo ei2 ON qcr.CompanyCD=ei2.CompanyCD AND qcr.Checker=ei2.ID
LEFT JOIN officedba.DeptInfo di2 ON qcr.CompanyCD=di2.CompanyCD AND qcr.CheckDeptId=di2.ID
LEFT JOIN officedba.DeptInfo di3 ON qcr.CompanyCD=di3.CompanyCD AND qcr.DeptID=di3.ID
LEFT JOIN officedba.EmployeeInfo ei3 ON qcr.CompanyCD=ei3.CompanyCD AND qcr.Creator=ei3.ID
LEFT JOIN officedba.EmployeeInfo ei4 ON qcr.CompanyCD=ei4.CompanyCD AND qcr.Confirmor=ei4.ID
LEFT JOIN officedba.EmployeeInfo ei5 ON qcr.CompanyCD=ei5.CompanyCD AND qcr.Closer=ei5.ID
LEFT JOIN officedba.EmployeeInfo ei6 ON qcr.CompanyCD=ei6.CompanyCD AND qcr.Principal=ei6.ID
WHERE qcr.ID=@ID ";
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID.ToString()));

            //指定命令的SQL文
            comm.CommandText = sqlStr;
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        /// <summary>
        /// 获取质检报告明细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetReportDetailInfo(int ID, string TypeFlag, string TypeCode, string CompanyCD)
        {
            #region 查询语句
            //查询SQL拼写
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select * from (");
            sql.AppendLine("SELECT  c.CheckStandard, e.EmployeeName as Checker, d.DeptName as CheckDeptID,isnull(c.CheckValue,'') as CheckValue, c.CheckNum, c.PassNum, c.NotPassNum,");
            sql.AppendLine("        isnull(c.StandardValue,'') as StandardValue, c.NormUpLimit, c.LowerLimit, isnull(c.CheckResult,'') as CheckResult");
            sql.AppendLine(",CASE c.isPass WHEN '1' THEN '合格' WHEN '0' THEN '不合格'  ELSE '' END AS isPass");
            sql.AppendLine("          ,c.Remark");
            sql.AppendLine("        ,isnull(f.TypeName,'') as  CheckItem ");
            sql.AppendLine("FROM    officedba.CheckReportDetail AS c LEFT  JOIN ");
            sql.AppendLine("        officedba.EmployeeInfo AS e ON c.Checker = e.ID left JOIN ");
            sql.AppendLine("        officedba.DeptInfo AS d ON c.CheckDeptID = d.ID ");
            sql.AppendLine("        left join officedba.CodePublicType as f on c.CheckItem=f.ID");
            sql.AppendLine(" where  c.ReportNo=(select top 1 ReportNo from officedba.QualityCheckReport where ID=@ID and CompanyCD=@CompanyCD) and c.CompanyCD=@CompanyCD");
            sql.AppendLine(") as info ");

            #endregion
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", ID.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            //指定命令的SQL文
            comm.CommandText = sql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }

        /// <summary>
        /// 更新附件字段
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="Attachment">附件URL</param>
        public static void UpDateAttachment(int ID, string Attachment)
        {
            string sql = "UPDATE officedba.QualityCheckReport SET Attachment = '{1}' WHERE ID={0}";
            sql = string.Format(sql, ID, Attachment);
            SqlHelper.ExecuteTransSql(sql, new SqlParameter[] { });
        }


    }
}

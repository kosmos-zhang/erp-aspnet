/**********************************************
 * 类作用：   期初库存数据库层处理
 * 建立人：   肖合明
 * 建立时间： 2009/04/01
 ***********************************************/
using System;
using XBase.Model.Office.StorageManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using XBase.Common;

namespace XBase.Data.Office.StorageManager
{
    public class ManufactureTaskInfoDBHelper
    {
        #region 生产任务单及其明细信息列表(弹出层显示)
        public static DataTable GetMTDetailInfo(string CompanyCD, string TaskNo, string Subject)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("select a.ID,a.TaskNo,a.Subject,a.Principal,");
            sql.AppendLine("ISNULL(s.EmployeeName,'') as PrincipalName,a.DeptID,");
            sql.AppendLine("ISNULL(h.DeptName,'') as DeptName");
            sql.AppendLine(",case a.ManufactureType when '0' then '普通' when '1' then '返修' when '2' then '拆件' end as ManufactureType");
            sql.AppendLine(",CONVERT(varchar(10),a.CreateDate, 23) as CreateDate");
            sql.AppendLine(",b.ID as DetailID,b.ProductID,ISNULL(c1.CodeName,'') as UnitName,b.ProductedCount JiBenCount,");
            sql.AppendLine("ISNULL(i.ProdNo,'') as ProdNo,ISNULL(i.StandardCost,0) as UnitPrice,");
            sql.AppendLine("ISNULL(i.ProductName,'') as ProductName");
            sql.AppendLine(",ISNULL(b.ProductedCount,0) as ProductCount,ISNULL(b.InCount,0) as InCount");
            sql.AppendLine(" from officedba.ManufactureTaskDetail b");
            sql.AppendLine(" left outer join officedba.ManufactureTask a on b.TaskNo=a.TaskNo and a.CompanyCD=b.CompanyCD ");
            sql.AppendLine(" left join officedba.DeptInfo h on a.DeptID=h.ID");
            sql.AppendLine(" left join officedba.ProductInfo i on b.ProductID=i.ID");
            sql.AppendLine(" left join officedba.EmployeeInfo s on a.Principal=s.ID");
            sql.AppendLine(" left join officedba.CodeUnitType c1 on c1.ID = i.UnitID ");
            sql.AppendLine(" where a.CompanyCD='" + CompanyCD + "' and a.BillStatus=2");
            sql.AppendLine(" and (ISNULL(b.ProductedCount,0)-ISNULL(b.InCount,0))>0");
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            if (!string.IsNullOrEmpty(TaskNo))
            {
                sql.AppendLine(" and a.TaskNo like '%'+ @TaskNo +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", TaskNo));
            }
            if (!string.IsNullOrEmpty(Subject))
            {
                sql.AppendLine(" and a.Subject like '%'+ @Subject +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Subject", Subject));
            }
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);

        }
        #endregion

        /// <summary>
        /// 根据生产任务单编号，获取基本信息
        /// </summary>
        /// <param name="TaskNo"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetMTInfo(string TaskNo, string CompanyCD)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select a.ID,a.TaskNo,a.DeptID,i.DeptName,a.ManufactureType,");
            sql.AppendLine("a.Principal,h.EmployeeName as PrincipalName");
            sql.AppendLine("from officedba.ManufactureTask a left join officedba.DeptInfo i on a.DeptID=i.ID ");
            sql.AppendLine("left join officedba.EmployeeInfo h on h.ID=a.Principal");
            sql.AppendLine("where a.CompanyCD='" + CompanyCD + "' and TaskNo='" + TaskNo + "'");
            return SqlHelper.ExecuteSql(sql.ToString());
        }


        /// <summary>
        /// 根据传过来的明细ID数组来获取明细列表
        /// </summary>
        /// <param name="strDetailIDList"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetInfoByDetalIDList(string strDetailIDList, string CompanyCD)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select a.ID,a.TaskNo,p.StorageID,p.IsBatchNo,a.UsedUnitID   ");
            sql.AppendLine(",a.ProductID,ISNULL(p.ProdNo,'') as ProdNo,ISNULL(p.ProductName,'') as ProductName  ");
            sql.AppendLine(" ,ISNULL(q.CodeName,'') as UnitID                                                   ");
            sql.AppendLine(" ,ISNULL(p.Specification,'') as Specification                                       ");
            sql.AppendLine(" ,ISNULL(a.ProductedCount,0) as FromBillCount                                       ");
            sql.AppendLine(" ,ISNULL(a.InCount,0) as InCount");
            sql.AppendLine("  ,ISNULL(a.ProductedCount,0)-ISNULL(a.InCount,0) as ProductCount                     ");
            sql.AppendLine(",ISNULL(p.StandardCost,0) as UnitPrice,a.FromType                                    ");
            sql.AppendLine(",case a.FromType when '0' then '无来源' else '生产任务知单' end as FromTypeName     ");
            sql.AppendLine(",a.SortNo as FromLineNo from officedba.ManufactureTaskDetail a                      ");
            sql.AppendLine(" left join officedba.ProductInfo p on p.ID=a.ProductID                              ");
            sql.AppendLine(" left join officedba.CodeUnitType q on q.ID=p.UnitID ");
            sql.AppendLine(" where a.CompanyCD='" + CompanyCD + "' and a.ID in ( ");
            for (int i = 0; i < strDetailIDList.Split(',').Length - 1; i++)
            {
                sql.AppendLine("'" + strDetailIDList.Split(',')[i] + "', ");
            }
            string strSql = sql.ToString().Remove(sql.ToString().LastIndexOf(','));
            strSql += ")"; 
            return SqlHelper.ExecuteSql(strSql);
        }

    }
}

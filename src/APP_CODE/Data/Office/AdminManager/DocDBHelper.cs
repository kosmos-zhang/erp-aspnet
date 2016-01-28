using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using XBase.Model.Office.AdminManager;
using System.Data.SqlTypes;


namespace XBase.Data.Office.AdminManager
{
    public class DocDBHelper
    {
        #region 添加文档信息的方法
        /// <summary>
        /// 添加文档信息的方法
        /// </summary>
        /// <param name="DocM">文档信息</param>
        /// <returns>被添加文档ID</returns>
        public static int DocAdd(DocModel DocM)
        {
            try
            {
                #region 设置参数
                SqlParameter[] param = new SqlParameter[12];
                param[0] = SqlHelper.GetParameter("@CompanyCD     ",DocM.CompanyCD     );
                param[1] = SqlHelper.GetParameter("@DocumentNo    ",DocM.DocumentNo    );
                param[2] = SqlHelper.GetParameter("@DocType       ",DocM.DocType       );
                param[3] = SqlHelper.GetParameter("@DocTitle      ",DocM.DocTitle      );
                param[4] = SqlHelper.GetParameter("@UploadUserID  ",DocM.UploadUserID  );
                param[5] = SqlHelper.GetParameter("@DeptID        ",DocM.DeptID        );                
                param[6] = SqlHelper.GetParameter("@UploadDate", DocM.UploadDate == null
                                       ? SqlDateTime.Null
                                       : SqlDateTime.Parse(DocM.UploadDate.ToString()));
                param[7] = SqlHelper.GetParameter("@DocumentName  ",DocM.DocumentName  );
                //param[8] = SqlHelper.GetParameter("@DocumentType  ",DocM.DocumentType  );
                param[8] = SqlHelper.GetParameter("@DocumentURL   ",DocM.DocumentURL   );                
                param[9] = SqlHelper.GetParameter("@ModifiedDate", DocM.ModifiedDate == null
                                       ? SqlDateTime.Null
                                       : SqlDateTime.Parse(DocM.ModifiedDate.ToString()));
                param[10] = SqlHelper.GetParameter("@ModifiedUserID",DocM.ModifiedUserID);


                SqlParameter paramid = new SqlParameter("@id", SqlDbType.Int);
                paramid.Direction = ParameterDirection.Output;
                param[11] = paramid;
                #endregion

                //创建命令
                SqlCommand comm = new SqlCommand();
                SqlHelper.ExecuteTransStoredProcedure("officedba.insertDoc", comm, param);
                int DocID = Convert.ToInt32(comm.Parameters["@id"].Value);

                return DocID;
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region 根据文档ID修改文档信息
        /// <summary>
        /// 根据文档ID修改文档信息
        /// </summary>
        /// <param name="DocM">文档信息</param>
        /// <returns>bool值</returns>
        public static bool UpdateDoc(DocModel DocM)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE officedba.DocInfo set ");
                sql.AppendLine("CompanyCD     =@CompanyCD     ,");
                sql.AppendLine("DocType       =@DocType       ,");
                sql.AppendLine("DocTitle      =@DocTitle      ,");
                sql.AppendLine("UploadUserID  =@UploadUserID  ,");
                sql.AppendLine("DeptID        =@DeptID        ,");
                sql.AppendLine("UploadDate    =@UploadDate    ,");
                sql.AppendLine("DocumentName  =@DocumentName  ,");
                //sql.AppendLine("DocumentType  =@DocumentType  ,");
                sql.AppendLine("DocumentURL   =@DocumentURL   ,");
                sql.AppendLine("ModifiedDate  =@ModifiedDate  ,");
                sql.AppendLine("ModifiedUserID=@ModifiedUserID");
                sql.AppendLine(" WHERE ");
                sql.AppendLine("ID = @ID ");

                SqlParameter[] param = new SqlParameter[11];
                param[0] = SqlHelper.GetParameter("@ID      ", DocM.ID);
                param[1] = SqlHelper.GetParameter("@CompanyCD     ",DocM.CompanyCD     );                
                param[2] = SqlHelper.GetParameter("@DocType       ",DocM.DocType       );
                param[3] = SqlHelper.GetParameter("@DocTitle      ",DocM.DocTitle      );
                param[4] = SqlHelper.GetParameter("@UploadUserID  ",DocM.UploadUserID  );
                param[5] = SqlHelper.GetParameter("@DeptID        ",DocM.DeptID        );               
                param[6] = SqlHelper.GetParameter("@UploadDate", DocM.UploadDate == null
                                        ? SqlDateTime.Null
                                        : SqlDateTime.Parse(DocM.UploadDate.ToString()));
                param[7] = SqlHelper.GetParameter("@DocumentName  ",DocM.DocumentName  );
                //param[8] = SqlHelper.GetParameter("@DocumentType  ",DocM.DocumentType  );
                param[8] = SqlHelper.GetParameter("@DocumentURL   ",DocM.DocumentURL   );                
                param[9] = SqlHelper.GetParameter("@ModifiedDate", DocM.ModifiedDate == null
                                        ? SqlDateTime.Null
                                        : SqlDateTime.Parse(DocM.ModifiedDate.ToString()));
                param[10] = SqlHelper.GetParameter("@ModifiedUserID",DocM.ModifiedUserID);


                SqlHelper.ExecuteTransSql(sql.ToString(), param);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 根据条件检索文档信息
        /// <summary>
        /// 根据条件检索文档信息
        /// </summary>
        /// <param name="DocM">文档信息</param>
        /// <param name="FileDateBegin">开始时间</param>
        /// <param name="FileDateEnd">结束时间</param>       
        /// <returns>文档列表信息</returns>
        public static DataTable GetDocBycondition(DocModel DocM, string FileDateBegin, string FileDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region sql语句
                string sql = "select" +
                       " di.ID,di.DocumentNo,di.DocumentName,cd.CodeName,isnull(ei.EmployeeName,'') EmployeeName," +
                       " isnull(CONVERT(varchar(100), di.UploadDate, 23),'') UploadDate," +
                       " di.DocumentURL" +
                   " from " +
                       " officedba.DocInfo di" +
                   " left join officedba.CodeDocType cd" +
                   " on cd.id = di.DocType" +
                   " left join officedba.EmployeeInfo ei" +
                   " on ei.id = di.UploadUserID" +
                   " where" +
                   " di.CompanyCD = '" + DocM.CompanyCD + "'";   
                            
                if (DocM.DocTitle != "")
                    sql += " and di.DocTitle like '%" + DocM.DocTitle + "%'";
                if (DocM.UploadUserID != 0)
                    sql += " and di.UploadUserID = " + DocM.UploadUserID + "";
                if (DocM.DocType != 0)
                    sql += " and di.DocType = " + DocM.DocType + "";
                if (FileDateBegin != "")
                    sql += " and di.UploadDate >= '" + FileDateBegin + "'";
                if (FileDateEnd != "")
                    sql += " and di.UploadDate <= '" + FileDateEnd + "'";

                #endregion

                //return SqlHelper.ExecuteSql(sql);
                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 根据ID获得文档详细信息
        /// <summary>
        /// 根据ID获得文档详细信息
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="DocID">文档ID</param>
        /// <returns>文档信息</returns>
        public static DataTable GetDocByID(string CompanyCD, int DocID)
        {
            try
            {
                string sql = "select " +
                                   " a.ID,a.CompanyCD,a.DocumentNo,a.DocType,cdt.CodeName,a.DocTitle," +
                                   " a.UploadUserID,ei.EmployeeName,a.DeptID,dpt.DeptName," +
                                   " a.UploadDate,a.DocumentName,a.DocumentURL," +
                                   " a.ModifiedDate,a.ModifiedUserID" +
                               " from " +
                                   " (select " +
                                       " di.ID,di.CompanyCD,	di.DocumentNo," +
                                       " di.DocType,	di.DocTitle,	di.UploadUserID," +
                                       " di.DeptID,	CONVERT(varchar(100),di.UploadDate, 23) UploadDate,	di.DocumentName," +
                                       " di.DocumentURL,	CONVERT(varchar(100),di.ModifiedDate, 23) ModifiedDate," +
                                       " di.ModifiedUserID" +
                                   " from " +
                                       " officedba.DocInfo di" +
                                   " where di.id = @id " +
                                   " and di.CompanyCD = @CompanyCD) a" +
                               " left join officedba.EmployeeInfo ei on ei.id = a.UploadUserID" +
                               " left join officedba.DeptInfo dpt on dpt.id = a.DeptID" +
                               " left join officedba.CodeDocType cdt on cdt.id = a.DocType ";

                SqlParameter[] param = new SqlParameter[2];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@id", DocID);
                return SqlHelper.ExecuteSql(sql, param);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 获取文档分类
        /// <summary>
        /// 获取文档分类
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetDocType(string CompanyCD)
        {
            try
            {
                string sql = "select " +
                                    "ID,CodeName,SupperID" +
                              " from " +
                                    "officedba.CodeDocType" +
                              " where " +
                                    "CompanyCD=@CompanyCD and UsedStatus=1";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                return SqlHelper.ExecuteSql(sql, param);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 导出文档列表
        /// <summary>
        /// 导出文档列表
        /// </summary>
        /// <param name="DocM"></param>
        /// <param name="FileDateBegin"></param>
        /// <param name="FileDateEnd"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable ExportDoc(DocModel DocM, string FileDateBegin, string FileDateEnd, string ord)
        {
            try
            {
                #region sql语句
                string sql = "select" +
                       " di.ID,di.DocumentNo,di.DocumentName,cd.CodeName,isnull(ei.EmployeeName,'') EmployeeName," +
                       " isnull(CONVERT(varchar(100), di.UploadDate, 23),'') UploadDate," +
                       " di.DocumentURL" +
                   " from " +
                       " officedba.DocInfo di" +
                   " left join officedba.CodeDocType cd" +
                   " on cd.id = di.DocType" +
                   " left join officedba.EmployeeInfo ei" +
                   " on ei.id = di.UploadUserID" +
                   " where" +
                   " di.CompanyCD = '" + DocM.CompanyCD + "'";

                if (DocM.DocTitle != "")
                    sql += " and di.DocTitle like '%" + DocM.DocTitle + "%'";
                if (DocM.UploadUserID != 0)
                    sql += " and di.UploadUserID = " + DocM.UploadUserID + "";
                if (DocM.DocType != 0)
                    sql += " and di.DocType = " + DocM.DocType + "";
                if (FileDateBegin != "")
                    sql += " and di.UploadDate >= '" + FileDateBegin + "'";
                if (FileDateEnd != "")
                    sql += " and di.UploadDate <= '" + FileDateEnd + "'";

                #endregion

                return SqlHelper.ExecuteSql(sql);
            }
            catch
            {
                return null;
            }
        }
        #endregion 
    }
}

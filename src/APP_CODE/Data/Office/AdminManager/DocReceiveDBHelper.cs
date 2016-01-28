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
    public class DocReceiveDBHelper
    {
        #region 添加收文信息的方法
        /// <summary>
        /// 添加收文信息的方法
        /// </summary>
        /// <param name="DocReceiveM">收文信息</param>
        /// <returns>被添加收文ID</returns>
        public static int DocReceiveAdd(DocReceiveModel DocReceiveM)
        {
            try
            {
                #region 设置参数
                SqlParameter[] param = new SqlParameter[25];
                param[0] = SqlHelper.GetParameter("@CompanyCD       ",DocReceiveM.CompanyCD       ); 
                param[1] = SqlHelper.GetParameter("@ReceiveDocNo    ",DocReceiveM.ReceiveDocNo    );
                param[2] = SqlHelper.GetParameter("@ReceiveDocTypeID",DocReceiveM.ReceiveDocTypeID);
                param[3] = SqlHelper.GetParameter("@SecretLevel     ",DocReceiveM.SecretLevel     );
                param[4] = SqlHelper.GetParameter("@Critical        ",DocReceiveM.Critical        );
                param[5] = SqlHelper.GetParameter("@FileDate", DocReceiveM.FileDate == null
                                       ? SqlDateTime.Null
                                       : SqlDateTime.Parse(DocReceiveM.FileDate.ToString()));
                param[6] = SqlHelper.GetParameter("@FileNo          ",DocReceiveM.FileNo          );
                param[7] = SqlHelper.GetParameter("@FileCompany     ",DocReceiveM.FileCompany     );
                param[8] = SqlHelper.GetParameter("@FileTitle       ",DocReceiveM.FileTitle       );
                param[9] = SqlHelper.GetParameter("@FileReason      ",DocReceiveM.FileReason      );
                param[10] = SqlHelper.GetParameter("@KeyWord         ",DocReceiveM.KeyWord         );
                param[11] = SqlHelper.GetParameter("@Description     ",DocReceiveM.Description     );
                param[12] = SqlHelper.GetParameter("@DeptID          ",DocReceiveM.DeptID          );
                param[13] = SqlHelper.GetParameter("@BackerNo        ",DocReceiveM.BackerNo        );
                param[14] = SqlHelper.GetParameter("@Backer          ",DocReceiveM.Backer          );
                param[15] = SqlHelper.GetParameter("@BackDate", DocReceiveM.BackDate == null
                                       ? SqlDateTime.Null
                                       : SqlDateTime.Parse(DocReceiveM.BackDate.ToString()));
                param[16] = SqlHelper.GetParameter("@BackContent     ",DocReceiveM.BackContent     );
                param[17] = SqlHelper.GetParameter("@Remark          ",DocReceiveM.Remark          );
                param[18] = SqlHelper.GetParameter("@RegisterUserID  ",DocReceiveM.RegisterUserID  );                
                param[19] = SqlHelper.GetParameter("@UploadDate", DocReceiveM.UploadDate == null
                                       ? SqlDateTime.Null
                                       : SqlDateTime.Parse(DocReceiveM.UploadDate.ToString()));
                param[20] = SqlHelper.GetParameter("@DocumentName    ",DocReceiveM.DocumentName    );
                param[21] = SqlHelper.GetParameter("@DocumentURL     ",DocReceiveM.DocumentURL     );                
                param[22] = SqlHelper.GetParameter("@ModifiedDate", DocReceiveM.ModifiedDate == null
                                       ? SqlDateTime.Null
                                       : SqlDateTime.Parse(DocReceiveM.ModifiedDate.ToString()));
                param[23] = SqlHelper.GetParameter("@ModifiedUserID  ",DocReceiveM.ModifiedUserID  );

                SqlParameter paramid = new SqlParameter("@id", SqlDbType.Int);
                paramid.Direction = ParameterDirection.Output;
                param[24] = paramid;
                #endregion

                //创建命令
                SqlCommand comm = new SqlCommand();
                SqlHelper.ExecuteTransStoredProcedure("officedba.insertDocReceive", comm, param);
                int DocReceiveID = Convert.ToInt32(comm.Parameters["@id"].Value);

                return DocReceiveID;
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region 根据收文ID修改收文信息
        /// <summary>
        /// 根据收文ID修改收文信息
        /// </summary>
        /// <param name="DocReceiveM">收文信息</param>
        /// <returns>bool值</returns>
        public static bool UpdateDocReceive(DocReceiveModel DocReceiveM)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE officedba.DocReceiveInfo set ");
                sql.AppendLine("CompanyCD       =@CompanyCD       ,");
                //sql.AppendLine("ReceiveDocNo    =@ReceiveDocNo    ,");
                sql.AppendLine("ReceiveDocTypeID=@ReceiveDocTypeID,");
                sql.AppendLine("SecretLevel     =@SecretLevel     ,");
                sql.AppendLine("Critical        =@Critical        ,");
                sql.AppendLine("FileDate        =@FileDate        ,");
                sql.AppendLine("FileNo          =@FileNo          ,");
                sql.AppendLine("FileCompany     =@FileCompany     ,");
                sql.AppendLine("FileTitle       =@FileTitle       ,");
                sql.AppendLine("FileReason      =@FileReason      ,");
                sql.AppendLine("KeyWord         =@KeyWord         ,");
                sql.AppendLine("Description     =@Description     ,");
                sql.AppendLine("DeptID          =@DeptID          ,");
                sql.AppendLine("BackerNo        =@BackerNo        ,");
                sql.AppendLine("Backer          =@Backer          ,");
                sql.AppendLine("BackDate        =@BackDate        ,");
                sql.AppendLine("BackContent     =@BackContent     ,");
                sql.AppendLine("Remark          =@Remark          ,");
                sql.AppendLine("RegisterUserID  =@RegisterUserID  ,");
                sql.AppendLine("UploadDate      =@UploadDate      ,");
                sql.AppendLine("DocumentName    =@DocumentName    ,");
                sql.AppendLine("DocumentURL     =@DocumentURL     ,");
                sql.AppendLine("ModifiedDate    =@ModifiedDate    ,");
                sql.AppendLine("ModifiedUserID  =@ModifiedUserID   ");
                sql.AppendLine(" WHERE ");
                sql.AppendLine("ID = @ID ");

                SqlParameter[] param = new SqlParameter[24];
                param[0] = SqlHelper.GetParameter("@ID      ", DocReceiveM.ID);
                param[1] = SqlHelper.GetParameter("@CompanyCD       ",DocReceiveM.CompanyCD       );
                param[2] = SqlHelper.GetParameter("@ReceiveDocTypeID",DocReceiveM.ReceiveDocTypeID);
                param[3] = SqlHelper.GetParameter("@SecretLevel     ",DocReceiveM.SecretLevel     );
                param[4] = SqlHelper.GetParameter("@Critical        ",DocReceiveM.Critical        );

                //param[5] = SqlHelper.GetParameter("@FileDate        ",DocReceiveM.FileDate        );
                param[5] = SqlHelper.GetParameter("@FileDate", DocReceiveM.FileDate == null
                                        ? SqlDateTime.Null
                                        : SqlDateTime.Parse(DocReceiveM.FileDate.ToString()));
                param[6] = SqlHelper.GetParameter("@FileNo          ",DocReceiveM.FileNo          );
                param[7] = SqlHelper.GetParameter("@FileCompany     ",DocReceiveM.FileCompany     );
                param[8] = SqlHelper.GetParameter("@FileTitle       ",DocReceiveM.FileTitle       );
                param[9] = SqlHelper.GetParameter("@FileReason      ",DocReceiveM.FileReason      );
                param[10] = SqlHelper.GetParameter("@KeyWord         ",DocReceiveM.KeyWord         );
                param[11] = SqlHelper.GetParameter("@Description     ",DocReceiveM.Description     );
                param[12] = SqlHelper.GetParameter("@DeptID          ",DocReceiveM.DeptID          );
                param[13] = SqlHelper.GetParameter("@BackerNo        ",DocReceiveM.BackerNo        );
                param[14] = SqlHelper.GetParameter("@Backer          ",DocReceiveM.Backer          );

                //param[15] = SqlHelper.GetParameter("@BackDate        ",DocReceiveM.BackDate        );
                param[15] = SqlHelper.GetParameter("@BackDate", DocReceiveM.BackDate == null
                                        ? SqlDateTime.Null
                                        : SqlDateTime.Parse(DocReceiveM.BackDate.ToString()));

                param[16] = SqlHelper.GetParameter("@BackContent     ",DocReceiveM.BackContent     );
                param[17] = SqlHelper.GetParameter("@Remark          ",DocReceiveM.Remark          );
                param[18] = SqlHelper.GetParameter("@RegisterUserID  ",DocReceiveM.RegisterUserID  );
                //param[19] = SqlHelper.GetParameter("@UploadDate      ",DocReceiveM.UploadDate      );
                param[19] = SqlHelper.GetParameter("@UploadDate", DocReceiveM.UploadDate == null
                                        ? SqlDateTime.Null
                                        : SqlDateTime.Parse(DocReceiveM.UploadDate.ToString()));

                param[20] = SqlHelper.GetParameter("@DocumentName    ",DocReceiveM.DocumentName    );
                param[21] = SqlHelper.GetParameter("@DocumentURL     ",DocReceiveM.DocumentURL     );
                //param[22] = SqlHelper.GetParameter("@ModifiedDate    ",DocReceiveM.ModifiedDate    );
                param[22] = SqlHelper.GetParameter("@ModifiedDate", DocReceiveM.ModifiedDate == null
                                        ? SqlDateTime.Null
                                        : SqlDateTime.Parse(DocReceiveM.ModifiedDate.ToString()));
                param[23] = SqlHelper.GetParameter("@ModifiedUserID  ",DocReceiveM.ModifiedUserID  );

                SqlHelper.ExecuteTransSql(sql.ToString(), param);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 根据条件检索收文信息
        /// <summary>
        /// 根据条件检索收文信息
        /// </summary>
        /// <param name="DocReceiveM">收文信息</param>
        /// <param name="FileDateBegin">开始时间</param>
        /// <param name="FileDateEnd">结束时间</param>
        /// <param name="FileCompany">来文单位</param>
        /// <returns>收文列表信息</returns>
        public static DataTable GetDocReceiveBycondition(DocReceiveModel DocReceiveM, string FileDateBegin, string FileDateEnd, string FileCompany, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region sql语句
                string sql = "select " +
                                   " dr.id,dr.ReceiveDocNo,cp.TypeName,CONVERT(varchar(100), dr.FileDate, 23) FileDate," +
                                   " dr.FileNo,dr.FileCompany DeptName,dr.FileTitle,dr.SecretLevel,dr.Critical" +
                               " from " +
                                   " officedba.DocReceiveInfo dr,officedba.CodePublicType cp" +
                               " where" +
                                   " dr.CompanyCD = '" + DocReceiveM.CompanyCD + "'" +
                               " and cp.id = dr.ReceiveDocTypeID";                              
                if (DocReceiveM.FileNo != "")
                    sql += " and dr.FileNo like '%" + DocReceiveM.FileNo + "%'";
                if (DocReceiveM.ReceiveDocTypeID != 0)
                    sql += " and dr.ReceiveDocTypeID = " + DocReceiveM.ReceiveDocTypeID + "";
                if (DocReceiveM.Critical != "0")
                    sql += " and dr.Critical = '" + DocReceiveM.Critical + "'";
                if (FileDateBegin.ToString() != "")
                    sql += " and dr.FileDate >= '" + FileDateBegin.ToString() + "'";
                if (FileDateEnd.ToString() != "")
                    sql += " and dr.FileDate <= '" + FileDateEnd.ToString() + "'";
                if (FileCompany != "")
                    sql += " and dr.FileCompany like '%" + FileCompany + "%'";
                if (DocReceiveM.SecretLevel != "0")
                    sql += " and dr.SecretLevel = '" + DocReceiveM.SecretLevel + "'";

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

        #region 根据ID获得收文详细信息
        /// <summary>
        /// 根据ID获得收文详细信息
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="DocReceiveID">收文ID</param>
        /// <returns>收文信息</returns>
        public static DataTable GetDocReceiveByID(string CompanyCD, int DocReceiveID)
        {
            try
            {
                string sql = "select a.id,a.CompanyCD,a.ReceiveDocNo,a.ReceiveDocTypeID," +
                                   " a.SecretLevel,a.Critical,a.FileDate,a.FileNo," +
                                   " a.FileCompany,a.FileTitle,a.FileReason,a.KeyWord," +
                                   " a.Description,a.DeptID,a.DeptName,a.BackerNo,a.Backer, ei.EmployeeName," +
                                   " a.BackDate,a.BackContent,a.Remark,a.RegisterUserID,a.RName," +
                                   " a.UploadDate,a.DocumentName,a.DocumentURL,a.ModifiedDate,a.ModifiedUserID" +
                                   " from " +
                               " (select dr.ID,dr.CompanyCD,dr.ReceiveDocNo,dr.ReceiveDocTypeID," +
                                       " dr.SecretLevel,dr.Critical,CONVERT(varchar(100),dr.FileDate, 23) FileDate,dr.FileNo," +
                                       " dr.FileCompany,dr.FileTitle,dr.FileReason,dr.KeyWord," +
                                       " dr.Description,dr.DeptID,di.DeptName,dr.BackerNo,dr.Backer," +
                                       " CONVERT(varchar(100),dr.BackDate, 23) BackDate,dr.BackContent,dr.Remark,dr.RegisterUserID,eif.EmployeeName RName," +
                                       " CONVERT(varchar(100),dr.UploadDate, 23) UploadDate,dr.DocumentName,dr.DocumentURL," +
                                       " CONVERT(varchar(100),dr.ModifiedDate, 23) ModifiedDate,dr.ModifiedUserID" +
                                " from " +
                                   " officedba.DocReceiveInfo dr,officedba.DeptInfo di,officedba.EmployeeInfo eif" +
                               " where" +
                                   " dr.id= @id" +
                               " and dr.CompanyCD = @CompanyCD " +
                               " and	di.id = dr.DeptID" +
                               " and eif.id = dr.RegisterUserID) a" +
                               " left join  officedba.EmployeeInfo ei" +
                               " on ei.id = a.backer";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@id", DocReceiveID);
                return SqlHelper.ExecuteSql(sql, param);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 收文明细
        /// <summary>
        /// 收文明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="TypeID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetDocReceiveList(string CompanyCD, string TypeID, string BeginDate, string EndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                string sql = "SELECT dr.ID, cp.TypeName, CONVERT(varchar(20), dr.FileDate, 23) AS FileDate, dr.FileNo, " +
                                   " di.DeptName, dr.FileTitle, dr.KeyWord Description, ei.EmployeeName, " +
                                   " CONVERT(varchar(20), dr.BackDate, 23) AS BackDate, dr.BackContent" +
                           " FROM officedba.DocReceiveInfo AS dr " +
                           " LEFT OUTER JOIN officedba.CodePublicType AS cp ON cp.ID = dr.ReceiveDocTypeID " +
                           " LEFT OUTER JOIN officedba.DeptInfo AS di ON di.ID = dr.DeptID" +
                           " LEFT OUTER JOIN officedba.EmployeeInfo AS ei ON ei.ID = dr.Backer" +
                           " where dr.CompanyCD = '" + CompanyCD + "'";

                if (TypeID != "0")
                    sql += " and dr.ReceiveDocTypeID = '" + TypeID + "'";
                if (BeginDate != "")
                    sql += " and dr.FileDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += " and dr.FileDate <= '" + EndDate + "'";

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 收文明细打印
        /// <summary>
        /// 收文明细打印
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="TypeID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable GetDocReceiveListPrint(string CompanyCD, string TypeID, string BeginDate, string EndDate,string ord)
        {
            try
            {
                string sql = "SELECT dr.ID, cp.TypeName, CONVERT(varchar(20), dr.FileDate, 23) AS FileDate, dr.FileNo, " +
                                   " di.DeptName, dr.FileTitle, dr.KeyWord Description, ei.EmployeeName, " +
                                   " CONVERT(varchar(20), dr.BackDate, 23) AS BackDate, dr.BackContent" +
                           " FROM officedba.DocReceiveInfo AS dr " +
                           " LEFT OUTER JOIN officedba.CodePublicType AS cp ON cp.ID = dr.ReceiveDocTypeID " +
                           " LEFT OUTER JOIN officedba.DeptInfo AS di ON di.ID = dr.DeptID" +
                           " LEFT OUTER JOIN officedba.EmployeeInfo AS ei ON ei.ID = dr.Backer" +
                           " where dr.CompanyCD = '" + CompanyCD + "'";

                if (TypeID != "0")
                    sql += " and dr.ReceiveDocTypeID = '" + TypeID + "'";
                if (BeginDate != "")
                    sql += " and dr.FileDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += " and dr.FileDate <= '" + EndDate + "'";

                sql = sql + ord;

                return SqlHelper.ExecuteSql(sql);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        public static DataTable GetSendReceiveCount(string CompanyCD, string TableName,string TypeIdName, string TypeID, string BeginDate, string EndDate )
        {
            try
            {
                string sql = "select " +                                   
                                   " cp.ID as TypeID, cp.TypeName,tmp.num" +
                               " from " +
                                   " (select " +
                                       " d." + TypeIdName + ", count(id) num " +
                                   " from " + TableName + " as d" +
                               " where d.CompanyCD='" + CompanyCD + "'";
                if (BeginDate != "")
                    sql += " and d.FileDate >='" + BeginDate + "'";
                if (EndDate != "")
                    sql += " and d.FileDate <='" + EndDate + "'";
                sql += " group by d." + TypeIdName + ") tmp" +
                        " left join officedba.CodePublicType cp on cp.id = tmp." + TypeIdName + "";
                           
                if (TypeID != "0")
                    sql += " where tmp." + TypeIdName + " = '" + TypeID + "'";
                

                
                return SqlHelper.ExecuteSql(sql);
            }
            catch
            {
                return null;
            }
        }

        #region 导出收文列表
        /// <summary>
        /// 导出收文列表
        /// </summary>
        /// <param name="DocReceiveM"></param>
        /// <param name="FileDateBegin"></param>
        /// <param name="FileDateEnd"></param>
        /// <param name="FileCompany"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable ExportDocReceive(DocReceiveModel DocReceiveM, string FileDateBegin, string FileDateEnd, string FileCompany, string ord)
        {
            try
            {
                #region sql语句
                string sql = "select " +
                                   " dr.id,dr.ReceiveDocNo,cp.TypeName,CONVERT(varchar(100), dr.FileDate, 23) FileDate," +
                                   " dr.FileNo,dr.FileCompany DeptName,dr.FileTitle,"+
                                   " (case dr.SecretLevel when '1' then '一般' when '2' then '不公开' when '3' then '秘密' when '4' then '机密' when '5' then '绝密' end)SecretLevel,"+
                                   "(case dr.Critical when '1' then '特提' when '2' then '特急' when '3' then '加急' when '4' then '平急' end) Critical" +
                               " from " +
                                   " officedba.DocReceiveInfo dr,officedba.CodePublicType cp" +
                               " where" +
                                   " dr.CompanyCD = '" + DocReceiveM.CompanyCD + "'" +
                               " and cp.id = dr.ReceiveDocTypeID";
                if (DocReceiveM.FileNo != "")
                    sql += " and dr.FileNo like '%" + DocReceiveM.FileNo + "%'";
                if (DocReceiveM.ReceiveDocTypeID != 0)
                    sql += " and dr.ReceiveDocTypeID = " + DocReceiveM.ReceiveDocTypeID + "";
                if (DocReceiveM.Critical != "0")
                    sql += " and dr.Critical = '" + DocReceiveM.Critical + "'";
                if (FileDateBegin.ToString() != "")
                    sql += " and dr.FileDate >= '" + FileDateBegin.ToString() + "'";
                if (FileDateEnd.ToString() != "")
                    sql += " and dr.FileDate <= '" + FileDateEnd.ToString() + "'";
                if (FileCompany != "")
                    sql += " and dr.FileCompany like '%" + FileCompany + "%'";
                if (DocReceiveM.SecretLevel != "0")
                    sql += " and dr.SecretLevel = '" + DocReceiveM.SecretLevel + "'";

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

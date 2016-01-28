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
    public class DocSendDBHelper
    {
        #region 添加发文信息的方法
        /// <summary>
        ///  添加发文信息的方法
        /// </summary>
        /// <param name="DocSendM">发文信息</param>
        /// <returns>发文信息ID</returns>
        public static int DocReceiveAdd(DocSendModel DocSendM)
        {
            try
            {
                #region 设置参数
                SqlParameter[] param = new SqlParameter[27];
                param[0] = SqlHelper.GetParameter("@CompanyCD     ",DocSendM.CompanyCD     ); 
                param[1] = SqlHelper.GetParameter("@DocumentNo    ",DocSendM.DocumentNo    ); 
                param[2] = SqlHelper.GetParameter("@SendDocTypeID ",DocSendM.SendDocTypeID ); 
                param[3] = SqlHelper.GetParameter("@SecretLevel   ",DocSendM.SecretLevel   ); 
                param[4] = SqlHelper.GetParameter("@EmerLevel     ",DocSendM.EmerLevel     ); 
                param[5] = SqlHelper.GetParameter("@SendDeptID    ",DocSendM.SendDeptID    ); 
                param[6] = SqlHelper.GetParameter("@FileNo        ",DocSendM.FileNo        ); 
                param[7] = SqlHelper.GetParameter("@FileTitle     ",DocSendM.FileTitle     );
                param[8] = SqlHelper.GetParameter("@FileDate", DocSendM.FileDate == null
                                      ? SqlDateTime.Null
                                      : SqlDateTime.Parse(DocSendM.FileDate.ToString()));
                param[9] = SqlHelper.GetParameter("@MainSend      ",DocSendM.MainSend      ); 
                param[10] = SqlHelper.GetParameter("@CCSend        ",DocSendM.CCSend        ); 
                param[11] = SqlHelper.GetParameter("@OutCompany    ",DocSendM.OutCompany    ); 
                param[12] = SqlHelper.GetParameter("@KeyWord       ",DocSendM.KeyWord       ); 
                param[13] = SqlHelper.GetParameter("@FileReason    ",DocSendM.FileReason    ); 
                param[14] = SqlHelper.GetParameter("@Description   ",DocSendM.Description   ); 
                param[15] = SqlHelper.GetParameter("@RegisterUserID",DocSendM.RegisterUserID); 
                param[16] = SqlHelper.GetParameter("@Backer        ",DocSendM.Backer        ); 
                param[17] = SqlHelper.GetParameter("@BackerNo      ",DocSendM.BackerNo      ); 
                param[18] = SqlHelper.GetParameter("@BackDate", DocSendM.BackDate == null
                                                     ? SqlDateTime.Null
                                                     : SqlDateTime.Parse(DocSendM.BackDate.ToString()));
                param[19] = SqlHelper.GetParameter("@BackContent   ",DocSendM.BackContent   );
                param[20] = SqlHelper.GetParameter("@UploadDate", DocSendM.UploadDate == null
                                                    ? SqlDateTime.Null
                                                    : SqlDateTime.Parse(DocSendM.UploadDate.ToString()));
                param[21] = SqlHelper.GetParameter("@DocumentName  ",DocSendM.DocumentName  );
                param[22] = SqlHelper.GetParameter("@DocumentURL  ", DocSendM.DocumentURL); 
                param[23] = SqlHelper.GetParameter("@Remark        ",DocSendM.Remark        );
                param[24] = SqlHelper.GetParameter("@ModifiedDate", DocSendM.ModifiedDate == null
                                                    ? SqlDateTime.Null
                                                    : SqlDateTime.Parse(DocSendM.ModifiedDate.ToString()));
                param[25] = SqlHelper.GetParameter("@ModifiedUserID",DocSendM.ModifiedUserID); 


                SqlParameter paramid = new SqlParameter("@id", SqlDbType.Int);
                paramid.Direction = ParameterDirection.Output;
                param[26] = paramid;
                #endregion

                //创建命令
                SqlCommand comm = new SqlCommand();
                SqlHelper.ExecuteTransStoredProcedure("officedba.insertDocSend", comm, param);
                int DocReceiveID = Convert.ToInt32(comm.Parameters["@id"].Value);

                return DocReceiveID;
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region 根据发文ID修改发文信息
        /// <summary>
        /// 根据发文ID修改发文信息
        /// </summary>
        /// <param name="DocSendM">发文信息</param>
        /// <returns>bool值</returns>
        public static bool UpdateDocSend(DocSendModel DocSendM)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE officedba.DocSendInfo set ");
                sql.AppendLine("CompanyCD     = @CompanyCD     ,");
                //sql.AppendLine("DocumentNo    = @DocumentNo    ,");
                sql.AppendLine("SendDocTypeID = @SendDocTypeID ,");
                sql.AppendLine("SecretLevel   = @SecretLevel   ,");
                sql.AppendLine("EmerLevel     = @EmerLevel     ,");
                sql.AppendLine("SendDeptID    = @SendDeptID    ,");
                sql.AppendLine("FileNo        = @FileNo        ,");
                sql.AppendLine("FileTitle     = @FileTitle     ,");
                sql.AppendLine("FileDate      = @FileDate      ,");
                sql.AppendLine("MainSend      = @MainSend      ,");
                sql.AppendLine("CCSend        = @CCSend        ,");
                sql.AppendLine("OutCompany    = @OutCompany    ,");
                sql.AppendLine("KeyWord       = @KeyWord       ,");
                sql.AppendLine("FileReason    = @FileReason    ,");
                sql.AppendLine("Description   = @Description   ,");
                sql.AppendLine("RegisterUserID= @RegisterUserID,");
                sql.AppendLine("Backer        = @Backer        ,");
                sql.AppendLine("BackerNo      = @BackerNo      ,");
                sql.AppendLine("BackDate      = @BackDate      ,");
                sql.AppendLine("BackContent   = @BackContent   ,");
                sql.AppendLine("UploadDate    = @UploadDate    ,");
                sql.AppendLine("DocumentName  = @DocumentName  ,");
                sql.AppendLine("DocumentURL   = @DocumentURL  ,");
                sql.AppendLine("Remark        = @Remark        ,");
                sql.AppendLine("ModifiedDate  = @ModifiedDate  ,");
                sql.AppendLine("ModifiedUserID= @ModifiedUserID");
                sql.AppendLine(" WHERE ");
                sql.AppendLine("ID = @ID ");

                SqlParameter[] param = new SqlParameter[26];
                param[0] = SqlHelper.GetParameter("@ID      ", DocSendM.ID);
                param[1] = SqlHelper.GetParameter("@CompanyCD     ",DocSendM.CompanyCD     );                
                param[2] = SqlHelper.GetParameter("@SendDocTypeID ",DocSendM.SendDocTypeID );
                param[3] = SqlHelper.GetParameter("@SecretLevel   ",DocSendM.SecretLevel   );
                param[4] = SqlHelper.GetParameter("@EmerLevel     ",DocSendM.EmerLevel     );
                param[5] = SqlHelper.GetParameter("@SendDeptID    ",DocSendM.SendDeptID    );
                param[6] = SqlHelper.GetParameter("@FileNo        ",DocSendM.FileNo        );
                param[7] = SqlHelper.GetParameter("@FileTitle     ",DocSendM.FileTitle     );
                param[8] = SqlHelper.GetParameter("@FileDate",DocSendM.FileDate == null
                                        ? SqlDateTime.Null
                                        : SqlDateTime.Parse(DocSendM.FileDate.ToString()));
                param[9] = SqlHelper.GetParameter("@MainSend      ",DocSendM.MainSend      );
                param[10] = SqlHelper.GetParameter("@CCSend        ",DocSendM.CCSend        );
                param[11] = SqlHelper.GetParameter("@OutCompany    ",DocSendM.OutCompany    );
                param[12] = SqlHelper.GetParameter("@KeyWord       ",DocSendM.KeyWord       );
                param[13] = SqlHelper.GetParameter("@FileReason    ",DocSendM.FileReason    );
                param[14] = SqlHelper.GetParameter("@Description   ",DocSendM.Description   );
                param[15] = SqlHelper.GetParameter("@RegisterUserID",DocSendM.RegisterUserID);
                param[16] = SqlHelper.GetParameter("@Backer        ",DocSendM.Backer        );
                param[17] = SqlHelper.GetParameter("@BackerNo      ",DocSendM.BackerNo      );                
                param[18] = SqlHelper.GetParameter("@BackDate", DocSendM.BackDate == null
                                        ? SqlDateTime.Null
                                        : SqlDateTime.Parse(DocSendM.BackDate.ToString()));
                param[19] = SqlHelper.GetParameter("@BackContent   ",DocSendM.BackContent   );                
                param[20] = SqlHelper.GetParameter("@UploadDate", DocSendM.UploadDate == null
                                       ? SqlDateTime.Null
                                       : SqlDateTime.Parse(DocSendM.UploadDate.ToString()));
                param[21] = SqlHelper.GetParameter("@DocumentName  ",DocSendM.DocumentName  );
                param[22] = SqlHelper.GetParameter("@DocumentURL  ", DocSendM.DocumentURL);
                param[23] = SqlHelper.GetParameter("@Remark        ",DocSendM.Remark        );                
                param[24] = SqlHelper.GetParameter("@ModifiedDate", DocSendM.ModifiedDate == null
                                      ? SqlDateTime.Null
                                      : SqlDateTime.Parse(DocSendM.ModifiedDate.ToString()));
                param[25] = SqlHelper.GetParameter("@ModifiedUserID",DocSendM.ModifiedUserID);


                SqlHelper.ExecuteTransSql(sql.ToString(), param);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 根据条件检索发文信息
        /// <summary>
        /// 根据条件检索发文信息
        /// </summary>
        /// <param name="DocReceiveM">发文信息</param>
        /// <param name="FileDateBegin">开始时间</param>
        /// <param name="FileDateEnd">结束时间</param>
        /// <returns>发文列表信息</returns>
        public static DataTable GetDocSendBycondition(DocSendModel DocSendM, string FileDateBegin, string FileDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region sql语句
                string sql = "select ds.ID,ds.DocumentNo,cp.TypeName,CONVERT(varchar(100), ds.FileDate, 23) FileDate," +
                                       " di.DeptName,ds.FileNo,ds.FileTitle,ds.SecretLevel,ds.EmerLevel" +
                               " from " +
                                    " officedba.DocSendInfo ds,officedba.CodePublicType cp,officedba.DeptInfo di" +
                               " where" +
                                   " di.id = ds.SendDeptID" +
                               " and" +
                                   " cp.id = ds.SendDocTypeID" +
                               " and ds.CompanyCD = '" + DocSendM.CompanyCD + "'";
                if (DocSendM.KeyWord != "")
                    sql += " and ds.KeyWord like '%" + DocSendM.KeyWord + "%'";
                if (DocSendM.FileTitle != "")
                    sql += " and ds.FileTitle like '%" + DocSendM.FileTitle + "%'";
                if (DocSendM.SendDocTypeID != 0)
                    sql += " and ds.SendDocTypeID = " + DocSendM.SendDocTypeID + "";
                if (DocSendM.SecretLevel != "0")
                    sql += " and ds.SecretLevel = '" + DocSendM.SecretLevel + "'";
                if (DocSendM.EmerLevel != "0")
                    sql += " and ds.EmerLevel = '" + DocSendM.EmerLevel + "'";
                if (FileDateBegin.ToString() != "")
                    sql += " and ds.FileDate >= '" + FileDateBegin.ToString() + "'";
                if (FileDateEnd.ToString() != "")
                    sql += " and ds.FileDate <= '" + FileDateEnd.ToString() + "'";
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

        #region 根据ID获得发文详细信息
        /// <summary>
        /// 根据ID获得发文详细信息
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="DocReceiveID">发文ID</param>
        /// <returns>发文信息</returns>
        public static DataTable GetDocSendByID(string CompanyCD, int DocSendID)
        {
            try
            {
                string sql = "select " +
                                   " ds.ID,ds.CompanyCD,ds.DocumentNo,ds.SendDocTypeID,ds.SecretLevel," +
                                   " ds.EmerLevel,ds.SendDeptID,di.DeptName,ds.FileNo,ds.FileTitle," +
                                   " CONVERT(varchar(100),ds.FileDate, 23) FileDate," +
                                   " ds.MainSend,ds.CCSend,ds.OutCompany,ds.KeyWord,ds.FileReason," +
                                   " ds.Description,ds.RegisterUserID,ei.EmployeeName,ds.Backer,ds.BackerNo," +
                                   " CONVERT(varchar(100),ds.BackDate, 23) BackDate," +
                                   " ds.BackContent,CONVERT(varchar(100),ds.UploadDate, 23) UploadDate," +
                                   " ds.DocumentName,ds.Remark,ds.ModifiedUserID,ds.DocumentURL," +
                                   " CONVERT(varchar(100),ds.ModifiedDate, 23) ModifiedDate" +
                               " from " +
                                   " officedba.DocSendInfo ds " +
                                   " left join officedba.DeptInfo di on  di.id = ds.SendDeptID " +
                                   " left join officedba.EmployeeInfo ei on ei.id = ds.RegisterUserID" +
                               " where" +
                                  " ds.id= @id" +
                              " and ds.CompanyCD = @CompanyCD ";
                             
               
                SqlParameter[] param = new SqlParameter[2];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@id", DocSendID);
                return SqlHelper.ExecuteSql(sql, param);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 发文明细表
        /// <summary>
        /// 发文明细
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
        public static DataTable GetDocSendList(string CompanyCD, string TypeID, string BeginDate, string EndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                string sql = "select ds.id,cp.TypeName," +
                                   " Convert(varchar(20),ds.FileDate,23) FileDate," +
                                       " ds.FileNo DocumentNo," +
                                       " di.DeptName," +
                                       " ds.FileTitle," +
                                       " ds.KeyWord Description," +
                                       " ds.Backer EmployeeName," +
                                       " Convert(varchar(20),ds.BackDate,23) BackDate," +
                                       " ds.BackContent " +
                               " from officedba.DocSendInfo ds" +
                               " left join officedba.CodePublicType cp on cp.id = ds.SendDocTypeID" +
                               " left join officedba.DeptInfo di on di.id = ds.SendDeptID" +                              
                           " where ds.CompanyCD = '" + CompanyCD + "'";

                if (TypeID != "0")
                    sql += " and ds.SendDocTypeID = '" + TypeID + "'";
                if (BeginDate != "")
                    sql += " and ds.FileDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += " and ds.FileDate <= '" + EndDate + "'";

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 发文明细打印
        /// <summary>
        /// 发文明细打印
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="TypeID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable GetDocSendList(string CompanyCD, string TypeID, string BeginDate, string EndDate, string ord)
        {
            try
            {
                string sql = "select ds.id,cp.TypeName," +
                                   " Convert(varchar(20),ds.FileDate,23) FileDate," +
                                       " ds.FileNo DocumentNo," +
                                       " di.DeptName," +
                                       " ds.FileTitle," +
                                       " ds.KeyWord Description," +
                                       " ds.Backer EmployeeName," +
                                       " Convert(varchar(20),ds.BackDate,23) BackDate," +
                                       " ds.BackContent " +
                               " from officedba.DocSendInfo ds" +
                               " left join officedba.CodePublicType cp on cp.id = ds.SendDocTypeID" +
                               " left join officedba.DeptInfo di on di.id = ds.SendDeptID" +                              
                           " where ds.CompanyCD = '" + CompanyCD + "'";

                if (TypeID != "0")
                    sql += " and ds.SendDocTypeID = '" + TypeID + "'";
                if (BeginDate != "")
                    sql += " and ds.FileDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += " and ds.FileDate <= '" + EndDate + "'";

                sql = sql + ord;

                return SqlHelper.ExecuteSql(sql);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 导出发文列表
        /// <summary>
        /// 导出发文列表
        /// </summary>
        /// <param name="DocSendM"></param>
        /// <param name="FileDateBegin"></param>
        /// <param name="FileDateEnd"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable ExportDocSend(DocSendModel DocSendM, string FileDateBegin, string FileDateEnd, string ord)
        {
            try
            {
                #region sql语句
                string sql = "select ds.ID,ds.DocumentNo,cp.TypeName,CONVERT(varchar(100), ds.FileDate, 23) FileDate," +
                                       " di.DeptName,ds.FileNo,ds.FileTitle,(case ds.SecretLevel when '1' then '一般' when '2' then '不公开' when '3' then '秘密' when '4' then '机密' when '5' then '绝密' end)SecretLevel,"+
                                       "(case ds.EmerLevel when '1' then '特提' when '2' then '特急' when '3' then '加急' when '4' then '平急' end) EmerLevel " +
                               " from " +
                                    " officedba.DocSendInfo ds,officedba.CodePublicType cp,officedba.DeptInfo di" +
                               " where" +
                                   " di.id = ds.SendDeptID" +
                               " and" +
                                   " cp.id = ds.SendDocTypeID" +
                               " and ds.CompanyCD = '" + DocSendM.CompanyCD + "'";
                if (DocSendM.KeyWord != "")
                    sql += " and ds.KeyWord like '%" + DocSendM.KeyWord + "%'";
                if (DocSendM.FileTitle != "")
                    sql += " and ds.FileTitle like '%" + DocSendM.FileTitle + "%'";
                if (DocSendM.SendDocTypeID != 0)
                    sql += " and ds.SendDocTypeID = " + DocSendM.SendDocTypeID + "";
                if (DocSendM.SecretLevel != "0")
                    sql += " and ds.SecretLevel = '" + DocSendM.SecretLevel + "'";
                if (DocSendM.EmerLevel != "0")
                    sql += " and ds.EmerLevel = '" + DocSendM.EmerLevel + "'";
                if (FileDateBegin.ToString() != "")
                    sql += " and ds.FileDate >= '" + FileDateBegin.ToString() + "'";
                if (FileDateEnd.ToString() != "")
                    sql += " and ds.FileDate <= '" + FileDateEnd.ToString() + "'";
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

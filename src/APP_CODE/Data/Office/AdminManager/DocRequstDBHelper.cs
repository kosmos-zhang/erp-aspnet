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
    public class DocRequstDBHelper
    {
        #region 添加请示信息的方法
        /// <summary>
        ///  添加请示信息的方法
        /// </summary>
        /// <param name="DocSendM">请示信息</param>
        /// <returns>请示信息ID</returns>
        public static int DocRequstAdd(DocRequstModel DocRequstM)
        {
            try
            {
                #region 设置参数
                SqlParameter[] param = new SqlParameter[20];
                param[0] = SqlHelper.GetParameter("@CompanyCD     ",DocRequstM.CompanyCD     );
                param[1] = SqlHelper.GetParameter("@DocumentNo    ",DocRequstM.DocumentNo    );
                param[2] = SqlHelper.GetParameter("@RequstDocType ",DocRequstM.RequstDocType );
                param[3] = SqlHelper.GetParameter("@SecretLevel   ",DocRequstM.SecretLevel   );
                param[4] = SqlHelper.GetParameter("@EmerLevel     ",DocRequstM.EmerLevel     );
                param[5] = SqlHelper.GetParameter("@Main          ",DocRequstM.Main          );
                param[6] = SqlHelper.GetParameter("@RequestMoney  ",DocRequstM.RequestMoney  );
                param[7] = SqlHelper.GetParameter("@RequestTitle  ",DocRequstM.RequestTitle  );
                param[8] = SqlHelper.GetParameter("@Description   ",DocRequstM.Description   );
                param[9] = SqlHelper.GetParameter("@RequstNo      ",DocRequstM.RequstNo      );
                param[10] = SqlHelper.GetParameter("@RequestDate", DocRequstM.RequestDate == null
                                      ? SqlDateTime.Null
                                      : SqlDateTime.Parse(DocRequstM.RequestDate.ToString()));
                param[11] = SqlHelper.GetParameter("@RequestDept      ", DocRequstM.RequestDept);
                param[12] = SqlHelper.GetParameter("@EmployeeID      ", DocRequstM.EmployeeID);
                param[13] = SqlHelper.GetParameter("@UploadUserID  ",DocRequstM.UploadUserID  );                
                param[14] = SqlHelper.GetParameter("@UploadDate", DocRequstM.UploadDate == null
                                      ? SqlDateTime.Null
                                      : SqlDateTime.Parse(DocRequstM.UploadDate.ToString()));
                param[15] = SqlHelper.GetParameter("@DocumentName  ",DocRequstM.DocumentName  );
                param[16] = SqlHelper.GetParameter("@DocumentURL   ",DocRequstM.DocumentURL   );               
                param[17] = SqlHelper.GetParameter("@ModifiedDate", DocRequstM.ModifiedDate == null
                                      ? SqlDateTime.Null
                                      : SqlDateTime.Parse(DocRequstM.ModifiedDate.ToString()));
                param[18] = SqlHelper.GetParameter("@ModifiedUserID",DocRequstM.ModifiedUserID);

                SqlParameter paramid = new SqlParameter("@id", SqlDbType.Int);
                paramid.Direction = ParameterDirection.Output;
                param[19] = paramid;
                #endregion

                //创建命令
                SqlCommand comm = new SqlCommand();
                SqlHelper.ExecuteTransStoredProcedure("officedba.insertDocRequst", comm, param);
                int DocRequstID = Convert.ToInt32(comm.Parameters["@id"].Value);

                return DocRequstID;
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
        public static bool UpdateDocRequst(DocRequstModel DocRequstM)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE officedba.DocRequstInfo set ");
                sql.AppendLine("CompanyCD     = @CompanyCD     ,");
                //sql.AppendLine("DocumentNo    = @DocumentNo    ,");
                sql.AppendLine("RequstDocType = @RequstDocType ,");
                sql.AppendLine("SecretLevel   = @SecretLevel   ,");
                sql.AppendLine("EmerLevel     = @EmerLevel     ,");
                sql.AppendLine("Main          = @Main          ,");
                sql.AppendLine("RequestMoney  = @RequestMoney  ,");
                sql.AppendLine("RequestTitle  = @RequestTitle  ,");
                sql.AppendLine("Description   = @Description   ,");
                sql.AppendLine("RequstNo      = @RequstNo      ,");
                sql.AppendLine("RequestDate      = @RequestDate,");
                sql.AppendLine("RequestDept      = @RequestDept,");
                sql.AppendLine("EmployeeID      = @EmployeeID,");
                sql.AppendLine("UploadUserID  = @UploadUserID  ,");
                sql.AppendLine("UploadDate    = @UploadDate    ,");
                sql.AppendLine("DocumentName  = @DocumentName  ,");
                sql.AppendLine("DocumentURL   = @DocumentURL   ,");
                sql.AppendLine("ModifiedDate  = @ModifiedDate  ,");
                sql.AppendLine("ModifiedUserID= @ModifiedUserID");
                sql.AppendLine(" WHERE ");
                sql.AppendLine("ID = @ID ");

                SqlParameter[] param = new SqlParameter[19];
                param[0] = SqlHelper.GetParameter("@ID      ", DocRequstM.ID);
                param[1] = SqlHelper.GetParameter("@CompanyCD     ",DocRequstM.CompanyCD     );               
                param[2] = SqlHelper.GetParameter("@RequstDocType ",DocRequstM.RequstDocType );
                param[3] = SqlHelper.GetParameter("@SecretLevel   ",DocRequstM.SecretLevel   );
                param[4] = SqlHelper.GetParameter("@EmerLevel     ",DocRequstM.EmerLevel     );
                param[5] = SqlHelper.GetParameter("@Main          ",DocRequstM.Main          );
                param[6] = SqlHelper.GetParameter("@RequestMoney  ",DocRequstM.RequestMoney  );
                param[7] = SqlHelper.GetParameter("@RequestTitle  ",DocRequstM.RequestTitle  );
                param[8] = SqlHelper.GetParameter("@Description   ",DocRequstM.Description   );
                param[9] = SqlHelper.GetParameter("@RequstNo      ",DocRequstM.RequstNo      );
                param[10] = SqlHelper.GetParameter("@RequestDate", DocRequstM.RequestDate == null
                                       ? SqlDateTime.Null
                                       : SqlDateTime.Parse(DocRequstM.RequestDate.ToString()));
                param[11] = SqlHelper.GetParameter("@RequestDept      ", DocRequstM.RequestDept);
                param[12] = SqlHelper.GetParameter("@EmployeeID      ", DocRequstM.EmployeeID);
                param[13] = SqlHelper.GetParameter("@UploadUserID  ",DocRequstM.UploadUserID  );                
                param[14] = SqlHelper.GetParameter("@UploadDate", DocRequstM.UploadDate == null
                                        ? SqlDateTime.Null
                                        : SqlDateTime.Parse(DocRequstM.UploadDate.ToString()));
                param[15] = SqlHelper.GetParameter("@DocumentName  ",DocRequstM.DocumentName  );
                param[16] = SqlHelper.GetParameter("@DocumentURL   ",DocRequstM.DocumentURL   );               
                param[17] = SqlHelper.GetParameter("@ModifiedDate", DocRequstM.ModifiedDate == null
                                        ? SqlDateTime.Null
                                        : SqlDateTime.Parse(DocRequstM.ModifiedDate.ToString()));
                param[18] = SqlHelper.GetParameter("@ModifiedUserID",DocRequstM.ModifiedUserID);
                SqlHelper.ExecuteTransSql(sql.ToString(), param);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 根据条件检索请示信息
        /// <summary>
        /// 根据条件检索请示信息的方法
        /// </summary>
        /// <param name="DocRequstM">请示信息</param>
        /// <param name="FileDateBegin">开始时间</param>
        /// <param name="FileDateEnd">结束时间</param>
        /// <returns>请示列表信息</returns>
        public static DataTable GetDocRequstBycondition(DocRequstModel DocRequstM, string FileDateBegin, string FileDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                #region sql语句
                string sql = "select" +
                                    " dr.id,dr.DocumentNo,cp.TypeName,dr.RequestTitle,dr.RequestMoney,isnull(di.DeptName,'') DeptName," +
                                    " isnull(ei.EmployeeName,'') EmployeeName,isnull(CONVERT(varchar(100), dr.RequestDate, 23),'') RequestDate,dr.EmerLevel" +
                               " from " +
                                   " officedba.DocRequstInfo dr" +
                               " left join officedba.CodePublicType cp" +
                               " on	cp.id = dr.RequstDocType" +
                               " left join officedba.DeptInfo di" +
                               " on dr.RequestDept = di.id" +
                               " left join officedba.EmployeeInfo ei" +
                               " on ei.id = dr.EmployeeID" +
                               " where dr.CompanyCD = '" + DocRequstM.CompanyCD + "'";
                               // " or dr.RequestDate is null";
                if (FileDateBegin.ToString() != "")
                    sql += " and dr.RequestDate >= '" + FileDateBegin.ToString() + "'";
                //else
                //    sql += " and (dr.RequestDate >= '" + FileDateBegin.ToString() + "' or dr.RequestDate is null)";
                if (FileDateEnd.ToString() != "")
                    sql += " and dr.RequestDate <= '" + FileDateEnd.ToString() +"'";
                //else
                //    sql += " and (dr.RequestDate <= '" + FileDateEnd.ToString() + "' or dr.RequestDate is null)";
                if (DocRequstM.RequestDept != 0)
                    sql += " and dr.RequestDept = " + DocRequstM.RequestDept.ToString() + "";
                if (DocRequstM.EmployeeID != 0)
                    sql += " and dr.EmployeeID = " + DocRequstM.EmployeeID.ToString() + "";
                if (DocRequstM.RequestTitle != "")
                    sql += " and dr.RequestTitle like '%" + DocRequstM.RequestTitle + "%'";
                if (DocRequstM.RequstDocType != 0)
                    sql += " and dr.RequstDocType = " + DocRequstM.RequstDocType + "";
                if (DocRequstM.SecretLevel != "0")
                    sql += " and dr.SecretLevel = '" + DocRequstM.SecretLevel + "'";
                if (DocRequstM.EmerLevel != "0")
                    sql += " and dr.EmerLevel = '" + DocRequstM.EmerLevel + "'";

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

        #region 根据ID获得请示详细信息
        /// <summary>
        /// 根据ID获得请示详细信息
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="DocRequstID">请示ID</param>
        /// <returns>请示信息</returns>
        public static DataTable GetDocRequstByID(string CompanyCD, int DocRequstID)
        {
            try
            {
                string sql = "select " +
                               " dr.ID,dr.CompanyCD,dr.DocumentNo,dr.RequstDocType,dr.SecretLevel," +
                               " dr.EmerLevel,dr.Main,dr.RequestMoney,dr.RequestTitle,dr.Description," +
                               " dr.RequstNo,CONVERT(varchar(100),dr.RequestDate, 23) RequestDate," +
                               " dr.RequestDept,di.DeptName,dr.EmployeeID,ei.EmployeeName,dr.UploadUserID,eiu.EmployeeName EName," +
                               " CONVERT(varchar(100),dr.UploadDate, 23) UploadDate,dr.DocumentName," +
                               " dr.DocumentURL,CONVERT(varchar(100),dr.ModifiedDate, 23) ModifiedDate," +
                               " dr.ModifiedUserID" +
                           " from officedba.DocRequstInfo dr" +
                           " left join officedba.DeptInfo di" +
                           " on di.id = dr.RequestDept " +
                           " left join officedba.EmployeeInfo ei" +
                           " on ei.id = dr.EmployeeID" +
                           " left join officedba.EmployeeInfo eiu" +
                           " on eiu.id = dr.UploadUserID" +
                           " where" +
                                " dr.id= @id" +
                           " and dr.CompanyCD = @CompanyCD ";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@id", DocRequstID);
                return SqlHelper.ExecuteSql(sql, param);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 请示明细
        /// <summary>
        /// 请示明细
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="RequestDept"></param>
        /// <param name="EmployeeID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="ord"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetDocRequestList(string CompanyCD, string RequestDept,string EmployeeID, string BeginDate, string EndDate, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                string sql = "select dr.id,di.DeptName," +
                                   " ei.EmployeeName," +
                                   " convert(varchar(20),dr.RequestDate,23) RequestDate," +
                                   " dr.RequestTitle,(case dr.EmerLevel when '1' then '特提' when '2' then '特急' when '3' then '加急' when '4' then '平急' end) Description" +
                               " from officedba.DocRequstInfo dr" +
                               " left join officedba.DeptInfo di on di.id = dr.RequestDept" +
                               " left join officedba.EmployeeInfo ei on ei.id = dr.EmployeeID" +
                           " where dr.CompanyCD = '" + CompanyCD + "'";

                if (RequestDept != "")
                    sql += " and dr.RequestDept = '" + RequestDept + "'";
                if (EmployeeID != "")
                    sql += " and dr.EmployeeID = '" + EmployeeID + "'";
                if (BeginDate != "")
                    sql += " and dr.RequestDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += " and dr.RequestDate <= '" + EndDate + "'";

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch
            {
                return null;
            }
        }
        #endregion 

        #region 请示明细打印
        /// <summary>
        /// 请示明细打印
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="RequestDept"></param>
        /// <param name="EmployeeID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable GetDocRequestListPrint(string CompanyCD, string RequestDept, string EmployeeID, string BeginDate, string EndDate,string ord)
        {
            try
            {
                string sql = "select dr.id,di.DeptName," +
                                   " ei.EmployeeName," +
                                   " convert(varchar(20),dr.RequestDate,23) RequestDate," +
                                   " dr.RequestTitle,(case dr.EmerLevel when '1' then '特提' when '2' then '特急' when '3' then '加急' when '4' then '平急' end) Description" +
                               " from officedba.DocRequstInfo dr" +
                               " left join officedba.DeptInfo di on di.id = dr.RequestDept" +
                               " left join officedba.EmployeeInfo ei on ei.id = dr.EmployeeID" +
                           " where dr.CompanyCD = '" + CompanyCD + "'";

                if (RequestDept != "")
                    sql += " and dr.RequestDept = '" + RequestDept + "'";
                if (EmployeeID != "")
                    sql += " and dr.EmployeeID = '" + EmployeeID + "'";
                if (BeginDate != "")
                    sql += " and dr.RequestDate >= '" + BeginDate + "'";
                if (EndDate != "")
                    sql += " and dr.RequestDate <= '" + EndDate + "'";
                
                sql += ord;

                return SqlHelper.ExecuteSql(sql);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 请示统计
        /// <summary>
        /// 请示统计
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="RequestDept"></param>
        /// <param name="EmployeeID"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable GetSendRequestCount(string CompanyCD, string RequestDept, string EmployeeID, string BeginDate, string EndDate)
        {
            try
            {
                string sql = "select " +
                               " tmp.RequestDept,di.DeptName," +
                               " tmp.EmployeeID,ei.EmployeeName," +
                               " tmp.num	" +
                           " from " +
                               " (select" +
                                   " dr.RequestDept,dr.EmployeeID,count(dr.id) num,dr.CompanyCD" +
                               " from" +
                                   " officedba.DocRequstInfo dr " +
                               " where dr.CompanyCD = '" + CompanyCD + "' ";
                if (BeginDate != "")
                    sql += " and dr.RequestDate >='" + BeginDate + "'";
                if (EndDate != "")
                    sql += " and dr.RequestDate <='" + EndDate + "'";

                sql += " group by dr.RequestDept,dr.EmployeeID,dr.CompanyCD) tmp" +
                       " left join officedba.DeptInfo di on di.id = tmp.RequestDept" +
                       " left join officedba.EmployeeInfo ei on ei.id = tmp.EmployeeID" +
                       " where tmp.CompanyCD = '" + CompanyCD + "'";

                if (RequestDept != "")
                    sql += " and tmp.RequestDept = '" + RequestDept + "'";
                if (EmployeeID != "")
                    sql += " and tmp.EmployeeID = '" + EmployeeID + "'";


                return SqlHelper.ExecuteSql(sql);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 导出请示列表
        /// <summary>
        /// 导出请示列表
        /// </summary>
        /// <param name="DocRequstM"></param>
        /// <param name="FileDateBegin"></param>
        /// <param name="FileDateEnd"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable ExportDocRequst(DocRequstModel DocRequstM, string FileDateBegin, string FileDateEnd, string ord)
        {
            try
            {
                #region sql语句
                string sql = "select" +
                                    " dr.id,dr.DocumentNo,cp.TypeName,dr.RequestTitle,dr.RequestMoney,isnull(di.DeptName,'') DeptName," +
                                    " isnull(ei.EmployeeName,'') EmployeeName,isnull(CONVERT(varchar(100), dr.RequestDate, 23),'') RequestDate,"+
                                    " (case dr.EmerLevel when '1' then '特提' when '2' then '特急' when '3' then '加急' when '4' then '平急' end) EmerLevel " +
                               " from " +
                                   " officedba.DocRequstInfo dr" +
                               " left join officedba.CodePublicType cp" +
                               " on	cp.id = dr.RequstDocType" +
                               " left join officedba.DeptInfo di" +
                               " on dr.RequestDept = di.id" +
                               " left join officedba.EmployeeInfo ei" +
                               " on ei.id = dr.EmployeeID" +
                               " where dr.CompanyCD = '" + DocRequstM.CompanyCD + "'";
                
                if (FileDateBegin.ToString() != "")
                    sql += " and dr.RequestDate >= '" + FileDateBegin.ToString() + "'";                
                if (FileDateEnd.ToString() != "")
                    sql += " and dr.RequestDate <= '" + FileDateEnd.ToString() + "'";               
                if (DocRequstM.RequestDept != 0)
                    sql += " and dr.RequestDept = " + DocRequstM.RequestDept.ToString() + "";
                if (DocRequstM.EmployeeID != 0)
                    sql += " and dr.EmployeeID = " + DocRequstM.EmployeeID.ToString() + "";
                if (DocRequstM.RequestTitle != "")
                    sql += " and dr.RequestTitle like '%" + DocRequstM.RequestTitle + "%'";
                if (DocRequstM.RequstDocType != 0)
                    sql += " and dr.RequstDocType = " + DocRequstM.RequstDocType + "";
                if (DocRequstM.SecretLevel != "0")
                    sql += " and dr.SecretLevel = '" + DocRequstM.SecretLevel + "'";
                if (DocRequstM.EmerLevel != "0")
                    sql += " and dr.EmerLevel = '" + DocRequstM.EmerLevel + "'";

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

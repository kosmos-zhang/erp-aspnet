using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using XBase.Data.DBHelper;
using XBase.Model.Personal.Note;
using XBase.Common;

namespace XBase.Data.Personal.Note
{
    /// <summary>
    /// 数据访问类NoteDBHelper。
    /// </summary>
    public class NoteDBHelper
    {
        public NoteDBHelper()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID from [officedba].PersonalNote");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DBHelper.SqlHelper.ExecuteSql(strSql.ToString(), parameters).Rows.Count > 0;
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(XBase.Model.Personal.Note.NoteInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [officedba].PersonalNote(");
            strSql.Append("CompanyCD,NoteNo,NoteDate,NoteContent,Attachment,CanViewUser,CanViewUserName,ToManagerID,ManagerNote,Status,Creator,CreatorUserName,CreateDate,ModifiedDate,ModifiedUserID,StudyContent,MyCheckContent,UpContent,RenZhenDF,KuaiDF,ChengNuoDF,RenWuDF,LeGuanDF,ZiXinDF,FenXianDF,JieKouDF )");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@NoteNo,@NoteDate,@NoteContent,@Attachment,@CanViewUser,@CanViewUserName,@ToManagerID,@ManagerNote,@Status,@Creator,@CreatorUserName,@CreateDate,@ModifiedDate,@ModifiedUserID,@StudyContent,@MyCheckContent,@UpContent,@RenZhenDF,@KuaiDF,@ChengNuoDF,@RenWuDF,@LeGuanDF,@ZiXinDF,@FenXianDF,@JieKouDF)");
            strSql.Append(";select @ID=@@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@NoteNo", SqlDbType.VarChar,50),
					new SqlParameter("@NoteDate", SqlDbType.DateTime),
					new SqlParameter("@NoteContent", SqlDbType.NText ),
					new SqlParameter("@Attachment", SqlDbType.VarChar,200),
					new SqlParameter("@CanViewUser", SqlDbType.VarChar,1024),
					new SqlParameter("@CanViewUserName", SqlDbType.VarChar,1024),
					new SqlParameter("@ToManagerID", SqlDbType.Int,4),
					new SqlParameter("@ManagerNote", SqlDbType.VarChar,1024),
					new SqlParameter("@NotedDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Char,1),
					new SqlParameter("@Creator", SqlDbType.Int,4),
                    new SqlParameter("@CreatorUserName", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50),
                   new SqlParameter("@StudyContent", SqlDbType.VarChar,2048),
                    new SqlParameter("@MyCheckContent", SqlDbType.VarChar,2048),
                    new SqlParameter("@UpContent", SqlDbType.VarChar,2048),
                    new SqlParameter("@RenZhenDF", SqlDbType.Int,4),
                    new SqlParameter("@KuaiDF", SqlDbType.Int,4),
                    new SqlParameter("@ChengNuoDF", SqlDbType.Int,4),
                    new SqlParameter("@RenWuDF", SqlDbType.Int,4),
                    new SqlParameter("@LeGuanDF", SqlDbType.Int,4),
                    new SqlParameter("@ZiXinDF", SqlDbType.Int,4),
                    new SqlParameter("@FenXianDF", SqlDbType.Int,4),
                    new SqlParameter("@JieKouDF", SqlDbType.Int,4),
                    new SqlParameter("@ID", SqlDbType.Int,0) };
            parameters[0].Value = model.CompanyCD;
            parameters[1].Value = model.NoteNo;
            parameters[2].Value = model.NoteDate;
            parameters[3].Value = model.NoteContent;
            parameters[4].Value = model.Attachment;
            parameters[5].Value = model.CanViewUser;
            parameters[6].Value = model.CanViewUserName;
            parameters[7].Value = model.ToManagerID;
            parameters[8].Value = model.ManagerNote;
            parameters[9].Value = model.NotedDate;
            parameters[10].Value = model.Status;
            parameters[11].Value = model.Creator;
            parameters[12].Value = model.CreatorUserName;
            parameters[13].Value = model.CreateDate;
            parameters[14].Value = model.ModifiedDate;
            parameters[15].Value = model.ModifiedUserID;
            parameters[16].Value = model.StudyContent;
            parameters[17].Value = model.MyCheckContent;
            parameters[18].Value = model.UpContent;
            parameters[19].Value = model.RenZhenDF;
            parameters[20].Value = model.KuaiDF;
            parameters[21].Value = model.ChengNuoDF;
            parameters[22].Value = model.RenWuDF;
            parameters[23].Value = model.LeGuanDF;
            parameters[24].Value = model.ZiXinDF;
            parameters[25].Value = model.FenXianDF;
            parameters[26].Value = model.JieKouDF;

            parameters[27].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);
            return int.Parse(parameters[27].Value.ToString());
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(XBase.Model.Personal.Note.NoteInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [officedba].PersonalNote set ");
            strSql.Append("CompanyCD=@CompanyCD,");
            strSql.Append("NoteNo=@NoteNo,");
            //   strSql.Append("NoteDate=@NoteDate,");
            strSql.Append("NoteContent=@NoteContent,");
            strSql.Append("Attachment=@Attachment,");
            strSql.Append("CanViewUser=@CanViewUser,");
            strSql.Append("CanViewUserName=@CanViewUserName,");
            strSql.Append("ToManagerID=@ToManagerID,");
            strSql.Append("ManagerNote=@ManagerNote,");
            if (model.NotedDate != new DateTime(1900, 1, 1))
                strSql.Append("NotedDate=@NotedDate,");
            strSql.Append("Status=@Status,");
            strSql.Append("Creator=@Creator,");
            strSql.Append("CreatorUserName=@CreatorUserName,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("ModifiedDate=@ModifiedDate,");
            strSql.Append("ModifiedUserID=@ModifiedUserID,");
            strSql.Append("StudyContent=@StudyContent,");
            strSql.Append("MyCheckContent=@MyCheckContent,");
            strSql.Append("UpContent=@UpContent,");
            strSql.Append("RenZhenDF=@RenZhenDF,");
            strSql.Append("KuaiDF=@KuaiDF,");
            strSql.Append("ChengNuoDF=@ChengNuoDF,");
            strSql.Append("RenWuDF=@RenWuDF,");
            strSql.Append("LeGuanDF=@LeGuanDF,");
            strSql.Append("ZiXinDF=@ZiXinDF,");
            strSql.Append("FenXianDF=@FenXianDF,");
            strSql.Append("JieKouDF=@JieKouDF ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@NoteNo", SqlDbType.VarChar,50),
					new SqlParameter("@NoteDate", SqlDbType.DateTime),
					new SqlParameter("@NoteContent", SqlDbType.NText ),
					new SqlParameter("@Attachment", SqlDbType.VarChar,200),
					new SqlParameter("@CanViewUser", SqlDbType.VarChar,1024),
					new SqlParameter("@CanViewUserName", SqlDbType.VarChar,1024),
					new SqlParameter("@ToManagerID", SqlDbType.Int,4),
					new SqlParameter("@ManagerNote", SqlDbType.VarChar,1024),
					new SqlParameter("@NotedDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Char,1),
					new SqlParameter("@Creator", SqlDbType.Int,4),
                    new SqlParameter("@CreatorUserName", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50),
                     new SqlParameter("@StudyContent", SqlDbType.VarChar,2048),
                    new SqlParameter("@MyCheckContent", SqlDbType.VarChar,2048),
                    new SqlParameter("@UpContent", SqlDbType.VarChar,2048),
                    new SqlParameter("@RenZhenDF", SqlDbType.Int,4),
                    new SqlParameter("@KuaiDF", SqlDbType.Int,4),
                    new SqlParameter("@ChengNuoDF", SqlDbType.Int,4),
                    new SqlParameter("@RenWuDF", SqlDbType.Int,4),
                    new SqlParameter("@LeGuanDF", SqlDbType.Int,4),
                    new SqlParameter("@ZiXinDF", SqlDbType.Int,4),
                    new SqlParameter("@FenXianDF", SqlDbType.Int,4),
                    new SqlParameter("@JieKouDF", SqlDbType.Int,4)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.CompanyCD;
            parameters[2].Value = model.NoteNo;
            parameters[3].Value = model.NoteDate;
            parameters[4].Value = model.NoteContent;
            parameters[5].Value = model.Attachment;
            parameters[6].Value = model.CanViewUser;
            parameters[7].Value = model.CanViewUserName;
            parameters[8].Value = model.ToManagerID;
            parameters[9].Value = model.ManagerNote;
            parameters[10].Value = model.NotedDate;
            parameters[11].Value = model.Status;
            parameters[12].Value = model.Creator;
            parameters[13].Value = model.CreatorUserName;
            parameters[14].Value = model.CreateDate;
            parameters[15].Value = model.ModifiedDate;
            parameters[16].Value = model.ModifiedUserID;
            parameters[17].Value = model.StudyContent;
            parameters[18].Value = model.MyCheckContent;
            parameters[19].Value = model.UpContent;
            parameters[20].Value = model.RenZhenDF;
            parameters[21].Value = model.KuaiDF;
            parameters[22].Value = model.ChengNuoDF;
            parameters[23].Value = model.RenWuDF;
            parameters[24].Value = model.LeGuanDF;
            parameters[25].Value = model.ZiXinDF;
            parameters[26].Value = model.FenXianDF;
            parameters[27].Value = model.JieKouDF;

            SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除多条数据
        /// </summary>
        public void Delete(string where)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete officedba.PersonalNote ");
            strSql.Append(" where ");
            if (where + "" == "")
            {
                throw new Exception("不允许使用不带 条件的DELETE语句");

            }
            strSql.Append(where);

            SqlHelper.ExecuteTransSql(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete officedba.PersonalNote ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public XBase.Model.Personal.Note.NoteInfoModel GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,CompanyCD,NoteNo,NoteDate,NoteContent,Attachment,CanViewUser,CanViewUserName,ToManagerID,ManagerNote,NotedDate,Status,Creator,CreatorUserName,CreateDate,ModifiedDate,ModifiedUserID,StudyContent,MyCheckContent,UpContent,RenZhenDF,KuaiDF,ChengNuoDF,RenWuDF,LeGuanDF,ZiXinDF,FenXianDF,JieKouDF  from officedba.PersonalNote ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            XBase.Model.Personal.Note.NoteInfoModel model = new XBase.Model.Personal.Note.NoteInfoModel();
            DataTable dt = SqlHelper.ExecuteSql(strSql.ToString(), parameters);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt.Copy());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.CompanyCD = ds.Tables[0].Rows[0]["CompanyCD"].ToString();
                model.NoteNo = ds.Tables[0].Rows[0]["NoteNo"].ToString();
                if (ds.Tables[0].Rows[0]["NoteDate"].ToString() != "")
                {
                    model.NoteDate = DateTime.Parse(ds.Tables[0].Rows[0]["NoteDate"].ToString());
                }
                model.NoteContent = ds.Tables[0].Rows[0]["NoteContent"].ToString();
                model.Attachment = ds.Tables[0].Rows[0]["Attachment"].ToString();
                model.CanViewUser = ds.Tables[0].Rows[0]["CanViewUser"].ToString();
                model.CanViewUserName = ds.Tables[0].Rows[0]["CanViewUserName"].ToString();
                if (ds.Tables[0].Rows[0]["ToManagerID"].ToString() != "")
                {
                    model.ToManagerID = int.Parse(ds.Tables[0].Rows[0]["ToManagerID"].ToString());
                }
                model.ManagerNote = ds.Tables[0].Rows[0]["ManagerNote"].ToString();
                if (ds.Tables[0].Rows[0]["NotedDate"].ToString() != "")
                {
                    model.NotedDate = DateTime.Parse(ds.Tables[0].Rows[0]["NotedDate"].ToString());
                }
                model.Status = ds.Tables[0].Rows[0]["Status"].ToString();
                if (ds.Tables[0].Rows[0]["Creator"].ToString() != "")
                {
                    model.Creator = int.Parse(ds.Tables[0].Rows[0]["Creator"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatorUserName"].ToString() != "")
                {
                    model.CreatorUserName = ds.Tables[0].Rows[0]["CreatorUserName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() != "")
                {
                    model.CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ModifiedDate"].ToString() != "")
                {
                    model.ModifiedDate = DateTime.Parse(ds.Tables[0].Rows[0]["ModifiedDate"].ToString());
                }



                if (ds.Tables[0].Rows[0]["StudyContent"].ToString() != "")
                {
                    model.StudyContent = ds.Tables[0].Rows[0]["StudyContent"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MyCheckContent"].ToString() != "")
                {
                    model.MyCheckContent = ds.Tables[0].Rows[0]["MyCheckContent"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UpContent"].ToString() != "")
                {
                    model.UpContent = ds.Tables[0].Rows[0]["UpContent"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RenZhenDF"].ToString() != "0")
                {
                    try
                    {
                        model.RenZhenDF = int.Parse(ds.Tables[0].Rows[0]["RenZhenDF"].ToString());
                    }
                    catch { model.RenZhenDF = 0; }
                }
                if (ds.Tables[0].Rows[0]["KuaiDF"].ToString() != "0")
                {
                    try
                    {
                        model.KuaiDF = int.Parse(ds.Tables[0].Rows[0]["KuaiDF"].ToString());
                    }
                    catch { model.KuaiDF = 0; }
                }
                if (ds.Tables[0].Rows[0]["ChengNuoDF"].ToString() != "0")
                {
                    try
                    {
                        model.ChengNuoDF = int.Parse(ds.Tables[0].Rows[0]["ChengNuoDF"].ToString());
                    }
                    catch
                    {
                        model.ChengNuoDF = 0;
                    }
                }
                if (ds.Tables[0].Rows[0]["RenWuDF"].ToString() != "0")
                {
                    try
                    {
                        model.RenWuDF = int.Parse(ds.Tables[0].Rows[0]["RenWuDF"].ToString());
                    }
                    catch
                    {
                        model.RenWuDF = 0;
                    }
                }
                if (ds.Tables[0].Rows[0]["LeGuanDF"].ToString() != "0")
                {
                    try
                    {
                        model.LeGuanDF = int.Parse(ds.Tables[0].Rows[0]["LeGuanDF"].ToString());
                    }
                    catch { model.LeGuanDF = 0; }
                }
                if (ds.Tables[0].Rows[0]["ZiXinDF"].ToString() != "0")
                {
                    try
                    {
                        model.ZiXinDF = int.Parse(ds.Tables[0].Rows[0]["ZiXinDF"].ToString());
                    }
                    catch { model.ZiXinDF = 0; }
                }
                if (ds.Tables[0].Rows[0]["FenXianDF"].ToString() != "0")
                {
                    try
                    {
                        model.FenXianDF = int.Parse(ds.Tables[0].Rows[0]["FenXianDF"].ToString());
                    }
                    catch { model.FenXianDF = 0; }
                }
                if (ds.Tables[0].Rows[0]["JieKouDF"].ToString() != "0")
                {
                    try
                    {
                        model.JieKouDF = int.Parse(ds.Tables[0].Rows[0]["JieKouDF"].ToString());
                    }
                    catch { model.JieKouDF = 0; }
                }
                model.ModifiedUserID = ds.Tables[0].Rows[0]["ModifiedUserID"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,CompanyCD,NoteNo,NoteDate,NoteContent,Attachment,CanViewUser,CanViewUserName,ToManagerID,ManagerNote,NotedDate,Status,Creator,CreatorUserName,CreateDate,ModifiedDate,ModifiedUserID ");
            strSql.Append(" FROM officedba.PersonalNote ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DBHelper.SqlHelper.ExecuteSql(strSql.ToString());
        }


        /// <summary>
        /// GetPageData
        /// </summary>    
        /// <param name="where"></param>
        /// <param name="fields"></param>
        /// <param name="orderExp"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public int GetPageData(out DataTable dt, string where, string fields, string orderExp, int pageindex, int pagesize)
        {
            /*          
            set @where = '1=1'
            set @fields = '*'
            set @OrderExp = '[ID] ASC'
            set @pageIndex=1
            set @pageSize=10
             */
            if (where.Trim() + "" == "")
            {
                where = "1=1";
            }

            SqlParameter[] prams = {                                      
									SqlParameterHelper.MakeInParam("@OrderExp",SqlDbType.NVarChar,0,orderExp),
									SqlParameterHelper.MakeInParam("@fields",SqlDbType.NVarChar,0,fields),
									SqlParameterHelper.MakeInParam("@where",SqlDbType.NVarChar,0,where),

									SqlParameterHelper.MakeInParam("@pageSize",SqlDbType.Int,0,pagesize),
									SqlParameterHelper.MakeInParam("@pageIndex",SqlDbType.Int,0,pageindex),
									SqlParameterHelper.MakeParam("@RecsCount",SqlDbType.Int,0,ParameterDirection.Output,null)
									
								   };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[PersonalNote_GetPageData]", prams);
            dt = ds.Tables[0];
            return Convert.ToInt32(prams[prams.Length - 1].Value);
        }


        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "PersonalNote";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #region 获取当前人前一次日志 填写的可查看人员
        /// <summary>
        /// 获取当前人前一次日志 填写的可查看人员 
        /// 2010-5-25 add by hexw
        /// </summary>
        /// <param name="curEmployeeID">当前人ID</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns></returns>
        public DataTable GetLastestCanViewUser(string curEmployeeID, string strCompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select top(1) CanViewUserName,CanViewUser,CreateDate ");
            strSql.AppendLine(" from officedba.PersonalNote ");
            strSql.AppendLine(" where Creator=@EmployeeID and CompanyCD=@CompanyCD ");
            strSql.AppendLine(" order by CreateDate desc ");

            SqlParameter[] param = { 
                                    new SqlParameter("@EmployeeID",curEmployeeID),
                                    new SqlParameter("@CompanyCD",strCompanyCD)
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion
        #endregion  成员方法
    }
}



namespace XBase.Data.Personal.Note
{
    /// <summary>
    /// 日志表数据处理类
    /// </summary>
    public class PersonalNoteDBHelper
    {
        #region 字段

        //sql语句
        private const string C_SELECT_ALL =
                                " SELECT ID,CompanyCD,NoteNo,NoteDate,NoteContent,Attachment,CanViewUser,CanViewUserName,ToManagerID,ManagerNote" +
                                "    ,NotedDate,Status,Creator,CreatorUserName,CreateDate,ModifiedDate,ModifiedUserID,StudyContent,MyCheckContent,UpContent" +
                                "    ,RenZhenDF,KuaiDF,ChengNuoDF,RenWuDF,LeGuanDF,ZiXinDF,FenXianDF,JieKouDF" +
                                " FROM officedba.PersonalNote";
        private const string C_SELECT_ID =
                                @" SELECT pn.ID, pn.CompanyCD, pn.NoteNo, pn.NoteDate,isnull(Replace(pn.[Attachment],'\',','),'') as Attachment, pn.NoteContent,
                                           pn.CanViewUser, pn.CanViewUserName, pn.ToManagerID, pn.ManagerNote,
                                           pn.NotedDate, pn.[Status], pn.Creator, pn.CreatorUserName, pn.CreateDate,
                                           pn.ModifiedDate, pn.ModifiedUserID, pn.StudyContent, pn.MyCheckContent,
                                           pn.UpContent, pn.RenZhenDF, pn.KuaiDF, pn.ChengNuoDF, pn.RenWuDF,
                                           pn.LeGuanDF, pn.ZiXinDF, pn.FenXianDF, pn.JieKouDF,isnull(ei.EmployeeName,'')  AS ToManagerName 
                                    FROM officedba.PersonalNote pn
                                    LEFT JOIN officedba.EmployeeInfo ei ON pn.ToManagerID=ei.ID" +
                                " WHERE pn.ID =@ID";
        private const string C_SELECT =
                                " SELECT ID,CompanyCD,NoteNo,NoteDate,NoteContent,Attachment,CanViewUser,CanViewUserName,ToManagerID,ManagerNote" +
                                "    ,NotedDate,Status,Creator,CreatorUserName,CreateDate,ModifiedDate,ModifiedUserID,StudyContent,MyCheckContent,UpContent" +
                                "    ,RenZhenDF,KuaiDF,ChengNuoDF,RenWuDF,LeGuanDF,ZiXinDF,FenXianDF,JieKouDF" +
                                " FROM officedba.PersonalNote" +
                                " WHERE ID=@ID  ";
        private const string C_INSERT =
                                " INSERT officedba.PersonalNote(" +
                                "    CompanyCD,NoteNo,NoteDate,NoteContent,Attachment,CanViewUser,CanViewUserName,ToManagerID,ManagerNote" +
                                "    ,NotedDate,Status,Creator,CreatorUserName,CreateDate,ModifiedDate,ModifiedUserID,StudyContent,MyCheckContent,UpContent" +
                                "    ,RenZhenDF,KuaiDF,ChengNuoDF,RenWuDF,LeGuanDF,ZiXinDF,FenXianDF,JieKouDF )" +
                                " VALUES (" +
                                "    @CompanyCD,@NoteNo,@NoteDate,@NoteContent,@Attachment,@CanViewUser,@CanViewUserName,@ToManagerID,@ManagerNote" +
                                "    ,@NotedDate,@Status,@Creator,@CreatorUserName,@CreateDate,@ModifiedDate,@ModifiedUserID,@StudyContent,@MyCheckContent,@UpContent" +
                                "    ,@RenZhenDF,@KuaiDF,@ChengNuoDF,@RenWuDF,@LeGuanDF,@ZiXinDF,@FenXianDF,@JieKouDF )";
        private const string C_UPDATE =
                                " UPDATE officedba.PersonalNote SET" +
                                "    NoteDate=@NoteDate,NoteContent=@NoteContent" +
                                "    ,Attachment=@Attachment,CanViewUser=@CanViewUser,CanViewUserName=@CanViewUserName,ToManagerID=@ToManagerID" +
                                "    ,NotedDate=@NotedDate,Status=@Status" +
                                "    ,ModifiedDate=@ModifiedDate,ModifiedUserID=@ModifiedUserID,StudyContent=@StudyContent,MyCheckContent=@MyCheckContent,UpContent=@UpContent" +
                                "    ,RenZhenDF=@RenZhenDF,KuaiDF=@KuaiDF,ChengNuoDF=@ChengNuoDF,RenWuDF=@RenWuDF,LeGuanDF=@LeGuanDF" +
                                "    ,ZiXinDF=@ZiXinDF,FenXianDF=@FenXianDF,JieKouDF=@JieKouDF" +
                                " WHERE ID=@ID";
        private const string C_DELETE =
                                " DELETE FROM officedba.PersonalNote WHERE ID=@ID ";

        private const string C_DELETE_ID =
                                " DELETE FROM officedba.PersonalNote WHERE ID IN (@ID) ";


        //字段顺序变量定义
        private const byte m_iDCol = 0; // 自动生成流水号列
        private const byte m_companyCDCol = 1; // 企业代码列
        private const byte m_noteNoCol = 2; // 日记编号(年月日时分秒+用户id)列
        private const byte m_noteDateCol = 3; // 日期列
        private const byte m_noteContentCol = 4; // 日记内容列
        private const byte m_attachmentCol = 5; // 附件列
        private const byte m_canViewUserCol = 6; // 可以查看工作日志的人员（ID，多个）列
        private const byte m_canViewUserNameCol = 7; // 可以查看工作日志的人员姓名列
        private const byte m_toManagerIDCol = 8; // 提交主管(对应员工表ID)列
        private const byte m_managerNoteCol = 9; // 主管点评列
        private const byte m_notedDateCol = 10; // 点评日期列
        private const byte m_statusCol = 11; // 日志状态（0草稿，1提交,2已点评）列
        private const byte m_creatorCol = 12; // 创建人ID(对应员工表ID)列
        private const byte m_creatorUserNameCol = 13; // 创建人名称列
        private const byte m_createDateCol = 14; // 创建时间列
        private const byte m_modifiedDateCol = 15; // 最后更新日期列
        private const byte m_modifiedUserIDCol = 16; // 最后更新用户ID（对应操作用户表中的UserID）列
        private const byte m_studyContentCol = 17; // 今日学习列
        private const byte m_myCheckContentCol = 18; // 今日反省列
        private const byte m_upContentCol = 19; // 改进办法列
        private const byte m_renZhenDFCol = 20; // 认真(分)列
        private const byte m_kuaiDFCol = 21; // 快(分)列
        private const byte m_chengNuoDFCol = 22; // 坚守承诺(分)列
        private const byte m_renWuDFCol = 23; // 保证完成任务(分)列
        private const byte m_leGuanDFCol = 24; // 乐观(分)列
        private const byte m_ziXinDFCol = 25; // 自信(分)列
        private const byte m_fenXianDFCol = 26; // 爱与奉献(分)列
        private const byte m_jieKouDFCol = 27; // 决不找借口(分)列
        #endregion

        #region 方法

        /// <summary>
        /// 查询数据记录
        /// </summary>
        /// <param name="iD"></param>
        /// <returns>返回DataSet数据集</returns>
        public static DataTable SelectDataTable(int iD)
        {
            // SQL语句
            StringBuilder sqlSentence = new StringBuilder(C_SELECT_ID);

            // 参数设置
            SqlParameter[] parameters = new SqlParameter[] 
                        {
                            new SqlParameter("@ID", SqlDbType.Int, 4)
                        };
            parameters[0].Value = iD; // 

            // 执行
            return SqlHelper.ExecuteSql(sqlSentence.ToString(), parameters);
        }

        /// <summary>
        /// 查询数据记录
        /// </summary>
        /// <param name="iD"></param>
        /// <returns>返回DataSet数据集</returns>
        public static DataTable SelectWithKey(int iD)
        {
            // SQL语句
            StringBuilder sqlSentence = new StringBuilder(C_SELECT);

            // 参数设置
            SqlParameter[] parameters = SetSelectAndDeleteParameters();
            parameters = SetSelectAndDeleteParametersValue(parameters, iD);

            // 执行
            return SqlHelper.ExecuteSql(sqlSentence.ToString(), parameters);
        }

        /// <summary>
        /// 列表界面查询方法
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageCount">每页记录数</param>
        /// <param name="orderBy">排序方法</param>
        /// <param name="TotalCount">总记录数</param>
        /// <param name="model">实体类</param>
        /// <returns></returns>
        public static DataTable SelectListData(int pageIndex, int pageCount, string orderBy, ref int TotalCount
            , PersonalNoteModel model)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder(C_SELECT_ALL);
            sql.Append(" WHERE 1=1 ");
            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);
        }

        /// <summary>
        /// 插入操作的执行命令
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns>插入操作的执行命令</returns>
        public static SqlCommand InsertCommand(PersonalNoteModel model)
        {
            // SQL语句
            SqlCommand comm = new SqlCommand();
            comm.CommandText = C_INSERT + " SET @IndexID = @@IDENTITY ";
            // 参数设置
            SqlParameter[] parameters = SetInsertAndUpdateParameters();
            parameters = SetInsertAndUpdateParametersValue(parameters, model);

            comm.Parameters.AddRange(parameters);
            SqlParameter IndexID = new SqlParameter("@IndexID", SqlDbType.Int);
            IndexID.Direction = ParameterDirection.Output;
            comm.Parameters.Add(IndexID);

            return comm;
        }

        /// <summary>
        /// 修改数据记录
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns>修改操作的执行命令</returns>
        public static SqlCommand UpdateCommand(PersonalNoteModel model)
        {
            // SQL语句
            SqlCommand comm = new SqlCommand();
            comm.CommandText = C_UPDATE;
            if (model.Status == "2")
            {
                comm.CommandText = " UPDATE officedba.PersonalNote SET ManagerNote=@ManagerNote,NotedDate=@NotedDate,Status=@Status " +
                " WHERE ID=@ID";
            }
            // 参数设置
            SqlParameter[] parameters = SetInsertAndUpdateParameters();
            parameters = SetInsertAndUpdateParametersValue(parameters, model);

            comm.Parameters.AddRange(parameters);

            return comm;
        }

        /// <summary>
        /// 修改数据记录
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns>修改数据是否成功:true成功,false不成功</returns>
        public static bool Update(PersonalNoteModel model)
        {
            bool returnValue = false; // 返回值

            // SQL语句
            string sqlSentence = C_UPDATE;
            if (model.Status == "2")
            {
                sqlSentence = " UPDATE officedba.PersonalNote SET ManagerNote=@ManagerNote,NotedDate=@NotedDate,Status=@Status " +
                " WHERE ID=@ID";
            }
            // 参数设置
            SqlParameter[] parameters = SetInsertAndUpdateParameters();
            parameters = SetInsertAndUpdateParametersValue(parameters, model);

            //执行SQL
            returnValue = SqlHelper.ExecuteTransSql(sqlSentence, parameters) > 0;

            return returnValue;
        }

        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="iD">ID集合</param>
        /// <returns>删除数据命令</returns>
        public static SqlCommand DeleteCommand(string iD)
        {
            // SQL语句
            SqlCommand comm = new SqlCommand();
            comm.CommandText = C_DELETE_ID;
            // 参数设置
            SqlParameter[] parameters = new SqlParameter[] 
                        {
                            new SqlParameter("@ID", SqlDbType.VarChar)
                        };
            parameters[0].Value = iD; //ID集合

            comm.Parameters.AddRange(parameters);

            return comm;
        }

        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="iD">ID集合</param>
        /// <returns>删除数据是否成功:true成功,false不成功</returns>
        public static bool Delete(string iD)
        {
            bool returnValue = false; // 返回值

            // SQL语句
            StringBuilder sqlSentence = new StringBuilder(C_DELETE_ID);

            // 参数设置
            SqlParameter[] parameters = new SqlParameter[] 
                        {
                            new SqlParameter("@ID", SqlDbType.VarChar)
                        };
            parameters[0].Value = iD; //ID集合


            // 执行
            returnValue = SqlHelper.ExecuteTransSql(sqlSentence.ToString(), parameters) > 0;

            return returnValue;
        }

        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="iD"></param>

        /// <returns>删除数据命令</returns>
        public static SqlCommand DeleteCommandWithKey(int iD)
        {
            // SQL语句
            SqlCommand comm = new SqlCommand();
            comm.CommandText = C_DELETE;
            // 参数设置
            SqlParameter[] parameters = SetSelectAndDeleteParameters();
            parameters = SetSelectAndDeleteParametersValue(parameters, iD);

            comm.Parameters.AddRange(parameters);

            return comm;
        }

        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="iD"></param>

        /// <returns>删除数据是否成功:true成功,false不成功</returns>
        public static bool DeleteWithKey(int iD)
        {
            bool returnValue = false; // 返回值

            // SQL语句
            StringBuilder sqlSentence = new StringBuilder(C_DELETE);

            // 参数设置
            SqlParameter[] parameters = SetSelectAndDeleteParameters();
            parameters = SetSelectAndDeleteParametersValue(parameters, iD);


            // 执行
            returnValue = SqlHelper.ExecuteTransSql(sqlSentence.ToString(), parameters) > 0;

            return returnValue;
        }


        /// <summary>
        /// 设置查询和删除的参数数组
        /// </summary>
        /// <returns>返回参数数组</returns>
        private static SqlParameter[] SetSelectAndDeleteParameters()
        {
            SqlParameter[] parameters = new SqlParameter[] 
                        {
                            new SqlParameter("@ID", SqlDbType.Int, 4) // 
                        };

            return parameters;
        }


        /// <summary>
        /// 设置新增和修改的参数数组
        /// </summary>
        /// <returns>返回参数数组</returns>
        private static SqlParameter[] SetInsertAndUpdateParameters()
        {
            SqlParameter[] parameters = new SqlParameter[] 
                        {
                            new SqlParameter("@ID", SqlDbType.Int,4), // 自动生成流水号
                            new SqlParameter("@CompanyCD", SqlDbType.VarChar,8), // 企业代码
                            new SqlParameter("@NoteNo", SqlDbType.VarChar,50), // 日记编号(年月日时分秒+用户id)
                            new SqlParameter("@NoteDate", SqlDbType.DateTime,8), // 日期
                            new SqlParameter("@NoteContent", SqlDbType.Text), // 日记内容
                            new SqlParameter("@Attachment", SqlDbType.VarChar,200), // 附件
                            new SqlParameter("@CanViewUser", SqlDbType.VarChar,1024), // 可以查看工作日志的人员（ID，多个）
                            new SqlParameter("@CanViewUserName", SqlDbType.VarChar,1024), // 可以查看工作日志的人员姓名
                            new SqlParameter("@ToManagerID", SqlDbType.Int,4), // 提交主管(对应员工表ID)
                            new SqlParameter("@ManagerNote", SqlDbType.VarChar,1024), // 主管点评
                            new SqlParameter("@NotedDate", SqlDbType.DateTime,8), // 点评日期
                            new SqlParameter("@Status", SqlDbType.Char,1), // 日志状态（0草稿，1提交,2已点评）
                            new SqlParameter("@Creator", SqlDbType.Int,4), // 创建人ID(对应员工表ID)
                            new SqlParameter("@CreatorUserName", SqlDbType.VarChar,50), // 创建人名称
                            new SqlParameter("@CreateDate", SqlDbType.DateTime,8), // 创建时间
                            new SqlParameter("@ModifiedDate", SqlDbType.DateTime,8), // 最后更新日期
                            new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,10), // 最后更新用户ID（对应操作用户表中的UserID）
                            new SqlParameter("@StudyContent", SqlDbType.VarChar,2048), // 今日学习
                            new SqlParameter("@MyCheckContent", SqlDbType.VarChar,2048), // 今日反省
                            new SqlParameter("@UpContent", SqlDbType.VarChar,2048), // 改进办法
                            new SqlParameter("@RenZhenDF", SqlDbType.Int,4), // 认真(分)
                            new SqlParameter("@KuaiDF", SqlDbType.Int,4), // 快(分)
                            new SqlParameter("@ChengNuoDF", SqlDbType.Int,4), // 坚守承诺(分)
                            new SqlParameter("@RenWuDF", SqlDbType.Int,4), // 保证完成任务(分)
                            new SqlParameter("@LeGuanDF", SqlDbType.Int,4), // 乐观(分)
                            new SqlParameter("@ZiXinDF", SqlDbType.Int,4), // 自信(分)
                            new SqlParameter("@FenXianDF", SqlDbType.Int,4), // 爱与奉献(分)
                            new SqlParameter("@JieKouDF", SqlDbType.Int,4)  // 决不找借口(分)
                        };

            return parameters;
        }



        /// <summary>
        /// 设置查询和删除的参数数组的值，此方法适用于两个字段作为主键的情况
        /// </summary>
        /// <param name="parameters">参数数组</param>
        /// <param name="iD">的值</param>

        /// <returns>返回参数数组</returns>
        private static SqlParameter[] SetSelectAndDeleteParametersValue(SqlParameter[] parameters, int iD)
        {
            parameters[0].Value = iD; // 


            return parameters;
        }



        /// <summary>
        /// 设置新增和修改的参数数组的值
        /// </summary>
        /// <param name="parameters">参数数组</param>
        /// <param name="model">实体类</param>
        /// <returns>返回参数数组</returns>
        private static SqlParameter[] SetInsertAndUpdateParametersValue(SqlParameter[] parameters, PersonalNoteModel model)
        {
            if (!model.ID.HasValue) parameters[m_iDCol].Value = System.DBNull.Value; else parameters[m_iDCol].Value = model.ID; // 自动生成流水号
            parameters[m_companyCDCol].Value = model.CompanyCD; // 企业代码
            parameters[m_noteNoCol].Value = model.NoteNo; // 日记编号(年月日时分秒+用户id)
            if (!model.NoteDate.HasValue) parameters[m_noteDateCol].Value = System.DBNull.Value; else parameters[m_noteDateCol].Value = model.NoteDate; // 日期
            parameters[m_noteContentCol].Value = model.NoteContent; // 日记内容
            parameters[m_attachmentCol].Value = model.Attachment; // 附件
            parameters[m_canViewUserCol].Value = model.CanViewUser; // 可以查看工作日志的人员（ID，多个）
            parameters[m_canViewUserNameCol].Value = model.CanViewUserName; // 可以查看工作日志的人员姓名
            if (!model.ToManagerID.HasValue) parameters[m_toManagerIDCol].Value = System.DBNull.Value; else parameters[m_toManagerIDCol].Value = model.ToManagerID; // 提交主管(对应员工表ID)
            parameters[m_managerNoteCol].Value = model.ManagerNote; // 主管点评
            if (!model.NotedDate.HasValue) parameters[m_notedDateCol].Value = System.DBNull.Value; else parameters[m_notedDateCol].Value = model.NotedDate; // 点评日期
            parameters[m_statusCol].Value = model.Status; // 日志状态（0草稿，1提交,2已点评）
            if (!model.Creator.HasValue) parameters[m_creatorCol].Value = System.DBNull.Value; else parameters[m_creatorCol].Value = model.Creator; // 创建人ID(对应员工表ID)
            parameters[m_creatorUserNameCol].Value = model.CreatorUserName; // 创建人名称
            if (!model.CreateDate.HasValue) parameters[m_createDateCol].Value = System.DBNull.Value; else parameters[m_createDateCol].Value = model.CreateDate; // 创建时间
            if (!model.ModifiedDate.HasValue) parameters[m_modifiedDateCol].Value = System.DBNull.Value; else parameters[m_modifiedDateCol].Value = model.ModifiedDate; // 最后更新日期
            parameters[m_modifiedUserIDCol].Value = model.ModifiedUserID; // 最后更新用户ID（对应操作用户表中的UserID）
            parameters[m_studyContentCol].Value = model.StudyContent; // 今日学习
            parameters[m_myCheckContentCol].Value = model.MyCheckContent; // 今日反省
            parameters[m_upContentCol].Value = model.UpContent; // 改进办法
            if (!model.RenZhenDF.HasValue) parameters[m_renZhenDFCol].Value = System.DBNull.Value; else parameters[m_renZhenDFCol].Value = model.RenZhenDF; // 认真(分)
            if (!model.KuaiDF.HasValue) parameters[m_kuaiDFCol].Value = System.DBNull.Value; else parameters[m_kuaiDFCol].Value = model.KuaiDF; // 快(分)
            if (!model.ChengNuoDF.HasValue) parameters[m_chengNuoDFCol].Value = System.DBNull.Value; else parameters[m_chengNuoDFCol].Value = model.ChengNuoDF; // 坚守承诺(分)
            if (!model.RenWuDF.HasValue) parameters[m_renWuDFCol].Value = System.DBNull.Value; else parameters[m_renWuDFCol].Value = model.RenWuDF; // 保证完成任务(分)
            if (!model.LeGuanDF.HasValue) parameters[m_leGuanDFCol].Value = System.DBNull.Value; else parameters[m_leGuanDFCol].Value = model.LeGuanDF; // 乐观(分)
            if (!model.ZiXinDF.HasValue) parameters[m_ziXinDFCol].Value = System.DBNull.Value; else parameters[m_ziXinDFCol].Value = model.ZiXinDF; // 自信(分)
            if (!model.FenXianDF.HasValue) parameters[m_fenXianDFCol].Value = System.DBNull.Value; else parameters[m_fenXianDFCol].Value = model.FenXianDF; // 爱与奉献(分)
            if (!model.JieKouDF.HasValue) parameters[m_jieKouDFCol].Value = System.DBNull.Value; else parameters[m_jieKouDFCol].Value = model.JieKouDF; // 决不找借口(分)

            return parameters;
        }


        #endregion

        #region 自定义
        /// <summary>
        /// 判断今天是否已经写日志
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns>true:存在;false:不存在</returns>
        public static bool ExisitTodayNote(PersonalNoteModel model)
        {
            string strSql = @"SELECT pn.ID 
                            FROM officedba.PersonalNote pn
                            WHERE pn.CompanyCD='{0}' AND pn.Creator={1} AND DATEDIFF(dd,pn.NoteDate,'{2}')=0";
            return DBHelper.SqlHelper.ExecuteSql(string.Format(strSql, model.CompanyCD, model.Creator, model.NoteDate.Value)).Rows.Count > 0;
        }


        /// <summary>
        /// 根据日期获得汇报任务列表
        /// </summary>
        /// <param name="reportDate">时间</param>
        /// <param name="isNew">是否是新增</param>
        /// <param name="userInfo">用户信息</param>
        /// <returns></returns>
        public static DataTable GetAllReport(DateTime reportDate, bool isNew, UserInfoUtil userInfo)
        {
            string strSql = @"SELECT t.id AS ID,t.Title AS Title,t.[Content] AS [Content],0 AS ReportType
FROM officedba.Task t
WHERE t.CompanyCD=@CompanyCD 
        AND (t.Principal=@userID OR CHARINDEX(','+@userName+',',','+ISNULL(t.Joins,'')+',')>0 ) 
	    AND DATEDIFF(DAY,t.CreateDate,@Time)>=0 AND DATEDIFF(DAY,@Time,t.CompleteDate)>=0
        {0}
UNION ALL
SELECT pda.id AS ID,pda.ArrangeTItle AS Title,pda.[Content] AS [Content],1 AS ReportType
FROM officedba.PersonalDateArrange pda
WHERE pda.CompanyCD=@CompanyCD 
        AND (pda.Creator=@userID OR CHARINDEX(','+cast(@userID AS VARCHAR)+',',','+ISNULL(pda.CanViewUser,'')+',')>0 ) 
	    AND DATEDIFF(DAY,pda.StartDate,@Time)>=0 AND DATEDIFF(DAY,@Time, pda.EndDate)>=0 {1} ";
            SqlParameter[] parameters = new SqlParameter[] 
                        {
                            new SqlParameter("@Time", SqlDbType.DateTime),
                            new SqlParameter("@CompanyCD", SqlDbType.VarChar),
                            new SqlParameter("@userID", SqlDbType.Int),
                            new SqlParameter("@userName", SqlDbType.VarChar)
                        };
            parameters[0].Value = reportDate;
            parameters[1].Value = userInfo.CompanyCD;
            parameters[2].Value = userInfo.EmployeeID;
            parameters[3].Value = userInfo.UserName;
            strSql = String.Format(strSql, isNew ? "AND t.Status=2" : "", isNew ? "AND pda.Status<>'2'" : "");
            return SqlHelper.ExecuteSql(strSql, parameters);
        }
        #endregion
    }
}

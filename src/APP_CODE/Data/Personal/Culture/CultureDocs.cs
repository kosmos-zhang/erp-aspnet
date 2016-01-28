using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
namespace XBase.Data.Personal.Culture
{
    /// <summary>
    /// 数据访问类CultureDocs。
    /// </summary>
    public class CultureDocs
    {
        public CultureDocs()
        { }
        #region  成员方法


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public int Exists(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select @count=count(*) from [officedba].CultureDocs");
            strSql.Append(" where " + where);
            SqlParameter[] parameters = {
					new SqlParameter("@count", SqlDbType.Int,4)};

            parameters[0].Direction = ParameterDirection.Output;

            DBHelper.SqlHelper.ExecuteSql(strSql.ToString(), parameters);

            return int.Parse(parameters[0].Value.ToString());

        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [officedba].CultureDocs");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DBHelper.SqlHelper.ExecuteSql(strSql.ToString(), parameters).Rows.Count > 0;
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(XBase.Model.Personal.Culture.CultureDocs model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [officedba].CultureDocs(");
            strSql.Append("CompanyCD,CultureTypeID,Title,Culturetent,Attachment,CreateDeptID,Creator,CreateDate,ModifiedDate,ModifiedUserID)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@CultureTypeID,@Title,@Culturetent,@Attachment,@CreateDeptID,@Creator,@CreateDate,@ModifiedDate,@ModifiedUserID)");
            strSql.Append(";select @ID=@@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@CultureTypeID", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@Culturetent", SqlDbType.NVarChar,1024),
					new SqlParameter("@Attachment", SqlDbType.VarChar,200),
                    new SqlParameter("@CreateDeptID", SqlDbType.Int,4),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,20),
                     new SqlParameter("@ID", SqlDbType.Int,0) };
            parameters[0].Value = model.CompanyCD;
            parameters[1].Value = model.CultureTypeID;
            parameters[2].Value = model.Title;
            parameters[3].Value = model.Culturetent;
            parameters[4].Value = model.Attachment;
            parameters[5].Value = model.CreateDeptID;
            parameters[6].Value = model.Creator;
            parameters[7].Value = model.CreateDate;
            parameters[8].Value = model.ModifiedDate;
            parameters[9].Value = model.ModifiedUserID;

            parameters[10].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);
            return int.Parse(parameters[10].Value.ToString());
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(XBase.Model.Personal.Culture.CultureDocs model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [officedba].CultureDocs set ");
            strSql.Append("CompanyCD=@CompanyCD,");
            strSql.Append("CultureTypeID=@CultureTypeID,");
            strSql.Append("Title=@Title,");
            strSql.Append("Culturetent=@Culturetent,");
            strSql.Append("Attachment=@Attachment,");
            strSql.Append("CreateDeptID=@CreateDeptID,");
            strSql.Append("Creator=@Creator,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("ModifiedDate=@ModifiedDate,");
            strSql.Append("ModifiedUserID=@ModifiedUserID");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@CultureTypeID", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@Culturetent", SqlDbType.NVarChar,1024),
					new SqlParameter("@Attachment", SqlDbType.VarChar,200),
                    new SqlParameter("@CreateDeptID", SqlDbType.Int,4),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,20)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.CompanyCD;
            parameters[2].Value = model.CultureTypeID;
            parameters[3].Value = model.Title;
            parameters[4].Value = model.Culturetent;
            parameters[5].Value = model.Attachment;
            parameters[6].Value = model.CreateDeptID;
            parameters[7].Value = model.Creator;
            parameters[8].Value = model.CreateDate;
            parameters[9].Value = model.ModifiedDate;
            parameters[10].Value = model.ModifiedUserID;

            SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete [officedba].CultureDocs ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public XBase.Model.Personal.Culture.CultureDocs GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,CompanyCD,CultureTypeID,Title,Culturetent,Attachment,CreateDeptID,Creator,CreateDate,ModifiedDate,ModifiedUserID from [officedba].CultureDocs ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            XBase.Model.Personal.Culture.CultureDocs model = new XBase.Model.Personal.Culture.CultureDocs();
            DataTable dt = SqlHelper.ExecuteSql(strSql.ToString(), parameters);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.CompanyCD = ds.Tables[0].Rows[0]["CompanyCD"].ToString();
                if (ds.Tables[0].Rows[0]["CultureTypeID"].ToString() != "")
                {
                    model.CultureTypeID = int.Parse(ds.Tables[0].Rows[0]["CultureTypeID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateDeptID"].ToString() != "")
                {
                    model.CreateDeptID = int.Parse(ds.Tables[0].Rows[0]["CreateDeptID"].ToString());
                }

                model.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                model.Culturetent = ds.Tables[0].Rows[0]["Culturetent"].ToString();
                model.Attachment = ds.Tables[0].Rows[0]["Attachment"].ToString();

                if (ds.Tables[0].Rows[0]["Creator"].ToString() != "")
                {
                    model.Creator = int.Parse(ds.Tables[0].Rows[0]["Creator"].ToString());
                }

                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() != "")
                {
                    model.CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ModifiedDate"].ToString() != "")
                {
                    model.ModifiedDate = DateTime.Parse(ds.Tables[0].Rows[0]["ModifiedDate"].ToString());
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
            strSql.Append("select ID,CompanyCD,CultureTypeID,Title,Culturetent,Attachment,CreateDeptID,Creator,CreateDate,ModifiedDate,ModifiedUserID ");
            strSql.Append(" FROM [officedba].CultureDocs ");
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
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[CultureDocs_GetPageData]", prams);
            dt = ds.Tables[0];
            return Convert.ToInt32(prams[prams.Length - 1].Value);
        }

        #endregion  成员方法
    }
}


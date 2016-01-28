using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Data;

using XBase.Data.DBHelper;

namespace XBase.Data.SystemManager
{

    /// <summary>
    /// 数据访问类SysNotice。
    /// </summary>
    public class SysNotice
    {
        public SysNotice()
        { }
        #region  成员方法

        
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from pubdba.SysNotice");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(XBase.Model.SystemManager.SysNotice model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into pubdba.SysNotice(");
            strSql.Append("Title,Content,PubDate,OverDate,CreatorUserID)");
            strSql.Append(" values (");
            strSql.Append("@Title,@Content,@PubDate,@OverDate,@CreatorUserID)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Title", SqlDbType.NVarChar,50),
					new SqlParameter("@Content", SqlDbType.NText),
					new SqlParameter("@PubDate", SqlDbType.DateTime),
                    new SqlParameter("@OverDate", SqlDbType.DateTime),
					new SqlParameter("@CreatorUserID", SqlDbType.VarChar,50)};
            parameters[0].Value = model.Title;
            parameters[1].Value = model.Content;
            parameters[2].Value = model.PubDate;
            parameters[3].Value = model.OverDate;
            parameters[4].Value = model.CreatorUserID;

            object obj = SqlHelper.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(XBase.Model.SystemManager.SysNotice model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update pubdba.SysNotice set ");
            strSql.Append("Title=@Title,");
            strSql.Append("Content=@Content,");
            strSql.Append("PubDate=@PubDate,");
            strSql.Append("OverDate=@OverDate,");
            strSql.Append("CreatorUserID=@CreatorUserID");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.NVarChar,50),
					new SqlParameter("@Content", SqlDbType.NText),
					new SqlParameter("@PubDate", SqlDbType.DateTime),
                     new SqlParameter("@OverDate", SqlDbType.DateTime),
					new SqlParameter("@CreatorUserID", SqlDbType.VarChar,50)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.Content;
            parameters[3].Value = model.PubDate;
            parameters[4].Value = model.OverDate;
            parameters[5].Value = model.CreatorUserID;

            SqlHelper.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete pubdba.SysNotice ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SqlHelper.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public XBase.Model.SystemManager.SysNotice GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,Title,Content,PubDate,OverDate,CreatorUserID from pubdba.SysNotice ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            XBase.Model.SystemManager.SysNotice model = new XBase.Model.SystemManager.SysNotice();
            DataTable ds = SqlHelper.ExecuteSql(strSql.ToString(), parameters);
            if (ds.Rows.Count > 0)
            {
                if (ds.Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Rows[0]["ID"].ToString());
                }
                model.Title = ds.Rows[0]["Title"].ToString();
                model.Content = ds.Rows[0]["Content"].ToString();
                if (ds.Rows[0]["PubDate"].ToString() != "")
                {
                    model.PubDate = DateTime.Parse(ds.Rows[0]["PubDate"].ToString());
                }
                if (ds.Rows[0]["OverDate"].ToString() != "")
                {
                    model.OverDate = DateTime.Parse(ds.Rows[0]["OverDate"].ToString());
                }
                if (ds.Rows[0]["CreatorUserID"].ToString() != "")
                {
                    model.CreatorUserID = ds.Rows[0]["CreatorUserID"].ToString();
                }
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
        public DataTable GetList(string strWhere,int topN)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top " + topN.ToString() + " ID,Title,Content,PubDate,OverDate,CreatorUserID ");
            strSql.Append(" FROM pubdba.SysNotice ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by pubdate desc");
             
            return SqlHelper.ExecuteSql(strSql.ToString());
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Title,Content,PubDate,OverDate,CreatorUserID ");
            strSql.Append(" FROM pubdba.SysNotice ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by pubdate desc");

            return SqlHelper.ExecuteSql(strSql.ToString());
        }
             

        #endregion  成员方法
    }

}

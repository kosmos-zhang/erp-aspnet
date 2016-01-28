using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;

namespace XBase.Data.Personal.MessageBox
{
	/// <summary>
	/// 数据访问类PublicNotice。
	/// </summary>
	public class PublicNotice
	{
		public PublicNotice()
		{}
		#region  成员方法

	

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from [officedba].PublicNotice");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

            return SqlHelper.ExecuteSql(strSql.ToString(), parameters).Rows.Count > 0;
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(XBase.Model.Personal.MessageBox.PublicNotice model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("insert into [officedba].PublicNotice(");
			strSql.Append("CompanyCD,NewsTitle,NewsContent,IsShow,Status,Comfirmor,ComfirmDate,Creator,CreateDate)");
			strSql.Append(" values (");
			strSql.Append("@CompanyCD,@NewsTitle,@NewsContent,@IsShow,@Status,@Comfirmor,@ComfirmDate,@Creator,@CreateDate)");
			strSql.Append(";select @ID=@@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),					
					new SqlParameter("@NewsTitle", SqlDbType.VarChar,100),
					new SqlParameter("@NewsContent", SqlDbType.VarChar,512),
					new SqlParameter("@IsShow", SqlDbType.Char,1),
					new SqlParameter("@Status", SqlDbType.Char,1),					
					new SqlParameter("@Comfirmor", SqlDbType.Int,4),
					new SqlParameter("@ComfirmDate", SqlDbType.DateTime),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
                     new SqlParameter("@ID", SqlDbType.Int,0) };
			parameters[0].Value = model.CompanyCD;			
			parameters[1].Value = model.NewsTitle;
			parameters[2].Value = model.NewsContent;
			parameters[3].Value = model.IsShow;
			parameters[4].Value = model.Status;			
			parameters[5].Value = model.Comfirmor;
			parameters[6].Value = model.ComfirmDate;
			parameters[7].Value = model.Creator;
			parameters[8].Value = model.CreateDate;

            parameters[9].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);
            return int.Parse(parameters[9].Value.ToString());
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(XBase.Model.Personal.MessageBox.PublicNotice model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("update [officedba].PublicNotice set ");
			strSql.Append("CompanyCD=@CompanyCD,");
			strSql.Append("NewsTitle=@NewsTitle,");
			strSql.Append("NewsContent=@NewsContent,");
			strSql.Append("IsShow=@IsShow,");
			strSql.Append("Status=@Status,");
			strSql.Append("Comfirmor=@Comfirmor,");
			strSql.Append("ComfirmDate=@ComfirmDate,");
			strSql.Append("Creator=@Creator,");
			strSql.Append("CreateDate=@CreateDate");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),					
					new SqlParameter("@NewsTitle", SqlDbType.VarChar,100),
					new SqlParameter("@NewsContent", SqlDbType.VarChar,512),
					new SqlParameter("@IsShow", SqlDbType.Char,1),
					new SqlParameter("@Status", SqlDbType.Char,1),
					new SqlParameter("@Comfirmor", SqlDbType.Int,4),
					new SqlParameter("@ComfirmDate", SqlDbType.DateTime),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime)};
			parameters[0].Value = model.ID;
            parameters[1].Value = model.CompanyCD;
            parameters[2].Value = model.NewsTitle;
            parameters[3].Value = model.NewsContent;
            parameters[4].Value = model.IsShow;
            parameters[5].Value = model.Status;
            parameters[6].Value = model.Comfirmor;
            parameters[7].Value = model.ComfirmDate;
            parameters[8].Value = model.Creator;
            parameters[9].Value = model.CreateDate;

            SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("delete [officedba].PublicNotice ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

            SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public XBase.Model.Personal.MessageBox.PublicNotice GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 ID,CompanyCD,NewsTitle,NewsContent,IsShow,Status,Comfirmor,ComfirmDate,Creator,CreateDate from [officedba].PublicNotice ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			XBase.Model.Personal.MessageBox.PublicNotice model=new XBase.Model.Personal.MessageBox.PublicNotice();
            DataTable dt = SqlHelper.ExecuteSql(strSql.ToString(), parameters);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				model.CompanyCD=ds.Tables[0].Rows[0]["CompanyCD"].ToString();			
				model.NewsTitle=ds.Tables[0].Rows[0]["NewsTitle"].ToString();
				model.NewsContent=ds.Tables[0].Rows[0]["NewsContent"].ToString();
				model.IsShow=ds.Tables[0].Rows[0]["IsShow"].ToString();
				model.Status=ds.Tables[0].Rows[0]["Status"].ToString();
				if(ds.Tables[0].Rows[0]["Comfirmor"].ToString()!="")
				{
					model.Comfirmor=int.Parse(ds.Tables[0].Rows[0]["Comfirmor"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ComfirmDate"].ToString()!="")
				{
					model.ComfirmDate=DateTime.Parse(ds.Tables[0].Rows[0]["ComfirmDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Creator"].ToString()!="")
				{
					model.Creator=int.Parse(ds.Tables[0].Rows[0]["Creator"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CreateDate"].ToString()!="")
				{
					model.CreateDate=DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
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
		public DataTable GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,CompanyCD,NewsTitle,NewsContent,IsShow,Status,Comfirmor,ComfirmDate,Creator,CreateDate ");
            strSql.Append(" FROM [officedba].PublicNotice ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[PublicNotice_GetPageData]", prams);
            dt = ds.Tables[0];
            return Convert.ToInt32(prams[prams.Length - 1].Value);
        }


        public int GetDeskTopPageData(out DataTable dt, string CompanyCD)
        {
            string sqlstr = "select top 10 * from  [officedba].PublicNotice  where IsShow='1'  and  CompanyCD='" + CompanyCD + "'   And Status = '1'   order by CreateDate Desc  ";
             dt = DBHelper.SqlHelper.ExecuteSql(sqlstr.ToString());  
            return  dt.Rows.Count ;
        }
		#endregion  成员方法
	}
}


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
	/// 数据访问类MessageInputBox。
	/// </summary>
	public class MessageInputBox
	{
		public MessageInputBox()
		{}
		#region  成员方法

		

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from officedba.MessageInputBox");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return SqlHelper.ExecuteSql(strSql.ToString(), parameters).Rows.Count > 0;
		}
      
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(XBase.Model.Personal.MessageBox.MessageInputBox model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into officedba.MessageInputBox(");
			strSql.Append("CompanyCD,Title,Content,FromID,SendUserID,ReceiveUserID,Status,ReadDate,CreateDate,MsgURL,ModifiedDate,ModifiedUserID)");
			strSql.Append(" values (");
			strSql.Append("@CompanyCD,@Title,@Content,@FromID,@SendUserID,@ReceiveUserID,@Status,@ReadDate,@CreateDate,@MsgURL,@ModifiedDate,@ModifiedUserID)");
            strSql.Append(";select @ID=@@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@Content", SqlDbType.VarChar,1024),
					new SqlParameter("@FromID", SqlDbType.Int,4),
					new SqlParameter("@SendUserID", SqlDbType.Int,4),
					new SqlParameter("@ReceiveUserID", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Char,1),
					new SqlParameter("@ReadDate", SqlDbType.DateTime),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@MsgURL", SqlDbType.VarChar,200),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,10),
                     new SqlParameter("@ID", SqlDbType.Int,0) };
			parameters[0].Value = model.CompanyCD;
			parameters[1].Value = model.Title;
			parameters[2].Value = model.Content;
			parameters[3].Value = model.FromID;
			parameters[4].Value = model.SendUserID;
			parameters[5].Value = model.ReceiveUserID;
			parameters[6].Value = model.Status;
			parameters[7].Value = model.ReadDate;
			parameters[8].Value = model.CreateDate;
			parameters[9].Value = model.MsgURL;
			parameters[10].Value = model.ModifiedDate;
			parameters[11].Value = model.ModifiedUserID;
            parameters[12].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);
            return int.Parse(parameters[12].Value.ToString());
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(XBase.Model.Personal.MessageBox.MessageInputBox model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update officedba.MessageInputBox set ");
			strSql.Append("CompanyCD=@CompanyCD,");
			strSql.Append("Title=@Title,");
			strSql.Append("Content=@Content,");
			strSql.Append("FromID=@FromID,");
			strSql.Append("SendUserID=@SendUserID,");
			strSql.Append("ReceiveUserID=@ReceiveUserID,");
			strSql.Append("Status=@Status,");
			strSql.Append("ReadDate=@ReadDate,");
			strSql.Append("CreateDate=@CreateDate,");
			strSql.Append("MsgURL=@MsgURL,");
			strSql.Append("ModifiedDate=@ModifiedDate,");
			strSql.Append("ModifiedUserID=@ModifiedUserID");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@Title", SqlDbType.VarChar,100),
					new SqlParameter("@Content", SqlDbType.VarChar,1024),
					new SqlParameter("@FromID", SqlDbType.Int,4),
					new SqlParameter("@SendUserID", SqlDbType.Int,4),
					new SqlParameter("@ReceiveUserID", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.Char,1),
					new SqlParameter("@ReadDate", SqlDbType.DateTime),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@MsgURL", SqlDbType.VarChar,200),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,10)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.CompanyCD;
			parameters[2].Value = model.Title;
			parameters[3].Value = model.Content;
			parameters[4].Value = model.FromID;
			parameters[5].Value = model.SendUserID;
			parameters[6].Value = model.ReceiveUserID;
			parameters[7].Value = model.Status;
			parameters[8].Value = model.ReadDate;
			parameters[9].Value = model.CreateDate;
			parameters[10].Value = model.MsgURL;
			parameters[11].Value = model.ModifiedDate;
			parameters[12].Value = model.ModifiedUserID;

			SqlHelper.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete officedba.MessageInputBox ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

            SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public XBase.Model.Personal.MessageBox.MessageInputBox GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,CompanyCD,Title,Content,FromID,SendUserID,ReceiveUserID,Status,ReadDate,CreateDate,MsgURL,ModifiedDate,ModifiedUserID from officedba.MessageInputBox ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = ID;

			XBase.Model.Personal.MessageBox.MessageInputBox model=new XBase.Model.Personal.MessageBox.MessageInputBox();
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
				model.Title=ds.Tables[0].Rows[0]["Title"].ToString();
				model.Content=ds.Tables[0].Rows[0]["Content"].ToString();
				if(ds.Tables[0].Rows[0]["FromID"].ToString()!="")
				{
					model.FromID=int.Parse(ds.Tables[0].Rows[0]["FromID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SendUserID"].ToString()!="")
				{
					model.SendUserID=int.Parse(ds.Tables[0].Rows[0]["SendUserID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReceiveUserID"].ToString()!="")
				{
					model.ReceiveUserID=int.Parse(ds.Tables[0].Rows[0]["ReceiveUserID"].ToString());
				}
				model.Status=ds.Tables[0].Rows[0]["Status"].ToString();
				if(ds.Tables[0].Rows[0]["ReadDate"].ToString()!="")
				{
					model.ReadDate=DateTime.Parse(ds.Tables[0].Rows[0]["ReadDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CreateDate"].ToString()!="")
				{
					model.CreateDate=DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
				}
				model.MsgURL=ds.Tables[0].Rows[0]["MsgURL"].ToString();
				if(ds.Tables[0].Rows[0]["ModifiedDate"].ToString()!="")
				{
					model.ModifiedDate=DateTime.Parse(ds.Tables[0].Rows[0]["ModifiedDate"].ToString());
				}
				model.ModifiedUserID=ds.Tables[0].Rows[0]["ModifiedUserID"].ToString();
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
			strSql.Append("select ID,CompanyCD,Title,Content,FromID,SendUserID,ReceiveUserID,Status,ReadDate,CreateDate,MsgURL,ModifiedDate,ModifiedUserID ");
			strSql.Append(" FROM officedba.MessageInputBox ");
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
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[MessageInputBox_GetPageData]", prams);
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
			parameters[0].Value = "officedba.MessageInputBox";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return SqlHelper.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  成员方法
	}
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;

namespace XBase.Data.Personal.Memo
{
    public class PersonalMemo
    {
        public PersonalMemo()
        { }
        #region  成员方法

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [officedba].PersonalMemo");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DBHelper.SqlHelper.ExecuteSql(strSql.ToString(), parameters).Rows.Count > 0;
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(XBase.Model.Personal.Memo.PersonalMemo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [officedba].PersonalMemo(");
            strSql.Append("CompanyCD,MemoNo,TItle,Content,CanViewUser,CanViewUserName,Attachment,Memoer,MemoDate,Creator,CreateDate,ModifiedDate,ModifiedUserID,Status)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@MemoNo,@TItle,@Content,@CanViewUser,@CanViewUserName,@Attachment,@Memoer,@MemoDate,@Creator,@CreateDate,@ModifiedDate,@ModifiedUserID,@Status)");
            strSql.Append(";select @ID=@@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@MemoNo", SqlDbType.VarChar,50),
					new SqlParameter("@TItle", SqlDbType.VarChar,100),
					new SqlParameter("@Content", SqlDbType.NText),
					new SqlParameter("@CanViewUser", SqlDbType.VarChar,1024),
					new SqlParameter("@CanViewUserName", SqlDbType.VarChar,1024),
					new SqlParameter("@Attachment", SqlDbType.VarChar,200),
					new SqlParameter("@Memoer", SqlDbType.Int,4),
					new SqlParameter("@MemoDate", SqlDbType.DateTime),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50),
                    new SqlParameter("@Status", SqlDbType.VarChar,50),
                     new SqlParameter("@ID", SqlDbType.Int,0) };
            parameters[0].Value = model.CompanyCD;
            parameters[1].Value = model.MemoNo;
            parameters[2].Value = model.TItle;
            parameters[3].Value = model.Content;
            parameters[4].Value = model.CanViewUser;
            parameters[5].Value = model.CanViewUserName;
            parameters[6].Value = model.Attachment;
            parameters[7].Value = model.Memoer;
            parameters[8].Value = model.MemoDate;
            parameters[9].Value = model.Creator;
            parameters[10].Value = model.CreateDate;
            parameters[11].Value = model.ModifiedDate;
            parameters[12].Value = model.ModifiedUserID;
            parameters[13].Value = model.Status;

            parameters[14].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);
            return int.Parse(parameters[14].Value.ToString());
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(XBase.Model.Personal.Memo.PersonalMemo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [officedba].PersonalMemo set ");
            strSql.Append("CompanyCD=@CompanyCD,");
            strSql.Append("MemoNo=@MemoNo,");
            strSql.Append("TItle=@TItle,");
            strSql.Append("Content=@Content,");
            strSql.Append("CanViewUser=@CanViewUser,");
            strSql.Append("CanViewUserName=@CanViewUserName,");
            strSql.Append("Attachment=@Attachment,");
            strSql.Append("Memoer=@Memoer,");
            strSql.Append("MemoDate=@MemoDate,");
            strSql.Append("Creator=@Creator,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("ModifiedDate=@ModifiedDate,");
            strSql.Append("ModifiedUserID=@ModifiedUserID,");
            strSql.Append("Status=@Status");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@MemoNo", SqlDbType.VarChar,50),
					new SqlParameter("@TItle", SqlDbType.VarChar,100),
					new SqlParameter("@Content", SqlDbType.NText),
					new SqlParameter("@CanViewUser", SqlDbType.VarChar,1024),
					new SqlParameter("@CanViewUserName", SqlDbType.VarChar,1024),
					new SqlParameter("@Attachment", SqlDbType.VarChar,200),
					new SqlParameter("@Memoer", SqlDbType.Int,4),
					new SqlParameter("@MemoDate", SqlDbType.DateTime),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50),
                    new SqlParameter("@Status", SqlDbType.VarChar,50)
                                        };
            parameters[0].Value = model.ID;
            parameters[1].Value = model.CompanyCD;
            parameters[2].Value = model.MemoNo;
            parameters[3].Value = model.TItle;
            parameters[4].Value = model.Content;
            parameters[5].Value = model.CanViewUser;
            parameters[6].Value = model.CanViewUserName;
            parameters[7].Value = model.Attachment;
            parameters[8].Value = model.Memoer;
            parameters[9].Value = model.MemoDate;
            parameters[10].Value = model.Creator;
            parameters[11].Value = model.CreateDate;
            parameters[12].Value = model.ModifiedDate;
            parameters[13].Value = model.ModifiedUserID;
            parameters[14].Value = model.Status;

            SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [officedba].PersonalMemo ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public XBase.Model.Personal.Memo.PersonalMemo GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,CompanyCD,MemoNo,TItle,Content,CanViewUser,CanViewUserName,Attachment,Memoer,MemoDate,Creator,CreateDate,ModifiedDate,ModifiedUserID,Status from [officedba].PersonalMemo ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            XBase.Model.Personal.Memo.PersonalMemo model = new XBase.Model.Personal.Memo.PersonalMemo();
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
                model.MemoNo = ds.Tables[0].Rows[0]["MemoNo"].ToString();
                model.TItle = ds.Tables[0].Rows[0]["TItle"].ToString();
                model.Content = ds.Tables[0].Rows[0]["Content"].ToString();
                model.CanViewUser = ds.Tables[0].Rows[0]["CanViewUser"].ToString();
                model.CanViewUserName = ds.Tables[0].Rows[0]["CanViewUserName"].ToString();
                model.Attachment = ds.Tables[0].Rows[0]["Attachment"].ToString();
                if (ds.Tables[0].Rows[0]["Memoer"].ToString() != "")
                {
                    model.Memoer = int.Parse(ds.Tables[0].Rows[0]["Memoer"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MemoDate"].ToString() != "")
                {
                    model.MemoDate = DateTime.Parse(ds.Tables[0].Rows[0]["MemoDate"].ToString());
                }
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
                model.Status = ds.Tables[0].Rows[0]["Status"].ToString();
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
            strSql.Append("select ID,CompanyCD,MemoNo,TItle,Content,CanViewUser,CanViewUserName,Attachment,Memoer,MemoDate,Creator,CreateDate,ModifiedDate,ModifiedUserID ");
            strSql.Append(" FROM [officedba].PersonalMemo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DBHelper.SqlHelper.ExecuteSql(strSql.ToString());
        }


        #endregion  成员方法


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
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[PersonalMemo_GetPageData]", prams);
            dt = ds.Tables[0];
            return Convert.ToInt32(prams[prams.Length - 1].Value);
        }

    }
}

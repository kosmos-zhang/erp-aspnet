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
    /// 数据访问类MobileMsgMonitor。
    /// </summary>
    public class MobileMsgMonitor
    {
        public MobileMsgMonitor()
        { }
        #region  成员方法
            

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from officedba.MobileMsgMonitor");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return SqlHelper.ExecuteSql(strSql.ToString(), parameters).Rows.Count > 0;
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(XBase.Model.Personal.MessageBox.MobileMsgMonitor model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.MobileMsgMonitor(");
            strSql.Append("CompanyCD,MsgType,SendUserID,SendUserName,ReceiveUserID,ReceiveUserName,ReceiveMobile,Content,Status,CreateDate,SendDate)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@MsgType,@SendUserID,@SendUserName,@ReceiveUserID,@ReceiveUserName,@ReceiveMobile,@Content,@Status,@CreateDate,@SendDate)");
            strSql.Append(";select @ID=@@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
                    new SqlParameter("@MsgType", SqlDbType.Char,1),
					new SqlParameter("@SendUserID", SqlDbType.Int,4),
					new SqlParameter("@SendUserName", SqlDbType.VarChar,100),
					new SqlParameter("@ReceiveUserID", SqlDbType.Int,4),
					new SqlParameter("@ReceiveUserName", SqlDbType.VarChar,100),
					new SqlParameter("@ReceiveMobile", SqlDbType.VarChar,20),
					new SqlParameter("@Content", SqlDbType.VarChar,1024),
					new SqlParameter("@Status", SqlDbType.Char,1),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@SendDate", SqlDbType.DateTime),
                     new SqlParameter("@ID", SqlDbType.Int,0) };
            parameters[0].Value = model.CompanyCD;
            parameters[1].Value = model.MsgType;
            parameters[2].Value = model.SendUserID;
            parameters[3].Value = model.SendUserName;
            parameters[4].Value = model.ReceiveUserID;
            parameters[5].Value = model.ReceiveUserName;
            parameters[6].Value = model.ReceiveMobile;
            parameters[7].Value = model.Content;
            parameters[8].Value = model.Status;
            parameters[9].Value = model.CreateDate;
            parameters[10].Value = model.SendDate;
            parameters[11].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);
            return int.Parse(parameters[11].Value.ToString());
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(XBase.Model.Personal.MessageBox.MobileMsgMonitor model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.MobileMsgMonitor set ");
            strSql.Append("CompanyCD=@CompanyCD,");
            strSql.Append("MsgType=@MsgType,");
            strSql.Append("SendUserID=@SendUserID,");
            strSql.Append("SendUserName=@SendUserName,");
            strSql.Append("ReceiveUserID=@ReceiveUserID,");
            strSql.Append("ReceiveUserName=@ReceiveUserName,");
            strSql.Append("ReceiveMobile=@ReceiveMobile,");
            strSql.Append("Content=@Content,");
            strSql.Append("Status=@Status,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("SendDate=@SendDate");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
                     new SqlParameter("@MsgType", SqlDbType.Char,1),
					new SqlParameter("@SendUserID", SqlDbType.Int,4),
					new SqlParameter("@SendUserName", SqlDbType.VarChar,100),
					new SqlParameter("@ReceiveUserID", SqlDbType.Int,4),
					new SqlParameter("@ReceiveUserName", SqlDbType.VarChar,100),
					new SqlParameter("@ReceiveMobile", SqlDbType.VarChar,20),
					new SqlParameter("@Content", SqlDbType.VarChar,1024),
					new SqlParameter("@Status", SqlDbType.Char,1),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@SendDate", SqlDbType.DateTime)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.CompanyCD;
            parameters[2].Value = model.MsgType;
            parameters[3].Value = model.SendUserID;
            parameters[4].Value = model.SendUserName;
            parameters[5].Value = model.ReceiveUserID;
            parameters[6].Value = model.ReceiveUserName;
            parameters[7].Value = model.ReceiveMobile;
            parameters[8].Value = model.Content;
            parameters[9].Value = model.Status;
            parameters[10].Value = model.CreateDate;
            parameters[11].Value = model.SendDate;

            SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete officedba.MobileMsgMonitor ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            SqlHelper.ExecuteTransSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public XBase.Model.Personal.MessageBox.MobileMsgMonitor GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,CompanyCD,MsgType,SendUserID,SendUserName,ReceiveUserID,ReceiveUserName,ReceiveMobile,Content,Status,CreateDate,SendDate from officedba.MobileMsgMonitor ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            XBase.Model.Personal.MessageBox.MobileMsgMonitor model = new XBase.Model.Personal.MessageBox.MobileMsgMonitor();
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
                model.MsgType = ds.Tables[0].Rows[0]["MsgType"].ToString();
                if (ds.Tables[0].Rows[0]["SendUserID"].ToString() != "")
                {
                    model.SendUserID = int.Parse(ds.Tables[0].Rows[0]["SendUserID"].ToString());
                }
                model.SendUserName = ds.Tables[0].Rows[0]["SendUserName"].ToString();
                if (ds.Tables[0].Rows[0]["ReceiveUserID"].ToString() != "")
                {
                    model.ReceiveUserID = int.Parse(ds.Tables[0].Rows[0]["ReceiveUserID"].ToString());
                }
                model.ReceiveUserName = ds.Tables[0].Rows[0]["ReceiveUserName"].ToString();
                model.ReceiveMobile = ds.Tables[0].Rows[0]["ReceiveMobile"].ToString();
                model.Content = ds.Tables[0].Rows[0]["Content"].ToString();
                model.Status = ds.Tables[0].Rows[0]["Status"].ToString();
                if (ds.Tables[0].Rows[0]["CreateDate"].ToString() != "")
                {
                    model.CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SendDate"].ToString() != "")
                {
                    model.SendDate = DateTime.Parse(ds.Tables[0].Rows[0]["SendDate"].ToString());
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,CompanyCD,MsgType,SendUserID,SendUserName,ReceiveUserID,ReceiveUserName,ReceiveMobile,Content,Status,CreateDate,SendDate ");
            strSql.Append(" FROM officedba.MobileMsgMonitor ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DBHelper.SqlHelper.ExecuteSql(strSql.ToString());
        }


        /// <summary>
        /// GetPageData
        /// </summary>
        /// <param name="table"></param>
        /// <param name="key"></param>
        /// <param name="where"></param>
        /// <param name="fields"></param>
        /// <param name="orderExp"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public int GetPageData(out DataTable dt,  string where, string fields, string orderExp, int pageindex, int pagesize)
        {
            /*
             set @table='[officedba].VflowMyApply'
            set @keyfield = '[ID]'
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
            string table = "officedba.MobileMsgMonitor";
            string key = "[ID]";

            SqlParameter[] prams = {
                                       SqlParameterHelper.MakeInParam("@table",SqlDbType.NVarChar,0,table),
                                       SqlParameterHelper.MakeInParam("@keyfield",SqlDbType.NVarChar,0,key),
									SqlParameterHelper.MakeInParam("@OrderExp",SqlDbType.NVarChar,0,orderExp),
									SqlParameterHelper.MakeInParam("@fields",SqlDbType.NVarChar,0,fields),
									SqlParameterHelper.MakeInParam("@where",SqlDbType.NVarChar,0,where),

									SqlParameterHelper.MakeInParam("@pageSize",SqlDbType.Int,0,pagesize),
									SqlParameterHelper.MakeInParam("@pageIndex",SqlDbType.Int,0,pageindex),
									SqlParameterHelper.MakeParam("@RecsCount",SqlDbType.Int,0,ParameterDirection.Output,null)
									
								   };
            DataSet ds = SqlHelper.ExecuteDataset("", "[dbo].GetPageData", prams);
            dt = ds.Tables[0];
            return Convert.ToInt32(prams[prams.Length - 1].Value);
        }



        #endregion  成员方法
    }
}

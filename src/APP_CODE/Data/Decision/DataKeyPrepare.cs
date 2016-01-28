using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Model;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;

namespace XBase.Data.Decision
{
    public class DataKeyPrepare
    {
        private DataSet ds = new DataSet();

        /// <summary>
        /// 获取所有的信息
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet GetDataKeyPrepareAll()
        {
            SqlParameter[] parameters = new SqlParameter[]{
			};
            ds = null;
            Database.RunProc("[statdba].DataKeyPrepare_Select", parameters, out ds);
            return ds;
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="cond">查询条件,多个条件应该以And连接</param>
        /// <param name="orderExp">展现顺序</param>
        /// <returns>List</returns>
        public IList<XBase.Model.Decision.DataKeyPrepare> GetDataKeyPrepareListbyCond(string cond, string orderExp)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                SqlParameterHelper.MakeInParam("@Where",SqlDbType.VarChar,100,cond),
                SqlParameterHelper.MakeInParam("@OrderExp",SqlDbType.VarChar,50,orderExp)
			};
            XBase.Model.Decision.DataKeyPrepare entity = null;
            List<XBase.Model.Decision.DataKeyPrepare> lst = new List<XBase.Model.Decision.DataKeyPrepare>();
            SqlDataReader dr;
            Database.RunProc("[statdba].DataKeyPrepare_SelectbyCond", parameters, out dr);
            while (dr.Read())
            {
                entity = GetList(dr);
                lst.Add(entity);
            }
            return lst;
        }

        #region 私有函数
        /// <summary>
        /// 填充一个实体类
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private XBase.Model.Decision.DataKeyPrepare GetList(SqlDataReader dr)
        {
            XBase.Model.Decision.DataKeyPrepare entity = new XBase.Model.Decision.DataKeyPrepare();
            entity.DataID = SqlClientUtility.GetInt32(dr, "DataID", 0);
            entity.DataName = SqlClientUtility.GetString(dr, "DataName", String.Empty);
            entity.DataVar = SqlClientUtility.GetString(dr, "DataVar", String.Empty);
            entity.DataVal = SqlClientUtility.GetString(dr, "DataVal", String.Empty);
            entity.Attachment = SqlClientUtility.GetString(dr, "Attachment", String.Empty);
            entity.DataSrc = SqlClientUtility.GetString(dr, "DataSrc", String.Empty);
            entity.IsCond = SqlClientUtility.GetString(dr, "IsCond", String.Empty);
            return entity;
        }
        #endregion
    }
}

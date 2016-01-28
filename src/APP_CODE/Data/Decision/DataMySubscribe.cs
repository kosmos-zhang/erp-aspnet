using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace XBase.Data.Decision
{
    public class DataMySubscribe
    {
        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool AddDataMySubscribe(XBase.Model.Decision.DataMySubscribe entity,out int Id)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                SqlParameterHelper.MakeInParam("@CompanyCD",SqlDbType.VarChar,8,entity.CompanyCD),
				SqlParameterHelper.MakeInParam("@DataID",SqlDbType.VarChar,100,entity.DataID),
				SqlParameterHelper.MakeInParam("@DataName",SqlDbType.VarChar,400,entity.DataName),
				SqlParameterHelper.MakeInParam("@DataVarValue",SqlDbType.VarChar,1000,entity.DataVarValue),
				SqlParameterHelper.MakeInParam("@Conditions",SqlDbType.VarChar,100,entity.Conditions),
                SqlParameterHelper.MakeInParam("@MyMobile",SqlDbType.VarChar,100,entity.MyMobile),
                SqlParameterHelper.MakeInParam("@Creator",SqlDbType.VarChar,20,entity.Creator),
                SqlParameterHelper.MakeInParam("@CreateDate",SqlDbType.DateTime,8,entity.CreateDate),
                SqlParameterHelper.MakeInParam("@DataNote",SqlDbType.VarChar,200,entity.DataNote),
                SqlParameterHelper.MakeInParam("@Frequency",SqlDbType.VarChar,50,entity.Frequency),
                SqlParameterHelper.MakeInParam("@Format",SqlDbType.VarChar,200,entity.Format),
                SqlParameterHelper.MakeOutParam("@ID",SqlDbType.Int,6)
			};
            bool ret;
            Database.RunProc("statdba.DataMySubscribe_Create", parameters,out ret);
            Id = Convert.ToInt32(parameters[11].Value);
            return ret;
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="id"></param>
        public bool DelDataMySubscrible(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]{
				SqlParameterHelper.MakeInParam("@ID",SqlDbType.Int,4,id)
             };
            bool ret;
            Database.RunProc("statdba.DataMySubscribe_Delete", parameters, out ret);
            return ret;
        }

        /// <summary>
        /// 修改一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool ModDataMySubscrible(XBase.Model.Decision.DataMySubscribe entity)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                SqlParameterHelper.MakeInParam("@ID",SqlDbType.Int,4,entity.ID),
                SqlParameterHelper.MakeInParam("@DataId",SqlDbType.VarChar,100,entity.DataID),
                SqlParameterHelper.MakeInParam("@DataName",SqlDbType.VarChar,400,entity.DataName),
                SqlParameterHelper.MakeInParam("@DataVarValue",SqlDbType.VarChar,1000,entity.DataVarValue),
				SqlParameterHelper.MakeInParam("@Conditions",SqlDbType.VarChar,100,entity.Conditions),
                SqlParameterHelper.MakeInParam("@MyMobile",SqlDbType.VarChar,100,entity.MyMobile),
                SqlParameterHelper.MakeInParam("@DataNote",SqlDbType.VarChar,200,entity.DataNote),
                SqlParameterHelper.MakeInParam("@Frequency",SqlDbType.VarChar,50,entity.Frequency),
                SqlParameterHelper.MakeInParam("@Format",SqlDbType.VarChar,200,entity.Format)
			};
            
            bool ret;
            Database.RunProc("statdba.DataMySubscribe_Modify", parameters, out ret);
            return ret;
        }


        /// <summary>
        /// 获取所有信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetDataMySubscribleALL(string CompanyCD)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                 SqlParameterHelper.MakeInParam("@CompanyCD",SqlDbType.VarChar,8,CompanyCD),
			};
            DataSet ds = null;
            Database.RunProc("statdba.DataMySubscribe_Select", parameters, out ds);
            return ds;
        }


        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="cond">查询条件,多个条件应该以And连接</param>
        /// <param name="orderExp">展现顺序</param>
        /// <returns>List</returns>
        public IList<XBase.Model.Decision.DataMySubscribe> GetDataMySubscribleListbyCond(string cond, string orderExp)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                SqlParameterHelper.MakeInParam("@Where",SqlDbType.VarChar,100,cond),
                SqlParameterHelper.MakeInParam("@OrderExp",SqlDbType.VarChar,50,orderExp)
			};
            XBase.Model.Decision.DataMySubscribe entity = null;
            List<XBase.Model.Decision.DataMySubscribe> lst = new List<XBase.Model.Decision.DataMySubscribe>();
            SqlDataReader dr;
            Database.RunProc("statdba.DataMySubscribe_SelectbyCond", parameters, out dr);
            while (dr.Read())
            {
                entity = GetList(dr);
                lst.Add(entity);
            }
            return lst;
        }

        /// <summary>
        /// 查询已经订阅指标
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public DataTable GetGetDataName(string CompanyCD)
        {
            SqlParameter[] prams = {									
									SqlParameterHelper.MakeInParam("@CompanyCD",SqlDbType.VarChar,8,CompanyCD)
                                   };
            DataSet ds;
            Database.RunProc("statdba.DataMySubscribe_DataName", prams, out ds);
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取数据订阅的分页数据
        /// </summary>
        /// <param name="tb">输出的数据表</param>
        /// <param name="PageSize">页尺寸</param>
        /// <param name="PageIndex">页索引</param>
        /// <param name="queryCondition">查询条件</param>
        /// <param name="sortExp">排序表达式如： ID ASC</param>
        /// <param name="fieldList">查询的字段列表</param>
        /// <returns>记录总数</returns>
        public int GetPageData(out DataTable tb, int PageSize, int PageIndex, string queryCondition, string sortExp, string fieldList)
        {
            /*
                @fields varchar(512),
                @where varchar(200),
                @OrderExp varchar(50),
                @pageIndex int,
                @pageSize int,
                @RecsCount int output
             */
            SqlParameter[] prams = {									
									SqlParameterHelper.MakeInParam("@OrderExp",SqlDbType.NVarChar,0,sortExp),
									SqlParameterHelper.MakeInParam("@fields",SqlDbType.NVarChar,0,fieldList),
									SqlParameterHelper.MakeInParam("@where",SqlDbType.NVarChar,0,queryCondition),

									SqlParameterHelper.MakeInParam("@pageSize",SqlDbType.Int,0,PageSize),
									SqlParameterHelper.MakeInParam("@pageIndex",SqlDbType.Int,0,PageIndex),
									SqlParameterHelper.MakeParam("@RecsCount",SqlDbType.Int,0,ParameterDirection.Output,null)
									
								   };

            DataSet ds;
            Database.RunProc("[statdba].[DataMySubscribe_GetPageData]", prams, out ds);
            tb = ds.Tables[0];

            int recCount = Convert.ToInt32(prams[prams.Length - 1].Value);

            return recCount;
        }

        public int CheckRecord(int Id)
        {
            SqlParameter[] prams = {									
									SqlParameterHelper.MakeInParam("@Id",SqlDbType.Int,0,Id)
									
								   };

            DataSet ds;
            Database.RunProc("statdba.DataMySubscribe_check", prams, out ds);
            DataTable tb = ds.Tables[0];

            int recCount =Convert.ToInt32(tb.Rows[0][0]);

            return recCount;
        }

        /// <summary>
        /// 根据Id获取产品信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataTable GetProductById(int Id)
        {
            SqlParameter[] prams = {									
									SqlParameterHelper.MakeInParam("@Id",SqlDbType.Int,0,Id)
								   };
            DataSet ds=Database.RunSql("select * from officedba.productInfo where [id]=@Id", prams);
            if(ds.Tables.Count>0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 根据Id获取部门信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataTable GetDeptById(int Id)
        {
            SqlParameter[] prams = {									
									SqlParameterHelper.MakeInParam("@Id",SqlDbType.Int,0,Id)
								   };
            DataSet ds = Database.RunSql("select * from officedba.DeptInfo where [id]=@Id", prams);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

        #region [私有函数]
        /// <summary>
        /// 添加一个实体类
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private XBase.Model.Decision.DataMySubscribe GetList(SqlDataReader dr)
        {
            XBase.Model.Decision.DataMySubscribe entity = new XBase.Model.Decision.DataMySubscribe();
            entity.ID = SqlClientUtility.GetInt32(dr, "ID", 0);
            entity.CompanyCD = SqlClientUtility.GetString(dr, "CompanyCD", String.Empty);
            entity.DataID = SqlClientUtility.GetString(dr, "DataID",String.Empty);
            entity.DataName = SqlClientUtility.GetString(dr, "DataName", String.Empty);
            entity.DataVarValue = SqlClientUtility.GetString(dr, "DataVarValue", String.Empty);
            entity.Conditions = SqlClientUtility.GetString(dr, "Conditions", String.Empty);
            entity.MyMobile = SqlClientUtility.GetString(dr, "MyMobile", String.Empty);
            entity.Creator = SqlClientUtility.GetString(dr, "Creator", String.Empty);
            entity.CreateDate = SqlClientUtility.GetDateTime(dr, "CreateDate", DateTime.Now);
            entity.DataNote = SqlClientUtility.GetString(dr, "DataNote", String.Empty);
            entity.Frequency = SqlClientUtility.GetString(dr, "Frequency", String.Empty);
            entity.Format = SqlClientUtility.GetString(dr, "Format", String.Empty);
            return entity;
        }

        #endregion
    }
}

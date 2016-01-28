/**********************************************
 * 类作用：  数据订阅
 * 建立人：   朱贤兆
 * 建立时间： 2009/06/05
 ***********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;

namespace XBase.Data.Decision
{
    public  class ActionDetail
    {
        private DataSet ds = null;

        /// <summary>
        /// 获取所有的信息
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet GetAll()
        {
            SqlParameter[] parameters = new SqlParameter[]{
			};

            ds = Database.RunSql("select * from [statdba].DataActionDetail", parameters);
            return ds;
        }

        public DataSet GetModel(int Id)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                 SqlParameterHelper.MakeInParam("@Id",SqlDbType.Int,4,Id)
			};

            ds = Database.RunSql("select * from [statdba].DataActionDetail where [Id]=@Id", parameters);
            return ds;
        }

        /// <summary>
        /// 根据动作Id获取所有的列表
        /// </summary>
        /// <returns>DataSet</returns>
        /// 
        public DataSet GetListByActionId(int ActionId)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                SqlParameterHelper.MakeInParam("@Id",SqlDbType.Int,4,ActionId)
			};

            ds = Database.RunSql("select * from [statdba].DataActionDetail where [ActionId]=@Id", parameters);
            return ds;
        }
    }
}

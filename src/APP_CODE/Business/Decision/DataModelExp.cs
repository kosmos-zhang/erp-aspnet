using System;
using System.Data;
using System.Collections.Generic;

namespace XBase.Business.Decision
{
	/// <summary>
	/// 业务逻辑类DataModelExp 的摘要说明。
	/// </summary>
	public class DataModelExp
	{
        private readonly XBase.Data.Decision.DataModelExp dal = new XBase.Data.Decision.DataModelExp();
		public DataModelExp()
		{}
		#region  成员方法

		

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			return dal.Exists(ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public void Add(XBase.Model.Decision.DataModelExp model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public void Update(XBase.Model.Decision.DataModelExp model)
		{
			dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int ID)
		{
			
			dal.Delete(ID);
		}

        /// <summary>
        /// 删除多条数据
        /// </summary>
        public void Delete(string strWhere)
        {
            dal.Delete(strWhere);
        }

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public XBase.Model.Decision.DataModelExp GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

	

		#endregion  成员方法
	}
}


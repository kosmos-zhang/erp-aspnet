using System;
using System.Data;
using System.Collections.Generic;

namespace XBase.Business.Personal.Culture
{
	/// <summary>
	/// 业务逻辑类CultureDocs 的摘要说明。
	/// </summary>
	public class CultureDocs
	{
		private readonly XBase.Data.Personal.Culture.CultureDocs dal=new XBase.Data.Personal.Culture.CultureDocs();
		public CultureDocs()
		{}
		#region  成员方法

          /// <summary>
        /// 是否存在该记录
        /// </summary>
        public int Exists(string where)
        {
            return dal.Exists(where);
        }
		

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
		public int  Add(XBase.Model.Personal.Culture.CultureDocs model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(XBase.Model.Personal.Culture.CultureDocs model)
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
		/// 得到一个对象实体
		/// </summary>
		public XBase.Model.Personal.Culture.CultureDocs GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

	

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataTable GetList(string strWhere)
		{
			return dal.GetList(strWhere);
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
            return dal.GetPageData(out dt, where, fields, orderExp, pageindex, pagesize);
        }

		#endregion  成员方法
	}
}

